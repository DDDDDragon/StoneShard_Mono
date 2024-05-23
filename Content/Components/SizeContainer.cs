

using Microsoft.Xna.Framework;

namespace StoneShard_Mono.Content.Components
{
    public class SizeContainer : Container
    {
        public SizeContainer(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public SizeContainer(Vector2 size)
        {
            _width = (int)size.X;
            _height = (int)size.Y;
        }
    }
}
