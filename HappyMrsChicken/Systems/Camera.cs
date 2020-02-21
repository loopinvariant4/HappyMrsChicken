using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Systems
{
    public class Camera : ISystem
    {
        #region public properties
        public Vector2 Position;
        public Viewport ViewPort { get => viewPort; set => viewPort = value; }
        #endregion

        #region vars
        private Viewport viewPort;
        private float Rotation;
        public float Scale = 1.0f;
        #endregion

        public Camera(Vector2 startPosition)
        {
            Position = startPosition;
        }

        public void UpdatePosition(Vector2 pos)
        {
            Position = pos;
        }

        public void UpdatePosition(float deltaX, float deltaY)
        {
            Position.X += deltaX;
            Position.Y += deltaY;
        }

        public void UpdateRotation(float angle)
        {
            Rotation = angle;
        }

        public void Init(Game game)
        {
            viewPort = game.GraphicsDevice.Viewport;
        }

        public Matrix Transformation
        {
            get
            {
                return Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position.X, Position.Y, 0f);
            }
        }
    }
}
