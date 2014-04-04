using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spel_project_1
{
    class boss:objects
    {
        public int type;
        public int switchDirection;
        public int maxSwitchDirection;
        public int gotoMenuCount;
        public int firerate;

        public boss(int type2, float x2, float y2)
        {
            type = type2;
            setCoords(x2, y2);
            hp = 25;
            switch (type)
            {
                case 4:
                    setSize(65, 32);
                    setSpriteCoords(34, 430);
                    maxSwitchDirection = 30 * 16 - 32;
                    direction = 3;
                    break;
            }
        }
        public void animation()
        {
            switch(type)
            {
                case 4:
                    if(direction == 3)
                    {
                        imgy = 463;
                    }
                    else
                    {
                        imgy = 430;
                    }
                    break;
            }
        }
        public void attacking(List<enemyBullet> enemyBullets)
        {
            switch (type)
            {
                case 4:
                    firerate += 1;
                    if (hp >= 1)
                    {
                        if (firerate == 64 || firerate == 64 + 16 || firerate == 64 + 32)
                        {
                            if (direction == 3)
                            {
                                enemyBullets.Add(new enemyBullet(x + 32, y + 11, -180, 1, 200));
                            }
                            if (direction == 4)
                            {
                                enemyBullets.Add(new enemyBullet(x + 32, y + 11, 0, 1, 200));
                            }
                        }
                        if (firerate > 64 + 32)
                        {
                            firerate = 0;
                        }
                    }
                    break;
            }
        }
        public void checkHealth(levelManager lm, ref string gameState, List<explosion> explosions)
        {
            if (hp <= 0)
            {
                if (gotoMenuCount <= 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        explosions.Add(new explosion(x + 16 * i, y, 32));
                    }
                }
                gotoMenuCount += 1;
                if (gotoMenuCount >= 128)
                {
                    lm.section = 1;
                    lm.levelsBeaten[type] = true;
                    gameState = "menu";
                    destroy = true;
                }
            }
        }
        public void movment()
        {
            switch (type)
            {
                case 4:
                    if (hp >= 1)
                    {
                        if (direction == 3)
                        {
                            x -= 1;
                        }
                        else
                        {
                            x += 1;
                        }
                    }
                    switchDirection += 1;
                    if (direction == 3 && switchDirection >= maxSwitchDirection)
                    {
                        direction = 4;
                        Console.WriteLine(direction);
                        switchDirection = 0;
                    }
                    if (direction == 4 && switchDirection >= maxSwitchDirection)
                    {
                        direction = 3;
                        Console.WriteLine(direction);
                        switchDirection = 0;
                    }
                    break;
            }
        }
    }
}
