namespace GameEditor
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblCol = new System.Windows.Forms.Label();
            this.lblRow = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.layoutPanelTerrain = new System.Windows.Forms.FlowLayoutPanel();
            this.layoutPanelTerrainObjects = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.Location = new System.Drawing.Point(24, 13);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(0, 13);
            this.lblX.TabIndex = 2;
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY.Location = new System.Drawing.Point(74, 13);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(0, 13);
            this.lblY.TabIndex = 3;
            // 
            // lblCol
            // 
            this.lblCol.AutoSize = true;
            this.lblCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCol.Location = new System.Drawing.Point(194, 13);
            this.lblCol.Name = "lblCol";
            this.lblCol.Size = new System.Drawing.Size(0, 13);
            this.lblCol.TabIndex = 7;
            // 
            // lblRow
            // 
            this.lblRow.AutoSize = true;
            this.lblRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRow.Location = new System.Drawing.Point(148, 13);
            this.lblRow.Name = "lblRow";
            this.lblRow.Size = new System.Drawing.Size(0, 13);
            this.lblRow.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Col";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Row";
            // 
            // layoutPanelTerrain
            // 
            this.layoutPanelTerrain.Location = new System.Drawing.Point(10, 40);
            this.layoutPanelTerrain.Name = "layoutPanelTerrain";
            this.layoutPanelTerrain.Size = new System.Drawing.Size(476, 29);
            this.layoutPanelTerrain.TabIndex = 8;
            // 
            // layoutPanelTerrainObjects
            // 
            this.layoutPanelTerrainObjects.AutoSize = true;
            this.layoutPanelTerrainObjects.ColumnCount = 5;
            this.layoutPanelTerrainObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.42966F));
            this.layoutPanelTerrainObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.57034F));
            this.layoutPanelTerrainObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.layoutPanelTerrainObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.layoutPanelTerrainObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.layoutPanelTerrainObjects.Location = new System.Drawing.Point(12, 86);
            this.layoutPanelTerrainObjects.Name = "layoutPanelTerrainObjects";
            this.layoutPanelTerrainObjects.RowCount = 1;
            this.layoutPanelTerrainObjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutPanelTerrainObjects.Size = new System.Drawing.Size(476, 37);
            this.layoutPanelTerrainObjects.TabIndex = 10;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 885);
            this.Controls.Add(this.layoutPanelTerrainObjects);
            this.Controls.Add(this.layoutPanelTerrain);
            this.Controls.Add(this.lblCol);
            this.Controls.Add(this.lblRow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InfoForm";
            this.Load += new System.EventHandler(this.InfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblCol;
        private System.Windows.Forms.Label lblRow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel layoutPanelTerrain;
        private System.Windows.Forms.TableLayoutPanel layoutPanelTerrainObjects;
    }
}