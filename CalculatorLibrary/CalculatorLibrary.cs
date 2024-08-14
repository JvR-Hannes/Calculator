using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        static List<double> calculationHistory = new List<double>();
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
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculationHistory.Add(result);
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculationHistory.Add(result);
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculationHistory.Add(result);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        calculationHistory.Add(result);
                    }
                    break;
                // Return text for an incorrect option entry.
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

        static void ViewHistory()
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
        static void DeleteFromHistory()
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

    }
}
