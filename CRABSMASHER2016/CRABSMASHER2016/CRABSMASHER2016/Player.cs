﻿using System;
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
    public class Player : Object
    {
        KeyboardState newState, oldState;
        public float rollLength;
        public int dir = 0;

        public StatsBar bar;

        float dx;
        float dy;
        float amount;
        int rollTimer;
        int rollMaxTimer;
        int attackTimer;
        int attackMaxTimer;

        //Rectangle Colition
        //{
        //    get
        //    {
        //        if (isRolling)
        //        {
                    
        //        }

        //    }
        //}

        const float maxHp = 100;
        float hp = maxHp;
        const float maxStamina = 100;
        float stamina = maxStamina;

        public Rectangle Collision 
        {
            get 
            {
                if (isRolling)
                {
                    switch (dir)
                    {
                     case 0:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 1:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 2:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 3:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 4:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 5:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 6:
	                    return new Rectangle(0,0,0,0);
	                    
                    case 7:
	                    return new Rectangle(0,0,0,0);
	                    
                    }
                }
                else
                {
                    switch (dir)
                    {
                        case 0:
                            return new Rectangle(0, 0, 0, 0);

                        case 1:
                            return new Rectangle(0, 0, 0, 0);

                        case 2:
                            return new Rectangle(0, 0, 0, 0);

                        case 3:
                            return new Rectangle(0, 0, 0, 0);

                        case 4:
                            return new Rectangle(0, 0, 0, 0);

                        case 5:
                            return new Rectangle(0, 0, 0, 0);

                        case 6:
                            return new Rectangle(0, 0, 0, 0);

                        case 7:
                            return new Rectangle(0, 0, 0, 0);

                    }
                }

                return new Rectangle(0, 0, 0, 0);
            }
        }


        Dir[] dirs;

        Color[] textureData;

        Cooldown cooldown_DisableControls;
        //Cooldown cooldown_frame;
        public static Texture2D hitBoxTexture;

        public bool isRolling;
        public bool isStunned;
        public bool isAttacking;

        public Player()
        {
            cooldown_DisableControls = new Cooldown(25, 1);

            amount = 0.08f; // set bether value later
            rollLength = 128 * 4f; // set more exact values later
            speed = 7;
            position = new Vector2(2500, 2000); // set bether position later
            isRolling = false;
            maxFrameTimer = 4;
            attackTimer = 0;
            attackMaxTimer = 27;
            rollTimer = 0;
            rollMaxTimer = 25 + 7;
            width = 146;
            height = 209;
            textureID = "player";
            textureData = new Color[hitBoxTexture.Width * hitBoxTexture.Height];
            hitBoxTexture.GetData<Color>(textureData);
            bar = new StatsBar();
            #region Movment keys
            dirs = new Dir[]
                {   
                    new Dir(new Keys[]{Keys.I, Keys.L}, 7, new Vector2(1, -0.8f)),
                    new Dir(new Keys[]{Keys.I, Keys.J}, 5, new Vector2(-1, -0.8f)),
                    new Dir(new Keys[]{Keys.K, Keys.L}, 1, new Vector2(1, 0.8f)),
                    new Dir(new Keys[]{Keys.K, Keys.J}, 3, new Vector2(-1, 0.8f)),
                    new Dir(new Keys[]{Keys.I}, 6, new Vector2(0, -0.8f)),
                    new Dir(new Keys[]{Keys.K}, 2, new Vector2(0, 0.8f)),
                    new Dir(new Keys[]{Keys.L}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.J}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.NumPad8, Keys.NumPad6}, 7, new Vector2(1, -0.8f)),
                    new Dir(new Keys[]{Keys.NumPad8, Keys.NumPad4}, 5, new Vector2(-1, -0.8f)),
                    new Dir(new Keys[]{Keys.NumPad2, Keys.NumPad6}, 1, new Vector2(1, 0.8f)),
                    new Dir(new Keys[]{Keys.NumPad2, Keys.NumPad4}, 3, new Vector2(-1, 0.8f)),
                    new Dir(new Keys[]{Keys.NumPad8}, 6, new Vector2(0, -0.8f)),
                    new Dir(new Keys[]{Keys.NumPad2}, 2, new Vector2(0, 0.8f)),
                    new Dir(new Keys[]{Keys.NumPad6}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.NumPad4}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.Up, Keys.Right}, 7, new Vector2(1, -0.8f)),
                    new Dir(new Keys[]{Keys.Up, Keys.Left}, 5, new Vector2(-1, -0.8f)),
                    new Dir(new Keys[]{Keys.Down, Keys.Right}, 1, new Vector2(1, 0.8f)),
                    new Dir(new Keys[]{Keys.Down, Keys.Left}, 3, new Vector2(-1, 0.8f)),
                    new Dir(new Keys[]{Keys.Up}, 6, new Vector2(0, -0.8f)),
                    new Dir(new Keys[]{Keys.Down}, 2, new Vector2(0, 0.8f)),
                    new Dir(new Keys[]{Keys.Right}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.Left}, 4, new Vector2(-1, 0)),

                    new Dir(new Keys[]{Keys.W, Keys.D}, 7, new Vector2(1, -0.8f)),
                    new Dir(new Keys[]{Keys.W, Keys.A}, 5, new Vector2(-1, -0.8f)),
                    new Dir(new Keys[]{Keys.S, Keys.D}, 1, new Vector2(1, 0.8f)),
                    new Dir(new Keys[]{Keys.S, Keys.A}, 3, new Vector2(-1, 0.8f)),
                    new Dir(new Keys[]{Keys.W}, 6, new Vector2(0, -0.8f)),
                    new Dir(new Keys[]{Keys.S}, 2, new Vector2(0, 0.8f)),
                    new Dir(new Keys[]{Keys.D}, 0, new Vector2(1, 0)),
                    new Dir(new Keys[]{Keys.A}, 4, new Vector2(-1, 0))
                };
            #endregion
        }

        public void Update()
        {
            cooldown_DisableControls.Update();
            bar.update(hp / maxHp, stamina / maxStamina);

            hp -= 0.1F;
            float F = 0.5f;
            if (isStunned)
            {
                if (velocity.X > 0) velocity.X -= F;
                if (velocity.X < 0) velocity.X += F;
                if (velocity.Y > 0) velocity.Y -= F;
                if (velocity.Y < 0) velocity.Y += F;

                if (velocity.X == 0) isStunned = false;
            }

            newState = Keyboard.GetState();

            #region movement
            if (!isRolling && !isStunned && !isAttacking)//(cooldown_DisableControls.Output())00
            {
                stamina = (stamina += 0.5f) > maxStamina ? maxStamina : stamina;

                //Movement 
                foreach (Dir dir in dirs)
                {
                    bool Continue = true;

                    foreach (Keys key in dir.keys)
                        if (!newState.IsKeyDown(key))
                            Continue = false;

                    velocity = Vector2.Zero;

                    if (Continue)
                    {
                        FrameTimer++;
                        

                        if (FrameTimer == 0 && CurrentFrame == 0 || FrameTimer == 0 && CurrentFrame == 4)
                        {
                            SoundManager.PlayWalkingSound();
                        }
                        Console.WriteLine(position);
                        this.dir = dir.dir;
                        velocity = Vector2.Normalize(dir.VecDir) * speed;
                        break;
                    }
                }

                currentAnimation = this.dir;

                if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter) && stamina > attackMaxTimer)
                {
                    cooldown_DisableControls.Use();
                    isAttacking = true;

                }

                if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && stamina >= rollMaxTimer)
                {
                    cooldown_DisableControls.Use();
                    isRolling = true;
                    CurrentFrame = 0;
                    dx = position.X + (float)Math.Cos(MathHelper.ToRadians(dir * 45)) * rollLength;
                    dy = position.Y + (float)Math.Sin(MathHelper.ToRadians(dir * 45)) * rollLength * 0.8f;
                }
            }
            if (isAttacking)
            {
                attackTimer++;
                FrameTimer++;
                stamina -= 1;
            }

            if (attackTimer == attackMaxTimer)
            {
                attackTimer = 0;
                isAttacking = false;
            }


            if (isRolling)
            {
                rollTimer++;
                FrameTimer++;
                stamina--;

                float deltaPositionX = MathHelper.Lerp(position.X, dx, amount) - position.X;
                float deltaPositionY = MathHelper.Lerp(position.Y, dy, amount) - position.Y;
                velocity = new Vector2(deltaPositionX, deltaPositionY);
                currentAnimation = this.dir + 8;
            }

            if (rollTimer == rollMaxTimer)
            {
                rollTimer = 0;
                isRolling = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                velocity.X = 20;
                isStunned = true;
            }
            #endregion

            oldState = newState;

            CheckEnviorementCollision(position, velocity);
        }

        private void CheckEnviorementCollision(Vector2 currentPosition, Vector2 velocity)
        {
            Vector2 velocityByX = new Vector2(velocity.X, 0);
            Vector2 velocityByY = new Vector2(0, velocity.Y);
            Vector2 nextPosition = position;

            Vector2 futurePosition = nextPosition + velocity;
            Rectangle hitBox = new Rectangle((int)futurePosition.X, (int)futurePosition.Y + rectangle.Height / 2, hitBoxTexture.Width, hitBoxTexture.Height);
            for (int i = 0; i < 12; i++)
            {
                //if (IntersectPixels(hitBox, textureData, Main.collisionMaskRects[i], Main.collisionsMaskDataArrays[i]))
                //{
                //    break;

                //}
                //else if (i == 11)
                //{
                //    nextPosition = futurePosition;
                //}
            }

            //futurePosition = nextPosition + velocityByY;
            hitBox = new Rectangle((int)futurePosition.X, (int)futurePosition.Y + rectangle.Height / 2, hitBoxTexture.Width, hitBoxTexture.Height);
            for (int i = 0; i < 12; i++)
            {
                //if (IntersectPixels(hitBox, textureData, Main.collisionMaskRects[i], Main.collisionsMaskDataArrays[i]))
                //{
                //    break;

                //}
                //else if (i == 11)
                //{
                //    nextPosition = futurePosition;
                //}
            }

            position = futurePosition;
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
