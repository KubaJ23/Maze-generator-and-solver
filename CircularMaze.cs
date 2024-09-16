using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Maze_Generator_and_solver
{
    public class CircularMaze : FlatMaze
    {
        public CircularMaze(int columnNum, int rowNum) : base(columnNum, rowNum)
        {
            ResetMaze();
            GenerateStartAndEndNodes(0);
        }
        protected override void ResetMaze()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    grid[i, k] = new Cell();
                }
            }
        }
        public override void GenerateStartAndEndNodes(UInt16 pathType)
        {
            Random rand = new Random();

            start = new Index(rand.Next(grid.GetLength(1)), 0);
            end = new Index(rand.Next(grid.GetLength(1)), grid.GetLength(0) - 1);
            grid[start.y, start.x].wall_N = false;
            grid[end.y, end.x].wall_S = false;
        }
        protected override Index ConnectCells(Index cellPos_1, int direction)
        {
            Index cellPos_2;
            cellPos_2 = FindNeighbourIndex(cellPos_1, direction);
            switch (direction)
            {
                case 0://go up
                    grid[cellPos_1.y, cellPos_1.x].wall_N = false;
                    grid[cellPos_2.y, cellPos_2.x].wall_S = false;
                    break;
                case 1://go right
                    grid[cellPos_1.y, cellPos_1.x].wall_E = false;
                    grid[cellPos_2.y, cellPos_2.x].wall_W = false;
                    break;
                case 2://go down
                    grid[cellPos_1.y, cellPos_1.x].wall_S = false;
                    grid[cellPos_2.y, cellPos_2.x].wall_N = false;
                    break;
                case 3://go left
                    grid[cellPos_1.y, cellPos_1.x].wall_W = false;
                    grid[cellPos_2.y, cellPos_2.x].wall_E = false;
                    break;
                default:
                    throw new ArgumentException("invalid direction chosen when connecting cells");
            }
            return cellPos_2;
        }
        public void GenerateMaze()
        {
            // uses kruskal's algorithm but adapted 

            List<Index> EastWalls = new List<Index>();
            List<Index> SouthWalls = new List<Index>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    EastWalls.Add(new Index(k, i));
                }
            }
            for (int i = 0; i < grid.GetLength(0) - 1; i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    SouthWalls.Add(new Index(k, i));
                }
            }

            Index[,] pointerGrid = new Index[grid.GetLength(0), grid.GetLength(1)];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    pointerGrid[i, k] = new Index(k, i);
                }
            }
            Index chosenCell;
            int chosenDirection;
            int sets = grid.GetLength(0) * grid.GetLength(1);
            //together they form the chosen wall to attempt to remove
            Random rand = new Random();
            do
            {
                if (EastWalls.Count > 0 && SouthWalls.Count > 0)// choosing a random wall to remove
                {
                    int chooseWallList = rand.Next(2);
                    if (chooseWallList == 0)
                    {
                        chosenCell = TakeRandomIndexFromList(EastWalls);
                        chosenDirection = 1;
                    }
                    else// (chooseWallList == 1)
                    {
                        chosenCell = TakeRandomIndexFromList(SouthWalls);
                        chosenDirection = 2;
                    }
                }
                else
                {
                    if (EastWalls.Count > 0)
                    {
                        chosenCell = TakeRandomIndexFromList(EastWalls);
                        chosenDirection = 1;
                    }
                    else
                    {
                        chosenCell = TakeRandomIndexFromList(SouthWalls);
                        chosenDirection = 2;
                    }
                }
                Index chosenCell2 = FindNeighbourIndex(chosenCell, chosenDirection);

                //see if the wall's 2 cells are in the same set
                Index root1 = FindFinalPointerIndex(pointerGrid, pointerGrid[chosenCell.y, chosenCell.x]);
                Index root2 = FindFinalPointerIndex(pointerGrid, pointerGrid[chosenCell2.y, chosenCell2.x]);

                if (!(root1.y == root2.y && root1.x == root2.x))
                {
                    pointerGrid[root2.y, root2.x] = root1;
                    chosenCell = ConnectCells(chosenCell, chosenDirection);

                    sets--;
                    if (sets == 0)
                    {
                        break;
                    }
                }
            } while (EastWalls.Count != 0 || SouthWalls.Count != 0);
        }
        private bool ValidIndex(Index pos)
        {
            if (pos.y >= grid.GetLength(0) || pos.y < 0)
            {
                return false;
            }
            return true;
        }
        public override List<Index> SolveMaze_Dijkstra()
        {
            MakeHeatmap();

            List<Index> solutionPath = new List<Index>();
            Index currentPos = end;
            //follows the decreasing numbers path from the end node to the start node
            while (!(currentPos.x == start.x && currentPos.y == start.y))
            {
                Index nextPos;
                int currentPosDistance = this.heatmap[currentPos.y, currentPos.x];

                nextPos = new Index(currentPos.x, currentPos.y - 1);
                if (!grid[currentPos.y, currentPos.x].wall_N && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = new Index((currentPos.x + 1) % grid.GetLength(1), currentPos.y);
                if (!grid[currentPos.y, currentPos.x].wall_E && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = new Index(currentPos.x, currentPos.y + 1);
                if (!grid[currentPos.y, currentPos.x].wall_S && ValidIndex(nextPos) && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = new Index((grid.GetLength(1) + currentPos.x - 1) % grid.GetLength(1), currentPos.y);
                if (!grid[currentPos.y, currentPos.x].wall_W && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
            }
            solutionPath.Add(start);
            return solutionPath;
        }
        public override void MakeHeatmap()
        {
            Queue<Index> frontline = new Queue<Index>();
            int[,] visitedCells = new int[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    visitedCells[i, k] = -1;
                }
            }

            frontline.Enqueue(start);
            visitedCells[start.y, start.x] = 0;

            // numbers each cell in terms of its path distance from the start
            while (frontline.Count > 0)
            {
                Index nextMove;
                Index currentCell = frontline.Dequeue();
                {
                    nextMove = new Index(currentCell.x, currentCell.y - 1);
                    if (ValidIndex(nextMove) && !grid[currentCell.y, currentCell.x].wall_N)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = new Index((currentCell.x + 1) % grid.GetLength(1), currentCell.y);
                    if (ValidIndex(nextMove) && !grid[currentCell.y, currentCell.x].wall_E)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = new Index(currentCell.x, currentCell.y + 1);
                    if (ValidIndex(nextMove) && !grid[currentCell.y, currentCell.x].wall_S)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = new Index((grid.GetLength(1) + currentCell.x - 1) % grid.GetLength(1), currentCell.y);
                    if (ValidIndex(nextMove) && !grid[currentCell.y, currentCell.x].wall_W)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                }
            }
            this.heatmap = visitedCells;
        }
        private T TakeRandomIndexFromList<T>(List<T> list)
        {
            Random rand = new Random();
            int chosenIndex = rand.Next(list.Count);
            T chosenCell = list[chosenIndex];
            list.RemoveAt(chosenIndex);
            return chosenCell;
        }
        private Index FindNeighbourIndex(Index index, int direction)
        {
            switch (direction)
            {
                case 0://go up
                    return new Index(index.x, index.y - 1);
                case 1://go right
                    return new Index((index.x + 1) % grid.GetLength(1), index.y);
                case 2://go down
                    return new Index(index.x, index.y + 1);
                case 3://go left
                    return new Index((grid.GetLength(1) + index.x - 1) % grid.GetLength(1), index.y);
                default:
                    throw new ArgumentException("invalid direction chosen when connecting cells");
            }
        }
        private Index FindFinalPointerIndex(Index[,] array, Index startPos)
        {
            if (!(startPos.y == array[startPos.y, startPos.x].y && startPos.x == array[startPos.y, startPos.x].x))
            {
                Index currentPos = startPos;
                Index nextPos = array[currentPos.y, currentPos.x];
                while (!(nextPos.y == array[nextPos.y, nextPos.x].y && nextPos.x == array[nextPos.y, nextPos.x].x))
                {
                    currentPos = nextPos;
                    nextPos = array[currentPos.y, currentPos.x];
                }
                return nextPos;
            }
            return startPos;
        }
    }
   
}