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
    class objects
    {
        public float x;
        public float y;
        public float renderX;
        public float renderY;

        public int imgx;
        public int imgy;
        public int width;
        public int height;
        public int hp;
        public int animationCount;

        public int direction;
        public int jumpDirection;

        public bool animationActive;
        public bool destroy;

        public float angle2;
        public float angle;
        public float speed;
        public float scale_x;
        public float scale_y;
        public float veclocity_x;
        public float veclocity_y;
        public const float gravity = 5;

        public float distanceTo(float x2, float y2)
        {
            return (float)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        }

        public int frame(int frame2)
        {
            return frame2 * 32 + frame2 + 1;
        }

        public void math()
        {
            angle2 = (angle * (float)Math.PI / 180);
            scale_x = (float)Math.Cos(angle2);
            scale_y = (float)Math.Sin(angle2);
            veclocity_x = (speed * scale_x);
            veclocity_y = (speed * scale_y);
        }

        public void mathAim(float speed2, float x2, float y2)
        {
            angle = (float)Math.Atan2(y2 - y, x2 - x);
            speed = speed2;
            veclocity_x = (speed * (float)Math.Cos(angle));
            veclocity_y = (speed * (float)Math.Sin(angle));
        }

        public void setCoords(float x2, float y2)
        {
            x = x2;
            y = y2;

            renderX = x2 + 1000;
            renderY = y2 + 1000;
        }

        public void setSpriteCoords(int imx2, int imy2)
        {
            imgx = imx2;
            imgy = imy2;
        }

        public void setSize(int w2, int h2)
        {
            width = w2;
            height = h2;
        }
        // något som ska ritas och inte påverkas av kamran
        public void drawSprite(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, new Vector2(x, y), new Rectangle(imgx, imgy, width, height), Color.White);
        }
        // om vi ska rita något som ska påverkas av kamran
        public void drawSpriteOffset(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, new Vector2(renderX, renderY), new Rectangle(imgx, imgy, width, height), Color.White);
        }
       
        public void applyOffset(Rectangle camera)
        {
            renderX = x - camera.X;
            renderY = y - camera.Y;
        }
    }
}
