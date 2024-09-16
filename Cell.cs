

namespace Maze_Generator_and_solver
{
    public class Cell
    {
        public bool wall_N;
        public bool wall_E;
        public bool wall_S;
        public bool wall_W;

        public Cell ()
        {
            wall_E = true;
            wall_S = true;
            wall_N = true;
            wall_W = true;
        }
    }
}