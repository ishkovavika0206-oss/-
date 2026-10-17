using System;

namespace Ргз_ОТУ
{
    // Первый игрок дает случайную подсказку: "к центру" или "от центра"
    public class Player1
    {
        private Random random;

        public Player1()
        {
            random = new Random();
        }

        // true  - от центра
        // false - к центру
        public bool GetHint()
        {
            return random.Next(2) == 0;
        }
    }
}