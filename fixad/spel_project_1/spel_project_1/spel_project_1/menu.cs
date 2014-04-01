using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spel_project_1
{
    class menu:objects
    {
        public int selectedBoss;

        public menu(List<titleCard> titleCards)
        {
            for (int y = 0; y < 3; y++)
            {
                titleCards.Add(new titleCard(150 + 58, 150 + y * 64, titleCards.Count));
                titleCards.Add(new titleCard(250 + 58, 150 + y * 64, titleCards.Count));
                titleCards.Add(new titleCard(350 + 58, 150 + y * 64, titleCards.Count));
            }

        }

        public void updateMenu(List<titleCard> titleCards, levelManager levelManager)
        {
            
        }

        public void drawMenu(SpriteBatch spriteBatch, Texture2D spritesheet, int cutSceneCount)
        {
            MouseState mouse = Mouse.GetState();
            spriteBatch.Draw(spritesheet, new Vector2(mouse.X, mouse.Y), new Rectangle(0, 0, 8, 8), Color.White);
            if (cutSceneCount >= 1)
            {
                // rite boss grej
            }
        }
    }
}
