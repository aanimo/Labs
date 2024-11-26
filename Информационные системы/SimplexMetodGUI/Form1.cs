using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimplexMetodGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateInputFields(int variablesCount, int constraintsCount)
        {
            // Очищаем панель перед добавлением новых элементов
            panel1.Controls.Clear();

            panel1.AutoScroll = true;

            // Настройки отступов
            int baseY = 20;  // Отступ от верхней стороны панели
            int paddingX = 120;  // Отступ по ширине между полями ввода
            int paddingY = 45;  // Отступ по высоте между строками ввода

            // Определяем общую ширину для всех полей ввода целевой функции
            int totalWidth = variablesCount * paddingX;

            // Вычисляем базовую X координату для центровки
            int baseX = (panel1.Width - totalWidth) / 2;

            // Метка для целевой функции
            Label objectiveLabel = new Label
            {
                Text = "Целевая функция (макс):",
                Location = new System.Drawing.Point(baseX, baseY),
                Size = new System.Drawing.Size(250, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel1.Controls.Add(objectiveLabel);

            // Поля для ввода коэффициентов целевой функции
            for (int i = 0; i < variablesCount; i++)
            {
                // Поле ввода коэффициента
                TextBox coefficientTextBox = new TextBox
                {
                    Name = $"objectiveCoefficientTextBox{i}",
                    Size = new System.Drawing.Size(50, 20),
                    Location = new System.Drawing.Point(baseX + i * paddingX, baseY + 30)
                };
                panel1.Controls.Add(coefficientTextBox);

                // Метка для переменной
                Label variableLabel = new Label
                {
                    Text = $"x{i + 1}",
                    Location = new System.Drawing.Point(baseX + i * paddingX + 52, baseY + 30),
                    Size = new System.Drawing.Size(35, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel1.Controls.Add(variableLabel);

                // Добавляем "+" между переменными, кроме последней
                if (i < variablesCount - 1)
                {
                    Label plusLabel = new Label
                    {
                        Text = "+",
                        Location = new System.Drawing.Point(baseX + i * paddingX + 87, baseY + 30),
                        Size = new System.Drawing.Size(25, 25),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panel1.Controls.Add(plusLabel);
                }
            }

            // Метка для ограничений
            Label constraintsLabel = new Label
            {
                Text = "Ограничения (≤):",
                Location = new System.Drawing.Point(baseX, baseY + 100),
                Size = new System.Drawing.Size(250, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel1.Controls.Add(constraintsLabel);

            // Создаем поля для ограничений
            for (int i = 0; i < constraintsCount; i++)
            {
                for (int j = 0; j < variablesCount; j++)
                {
                    // Поле для коэффициента ограничения
                    TextBox constraintTextBox = new TextBox
                    {
                        Name = $"constraintCoefficientTextBox{i}{j}",
                        Size = new System.Drawing.Size(50, 20),
                        Location = new System.Drawing.Point(baseX + j * paddingX, baseY + 140 + i * paddingY)
                    };
                    panel1.Controls.Add(constraintTextBox);

                    // Метка для переменной
                    Label constraintVariableLabel = new Label
                    {
                        Text = $"x{j + 1}",
                        Location = new System.Drawing.Point(baseX + j * paddingX + 52, baseY + 140 + i * paddingY),
                        Size = new System.Drawing.Size(35, 25),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panel1.Controls.Add(constraintVariableLabel);

                    // Добавляем "+" между переменными, кроме последней
                    if (j < variablesCount - 1)
                    {
                        Label plusLabel = new Label
                        {
                            Text = "+",
                            Location = new System.Drawing.Point(baseX + j * paddingX + 87, baseY + 140 + i * paddingY),
                            Size = new System.Drawing.Size(25, 25),
                            TextAlign = ContentAlignment.MiddleLeft
                        };
                        panel1.Controls.Add(plusLabel);
                    }
                }

                // Метка для "≤"
                // Метка для "≤"
                Label lessThanLabel = new Label
                {
                    Text = "≤",
                    Location = new System.Drawing.Point(baseX + variablesCount * paddingX - 20, baseY + 140 + i * paddingY), // Уменьшаем отступ
                    Size = new System.Drawing.Size(25, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel1.Controls.Add(lessThanLabel);

                // Поле для правой части ограничения
                TextBox rhsTextBox = new TextBox
                {
                    Name = $"constraintRhsTextBox{i}",
                    Size = new System.Drawing.Size(50, 20),
                    Location = new System.Drawing.Point(baseX + variablesCount * paddingX + 20, baseY + 140 + i * paddingY) // Уменьшен отступ
                };
                panel1.Controls.Add(rhsTextBox);

            }

            // Метка о неотрицательности переменных
            Label nonNegativeLabel = new Label
            {
                Text = "Все переменные неотрицательны (x1, x2, ..., xn ≥ 0)",
                Location = new System.Drawing.Point(baseX, baseY + 100 + constraintsCount * paddingY + 20),
                Size = new System.Drawing.Size(300, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel1.Controls.Add(nonNegativeLabel);
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(varTextBox.Text, out int variablesCount) &&
                int.TryParse(constTextBox.Text, out int constraintsCount))
            {
                // Создаем поля для ввода данных
                CreateInputFields(variablesCount, constraintsCount);
            }
            else
            {
                MessageBox.Show("Введите корректные значения для количества переменных и ограничений.");
            }
        }

        // Метод для парсинга целевой функции
        static double[] ParseObjectiveFunction(string func, int numVariables)
        {
            // Извлечение коэффициентов целевой функции
            var terms = func.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (terms.Length != numVariables)
            {
                throw new ArgumentException($"Ожидалось {numVariables} коэффициентов.");
            }
            double[] coefficients = new double[terms.Length];

            for (int i = 0; i < terms.Length; i++)
            {
                coefficients[i] = double.Parse(terms[i].Trim());
            }

            return coefficients;
        }

        // Метод для создания матрицы ограничений
        static double[,] CreateConstraintMatrix(List<double[]> constraints, int numVariables)
        {
            int numConstraints = constraints.Count;
            double[,] matrix = new double[numConstraints, numVariables + 1];

            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    matrix[i, j] = constraints[i][j];
                }
                matrix[i, numVariables] = constraints[i][numVariables]; // Свободный член
            }

            return matrix;
        }

        // Метод для решения задачи
        // Метод для решения задачи
        static (double, double[]) Solve(double[] coefficients, double[,] constraints, int numVariables, int numConstraints)
        {
            int totalVariables = numVariables + numConstraints;

            // Создание начальной таблицы
            double[,] tableau = new double[numConstraints + 1, totalVariables + 1];

            // Заполнение целевой функции
            for (int j = 0; j < numVariables; j++)
            {
                tableau[0, j] = -coefficients[j]; // Максимизация (поэтому минус)
            }

            // Заполнение ограничений
            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    tableau[i + 1, j] = constraints[i, j];
                }
                tableau[i + 1, totalVariables] = constraints[i, numVariables]; // Свободный член
                tableau[i + 1, numVariables + i] = 1; // Добавление искусственных переменных
            }

            // Основной цикл симплекс-метода
            while (true)
            {
                // Поиск индекса столбца с наибольшим отрицательным коэффициентом в целевой функции
                int pivotColumn = -1;
                double mostNegative = 0;
                for (int j = 0; j < totalVariables; j++)
                {
                    if (tableau[0, j] < mostNegative)
                    {
                        mostNegative = tableau[0, j];
                        pivotColumn = j;
                    }
                }

                // Если нет отрицательных коэффициентов, решение найдено
                if (pivotColumn == -1)
                    break;

                // Поиск индекса строки опорного элемента
                int pivotRow = -1;
                double minRatio = double.MaxValue;
                for (int i = 1; i <= numConstraints; i++)
                {
                    if (tableau[i, pivotColumn] > 0)
                    {
                        double ratio = tableau[i, totalVariables] / tableau[i, pivotColumn];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }

                // Применение Гаусса-Жордана
                double pivot = tableau[pivotRow, pivotColumn];
                for (int j = 0; j <= totalVariables; j++)
                    tableau[pivotRow, j] /= pivot;

                for (int i = 0; i <= numConstraints; i++)
                {
                    if (i != pivotRow)
                    {
                        double factor = tableau[i, pivotColumn];
                        for (int j = 0; j <= totalVariables; j++)
                            tableau[i, j] -= factor * tableau[pivotRow, j];
                    }
                }
            }

            // Считывание оптимального решения
            double[] solution = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                solution[j] = 0;
                for (int i = 1; i <= numConstraints; i++)
                {
                    if (tableau[i, j] == 1) // Базисная переменная
                    {
                        solution[j] = tableau[i, totalVariables];
                        break;
                    }
                }
            }

            // Оптимальное значение целевой функции
            double optimalValue = Math.Abs(-tableau[0, totalVariables]); // Взятие модуля

            return (optimalValue, solution);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Считываем коэффициенты целевой функции
                double[] coefficients = new double[int.Parse(varTextBox.Text)];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    var inputBox = panel1.Controls[$"objectiveCoefficientTextBox{i}"] as TextBox;
                    coefficients[i] = double.Parse(inputBox.Text);
                }

                // Считываем ограничения
                int constraintsCount = int.Parse(constTextBox.Text);
                List<double[]> constraints = new List<double[]>();
                for (int i = 0; i < constraintsCount; i++)
                {
                    double[] constraint = new double[coefficients.Length + 1];
                    for (int j = 0; j < coefficients.Length; j++)
                    {
                        var inputBox = panel1.Controls[$"constraintCoefficientTextBox{i}{j}"] as TextBox;
                        constraint[j] = double.Parse(inputBox.Text);
                    }
                    var rhsBox = panel1.Controls[$"constraintRhsTextBox{i}"] as TextBox;
                    constraint[constraint.Length - 1] = double.Parse(rhsBox.Text);
                    constraints.Add(constraint);
                }

                // Преобразование списка ограничений в матрицу
                var constraintMatrix = CreateConstraintMatrix(constraints, coefficients.Length);

                // Решение задачи
                var (optimalValue, solution) = Solve(coefficients, constraintMatrix, coefficients.Length, constraintsCount);

                // Вывод результата
                resultLabel.Text = $"Оптимальное значение: {optimalValue}\n";
                for (int i = 0; i < solution.Length; i++)
                {
                    resultLabel.Text += $"x{i + 1} = {solution[i]}\n"; // Выводим результат
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении: " + ex.Message);
            }
        }

    }
}
