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

        public MiniCrab()
        {
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

        float angle;
        float angleDif;
        int dist;
        public void GettingHit()
        {
            angle = Main.player.dir * 45;
            angleDif = MathHelper.ToDegrees((float)Math.Atan2(position.Y - Main.player.position.Y, position.X - Main.player.position.X));
            dist = (int)Math.Sqrt(Math.Pow(position.X - Main.player.position.X, 2) + Math.Pow(position.Y - Main.player.position.Y, 2));
            //angleDif = MathHelper.ToDegrees((float)Math.Atan2(Main.player.position.Y - position.Y, Main.player.position.X - position.X));
            Console.Clear();
            Console.WriteLine("angleDif " + angleDif);
            Console.WriteLine("player angle " + angle);
            Console.WriteLine("dist " + dist);
            if (angleDif > angle - 22.5f && angleDif < angle + 22.5f && dist < 270)
            {
                if (Main.player.isAttacking)
                {
                    destroy = true;
                    //crab death sound.play 
                    //eller crab hit sound.play
                }
            }
        }
    }
}
