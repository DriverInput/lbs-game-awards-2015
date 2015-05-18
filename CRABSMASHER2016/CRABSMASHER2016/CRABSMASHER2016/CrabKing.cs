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
    class CrabKing : Object
    {
        public CrabArm leftArm;
        public CrabArm rigthArm;

        public CrabKing()
        {
            textureID = "CrabKing";
            width = 100;
            height = 100;
            origin = new Vector2(width / 2, height / 2);
            position = new Vector2(1000, 500);
            leftArm = new CrabArm(position - Vector2.UnitX * (width), SpriteEffects.None);
            rigthArm = new CrabArm(position, SpriteEffects.FlipHorizontally);
        }

        public void Update()
        {
            CurrentFrame++;
            leftArm.Update();
            rigthArm.Update();
        }
    }
}
