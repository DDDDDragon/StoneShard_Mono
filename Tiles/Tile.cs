using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneShard_Mono.UIComponents;

namespace StoneShard_Mono.Tiles
{
    public class Tile : Entity
    {
        public Vector2 TileSize;

        public Vector2 TilePos;

        public Texture2D Texture;

        public virtual void SetDefaults()
        {

        }
    }
}
