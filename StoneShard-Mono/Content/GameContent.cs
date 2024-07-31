using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using StoneShard_Mono.Content.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Content
{
    public abstract class GameContent
    {
        public virtual string Name { get; set; }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void SetDefaults()
        {

        }

        public virtual void SetStaticDefaults()
        {

        }
    }
}
