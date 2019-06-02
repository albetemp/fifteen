namespace cs_Fifteen.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imgCells = new System.Windows.Forms.ImageList(this.components);
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.itmNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.itmShuffleCells = new System.Windows.Forms.ToolStripMenuItem();
            this.itmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pbxBoard = new System.Windows.Forms.PictureBox();
            this.mnuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // imgCells
            // 
            this.imgCells.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgCells.ImageStream")));
            this.imgCells.TransparentColor = System.Drawing.Color.Transparent;
            this.imgCells.Images.SetKeyName(0, "Cell_01.jpg");
            this.imgCells.Images.SetKeyName(1, "Cell_02.jpg");
            this.imgCells.Images.SetKeyName(2, "Cell_03.jpg");
            this.imgCells.Images.SetKeyName(3, "Cell_04.jpg");
            this.imgCells.Images.SetKeyName(4, "Cell_05.jpg");
            this.imgCells.Images.SetKeyName(5, "Cell_06.jpg");
            this.imgCells.Images.SetKeyName(6, "Cell_07.jpg");
            this.imgCells.Images.SetKeyName(7, "Cell_08.jpg");
            this.imgCells.Images.SetKeyName(8, "Cell_09.jpg");
            this.imgCells.Images.SetKeyName(9, "Cell_10.jpg");
            this.imgCells.Images.SetKeyName(10, "Cell_11.jpg");
            this.imgCells.Images.SetKeyName(11, "Cell_12.jpg");
            this.imgCells.Images.SetKeyName(12, "Cell_13.jpg");
            this.imgCells.Images.SetKeyName(13, "Cell_14.jpg");
            this.imgCells.Images.SetKeyName(14, "Cell_15.jpg");
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmNewGame,
            this.itmShuffleCells,
            this.itmAbout});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mnuMain.Size = new System.Drawing.Size(506, 28);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "menuStrip1";
            // 
            // itmNewGame
            // 
            this.itmNewGame.Name = "itmNewGame";
            this.itmNewGame.Size = new System.Drawing.Size(94, 24);
            this.itmNewGame.Text = "New Game";
            this.itmNewGame.Click += new System.EventHandler(this.ItmNewGame_Click);
            // 
            // itmShuffleCells
            // 
            this.itmShuffleCells.Name = "itmShuffleCells";
            this.itmShuffleCells.Size = new System.Drawing.Size(67, 24);
            this.itmShuffleCells.Text = "Shuffle";
            this.itmShuffleCells.Click += new System.EventHandler(this.itmShuffleCells_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new System.Drawing.Size(62, 24);
            this.itmAbout.Text = "About";
            this.itmAbout.Click += new System.EventHandler(this.itmAbout_Click);
            // 
            // pbxBoard
            // 
            this.pbxBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxBoard.Location = new System.Drawing.Point(9, 32);
            this.pbxBoard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbxBoard.Name = "pbxBoard";
            this.pbxBoard.Size = new System.Drawing.Size(486, 430);
            this.pbxBoard.TabIndex = 1;
            this.pbxBoard.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 473);
            this.Controls.Add(this.pbxBoard);
            this.Controls.Add(this.mnuMain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Fifteen - Gesture camera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.FrmMail_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgCells;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem itmNewGame;
        private System.Windows.Forms.ToolStripMenuItem itmAbout;
        private System.Windows.Forms.PictureBox pbxBoard;
        private System.Windows.Forms.ToolStripMenuItem itmShuffleCells;

        public delegate void ClearBoardDelegate();
    }
}

