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
        public Vector2 position;
        public Rectangle sourceRectangle;
        public int width;
        public float angle;
        public float angleOffset;
        public Vector2 origin;
        protected string textureID;

        public int speed = 8;

        public int windowWidth;
        public int windowHeight;

       

        public Vector2 velocity;

        public int frame;

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, width, TextureManager.Textures[textureID].Height);
            }
        }

        public int maxFrame
        {
            get
            {
                return (TextureManager.Textures[textureID].Width / width) - 1;
            }
        }
        
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.Textures[textureID], position, sourceRectangle, Color.White, angle + angleOffset, origin, 1f, SpriteEffects.None, 1f);
        }

        private Vector2 RectangleToRectangle(float x1, float y1, int w1, int h1, float x2, float y2, int w2, int h2)
        {
            float dw = 0.5f * (w1 + w2);
            float dh = 0.5f * (h1 + h2);
            float dx = (x1) - (x2 + w2 / 2);
            float dy = (y1) - (y2 + h2 / 2);

            if (Math.Abs(dx) <= dw && Math.Abs(dy) <= dh)
            {
                float wy = dw * dy;
                float hx = dh * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        y1 = y2 + h2 + h1 / 2;

                    }
                    else
                    {

                        x1 = x2 - h1 / 2;
                    }
                }
                else
                {
                    if (wy > -hx)
                    {
                        x1 = x2 + w2 + w1 / 2;
                    }
                    else
                    {
                        y1 = y2 - h1 / 2;
                    }
                }
            }

            return new Vector2(x1, y1);
        }
    }
}
