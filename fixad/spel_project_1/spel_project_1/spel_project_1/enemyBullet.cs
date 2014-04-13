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
    class enemyBullet:objects
    {
        public int type;
        public int maxLifeTime;
        public int lifeTime;
        public enemyBullet(float x2, float y2, float ang, int type2, int maxLifeTime2)
        {
            type = type2;
            setCoords(x2, y2);
            angle = ang;
            destroy = false;
            maxLifeTime = maxLifeTime2;
            switch (type)
            {
                case 1:
                    speed = 7;
                    setSize(3, 3);
                    setSpriteCoords(232, 28);
                    break;
            }
        }
        public void update(Rectangle camera, SoundEffect shoot2Sfx)
        {
            applyOffset(camera);
            lifeTime += 1;
            if (lifeTime == 1)
            {
                shoot2Sfx.Play();
            }
            if (lifeTime >= maxLifeTime)
            {
                destroy = true;
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
            }
        }
    }
}
