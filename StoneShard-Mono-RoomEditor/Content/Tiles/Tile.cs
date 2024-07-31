using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono_RoomEditor.Managers;

namespace StoneShard_Mono_RoomEditor.Content.Tiles
{
    public abstract class Tile : Entity
    {
        public virtual Texture2D Texture { get; set; }

        public Vector2 TileSize;

        public int SubID 
        { 
            get {
                return _subID;
            }
            set {
                _subID = value;
                SetSubTexture(value);
            } 
        }

        private int _subID;

        public override int Width => Texture.Width;

        public override int Height => Texture.Height;

        public override Vector2 Position 
        { 
            get => (TilePosition + CurrentRoom.TilePostion) * Main.TileSize; 
        }

        public Vector2 TextureOffset;

        public virtual void SetSubTexture(int subID)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + DrawOffset + TextureOffset, null, Color.White * Alpha, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 - TilePosition.Y / 1000);
        }
    }
}
