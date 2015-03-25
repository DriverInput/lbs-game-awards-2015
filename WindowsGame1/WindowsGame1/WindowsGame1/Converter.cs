using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Converter
    {
        class Float
        {
            public static float Cos(float Angle)
            {
                return (float)Math.Cos(Angle);
            }

            public static float Sin(float Angle)
            {
                return (float)Math.Sin(Angle);
            }

            public static Vector2 CosSin(float Angle)
            {
                return new Vector2(Cos(Angle), Sin(Angle));
            }

        }
    }
}
