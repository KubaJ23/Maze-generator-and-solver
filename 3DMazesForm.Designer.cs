namespace Maze_Generator_and_solver
{
    partial class Maze3DForm
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
            this.MazeLength_count = new System.Windows.Forms.NumericUpDown();
            this.MazeLength_lbl = new System.Windows.Forms.Label();
            this.GenerateMaze_btn = new System.Windows.Forms.Button();
            this.SolveMaze_btn = new System.Windows.Forms.Button();
            this.Back_btn = new System.Windows.Forms.Button();
            this.SaveMaze_btn = new System.Windows.Forms.Button();
            this.mazePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MazeLength_count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MazeLength_count
            // 
            this.MazeLength_count.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MazeLength_count.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MazeLength_count.Location = new System.Drawing.Point(113, 7);
            this.MazeLength_count.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MazeLength_count.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MazeLength_count.Name = "MazeLength_count";
            this.MazeLength_count.Size = new System.Drawing.Size(60, 29);
            this.MazeLength_count.TabIndex = 11;
            this.MazeLength_count.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // MazeLength_lbl
            // 
            this.MazeLength_lbl.AutoSize = true;
            this.MazeLength_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MazeLength_lbl.Location = new System.Drawing.Point(12, 9);
            this.MazeLength_lbl.Name = "MazeLength_lbl";
            this.MazeLength_lbl.Size = new System.Drawing.Size(95, 21);
            this.MazeLength_lbl.TabIndex = 12;
            this.MazeLength_lbl.Text = "Side Length:";
            // 
            // GenerateMaze_btn
            // 
            this.GenerateMaze_btn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GenerateMaze_btn.Location = new System.Drawing.Point(179, 7);
            this.GenerateMaze_btn.Name = "GenerateMaze_btn";
            this.GenerateMaze_btn.Size = new System.Drawing.Size(110, 29);
            this.GenerateMaze_btn.TabIndex = 13;
            this.GenerateMaze_btn.Text = "Generate Maze";
            this.GenerateMaze_btn.UseVisualStyleBackColor = true;
            this.GenerateMaze_btn.Click += new System.EventHandler(this.GenerateMaze_btn_Click);
            // 
            // SolveMaze_btn
            // 
            this.SolveMaze_btn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SolveMaze_btn.Location = new System.Drawing.Point(295, 7);
            this.SolveMaze_btn.Name = "SolveMaze_btn";
            this.SolveMaze_btn.Size = new System.Drawing.Size(110, 29);
            this.SolveMaze_btn.TabIndex = 14;
            this.SolveMaze_btn.Text = "Show Solution";
            this.SolveMaze_btn.UseVisualStyleBackColor = true;
            this.SolveMaze_btn.Click += new System.EventHandler(this.SolveMaze_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Back_btn.Location = new System.Drawing.Point(527, 7);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(110, 29);
            this.Back_btn.TabIndex = 16;
            this.Back_btn.Text = "Go Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // SaveMaze_btn
            // 
            this.SaveMaze_btn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveMaze_btn.Location = new System.Drawing.Point(411, 7);
            this.SaveMaze_btn.Name = "SaveMaze_btn";
            this.SaveMaze_btn.Size = new System.Drawing.Size(110, 29);
            this.SaveMaze_btn.TabIndex = 15;
            this.SaveMaze_btn.Text = "Save Maze";
            this.SaveMaze_btn.UseVisualStyleBackColor = true;
            this.SaveMaze_btn.Click += new System.EventHandler(this.SaveMaze_btn_Click);
            // 
            // mazePictureBox
            // 
            this.mazePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mazePictureBox.Location = new System.Drawing.Point(2, 39);
            this.mazePictureBox.Margin = new System.Windows.Forms.Padding(99);
            this.mazePictureBox.Name = "mazePictureBox";
            this.mazePictureBox.Size = new System.Drawing.Size(979, 621);
            this.mazePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mazePictureBox.TabIndex = 17;
            this.mazePictureBox.TabStop = false;
            // 
            // Maze3DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.mazePictureBox);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.SaveMaze_btn);
            this.Controls.Add(this.SolveMaze_btn);
            this.Controls.Add(this.GenerateMaze_btn);
            this.Controls.Add(this.MazeLength_lbl);
            this.Controls.Add(this.MazeLength_count);
            this.KeyPreview = true;
            this.Name = "Maze3DForm";
            this.Text = "3D mazes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Maze3DForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Maze3DForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MazeLength_count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown MazeLength_count;
        private Label MazeLength_lbl;
        private Button GenerateMaze_btn;
        private Button SolveMaze_btn;
        private Button Back_btn;
        private Button SaveMaze_btn;
        private PictureBox pictureBox1;
        private PictureBox mazePictureBox;
    }
}