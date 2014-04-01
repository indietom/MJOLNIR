using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spel_project_1
{
    class boss:objects
    {
        public int type;

        public boss(int type2)
        {
            setSize(32, 32);
            type = type2;
            setCoords(320, 240);
            switch (type)
            {

            }
        }
        public void animation()
        {

        }
        public void attacking()
        {

        }
        public void movment()
        {

        }
    }
}
