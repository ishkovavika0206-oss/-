namespace Ргз_ОТУ
{
    // Класс отвечает за проведение одной партии
    public class GameSimulator
    {
        private Player1 player1;

        public GameSimulator()
        {
            player1 = new Player1();
        }

        // Метод проводит одну партию.
        // true  - выиграл игрок 2
        // false - выиграл игрок 1

        public bool PlayGame(int maxMoves, int scenario)
        {
            // Положение игрока:
            // 0 - центр
            
            // 1 - кольцо:
            // 7, 12, 16, 17, 14
            
            // 2 - кольцо:
            // 6, 15, 22, 18, 8
            
            // 3 - кольцо:
            // 3, 10, 21, 23, 11
            
            // 4 - кольцо:
            // 4, 2, 20, 25, 19
            
            // 5 - выходы:
            // 1, 5, 24, 26, 9

            int position = 0;


            // Создаем второго игрока с выбранным сценарием

            Player2 player2 =
                new Player2(scenario);


            // Выполняем ходы
            for (int move = 0;
                 move < maxMoves;
                 move++)
            {

                // ХОД ПЕРВОГО ИГРОКА

                // Первый игрок случайно выбирает:
                // true  - "от центра"
                // false - "к центру"

                bool hintFromCenter =
                    player1.GetHint();

                // ХОД ВТОРОГО ИГРОКА
                // Второй игрок отвечает согласно выбранному сценарию.

                bool answerYes =
                    player2.GetAnswer(move);

                // ДВИЖЕНИЕ
                // Если сейчас находимся в центре

                if (position == 0)
                {
                    // Центр -> кольцо 1
                    position = 1;
                }

                // Если находимся на кольце
                else
                {
                    bool moveFromCenter;


                    // Если второй игрок отвечает "Да", он следует подсказке первого игрока.
                    if (answerYes)
                    {
                        moveFromCenter =
                            hintFromCenter;
                    }
                    // Если второй игрок отвечает "Нет", он делает наоборот.
                    else
                    {
                        moveFromCenter =
                            !hintFromCenter;
                    }

                    // Если движение "от центра"
                    if (moveFromCenter)
                    {
                        position++;
                    }

                    // Если движение "к центру"
                    else
                    {
                        position--;
                    }
                }

                // ПРОВЕРКА ПОБЕДЫ

                if (position == 5)
                {
                    return true;
                }
            }

            // Если цикл закончился, значит максимальное количество ходов было использовано,но выход не найден.
            // Выиграл игрок 1.

            return false;
        }
    }
}
