using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace spel_project_1
{
    class explosion:objects
    {
        public int size;

        public explosion(float x2, float y2, int size2)
        {
            setCoords(x2, y2);
            size = size2;
            switch (size)
            {
                case 16:
                    setSize(32, 32);
                    setSpriteCoords(1, 298);
                    break;
                case 32:
                    setSize(32, 32);
                    setSpriteCoords(1, 331);
                    break;
                case 64:
                    setSpriteCoords(1, 364);
                    setSize(64, 64);
                    break;
            }
        }
        public void animation(SoundEffect explosionSfx)
        {
            animationCount += 1;
            if (animationCount == 1)
            {
                explosionSfx.Play();
            }
            if (size <= 32)
            {
                if (animationCount >= 5)
                {
                    imgx += 33;
                    animationCount = 0;
                }
            }
            else
            {
                if (animationCount >= 5)
                {
                    imgx += 65;
                    animationCount = 0;
                }
            }
            if (imgx >= frame(4) && size <= 32)
            {
                destroy = true;
            }
            if (imgx >= frame(9) && size > 32)
            {
                destroy = true;
            }
        }
    }
}
