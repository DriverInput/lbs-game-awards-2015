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

        public void Update()
        {
            if (Main.player.rectangle.Intersects(rectangle))
            {
                CurrentFrame++;
                if (spriteEffect == SpriteEffects.None)
                {
                    Main.player.velocity.X = -16;
                }
                else
                {
                    Main.player.velocity.X = 16;
                }
                
                Main.player.isStunned = true;
                Main.player.isRolling = false;
                Main.player.CurrentFrame = 0;
            }
            else
            {
                CurrentFrame = 0;
            }
        }
    }
}
