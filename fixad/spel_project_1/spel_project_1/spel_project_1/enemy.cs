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
    class enemy:objects
    {
        public int type;
        public int firerate;
        public bool shooting;
        public int gunType;
        public int walkCount;
        public bool vulnerable;
        public bool onWall;
        public bool onGround;
        public bool mech;
        public int switchDirection;
        public int maxSwitchDirection;

        public enemy(float x2, float y2, int type2, int direction2)
        {
            vulnerable = true;
            setCoords(x2, y2);
            type = type2;
            direction = direction2;
            animationActive = true;
            destroy = false;
            vulnerable = true;
            switch (type)
            {
                // snubben med sköld
                case 1:
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 3;
                    break;
                case 2:
                    //Mech
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 5;
                    mech = true;
                    break;
                case 3:
                    // spike ball thing
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 1;
                    break;
                case 4:
                    // chopper
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 5;
                    break;
                    //Flying Mech
                case 5:
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 5;
                    break;
            }
        }
        public enemy(float x2, float y2, int type2, int direction2, int maxSwitchDirection2)
        {
            maxSwitchDirection = maxSwitchDirection2;
            vulnerable = true;
            setCoords(x2, y2);
            type = type2;
            direction = direction2;
            animationActive = true;
            destroy = false;
            vulnerable = true;
            switch (type)
            {
                // snubben med sköld
                case 1:
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 5;
                    break;
                case 2:
                    //Mech
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 10;
                    mech = true;
                    break;
                case 3:
                    // spike ball thing
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 1;
                    vulnerable = false;
                    break;
                case 4:
                    // chopper
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 5;
                    break;
                //Flying Mech
                case 5:
                    setSpriteCoords(1, 1);
                    setSize(32, 32);
                    hp = 10;
                    break;
            }
        }
        public void checkHealth(List<explosion> explosions)
        {
            if (hp <= 0)
            {
                if (type == 1)
                {
                    explosions.Add(new explosion(x, y, 16));
                }
                if (type == 2 || type == 5)
                {
                    explosions.Add(new explosion(x, y, 32));
                }
                if (type == 4)
                {
                    explosions.Add(new explosion(x, y, 64));
                }
                destroy = true;
            }
        }
        public void update(Rectangle camera)
        {
            applyOffset(camera);
        }
        public void attacking(List<enemyBullet> enemyBullets, player player, List<particle> particles)
        {
            Random random = new Random();
            switch (type)
            {
                case 1:
                    if (player.x >= x)
                    {
                        direction = 4;
                    }
                    else
                    {
                        direction = 3;
                    }
                    firerate += 2;
                    if (firerate >= 32 * 3 && distanceTo(player.x, player.y) <= 440)
                    {
                        vulnerable = true;
                        shooting = true;
                        if (firerate == 32 * 4)
                        {
                            if (direction == 3)
                            {
                                enemyBullets.Add(new enemyBullet(x + 16, y + 16, -180, 1, 200));
                                particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -70, 7));
                            }
                            else
                            {
                                enemyBullets.Add(new enemyBullet(x + 16, y + 16, 0, 1, 200));
                                particles.Add(new particle(x + 16, y + 10, 200, 3, "yellow", -110, 7));
                            }
                        }
                    }
                    if (firerate >= 32 * 6)
                    {
                        shooting = false;
                        vulnerable = false;
                        firerate = 0;
                    }
                    break;
                case 2:
                    if (walkCount == 64 + 32 || walkCount == 64 * 2 || walkCount == 64 * 2 + 32 || walkCount == 64 * 3 || walkCount == 64 * 3 + 32 || walkCount == 64 * 4)
                    {
                        if (direction == 3)
                        {
                            if (distanceTo(player.x, player.y) <= 440)
                                enemyBullets.Add(new enemyBullet(x, y + 16, random.Next(-200, -160), 1, 200));
                        }
                        else
                        {
                            if (distanceTo(player.x, player.y) <= 440)
                                enemyBullets.Add(new enemyBullet(x, y + 16, random.Next(-20, 20), 1, 200));
                        }
                    }
                    if (player.x < x)
                    {
                        direction = 3;
                    }
                    else
                    {
                        direction = 4;
                    }
                    y += 6;
                    vulnerable = true;
                    break;
                case 3:
                    if (direction == 1)
                    {
                        y += 1;
                    }
                    if (direction == 2)
                    {
                        y -= 1;
                    }
                    if (direction == 3)
                    {
                        x -= 1;
                    }
                    if (direction == 4)
                    {
                        x += 1;
                    }
                    switchDirection += 1;
                    if (switchDirection >= maxSwitchDirection && direction == 4)
                    {
                        direction = 3;
                        switchDirection = 0;
                    }
                    if (switchDirection >= maxSwitchDirection && direction == 3)
                    {
                        direction = 4;
                        switchDirection = 0;
                    }
                    if (switchDirection >= maxSwitchDirection && direction == 1)
                    {
                        direction = 2;
                        switchDirection = 0;
                    }
                    if (switchDirection >= maxSwitchDirection && direction == 2)
                    {
                        direction = 1;
                        switchDirection = 0;
                    }
                    break;
                    
                case 4:
                    firerate += 1;
                    if (firerate == 64 || firerate == 64 + 32 || firerate == 128)
                    {
                        if (direction == 3)
                        {
                            enemyBullets.Add(new enemyBullet(x + 16, y + 16, random.Next(-280, -200), 1, 200));
                        }
                        else
                        {
                            enemyBullets.Add(new enemyBullet(x + 16, y + 16, random.Next(-350, -270), 1, 200));
                        }
                    }
                    if (firerate >= 128 + 32)
                    {
                        firerate = 0;
                    }
                    if (maxSwitchDirection >= 1)
                    {
                        switchDirection += 1;
                        if (switchDirection >= maxSwitchDirection)
                        {
                            if (direction == 3)
                            {
                                direction = 4;
                            }
                            else
                            {
                                direction = 3;
                            }
                            switchDirection = 0;
                        }
                    }
                    break;
                case 5:
                    firerate += 1;
                    if (player.x < x)
                    {
                        direction = 3;
                    }
                    else
                    {
                        direction = 4;
                    }
                    if (player.y < y)
                    {
                        y -= 1;
                    }
                    if (player.y > y)
                    {
                        y += 1;
                    }
                    if (firerate == 64)
                    {
                        if (direction == 3)
                        {
                            enemyBullets.Add(new enemyBullet(x, y + 16, random.Next(-200, -160), 1, 200));
                        }
                        else
                        {
                            enemyBullets.Add(new enemyBullet(x, y + 16, random.Next(-20, 20), 1, 200));
                        }
                        firerate = 0;
                    }
                    break;
            }
        }
        public void movment()
        {
            switch (type)
            {
                case 2:
                    walkCount += 1;
                    if (walkCount <= 64 * 2)
                    {
                        if (direction == 3)
                        {
                            x -= 1;
                        }
                        else
                        {
                            x += 1;
                        }   
                        animationCount += 1;
                    }
                    if (walkCount >= 64 * 5)
                    {
                        animationCount = 0;
                        imgx = frame(11);
                        walkCount = 0;
                    }
                    break;
                case 4:
                    if (direction == 3)
                    {
                        x -= 1;
                    }
                    else
                    {
                        x += 1;
                    }
                    break;
                case 5:

                    if (direction == 3)
                    {
                        x -= 1;
                    }
                    else
                    {
                        x += 1;
                    }
                    break;
            }
        }
        public void animation()
        {
            switch (type)
            {
                case 1:
                    if (shooting)
                    {
                        imgx = frame(1);
                    }
                    else
                    {
                        imgx = 1;
                    }
                    if (direction == 3)
                    {
                        imgy = frame(3);
                    }
                    else
                    {
                        imgy = frame(4);
                    }
                    break;
                case 2:
                    if (direction == 3)
                    {
                        imgy = frame(1);
                    }
                    else
                    {
                        imgy = 1;
                    }
                    if (animationCount == 10)
                    {
                        imgx = frame(12);
                    }
                    if (animationCount == 20)
                    {
                        imgx = frame(13);
                    }
                    if (animationCount == 30)
                    {
                        imgx = frame(14);
                    }
                    if (animationCount == 40)
                    {
                        imgx = frame(11);
                        animationCount = 0;
                    }
                    break;
                case 5:
                    animationCount += 1;
                    if (direction == 3)
                    {
                        imgy = frame(3);
                    }
                    else
                    {
                        imgy = frame(2);
                    }
                    if (animationCount == 10)
                    {
                        imgx = frame(12);
                    }
                    if (animationCount == 20)
                    {
                        imgx = frame(13);
                    }
                    if (animationCount == 30)
                    {
                        imgx = frame(14);
                    }
                    if (animationCount == 40)
                    {
                        imgx = frame(11);
                        animationCount = 0;
                    }
                    break;
            }
        }
    }
}
