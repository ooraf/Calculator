using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private string input = string.Empty;
        private string operand1 = string.Empty;
        private string operand2 = string.Empty;
        private char operation;
        private double result = 0.0;
        private Button activeOperatorButton = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            input += button.Content.ToString();
            ResultTextBox.Text = input;
            if (activeOperatorButton != null)
            {
                activeOperatorButton.ClearValue(Button.TagProperty);
                activeOperatorButton = null;
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            operand1 = input;
            operation = Convert.ToChar(button.Content.ToString().Replace("×", "*").Replace("÷", "/").Replace("AC", "+/-"));
            input = string.Empty;

            // Zurücksetzen der vorher aktiven Schaltfläche
            if (activeOperatorButton != null)
            {
                activeOperatorButton.ClearValue(Button.TagProperty);
            }

            // Setzen der aktiven Schaltfläche
            button.Tag = "Active";
            activeOperatorButton = button;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            operand2 = input;
            double num1, num2;
            double.TryParse(operand1, out num1);
            double.TryParse(operand2, out num2);

            switch (operation)
            {
                case '+':
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    if (num2 != 0)
                        result = num1 / num2;
                    else
                        MessageBox.Show("Division durch 0 nicht möglich");
                    break;
            }
            ResultTextBox.Text = result.ToString();
            input = string.Empty;
            operand1 = string.Empty;
            operand2 = string.Empty;
            if (activeOperatorButton != null)
            {
                activeOperatorButton.ClearValue(Button.TagProperty);
                activeOperatorButton = null;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            input = string.Empty;
            operand1 = string.Empty;
            operand2 = string.Empty;
            ResultTextBox.Text = "0";
            if (activeOperatorButton != null)
            {
                activeOperatorButton.ClearValue(Button.TagProperty);
                activeOperatorButton = null;
            }
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("-"))
                    input = input.Substring(1);
                else
                    input = "-" + input;
                ResultTextBox.Text = input;
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(input, out double number))
            {
                number /= 100;
                input = number.ToString();
                ResultTextBox.Text = input;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                // Zahlen-Keys (oberer Teil der Tastatur)
                NumberButton_Click(GetButtonByContent(e.Key - Key.D0), null);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                // Zahlen-Keys (Numpad)
                NumberButton_Click(GetButtonByContent(e.Key - Key.NumPad0), null);
            }
            else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                // Dezimalpunkt
                NumberButton_Click(FindName("DecimalButton") as Button, null);
            }
            else if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                // Addition
                OperatorButton_Click(FindName("PlusButton") as Button, null);
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                // Subtraktion
                OperatorButton_Click(FindName("MinusButton") as Button, null);
            }
            else if (e.Key == Key.Multiply)
            {
                // Multiplikation
                OperatorButton_Click(FindName("MultiplyButton") as Button, null);
            }
            else if (e.Key == Key.Divide)
            {
                // Division
                OperatorButton_Click(FindName("DivideButton") as Button, null);
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                // Gleichheitszeichen
                EqualsButton_Click(FindName("EqualsButton") as Button, null);
            }
            else if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                // Löschen
                ClearButton_Click(FindName("ClearButton") as Button, null);
            }
        }


        private Button GetButtonByContent(int number)
        {
            return FindName($"Button{number}") as Button;
        }
    }
}
