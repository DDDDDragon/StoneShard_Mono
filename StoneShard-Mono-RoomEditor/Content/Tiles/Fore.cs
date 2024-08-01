using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Extensions;

namespace StoneShard_Mono_RoomEditor.Content.Tiles
{
    public class OpaqueFore : Tile
    {
        public override void SetDefaults()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + DrawOffset, null, Color.White * Alpha, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 - (TilePosition.Y + Height / Main.TileSize - 1) / 1000);
        }
    }

    public class TransparentFore : Tile
    {
        public override void SetDefaults()
        {
            Alpha = 79f / 255;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + DrawOffset, null, Color.White * Alpha, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 - (TilePosition.Y + Height / Main.TileSize - 1) / 1000);
        }
    }
}
