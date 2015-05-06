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
    class Object
    {
        public Vector2 position;
        public Rectangle sourceRectangle
        { 
            get{
                return new Rectangle(CurrentFrame * width, (currentAnimation) * height, width, height);
            }
        }
        public int width;
        public int height;
        public float angle;
        public float angleOffset;
        public Vector2 origin;
        protected string textureID;

        public int speed = 8;

        public int windowWidth;
        public int windowHeight;

        public Vector2 velocity;

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, width, height);
            }
            set
            {
                rectangle = value;
            }
        }

        private int currentFrame = 0;
        public int CurrentFrame {
            get
            {
                return currentFrame;
            }
            set
            {
                if (value < maxFrame)
                    currentFrame = value;
                else if (value == maxFrame)
                    currentFrame = 0;
            }
        }
        public int currentAnimation;

        private int frameTimer;
        public int FrameTimer
        {
            get
            {
                return frameTimer;
            }
            set
            {
                if (value <= maxFrameTimer)
                    frameTimer = value;
                else if (value >= maxFrameTimer)
                {
                    frameTimer = 0;
                    CurrentFrame++;
                }
            }
        }

        public int maxFrameTimer;

        public int maxFrame { get { return (TextureManager.Textures[textureID].Width / width)/2 - 1; } }
        public int maxAnimation { get { return (TextureManager.Textures[textureID].Height / height) - 1; } }

        public SpriteEffects spriteEffect = SpriteEffects.None;

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.Textures[textureID], rectangle, sourceRectangle, Color.White, angle + angleOffset, origin, spriteEffect, 1f);
        }

        public static Vector2 RectangleToRectangle(float x1, float y1, int w1, int h1, float x2, float y2, int w2, int h2)
        {
            float dw = 0.5f * (w1 + w2);
            float dh = 0.5f * (h1 + h2);
            float dx = (x1 + (w1 / 2)) - (x2 + (w2 / 2));
            float dy = (y1 + (w1 / 2)) - (y2 + (h2 / 2));

            if (Math.Abs(dx) <= dw && Math.Abs(dy) <= dh)
            {
                float wy = dw * dy;
                float hx = dh * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        //bottom
                        if (y1 <= y2 + h2)
                        {
                            y1 = y2 + h2;
                        }
                    }
                    else
                    {
                        //left
                        x1 = x2 - w1;
                    }
                }
                else
                {
                    if (wy > -hx)
                    {
                        //right
                        x1 = x2 + w2;
                    }
                    else
                    {
                        //top
                        y1 = y2 - h1 + 30;
                    }
                }
            }

            return new Vector2(x1, y1);
        }
        public static Vector2 RectangleToRectangle(Rectangle A, Rectangle B)
        {
            float dw = 0.5f * (A.Width + B.Width);
            float dh = 0.5f * (A.Height + B.Height);
            float dx = A.Center.X - B.Center.X;
            float dy = A.Center.Y - B.Center.Y;

            if (Math.Abs(dx) <= dw && Math.Abs(dy) <= dh)
            {
                float wy = dw * dy;
                float hx = dh * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        //bottom
                        A.Y = B.Y + B.Height;
                    }
                    else
                    {
                        //left
                        A.X = B.X - A.Width;
                    }
                }
                else
                {
                    if (wy > -hx)
                    {
                        //right
                        A.X = B.X + B.Width;
                    }
                    else
                    {
                        //top
                        A.Y = B.Y - A.Height;
                    }
                }
            }

            return new Vector2(A.X, A.Y);
        }
        public static Vector2 RectangleToRectangle(Rectangle A, int AxOffSet, int AyOffset, Rectangle B)
        {
            float dw = 0.5f * (A.Width + B.Width);
            float dh = 0.5f * (A.Height + B.Height);
            float dx = (A.Center.X + AxOffSet) - B.Center.X;
            float dy = (A.Center.Y + AyOffset) - B.Center.Y;

            if (Math.Abs(dx) <= dw && Math.Abs(dy) <= dh)
            {
                float wy = dw * dy;
                float hx = dh * dx;

                if (wy > hx)
                {
                    if (wy > -hx)
                    {
                        //bottom
                        A.Y = B.Y + B.Height;
                    }
                    else
                    {
                        //left
                        A.X = B.X - A.Width;
                    }
                }
                else
                {
                    if (wy > -hx)
                    {
                        //right
                        A.X = B.X + B.Width;
                    }
                    else
                    {
                        //top
                        A.Y = B.Y - A.Height;
                    }
                }
            }

            return new Vector2(A.X - AxOffSet, A.Y - AyOffset);
        }

        public static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
                                    Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }
    }
}
