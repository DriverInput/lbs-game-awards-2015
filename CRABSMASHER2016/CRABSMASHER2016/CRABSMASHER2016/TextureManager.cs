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
        public static Dictionary<string, string> InitializeTextures = new Dictionary<string, string>();
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        
        public static void LoadContent(ContentManager cm)
        {
            foreach (KeyValuePair<string, string> item in InitializeTextures)
            {
                Textures.Add(item.Key, cm.Load<Texture2D>(item.Value));
            }
            InitializeTextures = null;
        }
    }
}
