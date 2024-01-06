namespace Conway.Core
{
    /// <summary>
    /// Implements Game of Life simulator
    /// </summary>
    public static class GameOfLife
    {
        /// <summary>
        /// Given a board, calculate the state it will have after a number of ticks
        /// </summary>
        /// <param name="board"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<bool>> CalculateState(IEnumerable<IEnumerable<bool>>? board, int tick)
        {
            if (board is null)
            {
                throw new ArgumentNullException("Board cannot be null");
            }
            if (tick <= 0)
            {
                throw new Exception("Tick must be greater than zero");
            }

            for (int i = 0; i < tick; i++)
            {
                board = UpdateBoard(board);
            }

            return board;
        }

        private static IEnumerable<IEnumerable<bool>> UpdateBoard(IEnumerable<IEnumerable<bool>> board)
        {
            int rows = board.Count();
            int cols = board.First().Count();

            var newBoard = new List<List<bool>>();

            for (int i = 0; i < rows; i++)
            {
                var newRow = new List<bool>();

                for (int j = 0; j < cols; j++)
                {
                    int neighbors = CountNeighbors(board, i, j);

                    if (board.ElementAt(i).ElementAt(j))
                    {
                        // Cell is alive
                        if (neighbors == 2 || neighbors == 3)
                        {
                            newRow.Add(true);
                        }
                        else
                        {
                            newRow.Add(false);
                        }
                    }
                    else
                    {
                        // Cell is dead
                        if (neighbors == 3)
                        {
                            newRow.Add(true);
                        }
                        else
                        {
                            newRow.Add(false);
                        }
                    }
                }

                newBoard.Add(newRow);
            }

            return newBoard;
        }

        private static int CountNeighbors(IEnumerable<IEnumerable<bool>> board, int row, int col)
        {
            int count = 0;
            int rows = board.Count();
            int cols = board.First().Count();

            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int k = 0; k < 8; k++)
            {
                int r = row + dr[k];
                int c = col + dc[k];

                if (r >= 0 && r < rows && c >= 0 && c < cols && board.ElementAt(r).ElementAt(c))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
