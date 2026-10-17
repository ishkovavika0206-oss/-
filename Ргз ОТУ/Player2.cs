namespace Ргз_ОТУ
{
    // Второй игрок отвечает на подсказку первого игрока: "Да" или "Нет"
    public class Player2
    {
        private int scenario;

        // scenario:
        // 0 - всегда "Да"
        // 1 - всегда "Нет"
        // 2 - заданная последовательность
        public Player2(int scenario)
        {
            this.scenario = scenario;
        }

        public bool GetAnswer(int moveNumber)
        {
            // Сценарий 1: всегда "Да"
            if (scenario == 0)
            {
                return true;
            }
            // Сценарий 2: всегда "Нет"
            if (scenario == 1)
            {
                return false;
            }
            // Сценарий 3: заданная последовательность
            bool[] sequence =
            {
                // 5 раз "Да"
                true, true, true, true, true,

                // 6 раз "Нет"
                false, false, false,
                false, false, false,

                // 2 раза "Да"
                true, true,

                // 4 раза "Нет"
                false, false, false, false,

                // 4 раза "Да"
                true, true, true, true,

                // 1 раз "Нет"
                false,

                // 1 раз "Да"
                true,

                // 2 раза "Нет"
                false, false
            };


            // Всего в последовательности 25 ответов.
            // moveNumber начинается с 0:
            // 0 - первый ход,
            // 1 - второй ход и т.д.

            return sequence[moveNumber % sequence.Length];
        }
    }
}