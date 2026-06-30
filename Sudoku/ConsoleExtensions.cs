using System;

namespace Sudoku
{
    public static class ConsoleExtensions
    {
        public static void Print(this object? obj)
        {
            Console.WriteLine(obj?.ToString() ?? "null");
        }

        public static void PrintBoard(this int[,] board)
        {
            int size = board.GetLength(0);
            int blockSize = (int)Math.Sqrt(size);

            for (int i = 0; i < size; i++)
            {
                if (i % blockSize == 0 && i != 0)
                {
                    Console.WriteLine(new string('-', size * 2 + blockSize));
                }

                for (int j = 0; j < size; j++)
                {
                    if (j % blockSize == 0 && j != 0) Console.Write("| ");
                    Console.Write(board[i, j] == 0 ? ". " : $"{board[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}