namespace Maze_Generator_and_solver
{
    partial class MazesCircularForm
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
            this.layers_lbl = new System.Windows.Forms.Label();
            this.GenerateMaze_btn = new System.Windows.Forms.Button();
            this.MazeLayers_count = new System.Windows.Forms.NumericUpDown();
            this.MazeWidth_lbl = new System.Windows.Forms.Label();
            this.MazeWidth_count = new System.Windows.Forms.NumericUpDown();
            this.SolveMaze_btn = new System.Windows.Forms.Button();
            this.MazeHeatmap_btn = new System.Windows.Forms.Button();
            this.SaveMaze_btn = new System.Windows.Forms.Button();
            this.Back_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MazeLayers_count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MazeWidth_count)).BeginInit();
            this.SuspendLayout();
            // 
            // layers_lbl
            // 
            this.layers_lbl.AutoSize = true;
            this.layers_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.layers_lbl.ForeColor = System.Drawing.Color.White;
            this.layers_lbl.Location = new System.Drawing.Point(12, 9);
            this.layers_lbl.Name = "layers_lbl";
            this.layers_lbl.Size = new System.Drawing.Size(58, 21);
            this.layers_lbl.TabIndex = 15;
            this.layers_lbl.Text = "Layers:";
            // 
            // GenerateMaze_btn
            // 
            this.GenerateMaze_btn.Location = new System.Drawing.Point(365, 9);
            this.GenerateMaze_btn.Name = "GenerateMaze_btn";
            this.GenerateMaze_btn.Size = new System.Drawing.Size(119, 26);
            this.GenerateMaze_btn.TabIndex = 16;
            this.GenerateMaze_btn.Text = "Generate Maze";
            this.GenerateMaze_btn.UseVisualStyleBackColor = true;
            this.GenerateMaze_btn.Click += new System.EventHandler(this.GenerateMaze_btn_Click);
            // 
            // MazeLayers_count
            // 
            this.MazeLayers_count.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MazeLayers_count.Location = new System.Drawing.Point(76, 9);
            this.MazeLayers_count.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.MazeLayers_count.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MazeLayers_count.Name = "MazeLayers_count";
            this.MazeLayers_count.Size = new System.Drawing.Size(49, 23);
            this.MazeLayers_count.TabIndex = 17;
            this.MazeLayers_count.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // MazeWidth_lbl
            // 
            this.MazeWidth_lbl.AutoSize = true;
            this.MazeWidth_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MazeWidth_lbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.MazeWidth_lbl.Location = new System.Drawing.Point(131, 9);
            this.MazeWidth_lbl.Name = "MazeWidth_lbl";
            this.MazeWidth_lbl.Size = new System.Drawing.Size(173, 21);
            this.MazeWidth_lbl.TabIndex = 18;
            this.MazeWidth_lbl.Text = "Cells on Circumference:";
            // 
            // MazeWidth_count
            // 
            this.MazeWidth_count.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MazeWidth_count.Location = new System.Drawing.Point(310, 9);
            this.MazeWidth_count.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.MazeWidth_count.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MazeWidth_count.Name = "MazeWidth_count";
            this.MazeWidth_count.Size = new System.Drawing.Size(49, 23);
            this.MazeWidth_count.TabIndex = 19;
            this.MazeWidth_count.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // SolveMaze_btn
            // 
            this.SolveMaze_btn.Location = new System.Drawing.Point(490, 9);
            this.SolveMaze_btn.Name = "SolveMaze_btn";
            this.SolveMaze_btn.Size = new System.Drawing.Size(119, 26);
            this.SolveMaze_btn.TabIndex = 20;
            this.SolveMaze_btn.Text = "Solve Maze";
            this.SolveMaze_btn.UseVisualStyleBackColor = true;
            this.SolveMaze_btn.Click += new System.EventHandler(this.SolveMaze_btn_Click);
            // 
            // MazeHeatmap_btn
            // 
            this.MazeHeatmap_btn.Location = new System.Drawing.Point(615, 9);
            this.MazeHeatmap_btn.Name = "MazeHeatmap_btn";
            this.MazeHeatmap_btn.Size = new System.Drawing.Size(119, 26);
            this.MazeHeatmap_btn.TabIndex = 21;
            this.MazeHeatmap_btn.Text = "Show Heatmap";
            this.MazeHeatmap_btn.UseVisualStyleBackColor = true;
            this.MazeHeatmap_btn.Click += new System.EventHandler(this.MazeHeatmap_btn_Click);
            // 
            // SaveMaze_btn
            // 
            this.SaveMaze_btn.Location = new System.Drawing.Point(740, 9);
            this.SaveMaze_btn.Name = "SaveMaze_btn";
            this.SaveMaze_btn.Size = new System.Drawing.Size(119, 26);
            this.SaveMaze_btn.TabIndex = 22;
            this.SaveMaze_btn.Text = "Save Maze to File";
            this.SaveMaze_btn.UseVisualStyleBackColor = true;
            this.SaveMaze_btn.Click += new System.EventHandler(this.SaveMaze_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(865, 9);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(119, 26);
            this.Back_btn.TabIndex = 23;
            this.Back_btn.Text = "Go Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // MazesCircularForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1032, 674);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.SaveMaze_btn);
            this.Controls.Add(this.MazeHeatmap_btn);
            this.Controls.Add(this.SolveMaze_btn);
            this.Controls.Add(this.MazeWidth_count);
            this.Controls.Add(this.MazeWidth_lbl);
            this.Controls.Add(this.MazeLayers_count);
            this.Controls.Add(this.GenerateMaze_btn);
            this.Controls.Add(this.layers_lbl);
            this.Name = "MazesCircularForm";
            this.Text = "Circular Mazes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MazesCircularForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MazesCircularForm_Paint);
            this.Resize += new System.EventHandler(this.MazesCircularForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.MazeLayers_count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MazeWidth_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label layers_lbl;
        private Button GenerateMaze_btn;
        private NumericUpDown MazeLayers_count;
        private Label MazeWidth_lbl;
        private NumericUpDown MazeWidth_count;
        private Button SolveMaze_btn;
        private Button MazeHeatmap_btn;
        private Button SaveMaze_btn;
        private Button Back_btn;
    }
}