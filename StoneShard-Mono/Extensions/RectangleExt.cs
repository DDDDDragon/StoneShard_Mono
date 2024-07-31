using Microsoft.Xna.Framework;

namespace StoneShard_Mono.Extensions
{
    public static class RectangleExt
    {
        public static Rectangle StretchDown(this Rectangle rect, int Height)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + Height);
        }

        public static Rectangle Divide(this Rectangle rect, int num)
        {
            return new Rectangle(rect.X / num, rect.Y / num, rect.Width / num, rect.Height / num);
        }
    }
}
