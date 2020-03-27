using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken
{
    [Serializable]
    public class Tile
    {
        #region vars
        public const int SIZE = 32;

        public Texture2D Texture { get; set; }

        #endregion

        #region properties 
        public int X { get; set; }
        public int Y { get; set; }
        public char TileType { get; set; } = 'B';
        public bool IsOccupied { get; set; }

        public bool HasFogOfWar { get; set; }

        public bool IsPassable
        {
            get
            {
                return IsOccupied == false && TileType != 'W' && TileType != 'F';
            }
        }
        #endregion

        public Tile()
        {

        }
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Tile(int x, int y, char tileType, Texture2D texture) : this(x, y)
        {
            this.TileType = tileType;
            Texture = texture;
        }

        public void WriteToStream(BinaryWriter bw)
        {
            bw.Write(X);
            bw.Write(Y);
            bw.Write(TileType);
            bw.Write(HasFogOfWar);
            bw.Write(IsOccupied);
        }

        public static Tile ReadFromStream(BinaryReader br)
        {
            //TODO: Ensure all exceptions are handled up the call stack 
            Tile t = new Tile();
            t.X = br.ReadInt32();
            t.Y = br.ReadInt32();
            t.TileType = br.ReadChar();
            t.HasFogOfWar = br.ReadBoolean();
            t.IsOccupied = br.ReadBoolean();
            return t;
        }


        public void Render(SpriteBatch sb)
        {
            sb.Draw(Texture, new Vector2(X * SIZE, Y * SIZE), Color.White);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}, {2}", X, Y, TileType);
        }
    }
}
