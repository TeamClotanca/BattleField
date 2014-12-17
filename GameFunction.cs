namespace BattleField
{
    using System;
    using System.Linq;

    public class GameFunction
    {
        public static void DrawGameField(int[,] fieldArr, int fieldSize)
        {
            Console.Write(" ");

            for (int rowsIndex = 0; rowsIndex < fieldSize; rowsIndex++)
            {
                Console.Write(" {0}", rowsIndex);
            }

            Console.WriteLine();
            Console.Write("  ");

            for (int rowsUnderline = 0; rowsUnderline < fieldSize*2; rowsUnderline++)
            {
                Console.Write("-");
            }

            Console.WriteLine();

            for (int fieldRow = 0; fieldRow < fieldSize; fieldRow++)
            {
                Console.Write("{0}|", fieldRow);
                for (int fieldCol = 0; fieldCol < fieldSize; fieldCol++)
                {
                    char fieldElement;
                    switch (fieldArr[fieldRow, fieldCol])
                    {
                        case 0:
                            fieldElement = '-';
                            break;
                        case -1:
                            fieldElement = 'X';
                            break;
                        default:
                            fieldElement = (char) ('0' + fieldArr[fieldRow, fieldCol]);
                            break;
                    }
                    Console.Write("{0} ", fieldElement);
                }
                Console.WriteLine();
            }
        }

        public static int Explosion(int[,] playerInputArr, int fieldSize, int playerInputX, int playerInputY)
        {
            int[,] mineType;
            switch (playerInputArr[playerInputX, playerInputY])
            {
                case 1:
                    mineType = MineTypes.firstMine;
                    break;
                case 2:
                    mineType = MineTypes.secondMine;
                    break;
                case 3:
                    mineType = MineTypes.thirdMine;
                    break;
                case 4:
                    mineType = MineTypes.fourthMine;
                    break;
                default:
                    mineType = MineTypes.fivethMine;
                    break;
            }

            int explosionRange = 0;

            for (int fieldRow = -2; fieldRow < 3; fieldRow++)
            {
                for (int fieldCol = -2; fieldCol < 3; fieldCol++)
                {
                    if (playerInputX + fieldRow >= 0 && playerInputX + fieldRow < fieldSize &&
                        playerInputY + fieldCol >= 0 && playerInputY + fieldCol < fieldSize)
                    {
                        if (mineType[fieldRow + 2, fieldCol + 2] == 1)
                        {
                            if (playerInputArr[playerInputX + fieldRow, playerInputY + fieldCol] > 0)
                            {
                                explosionRange++;
                            }

                            playerInputArr[playerInputX + fieldRow, playerInputY + fieldCol] = -1;
                        }
                    }
                }
            }

            return explosionRange;
        }

        public static int ReadPlayerInput(int[,] gameField, int fieldSize)
        {
            int playerInputX = 0;
            int playerInputY = 0;
            bool isCorrectInput = true;

            while (isCorrectInput)
            {
                Console.Write("Please enter coordinates: ");
                string playerInput = Console.ReadLine();

                if (playerInput.Length > 2)
                {
                    playerInputX = playerInput.ElementAt(0) - '0';
                    playerInputY = playerInput.ElementAt(2) - '0';

                    if (playerInputX < 0 || playerInputX > 9 || playerInputY < 0 ||
                        playerInputY > 9 || playerInput.ElementAt(1) != ' ')
                    {
                        Console.WriteLine("Invalid move!");
                    }
                    else
                    {
                        if (playerInput.Length > 3)
                        {
                            if (playerInput.ElementAt(3) != ' ')
                                Console.WriteLine("Invalid move!");
                            else isCorrectInput = false;
                        }
                        else isCorrectInput = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }

                if (isCorrectInput == false)
                {
                    if (gameField[playerInputX, playerInputY] <= 0)
                    {
                        isCorrectInput = true;
                        Console.WriteLine("Invalid move!");
                    }
                }
            }

            return Explosion(gameField, fieldSize, playerInputX, playerInputY);
        }
    }
}
