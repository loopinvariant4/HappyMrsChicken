using HappyMrsChicken.Components;
using HappyMrsChicken.Entities;
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

        // tiles refer to the absolute ground and paint the terrain
        private List<List<Tile>> tiles = new List<List<Tile>>();

        //terrain objects impede movement through them and can be placed anywhere on the tiles. Each object can be one or more tiles in size 
        private List<Entity> terrainObjects = new List<Entity>();

        private Perlin noise;
        private ContentManager contentMgr;

        Dictionary<char, string> tileMapper = new Dictionary<char, string>()
        {
            {'W', "terrain/water" },
            {'F', "terrain/fence" },
            {'G', "terrain/grass" }
        };

        Dictionary<char, Texture2D> textureMapper = new Dictionary<char, Texture2D>();
        #endregion

        #region ctor
        public TileManager(Dictionary<char, Texture2D> mapper)
        {
            textureMapper = mapper;
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

        #region public methods
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

        public void LoadEmptyGrid(int rows, int cols, Texture2D defaultTexture)
        {
            for (int r = 0; r < rows; r++)
            {
                tiles.Add(new List<Tile>());
                for (int c = 0; c < cols; c++)
                {
                    tiles[r].Add(new Tile(c, r, 'G', defaultTexture));
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

            foreach (var e in terrainObjects)
            {
                var s = EntityManager.Instance.GetComponent<Sprite>(e.Id);
                s.Draw(gameTime, sb);
            }
        }

        public void Init(Game game)
        {
            this.contentMgr = game.Content;
        }

        public List<Tile> GetTilesUnderArea(float startX, float startY, float endX, float endY)
        {
            int colStart = (int)startX / Tile.SIZE;
            int colEnd = Math.Min((int)endX / Tile.SIZE, ColCount - 1);
            int rowStart = (int)startY / Tile.SIZE;
            int rowEnd = Math.Min((int)endY / Tile.SIZE, RowCount - 1);

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

        public List<Entity> GetTerrainObjectsUnderArea(float startX, float startY, Vector2 size)
        {
            var rect = new System.Drawing.RectangleF(startX, startY, size.X, size.Y);
            List<Entity> items = new List<Entity>();
            foreach (var item in terrainObjects)
            {
                var pos = EntityManager.Instance.GetComponent<Position>(item.Id);
                if(pos.Rectangle.IntersectsWith(rect))
                {
                    items.Add(item);
                }

            }
            return items;
        }

        public void RemoveItem(Entity item)
        {
            terrainObjects.Remove(item);
        }

        public Entity GetObjectUnderPoint(int x, int y)
        {
            foreach(var item in terrainObjects)
            {
                var pos = EntityManager.Instance.GetComponent<Position>(item.Id);
                if(pos.Rectangle.Contains(x, y))
                {
                    return item;
                }
            }
            return null;
        }

        public Tile GetTileUnderPoint(int x, int y)
        {
            int col = x / Tile.SIZE;
            int row = y / Tile.SIZE;

            return tiles[row][col];
        }

        public void SetTile(int x, int y, Tile t)
        {
            int col = x / Tile.SIZE;
            int row = y / Tile.SIZE;

            tiles[row][col] = t;
        }

        public void SaveFileHMC(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (BinaryWriter bw = new BinaryWriter(new FileStream(filename, FileMode.CreateNew)))
            {
                //save terrain tiles
                bw.Write(RowCount);
                bw.Write(ColCount);
                foreach (var row in tiles)
                {
                    foreach (var col in row)
                    {
                        col.WriteToStream(bw);
                    }
                }

                //save trrrain objects
                bw.Write(terrainObjects.Count);
                foreach(var e in terrainObjects)
                {
                    //write terrain object name, xy position
                    var pos = EntityManager.Instance.GetComponent<Position>(e.Id);
                    var s = EntityManager.Instance.GetComponent<Sprite>(e.Id);
                    bw.Write(s.SpriteName);
                    bw.Write((int)pos.X);
                    bw.Write((int)pos.Y);
                }

                //save doodads
            }

        }

        public void ReadFileHMC(string filename)
        {
            using (BinaryReader br = new BinaryReader(new FileStream(filename, FileMode.Open)))
            {
                //read terrain
                var rows = br.ReadInt32();
                var cols = br.ReadInt32();
                tiles = new List<List<Tile>>(rows);
                for (int r = 0; r < rows; r++)
                {
                    var row = new List<Tile>(cols);
                    for (int i = 0; i < cols; i++)
                    {
                        var tile = Tile.ReadFromStream(br);
                        tile.Texture = textureMapper[tile.TileType];
                        row.Add(tile);
                    }
                    tiles.Add(row);
                }

                //read terrain objects
                var objectCount = br.ReadInt32();
                for(int i = 0; i < objectCount; i++)
                {
                    var assetName = br.ReadString();
                    var x = br.ReadInt32();
                    var y = br.ReadInt32();
                    AddTerrainObject(assetName, x, y);
                }
            }
        }

        public void AddTerrainObject(string assetName, int x, int y)
        {
            if (!Contains(assetName, x, y))
            {
                Entity e = new Entity();
                Sprite s = new Sprite(e.Id, assetName, contentMgr, Point.Zero);
                Position p = new Position(e.Id, s.Size);
                p.XY = new Vector2(x + s.Size.X / 2, y + s.Size.Y / 2);
                //p.XY = new Vector2(x, y);
                terrainObjects.Add(e);
                EntityManager.Instance.AddEntity(e);
                EntityManager.Instance.AddComponent<Sprite>(e.Id, s);
                EntityManager.Instance.AddComponent<Position>(e.Id, p);
            }
        }

        public bool Contains(string assetName, int x, int y)
        {
            return terrainObjects.Count(e =>
            {
                var p = EntityManager.Instance.GetComponent<Position>(e.Id);
                var s = EntityManager.Instance.GetComponent<Sprite>(e.Id);
                return s.SpriteName == assetName && p.X == x && p.Y == y;

            }) > 0;
        }

        #endregion

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

        
        #endregion
    }
}
