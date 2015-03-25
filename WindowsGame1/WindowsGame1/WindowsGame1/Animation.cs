using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game
{
    class Animation
    {
        Vector2 position;
        Rectangle sourceRectangle;
        Texture2D animation;

        float elapsed;
        float frameTime;
        int numOfFrames, currentFrame, width, height, frameWidth, frameHeight, divide, row;
        bool looping;
        string assetName;

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public float FrameSpeed
        {
            get { return this.frameTime; }
            set { this.frameTime = value; }
        }

        public Animation(ContentManager Content, string assetName, float frameSpeed, int numOfFrames, bool looping, Vector2 position, int divide, int row)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            frameWidth = (TextureManager.Textures[assetName].Width/ numOfFrames);
            frameHeight = (animation.Height / divide);
            this.position = position;
            this.divide = divide;
            this.row = row;
            this.assetName = assetName;
        }

        public void PlayAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRectangle = new Rectangle(currentFrame * frameWidth, frameHeight * row, frameWidth, frameHeight);

            if(elapsed >= frameTime)
            {
                if(currentFrame >= numOfFrames - 1)
                {
                    if (looping)
                        currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                }
                elapsed = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.Textures[assetName], position, sourceRectangle, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }

    }
}
