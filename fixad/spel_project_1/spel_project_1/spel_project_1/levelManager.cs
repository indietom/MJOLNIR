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
            bosses.Clear();
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
                    enemies.Add(new enemy(118, 385, 1, 4));
                    enemies.Add(new enemy(224, 432, 1, 4));
                    enemies.Add(new enemy(368, 432, 1, 4));
                    powerUps.Add(new powerUp(272, 448, 3));
                }
                if (section == 3)
                {
                    player.x = 0;
                    enemies.Add(new enemy(192, 385, 1, 4));
                    enemies.Add(new enemy(240, 337, 1, 4));
                    enemies.Add(new enemy(384, 193, 1, 4));
                    enemies.Add(new enemy(432, 145, 1, 3));
                }
                if (section == 4)
                {
                    player.x = 0;
                    enemies.Add(new enemy(150, 100, 2, 4));
                    enemies.Add(new enemy(500, 100, 2, 3));
                    powerUps.Add(new powerUp(288, 112, 2));
                }
                if (section == 5)
                {
                    player.x = 0;
                    enemies.Add(new enemy(200, 50, 3, 1, 100));
                    enemies.Add(new enemy(248, 98, 3, 1, 100));
                    enemies.Add(new enemy(296, 146, 3, 1, 100));
                    enemies.Add(new enemy(344, 194, 3, 1, 100));
                    enemies.Add(new enemy(392, 242, 3, 1, 100));
                    enemies.Add(new enemy(440, 290, 3, 1, 100));
                }
                if (section == 6)
                {
                    player.x = 0;
                    enemies.Add(new enemy(300, 257, 1, 4));
                    enemies.Add(new enemy(500, 200, 3, 2, 70));
                    enemies.Add(new enemy(130, 81, 1, 3));
                    powerUps.Add(new powerUp(256, 336, 1));
                    powerUps.Add(new powerUp(64, 96, 4));
                }
                if (section == 7)
                {
                    enemies.Add(new enemy(287, 257, 1, 4));
                    enemies.Add(new enemy(464, 226, 1, 4));
                    player.x = 0;
                    enemies.Add(new enemy(178, 288, 3, 3, 50));
                    enemies.Add(new enemy(448, 112, 1, 4));
                }
                if (section == 8)
                {
                    player.x = 0;
                }
                if (section == 9)
                {
                    player.y = 0;
                    bosses.Add(new boss(2, 539 - 32, 500));
                }
            }

            if (currentLevel == 3)
            {
                if (section == 1)
                {
                    enemies.Add(new enemy(350, 97, 1, 4));
                    powerUps.Add(new powerUp(432, 288, 3));
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
                    powerUps.Add(new powerUp(240, 240, 2));
                }
                if (section == 4)
                {
                    player.x = 0;
                    enemies.Add(new enemy(400, 70, 5, 4));
                    powerUps.Add(new powerUp(320, 288, 4));
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
                    bosses.Add(new boss(3, 539 - 32, 390 - 6));
                    player.x = 320;
                    player.y = 0;
                }
            }
            if (currentLevel == 4)
            {
                if (section == 1)
                {
                    Console.WriteLine("lel");
                    enemies.Add(new enemy(780, 324, 1, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    powerUps.Add(new powerUp(921, 357 + 16, 2));
                    player.setCoords(300, 15);
                }
                if (section == 2)
                {
                    enemies.Add(new enemy(387, 371, 1, 3));
                    enemies.Add(new enemy(729, 309, 3, 3, 16*15));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 32;
                    powerUps.Add(new powerUp(617, 310 + 16, 2));
                }
                if (section == 3)
                {
                    enemies.Add(new enemy(16 * 63, 16 * 19, 2, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 17;
                    powerUps.Add(new powerUp(473, 421 + 16, 2));
                    powerUps.Add(new powerUp(493, 421 + 16, 3));
                    powerUps.Add(new powerUp(510, 421 + 16, 1));
                }
                if (section == 4)
                {
                    enemies.Add(new enemy(16 * 63, 16 * 19, 2, 3));
                    enemies.Add(new enemy(16 * 43, 16 * 19, 2, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 17;
                }
                if (section == 5)
                {
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 17;
                    player.y = 0;
                    bosses.Add(new boss(4, 539-32, 390-6));
                    enemies.Add(new enemy(5 * 16, 390 - 6 - 64, 3, 4, 30 * 16 - 32));
                }
            }
            if (currentLevel == 6)
            {
                if (section == 1)
                {
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 50;
                    player.y = 300;
                }
                if (section == 2)
                {
                    enemies.Add(new enemy(513, 338, 1, 3));
                    enemies.Add(new enemy(956, 388, 1, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.setCoords(17, 32);
                    powerUps.Add(new powerUp(612 + 16, 338 + 16, 3));
                }
                if (section == 3)
                {
                    enemies.Add(new enemy(540, 326, 3, 2, 16*10));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 17;
                }
                if (section == 4)
                {
                    enemies.Add(new enemy(473, 324, 1, 3));
                    camera.X = 0;
                    camera.Y = 0;
                    player.x = 17;
                    powerUps.Add(new powerUp(407 + 16, 372 + 16, 3));
                }
                if (section == 5)
                {
                    player.y = 350;
                    player.x = 17;
                    camera.X = 0;
                    camera.Y = 0;
                    powerUps.Add(new powerUp(1076+16, 420 + 16, 1));
                    powerUps.Add(new powerUp(1076 + 16, 420 + 16, 1));
                }
                if (section == 6)
                {
                    player.y = 0;
                    player.x = 320;
                    bosses.Add(new boss(6, 539 - 32, 90));
                }
            }

            if (currentLevel == 5)
            {
                if (section == 1)
                {
                    player.setCoords(100, 100);
                    player.hp = 10;
                    player.respawnCounter = 0;
                    player.dead = false;
                    Console.WriteLine(player.x+"\n"+player.y);
                }
                if (section == 2)
                {
                    player.x = 17;
                    enemies.Add(new enemy(80, 400, 1, 4));
                    enemies.Add(new enemy(288, 368, 1, 4));
                    powerUps.Add(new powerUp(288, 448, 4));
                }
                if (section == 3)
                {
                    player.x = 17;
                    enemies.Add(new enemy(544, 432, 3, 4, 60));
                    enemies.Add(new enemy(368, 288, 1, 4));
                    powerUps.Add(new powerUp(544, 448, 2));
                }
                if (section == 4)
                {
                    player.x = 17;
                    enemies.Add(new enemy(352, 336, 1, 4));
                    enemies.Add(new enemy(448, 416, 2, 4));
                    enemies.Add(new enemy(96, 416, 2, 4));
                    powerUps.Add(new powerUp(544, 448, 3));
                }
                if (section == 5)
                {
                    player.x = 17;
                    enemies.Add(new enemy(432, 336, 2, 4));
                }
                if (section == 6)
                {
                    player.x = 17;
                    enemies.Add(new enemy(400, 240, 3, 2, 60));
                    enemies.Add(new enemy(528, 256, 1, 4));
                }
                if (section == 7)
                {
                    player.x = 17;
                    enemies.Add(new enemy(480, 304, 1, 4));
                    enemies.Add(new enemy(80, 208, 3, 1, 100));
                }
                if (section == 8)
                {
                    player.x = 17;
                }
                if (section == 9)
                {
                    player.x = 17;
                    bosses.Add(new boss(5, 539 - 32, 90));
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
