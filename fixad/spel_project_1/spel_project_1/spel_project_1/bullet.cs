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
    class bullet:objects
    {
        public int lifeTime;
        public int maxLifeTime;
        public int type;
        public float accel;
        public int cloudCount;

        public bullet(float x2, float y2, float ang, int maxLifeTime2, int type2)
        {
            maxLifeTime = maxLifeTime2;
            angle = ang;
            type = type2;
            setCoords(x2, y2);
            lifeTime = 0;
            switch (type)
            {
                case 1:
                    speed = 8;
                    setSize(3, 3);
                    setSpriteCoords(232, 28);
                    break;
                case 2:
                    //rocket
                    accel = -2;
                    speed = accel;
                    setSize(8, 6);
                    setSpriteCoords(232, 28);
                    break;
                case 3:
                    break;
            }
        }

        public void update(Rectangle camera, List<particle> particles)
        {
            Random random = new Random();
            applyOffset(camera);
            lifeTime += 1;
            if (lifeTime >= maxLifeTime)
            {
                destroy = true;
            }
            if (type == 2)
            {
                cloudCount += 1;
                if (cloudCount >= 8)
                {
                    if (angle == -180)
                    {
                        particles.Add(new particle(x, y, 200, 2, "grey", random.Next( -20, 20), random.Next(10, 15)));
                    }
                    if (angle == 0)
                    {
                        particles.Add(new particle(x, y, 200, 2, "grey", random.Next(-200, -160), random.Next(10, 15)));
                    }
                    cloudCount = 0;
                }
                if (angle == -180)
                {
                    setSpriteCoords(265, 2);
                }
                else
                {
                    setSpriteCoords(265, 10);
                }
            }
        }

        public void movment()
        {
            switch (type)
            {
                case 1:
                    x += veclocity_x;
                    y += veclocity_y;
                    math();
                    break;
                case 2:
                    math();
                    speed = accel;
                    accel += 0.1f;
                    x += veclocity_x;
                    y += veclocity_y;
                    break;
                case 3:
                    break;
            }
        }
    }
}
