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
            Console.WriteLine("2. Perform Trigonometry");
            Console.WriteLine("2. Calculate Ten Power");
            Console.WriteLine("2. Calculate To Power");
            Console.WriteLine("2. Calculate Square Root");
            Console.WriteLine(". View calculation history");
            Console.WriteLine(". Delete a calculation from history");
            Console.WriteLine(". Reuse a calculation from history");
            Console.WriteLine(". Exit");
            Console.Write("Choose an option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    //TODO: Use the Method that performs a calculation
                    break;
                case "2":
                    PerformTrigonometry();
                    break;
                case "3":
                    CalculateTenPowerX();
                    break;
                case "4":
                    CalculatePower();
                    break;
                case "5":
                    CalculateSquareRoot();
                    break;
                case "6":
                    ViewHistory();
                    break;
                case "7":
                    DeleteFromHistory();
                    break;
                case "8":
                    ReuseCalculation();
                    break;
                case "9":
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
                double num1 = Convert.ToDouble(Console.ReadLine());
                double num2 = Convert.ToDouble(Console.ReadLine());
            }
        }
        static void PerformTrigonometry()
        {
            Console.WriteLine("\nTrigonometry Functions:");
            Console.WriteLine("1. Sine (sin)");
            Console.WriteLine("2. Cosine (cos)");
            Console.WriteLine("3. Tangent (tan)");
            Console.Write("Choose a function: ");
            string choice = Console.ReadLine();

            Console.WriteLine("Enter the angle in degrees:");
            double angle = Convert.ToDouble(Console.ReadLine());
            double radians = angle * (Math.PI / 180); 

            double result = 0;
            string function = "";

            switch (choice)
            {
                case "1":
                    result = Math.Sin(radians);
                    function = "sin";
                    break;
                case "2":
                    result = Math.Cos(radians);
                    function = "cos";
                    break;
                case "3":
                    result = Math.Tan(radians);
                    function = "tan";
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            string calculation = $"{function}({angle}°) = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
        static void CalculateTenPowerX()
        {
            Console.WriteLine("\nEnter the exponent (x) for 10^x:");
            double exponent = Convert.ToDouble(Console.ReadLine());

            double result = Math.Pow(10, exponent);

            string calculation = $"10 ^ {exponent} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
        static void CalculatePower()
        {
            Console.WriteLine("\nEnter the base number:");
            double baseNum = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the exponent:");
            double exponent = Convert.ToDouble(Console.ReadLine());

            double result = Math.Pow(baseNum, exponent);

            string calculation = $"{baseNum} ^ {exponent} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
        static void CalculateSquareRoot()
        {
            Console.WriteLine("\nEnter the number to find the square root of:");
            double num = Convert.ToDouble(Console.ReadLine());
            double result = Math.Sqrt(num);

            string calculation = $"√{num} = {result}";
            calculationHistory.Add(calculation);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("Calculation saved to history.");
        }
    }
}