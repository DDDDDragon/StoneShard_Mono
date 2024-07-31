using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace StoneShard_Mono_RoomEditor.Extensions
{
    public static class VectorExt
    {
        public static float GetRadian(this Vector2 v) => MathF.Atan2(v.Y, v.X);

        public static Vector2 GetAngle(this float rad)
        {
            return new Vector2((float)Math.Cos((double)rad), (float)Math.Sin((double)rad));
        }

        public static List<Vector2> Neighbors(this Vector2 v)
        {
            return new() 
            { 
                new(v.X, v.Y + 1),
                new(v.X - 1, v.Y + 1),
                new(v.X - 1, v.Y),
                new(v.X - 1, v.Y - 1),
                new(v.X, v.Y - 1),
                new(v.X + 1, v.Y - 1),
                new(v.X + 1, v.Y),
                new(v.X + 1, v.Y + 1),
            };
        }
    }
}
