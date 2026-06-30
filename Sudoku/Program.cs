using System;
using System.Collections.Generic;

namespace Sudoku
{
    internal class SudokuGame
    {
        private int size = -1;
        private int Size
        {
            get
            {
                if (size == -1)
                {
                    throw new Exception("Размерность не установлена");
                }
                return size;
            }
            set => size = value;
        }
        private int BlockCount => Size;
        private int BlockSize => (int)Math.Sqrt(Size);
        private const int CONSTRAINTS = 4;
        private int MatrixRows { get => Size * Size * Size; }
        private int MatrixColumns { get => Size * Size * CONSTRAINTS; }

        private List<int[]> matrix = new List<int[]>();
        private List<int> positionRstsForKnownNmbs = new List<int>();

        public SudokuGame(int[,] board)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);
            if (rows != columns)
                throw new ArgumentException("доска не квадратная!");
            double checkVal = Math.Sqrt(rows);
            if (Math.Floor(checkVal) == checkVal && rows > 1)
                Size = rows;
            else
            {
                throw new Exception("неверная желаемая размерность!");
            }
            BuildExactCoverMatrix(board);
            FillPosRestr(board);
            Solve();
        }

        private void FillPosRestr(int[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] > 0)
                        positionRstsForKnownNmbs.Add(PositionConstraint(row, col));
                }
            }
        }

        private int PositionConstraint(int row, int col) => row * Size + col;
        private int RowConstraint(int row, int num) => (int)Math.Pow(Size, 2) + row * Size + (num - 1);
        private int ColConstraint(int col, int num) => 2 * (int)Math.Pow(Size, 2) + col * Size + (num - 1);
        private int BlockConstraint(int row, int col, int num)
        {
            int blockRow = row / BlockSize;
            int blockCol = col / BlockSize;
            int block = blockRow * BlockSize + blockCol;
            return 3 * (int)Math.Pow(Size, 2) + block * Size + (num - 1);
        }

        private void BuildExactCoverMatrix(int[,] board)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    for (int num = 1; num <= Size; num++)
                    {
                        if (board[row, col] != 0 && board[row, col] != num)
                            continue;

                        int[] restrictionRow = new int[MatrixColumns];

                        restrictionRow[PositionConstraint(row, col)] = 1;
                        restrictionRow[RowConstraint(row, num)] = 1;
                        restrictionRow[ColConstraint(col, num)] = 1;
                        restrictionRow[BlockConstraint(row, col, num)] = 1;

                        matrix.Add(restrictionRow);
                    }
                }
            }
        }

        private void Solve()
        {
            int[,] tempArr = new int[matrix.Count, matrix[0].Length];
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                    tempArr[i, j] = matrix[i][j];
            }

            AlgorythmDancingLinks dancingLink = new AlgorythmDancingLinks(tempArr, MatrixColumns, positionRstsForKnownNmbs.ToArray());
            dancingLink.SearchSolution();
        }

        static void Main(string[] args)
        {
            "Hello world".Print();
            2024.Print();
            0.08.Print();

            Console.WriteLine("\n" + new string('=', 20) + "\n");

            int[,] board = new int[4, 4]
            {
                {0,2,0,0},
                {0,0,3,0},
                {0,0,0,1},
                {4,0,0,0}
            };

            Console.WriteLine("Исходное игровое поле:");
            board.PrintBoard();

            SudokuGame solver = new SudokuGame(board);

            Console.ReadKey();
        }
    }
}
