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
    struct EnvDetails
    {
        public Vector2 pos;
        public Texture2D tex;

        public EnvDetails(Vector2 newPos, Texture2D newTex)
        {
            pos = newPos;
            tex = newTex;
        }
    }

    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Player player;
        CrabKing crabKing = new CrabKing();
        Camera camera = new Camera();

        List<MiniCrab> miniCrabs = new List<MiniCrab>();
        List<Tile> tiles = new List<Tile>();
        List<Rectangle> rectangeList = new List<Rectangle>();
        public static Rectangle[] collisionMaskRects = new Rectangle[12];
        Texture2D[] collisionMasks = new Texture2D[12];
        public static Color[][] collisionsMaskDataArrays = new Color[12][];
        Texture2D[] environment = new Texture2D[12];
        List<EnvDetails> envDetails = new List<EnvDetails>();
        bool debugStart = false;
        private float zoom;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            if (debugStart)
            {
                Console.WriteLine("INITIALIZE DATAARRAYS, LOAD COLLISION MASKS AND SAVE THEIR DATA");
                Console.WriteLine();
            }
            for (sbyte i = 0; i < 12; i++)
            {
                collisionMasks[i] = Content.Load<Texture2D>("CollisionMasks/CM" + (i + 1));
                collisionsMaskDataArrays[i] = new Color[10833920];
                collisionMasks[i].GetData<Color>(collisionsMaskDataArrays[i]);
                collisionMasks[i].Dispose();
                GC.Collect();
                Console.WriteLine("Loaded, saved its data, disposed CM " + (i+1) + "/12");
                
            }
            if (debugStart)
            {
                Console.WriteLine("FINISHED LOADING CMs AND DATAARRAYS");

                Console.WriteLine("Currently using " + GC.GetTotalMemory(false) + " bytes");
                Console.ReadLine();

                Console.WriteLine("LOAD ENIRONMENT TEXTURES");
            }
            
            for (sbyte i = 1; i < 13; i++)
            {
                environment[i - 1] = Content.Load<Texture2D>("mapParts/MapPart" + i);
            }
            envDetails.Add(new EnvDetails(new Vector2(0, 1633), Content.Load<Texture2D>("cliffs")));
            envDetails.Add(new EnvDetails(new Vector2(3300, 1633-1), Content.Load<Texture2D>("valv")));
            if (debugStart)
            {
                Console.WriteLine("FINISHED LODING ENVIRONMENT TEXTURES");

                Console.WriteLine("Currently using " + GC.GetTotalMemory(false) + " bytes");
                Console.ReadLine();

                Console.WriteLine("LOAD THE REST OF THE TEXTURE");
            }
            TextureManager.InitializeTextures.Add("player", "spriteplaceholder");
            TextureManager.InitializeTextures.Add("minicrab", "lil krabba spritesheet");
            TextureManager.InitializeTextures.Add("CrabKing", "CrabKing");
            TextureManager.InitializeTextures.Add("CrabArm", "CrabArm");
            TextureManager.InitializeTextures.Add("bar", "bar");
            TextureManager.InitializeTextures.Add("hp", "hp");
            TextureManager.InitializeTextures.Add("stamina", "stamina");
            TextureManager.InitializeTextures.Add("bord", "bord");
            //JAG HAR SYNDAT! SORRY JESS!
            TextureManager.LoadContent(Content);

            Player.hitBoxTexture = Content.Load<Texture2D>("hitBoxTexture");
            if (debugStart)
            {
                Console.WriteLine("FINISHED LODING THE REST OF THE TEXTURES");

                Console.WriteLine("Currently using " + GC.GetTotalMemory(false) + " bytes");
                Console.ReadLine();

                Console.WriteLine("CALCULATE CMrects");
            }

            short w = 4096;
            short h = 2645;
            sbyte n = 0;

            for (sbyte x = 0; x < 3; x++)
                for (sbyte y = 0; y < 4; y++)
                {
                    collisionMaskRects[n] = new Rectangle(y * w, x * h, w, h);
                    n++;
                }

            if (debugStart)
            {
                Console.WriteLine("FINISHED CALCULATING CMrects");

                Console.WriteLine("Currently using " + GC.GetTotalMemory(false) + " bytes");
                Console.ReadLine();
            }
            player = new Player();

            Console.WriteLine("DONE! RUN GAME!");
            if (debugStart)
            {
                Console.ReadLine();
            }

            miniCrabs.Add(new MiniCrab());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundManager.LoadContent(Content);
        }

        //float zoom = 0.8f;
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            SoundManager.EnviormentSounds();

            player.Update();

            foreach (MiniCrab m in miniCrabs)
            {
                if (!m.dead)
                {
                    m.Update(player);
                    m.GettingHit();
                }
            }
            for (int i = 0; i < miniCrabs.Count; i++)
                if (miniCrabs[i].destroy)
                    miniCrabs.RemoveAt(i);

            if (miniCrabs.Count == 0) { miniCrabs.Add(new MiniCrab()); }



            camera.Pos = player.position + new Vector2(player.width / 2, player.height / 2);
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                zoom -= 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.E))
                zoom += 0.01f;

            camera.Zoom = zoom;
            crabKing.Update();
            base.Update(gameTime);
        }

        static bool IntersectPixels(Rectangle rectangleA, Color[] dataA,
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,null, null, null, null, camera.get_transformation(GraphicsDevice));

            for (int i = 0; i < 12; i++)
                spriteBatch.Draw(environment[i], collisionMaskRects[i], Color.White);

            foreach (MiniCrab m in miniCrabs)
            {
                if (!m.dead) m.Draw(spriteBatch);

                if (m.dead)
                {
                    m.timer++;

                    if (m.timer > m.maxTimer / 2)
                        m.Draw(spriteBatch);
                    else if (m.timer > m.maxTimer)
                        m.maxTimer++;

                    if (m.maxTimer > 20) m.destroy = true;    
                }
            }
            player.Draw(spriteBatch);
            crabKing.Draw(spriteBatch);
            crabKing.leftArm.Draw(spriteBatch);
            crabKing.rigthArm.Draw(spriteBatch);

            foreach (EnvDetails env in envDetails)
                spriteBatch.Draw(env.tex, env.pos, Color.White);

            spriteBatch.End();

            spriteBatch.Begin();
            player.bar.Draw(spriteBatch);
            spriteBatch.End();

            //Console debuging
            //Console.Clear();
            //Console.WriteLine(player.position);
            //Console.WriteLine(gameTime.IsRunningSlowly);

            base.Draw(gameTime);
        }
    }
}
