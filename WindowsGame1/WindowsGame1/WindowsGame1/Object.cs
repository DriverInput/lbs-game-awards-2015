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
    class Object
    {
        protected TextureManager TM = new TextureManager();

        protected Texture2D texture;
        protected Rectangle rectangle;
        protected Vector2 position;
        protected Vector2 velocity;
        protected float speed;
        protected int life;
        protected int maxLife;
        protected int dmg;



        

    }
}
