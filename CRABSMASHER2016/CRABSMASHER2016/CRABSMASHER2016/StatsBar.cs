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
    public class StatsBar
    {
        Vector2 position = new Vector2(0, 0);

        Rectangle hpRect;
        Rectangle staminaRect;

        Texture2D bord = TextureManager.Textures["bord"];
        Texture2D stamina = TextureManager.Textures["stamina"];
        Texture2D hp = TextureManager.Textures["hp"];
        Texture2D bar = TextureManager.Textures["bar"];

        public StatsBar()
        {
            staminaRect = stamina.Bounds;
            hpRect = hp.Bounds;
            
        }

        public void update(float hpProcentage, float staminaProcentage)
        {
            
            staminaRect.Width = (int)(stamina.Bounds.Width * staminaProcentage);
            hpRect.Width = (int)(hp.Bounds.Width * hpProcentage);
            
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(bord, position, Color.White);
            sp.Draw(stamina, position + new Vector2(135, 42), staminaRect, Color.White);
            sp.Draw(hp, position + new Vector2(135, 88), hpRect, Color.White);
            sp.Draw(bar, position, Color.White);
        }
    }
}
