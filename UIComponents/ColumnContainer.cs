using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace StoneShard_Mono.UIComponents
{
    public class ColumnContainer : Container
    {
        public ColumnContainer() { }

        public int ChildrenMargin = 0;

        public override void Update(GameTime gameTime)
        {
            _height = 0;

            foreach (var component in Children)
                _width = Math.Max(component.Width, Width);
            foreach (var component in Children)
            {
                component.RelativePosition = new(0, _height);
                _height += component.Height + ChildrenMargin;
                component.Update(gameTime);
            }
            base.Update(gameTime);
            if (!_init) _init = true;
        }
    }
}
