using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spel_project_1
{
    class factory:objects
    {
        // det här objectet ska spawna andra fiender
        public int enemyType;
        public int spawnCount;
        // den här varablen är kontrellerar hur många fiender fabriken ska spawna
        public int level;

        public factory(float x2, float y2, int enemyType2, int level2)
        {
            setCoords(x2, y2);
            setSpriteCoords(1, 1);
            setSize(64, 64);
            level = level2;
            enemyType = enemyType2;
            hp = 6;
        }
        public void checkHealth(List<explosion> explosions)
        {
            if (hp <= 0)
            {
                explosions.Add(new explosion(x + 32, y, 64));
                destroy = true;
            }
        }
        public void spawnEnemeis(List<enemy> enemies)
        {
            spawnCount += 1;
            if (spawnCount >= 64 * 2)
            {
                for (int i = 0; i < level * 2; i++)
                {
                    enemies.Add(new enemy(x + 32, y + 32, enemyType, 0));
                }
                spawnCount = 0;
            }
        }
        public void animation()
        {

        }
    }
}
