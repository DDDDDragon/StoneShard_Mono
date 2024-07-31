using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StoneShard_Mono.Extensions;
using StoneShard_Mono.Content.Components;

namespace StoneShard_Mono.Content.Scenes
{
    public class Scene
    {
        public Main Instace => Main.Instance;

        public GraphicsDevice Device => Instace.GraphicsDevice;

        public Texture2D Background = null;

        public Color BackgroundColor = default;

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Background != null)
                spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            else if (BackgroundColor != default)
                spriteBatch.DrawRectangle(new(0, 0, Main.GameWidth, Main.GameHeight), BackgroundColor);
            foreach (var component in Components)
                component.Draw(spriteBatch, gameTime);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }

        public List<Component> Components { get; set; } = new List<Component>();
    }
}
