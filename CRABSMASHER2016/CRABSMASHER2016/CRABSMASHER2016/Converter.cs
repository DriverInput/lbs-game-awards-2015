using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Converter
    {
        public class Vector2
        {
            public static bool AnyGreater(Microsoft.Xna.Framework.Vector2 x1 ,Microsoft.Xna.Framework.Vector2 x2)
            {
                if (x1.X > x2.X)
                    return true;
                if (x1.Y > x2.Y)
                    return true;
                return false;
            }
        }
        public class Float
        {
            public static float Cos(float Angle)
            {
                return (float)Math.Cos(Angle);
            }

            public static float Sin(float Angle)
            {
                return (float)Math.Sin(Angle);
            }

            public static Microsoft.Xna.Framework.Vector2 CosSin(float Angle)
            {
                return new Microsoft.Xna.Framework.Vector2(Cos(Angle), Sin(Angle));
            }

        }
    }
}
