namespace FtitestUSB
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.fsdfsdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asdToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.etapasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dispositivoHardwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asdToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.arduinoPort = new System.IO.Ports.SerialPort(this.components);
            this.metroTileVisualizar = new MetroFramework.Controls.MetroTile();
            this.metroTilePruebas = new MetroFramework.Controls.MetroTile();
            this.metroTileAtletas = new MetroFramework.Controls.MetroTile();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroTileReportes = new MetroFramework.Controls.MetroTile();
            this.metroContextMenu1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.metroContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fsdfsdfToolStripMenuItem});
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.metroContextMenu1.ShowCheckMargin = true;
            this.metroContextMenu1.Size = new System.Drawing.Size(163, 34);
            // 
            // fsdfsdfToolStripMenuItem
            // 
            this.fsdfsdfToolStripMenuItem.Name = "fsdfsdfToolStripMenuItem";
            this.fsdfsdfToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.fsdfsdfToolStripMenuItem.Text = "fsdfsdf";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.acercaDeToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(20, 60);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1066, 33);
            this.menuStrip2.TabIndex = 9;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asdToolStripMenuItem,
            this.etapasToolStripMenuItem,
            this.dispositivoHardwareToolStripMenuItem,
            this.asdToolStripMenuItem1});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(84, 29);
            this.toolStripMenuItem1.Text = "Archivo";
            // 
            // asdToolStripMenuItem
            // 
            this.asdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asdToolStripMenuItem2});
            this.asdToolStripMenuItem.Name = "asdToolStripMenuItem";
            this.asdToolStripMenuItem.Size = new System.Drawing.Size(266, 30);
            this.asdToolStripMenuItem.Text = "Nomencladores";
            // 
            // asdToolStripMenuItem2
            // 
            this.asdToolStripMenuItem2.Name = "asdToolStripMenuItem2";
            this.asdToolStripMenuItem2.Size = new System.Drawing.Size(182, 30);
            this.asdToolStripMenuItem2.Text = "Modalidad";
            this.asdToolStripMenuItem2.Click += new System.EventHandler(this.AsdToolStripMenuItem2_Click);
            // 
            // etapasToolStripMenuItem
            // 
            this.etapasToolStripMenuItem.Name = "etapasToolStripMenuItem";
            this.etapasToolStripMenuItem.Size = new System.Drawing.Size(266, 30);
            this.etapasToolStripMenuItem.Text = "Período";
            this.etapasToolStripMenuItem.Visible = false;
            this.etapasToolStripMenuItem.Click += new System.EventHandler(this.EtapasToolStripMenuItem_Click);
            // 
            // dispositivoHardwareToolStripMenuItem
            // 
            this.dispositivoHardwareToolStripMenuItem.Name = "dispositivoHardwareToolStripMenuItem";
            this.dispositivoHardwareToolStripMenuItem.Size = new System.Drawing.Size(266, 30);
            this.dispositivoHardwareToolStripMenuItem.Text = "Dispositivo Hardware";
            this.dispositivoHardwareToolStripMenuItem.Click += new System.EventHandler(this.DispositivoHardwareToolStripMenuItem_Click);
            // 
            // asdToolStripMenuItem1
            // 
            this.asdToolStripMenuItem1.Name = "asdToolStripMenuItem1";
            this.asdToolStripMenuItem1.Size = new System.Drawing.Size(266, 30);
            this.asdToolStripMenuItem1.Text = "Importar Datos";
            this.asdToolStripMenuItem1.Click += new System.EventHandler(this.AsdToolStripMenuItem1_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(101, 29);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.AcercaDeToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(57, 29);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Centaur", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(292, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 108);
            this.label1.TabIndex = 10;
            this.label1.Text = "FatigTEST+";
            // 
            // metroTileVisualizar
            // 
            this.metroTileVisualizar.ActiveControl = null;
            this.metroTileVisualizar.Location = new System.Drawing.Point(541, 184);
            this.metroTileVisualizar.Name = "metroTileVisualizar";
            this.metroTileVisualizar.Size = new System.Drawing.Size(220, 174);
            this.metroTileVisualizar.TabIndex = 6;
            this.metroTileVisualizar.Text = "Visualizar Resultados";
            this.metroTileVisualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroTileVisualizar.TileImage = global::FtitestUSB.Properties.Resources.icons8_view_64;
            this.metroTileVisualizar.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTileVisualizar.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTileVisualizar.UseSelectable = true;
            this.metroTileVisualizar.UseTileImage = true;
            this.metroTileVisualizar.Click += new System.EventHandler(this.MetroTile1_Click);
            // 
            // metroTilePruebas
            // 
            this.metroTilePruebas.ActiveControl = null;
            this.metroTilePruebas.Location = new System.Drawing.Point(72, 184);
            this.metroTilePruebas.Name = "metroTilePruebas";
            this.metroTilePruebas.Size = new System.Drawing.Size(220, 174);
            this.metroTilePruebas.TabIndex = 5;
            this.metroTilePruebas.Text = "Realizar Pruebas";
            this.metroTilePruebas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroTilePruebas.TileImage = global::FtitestUSB.Properties.Resources.icons8_test_passed_64;
            this.metroTilePruebas.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTilePruebas.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTilePruebas.UseSelectable = true;
            this.metroTilePruebas.UseTileImage = true;
            this.metroTilePruebas.Click += new System.EventHandler(this.MetroTile4_Click);
            // 
            // metroTileAtletas
            // 
            this.metroTileAtletas.ActiveControl = null;
            this.metroTileAtletas.Location = new System.Drawing.Point(310, 184);
            this.metroTileAtletas.Name = "metroTileAtletas";
            this.metroTileAtletas.Size = new System.Drawing.Size(220, 174);
            this.metroTileAtletas.TabIndex = 7;
            this.metroTileAtletas.Text = "Gestionar Atleta";
            this.metroTileAtletas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroTileAtletas.TileImage = global::FtitestUSB.Properties.Resources.icons8_user_70;
            this.metroTileAtletas.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTileAtletas.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTileAtletas.UseSelectable = true;
            this.metroTileAtletas.UseTileImage = true;
            this.metroTileAtletas.Click += new System.EventHandler(this.MetroTile2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::FtitestUSB.Properties.Resources._62131763_blanco_azul_del_agua_fondo_degradado_ilustración_vector2;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1066, 394);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // metroTileReportes
            // 
            this.metroTileReportes.ActiveControl = null;
            this.metroTileReportes.Location = new System.Drawing.Point(796, 184);
            this.metroTileReportes.Name = "metroTileReportes";
            this.metroTileReportes.Size = new System.Drawing.Size(220, 174);
            this.metroTileReportes.TabIndex = 12;
            this.metroTileReportes.Text = "Exportar Resultados";
            this.metroTileReportes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroTileReportes.TileImage = global::FtitestUSB.Properties.Resources.icons8_microsoft_excel_64;
            this.metroTileReportes.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTileReportes.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.metroTileReportes.UseSelectable = true;
            this.metroTileReportes.UseTileImage = true;
            this.metroTileReportes.Click += new System.EventHandler(this.MetroTile3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FtitestUSB.Properties.Resources._62131763_blanco_azul_del_agua_fondo_degradado_ilustración_vector1;
            this.ClientSize = new System.Drawing.Size(1106, 474);
            this.Controls.Add(this.metroTileReportes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.metroTileVisualizar);
            this.Controls.Add(this.metroTilePruebas);
            this.Controls.Add(this.metroTileAtletas);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.metroContextMenu1.ResumeLayout(false);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem fsdfsdfToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem asdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asdToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem asdToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTile metroTilePruebas;
        private MetroFramework.Controls.MetroTile metroTileAtletas;
        private MetroFramework.Controls.MetroTile metroTileVisualizar;
        private System.IO.Ports.SerialPort arduinoPort;
        private System.Windows.Forms.ToolStripMenuItem etapasToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private MetroFramework.Controls.MetroTile metroTileReportes;
        private System.Windows.Forms.ToolStripMenuItem dispositivoHardwareToolStripMenuItem;
    }
}

