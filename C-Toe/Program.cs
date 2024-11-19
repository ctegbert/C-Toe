using System;
using System.IO;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToe game = new TicTacToe();
            game.StartGame();
        }
    }

    // Abstract class for player
    abstract class Player
    {
        public abstract char Marker { get; }
        public abstract string Name { get; }
    }

    // Derived classes for Player X and Player O
    class PlayerX : Player
    {
        public override char Marker => 'X';
        public override string Name => "Player X";
    }

    class PlayerO : Player
    {
        public override char Marker => 'O';
        public override string Name => "Player O";
    }

    // Main game class
    class TicTacToe
    {
        private char[,] board = new char[3, 3];
        private Player player1 = new PlayerX();
        private Player player2 = new PlayerO();

        public void StartGame()
        {
            InitializeBoard();
            Player currentPlayer = player1;
            bool isGameRunning = true;

            while (isGameRunning)
            {
                DisplayBoard();
                Console.WriteLine($"{currentPlayer.Name}'s turn. Enter row and column (1-3):");

                int row = GetInput() - 1;
                int col = GetInput() - 1;

                if (IsValidMove(row, col))
                {
                    board[row, col] = currentPlayer.Marker;
                    if (CheckWin(currentPlayer.Marker))
                    {
                        DisplayBoard();
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        isGameRunning = false;
                    }
                    else if (IsBoardFull())
                    {
                        DisplayBoard();
                        Console.WriteLine("It's a draw!");
                        isGameRunning = false;
                    }
                    else
                    {
                        currentPlayer = currentPlayer == player1 ? player2 : player1;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        private void DisplayBoard()
        {
            Console.WriteLine("  1 2 3");
            for (int i = 0; i < 3; i++)
            {
                Console.Write((i + 1) + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("  -----");
            }
        }

        private int GetInput()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 3)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 3:");
            }
            return input;
        }

        private bool IsValidMove(int row, int col)
        {
            return board[row, col] == ' ';
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == ' ') return false;
            }
            return true;
        }

        private bool CheckWin(char marker)
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == marker && board[i, 1] == marker && board[i, 2] == marker) ||
                    (board[0, i] == marker && board[1, i] == marker && board[2, i] == marker))
                {
                    return true;
                }
            }
            // Check diagonals
            return (board[0, 0] == marker && board[1, 1] == marker && board[2, 2] == marker) ||
                   (board[0, 2] == marker && board[1, 1] == marker && board[2, 0] == marker);
        }
    }
}
