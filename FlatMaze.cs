namespace Maze_Generator_and_solver
{
    public abstract class FlatMaze : Maze
    {
        public Cell[,] grid { get; }
        
        public FlatMaze(int columnNum, int rowNum)
        {
            grid = new Cell[rowNum, columnNum];
            heatmap = new int[columnNum, rowNum];
        }
        protected abstract Index ConnectCells(Index cellPos_1, int direction);
    }
   
}