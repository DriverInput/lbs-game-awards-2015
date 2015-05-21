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

        public static SoundEffect crabHit, crabDead;

        static SoundEffectInstance waveInstance, natureInstance;

        static SoundEffect waves;
        static SoundEffect nature;

        static Rectangle waveRectangle1, waveRectangle2;

        public static void LoadContent(ContentManager content) 
        {
            waveRectangle1 = new Rectangle(5388, 1176, 7669, 6121);
            waveRectangle2 = new Rectangle(492, 3444, 4897, 2173);

            waves = content.Load<SoundEffect>("Waves");
            nature = content.Load<SoundEffect>("Nature sounds");

            crabHit = content.Load<SoundEffect>("Crab hit");
            crabDead = content.Load<SoundEffect>("Crab dead");

            waveInstance = waves.CreateInstance();
            waveInstance.IsLooped = true;
            waveInstance.Play();
            waveInstance.Volume = 1;

            natureInstance = nature.CreateInstance();
            natureInstance.IsLooped = true;
            natureInstance.Play();
            natureInstance.Volume = 1;

           

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
        public static void EnviormentSounds()
        {
            if ((Main.player.rectangle.Intersects(waveRectangle1) || Main.player.rectangle.Intersects(waveRectangle2)) && waveInstance.Volume < 0.99)
            {
                waveInstance.Volume += 0.01f;
                if (natureInstance.Volume > 0.01)
                {
                    natureInstance.Volume -= 0.01f;
                }
            }
            else if (waveInstance.Volume > 0.01)
            {
                waveInstance.Volume -= 0.01f;
                if (natureInstance.Volume < 0.99)
                {
                    natureInstance.Volume += 0.01f;
                }
            }
        }
    }
}
