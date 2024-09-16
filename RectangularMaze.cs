using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Generator_and_solver
{
    public class RectangularMaze : FlatMaze
    {
        private Action updateMazeGraphics;
        private int generationDelay;
        public List<Index> highlightIndexes;

        public Index player;
        public RectangularMaze(int columnNum, int rowNum, UInt16 pathType, Action updateMazeGraphics, int generationDelay) : base(columnNum, rowNum)
        {
            ResetMaze();
            GenerateStartAndEndNodes(pathType);
            this.updateMazeGraphics = updateMazeGraphics;
            highlightIndexes = new List<Index>();
            this.generationDelay = generationDelay;
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
        public bool MovePlayer(int direction)// returns true if the player has reached the end node, therefore solving the maze. otherwise returns false.
        {
            Index possiblePos = FindNeighbourIndex(player, direction);
            if (ValidIndex(possiblePos) && !CheckForWall(player, direction))
            {
                player = possiblePos;
                if (possiblePos.x == end.x && possiblePos.y == end.y)
                {
                    return true;
                }
            }
            return false;
        }
        public override void GenerateStartAndEndNodes(UInt16 pathType)
        {
            // pathType options: west-east, north-south, centre-wall, random-random, max ddifficulty  (vice-versa)
            Random rand = new Random();
            switch (pathType)
            {
                case 0:
                    int x = rand.Next(2) * (grid.GetLength(1) - 1);
                    start = new Index(x, rand.Next(grid.GetLength(0)));
                    end = new Index(grid.GetLength(1) - 1 - x, rand.Next(grid.GetLength(0)));
                    break;
                case 1:
                    int y = rand.Next(2) * (grid.GetLength(0) - 1);
                    start = new Index(rand.Next(grid.GetLength(1)), y);
                    end = new Index(rand.Next(grid.GetLength(1)), grid.GetLength(0) - 1 - y);
                    break;
                case 2:
                    start = new Index((grid.GetLength(1) - 1) / 2, (grid.GetLength(0) - 1) / 2);
                    end = new Index(rand.Next(2) * (grid.GetLength(1) - 1), rand.Next(2) * (grid.GetLength(0) - 1));
                    break;
                case 3:
                    start = new Index(rand.Next(grid.GetLength(1)), rand.Next(grid.GetLength(0)));
                    Index tempindex = new Index(rand.Next(grid.GetLength(1)), rand.Next(grid.GetLength(0)));
                    while ((start.x == tempindex.x) && (start.y == tempindex.y))
                    {
                        tempindex = new Index(rand.Next(grid.GetLength(1)), rand.Next(grid.GetLength(0)));
                    }
                    end = tempindex;
                    break;
                case 4:
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
                    break;
                default:
                    throw new ArgumentException("No valid path type chosen for maze");
            }
            this.player = start;
        }
        public void GenerateMaze_RecursiveBacktracking()
        {
            Stack<Index> path = new Stack<Index>();
            bool[,] visitedGrid = new bool[grid.GetLength(0), grid.GetLength(1)];

            Index currentCell = start;
            Random rand = new Random();
            do
            {
                List<int> possibleDirections = FindPossibleDirections(currentCell, visitedGrid);
                if (possibleDirections.Count == 0)
                {
                    visitedGrid[currentCell.y, currentCell.x] = true;
                    currentCell = path.Pop();
                }
                else
                {
                    visitedGrid[currentCell.y, currentCell.x] = true;
                    path.Push(currentCell);
                    int direction = possibleDirections[rand.Next(possibleDirections.Count)];
                    currentCell = ConnectCells(currentCell, direction);
                }
                UpdateHighlitedIndexes(path.ToList<Index>());
                updateMazeGraphics();
                Thread.Sleep(generationDelay);
            } while (path.Count > 0);
        }
        public void GenerateMaze_Kruskal()
        {
            List<Index> EastWalls = new List<Index>();
            List<Index> SouthWalls = new List<Index>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1) - 1; k++)
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
                UpdateHighlitedIndexes(new List<Index>() { chosenCell, chosenCell2 });

                //see if the wall's 2 cells are in the same set
                Index root1 = FindFinalPointerIndex(pointerGrid, pointerGrid[chosenCell.y, chosenCell.x]);
                Index root2 = FindFinalPointerIndex(pointerGrid, pointerGrid[chosenCell2.y, chosenCell2.x]);

                if (!(root1.y == root2.y && root1.x == root2.x))
                {
                    pointerGrid[root2.y, root2.x] = root1;
                    chosenCell = ConnectCells(chosenCell, chosenDirection);

                    Thread.Sleep(generationDelay);
                    updateMazeGraphics();

                    sets--;
                    if (sets <= 0)
                    {
                        break;
                    }
                }
            } while (EastWalls.Count != 0 || SouthWalls.Count != 0);
            UpdateHighlitedIndexes(new List<Index>());
        }
        public void GenerateMaze_Prim()
        {
            bool[,] visitedCells = new bool[grid.GetLength(0), grid.GetLength(1)];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    visitedCells[i, k] = false;
                }
            }//makes an array of false boolean values representing that all the cells have not been visited yet

            List<Index> FrontlineCells = new List<Index>();

            FrontlineCells.Add(start);

            while (FrontlineCells.Count > 0)
            {
                Index chosenCell = TakeRandomIndexFromList(FrontlineCells);
                visitedCells[chosenCell.y, chosenCell.x] = true;
                List<int> possibleDirections = new List<int>();
                Index possibleMove;
                {
                    possibleMove = new Index(chosenCell.x, chosenCell.y - 1);
                    if (ValidIndex(possibleMove))
                    {
                        if (!FrontlineCells.Contains(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
                        {
                            FrontlineCells.Add(possibleMove);
                        }
                        if (visitedCells[possibleMove.y, possibleMove.x])
                        {
                            possibleDirections.Add(0);
                        }
                    }

                    possibleMove = new Index(chosenCell.x + 1, chosenCell.y);
                    if (ValidIndex(possibleMove))
                    {
                        if (!FrontlineCells.Contains(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
                        {
                            FrontlineCells.Add(possibleMove);
                        }
                        if (visitedCells[possibleMove.y, possibleMove.x])
                        {
                            possibleDirections.Add(1);
                        }
                    }

                    possibleMove = new Index(chosenCell.x, chosenCell.y + 1);
                    if (ValidIndex(possibleMove))
                    {
                        if (!FrontlineCells.Contains(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
                        {
                            FrontlineCells.Add(possibleMove);
                        }
                        if (visitedCells[possibleMove.y, possibleMove.x])
                        {
                            possibleDirections.Add(2);
                        }
                    }

                    possibleMove = new Index(chosenCell.x - 1, chosenCell.y);
                    if (ValidIndex(possibleMove))
                    {
                        if (!FrontlineCells.Contains(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
                        {
                            FrontlineCells.Add(possibleMove);
                        }
                        if (visitedCells[possibleMove.y, possibleMove.x])
                        {
                            possibleDirections.Add(3);
                        }
                    }
                }
                if (possibleDirections.Count != 0)
                {
                    chosenCell = ConnectCells(chosenCell, TakeRandomIndexFromList(possibleDirections));
                }
                UpdateHighlitedIndexes(FrontlineCells);
                updateMazeGraphics();
                Thread.Sleep(generationDelay);
            }
        }
        public void GenerateMaze_Eller()
        {
            int chanceToConnectSet = 2;
            int chanceToConnectDown = 2;

            int setCounter;
            int[] currentRow = new int[grid.GetLength(1)];
            int[] newRow = new int[grid.GetLength(1)];
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                currentRow[i] = i;
            }
            setCounter = currentRow.Length;

            highlightIndexes.Clear();
            Random rand = new Random();
            for (int i = 0; i < grid.GetLength(0) - 1; i++)
            {
                for (int k = 0; k < currentRow.Length - 1; k++)
                {
                    int root1 = FindFinalPointerIndex(currentRow, k);
                    int root2 = FindFinalPointerIndex(currentRow, k + 1);
                    if (root1 != root2)
                    {
                        // 50/50 chance that this will happen
                        if (rand.Next(chanceToConnectSet) == 0)
                        {
                            Index tempindex = new Index(k, i);
                            tempindex = ConnectCells(tempindex, 1);
                            currentRow[root2] = root1;
                            setCounter--;
                        }
                    }
                }

                Dictionary<int, int> lastItemInSet = new Dictionary<int, int>();
                for (int b = currentRow.Length - 1; b >= 0; b--)
                {
                    int root = FindFinalPointerIndex(currentRow, b);
                    if (!lastItemInSet.ContainsKey(root))
                    {
                        lastItemInSet.Add(root, b);
                        if (lastItemInSet.Count == setCounter)
                        {
                            break;
                        }
                    }
                }

                Dictionary<int, int> numConnectionsPerSet = new Dictionary<int, int>(lastItemInSet.Count);
                Dictionary<int, int> oldRootsToNewRoots = new Dictionary<int, int>(lastItemInSet.Count);
                // key is the old set root, value is the new set root
                foreach (var item in lastItemInSet)
                {
                    numConnectionsPerSet.Add(item.Key, 0);
                }
                for (int b = 0; b < currentRow.Length; b++)
                {
                    int set = FindFinalPointerIndex(currentRow, b);
                    if (!(b == lastItemInSet[set] && numConnectionsPerSet[set] == 0))
                    {
                        if (rand.Next(chanceToConnectDown) == 0)
                        {
                            Index tempindex = new Index(b, i);
                            tempindex = ConnectCells(tempindex, 2);
                            numConnectionsPerSet[set] = numConnectionsPerSet[set] + 1;
                            if (oldRootsToNewRoots.ContainsKey(set))
                            {
                                newRow[b] = oldRootsToNewRoots[set];
                            }
                            else
                            {
                                oldRootsToNewRoots.Add(set, b);
                                newRow[b] = b;
                            }
                        }
                        else
                        {
                            newRow[b] = b;
                            setCounter++;
                        }
                    }
                    else
                    {
                        Index tempindex = new Index(b, i);
                        tempindex = ConnectCells(tempindex, 2);
                        numConnectionsPerSet[set]++;
                        if (oldRootsToNewRoots.ContainsKey(set))
                        {
                            newRow[b] = oldRootsToNewRoots[set];
                        }
                        else
                        {
                            oldRootsToNewRoots.Add(set, b);
                            newRow[b] = b;
                        }
                    }

                    // build the list of indexes to highlight, inside of another foorloop so that i doesnt need its own forloop which would reduce performance
                    highlightIndexes.Add(new Index(b, i + 1));
                }
                currentRow = newRow;
                newRow = new int[grid.GetLength(1)];


                updateMazeGraphics();
                Thread.Sleep(generationDelay);
                highlightIndexes.Clear();
            }

            for (int k = 0; k < currentRow.Length - 1; k++)
            {
                int root1 = FindFinalPointerIndex(currentRow, k);
                int root2 = FindFinalPointerIndex(currentRow, k + 1);
                if (root1 != root2)
                {
                    Index tempindex = new Index(k, grid.GetLength(0) - 1);
                    tempindex = ConnectCells(tempindex, 1);
                    currentRow[root1] = root2;
                }
            }
        }
        public void SolveMaze_DFS()
        {
            List<Index> solutionPath = new List<Index>();
            DFS_recursion(ref solutionPath, start, start);
            UpdateHighlitedIndexes(solutionPath);
        }
        private bool DFS_recursion(ref List<Index> list, Index currentPos, Index previousePos)
        {
            if (currentPos.x == end.x && currentPos.y == end.y)
            {
                list.Add(currentPos);
                return true;
            }
            else
            {
                Index nextPos;
                if (!grid[currentPos.y, currentPos.x].wall_N)
                {
                    nextPos = new Index(currentPos.x, currentPos.y - 1);
                    if (!(nextPos.x == previousePos.x && nextPos.y == previousePos.y))
                    {
                        if (DFS_recursion(ref list, nextPos, currentPos))
                        {
                            list.Add(currentPos);
                            return true;
                        }
                    }
                }
                if (!grid[currentPos.y, currentPos.x].wall_E)
                {
                    nextPos = new Index(currentPos.x + 1, currentPos.y);
                    if (!(nextPos.x == previousePos.x && nextPos.y == previousePos.y))
                    {
                        if (DFS_recursion(ref list, nextPos, currentPos))
                        {
                            list.Add(currentPos);
                            return true;
                        }
                    }
                }
                if (!grid[currentPos.y, currentPos.x].wall_S)
                {
                    nextPos = new Index(currentPos.x, currentPos.y + 1);
                    if (!(nextPos.x == previousePos.x && nextPos.y == previousePos.y))
                    {
                        if (DFS_recursion(ref list, nextPos, currentPos))
                        {
                            list.Add(currentPos);
                            return true;
                        }
                    }
                }
                if (!grid[currentPos.y, currentPos.x].wall_W)
                {
                    nextPos = new Index(currentPos.x - 1, currentPos.y);
                    if (!(nextPos.x == previousePos.x && nextPos.y == previousePos.y))
                    {
                        if (DFS_recursion(ref list, nextPos, currentPos))
                        {
                            list.Add(currentPos);
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public override List<Index> SolveMaze_Dijkstra()
        {
            MakeHeatmap();//makes heatmap of the current maze

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
                nextPos = new Index(currentPos.x + 1, currentPos.y);
                if (!grid[currentPos.y, currentPos.x].wall_E && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = new Index(currentPos.x, currentPos.y + 1);
                if (!grid[currentPos.y, currentPos.x].wall_S && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
                nextPos = new Index(currentPos.x - 1, currentPos.y);
                if (!grid[currentPos.y, currentPos.x].wall_W && this.heatmap[nextPos.y, nextPos.x] < currentPosDistance)
                {
                    solutionPath.Add(currentPos);
                    currentPos = nextPos;
                    continue;
                }
            }
            UpdateHighlitedIndexes(solutionPath);
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
                    nextMove = new Index(currentCell.x + 1, currentCell.y);
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
                    nextMove = new Index(currentCell.x - 1, currentCell.y);
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
            heatmap = visitedCells;
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
        private bool ValidIndex(Index pos)
        {
            if (pos.x >= grid.GetLength(1) || pos.y >= grid.GetLength(0) || pos.y < 0 || pos.x < 0)
            {
                return false;
            }
            return true;
        }
        private List<int> FindPossibleDirections(Index pos, bool[,] visitedCells)
        {
            List<int> possibleDirections = new List<int>();
            Index possibleMove;

            possibleMove = new Index(pos.x, pos.y - 1);
            if (ValidIndex(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleDirections.Add(0);
            }

            possibleMove = new Index(pos.x + 1, pos.y);
            if (ValidIndex(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleDirections.Add(1);
            }

            possibleMove = new Index(pos.x, pos.y + 1);
            if (ValidIndex(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleDirections.Add(2);
            }

            possibleMove = new Index(pos.x - 1, pos.y);
            if (ValidIndex(possibleMove) && !visitedCells[possibleMove.y, possibleMove.x])
            {
                possibleDirections.Add(3);
            }

            return possibleDirections;
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
                    return new Index(index.x + 1, index.y);
                case 2://go down
                    return new Index(index.x, index.y + 1);
                case 3://go left
                    return new Index(index.x - 1, index.y);
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
        private int FindFinalPointerIndex(int[] array, int startPos)
        {
            if (startPos != array[startPos])
            {
                int currentPos = startPos;
                int nextPos = array[currentPos];
                while (nextPos != array[nextPos])
                {
                    currentPos = nextPos;
                    nextPos = array[currentPos];
                }
                return nextPos;
            }
            return startPos;
        }
        private void UpdateHighlitedIndexes(List<Index> list)
        {
            lock (highlightIndexes)
            {
                highlightIndexes = list;
            }
        }
        private bool CheckForWall(Index pos, int direction)
        {
            switch (direction)
            {
                case 0:
                    return grid[pos.y, pos.x].wall_N;
                case 1:
                    return grid[pos.y, pos.x].wall_E;
                case 2:
                    return grid[pos.y, pos.x].wall_S;
                case 3:
                    return grid[pos.y, pos.x].wall_W;
                default:
                    throw new Exception("invalid direction");
            }
        }
    }
}
