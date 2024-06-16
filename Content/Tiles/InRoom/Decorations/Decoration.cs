using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content.Tiles.InRoom.Decorations
{
    public abstract class Decoration : Tile
    {
        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position + DrawOffset, null, Color.White * Alpha, 0, Vector2.Zero, 1f, SpriteEffects.None, 1);
        }
    }
}
