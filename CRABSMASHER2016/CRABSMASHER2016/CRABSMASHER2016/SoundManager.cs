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
    static class SoundManager
    {
        #region jessy read this
        //JESSY DU RÖR INTE DENNA KLASSEN FÖR DÅ KOMMER DU BARA SKRIVA OM DEN
        #endregion

        static SoundEffect[] stepSounds = new SoundEffect[10];

        public static void LoadContent(ContentManager content) 
        {
            stepSounds[0] = content.Load<SoundEffect>("step1");
            stepSounds[1] = content.Load<SoundEffect>("step2");
            stepSounds[2] = content.Load<SoundEffect>("step3");
            stepSounds[3] = content.Load<SoundEffect>("step4");
            stepSounds[4] = content.Load<SoundEffect>("step5");
            stepSounds[5] = content.Load<SoundEffect>("step6");
            stepSounds[6] = content.Load<SoundEffect>("step7");
            stepSounds[7] = content.Load<SoundEffect>("step8");
            stepSounds[8] = content.Load<SoundEffect>("step9");
            stepSounds[9] = content.Load<SoundEffect>("step10");
        }

        public static void WalkingSounds() 
        {
            Random rnd = new Random();
            stepSounds[rnd.Next(0,10)].Play();
        }
    }
}
