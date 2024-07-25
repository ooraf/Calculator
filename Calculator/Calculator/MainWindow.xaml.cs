using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string input = string.Empty;
        private string operand1 = string.Empty;
        private string operand2 = string.Empty;
        private char operation;
        private double result = 0.0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            input += button.Content.ToString();
            ResultTextBox.Text = input;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            operand1 = input;
            operation = Convert.ToChar(button.Content.ToString().Replace("×", "*").Replace("÷", "/"));
            input = string.Empty;
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
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            input = string.Empty;
            operand1 = string.Empty;
            operand2 = string.Empty;
            ResultTextBox.Text = "0";
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
    }
}

