using System;

namespace TicTacToe
{
    class Program
    {
        const string X = "X";
        const string O = "O";
        const string Empty = " ";

        static void Main(string[] args)
        {
            string[,] board = InitializeBoard();
            GamePlay(board);
        }

        static string[,] InitializeBoard()
        {
            return new string[3, 3]
            {
                { "1", "2", "3" },
                { "4", "5", "6" },
                { "7", "8", "9" }
            };
        }

        static void GamePlay(string[,] board)
        {
            int size = board.GetLength(0);
            bool gameOver = false;

            for (int i = 1; i <= size * size && !gameOver; i++)
            {
                Console.Clear();
                PrintBoard(board);
                int player = i % 2 == 0 ? 2 : 1;
                Console.WriteLine($"Player {player}'s Turn: Choose Field: ");
                gameOver = HandleInput(board, player);
            }
        }

        static void PrintBoard(string[,] board)
        {
            Console.WriteLine("-------------");
            for (int row = 0; row < 3; row++)
            {
                Console.Write("| ");
                for (int col = 0; col < 3; col++)
                {
                    Console.Write($"{board[row, col]} | ");
                }
                Console.WriteLine("\n-------------");
            }
        }

        static bool HandleInput(string[,] board, int player)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int fieldNumber))
            {
                if (fieldNumber >= 1 && fieldNumber <= 9)
                {
                    int row = (fieldNumber - 1) / 3;
                    int col = (fieldNumber - 1) % 3;
                    string cellValue = board[row, col];

                    if (cellValue != X && cellValue != O)
                    {
                        string value = player == 1 ? O : X;
                        board[row, col] = value;
                        return CheckWinner(board);
                    }
                    else
                    {
                        Console.WriteLine("Field already Taken! Choose another one.");
                    }
                }
                else
                {
                    Console.WriteLine("Enter a number between 1 and 9!");
                }
            }
            else
            {
                Console.WriteLine("Enter valid input!");
            }
            return false;
        }


        // Checks if there is a winner
        static bool CheckWinner(string[,] board)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                bool rowWin = true;
                bool colWin = true;

                for (int j = 1; j < size; j++)
                {
                    if (board[i, j] != board[i, j - 1] || board[i, j] == Empty)
                    {
                        rowWin = false;
                    }

                    if (board[j, i] != board[j - 1, i] || board[j, i] == Empty)
                    {
                        colWin = false;
                    }
                }

                if (rowWin || colWin)
                {
                    return true;
                }
            }

            bool mainDiagonalWin = true;
            bool antiDiagonalWin = true;

            for (int i = 1; i < size; i++)
            {
                if (board[i, i] != board[i - 1, i - 1] || board[i, i] == Empty)
                {
                    mainDiagonalWin = false;
                }

                if (board[i, size - i - 1] != board[i - 1, size - i] || board[i, size - i - 1] == Empty)
                {
                    antiDiagonalWin = false;
                }
            }

            return mainDiagonalWin || antiDiagonalWin;
        }
    }
}
