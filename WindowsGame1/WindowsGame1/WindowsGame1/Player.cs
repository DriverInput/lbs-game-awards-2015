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
    class Player : Object
    {
        KeyboardState newState, oldState;
        public float rollLength;
        public int dir;

        float dx;
        float dy;
        float amount;
        int rollTimer;
        int rollMaxTimer;

        bool isRolling;

        Animation playerAnimation;

        public Player(ContentManager Content)
        {
            amount = 0.6f;
            rollLength = 128 * 5f; // setting more exact values later
            speed = 5;
            position = new Vector2(50, 50); // setting bether position later
            isRolling = false;
            rollTimer = 0;
            rollMaxTimer = 40;
            LaddaenKatt(Content);
            TextureManager.InitializeTextures.Add("walking", "walking");
        }

        public void LaddaenKatt(ContentManager Content)
        {
            playerAnimation = new Animation(Content, "walking", 150f, 8, true, position, 8, 0);
        }

        public void Update(GameTime gameTime) 
        {
            newState = Keyboard.GetState();

            if (!isRolling)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    dir = 6;
                    position.Y -= speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        dir = 7;
                    }

                    else
                        dir = 0;
                    position.X += speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        dir = 1;
                    }
                    else
                        dir = 2;
                    position.Y += speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        dir = 3;
                    }
                    else
                        dir = 4;
                    position.X -= speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    dir = 5;
                }
            }

            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && isRolling == false)
            {
                isRolling = true;
                dx = position.X + (float)Math.Cos(MathHelper.ToRadians(dir * 45)) * rollLength;
                dy = position.Y + (float)Math.Sin(MathHelper.ToRadians(dir * 45)) * rollLength;
            }

            if (isRolling)
            {
                rollTimer++;
                position.X = MathHelper.Lerp(position.X, dx, amount);
                position.Y = MathHelper.Lerp(position.Y, dy, amount);
            }

            if (rollTimer == rollMaxTimer)
            {
                rollTimer = 0;
                isRolling = false;
            }

            oldState = newState;

            playerAnimation.PlayAnimation(gameTime);
        }
    }
}
