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
    class MiniCrab : Object
    {
        float targetAngle;

        public MiniCrab()
        {
            width = 180;
            height = 128;
            angle = 0;
            position = new Vector2(25, 25); // more exact values later
            speed = 2;
            velocity = new Vector2();
            textureID = "minicrab";
        }

        public void Update(Player player) 
        {
            targetAngle = (float)Math.Atan2(player.position.Y - position.Y, player.position.X - position.X);
            velocity = Converter.Float.CosSin(targetAngle) * speed;
            position += velocity;

            position = RectangleToRectangle(position.X, position.Y, width, height, player.position.X, player.position.Y, player.width, player.height);

            FrameTimer++;
        }
    }
}
