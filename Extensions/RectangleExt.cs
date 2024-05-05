using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneShard_Mono.Extensions
{
    public static class RectangleExt
    {
        public static Rectangle StretchDown(this Rectangle rect, int Height)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + Height);
        }
    }
}
