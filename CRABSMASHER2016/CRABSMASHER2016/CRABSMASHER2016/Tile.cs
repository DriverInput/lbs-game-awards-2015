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
    class Tile : Object
    {
        public Tile(string newTextureId, Rectangle newRectangle)
        {
            textureID = newTextureId;
            position = new Vector2(newRectangle.X, newRectangle.Y);
            width = newRectangle.Width;
            height = newRectangle.Height;
        }
    }
}
