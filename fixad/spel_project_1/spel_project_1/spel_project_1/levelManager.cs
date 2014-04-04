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
    class levelManager
    {
        // vi har inget just nu så den får vara oanvänd
        public string tilesheet;
        public bool[] levelsBeaten;
        public int currentLevel;
        public int section;
        public bool transitioning;
        public int[,] currentSectionC;
        public int[,] currentSection;
        public levelManager()
        {
            levelsBeaten = new bool[9];
            section = 1;
        }
        public void resetLevel(List<enemy> enemies, List<bullet> bullets)
        {
            enemies.Clear();
            bullets.Clear();
            section = 1;
        }
        public void roomTransition(ref bool input, List<enemy> enemies, List<bullet> bullets, player player, List<particle> particles, List<powerUp> powerUps, ref Rectangle camera, List<enemyBullet> enemyBullets, List<boss> bosses)
        {
            enemies.Clear();
            bullets.Clear();
            particles.Clear();
            enemyBullets.Clear();
            powerUps.Clear();
            if (currentLevel == 0)
            {
                if (section == 1)
                {
                    enemies.Add(new enemy(100, 100, 3, 1, 50));
                    enemies.Add(new enemy(200, 200, 2, 4));
                    enemies.Add(new enemy(300, 200, 4, 4, 100));
                    powerUps.Add(new powerUp(200, 300, 3));
                    player.x = 500;
                    player.y = 100;
                }
                if (section == 2)
                {
                    enemies.Add(new enemy(200, 200, 2, 4));
                }

            }

            if (currentLevel == 2)
            {
                if (section == 1)
                {
                    player.x = 320;
                    player.y = 260;
                }
                if (section == 2)
                {
                    player.x = 0;
                    enemies.Add(new enemy(150, 385, 1, 4));
                }
                if (section == 3)
                {
                    player.x = 0;
                    enemies.Add(new enemy(192, 385, 1, 4));
                    enemies.Add(new enemy(240, 337, 1, 4));
                    enemies.Add(new enemy(384, 193, 1, 4));
                    enemies.Add(new enemy(432, 145, 1, 4));
                }
                if (section == 4)
                {
                    player.x = 0;
                    enemies.Add(new enemy(150, 100, 2, 4));
                    enemies.Add(new enemy(500, 100, 2, 3));
                }
            }

            if (currentLevel == 3)
            {
                if (section == 1)
                {
                    enemies.Add(new enemy(350, 97, 1, 4));
                    player.x = 50;
                    player.y = 300;
                }
                if (section == 2)
                {
                    enemies.Add(new enemy(300, 90, 3, 2, 100));
                    enemies.Add(new enemy(600, 257, 1, 4));
                    player.x = 50;
                    player.y = 120;
                }
                if (section == 3)
                {
                    player.y = 0;
                    enemies.Add(new enemy(224, 226, 1, 4));
                }
                if (section == 4)
                {
                    player.x = 0;
                    enemies.Add(new enemy(600, 170, 5, 4));
                }
                if (section == 5)
                {
                    player.x = 0;
                    enemies.Add(new enemy(300, 100, 2, 4));
                }
                if (section == 6)
                {
                    player.x = 0;
                    enemies.Add(new enemy(100, 50, 4, 4, 170));
                    enemies.Add(new enemy(200, 100, 4, 4, 200));
                }
                if (section == 7)
                {
                    player.x = 0;
                }
                if (section == 8)
                {
                    player.x = 0;
                }
                if (section == 9)
                {
                    player.x = 320;
                    player.y = 0;
                }
            }
            if (currentLevel == 4)
            {
                if (section == 1)
                {
                    enemies.Add(new enemy(780, 324, 1, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 128;
                }
                if (section == 2)
                {
                    enemies.Add(new enemy(387, 371, 1, 3));
                    enemies.Add(new enemy(729, 309, 3, 3, 16*15));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 32;
                }
                if (section == 3)
                {
                    enemies.Add(new enemy(16 * 63, 16 * 19, 2, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 32;
                }
                if (section == 4)
                {
                    enemies.Add(new enemy(16 * 63, 16 * 19, 2, 3));
                    enemies.Add(new enemy(16 * 43, 16 * 19, 2, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 32;
                }
                if (section == 5)
                {
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 32;
                    player.y = 0;
                    bosses.Add(new boss(4, 539-32, 390-6));
                    enemies.Add(new enemy(5 * 16, 390 - 6 - 64, 3, 4, 30 * 16 - 32));
                }
            }
        }
        public void checkSection(level level)
        {
            level.loadLevel("level" + currentLevel + "\\section"+ section +".txt", ref currentSection);
            level.loadLevel("level" + currentLevel + "\\sectionC"+ section +".txt", ref currentSectionC);
        }
        
        public void checkLevel()
        {
            if (currentLevel == 0)
            {
                tilesheet = "spritesheet";
            }
        }
    }
}
