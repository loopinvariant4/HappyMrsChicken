using HappyMrsChicken;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEditor
{
    public partial class InfoForm : Form
    {
        GameEditor wndEditor;
        Dictionary<string, char> terrainMapper = new Dictionary<string, char>()
        {
            {"grass", 'G' },
            {"water", 'W' },
            {"transparent", 'B'}
        };
        public InfoForm(GameEditor game)
        {
            wndEditor = game;
            wndEditor.Info += WndEditor_Info;
            InitializeComponent();
        }

        private void WndEditor_Info(object sender, InfoEventArgs e)
        {
            switch (e.Key)
            {
                case "mouse":
                    updateMouseCoordinates(e);
                    break;
            }
        }

        private void updateMouseCoordinates(InfoEventArgs e)
        {
            lblX.Text = e.Values[0].ToString();
            lblY.Text = e.Values[1].ToString();
            lblRow.Text = (Int32.Parse(e.Values[1].ToString()) / Tile.SIZE).ToString();
            lblCol.Text = (Int32.Parse(e.Values[0].ToString()) / Tile.SIZE).ToString();

        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            loadTerrainTextures();
            loadTerrainObjectTextures();
        }

        private void loadTerrainObjectTextures()
        {
            var files = Directory.GetFiles(@".\Content\terrain_objects\", "*.xnb");
            int colSize = 0;
            //layoutPanelTerrainObjects.ColumnCount;
            int fileIdx = 0;
            for (int i = 0; i < files.Length; i++)
            {
                Button b = new Button();
                b.Text = files[i].Substring(files[i].LastIndexOf("\\") + 1, files[i].LastIndexOf(".") - ((files[i].LastIndexOf("\\") + 1)));
                b.Click += (o, e) =>
                {
                    wndEditor.ActiveSprite = ActiveSpriteObject.TerrainObject;
                    wndEditor.SelectedTerrainObject = b.Text;
                };

                layoutPanelTerrainObjects.Controls.Add(b);
            }
        }

        private void loadTerrainTextures()
        {
            var files = Directory.GetFiles(@".\Content\terrain\", "*.xnb");
            foreach (var file in files)
            {
                Button b = new Button();
                b.Text = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - ((file.LastIndexOf("\\") + 1)));
                b.Click += (o, e) =>
                {
                    wndEditor.ActiveSprite = ActiveSpriteObject.Terrain;
                    wndEditor.SelectedTerrain = terrainMapper[b.Text].ToString();
                };
                layoutPanelTerrain.Controls.Add(b);
            }
        }
    }
}
