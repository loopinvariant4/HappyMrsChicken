using HappyMrsChicken.Systems;
using HappyMrsChicken.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken
{
    public class TileManager : ISystem, IRenderable
    {
        #region vars

        private List<List<Tile>> tiles = new List<List<Tile>>();
        private Perlin noise;
        private ContentManager contentMgr;

        Dictionary<char, string> tileMapper = new Dictionary<char, string>()
        {
            {'W', "water" },
            {'F', "fence" },
            {'G', "grass" }
        };
        #endregion

        #region ctor
        public TileManager()
        {
        }

        public TileManager(int seed, int octave, double persistence)
        {
            Seed = seed;
            Octave = octave;
            Persistance = persistence;
            noise = new Perlin(seed: Seed);
        }
        #endregion

        #region properties
        public int Seed { get; set; }
        public int Octave { get; set; }
        public double Persistance { get; set; }

        public int RowCount => tiles.Count;
        public int ColCount => tiles[0].Count;

        #endregion

        public void LoadFromFile(string filename)
        {
            string s = "";
            int row = 0, col = 0;
            using (StreamReader sr = new StreamReader(filename))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    List<Tile> tileRow = new List<Tile>();
                    col = 0;
                    foreach (char c in s)
                    {
                        tileRow.Add(new Tile(col++, row, c, GetTextureFromType(c)));
                    }
                    row++;
                    tiles.Add(tileRow);
                }
            }
        }

        #region private methods
        private void writeMapGen(BinaryWriter bw)
        {
            bw.Write(Seed);
            bw.Write(Octave);
            bw.Write(Persistance);
        }

        private void readMapGen(BinaryReader br)
        {
            Seed = br.ReadInt32();
            Octave = br.ReadInt32();
            Persistance = br.ReadDouble();
        }

        private Texture2D GetTextureFromType(char tileType)
        {
            return contentMgr.Load<Texture2D>(tileMapper[tileType]);
        }

        private Chunk readChunk(BinaryReader br)
        {
            var first = Tile.ReadFromStream(br);
            Debug.Assert(first.X % Chunk.SIZE == 0 && first.Y % Chunk.SIZE == 0, "Chunk's first tile must always be a multiple of the chunk length");
            var chunk = new Chunk(first.X / Chunk.SIZE, first.Y / Chunk.SIZE);
            chunk[0, 0] = first;
            for (int x = 0; x < Chunk.SIZE; x++)
            {
                for (int y = 0; y < Chunk.SIZE; y++)
                {
                    if (x == 0 && y == 0) continue;
                    var tile = Tile.ReadFromStream(br);
                    chunk[x, y] = tile;
                }
            }
            return chunk;
        }

        /// <summary>
        /// we iterate through all tiles row by row and write them to a file. 
        /// </summary>
        private void writeChunk(BinaryWriter bw, Chunk chunk)
        {
            for (int x = 0; x < Chunk.SIZE; x++)
            {
                for (int y = 0; y < Chunk.SIZE; y++)
                {
                    chunk[x, y].WriteToStream(bw);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            foreach (var row in tiles)
            {
                foreach (var colTile in row)
                {
                    colTile.Render(sb);
                }
            }
        }

        public void Init(Game game)
        {
            this.contentMgr = game.Content;
        }

        public List<Tile> GetTilesUnderArea(float startX, float startY, float endX, float endY)
        {
            int colStart = (int) startX / Tile.SIZE;
            int colEnd = Math.Min((int) endX / Tile.SIZE, ColCount - 1);
            int rowStart = (int) startY / Tile.SIZE;
            int rowEnd = Math.Min((int) endY / Tile.SIZE, RowCount - 1);

            List<Tile> area = new List<Tile>();
            for (int r = rowStart; r <= rowEnd; r++)
            {
                for (int c = colStart; c <= colEnd; c++)
                {
                    area.Add(tiles[r][c]);
                }
            }
            return area;
        }
            #endregion
        }
    }
