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
    class MiniCrab : Object
    {
        float targetAngle;
        public bool destroy;
        public bool dead;
        int hitPoints;
        public int timer;
        public int maxTimer;

        public MiniCrab()
        {
            timer = 0;
            maxTimer = 5;
            hitPoints = 2;
            dead = true;
            destroy = false;
            width = 180;
            height = 128;
            angle = 0;
            position = new Vector2(25, 25); // more exact values later
            speed = 2;
            maxFrameTimer = 8;
            velocity = new Vector2();
            textureID = "minicrab";
        }

        public void Update(Player player) 
        {
            targetAngle = (float)Math.Atan2((player.position.Y + player.height * 0.5f) - (position.Y + height * 0.5f), (player.position.X + player.width * 0.5f) - (position.X + width * 0.5f));
            velocity = Converter.Float.CosSin(targetAngle) * speed;
            position += velocity;

            Rectangle playerOffSetRectangle = new Rectangle(player.rectangle.X, player.rectangle.Y, player.rectangle.Width, player.rectangle.Height);
            player.position = RectangleToRectangle(player.rectangle, this.rectangle);// RectangleToRectangle(player.position.X, player.position.Y, player.width, player.height, position.X, position.Y, width, height);
            FrameTimer++;
        }

        public void GettingHit()
        {
            if (true && !dead)
            {
                if (hitPoints == 0)
                {
                    //push
                    SoundManager.crabDead.Play();
                    //currentAnimation = 1;
                    dead = true;
                }
                else
                {
                    hitPoints--;

                    //Pushback
                    SoundManager.crabHit.Play();

                }
            }
        }
    }
}
