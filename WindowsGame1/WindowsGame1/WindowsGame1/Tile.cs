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
    class Tile : Object
    {
        Texture2D temp;
        int tag;
        int wh;

        public Tile(int newTag , Vector2 newPosition, int newWh)
        {
            tag = newTag;
            position = newPosition;
            wh = newWh;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(temp, position, Color.White);
        }
    }
}
