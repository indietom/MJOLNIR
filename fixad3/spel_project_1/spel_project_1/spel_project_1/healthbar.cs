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
    class healthbar:objects
    {
        public healthbar()
        {
            setSpriteCoords(1, 430);
            setSize(10, 100);
            setCoords(0, 0);
        }
    }
}
