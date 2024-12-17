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
            panel1.Controls.Clear();

            panel1.AutoScroll = true;

            int baseY = 20;
            int paddingX = 120;
            int paddingY = 45;

            int totalWidth = variablesCount * paddingX;

            int baseX = (panel1.Width - totalWidth) / 2;

            Label objectiveLabel = new Label
            {
                Text = "Целевая функция (макс):",
                Location = new System.Drawing.Point(baseX, baseY),
                Size = new System.Drawing.Size(250, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel1.Controls.Add(objectiveLabel);

            for (int i = 0; i < variablesCount; i++)
            {
                TextBox coefficientTextBox = new TextBox
                {
                    Name = $"objectiveCoefficientTextBox{i}",
                    Size = new System.Drawing.Size(50, 20),
                    Location = new System.Drawing.Point(baseX + i * paddingX, baseY + 30)
                };
                panel1.Controls.Add(coefficientTextBox);

                Label variableLabel = new Label
                {
                    Text = $"x{i + 1}",
                    Location = new System.Drawing.Point(baseX + i * paddingX + 52, baseY + 30),
                    Size = new System.Drawing.Size(35, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel1.Controls.Add(variableLabel);

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

            Label constraintsLabel = new Label
            {
                Text = "Ограничения (≤):",
                Location = new System.Drawing.Point(baseX, baseY + 100),
                Size = new System.Drawing.Size(250, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel1.Controls.Add(constraintsLabel);

            for (int i = 0; i < constraintsCount; i++)
            {
                for (int j = 0; j < variablesCount; j++)
                {
                    TextBox constraintTextBox = new TextBox
                    {
                        Name = $"constraintCoefficientTextBox{i}{j}",
                        Size = new System.Drawing.Size(50, 20),
                        Location = new System.Drawing.Point(baseX + j * paddingX, baseY + 140 + i * paddingY)
                    };
                    panel1.Controls.Add(constraintTextBox);

                    Label constraintVariableLabel = new Label
                    {
                        Text = $"x{j + 1}",
                        Location = new System.Drawing.Point(baseX + j * paddingX + 52, baseY + 140 + i * paddingY),
                        Size = new System.Drawing.Size(35, 25),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    panel1.Controls.Add(constraintVariableLabel);

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

                Label lessThanLabel = new Label
                {
                    Text = "≤",
                    Location = new System.Drawing.Point(baseX + variablesCount * paddingX - 20, baseY + 140 + i * paddingY), // Уменьшаем отступ
                    Size = new System.Drawing.Size(25, 25),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel1.Controls.Add(lessThanLabel);

                TextBox rhsTextBox = new TextBox
                {
                    Name = $"constraintRhsTextBox{i}",
                    Size = new System.Drawing.Size(50, 20),
                    Location = new System.Drawing.Point(baseX + variablesCount * paddingX + 20, baseY + 140 + i * paddingY) // Уменьшен отступ
                };
                panel1.Controls.Add(rhsTextBox);

            }

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
                CreateInputFields(variablesCount, constraintsCount);
            }
            else
            {
                MessageBox.Show("Введите корректные значения для количества переменных и ограничений.");
            }
        }

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
                matrix[i, numVariables] = constraints[i][numVariables];
            }

            return matrix;
        }

        static (double, double[]) Solve(double[] coefficients, double[,] constraints, int numVariables, int numConstraints)
        {
            int totalVariables = numVariables + numConstraints;

            double[,] tableau = new double[numConstraints + 1, totalVariables + 1];

            for (int j = 0; j < numVariables; j++)
            {
                tableau[0, j] = -coefficients[j];
            }

            for (int i = 0; i < numConstraints; i++)
            {
                for (int j = 0; j < numVariables; j++)
                {
                    tableau[i + 1, j] = constraints[i, j];
                }
                tableau[i + 1, totalVariables] = constraints[i, numVariables];
                tableau[i + 1, numVariables + i] = 1;
            }

            // Основной цикл симплекс-метода
            while (true)
            {
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

                if (pivotColumn == -1)
                    break;

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

            double[] solution = new double[numVariables];
            for (int j = 0; j < numVariables; j++)
            {
                solution[j] = 0;
                for (int i = 1; i <= numConstraints; i++)
                {
                    if (tableau[i, j] == 1)
                    {
                        solution[j] = tableau[i, totalVariables];
                        break;
                    }
                }
            }

            double optimalValue = Math.Abs(-tableau[0, totalVariables]);

            return (optimalValue, solution);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                double[] coefficients = new double[int.Parse(varTextBox.Text)];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    var inputBox = panel1.Controls[$"objectiveCoefficientTextBox{i}"] as TextBox;
                    coefficients[i] = double.Parse(inputBox.Text);
                }

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

                var constraintMatrix = CreateConstraintMatrix(constraints, coefficients.Length);

                var (optimalValue, solution) = Solve(coefficients, constraintMatrix, coefficients.Length, constraintsCount);

                resultLabel.Text = $"Оптимальное значение: {optimalValue}\n";
                for (int i = 0; i < solution.Length; i++)
                {
                    resultLabel.Text += $"x{i + 1} = {solution[i]}\n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении: " + ex.Message);
            }
        }

    }
}
