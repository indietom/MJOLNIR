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
        public bool reSurface;
        public int reSurfaceCounter;
        public bool snipepos;
        public int snipeposCount;
        public bool jumping;
        public int jumpingCount;
        public float jumpForce;
        public int jumpInterval;
        public bool hit;

        public boss(int type2, float x2, float y2)
        {
            type = type2;
            setCoords(x2, y2);
            hp = 15;
            switch (type)
            {
                case 8:
                    direction = 3;
                    setSpriteCoords(364, 364);
                    setSize(65, 65);
                    hp = 30;
                    break;
                case 1:
                    setSpriteCoords(430, 430);
                    setSize(65, 65);
                    direction = 3;
                    maxSwitchDirection = 30 * 16 - 32;
                    break;
                case 7:
                    setSpriteCoords(232, 496);
                    setSize(32, 32);
                    maxSwitchDirection = 640-32;
                    direction = 3;
                    hp = 10;
                    break;
                case 0:
                    setSize(65, 65);
                    setSpriteCoords(364, 430);
                    break;
                case 5:
                    setSize(32, 32);
                    setSpriteCoords(166, 496);
                    hp = 10;
                    break;
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
                case 2:
                    setSize(65, 65);
                    setSpriteCoords(298, 430);
                    maxSwitchDirection = 20 * 16 - 32;
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
                    if (!dive)
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
            Random random = new Random();
            switch (type)
            {
                case 8:
                    if (hp >= 1)
                    {
                        firerate += 1;
                        if (firerate >= 64)
                        {
                            if (jumping)
                            {
                                enemyBullets.Add(new enemyBullet(x + 32, y + 16, -270, 2, 200));
                            }
                            else
                            {
                                if(direction == 3)
                                    enemyBullets.Add(new enemyBullet(x + 32, y + 16, -180, 2, 200));
                                else
                                    enemyBullets.Add(new enemyBullet(x + 32, y + 16, -0, 2, 200));
                            }
                            firerate = 0;
                        }
                        if (direction == 3)
                        {
                            imgx = 364;
                            x--;
                        }
                        else
                        {
                            imgx = 364 + 66; 
                            x++;
                        }
                        if (direction == 3 && x < 16 * 5)
                        {
                            direction = 4;
                            x += 5;
                            direction = 4;
                        }
                        if (direction == 4 && x >= 640 - 32 - 16 * 5)
                        {
                            direction = 3;
                            x -= 5;
                            direction = 3;
                        }
                        jumpingCount += 1;
                        if (jumpingCount == 600)
                        {
                            jumping = true;
                        }
                        if (jumping)
                        {
                            y -= jumpForce;
                            if (jumpForce > 0)
                                jumpForce -= 0.1f;
                            jumpInterval += 1;

                            if (jumpInterval >= 50)
                            {
                                if (y >= 300)
                                    jumpForce = 8;
                                else
                                    jumpInterval = 0;
                            }
                            if (y <= 480 - 16 * 6)
                                y += 3;
                            if (jumpingCount >= 2000 && jumpInterval <= 0 && jumpForce <= 0)
                            {
                                jumping = false;
                                jumpingCount = 0;
                            }
                        }
                        if (!jumping)
                        {
                            if (y <= 480 - 16 * 6)
                                y += 1;
                        }
                    }
                    break;
                case 1:
                    if (direction == 3 && x < 16*5)
                        {
                            direction = 4;
                            x += 5;
                            direction = 4;
                        }
                        if (direction == 4 && x >= 640-32-16*5)
                        {
                            direction = 3;
                            x -= 5;
                            direction = 3;
                        }
                        if (direction == 3)
                        {
                            imgx = 496;
                            if (hp >= 1)
                                x--;
                        }
                        else
                        {
                            imgx = 430;
                            if (hp >= 1)
                                x++;
                        }
                    break;
                case 0:
                    firerate += 1;
                    if (firerate == 64 || firerate == 64 + 16 || firerate == 64 + 32)
                    {
                        enemyBullets.Add(new enemyBullet(x + 32, y + 32, -270, 1, 200));
                    }
                    if (firerate >= 64 + 32 + 16)
                    {
                        firerate = 0;
                    }
                    y += 2;
                    if (y >= 600)
                    {
                        y = -100;
                        x = random.Next(640 - 65);
                    }
                    break;
                case 5:
                    if (!snipepos)
                    {
                        setSpriteCoords(199, 496);
                    }
                    else
                    {
                        setSpriteCoords(166, 496);
                    }
                    if (hp <= 0)
                    {
                        setSpriteCoords(199, 496);
                        y += 7;
                    }
                    if (snipepos && hp >= 1)
                    {
                        firerate += 1;
                        if (firerate == 16 || firerate == 32 || firerate == 32+16)
                        {
                            enemyBullets.Add(new enemyBullet(x + 16, y + 16, -270, 1, 200));
                        }
                        if (firerate >= 64+32)
                        {
                            firerate = 0;
                        }
                    }
                    diveCounter += 1;
                    if (diveCounter >= 500 && hp >= 1 && snipepos)
                    {
                        snipepos = false;
                        diveCounter = 0;
                    }
                    if (!snipepos)
                    {
                        y += 3;
                    }
                    if (y >= 700 && hp >= 1)
                    {
                        x = random.Next(640 - 32);
                        y = -32;
                        snipepos = true;
                    }
                    if (y < 0 && snipepos)
                    {
                        y += 1;
                    }
                    break;
                case 7:
                    if (!snipepos)
                    {
                        setSpriteCoords(265, 496);
                    }
                    else
                    {
                        setSpriteCoords(232, 496);
                    }
                    if (hp <= 0)
                    {
                        setSpriteCoords(265, 496);
                        y += 7;
                    }
                    if (snipepos && hp >= 1)
                    {
                        firerate += 1;
                        if (firerate == 16 || firerate == 32 || firerate == 32+16)
                        {
                            enemyBullets.Add(new enemyBullet(x + 16, y + 16, -270, 1, 200));
                        }
                        if (firerate >= 64+32)
                        {
                            firerate = 0;
                        }
                        if (direction == 3 && x < 0)
                        {
                            direction = 4;
                            x += 5;
                            direction = 4;
                        }
                        if (direction == 4 && x >= 640-32)
                        {
                            direction = 3;
                            x -= 5;
                            direction = 3;
                        }
                        if (direction == 3)
                            x--;
                        else
                            x++;
                    }
                    diveCounter += 1;
                    if (diveCounter >= 1000 && hp >= 1 && snipepos)
                    {
                        snipepos = false;
                        diveCounter = 0;
                    }
                    if (!snipepos)
                    {
                        y += 3;
                    }
                    if (y >= 700 && hp >= 1)
                    {
                        x = random.Next(640 - 32);
                        y = -32;
                        snipepos = true;
                    }
                    if (y < 0 && snipepos)
                    {
                        y += 1;
                    }
                    break;
                case 2:
                    firerate += 1;
                    if (firerate >= 24 && hp >= 1)
                    {
                        if (y < 500)
                        {
                            enemyBullets.Add(new enemyBullet(x + 65 / 2, y + 65 / 2, 0, 1, 200));
                            enemyBullets.Add(new enemyBullet(x + 65 / 2, y + 65 / 2, -180, 1, 200));
                        }
                        firerate = 0;
                    }
                    if (y >= 500)
                    {
                        reSurfaceCounter += 1;
                    }
                    if (reSurfaceCounter >= 100)
                    {
                        reSurface = true;
                        reSurfaceCounter = random.Next(64);
                    }
                    if (reSurface && hp >= 1)
                    {
                        y -= 4;
                        if (y <= 420 - 16 * 4)
                        {
                            y += 4;
                            reSurface = false;
                        }
                    }
                    if (!reSurface && hp >= 1)
                    {
                        if (y <= 500)
                        {
                            y += 1;
                        }
                    }
                    break;
                case 6:
                    if (y <= 100)
                    {
                        diveCounter += 1;
                    }
                    if (diveCounter >= 100)
                    {
                        dive = true;
                        diveCounter = random.Next(64);
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
                                enemyBullets.Add(new enemyBullet(x + 32, y + 4, -185, 1, 200));
                            }
                            if (direction == 4)
                            {
                                enemyBullets.Add(new enemyBullet(x + 32, y + 4, -355, 1, 200));
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
        public void checkHealth(levelManager lm, ref string gameState, List<explosion> explosions, ref player player, List<particle> particles)
        {
            Random random = new Random();
            if (hit)
            {
                for (int i = 0; i < 50; i++)
                {
                    particles.Add(new particle(x + random.Next(32), y + random.Next(32), 200, 3, "grey", random.Next(-270, -80), 10));
                }
                hit = false;
            }
            if (hp <= 0)
            {
                if (gotoMenuCount <= 0)
                {
                    if (type != 7 && type != 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            explosions.Add(new explosion(x + 16 * i, y, 32));
                        }
                    }
                    else
                    {
                        explosions.Add(new explosion(x, y, 32));
                    }
                }
                gotoMenuCount += 1;
                if (gotoMenuCount >= 128)
                {
                    player.setCoords(320, 240);
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
                case 2:
                    if (hp >= 1 && !reSurface && y >= 420)
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
                    if (y >= 500)
                    {
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
