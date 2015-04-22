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
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Camera camera;
        MiniCrab miniCrab;
        CrabKing crabKing;

        private const int test = 5;

        List<Tile> tiles = new List<Tile>();
        List<Rectangle> rectangeList = new List<Rectangle>();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            TextureManager.InitializeTextures.Add("player", "spriteplaceholder");
            TextureManager.InitializeTextures.Add("minicrab", "lil krabba spritesheet");
            TextureManager.InitializeTextures.Add("CrabKing", "CrabKing");
            TextureManager.InitializeTextures.Add("CrabArm", "CrabArm");
            TextureManager.InitializeTextures.Add("bar", "bar");
            TextureManager.InitializeTextures.Add("hp", "hp");
            TextureManager.InitializeTextures.Add("stamina", "stamina");
            TextureManager.InitializeTextures.Add("bord", "bord");

            for (int i = 1; i < 17; i++)            
                TextureManager.InitializeTextures.Add(i.ToString(), "mapParts/mapPart" + i);

            //JAG HAR SYNDAT! SORRY JESS!
            TextureManager.LoadContent(Content);

            player = new Player();
            miniCrab = new MiniCrab();
            crabKing = new CrabKing();
            camera = new Camera();

            Map map = MapLoader.LoadMap("map.txt");
            int hw = 64;

            for (int x = 0; x < map.X; x++)
                for (int y = 0; y < map.Y; y++)
                {
                    if (map[x, y] != 0 && map[x, y] < 17)
                        tiles.Add(new Tile(map[x, y].ToString(), new Rectangle(x * hw, y * hw, 1024, 1024)));

                    if (map[x,y] == 17)
                    {
                        rectangeList.Add(new Rectangle(x * 64, y * 64, 64, 64));
                    }
                }
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(rectangeList);
            miniCrab.Update(player);
            camera.Pos = player.position + new Vector2(player.width/2, player.height/2);
            crabKing.Update(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null,null,null,null,camera.get_transformation(GraphicsDevice));
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
            miniCrab.Draw(spriteBatch);
            player.Draw(spriteBatch);
            crabKing.Draw(spriteBatch);
            crabKing.leftArm.Draw(spriteBatch);
            crabKing.rigthArm.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            player.bar.Draw(spriteBatch);
            spriteBatch.End();

            //Console debuging
            Console.Clear();
            Console.WriteLine(player.position);
            Console.WriteLine(gameTime.IsRunningSlowly);

            base.Draw(gameTime);
        }
    }
}
