using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class Position : ComponentBase
    {
        private Vector2 xy;
        private Vector2 size;
        RectangleF rect;

        public float X
        {
            get => xy.X;
            set
            {
                xy.X = value;
                rect.X = value;
            }
        }
        public float Y
        {
            get => xy.Y;
            set
            {
                xy.Y = value;
                rect.Y = value;
            }
        }

        public Vector2 XY
        {
            get => xy;
            set
            {
                xy = value;
                rect.X = value.X;
                rect.Y = value.Y;
            }
        }

        public RectangleF Rectangle => rect;
        public Vector2 Size => size;

        public Position(int entityId, float x, float y, Vector2 size) : this(entityId, new Vector2(x, y), size)
        {
        }

        public Position(int entityId, Vector2 pos, Vector2 size) : base(entityId)
        {
            xy = pos;
            this.size = size;
            rect = new RectangleF(X, Y, size.X, size.X);
        }

        public Position(int entityId, Vector2 size) : this(entityId, Vector2.Zero, size)
        {
        }
    }
}
