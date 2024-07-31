using Microsoft.Xna.Framework;

namespace StoneShard_Mono_RoomEditor.Extensions
{
    public static class PointExt
    {
        public static Point Scale(this Point point, int scale) 
        {
            return new Point(point.X * scale, point.Y * scale);
        }
    }
}
