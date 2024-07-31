using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Utils;
using System;
using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Content.Components;
using StoneShard_Mono.Content.Animations;
using FontStashSharp;

namespace StoneShard_Mono.Content.Scenes
{
    public class GameScene : Scene
    {
        public GameScene()
        {
            Timer = new();

            TurnController = new();

            Screen = new SizeContainer(Main.GameSize) { Name = "Screen" };

            CreateBottomPanel();

            Components.Add(Screen);

            selectBox = Main.TextureManager[TexType.Tile, "selectBox"];

            wayPoint = Main.TextureManager[TexType.Tile, "wayPoint"];

            CurrentRoom = Room.Empty;
        }

        public Room CurrentRoom;

        private Room _targetRoom;

        public TurnController TurnController;

        public Player Player => CurrentRoom.LocalPlayer;

        public Texture2D selectBox;

        public Texture2D wayPoint;

        public MouseState PreviousMouseState;

        public MouseState MouseState;

        public Vector2 PreviousMouseTile;

        public Vector2 MouseTile;

        public Timer Timer;

        public SizeContainer Screen;

        private bool _changeRoom = false;

        public bool DrawSelectBox = true;

        public bool DrawPath = true;

        public bool InBattle = false;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(new(0, 0, Main.GameWidth, Main.GameHeight), GameColors.RoomDark, layerDepth: 1);

            CurrentRoom?.Draw(spriteBatch, gameTime);

            spriteBatch.DrawRectangle(new(0, 0, Main.GameWidth, Main.GameHeight), GameColors.RoomDark * (Timer[0] / 255), layerDepth: 0);

            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone);

            if (CurrentRoom is EmptyRoom) return;

            if (DrawSelectBox)
            {
                spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3 + CurrentRoom.TilePostion) * Main.TileSize + new Vector2(2, 2), Color.Black);

                if (Reachable((int)MouseTile.X, (int)MouseTile.Y))
                    spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3 + CurrentRoom.TilePostion) * Main.TileSize, Color.White);
                else
                    spriteBatch.Draw(selectBox, (MouseTile * 2 / 3 + PreviousMouseTile / 3 + CurrentRoom.TilePostion) * Main.TileSize, Color.Red);
            }

            var wayfinder = new WayFinder(delegate (Vector2 vec) { return Reachable((int)vec.X, (int)vec.Y); }, Player.TilePosition, MouseTile, CurrentRoom);

            if (!Player.IsMove)
            {
                if (Reachable((int)MouseTile.X, (int)MouseTile.Y))
                    Player.CurrentPath = wayfinder.FindPath();
                else Player.CurrentPath?.Clear();
            }

            if (DrawPath)
            {
                foreach (var item in Player.CurrentPath)
                {
                    var rect = new Rectangle(0, 0, wayPoint.Width, wayPoint.Height);
                    var scale = new Vector2((float)(0.7 + Math.Cos(gameTime.TotalGameTime.TotalMilliseconds / 200 + Player.CurrentPath.IndexOf(item)) / 6));
                    var offset = new Vector2(Main.TileSize - wayPoint.Width * scale.X) / 2;
                    var drawPos = item + CurrentRoom.TilePostion;

                    if (Player.CurrentPath.IndexOf(item) == 0)
                    {
                        if (Vector2.Distance(item, Player.TargetTilePos) == 1)
                        {
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        }
                        else
                        {
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, (drawPos + (Player.TargetTilePos - item) / 2) * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                            spriteBatch.Draw(wayPoint, (drawPos + (Player.TargetTilePos - item) / 2) * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        }
                    }
                    else
                    {
                        if (Vector2.Distance(item, Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1]) == 1)
                        {
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0.001f);
                        }
                        else
                        {
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, (drawPos + (Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1] - item) / 2) * Main.TileSize + offset + new Vector2(2, 2), rect, Color.Black, 0, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
                            spriteBatch.Draw(wayPoint, drawPos * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                            spriteBatch.Draw(wayPoint, (drawPos + (Player.CurrentPath[Player.CurrentPath.IndexOf(item) - 1] - item) / 2) * Main.TileSize + offset, rect, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                        }
                    }
                }
            }

            spriteBatch.DrawString(Main.FontManager["SSFont", 20], "Global: " + (MouseTile + CurrentRoom.TilePostion).ToString(), MouseState.Position.ToVector2() + new Vector2(30, 10), Color.White);
            spriteBatch.DrawString(Main.FontManager["SSFont", 20], "InRoom: " + MouseTile.ToString(), MouseState.Position.ToVector2() + new Vector2(30, 30), Color.White);

            foreach (var component in Components)
                component.Draw(spriteBatch, gameTime);
        }

        public bool Reachable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= (int)CurrentRoom.TileMapSize.X || y >= (int)CurrentRoom.TileMapSize.Y)
                return false;
            return CurrentRoom[x, y] == 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_changeRoom && Timer[0] < 255) //切换房间的过渡
                Timer[0] += 7;

            if (_changeRoom && Timer[0] > 255)
            {
                _changeRoom = false;

                _targetRoom.Enter(CurrentRoom);

                CurrentRoom = _targetRoom;

                _targetRoom = null;
            }

            if (!_changeRoom && Timer[0] > 0)
                Timer[0] -= 7;

            if (CurrentRoom is EmptyRoom) return;

            PreviousMouseState = MouseState;

            MouseState = Mouse.GetState();

            PreviousMouseTile = MouseTile;

            MouseTile = new Vector2(MouseState.X / Main.TileSize, MouseState.Y / Main.TileSize) - CurrentRoom.TilePostion;

            CurrentRoom?.Update(gameTime);

            if (Reachable((int)MouseTile.X, (int)MouseTile.Y) && Main.PlayerTurn)
            {
                if (PreviousMouseState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Released)
                {
                    if (Player.CurrentAnimation is EntityMoveChain moveChain && Player.CurrentAnimation.MaxTime != 0)
                        moveChain.ShouldBreak = true;
                    else if (!InBattle)
                    {
                        var chain = new EntityMoveChain();

                        foreach (var pos in Player.CurrentPath)
                            chain.RegisterAnimation(new EntityMoveAnimation(pos));

                        Player.PlayAnimation(chain);
                    }
                    else
                    {

                    }
                }

                if (PreviousMouseState.RightButton == ButtonState.Pressed && MouseState.RightButton == ButtonState.Released)
                {
                    var dir = (MouseTile - Player.TilePosition).X;
                    Player.Direction = dir > 0 ? 1 : dir == 0 ? Player.Direction : -1;
                    Player.TilePosition = MouseTile;
                    Player.LastTilePos = MouseTile;
                    Player.TargetTilePos = MouseTile;
                }
            }

            CurrentRoom.UpdatePlayer(gameTime);

            foreach (var component in Components)
                component.Update(gameTime);
        }

        public void GoToRoom(Room room)
        {
            _targetRoom = room;
            _changeRoom = true;
        }

        public void CreateBottomPanel()
        {
            var bottomPanel = new SizeContainer(Main.TextureManager[TexType.UI, "InGame\\bottomPanel"].GetSize());

            bottomPanel.HorizontalMiddle = true;

            bottomPanel.Anchor = Anchor.Bottom;

            bottomPanel.OnHover += (sender, args) =>
            {
                DrawSelectBox = false;
            };

            bottomPanel.RegisterChild(new UIImage("InGame\\bottomPanel"));

            var drawing = new EventHandler<(SpriteBatch spriteBatch, GameTime gameTime)>((obj, args) =>
            {
                var button = obj as Button;

                var destination = new Rectangle(button.Position.ToPoint() + (button.Size / 2).ToPoint(), button.Size.ToPoint());
                if (button._isHovering)
                {
                    if (button._currentMouse.LeftButton == ButtonState.Pressed)
                        args.spriteBatch.Draw(button.Press, destination, new(new(0, 0), button.Size.ToPoint()), Color.White * button.Alpha, button.Rotation, button.Size / 2, SpriteEffects.None, 0);
                    else
                        args.spriteBatch.Draw(button.Hover, destination, new(new(0, 0), button.Size.ToPoint()), Color.White * button.Alpha, button.Rotation, button.Size / 2, SpriteEffects.None, 0);
                }
                else
                    args.spriteBatch.Draw(button._texture, destination, new(new(0, 0), button.Size.ToPoint()), Color.White * button.Alpha, button.Rotation, button.Size / 2, SpriteEffects.None, 0);
            });

            bottomPanel.RegisterChild(new Button("InGame\\inventoryButton", hoverID: "InGame\\inventoryButtonHover", pressID: "InGame\\inventoryButtonPress",
                relativePos: new(88, 52), drawing: drawing)
            { Name = "inventoryButton" });

            bottomPanel.RegisterChild(new Button("InGame\\characterButton", hoverID: "InGame\\characterButtonHover", pressID: "InGame\\characterButtonPress",
                relativePos: new(130, 52), drawing: drawing)
            { Name = "characterButton" });

            bottomPanel.RegisterChild(new Button("InGame\\skillButton", hoverID: "InGame\\skillButtonHover", pressID: "InGame\\skillButtonPress",
                relativePos: new(172, 52), drawing: drawing)
            { Name = "skillButton" });

            bottomPanel.RegisterChild(new Button("InGame\\taskButton", hoverID: "InGame\\taskButtonHover", pressID: "InGame\\taskButtonPress",
                relativePos: new(214, 52), drawing: drawing)
            { Name = "taskButton" });

            Screen.RegisterChild(bottomPanel);
        }
    }
}
