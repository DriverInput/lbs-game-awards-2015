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
        Map map;
        Player player;

        List<Tile> tiles = new List<Tile>();

        #region Top Hemligt
            //Jag ljög
        #endregion

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(Content);
        }
        #region DONT TOUCH!
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //HAHA #REKT MLG 360NOSCOPED FAG GIT GUD SNASE!

            base.Initialize();
        }
        #endregion
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            map = MapLoader.LoadMap("C:\\PerfLogs\\Nytt textdokument.txt");
            for (int y = 0; y < map.Y; y++)
            {
                for (int x = 0; x < map.X; x++)
                {
                    if (map[x,y] > 1)
                    {
                        tiles.Add(new Tile((int)map[x, y], new Vector2(x, y), 64));
                    }
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {

            player.Update(gameTime);


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(gameTime);
        }
    }
}
