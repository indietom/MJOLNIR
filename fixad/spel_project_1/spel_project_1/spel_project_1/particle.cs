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
    class particle:objects
    {
        public int maxLifeTime;
        public int lifeTime;
        public int type;
        public float accel;

        public particle(float x2, float y2, int maxLifeTime2, int type2, string color, float ang, float spe)
        {
            setCoords(x2, y2);
            destroy = false;
            type = type2;
            maxLifeTime = maxLifeTime2;
            angle = ang;
            setSize(3, 3);
            switch (color)
            {
                case "lightblue":
                    setSpriteCoords(232, 7);
                    break;
                case "yellow":
                    setSpriteCoords(232, 4);
                    break;
                case "lightgreen":
                    break;
                case "red":
                    setSpriteCoords(232, 1);
                    break;
                case "shotgunshell":
                    setSize(6, 3);
                    setSpriteCoords(238, 1);
                    break;
                case "grey":
                    setSpriteCoords(235, 25);
                    break;
            }
            switch (type)
            {
                case 1:
                    accel = spe;
                    speed = accel;
                    break;
                case 2:
                    speed = spe;
                    break;
                case 3:
                    accel = spe;
                    speed = accel;
                    break;
                case 4:
                    accel = spe;
                    speed = accel;
                    break;
            }
        }
        public void update(Rectangle camera)
        {
            applyOffset(camera);
            if (type != 1)
            {
                lifeTime += 1;
                if (lifeTime >= maxLifeTime)
                {
                    destroy = true;
                }
            }
        }
        public void movment()
        {
            switch (type)
            {
                case 1:
                    math();
                    speed = accel;
                    x += veclocity_x;
                    y += veclocity_y;
                    y += 0.1f;
                    accel -= 0.7f;
                    if (accel <= 0)
                    {
                        destroy = true;
                    }
                    break;
                case 2:
                    math();
                    x += veclocity_x;
                    y += veclocity_y;
                    break;
                case 3:
                    math();
                    x += veclocity_x;
                    y += veclocity_y;
                    y += gravity;
                    speed = accel;
                    accel -= 0.1f;
                    break;
                case 4:
                    math();
                    x += veclocity_x;
                    y += veclocity_y;
                    speed = accel;
                    accel -= 0.01f;
                    break;
            }
        }
    }
}
