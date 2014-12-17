namespace BattleField
{
    using System;

    class GameEngine
    {
        public static void Run()
        {
            int fieldSize = 10;

            Console.Write("Welcome to \"Battle Field\" game.\nEnter battle field size: fieldSize = ");

            int.TryParse(Console.ReadLine(), out fieldSize);

            while (fieldSize < 1 || fieldSize > 10)
            {
                Console.Write("fieldSize is between 1 and 10! Please enter new fieldSize = ");
                int.TryParse(Console.ReadLine(), out fieldSize);
            }

            int[,] gameFieldArr = new int[fieldSize, fieldSize];
            Random randomMine = new Random();
            int minesCount = randomMine.Next(15 * fieldSize * fieldSize / 100, 30 * fieldSize * fieldSize / 100 + 1);

            for (int mineIndex = 0; mineIndex < minesCount; mineIndex++)
            {
                int minePositionX = randomMine.Next(0, fieldSize);
                int minePositionY = randomMine.Next(0, fieldSize);

                while (gameFieldArr[minePositionX, minePositionY] != 0)
                {
                    minePositionX = randomMine.Next(0, fieldSize);
                    minePositionY = randomMine.Next(0, fieldSize);
                }

                gameFieldArr[minePositionX, minePositionY] = randomMine.Next(1, 6);
            }

            GameFunction.DrawGameField(gameFieldArr, fieldSize);

            int counter = 0;

            while (minesCount > 0)
            {
                int tmp = GameFunction.ReadPlayerInput(gameFieldArr, fieldSize);
                minesCount -= tmp;
                GameFunction.DrawGameField(gameFieldArr, fieldSize);

                Console.WriteLine("Mines Blowed this round: {0}", tmp);

                counter++;
            }

            Console.WriteLine("Player Turns：{0}", counter);
        }
    }
}
