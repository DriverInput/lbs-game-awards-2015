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

        Dir[] dirs;

        Cooldown cooldown_DisableControls;
        //Cooldown cooldown_frame;

        bool isRolling;

        public Player()
        {
            cooldown_DisableControls = new Cooldown(25,1);

            amount = 0.05f; // set bether value later
            rollLength = 128 * 5f; // set more exact values later
            speed = 7;
            position = new Vector2(50, 50); // set bether position later
            isRolling = false;
            maxFrameTimer = 3;
            rollTimer = 0;
            rollMaxTimer = 25;
            width = 146;
            height = 209;
            textureID = "player";
            #region Movment keys
            dirs = new Dir[]
                {   
                    new Dir(new Keys[]{Keys.I, Keys.L}, 7, new Vector2(1, -1)),
                    new Dir(new Keys[]{Keys.I, Keys.J}, 5, new Vector2(-1, -1)),
                    new Dir(new Keys[]{Keys.K, Keys.L}, 1, new Vector2(1, 1)),
                    new Dir(new Keys[]{Keys.K, Keys.J}, 3, new Vector2(-1, 1)),
                    new Dir(new Keys[]{Keys.I}, 6, new Vector2(0, -1)),
                    new Dir(new Keys[]{Keys.K}, 2, new Vector2(0, 1)),
                    new Dir(new Keys[]{Keys.L}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.J}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.NumPad8, Keys.NumPad6}, 7, new Vector2(1, -1)),
                    new Dir(new Keys[]{Keys.NumPad8, Keys.NumPad4}, 5, new Vector2(-1, -1)),
                    new Dir(new Keys[]{Keys.NumPad2, Keys.NumPad6}, 1, new Vector2(1, 1)),
                    new Dir(new Keys[]{Keys.NumPad2, Keys.NumPad4}, 3, new Vector2(-1, 1)),
                    new Dir(new Keys[]{Keys.NumPad8}, 6, new Vector2(0, -1)),
                    new Dir(new Keys[]{Keys.NumPad2}, 2, new Vector2(0, 1)),
                    new Dir(new Keys[]{Keys.NumPad6}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.NumPad4}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.Up, Keys.Right}, 7, new Vector2(1, -1)),
                    new Dir(new Keys[]{Keys.Up, Keys.Left}, 5, new Vector2(-1, -1)),
                    new Dir(new Keys[]{Keys.Down, Keys.Right}, 1, new Vector2(1, 1)),
                    new Dir(new Keys[]{Keys.Down, Keys.Left}, 3, new Vector2(-1, 1)),
                    new Dir(new Keys[]{Keys.Up}, 6, new Vector2(0, -1)),
                    new Dir(new Keys[]{Keys.Down}, 2, new Vector2(0, 1)),
                    new Dir(new Keys[]{Keys.Right}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.Left}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.W, Keys.D}, 7, new Vector2(1, -1)),
                    new Dir(new Keys[]{Keys.W, Keys.A}, 5, new Vector2(-1, -1)),
                    new Dir(new Keys[]{Keys.S, Keys.D}, 1, new Vector2(1, 1)),
                    new Dir(new Keys[]{Keys.S, Keys.A}, 3, new Vector2(-1, 1)),
                    new Dir(new Keys[]{Keys.W}, 6, new Vector2(0, -1)),
                    new Dir(new Keys[]{Keys.S}, 2, new Vector2(0, 1)),
                    new Dir(new Keys[]{Keys.D}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.A}, 4, new Vector2(-1, 0))
                };
            #endregion
        }
        public void Update()
        {
            cooldown_DisableControls.Update();

            newState = Keyboard.GetState();

            //Keys[] AttackKeys = new Keys[] { Keys.Enter, Keys.Escape, Keys.NumPad9, Keys.E };

            //foreach (Keys key in AttackKeys)
            //    if (newState.IsKeyDown(key))
            //    {
            //        cooldown_DisableControls.Use();

            //    }


            if (cooldown_DisableControls.Output())
            {
                #region movement
                //Movement 
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
                        dir.VecDir.Normalize();
                        position += dir.VecDir * speed;

                        break;
                    }
                }

                Console.WriteLine(position);

                currentAnimation = this.dir;

                if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && isRolling == false)
                {
                    cooldown_DisableControls.Use();
                    isRolling = true;
                    CurrentFrame = 0;
                    dx = position.X + (float)Math.Cos(MathHelper.ToRadians(dir * 45)) * rollLength;
                    dy = position.Y + (float)Math.Sin(MathHelper.ToRadians(dir * 45)) * rollLength;
                }
            }
                if (isRolling)
                {
                    rollTimer++;
                    FrameTimer++;
                    position.X = MathHelper.Lerp(position.X, dx, amount);
                    position.Y = MathHelper.Lerp(position.Y, dy, amount);
                    currentAnimation = this.dir + 8;
                }

                if (rollTimer == rollMaxTimer)
                {
                    rollTimer = 0;
                    isRolling = false;
                }
            
                #endregion

            oldState = newState;


        }

        private struct Dir
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
    }

}
