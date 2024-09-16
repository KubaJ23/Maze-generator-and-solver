using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze_Generator_and_solver
{
    public partial class Maze3DForm : Form
    {
        Maze3D maze;

        Bitmap mazeNetImage;
        Graphics mazeNetGraphics;

        Bitmap mazeFaceImage;
        Graphics mazeFaceGraphics;

        Pen wallPen = new Pen(Brushes.Black, 4);
        public Maze3DForm()
        {
            InitializeComponent();
        }

        private void Maze3DForm_Load(object sender, EventArgs e)
        {
            mazeNetImage = new Bitmap(2000, 1500);
            mazeNetGraphics = Graphics.FromImage(mazeNetImage);

            mazeFaceImage = new Bitmap(1000, 1000);
            mazeFaceGraphics = Graphics.FromImage(mazeFaceImage);

            this.mazePictureBox.Image = mazeFaceImage;
            mazeFaceGraphics.Clear(Color.LightGray);
            maze = new Maze3D((int)this.MazeLength_count.Value);
            maze.GenerateMaze();
            DrawCurrentMazeFace(mazeFaceGraphics);
        }
        private void DrawMazeNet(Graphics g)
        {
            float cellWallLength = mazeNetImage.Width/maze.grid.GetLength(1);

            g.FillRectangle(Brushes.Orange, maze.start.x * cellWallLength, maze.start.y * cellWallLength, cellWallLength, cellWallLength);
            g.FillRectangle(Brushes.Purple, maze.end.x * cellWallLength, maze.end.y * cellWallLength, cellWallLength, cellWallLength);

            g.FillEllipse(Brushes.Green, maze.player.x * cellWallLength, maze.player.y * cellWallLength, cellWallLength, cellWallLength);

            for (int i = 0; i < maze.grid.GetLength(0); i++)
            {
                for (int k = 0; k < maze.grid.GetLength(1); k++)
                {
                    if (maze.grid[i,k] != null)
                    {
                        if (maze.grid[i, k].wall_N)
                        {
                            g.DrawLine(wallPen, k * cellWallLength + 1, i * cellWallLength, k * cellWallLength + cellWallLength + 1, i * cellWallLength);
                        }
                        if (maze.grid[i, k].wall_E)
                        {
                            g.DrawLine(wallPen, k * cellWallLength + cellWallLength + 1, i * cellWallLength, k * cellWallLength + cellWallLength + 1, i * cellWallLength + cellWallLength);
                        }
                        if (maze.grid[i, k].wall_S)
                        {
                            g.DrawLine(wallPen, k * cellWallLength + 1, i * cellWallLength + cellWallLength, k * cellWallLength + cellWallLength + 1, i * cellWallLength + cellWallLength);
                        }
                        if (maze.grid[i, k].wall_W)
                        {
                            g.DrawLine(wallPen, k * cellWallLength + 1, i * cellWallLength, k * cellWallLength + 1, i * cellWallLength + cellWallLength);
                        }
                    }
                }
            }
        }
        private void DrawSolutionOnNet(Graphics g, List<Index> list)
        {
            float cellWallLength = mazeNetImage.Width / maze.grid.GetLength(1);
            foreach (var item in list)
            {
                g.FillRectangle(Brushes.LightSkyBlue, item.x * cellWallLength, item.y * cellWallLength, cellWallLength, cellWallLength);
            }
            DrawMazeNet(g);
        }
        private void DrawCurrentMazeFace(Graphics g)
        {
            float cellWallLength = mazeFaceImage.Width / maze.sideLength;
            int faceYPos = maze.player.y / maze.sideLength;
            int faceXPos = maze.player.x / maze.sideLength;

            for (int y = 0; y < maze.sideLength; y++)
            {
                for (int x = 0; x < maze.sideLength; x++)
                {
                    int YgridOffset = faceYPos * maze.sideLength;
                    int XgridOffset = faceXPos * maze.sideLength;

                    if (maze.start.Equals(new Index(x + XgridOffset, y + YgridOffset)))
                    {
                        g.FillRectangle(Brushes.Orange, x * cellWallLength, y * cellWallLength, cellWallLength, cellWallLength);
                    }
                    if (maze.end.Equals(new Index(x + XgridOffset, y + YgridOffset)))
                    {
                        g.FillRectangle(Brushes.Purple, x * cellWallLength, y * cellWallLength, cellWallLength, cellWallLength);
                    }
                    if (maze.player.Equals(new Index(x + XgridOffset, y + YgridOffset)))
                    {
                        g.FillEllipse(Brushes.Green, x * cellWallLength, y * cellWallLength, cellWallLength, cellWallLength);
                    }

                    if (maze.grid[y + YgridOffset, x + XgridOffset].wall_N)
                    {
                        g.DrawLine(wallPen, x * cellWallLength, y * cellWallLength, x * cellWallLength + cellWallLength, y * cellWallLength);
                    }
                    if (maze.grid[y + YgridOffset, x + XgridOffset].wall_E)
                    {
                        g.DrawLine(wallPen, x * cellWallLength + cellWallLength, y * cellWallLength, x * cellWallLength + cellWallLength, y * cellWallLength + cellWallLength);
                    }
                    if (maze.grid[y + YgridOffset, x + XgridOffset].wall_S)
                    {
                        g.DrawLine(wallPen, x * cellWallLength, y * cellWallLength + cellWallLength, x * cellWallLength + cellWallLength, y * cellWallLength + cellWallLength);
                    }
                    if (maze.grid[y + YgridOffset, x + XgridOffset].wall_W)
                    {
                        g.DrawLine(wallPen, x * cellWallLength, y * cellWallLength, x * cellWallLength, y * cellWallLength + cellWallLength);
                    }
                }
            }
        }
        private void GenerateMaze_btn_Click(object sender, EventArgs e)
        {
            mazeFaceGraphics.Clear(Color.LightGray);
            maze = new Maze3D((int)this.MazeLength_count.Value);
            maze.GenerateMaze();
            DrawCurrentMazeFace(mazeFaceGraphics);

            this.mazePictureBox.Image = mazeFaceImage;
            Refresh();
        }
        private void SolveMaze_btn_Click(object sender, EventArgs e)
        {
            mazeNetGraphics.Clear(Color.LightGray);
            DrawSolutionOnNet(mazeNetGraphics, maze.SolveMaze_Dijkstra());
            this.mazePictureBox.Image = mazeNetImage;
            Refresh();
        }
        private void Maze3DForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool mazecompleted = false;
            if (e.KeyCode == Keys.W)
            {
                mazecompleted = maze.MovePlayer(0);
            }
            else if (e.KeyCode == Keys.D)
            {
                mazecompleted = maze.MovePlayer(1);
            }
            else if (e.KeyCode == Keys.S)
            {
                mazecompleted = maze.MovePlayer(2);
            }
            else if (e.KeyCode == Keys.A)
            {
                mazecompleted = maze.MovePlayer(3);
            }
            mazeFaceGraphics.Clear(Color.LightGray);
            DrawCurrentMazeFace(mazeFaceGraphics);
            Refresh();

            if (mazecompleted)
            {
                MessageBox.Show("Congratulations You have completed this 3D cube maze");
            }
        }

        private void SaveMaze_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int mazeNumber = rand.Next(999999999);

            mazeNetGraphics.Clear(Color.LightGray);
            DrawSolutionOnNet(mazeNetGraphics, maze.SolveMaze_Dijkstra());
            mazeNetImage.Save("3D Maze Solution" + mazeNumber);

            mazeNetGraphics.Clear(Color.LightGray);
            DrawMazeNet(mazeNetGraphics);
            mazeNetImage.Save("3D Maze Unsolved" + mazeNumber);
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
