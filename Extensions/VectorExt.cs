using Microsoft.Xna.Framework;
using System;
namespace StoneShard_Mono.Extensions
{
    public static class VectorExt
    {
        public static float GetRadian(this Vector2 v) => MathF.Atan2(v.Y, v.X);

        public static Vector2 GetAngle(this float rad)
        {
            return new Vector2((float)Math.Cos((double)rad), (float)Math.Sin((double)rad));
        }
    }
}
