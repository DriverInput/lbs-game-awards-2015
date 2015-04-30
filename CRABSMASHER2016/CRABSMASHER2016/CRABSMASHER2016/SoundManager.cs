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

        static SoundEffect tempSwordSound;
        static SoundEffect[] stepSounds = new SoundEffect[10];

        public static void LoadContent(ContentManager content) 
        {
            for (int i = 0; i < stepSounds.Length; i++)
            {
                stepSounds[i] = content.Load<SoundEffect>("step" + (i + 1));
            }
            tempSwordSound = content.Load<SoundEffect>("Temp sound");
        }

        static int prevRandom = 0;
        static int newRandom = 0;
        public static void PlayWalkingSound() 
        {
            Random rnd = new Random();
            do
            {
              newRandom = rnd.Next(0,stepSounds.Length);
            } while (newRandom == prevRandom);
            prevRandom = newRandom;
            stepSounds[newRandom].Play();
        }

        public static void PlaySwordSound() 
        {
            tempSwordSound.Play();
        }
    }
}
