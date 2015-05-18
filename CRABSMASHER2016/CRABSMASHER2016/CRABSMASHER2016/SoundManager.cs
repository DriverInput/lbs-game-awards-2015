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

        static SoundEffect[] magicalSounds = new SoundEffect[6];
        static SoundEffect[] stepSounds = new SoundEffect[10];
        static Vector2[] soundPoints = new Vector2[]{
            new Vector2(1300,500),
            new Vector2(3000,5200),
            new Vector2(10000,5000)
        };

        static Song waves;
        static Song nature;

        public static void LoadContent(ContentManager content) 
        {
            //waves = content.Load<Song>("Waves");
            nature = content.Load<Song>("Nature sounds");


            for (int i = 0; i < stepSounds.Length; i++)
            {
                stepSounds[i] = content.Load<SoundEffect>("stepSounds/step" + (i + 1));
            }
            //for (int i = 0; i < swordSounds.Length; i++)
            //{
            //    swordSounds[i] = content.Load<SoundEffect>("Sword " + (i + 2));
            //}
            for (int i = 0; i < magicalSounds.Length; i++)
            {
                magicalSounds[i] = content.Load<SoundEffect>("magical sound " + (i + 1));
            }
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

        static int prevRandom2 = 0;
        static int newRandom2 = 0;
        public static void PlaySwordSound() 
        {
            Random rnd = new Random();
            Random rnd2 = new Random();
            do
            {
                //newRandom = rnd.Next(0, swordSounds.Length);
                newRandom2 = rnd.Next(0, magicalSounds.Length);
            } while (newRandom == prevRandom && newRandom2 == prevRandom2);
            prevRandom = newRandom;
            prevRandom2 = newRandom2;
            //swordSounds[newRandom].Play();
            magicalSounds[newRandom2].Play();
        }
        public static void EnviormentSounds(Vector2 pos)
        {
            MediaPlayer.Play(waves);
            MediaPlayer.Play(nature);
        }
    }
}
