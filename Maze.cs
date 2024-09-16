

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Maze_Generator_and_solver
{
    public abstract class Maze
    {
        public Index start, end;
        public int[,] heatmap;
        public Maze()
        {
            
        }

        protected abstract void ResetMaze();
        public abstract void MakeHeatmap();
        public abstract List<Index> SolveMaze_Dijkstra();
        public abstract void GenerateStartAndEndNodes(UInt16 pathType);
    }
   
}