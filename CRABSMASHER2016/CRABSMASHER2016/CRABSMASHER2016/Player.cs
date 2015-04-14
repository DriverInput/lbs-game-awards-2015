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
            amount = 0.05f; // set bether value later
            rollLength = 128 * 5f; // set more exact values later
            speed = 5;
            position = new Vector2(50, 50); // set bether position later
            isRolling = false;
            rollTimer = 0;
            rollMaxTimer = 40;
            width = 146;
            height = 209;
            textureID = "player";

        }
        public class Dir
        {
            public Vector2 VecDir;
            public Keys[] keys;
            public int dir;
            public Dir(Keys[] keys, int dir, Vector2 VecDir)
            {
                this.keys = keys;
                this.dir = dir;
                this.VecDir = VecDir;
            }
        }

        public void Update()
        {
            newState = Keyboard.GetState();

            Dir[] dirs = new Dir[]
            {
                new Dir(new Keys[]{Keys.W, Keys.D}, 7, new Vector2(1, -1)),
                new Dir(new Keys[]{Keys.W, Keys.A}, 5, new Vector2(-1, -1)),
                new Dir(new Keys[]{Keys.S, Keys.D}, 1, new Vector2(1, 1)),
                new Dir(new Keys[]{Keys.S, Keys.A}, 3, new Vector2(-1, 1)),
                new Dir(new Keys[]{Keys.W}, 6, new Vector2(0, -1)),
                new Dir(new Keys[]{Keys.S}, 2, new Vector2(0, 1)),
                new Dir(new Keys[]{Keys.D}, 0, new Vector2(1, 0)),
                new Dir(new Keys[]{Keys.A}, 4, new Vector2(-1, 0))
            };
            foreach (Dir dir in dirs)
            {
                bool Continue = true;
                
                foreach (Keys key in dir.keys)                
                    if (!newState.IsKeyDown(key))
                        Continue = false;

                if (Continue) 
                {
                    FrameTimer++;
                    this.dir = dir.dir;
                    position += dir.VecDir * speed;
                    break;
                }
            }

            

            Console.WriteLine(position);

            #region movement
            //if (!isRolling)
            //{
            //    if (Keyboard.GetState().IsKeyDown(Keys.W))
            //    {
            //        dir = 6;
            //        FrameTimer++;
            //        position.Y -= speed;
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    {
            //        if (Keyboard.GetState().IsKeyDown(Keys.W))
            //        {
            //            dir = 7;
            //            FrameTimer++;
            //        }
            //        else
            //        {
            //            dir = 0;
            //            FrameTimer++;
            //        }
            //        position.X += speed;
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    {
            //        if (Keyboard.GetState().IsKeyDown(Keys.D))
            //        {
            //            dir = 1;
            //            FrameTimer++;
            //        }
            //        else
            //        {
            //            dir = 2;
            //            FrameTimer++;
            //        }
            //        position.Y += speed;
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    {
            //        if (Keyboard.GetState().IsKeyDown(Keys.S))
            //        {
            //            dir = 3;
            //            FrameTimer++;
            //        }
            //        else
            //        {
            //            dir = 4;
            //            FrameTimer++;
            //        }
            //        position.X -= speed;
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
            //    {
            //        dir = 5;
            //        FrameTimer++;
            //    }
            //}

            #endregion

            currentAnimation = this.dir;

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
