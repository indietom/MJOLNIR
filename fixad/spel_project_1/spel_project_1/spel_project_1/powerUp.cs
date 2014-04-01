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
    class powerUp:objects
    {
        public int type;

        public powerUp(float x2, float y2, int type2)
        {
            setCoords(x2, y2);
            setSize(16, 16);
            type = type2;
            switch (type)
            {
                case 1:
                    // healthpack
                    setSpriteCoords(1, frame(17));
                    break;
                case 2:
                    setSpriteCoords(frame(1), frame(17));
                    break;
                case 3:
                    setSpriteCoords(frame(2), frame(17));
                    break;
                case 4:
                    setSpriteCoords(frame(3), frame(17));
                    break;
            }
        }
        public void update(Rectangle camera)
        {
            applyOffset(camera);
        }
    }
}
