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
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        MiniCrab miniCrab;
        Camera camera;

        List<Tile> tiles = new List<Tile>();

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

            for (int i = 1; i < 17; i++)            
                TextureManager.InitializeTextures.Add(i.ToString(), "mapParts/mapPart" + i);
            
            player = new Player();
            miniCrab = new MiniCrab();
            camera = new Camera();

            
            //Map map = MapLoader.LoadMap("");
            //int hw = 32;

            //for (int x = 0; x < map.X; x++)
            //    for (int y = 0; y < map.Y; y++)
            //        tiles.Add(new Tile(map[x,y].ToString(), new Rectangle(x * hw, y * hw, hw, hw )));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update();
            miniCrab.Update(player);
            camera.Pos = player.position + new Vector2(player.width/2, player.height/2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null,null,null,null,camera.get_transformation(GraphicsDevice));
            player.Draw(spriteBatch);
            miniCrab.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
