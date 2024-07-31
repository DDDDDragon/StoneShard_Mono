using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono.Managers;

namespace StoneShard_Mono.Content.Tiles
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for(int i = 0; i < TileSize.X; i++)
            {
                for(int j = 0; j < TileSize.Y; j++)
                {
                    CurrentRoom[j + (int)TilePosition.Y, i + (int)TilePosition.X] = Main.TileID[GetType().Name];
                }
            }
        }

        public override void SetStaticDefaults()
        {
            if (Main.TileID.ContainsKey(GetType().Name)) return;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + DrawOffset + TextureOffset, null, Color.White * Alpha, 0, Vector2.Zero, 1f, SpriteEffects.None, 1 - TilePosition.Y / 1000);
        }
    }
}
