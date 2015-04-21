using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game
{
    class CollisionBox
    {
        public Rectangle rectangle;

        public CollisionBox(int x,int y)    
        {
            rectangle = new Rectangle(x, y, 64, 64);
        }
    }
}
