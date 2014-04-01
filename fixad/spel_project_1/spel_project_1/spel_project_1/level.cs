using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spel_project_1
{
    class level
    {
        public void drawLevel(SpriteBatch spriteBatch, Texture2D tileSet, int[,] map, Rectangle camera, int width, int height)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    for (int i = 0; i < 320; i++)
                    {
                        if (map[y, x] == i)
                        {
                            spriteBatch.Draw(tileSet, new Rectangle((x * 16 - camera.X), y * 16 - camera.Y, 16, 16), new Rectangle(i * 16 - 16, 0, 16, 16), Color.White);
                        }
                    }
                }
        }

        public void loadLevel(string name, ref int[,] map)
        {
            string mapData = name;
            int width = 0;
            int height = File.ReadLines(mapData).Count();

            StreamReader sReader = new StreamReader(mapData);
            string line = sReader.ReadLine();
            string[] tileNo = line.Split(',');

            width = tileNo.Count();

            // Creating a new instance of the tile map
            map = new int[height, width];
            sReader = new StreamReader(mapData);
            //
            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                {
                    map[y, x] = Convert.ToInt32(tileNo[x]);
                }
            }
            sReader.Close();
        }
    }
}











