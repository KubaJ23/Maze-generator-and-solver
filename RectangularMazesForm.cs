using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Maze_Generator_and_solver
{
    public partial class Mazes2DForm : Form
    {
        RectangularMaze maze;
        Graphics g;
        Thread mazeGenerationThread;

        Pen wallPen;
        int yOffSet = 55;// amount of pixels that the maze grid is offset from the top left corner 150,10
        int xOffSet = 3;

        bool maskMaze;
        bool showheatmap;

        Graphics saveImageGFX;
        Bitmap MazeImage;   

        StringFormat format = new StringFormat();

        public Mazes2DForm()
        {
            InitializeComponent();
        }

        private void Mazes2DForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            mazeGenerationThread = new Thread(() => GenerateMaze(pathType_combobox.Text, mazeGeneration_combobox.Text));
            wallPen = new Pen(Brushes.Black, 2f);
            maskMaze = false;
            showheatmap= false;

            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            MazeImage = new Bitmap(this.Bounds.Width, this.Bounds.Height, PixelFormat.Format32bppArgb);
            saveImageGFX = Graphics.FromImage(MazeImage);

            pathType_combobox.Text = "North - South";
            mazeGeneration_combobox.Text = "Eller's Algorithm";
            mazesolvemethod_combobox.Text = "Depth-first search";
            GenerateMaze("North - South", "Eller's Algorithm");
        }
        private void UpdateMazeGraphics()
        {
            Invalidate(new Rectangle(xOffSet, yOffSet, this.ClientSize.Width - 2 * xOffSet, this.ClientSize.Height - yOffSet - 10));
        }
        private void Mazes2DForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            float cellWallLength = (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0);
            g.FillRectangle(Brushes.WhiteSmoke, xOffSet, yOffSet, cellWallLength * maze.grid.GetLength(1), cellWallLength * maze.grid.GetLength(0));

            // makes the grid squares orange which are specified by the highlightIndexes list of indexes
            DrawSolution(g, cellWallLength);

            if (maskMaze)
            {
                MaskMaze(maze.player, 2, cellWallLength);
            }
            if (showheatmap)
            {
                DisplayHeatMap(maze.heatmap);
                showheatmap = false;
            }
            DrawMazeGrid(g,cellWallLength);
            g.FillEllipse(Brushes.Aqua, maze.player.x * cellWallLength + xOffSet + (cellWallLength / 6), maze.player.y * cellWallLength + yOffSet + (cellWallLength / 6), cellWallLength * 2 / 3, cellWallLength * 2 / 3);
        }
        private void DrawSolution(Graphics g, float cellWallLength)
        {
            lock (maze.highlightIndexes)
            {
                try
                {
                    foreach (Index pos in maze.highlightIndexes)
                    {
                        g.FillRectangle(Brushes.Orange, pos.x * cellWallLength + xOffSet, pos.y * cellWallLength + yOffSet, cellWallLength, cellWallLength);
                    }
                }
                catch { }
            }
        }
        private void DrawStartAndEnd (Graphics g,float cellWallLength)
        {
            g.FillRectangle(Brushes.Gold, maze.start.x * cellWallLength + xOffSet, maze.start.y * cellWallLength + yOffSet, cellWallLength, cellWallLength);
            g.DrawString("S", new Font("Arial", cellWallLength / 2), Brushes.Black, new RectangleF(maze.start.x * cellWallLength + xOffSet, maze.start.y * cellWallLength + yOffSet, cellWallLength, cellWallLength), format);

            g.FillRectangle(Brushes.HotPink, maze.end.x * cellWallLength + xOffSet, maze.end.y * cellWallLength + yOffSet, cellWallLength, cellWallLength);
            g.DrawString("E", new Font("Arial", cellWallLength / 2), Brushes.Black, new RectangleF(maze.end.x * cellWallLength + xOffSet, maze.end.y * cellWallLength + yOffSet, cellWallLength, cellWallLength), format);
        }
        private void MaskMaze(Index pos, int range, float cellWallLength)
        {
            for (int i = 0; i < maze.grid.GetLength(0); i++)
            {
                for (int k = 0; k < maze.grid.GetLength(1); k++)
                {
                    if (Math.Abs(k - pos.x) > range || Math.Abs(i - pos.y) > range)
                    {
                        g.FillRectangle(Brushes.Black, k * cellWallLength + xOffSet, i * cellWallLength + yOffSet, cellWallLength, cellWallLength);
                    }
                }
            }
        }
        private void DrawMazeGrid(Graphics g, float cellWallLength)
        {
            DrawStartAndEnd(g, cellWallLength);

            g.DrawLine(wallPen, xOffSet, yOffSet, xOffSet + cellWallLength * maze.grid.GetLength(1), yOffSet);
            g.DrawLine(wallPen, xOffSet, yOffSet, xOffSet, yOffSet + cellWallLength * maze.grid.GetLength(0));
            for (int i = 0; i < maze.grid.GetLength(0); i++)
            {
                for (int k = 0; k < maze.grid.GetLength(1); k++)
                {
                    if (maze.grid[i, k].wall_E)
                    {
                        g.DrawLine(wallPen, k * cellWallLength + cellWallLength + xOffSet, i * cellWallLength + yOffSet, k * cellWallLength + cellWallLength + xOffSet, i * cellWallLength + cellWallLength + yOffSet);
                    }
                    if (maze.grid[i, k].wall_S)
                    {
                        g.DrawLine(wallPen, k * cellWallLength + xOffSet, i * cellWallLength + cellWallLength + yOffSet, k * cellWallLength + cellWallLength + xOffSet, i * cellWallLength + cellWallLength + yOffSet);
                    }
                }
            }
        }
        private void SolveMaze_btn_Click(object sender, EventArgs e)
        {
            if (!mazeGenerationThread.IsAlive)
            {
                maze.player = new Index(-5, -5);
                maskMaze = false;

                string a = mazesolvemethod_combobox.Text;
                mazeGenerationThread = new Thread(() => SolveMaze(a));
                mazeGenerationThread.Start();
            }
        }
        private void DisplayHeatMap(int[,] heatmap)
        {
            float cellWallLength = (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0);
            int maxNum = 1;
            foreach (var num in heatmap)
            {
                if (num > maxNum) { maxNum = num; }
            }

            for (int i = 0; i < maze.grid.GetLength(0); i++)
            {
                for (int k = 0; k < maze.grid.GetLength(1); k++)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255 * heatmap[i, k] / maxNum, 255 * heatmap[i, k] / maxNum)), k * cellWallLength + xOffSet, i * cellWallLength + yOffSet, cellWallLength, cellWallLength);
                }
            }
            DrawMazeGrid(g,cellWallLength);
        }
        private void SolveMaze(string type)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (type == "Dijkstra's algorithm path")
            {
                maze.SolveMaze_Dijkstra();
            }
            else if (type == "Dijkstra's algorithm heatmap")
            {
                maze.MakeHeatmap();
                showheatmap = true;
            }
            else // type == Depth-first search
            {
                maze.SolveMaze_DFS();
            }
            UpdateMazeGraphics();

            sw.Stop();
            if (solvetime_lbl.InvokeRequired)
            {
                solvetime_lbl.Invoke(() => (solvetime_lbl.Text = "Time to solve: " + sw.Elapsed.TotalMilliseconds));
            }
            else
            {
                solvetime_lbl.Text = "Time to solve: " + sw.Elapsed.TotalMilliseconds;
            }
        }
        private void GenerateMaze_btn_Click(object sender, EventArgs e)
        {
            if (!mazeGenerationThread.IsAlive)
            {
                string a = pathType_combobox.Text;
                string b = mazeGeneration_combobox.Text;

                mazeGenerationThread = new Thread(() => GenerateMaze(a, b));
                mazeGenerationThread.Start();
            }

        }
        private void GenerateMaze(string pathtype, string generationMethod)
        {
            UInt16 pathtypeNum;
            Stopwatch sw = new Stopwatch();

            if (pathtype == "West - East")
            {
                pathtypeNum = 0;
            }
            else if (pathtype == "North - South")
            {
                pathtypeNum = 1;
            }
            else if (pathtype == "Centre - Edge")
            {
                pathtypeNum = 2;
            }
            else if(pathtype == "Random - Random")
            {
                pathtypeNum = 3;
            }
            else //pathtype == Max Difficulty
            {
                pathtypeNum = 4;
            }

            sw.Start();
            if (generationMethod == "Recursive Backtracking")
            {
                maze = new RectangularMaze((int)MazeWidth_count.Value, (int)MazeHeight_count.Value, pathtypeNum, new Action(UpdateMazeGraphics), (int)mazeGenerationDelay_count.Value);
                maze.GenerateMaze_RecursiveBacktracking();
            }
            else if (generationMethod == "Kruskal's Algorithm")
            {
                maze = new RectangularMaze((int)MazeWidth_count.Value, (int)MazeHeight_count.Value, pathtypeNum, new Action(UpdateMazeGraphics), (int)mazeGenerationDelay_count.Value);
                maze.GenerateMaze_Kruskal();
            }
            else if (generationMethod == "Prim's Algorithm")
            {
                maze = new RectangularMaze((int)MazeWidth_count.Value, (int)MazeHeight_count.Value, pathtypeNum, new Action(UpdateMazeGraphics), (int)mazeGenerationDelay_count.Value);
                maze.GenerateMaze_Prim();
            }
            else if (generationMethod == "Eller's Algorithm")
            {
                maze = new RectangularMaze((int)MazeWidth_count.Value, (int)MazeHeight_count.Value, pathtypeNum, new Action(UpdateMazeGraphics), (int)mazeGenerationDelay_count.Value);
                maze.GenerateMaze_Eller();
            }
            sw.Stop();
            if (pathtypeNum == 4)
            {
                maze.GenerateStartAndEndNodes(pathtypeNum);
            }
            UpdateMazeGraphics();

            if (generationtime_lbl.InvokeRequired)
            {
                generationtime_lbl.Invoke(() => (generationtime_lbl.Text = "Time to generate: " + sw.Elapsed.TotalMilliseconds));
            }
            else
            {
                generationtime_lbl.Text = "Time to generate: " + sw.Elapsed.TotalMilliseconds;
            }
        }
        private void Mazes2DForm_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Mazes2DForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.CapsLock)
            {
                maskMaze = maskMaze ^ true;
            }
            bool completedMaze = false;
            if (e.KeyData == Keys.W)
            {
                completedMaze = maze.MovePlayer(0);
            }
            else if (e.KeyData == Keys.D)
            {
                completedMaze = maze.MovePlayer(1);
            }
            else if (e.KeyData == Keys.S)
            {
                completedMaze = maze.MovePlayer(2);
            }
            else if (e.KeyData == Keys.A)
            {
                completedMaze = maze.MovePlayer(3);
            }
            Invalidate();
            if (completedMaze)
            {
                MessageBox.Show($"Well done! You have completed this {maze.grid.GetLength(0)} x {maze.grid.GetLength(1)} Maze");

            }
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveMaze_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            maze.SolveMaze_DFS();

            int x = rand.Next(9999999);
            string filename1 = "Rectangular Maze Solution " + x;
            string filename2 = "Rectangular Maze Unsolved " + x;

            saveImageGFX.Clear(Color.White);
            DrawSolution(saveImageGFX, (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0));
            DrawStartAndEnd(saveImageGFX, (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0));
            DrawMazeGrid(saveImageGFX, (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0));
            MazeImage.Save(filename1);

            saveImageGFX.Clear(Color.White);
            DrawStartAndEnd(saveImageGFX, (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0));
            DrawMazeGrid(saveImageGFX, (this.ClientSize.Height - yOffSet - 10) / maze.grid.GetLength(0));
            MazeImage.Save(filename2);
        }
    }
}
