using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Maze_Generator_and_solver
{
    public partial class MazesCircularForm : Form
    {
        CircularMaze maze;
        Graphics g;
        Thread mazeGenerationThread;

        Pen wallPen;
        SolidBrush backgroundBrush;
        int yOffSet = 55;// amount of pixels that the maze grid is offset from the top left corner 150,10
        int xOffSet = 3;
        int centrePadding = 16;

        Graphics saveImageGFX;
        Bitmap MazeImage;

        bool showheatmap, showSolution;
        List<Index> highlightIndexes;
        public MazesCircularForm()
        {
            InitializeComponent();
        }

        private void MazesCircularForm_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            maze = new CircularMaze(10,10);
            GenerateMaze();
            mazeGenerationThread = new Thread(() => maze.GenerateMaze());
            wallPen = new Pen(Brushes.Black, 2f);
            backgroundBrush = new SolidBrush(this.BackColor);
            showheatmap = false;
            showSolution= false;
            MazeImage = new Bitmap(this.Bounds.Width, this.Bounds.Height, PixelFormat.Format32bppArgb);
            saveImageGFX = Graphics.FromImage(MazeImage);
        }

        private void GenerateMaze_btn_Click(object sender, EventArgs e)
        {
            if (!mazeGenerationThread.IsAlive)
            {
                mazeGenerationThread = new Thread(() => GenerateMaze());
                mazeGenerationThread.Start();
            }
        }
        private void GenerateMaze()
        {
            maze = new CircularMaze((int)MazeLayers_count.Value, (int)MazeWidth_count.Value);
            maze.GenerateMaze();
            Invalidate();
        }
        private void MazesCircularForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            if (showSolution)
            {
                showSolution= false;
                DrawSolution(g,highlightIndexes, centrePadding, (ClientSize.Height - yOffSet) / (2 * maze.grid.GetLength(0) + centrePadding), 360f / maze.grid.GetLength(1));
            }
            if (showheatmap)
            {
                showheatmap = false;
                DrawHeatmap(g, highlightIndexes, centrePadding, (ClientSize.Height - yOffSet) / (2 * maze.grid.GetLength(0) + centrePadding), 360f / maze.grid.GetLength(1), maze.heatmap);
            }
            DrawMaze(g,centrePadding);
        }
        private void DrawSolution(Graphics g,List<Index> list, int centrePadding, float cellLength, float cellAngleSizeDegrees)
        {
            list = MergeSort(list);
            for (int i = 0; i < list.Count; i++)
            {
                float sideLength = centrePadding * cellLength + 2 * (cellLength * (list[i].y + 1));
                float x = xOffSet + cellLength * (maze.grid.GetLength(0) - list[i].y - 1);
                float y = yOffSet + cellLength * (maze.grid.GetLength(0) - list[i].y - 1);
                Rectangle outsideSquare = new Rectangle((int)x, (int)y, (int)sideLength, (int)sideLength);

                g.FillPie(Brushes.OrangeRed, outsideSquare, list[i].x * -cellAngleSizeDegrees, -cellAngleSizeDegrees);

                Rectangle insideSquare = new Rectangle((int)(x + cellLength), (int)(y + cellLength), (int)(sideLength - 2 * cellLength), (int)(sideLength - 2 * cellLength));
                g.FillPie(backgroundBrush, insideSquare, list[i].x * -cellAngleSizeDegrees, -cellAngleSizeDegrees);
            }
        }
        private void DrawHeatmap(Graphics g, List<Index> list, int centrePadding, float cellLength, float cellAngleSizeDegrees, int[,] heatmap)
        {
            int maxNum = 1;
            foreach (var num in heatmap)
            {
                if (num > maxNum) { maxNum = num; }
            }
            for (int i = heatmap.GetLength(0)-1; i >= 0; i--)
            {
                for (int k = 0; k < heatmap.GetLength(1); k++)
                {
                    float sideLength = centrePadding * cellLength + 2 * (cellLength * (i + 1));
                    float x = xOffSet + cellLength * (maze.grid.GetLength(0) - i - 1);
                    float y = yOffSet + cellLength * (maze.grid.GetLength(0) - i - 1);
                    Rectangle outsideSquare = new Rectangle((int)x, (int)y, (int)sideLength, (int)sideLength);

                    SolidBrush br = new SolidBrush(Color.FromArgb(255, 255 * heatmap[i, k] / maxNum, 255 * heatmap[i, k] / maxNum));
                    g.FillPie(br, outsideSquare, k * -cellAngleSizeDegrees, -cellAngleSizeDegrees);

                    Rectangle insideSquare = new Rectangle((int)(x + cellLength), (int)(y + cellLength), (int)(sideLength - 2 * cellLength), (int)(sideLength - 2 * cellLength));
                    g.FillPie(backgroundBrush, insideSquare, k * -cellAngleSizeDegrees, -cellAngleSizeDegrees);
                }
            }
        }
        private List<Index> MergeSort(List<Index> list)
        {
            if (list.Count > 1)
            {
                List<Index> l1 = list.GetRange(0, list.Count / 2);
                List<Index> l2 = list.GetRange(list.Count / 2, (list.Count / 2) + (list.Count%2));
                l1 = MergeSort(l1);
                l2 = MergeSort(l2);
                list = MergeList(l1, l2);
            }
            
            return list;
        }
        private List<Index> MergeList(List<Index> list1, List<Index> list2) // sorts list high to low
        {
            List<Index> combinedList = new();
            int list1front = 0;
            int list2front = 0;
            while (list1front < list1.Count && list2front < list2.Count)
            {
                if (list1[list1front].y > list2[list2front].y)
                {
                    combinedList.Add(list1[list1front]);
                    list1front++;
                }
                else
                {
                    combinedList.Add(list2[list2front]);
                    list2front++;
                }
            }
            while (list1front < list1.Count)
            {
                combinedList.Add(list1[list1front]);
                list1front++;
            }
            while (list2front < list2.Count)
            {
                combinedList.Add(list2[list2front]);
                list2front++;
            }
            return combinedList;
        }
        private void DrawMaze(Graphics g, int centrePadding)
        {
            float cellAngleSizeDegrees = 360f / maze.grid.GetLength(1);
            float cellAngleSizeRadians = 2 * MathF.PI / maze.grid.GetLength(1);
            float cellLength = (ClientSize.Height - yOffSet) / (2 * maze.grid.GetLength(0) + centrePadding);

            float x = xOffSet + cellLength * maze.grid.GetLength(0);
            float y = yOffSet + cellLength * maze.grid.GetLength(0);
            RectangleF mazeBox = new RectangleF(x, y, centrePadding * cellLength, centrePadding * cellLength);
            PointF centre = new PointF(x + (centrePadding * cellLength / 2), y + (centrePadding * cellLength / 2));

            for (int i = 0; i < maze.grid.GetLength(0); i++)
            {
                for (int k = 0; k < maze.grid.GetLength(1); k++)
                {
                    if (maze.grid[i, k].wall_N)
                    {
                        g.DrawArc(wallPen, mazeBox, k * -cellAngleSizeDegrees, -cellAngleSizeDegrees);
                    }
                    if (maze.grid[i, k].wall_W)
                    {
                        float hypotenuse1 = cellLength * i + centrePadding * cellLength / 2;
                        float hypotenuse2 = cellLength * i + centrePadding * cellLength / 2 + cellLength;
                        PointF p1 = new PointF(centre.X + (hypotenuse1 * MathF.Cos(cellAngleSizeRadians * k)), centre.Y - (hypotenuse1 * MathF.Sin(cellAngleSizeRadians * k)));
                        PointF p2 = new PointF(centre.X + (hypotenuse2 * MathF.Cos(cellAngleSizeRadians * k)), centre.Y - (hypotenuse2 * MathF.Sin(cellAngleSizeRadians * k)));
                        g.DrawLine(wallPen, p1, p2);
                    }
                }
                mazeBox.Y -= cellLength;
                mazeBox.X -= cellLength;
                mazeBox.Width += 2 * cellLength;
                mazeBox.Height += 2 * cellLength;
            }
            for (int k = 0; k < maze.grid.GetLength(1); k++)
            {
                if (maze.grid[maze.grid.GetLength(0) - 1, k].wall_S)
                {
                    g.DrawArc(wallPen, mazeBox, k * -cellAngleSizeDegrees, -cellAngleSizeDegrees);
                }
            }
        }

        private void MazesCircularForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void SaveMaze_btn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            SolveMaze();
           
            int x = rand.Next(9999999);
            string filename1 = "Circular Maze Solution " + x;
            string filename2 = "Circular Maze Unsolved " + x;

            saveImageGFX.Clear(this.BackColor);
            DrawSolution(saveImageGFX, highlightIndexes, centrePadding, (ClientSize.Height - yOffSet) / (2 * maze.grid.GetLength(0) + centrePadding), 360f / maze.grid.GetLength(1));
            DrawMaze(saveImageGFX, centrePadding);
            MazeImage.Save(filename1);

            saveImageGFX.Clear(this.BackColor);
            DrawMaze(saveImageGFX, centrePadding);
            MazeImage.Save(filename2);
        }

        private void SolveMaze_btn_Click(object sender, EventArgs e)
        {
            showSolution = true;
            SolveMaze();
            Invalidate();
        }

        private void MazeHeatmap_btn_Click(object sender, EventArgs e)
        {
            showheatmap= true;
            maze.MakeHeatmap();
            Invalidate();
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SolveMaze()
        {
            highlightIndexes = maze.SolveMaze_Dijkstra();
        }
    }
}
