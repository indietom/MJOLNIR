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
    class titleCard:objects
    {
        public int bossNumber;
        public int cutSceneCount;
        public titleCard(float x2, float y2, int bossNumber2)
        {
            setCoords(x2, y2);
            setSize(32, 32);
            bossNumber = bossNumber2;
            imgy = frame(8);
            imgx = frame(bossNumber);
            if (bossNumber == 0)
            {
                imgx = 1;
            }
        }
        public void update(levelManager lm, ref string gameState, List<enemy> enemies, List<bullet> bullets, player player, List<particle> particles, List<powerUp> powerUps, Rectangle camera, List<enemyBullet> enemyBullets, List<boss> bosses)
        {
            MouseState mouse = Mouse.GetState();
            Rectangle cursor = new Rectangle(mouse.X, mouse.Y, 8, 8);
            Rectangle titleCardC = new Rectangle((int)x, (int)y, 32, 32);
            if (cursor.Intersects(titleCardC) && cutSceneCount <= 0)
            {
                imgy = frame(7);
                if (mouse.LeftButton == ButtonState.Pressed && !lm.levelsBeaten[bossNumber])
                {
                    lm.currentLevel = bossNumber;
                    cutSceneCount = 1;
                }
            }
            else
            {
                imgy = frame(8);
            }
            // du vad grabben
            if (cutSceneCount >= 1)
            {
                cutSceneCount += 1;
                if (cutSceneCount >= 64 * 2)
                {
                    gameState = "game";
                    lm.roomTransition(ref player.inputActive, enemies, bullets, player, particles, powerUps, ref camera, enemyBullets, bosses);
                    cutSceneCount = 0;
                }
            }
        }
    }
}
