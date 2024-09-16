namespace Maze_Generator_and_solver
{
    partial class MazesMenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.maze2D_btn = new System.Windows.Forms.Button();
            this.mazeCircular_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.maze3Dsurface_btn = new System.Windows.Forms.Button();
            this.help_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // maze2D_btn
            // 
            this.maze2D_btn.BackColor = System.Drawing.Color.OrangeRed;
            this.maze2D_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.maze2D_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maze2D_btn.Location = new System.Drawing.Point(3, 195);
            this.maze2D_btn.Name = "maze2D_btn";
            this.maze2D_btn.Size = new System.Drawing.Size(750, 98);
            this.maze2D_btn.TabIndex = 0;
            this.maze2D_btn.Text = "2D Mazes";
            this.maze2D_btn.UseVisualStyleBackColor = false;
            this.maze2D_btn.Click += new System.EventHandler(this.maze2D_btn_Click);
            // 
            // mazeCircular_btn
            // 
            this.mazeCircular_btn.BackColor = System.Drawing.Color.Yellow;
            this.mazeCircular_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.mazeCircular_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mazeCircular_btn.Location = new System.Drawing.Point(3, 299);
            this.mazeCircular_btn.Name = "mazeCircular_btn";
            this.mazeCircular_btn.Size = new System.Drawing.Size(750, 98);
            this.mazeCircular_btn.TabIndex = 1;
            this.mazeCircular_btn.Text = "Circular Mazes";
            this.mazeCircular_btn.UseVisualStyleBackColor = false;
            this.mazeCircular_btn.Click += new System.EventHandler(this.mazeCircular_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(711, 95);
            this.label1.TabIndex = 2;
            this.label1.Text = "Maze Application";
            // 
            // maze3Dsurface_btn
            // 
            this.maze3Dsurface_btn.BackColor = System.Drawing.Color.Cyan;
            this.maze3Dsurface_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.maze3Dsurface_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maze3Dsurface_btn.Location = new System.Drawing.Point(3, 403);
            this.maze3Dsurface_btn.Name = "maze3Dsurface_btn";
            this.maze3Dsurface_btn.Size = new System.Drawing.Size(750, 98);
            this.maze3Dsurface_btn.TabIndex = 3;
            this.maze3Dsurface_btn.Text = "3D Surface Mazes";
            this.maze3Dsurface_btn.UseVisualStyleBackColor = false;
            this.maze3Dsurface_btn.Click += new System.EventHandler(this.maze3Dsurface_btn_Click);
            // 
            // help_btn
            // 
            this.help_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.help_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.help_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.help_btn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.help_btn.Location = new System.Drawing.Point(273, 507);
            this.help_btn.Name = "help_btn";
            this.help_btn.Size = new System.Drawing.Size(205, 58);
            this.help_btn.TabIndex = 4;
            this.help_btn.Text = "Help !";
            this.help_btn.UseVisualStyleBackColor = false;
            this.help_btn.Click += new System.EventHandler(this.help_btn_Click);
            // 
            // MazesMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(758, 672);
            this.Controls.Add(this.help_btn);
            this.Controls.Add(this.maze3Dsurface_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mazeCircular_btn);
            this.Controls.Add(this.maze2D_btn);
            this.MaximumSize = new System.Drawing.Size(774, 711);
            this.MinimumSize = new System.Drawing.Size(774, 711);
            this.Name = "MazesMenuForm";
            this.Text = "MAZES";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button maze2D_btn;
        private Button mazeCircular_btn;
        private Label label1;
        private Button maze3Dsurface_btn;
        private Button help_btn;
    }
}