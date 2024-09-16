using System.Numerics;

namespace Maze_Generator_and_solver
{
    public class Maze3D : Maze
    {
        public PointerCell[,] grid { get; }
        public Index player;
        public int sideLength { get; }
        public Maze3D(int sideLength)
        {
            this.sideLength = sideLength;
            grid = new PointerCell[3*sideLength,4*sideLength];
            heatmap = new int[3 * sideLength, 4 * sideLength];
            ResetMaze();

            start = new Index(sideLength + sideLength / 2, sideLength + sideLength / 2);
        }
        public bool MovePlayer(int direction)
        {
            if (direction == 0 && !grid[player.y, player.x].wall_N)
            {
                player = grid[player.y, player.x].North;
            }
            else if (direction == 1 && !grid[player.y, player.x].wall_E)
            {
                player = grid[player.y, player.x].East;
            }
            else if (direction == 2 && !grid[player.y, player.x].wall_S)
            {
                player = grid[player.y, player.x].South;
            }
            else if (direction == 3 && !grid[player.y, player.x].wall_W)
            {
                player = grid[player.y, player.x].West;
            }
            if (player.Equals(end))
            {
                return true;
            }
            return false;
        }
        protected override void ResetMaze()
        {
            /* 3D Maze Net:
             -> face numbers are referenced in the comments below
                  _________
                  |       |
                 ___      |
                |   |     |
               /| 0 |\   \./
             ___|___|_______      ___
            |   |   |   |   |    |   
        ->  | 1 | 2 | 3 | 4 | -> | 1  ...
            |___|___|___|___|    |___
               \|   |/   /`\      
                | 5 |     |      
                |___|     |     
                  |_______|
             */
            for (int i = sideLength; i < sideLength * 2; i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    grid[i, k] = new PointerCell();
                }
            }
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = sideLength; k < sideLength * 2; k++)
                {
                    grid[i, k] = new PointerCell();
                }
            }

            //connect middles of faces 1,2,3,4 (4 loops to -> 1) 
            for (int i = sideLength; i < sideLength * 2 - 1; i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    Index currentCell = new Index(k, i);
                    Index adjacentCell = new Index((k + 1) % grid.GetLength(1), i);
                    ConnectAdjacentCellsPointers(currentCell, adjacentCell);
                    adjacentCell = new Index(k, i + 1);
                    ConnectAdjacentCellsPointers(currentCell, adjacentCell);
                }
            }
            for (int k = 0; k < grid.GetLength(1); k++)
            {
                Index currentCell = new Index(k, sideLength * 2 - 1);
                Index adjacentCell = new Index((k + 1) % grid.GetLength(1), sideLength * 2 - 1);
                ConnectAdjacentCellsPointers(currentCell, adjacentCell);
            }

            //connect middle parts of faces 0,2,5
            for (int i = 0; i < grid.GetLength(0) - 1; i++)
            {
                for (int k = sideLength; k < 2 * sideLength; k++)
                {
                    Index currentCell = new Index(k, i);
                    Index adjacentCell = new Index(k, i + 1);
                    ConnectAdjacentCellsPointers(currentCell, adjacentCell);
                }
                for (int k = sideLength; k < 2 * sideLength - 1; k++)
                {
                    Index currentCell = new Index(k, i);
                    Index adjacentCell = new Index(k + 1, i);
                    ConnectAdjacentCellsPointers(currentCell, adjacentCell);
                }
                if (i == grid.GetLength(0) - 2)
                {
                    i++;
                    for (int k = sideLength; k < 2 * sideLength - 1; k++)
                    {
                        Index currentCell = new Index(k, i);
                        Index adjacentCell = new Index(k + 1, i);
                        ConnectAdjacentCellsPointers(currentCell, adjacentCell);
                    }
                }
            }

            //connect edges between faces 0,3 & 0,1
            for (int i = 0; i < sideLength; i++)
            {
                int x = 2 * sideLength - 1;
                Index cell2 = new Index(x + sideLength - i, sideLength);
                grid[i, x].East = cell2;
                grid[cell2.y, cell2.x].North = new Index(x, i);

                cell2 = new Index(i, sideLength);
                grid[i, sideLength].West = cell2;
                grid[cell2.y, cell2.x].North = new Index(sideLength, i);
            }

            //connect edges between faces 5,3 & 5,1
            for (int i = 2 * sideLength; i < grid.GetLength(0); i++)
            {
                int x = 2 * sideLength - 1;
                Index cell2 = new Index(x + i - 2 * sideLength + 1, 2 * sideLength - 1);
                grid[i, x].East = cell2;
                grid[cell2.y, cell2.x].South = new Index(x, i);

                cell2 = new Index(3 * sideLength - 1 - i, 2 * sideLength - 1);
                grid[i, sideLength].West = cell2;
                grid[cell2.y, cell2.x].South = new Index(sideLength, i);
            }

            //connect edges between faces 0,4 & 5,4
            for (int x = sideLength; x < 2 * sideLength; x++)
            {
                Index cell2 = new Index(5 * sideLength - 1 - x, sideLength);
                grid[0, x].North = cell2;
                grid[cell2.y, cell2.x].North = new Index(x, 0);

                cell2 = new Index(5 * sideLength - 1 - x, 2 * sideLength - 1);
                grid[3 * sideLength - 1, x].South = cell2;
                grid[cell2.y, cell2.x].South = new Index(x, 3 * sideLength - 1);
            }
        }
        public override void GenerateStartAndEndNodes(UInt16 pathType)
        {
            start = new Index(sideLength + sideLength / 2, sideLength + sideLength / 2);

            MakeHeatmap();
            int maxnum = 0;
            Index maxnumPos = start;
            for (int i = 0; i < this.heatmap.GetLength(0); i++)
            {
                for (int k = 0; k < this.heatmap.GetLength(1); k++)
                {
                    if (heatmap[i, k] > maxnum)
                    {
                        maxnum = heatmap[i, k];
                        maxnumPos = new Index(k, i);
                    }
                }
            }
            start = maxnumPos;
            maxnum = 0;

            MakeHeatmap();
            for (int i = 0; i < this.heatmap.GetLength(0); i++)
            {
                for (int k = 0; k < this.heatmap.GetLength(1); k++)
                {
                    if (heatmap[i, k] > maxnum)
                    {
                        maxnum = heatmap[i, k];
                        maxnumPos = new Index(k, i);
                    }
                }
            }
            end = maxnumPos;

            player = start;
        }
        private void ConnectAdjacentCellsPointers(Index cell1, Index cell2)
        {
            if (cell2.Equals(new Index(cell1.x, cell1.y - 1)))
            {
                grid[cell1.y, cell1.x].North = cell2;
                grid[cell2.y, cell2.x].South = cell1;
            }
            else if (cell2.Equals(new Index((cell1.x + 1) % grid.GetLength(1), cell1.y)))
            {
                grid[cell1.y, cell1.x].East = cell2;
                grid[cell2.y, cell2.x].West = cell1;
            }
            else if (cell2.Equals(new Index(cell1.x, cell1.y + 1)))
            {
                grid[cell1.y, cell1.x].South = cell2;
                grid[cell2.y, cell2.x].North = cell1;
            }
            else if (cell2.Equals(new Index((cell1.x - 1) % grid.GetLength(1), cell1.y)))
            {
                grid[cell1.y, cell1.x].West = cell2;
                grid[cell2.y, cell2.x].East = cell1;
            }
            else { throw new Exception("You tried to connect 2 cells which are not adjacent"); }
        }
        private void ConnectNonAdjacentCellsWalls(Index cell1, Index cell2)
        {
            if (grid[cell1.y, cell1.x].North.Equals(cell2))
            {
                grid[cell1.y, cell1.x].wall_N = false;
            }
            else if (grid[cell1.y, cell1.x].East.Equals(cell2))
            {
                grid[cell1.y, cell1.x].wall_E = false;
            }
            else if (grid[cell1.y, cell1.x].South.Equals(cell2))
            {
                grid[cell1.y, cell1.x].wall_S = false;
            }
            else if (grid[cell1.y, cell1.x].West.Equals(cell2))
            {
                grid[cell1.y, cell1.x].wall_W = false;
            }

            if (grid[cell2.y, cell2.x].North.Equals(cell1))
            {
                grid[cell2.y, cell2.x].wall_N = false;
            }
            else if (grid[cell2.y, cell2.x].East.Equals(cell1))
            {
                grid[cell2.y, cell2.x].wall_E = false;
            }
            else if (grid[cell2.y, cell2.x].South.Equals(cell1))
            {
                grid[cell2.y, cell2.x].wall_S = false;
            }
            else if (grid[cell2.y, cell2.x].West.Equals(cell1))
            {
                grid[cell2.y, cell2.x].wall_W = false;
            }
        }
        private List<Index> FindPossibleMoves(PointerCell cell, bool[,] visitedCells)
        {
            List<Index> possibleMoves = new List<Index>();
            Index possibleMove;

            possibleMove = cell.North;
            if (!visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleMoves.Add(possibleMove);
            }

            possibleMove = cell.East;
            if (!visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleMoves.Add(possibleMove);
            }

            possibleMove = cell.South;
            if (!visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleMoves.Add(possibleMove);
            }

            possibleMove = cell.West;
            if (!visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleMoves.Add(possibleMove);
            }

            return possibleMoves;
        }
        public void GenerateMaze() //RecursiveBacktracking
        {
            Stack<Index> path = new Stack<Index>();
            bool[,] visitedGrid = new bool[grid.GetLength(0), grid.GetLength(1)];

            Index currentCell = start;
            Random rand = new Random();
            do
            {
                List<Index> possibleMoves = FindPossibleMoves(grid[currentCell.y, currentCell.x], visitedGrid);
                if (possibleMoves.Count == 0)
                {
                    visitedGrid[currentCell.y, currentCell.x] = true;
                    currentCell = path.Pop();
                }
                else
                {
                    visitedGrid[currentCell.y, currentCell.x] = true;
                    path.Push(currentCell);
                    Index chosenMove = possibleMoves[rand.Next(possibleMoves.Count)];
                    ConnectNonAdjacentCellsWalls(currentCell, chosenMove);
                    currentCell= chosenMove;
                }
            } while (path.Count > 0);

            GenerateStartAndEndNodes(0);
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
                    nextMove = grid[currentCell.y, currentCell.x].North;
                    if (!grid[currentCell.y, currentCell.x].wall_N)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = grid[currentCell.y, currentCell.x].East;
                    if (!grid[currentCell.y, currentCell.x].wall_E)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = grid[currentCell.y, currentCell.x].South;
                    if (!grid[currentCell.y, currentCell.x].wall_S)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                    nextMove = grid[currentCell.y, currentCell.x].West;
                    if (!grid[currentCell.y, currentCell.x].wall_W)
                    {
                        if (visitedCells[nextMove.y, nextMove.x] == -1)
                        {
                            visitedCells[nextMove.y, nextMove.x] = visitedCells[currentCell.y, currentCell.x] + 1;
                            frontline.Enqueue(nextMove);
                        }
                    }
                }
            }
            heatmap = visitedCells;
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

                nextPos = grid[currentPos.y, currentPos.x].North;
                if (!grid[currentPos.y, currentPos.x].wall_N && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = grid[currentPos.y, currentPos.x].East;
                if (!grid[currentPos.y, currentPos.x].wall_E && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = grid[currentPos.y, currentPos.x].South;
                if (!grid[currentPos.y, currentPos.x].wall_S && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = grid[currentPos.y, currentPos.x].West;
                if (!grid[currentPos.y, currentPos.x].wall_W && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
            }
            return solutionPath;
        }
    }
   
}