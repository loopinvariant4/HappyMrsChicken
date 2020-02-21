using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken
{
    public class Chunk
    {
        #region vars
        public const int SIZE = 32;
        private Tile[,] tiles = new Tile[SIZE, SIZE];
        readonly int sx, sy, ex, ey;
        readonly int x, y;
        #endregion


        #region ctor
        public Chunk(int x, int y)
        {
            this.x = x;
            this.y = y;
            sx = x * 32;
            sy = y * 32;
            ex = sx  + SIZE;
            ey = sy + SIZE;
            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    tiles[i, j] = new Tile(x + i, y + j);
                }
            }
            
        }
        #endregion

        #region methods
        public bool Contains(Tile t)
        {
            return t.X >= sx && t.X <= ex && t.Y >= sy && t.Y <= ey;
        }
        #endregion

        #region properties
        public Tile this[int x, int y]
        {
            get
            {
                return tiles[x, y];
            }
            set
            {
                tiles[x, y] = value;
            }
        }

        public int X { get => x; }
        public int Y { get => y; }

        public int TopLeftTileX { get => sx; }
        public int TopLeftTileY { get => sy; }
        #endregion
    }
}
