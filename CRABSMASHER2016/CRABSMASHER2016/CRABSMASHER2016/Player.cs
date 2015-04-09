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
        public int dir = 0;

        float dx;
        float dy;
        float amount;
        int rollTimer;
        int rollMaxTimer;

        bool isRolling;

        public Player()
        {
            amount = 0.6f;
            rollLength = 128 * 5f; // setting more exact values later
            speed = 5;
            position = new Vector2(50, 50); // setting bether position later
            isRolling = false;
            rollTimer = 0;
            rollMaxTimer = 40;
            width = 146;
            height = 209;
            textureID = "player";

        }

        public void Update()
        {
            newState = Keyboard.GetState();

            #region movement
            if (!isRolling)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    dir = 6;
                    FrameTimer++;
                    position.Y -= speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        dir = 7;
                        FrameTimer++;
                    }
                    else
                    {
                        dir = 0;
                        FrameTimer++;
                    }
                    position.X += speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        dir = 1;
                        FrameTimer++;
                    }
                    else
                    {
                        dir = 2;
                        FrameTimer++;
                    }
                    position.Y += speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        dir = 3;
                        FrameTimer++;
                    }
                    else
                    {
                        dir = 4;
                        FrameTimer++;
                    }
                    position.X -= speed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    dir = 5;
                    FrameTimer++;
                }
            }

            #endregion

            currentAnimation = dir;

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

        }
    }
}
