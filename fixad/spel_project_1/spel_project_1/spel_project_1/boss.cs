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
        public bool elevate;
        public int changeHeightCount;
        public bool dive;
        public int diveCounter;

        public boss(int type2, float x2, float y2)
        {
            type = type2;
            setCoords(x2, y2);
            hp = 20;
            switch (type)
            {
                case 6:
                    setSize(65, 65);
                    setSpriteCoords(166, 430);
                    maxSwitchDirection = 30 * 16 - 32;
                    direction = 3;
                    break;
                case 4:
                    setSize(65, 32);
                    setSpriteCoords(34, 430);
                    maxSwitchDirection = 30 * 16 - 32;
                    direction = 3;
                    break;
                case 3:
                    setSize(65, 32);
                    setSpriteCoords(100, 430);
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
                case 6:
                    if (direction == 3)
                    {
                        imgx = 166;
                    }
                    else
                    {
                        imgx = 232;
                    }
                    break;
            }
        }
        public void attacking(List<enemyBullet> enemyBullets, List<enemy> enemies)
        {
            switch (type)
            {
                case 6:
                    if (y <= 100)
                    {
                        diveCounter += 1;
                    }
                    if (diveCounter >= 100)
                    {
                        dive = true;
                        diveCounter = 0;
                    }
                    if (dive && hp >= 1)
                    {
                        y += 4;
                        if (y >= 480 - 16 * 4)
                        {
                            y -= 4;
                            dive = false;
                        }
                    }
                    if (!dive && hp >= 1)
                    {
                        if (y >= 100)
                        {
                            y -= 1;
                        }
                    }
                    break;
                case 3:
                    firerate += 1;
                    if (hp >= 1)
                    {
                        if (changeHeightCount == 499 && !elevate)
                        {
                            enemies.Add(new enemy(x, y, 2, 3));
                        }
                        if (elevate)
                        {
                            if (firerate == 64 || firerate == 64 + 16 || firerate == 64 + 32)
                            {
                                enemyBullets.Add(new enemyBullet(x + 32, y + 11, -270, 1, 200));
                            }
                            if (firerate > 64 + 32)
                            {
                                firerate = 0;
                            }
                        }
                    }
                    break;
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
                case 6:
                    if (!dive && y <= 100)
                    {
                        Console.WriteLine(switchDirection);
                        switchDirection += 1;
                        if (direction == 3 && switchDirection >= maxSwitchDirection)
                        {
                            direction = 4;
                            switchDirection = 0;
                        }
                        if (direction == 4 && switchDirection >= maxSwitchDirection)
                        {
                            direction = 3;
                            switchDirection = 0;
                        }
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
                    }
                    break;
                case 3:
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
                        if (elevate)
                        {
                            if (y > 50)
                            {
                                y -= 1;
                            }
                        }
                        else
                        {
                            if (y < 390 + 32)
                            {
                                y += 1;
                            }
                        }
                    }
                    changeHeightCount += 1;
                    if (changeHeightCount >= 500)
                    {
                        if (elevate)
                        {
                            elevate = false;
                        }
                        else
                        {
                            elevate = true;
                        }
                        changeHeightCount = 0;
                    }
                    switchDirection += 1;
                    if (direction == 3 && switchDirection >= maxSwitchDirection)
                    {
                        direction = 4;
                        switchDirection = 0;
                    }
                    if (direction == 4 && switchDirection >= maxSwitchDirection)
                    {
                        direction = 3;
                        switchDirection = 0;
                    }
                    break;
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
                        switchDirection = 0;
                    }
                    if (direction == 4 && switchDirection >= maxSwitchDirection)
                    {
                        direction = 3;
                        switchDirection = 0;
                    }
                    break;
            }
        }
    }
}
