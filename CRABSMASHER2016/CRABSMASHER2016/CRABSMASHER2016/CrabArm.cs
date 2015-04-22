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
    class CrabArm : Object
    {
        public CrabArm(Vector2 pos, SpriteEffects se)
        {
            width = 100;
            height = 100;
            position = pos;
            spriteEffect = se;
            textureID = "CrabArm";
        }

        public void Update(Player player)
        {
            if (player.rectangle.Intersects(rectangle))
            {
                CurrentFrame++;
                if (CurrentFrame == 7)
                {
                    
                    CurrentFrame = 0;
                }
            }
            else
            {
                CurrentFrame = 0;
            }
        }
    }
}
