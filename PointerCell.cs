namespace Maze_Generator_and_solver
{
    public class PointerCell : Cell
    {
        public Index North;
        public Index East;
        public Index South;
        public Index West;

        public PointerCell(Index n, Index e, Index s, Index w) : base()
        {
            East = e;
            South = s;
            North = n;
            West = w;
            wall_N = true;
            wall_E = true;
            wall_S = true;
            wall_W = true;
        }
        public PointerCell() : base()
        {
            wall_N = true;
            wall_E = true;
            wall_S = true;
            wall_W = true;
        }
    }
   
}