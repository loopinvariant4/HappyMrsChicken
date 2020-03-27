using HappyMrsChicken.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    public class Sprite : ComponentBase, IRenderable
    {
        #region vars
        private Texture2D texture;
        private int width, height;
        private Rectangle source;
        Vector2 size;
        private Point textureOffset = Point.Zero;
        #endregion

        #region properties
        public float Scale { get; set; }
        public bool IsVisible { get; set; }
        public Vector2 Origin { get; set; }
        public string SpriteName { get; set; }
        public Vector2 Size => size;
        #endregion

        #region ctor
        public Sprite(int entityId, string spriteName, ContentManager cm, Point texOffset) : base(entityId)
        {
            textureOffset = texOffset;
            SpriteName = spriteName;
            loadTexture(spriteName, cm);
            Scale = 1f;
            Origin = new Vector2(0.5f, 0.5f);
            IsVisible = true;
        }
        #endregion

        #region methods
        /// <summary>
        /// Draw the sprite with a spritebatch. The expectation is for the spritebatch's begin to already be called. This is because we expect to draw a large number of sprites and it would be inefficient to expect each draw to call begin and end
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sb"></param>
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (IsVisible == false)
            {
                return;
            }
            var pos = EntityManager.Instance.GetComponent<Position>(EntityId);
            sb.Draw(texture, pos.XY, source, Color.White, 0f, Origin, Scale, SpriteEffects.None, 0f);
        }

        private void loadTexture(string spriteName, ContentManager cm)
        {
            texture = cm.Load<Texture2D>(spriteName);
            width = texture.Width;
            height = texture.Height;
            size = new Vector2(width, height);
            source = new Rectangle(textureOffset.X, textureOffset.Y, width, height);
        }
        #endregion
    }
}
