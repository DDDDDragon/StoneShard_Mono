using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Managers;
using StoneShard_Mono.Utils;
using System;
using StoneShard_Mono.Content.Players;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Content.Components;
using StoneShard_Mono.Content.Animations;
using StoneShard_Mono.Content.Rooms.Osbrook;
using FontStashSharp;

namespace StoneShard_Mono.Content.Scenes
{
    public class GameScene : Scene
    {
        public GameScene()
        {
            CurrentRoom = ContentInstance<tavernInside_floor_1>.NewInstance();

            Timer = new();

            TurnController = new();

            selectBox = Main.TextureManager[TexType.Tile, "selectBox"];

            wayPoint = Main.TextureManager[TexType.Tile, "wayPoint"];
        }

        public Room CurrentRoom;

        public TurnController TurnController;

        public Player Player => CurrentRoom.Player;

        public Texture2D selectBox;

        public Texture2D wayPoint;

        public MouseState PreviousMouseState;

        public MouseState MouseState;

        public Vector2 PreviousMouseTile;

        public Vector2 MouseTile;

        public Timer Timer;

        public bool DrawSelectBox = true;

        public bool DrawPath = true;

        public bool InBattle = false;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawRectangle(new(0, 0, Main.GameWidth, Main.GameHeight), GameColors.RoomDark, layerDepth: 1);

            CurrentRoom?.Draw(spriteBatch, gameTime);

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
        }

        public bool Reachable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= (int)CurrentRoom.TileMapSize.X || y >= (int)CurrentRoom.TileMapSize.Y)
                return false;
            return CurrentRoom[x, y] == 0;
        }

        public override void Update(GameTime gameTime)
        {
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
        }

        public void GoToRoom(Room room)
        {
            CurrentRoom = room;
            room.Enter();
        }
    }
}
