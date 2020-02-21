using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class GridLines
    {
        static BasicEffect effect;
        static int SPACING = Tile.SIZE;
        static Texture2D line;

        private static float mod(float a, float b)
        {
            var r = a % b;
            return (r < 0) ? r + b : r;
        }
        public static void DrawSpriteGrid(Vector2 screenStart, Viewport view, float scale, SpriteBatch sb)
        {
            Debug.Assert(line != null, "The texture line cannot be null. It is expected to be initialised before this class is used");
            var scaled = SPACING * scale;
            var startX = mod(screenStart.X, scaled);
            var startY = mod(screenStart.Y, scaled);

            var width = view.Width + scaled;
            var height = view.Height + scaled;


            while (startX < view.Width)
            {
                DrawLine(sb, new Vector2(startX, 0), height, MathHelper.PiOver2, new Color(0, 0, 0), 1f);
                //drawSpriteLine(startX, 0, 1, height, sb);
                startX += scaled;
            }
            while (startY < view.Height)
            {
                DrawLine(sb, new Vector2(0, startY), width, 0, new Color(0, 0, 0), 1f);
                startY += scaled;
            }
        }

        private static void drawSpriteLine(float x, float y, float width, float height, SpriteBatch sb)
        {
            sb.Draw(line, new Rectangle((int)x, (int)y, (int)width, (int)height), Color.Red);
        }

        public static void DrawLine(SpriteBatch sb, Vector2 point, float length, float angle, Color color, float thickness = 1.0f, float layerDepth = 0)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            sb.Draw(line, point, null, color, angle, origin, scale, SpriteEffects.None, layerDepth);
        }

        public static void DrawRectangle(SpriteBatch sb, Vector2 rect, int width, int height, Vector2 origin, Color color)
        {
            sb.Draw(line, new Vector2(rect.X - origin.X, rect.Y - origin.Y), null, color, 0, Vector2.Zero, new Vector2(width, 1f), SpriteEffects.None, 0);
            sb.Draw(line, new Vector2(rect.X - origin.X, rect.Y + height - origin.Y), null, color, 0, Vector2.Zero, new Vector2(width, 1f), SpriteEffects.None, 0);
            sb.Draw(line, new Vector2(rect.X - origin.X, rect.Y - origin.Y), null, color, MathHelper.PiOver2, Vector2.Zero, new Vector2(height, 1f), SpriteEffects.None, 0);
            sb.Draw(line, new Vector2(rect.X + width - origin.X, rect.Y - origin.Y), null, color, MathHelper.PiOver2, Vector2.Zero, new Vector2(height, 1f), SpriteEffects.None, 0);
        }

        public static void InitLine(GraphicsDevice device)
        {
            line = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            Int32[] pixel = { 0xFFFFFF };
            line.SetData<Int32>(pixel, 0, line.Width * line.Height);
        }
    }
}
