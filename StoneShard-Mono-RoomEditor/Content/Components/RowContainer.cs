using Microsoft.Xna.Framework;
using System;

namespace StoneShard_Mono_RoomEditor.Content.Components
{
    public class RowContainer : Container
    {
        public RowContainer() { }

        public int ChildrenMargin = 0;

        public override void Update(GameTime gameTime)
        {
            _width = 0;

            foreach (var component in Children)
                _height = Math.Max(component.Height, Height);
            foreach (var component in Children)
            {
                component.RelativePosition.X = _width;
                component.RelativePosition.Y = 0;
                _width += component.Width + ChildrenMargin;
                component.Update(gameTime);
            }

            base.Update(gameTime);

            if (!_init) _init = true;
        }
    }
}
