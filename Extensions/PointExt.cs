using Microsoft.Xna.Framework;

namespace StoneShard_Mono.Extensions
{
    public static class PointExt
    {
        public static Point Scale(this Point point, int scale) 
        {
            return new Point(point.X * scale, point.Y * scale);
        }
    }
}
