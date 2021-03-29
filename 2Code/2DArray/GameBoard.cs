using System;

class GameBoard
{
    static char[,] board;

    public static void SetupBoard()
    {
        board = new char[,] {
            { 'O', 'X', 'X', },
            { 'O', 'X', 'O', },
            { 'X', 'X', 'O', },
        };
    }

    public static void PrintBoard()
    {
		// print the board contents using Console.Write()
		// and Console.WriteLine()
        // ...
    }

    static void CheckWinners()
    {
        int num = 0;

        num = Matrix.CountRow(board, 0);
        if (num == 3)
            Console.WriteLine("X wins in row 1 on board");

        num = Matrix.CountMainDiagonal(board);
        if (num == 3)
            Console.WriteLine("X wins on main diagonal");

        num = Matrix.CountSecondDiagonal(board);
        if (num == 3)
            Console.WriteLine("X wins on second diagonal");
    }

    static void Main(string[] args)
    {
        SetupBoard();
        PrintBoard();
        CheckWinners();

        Console.WriteLine("Press Enter to Quit");
        Console.ReadLine();
    }
}

