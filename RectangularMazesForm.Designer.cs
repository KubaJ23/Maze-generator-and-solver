namespace Maze_Generator_and_solver
{
    partial class Mazes2DForm
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
            this.GenerateMaze_btn = new System.Windows.Forms.Button();
            this.Back_btn = new System.Windows.Forms.Button();
            this.SolveMaze_btn = new System.Windows.Forms.Button();
            this.MazeWidth_count = new System.Windows.Forms.NumericUpDown();
            this.MazeHeight_count = new System.Windows.Forms.NumericUpDown();
            this.mazeGenerationDelay_count = new System.Windows.Forms.NumericUpDown();
            this.pathType_combobox = new System.Windows.Forms.ComboBox();
            this.pathType_lbl = new System.Windows.Forms.Label();
            this.mazegeneration_lbl = new System.Windows.Forms.Label();
            this.mazeGeneration_combobox = new System.Windows.Forms.ComboBox();
            this.width_lbl = new System.Windows.Forms.Label();
            this.sleeptime_lbl = new System.Windows.Forms.Label();
            this.height_lbl = new System.Windows.Forms.Label();
            this.mazesolvemethod_lbl = new System.Windows.Forms.Label();
            this.mazesolvemethod_combobox = new System.Windows.Forms.ComboBox();
            this.generationtime_lbl = new System.Windows.Forms.Label();
            this.solvetime_lbl = new System.Windows.Forms.Label();
            this.SaveMaze_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MazeWidth_count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MazeHeight_count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazeGenerationDelay_count)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerateMaze_btn
            // 
            this.GenerateMaze_btn.Location = new System.Drawing.Point(876, 2);
            this.GenerateMaze_btn.Name = "GenerateMaze_btn";
            this.GenerateMaze_btn.Size = new System.Drawing.Size(64, 49);
            this.GenerateMaze_btn.TabIndex = 4;
            this.GenerateMaze_btn.Text = "Generate Maze";
            this.GenerateMaze_btn.UseVisualStyleBackColor = true;
            this.GenerateMaze_btn.Click += new System.EventHandler(this.GenerateMaze_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.Location = new System.Drawing.Point(946, 2);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(85, 22);
            this.Back_btn.TabIndex = 5;
            this.Back_btn.Text = "Go Back";
            this.Back_btn.UseVisualStyleBackColor = true;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // SolveMaze_btn
            // 
            this.SolveMaze_btn.Location = new System.Drawing.Point(815, 2);
            this.SolveMaze_btn.Name = "SolveMaze_btn";
            this.SolveMaze_btn.Size = new System.Drawing.Size(55, 49);
            this.SolveMaze_btn.TabIndex = 6;
            this.SolveMaze_btn.Text = "Solve Maze";
            this.SolveMaze_btn.UseVisualStyleBackColor = true;
            this.SolveMaze_btn.Click += new System.EventHandler(this.SolveMaze_btn_Click);
            // 
            // MazeWidth_count
            // 
            this.MazeWidth_count.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MazeWidth_count.Location = new System.Drawing.Point(35, 28);
            this.MazeWidth_count.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.MazeWidth_count.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MazeWidth_count.Name = "MazeWidth_count";
            this.MazeWidth_count.Size = new System.Drawing.Size(49, 23);
            this.MazeWidth_count.TabIndex = 9;
            this.MazeWidth_count.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // MazeHeight_count
            // 
            this.MazeHeight_count.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MazeHeight_count.Location = new System.Drawing.Point(120, 28);
            this.MazeHeight_count.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.MazeHeight_count.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MazeHeight_count.Name = "MazeHeight_count";
            this.MazeHeight_count.Size = new System.Drawing.Size(49, 23);
            this.MazeHeight_count.TabIndex = 10;
            this.MazeHeight_count.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // mazeGenerationDelay_count
            // 
            this.mazeGenerationDelay_count.Location = new System.Drawing.Point(267, 28);
            this.mazeGenerationDelay_count.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.mazeGenerationDelay_count.Name = "mazeGenerationDelay_count";
            this.mazeGenerationDelay_count.Size = new System.Drawing.Size(60, 23);
            this.mazeGenerationDelay_count.TabIndex = 12;
            // 
            // pathType_combobox
            // 
            this.pathType_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pathType_combobox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pathType_combobox.FormattingEnabled = true;
            this.pathType_combobox.Items.AddRange(new object[] {
            "West - East",
            "North - South",
            "Centre - Edge",
            "Random - Random",
            "Max Difficulty"});
            this.pathType_combobox.Location = new System.Drawing.Point(90, 2);
            this.pathType_combobox.Name = "pathType_combobox";
            this.pathType_combobox.Size = new System.Drawing.Size(153, 22);
            this.pathType_combobox.TabIndex = 13;
            // 
            // pathType_lbl
            // 
            this.pathType_lbl.AutoSize = true;
            this.pathType_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pathType_lbl.ForeColor = System.Drawing.Color.White;
            this.pathType_lbl.Location = new System.Drawing.Point(1, -1);
            this.pathType_lbl.Name = "pathType_lbl";
            this.pathType_lbl.Size = new System.Drawing.Size(83, 21);
            this.pathType_lbl.TabIndex = 14;
            this.pathType_lbl.Text = "Path Type: ";
            // 
            // mazegeneration_lbl
            // 
            this.mazegeneration_lbl.AutoSize = true;
            this.mazegeneration_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mazegeneration_lbl.ForeColor = System.Drawing.Color.White;
            this.mazegeneration_lbl.Location = new System.Drawing.Point(251, 0);
            this.mazegeneration_lbl.Name = "mazegeneration_lbl";
            this.mazegeneration_lbl.Size = new System.Drawing.Size(189, 21);
            this.mazegeneration_lbl.TabIndex = 15;
            this.mazegeneration_lbl.Text = "Maze Generation Method:";
            // 
            // mazeGeneration_combobox
            // 
            this.mazeGeneration_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mazeGeneration_combobox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mazeGeneration_combobox.FormattingEnabled = true;
            this.mazeGeneration_combobox.Items.AddRange(new object[] {
            "Recursive Backtracking",
            "Kruskal\'s Algorithm",
            "Prim\'s Algorithm",
            "Eller\'s Algorithm"});
            this.mazeGeneration_combobox.Location = new System.Drawing.Point(446, 2);
            this.mazeGeneration_combobox.Name = "mazeGeneration_combobox";
            this.mazeGeneration_combobox.Size = new System.Drawing.Size(182, 22);
            this.mazeGeneration_combobox.TabIndex = 16;
            // 
            // width_lbl
            // 
            this.width_lbl.AutoSize = true;
            this.width_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.width_lbl.ForeColor = System.Drawing.Color.White;
            this.width_lbl.Location = new System.Drawing.Point(1, 25);
            this.width_lbl.Name = "width_lbl";
            this.width_lbl.Size = new System.Drawing.Size(28, 21);
            this.width_lbl.TabIndex = 17;
            this.width_lbl.Text = "W:";
            // 
            // sleeptime_lbl
            // 
            this.sleeptime_lbl.AutoSize = true;
            this.sleeptime_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sleeptime_lbl.ForeColor = System.Drawing.Color.White;
            this.sleeptime_lbl.Location = new System.Drawing.Point(175, 25);
            this.sleeptime_lbl.Name = "sleeptime_lbl";
            this.sleeptime_lbl.Size = new System.Drawing.Size(86, 21);
            this.sleeptime_lbl.TabIndex = 21;
            this.sleeptime_lbl.Text = "Sleep time:";
            // 
            // height_lbl
            // 
            this.height_lbl.AutoSize = true;
            this.height_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.height_lbl.ForeColor = System.Drawing.Color.White;
            this.height_lbl.Location = new System.Drawing.Point(90, 25);
            this.height_lbl.Name = "height_lbl";
            this.height_lbl.Size = new System.Drawing.Size(24, 21);
            this.height_lbl.TabIndex = 23;
            this.height_lbl.Text = "H:";
            // 
            // mazesolvemethod_lbl
            // 
            this.mazesolvemethod_lbl.AutoSize = true;
            this.mazesolvemethod_lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mazesolvemethod_lbl.ForeColor = System.Drawing.Color.White;
            this.mazesolvemethod_lbl.Location = new System.Drawing.Point(333, 25);
            this.mazesolvemethod_lbl.Name = "mazesolvemethod_lbl";
            this.mazesolvemethod_lbl.Size = new System.Drawing.Size(150, 21);
            this.mazesolvemethod_lbl.TabIndex = 24;
            this.mazesolvemethod_lbl.Text = "Maze Solve method:";
            // 
            // mazesolvemethod_combobox
            // 
            this.mazesolvemethod_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mazesolvemethod_combobox.FormattingEnabled = true;
            this.mazesolvemethod_combobox.Items.AddRange(new object[] {
            "Depth-first search",
            "Dijkstra\'s algorithm path",
            "Dijkstra\'s algorithm heatmap"});
            this.mazesolvemethod_combobox.Location = new System.Drawing.Point(489, 28);
            this.mazesolvemethod_combobox.Name = "mazesolvemethod_combobox";
            this.mazesolvemethod_combobox.Size = new System.Drawing.Size(139, 23);
            this.mazesolvemethod_combobox.TabIndex = 25;
            // 
            // generationtime_lbl
            // 
            this.generationtime_lbl.AutoSize = true;
            this.generationtime_lbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.generationtime_lbl.ForeColor = System.Drawing.Color.White;
            this.generationtime_lbl.Location = new System.Drawing.Point(634, 3);
            this.generationtime_lbl.Name = "generationtime_lbl";
            this.generationtime_lbl.Size = new System.Drawing.Size(100, 13);
            this.generationtime_lbl.TabIndex = 26;
            this.generationtime_lbl.Text = "Time to generate: ";
            // 
            // solvetime_lbl
            // 
            this.solvetime_lbl.AutoSize = true;
            this.solvetime_lbl.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.solvetime_lbl.ForeColor = System.Drawing.Color.White;
            this.solvetime_lbl.Location = new System.Drawing.Point(634, 30);
            this.solvetime_lbl.Name = "solvetime_lbl";
            this.solvetime_lbl.Size = new System.Drawing.Size(80, 13);
            this.solvetime_lbl.TabIndex = 27;
            this.solvetime_lbl.Text = "Time to solve: ";
            // 
            // SaveMaze_btn
            // 
            this.SaveMaze_btn.Location = new System.Drawing.Point(946, 25);
            this.SaveMaze_btn.Name = "SaveMaze_btn";
            this.SaveMaze_btn.Size = new System.Drawing.Size(85, 26);
            this.SaveMaze_btn.TabIndex = 28;
            this.SaveMaze_btn.Text = "Save Maze";
            this.SaveMaze_btn.UseVisualStyleBackColor = true;
            this.SaveMaze_btn.Click += new System.EventHandler(this.SaveMaze_btn_Click);
            // 
            // Mazes2DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1032, 674);
            this.Controls.Add(this.SaveMaze_btn);
            this.Controls.Add(this.solvetime_lbl);
            this.Controls.Add(this.generationtime_lbl);
            this.Controls.Add(this.mazesolvemethod_combobox);
            this.Controls.Add(this.mazesolvemethod_lbl);
            this.Controls.Add(this.height_lbl);
            this.Controls.Add(this.sleeptime_lbl);
            this.Controls.Add(this.width_lbl);
            this.Controls.Add(this.mazeGeneration_combobox);
            this.Controls.Add(this.mazegeneration_lbl);
            this.Controls.Add(this.pathType_lbl);
            this.Controls.Add(this.pathType_combobox);
            this.Controls.Add(this.mazeGenerationDelay_count);
            this.Controls.Add(this.MazeHeight_count);
            this.Controls.Add(this.MazeWidth_count);
            this.Controls.Add(this.SolveMaze_btn);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.GenerateMaze_btn);
            this.KeyPreview = true;
            this.Name = "Mazes2DForm";
            this.Text = "2D Mazes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Mazes2DForm_Load);
            this.SizeChanged += new System.EventHandler(this.Mazes2DForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Mazes2DForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Mazes2DForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MazeWidth_count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MazeHeight_count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazeGenerationDelay_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button GenerateMaze_btn;
        private Button Back_btn;
        private Button SolveMaze_btn;
        private NumericUpDown MazeWidth_count;
        private NumericUpDown MazeHeight_count;
        private NumericUpDown mazeGenerationDelay_count;
        private ComboBox pathType_combobox;
        private Label pathType_lbl;
        private Label mazegeneration_lbl;
        private ComboBox mazeGeneration_combobox;
        private Label width_lbl;
        private Label sleeptime_lbl;
        private Label height_lbl;
        private Label mazesolvemethod_lbl;
        private ComboBox mazesolvemethod_combobox;
        private Label generationtime_lbl;
        private Label solvetime_lbl;
        private Button SaveMaze_btn;
    }
}