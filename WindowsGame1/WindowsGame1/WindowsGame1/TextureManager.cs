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

namespace game
{
    public class TextureManager
    {
        static Texture2D test;
        public TextureManager()
        {
               
        }

        public static void LoadAll(ContentManager Content)
        {
            test = Content.Load<Texture2D>("test");
        }
    }
}
