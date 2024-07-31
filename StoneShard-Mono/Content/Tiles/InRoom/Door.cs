using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Extensions;

namespace StoneShard_Mono.Content.Tiles.InRoom
{
    public class Door : Tile
    {
        public Vector2 realPos;

        public Room ToRoom;

        private bool _enterCheck = false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.TileID[GetType().Name] = 3;
        }

        public override void SetDefaults()
        {
            Mod = "StoneShard";

            TileSize = new(1, 1);

            UseAdditive = true;

            OnClick += (e, args) =>
            {
                if (Main.LocalPlayer.TilePosition == realPos && !Main.LocalPlayer.IsMove)
                {
                    Main.GameScene.GoToRoom(ToRoom);
                    Main.SetCursor("cursor");
                }

                _enterCheck = true;
            };
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.BackToFront, blendState: BlendState.Additive);
            if(_isHovering)
            {
                base.Draw(spriteBatch, gameTime);
            }
            spriteBatch.Rebegin(samplerState: SamplerState.PointClamp, rasterizerState: RasterizerState.CullNone, sortMode: SpriteSortMode.BackToFront);
        }

        public override void Update(GameTime gameTime)
        {
            if (_isHovering)
            {
                if (Main.CursorType == "cursor")
                    Main.SetCursor("cursor_exit");
                Main.GameScene.MouseTile = realPos;
                Main.GameScene.DrawSelectBox = false;
            }
            else if (Main.CursorType == "cursor_exit")
                Main.SetCursor("cursor");

            if(_enterCheck && !Main.LocalPlayer.IsMove)
            {
                if (Main.LocalPlayer.TilePosition == realPos)
                {
                    Main.GameScene.GoToRoom(ToRoom);
                    Main.SetCursor("cursor");
                }
                _enterCheck = false;
            }

            base.Update(gameTime);
        }
    }
}
