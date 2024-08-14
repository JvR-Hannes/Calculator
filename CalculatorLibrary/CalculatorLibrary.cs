using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        static List<string> calculationHistory = new List<string>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculationHistory.Add(result.ToString());
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculationHistory.Add(result.ToString());
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculationHistory.Add(result.ToString());
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        calculationHistory.Add(result.ToString());
                    }
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public static void ViewHistory()
        {
            if (calculationHistory.Count == 0)
            {
                Console.WriteLine("No calculations in history");
                return;
            }

            Console.WriteLine("\nCalculation History:");
            for (int i = 0; i < calculationHistory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {calculationHistory[i]}");
            }
        }
        public static void DeleteFromHistory()
        {
            ViewHistory();

            if (calculationHistory.Count == 0)
                return;

            Console.Write("\nEnter the number of the calculation to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= calculationHistory.Count)
            {
                calculationHistory.RemoveAt(index - 1);
                Console.WriteLine("Calculation deleted.");
            }
            else
            {
                Console.WriteLine("Invalid input. No calculation deleted.");
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Console.WriteLine("Calculator Menu:");
            Console.WriteLine("1. Perform a new calculation");
            Console.WriteLine("2. View calculation history");
            Console.WriteLine("3. Delete a calculation from history");
            Console.WriteLine("4. Reuse a calculation from history");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    //TODO: Use the Method that performs a calculation
                    break;
                case "2":
                    ViewHistory();
                    break;
                case "3":
                    DeleteFromHistory();
                    break;
                case "4":
                    ReuseCalculation();
                    break;
                case "5":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }
        private static void ReuseCalculation()
        {
            ViewHistory();

            if (calculationHistory.Count == 0)
                return;

            Console.Write("\nEnter the number of the calculation to reuse: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= calculationHistory.Count)
            {
                string selectedCalculation = calculationHistory[index - 1];
                Console.WriteLine($"Reusing: {selectedCalculation}");

                double result = double.Parse(selectedCalculation.Split('=')[1].Trim());

                Console.WriteLine("Enter an operator (+, -, *, /) to apply on the previous result:");
                string? op = Console.ReadLine();

                Console.WriteLine("Enter the number to use in the new calculation:");
                double num2 = Convert.ToDouble(Console.ReadLine());
            }
        }
    }
}