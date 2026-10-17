

using System;
using System.Drawing;
using System.Windows.Forms;
using Ргз_ОТУ;

namespace Ргз_ОТУ
{
    public partial class Form1 : Form
    {
        private ComboBox comboScenario;
        private NumericUpDown numGames;
        private Button btnStart;
        private DataGridView table;
        private Label lblResult;

        public Form1()
        {
            InitializeComponent();
            CreateInterface();
        }

        // Создание интерфейса
        private void CreateInterface()
        {
            this.Text = "Имитационная модель - Лабиринт";
            this.Size = new Size(950, 720);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label title = new Label();
            title.Text = "ИМИТАЦИОННАЯ МОДЕЛЬ «ЛАБИРИНТ»";
            title.Font = new Font("Arial", 16, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(220, 20);
            this.Controls.Add(title);

            Label scenarioLabel = new Label();
            scenarioLabel.Text = "Выберите вариант:";
            scenarioLabel.Font = new Font("Arial", 11);
            scenarioLabel.AutoSize = true;
            scenarioLabel.Location = new Point(30, 80);
            this.Controls.Add(scenarioLabel);

            comboScenario = new ComboBox();
            comboScenario.Location = new Point(220, 77);
            comboScenario.Size = new Size(420, 30);
            comboScenario.DropDownStyle = ComboBoxStyle.DropDownList;

            comboScenario.Items.Add("Задание 1 - Всегда Да");
            comboScenario.Items.Add("Задание 1 - Всегда Нет");
            comboScenario.Items.Add("Задание 1 - По алгоритму");
            comboScenario.Items.Add("Задание 2 - По алгоритму с уменьшением ходов");

            comboScenario.SelectedIndex = 0;
            this.Controls.Add(comboScenario);

            Label gamesLabel = new Label();
            gamesLabel.Text = "Количество партий:";
            gamesLabel.Font = new Font("Arial", 11);
            gamesLabel.AutoSize = true;
            gamesLabel.Location = new Point(30, 130);
            this.Controls.Add(gamesLabel);

            numGames = new NumericUpDown();
            numGames.Location = new Point(220, 127);
            numGames.Size = new Size(150, 30);
            numGames.Minimum = 1;
            numGames.Maximum = 1000000;
            numGames.Value = 10000;
            this.Controls.Add(numGames);

            Label scheme = new Label();
            scheme.Text =
                "Схема движения:\n" +
                "Центр ↔ 1 ↔ 2 ↔ 3 ↔ 4 ↔ 5 (выход)";
            scheme.Font = new Font("Arial", 10, FontStyle.Bold);
            scheme.AutoSize = true;
            scheme.Location = new Point(570, 130);
            this.Controls.Add(scheme);

            btnStart = new Button();
            btnStart.Text = "ЗАПУСТИТЬ МОДЕЛИРОВАНИЕ";
            btnStart.Font = new Font("Arial", 11, FontStyle.Bold);
            btnStart.Location = new Point(30, 185);
            btnStart.Size = new Size(350, 45);
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            table = new DataGridView();
            table.Location = new Point(30, 255);
            table.Size = new Size(870, 290);
            table.AllowUserToAddRows = false;
            table.ReadOnly = true;
            table.RowHeadersVisible = false;
            table.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            table.Columns.Add("moves", "Максимум ходов");
            table.Columns.Add("player1", "Победы игрока 1");
            table.Columns.Add("player2", "Победы игрока 2");
            table.Columns.Add("difference", "Разница");

            this.Controls.Add(table);

            lblResult = new Label();
            lblResult.Text = "После запуска здесь появится результат.";
            lblResult.Font = new Font("Arial", 11, FontStyle.Bold);
            lblResult.AutoSize = true;
            lblResult.Location = new Point(30, 565);
            this.Controls.Add(lblResult);
        }

        // Запуск моделирования
        private void BtnStart_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();

            int games = (int)numGames.Value;
            int selectedScenario = comboScenario.SelectedIndex;

            GameSimulator simulator = new GameSimulator();

            // ЗАДАНИЕ 1
            // Три сценария без уменьшения длины партии
            if (selectedScenario <= 2)
            {
                int player1Wins = 0;
                int player2Wins = 0;

                for (int game = 0; game < games; game++)
                {
                    bool player2Won =
                        simulator.PlayGame(25, selectedScenario);

                    if (player2Won)
                    {
                        player2Wins++;
                    }
                    else
                    {
                        player1Wins++;
                    }
                }

                int difference =
                    Math.Abs(player1Wins - player2Wins);

                table.Rows.Add(
                    25,
                    player1Wins,
                    player2Wins,
                    difference
                );

                double percent1 =
                    (double)player1Wins / games * 100;

                double percent2 =
                    (double)player2Wins / games * 100;

                lblResult.Text =
                    "Задание 1. Статистика моделирования:\n" +
                    "Всего партий: " + games + "\n" +
                    "Победы игрока 1: " + player1Wins +
                    " (" + percent1.ToString("F2") + "%)\n" +
                    "Победы игрока 2: " + player2Wins +
                    " (" + percent2.ToString("F2") + "%)";
            }

            // ЗАДАНИЕ 2
            // Алгоритм с уменьшением длины партии
            else
            {
                int bestMoves = 25;
                int bestDifference = games + 1;
                int bestPlayer1 = 0;
                int bestPlayer2 = 0;

                // Сценарий 2 означает
                // заданную последовательность ответов
                int scenario = 2;

                for (int maxMoves = 25;
                     maxMoves >= 1;
                     maxMoves--)
                {
                    int player1Wins = 0;
                    int player2Wins = 0;

                    for (int game = 0; game < games; game++)
                    {
                        bool player2Won =
                            simulator.PlayGame(maxMoves, scenario);

                        if (player2Won)
                        {
                            player2Wins++;
                        }
                        else
                        {
                            player1Wins++;
                        }
                    }

                    int difference =
                        Math.Abs(player1Wins - player2Wins);

                    table.Rows.Add(
                        maxMoves,
                        player1Wins,
                        player2Wins,
                        difference
                    );

                    if (difference < bestDifference)
                    {
                        bestDifference = difference;
                        bestMoves = maxMoves;
                        bestPlayer1 = player1Wins;
                        bestPlayer2 = player2Wins;
                    }
                }

                lblResult.Text =
                    "Задание 2. Наиболее близкое равенство:\n" +
                    "Максимальная длина партии: " +
                    bestMoves + " ходов\n" +
                    "Победы игрока 1: " + bestPlayer1 + "\n" +
                    "Победы игрока 2: " + bestPlayer2 + "\n" +
                    "Разница: " + bestDifference;
            }
        }
    }
}
