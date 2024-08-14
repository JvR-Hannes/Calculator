using System.Text.RegularExpressions;
using CalculatorLibrary;
using System;
using System.IO;
using System.ComponentModel.Design;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string usageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usageCount.txt");
            IncrementUsageCount(usageFilePath);
            int usageCount = GetUsageCount(usageFilePath);

            bool endApp = false;

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Calculator.ShowMenu();
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                // TODO: Insert a Method here to call history
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        private static void IncrementUsageCount(string filePath)
        {
            int count = GetUsageCount(filePath);
            count++;
            File.WriteAllText(filePath, count.ToString());
        }

        private static int GetUsageCount(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            string countText = File.ReadAllText(filePath);
            if (int.TryParse(countText,out int count))
            {
                return count;
            }

            return 0;
        }
    }
}