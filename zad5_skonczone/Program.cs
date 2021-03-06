//Zadanie 5 - C# - MS VISUAL STUDIO 2017
using System;

namespace zad5
{
    class Program
    {
        private static bool Exit  = true;
        static void Main(string[] args)
        {
            do
            {
                double startAngle;
                ConsoleDoubleInput("kąta początkowego", out startAngle);
                
                double endAngle;
                ConsoleDoubleInput("kąta końcowego", out endAngle);
                
                double density;
                ConsoleDoubleInput("gęstości podziału", out density);

                string input = ChooseFunctionConsoleInput();

                ChooseFunction(input, startAngle, endAngle, density);

                ExitGame();
            }
            while(Exit);
            
        }

        // Funkcja a)
        private static void GetCurveLengthA(double startAngle, double endAngle, double density)
        {
            double output = 0;
            double angle = endAngle - startAngle;
            double angleStep = angle / density;

            for (double i = startAngle; i < endAngle - angleStep; i += angleStep)
            {
                double littleAngle = Math.Cos(angleStep * (Math.PI) / 180);
                int a = 2;
                int b = 2;

                double d = Math.Pow(a, 2) + Math.Pow(b, 2) - ( 2 * a * b  * littleAngle);
                output += Math.Sqrt(d);
            }
            Console.WriteLine(output);
            
        }

        // Funkcja b)
        private static void GetCurveLengthB(double startAngle, double endAngle, double density)
        {
            double output = 0;
            double angle = endAngle - startAngle;
            double angleStep = angle / density;

            for (double i = startAngle; i < endAngle - angleStep; i += angleStep)
            {
                double littleAngle = Math.Cos(angleStep * (Math.PI) / 180);
                double a = 2 * (1 - Math.Sin(i* (Math.PI) / 180));
                double b = 2 * (1 - Math.Sin((i + angleStep ) * (Math.PI) / 180));

                double d = Math.Pow(a, 2) + Math.Pow(b, 2) - ( 2 * a * b  * littleAngle);
                output += Math.Sqrt(d);
            }
            Console.WriteLine(output);            
        }

        // Funkcja c)
        private static void GetCurveLengthC(double startAngle, double endAngle, double density)
        {
            double output = 0;
            double angle = endAngle - startAngle;
            double angleStep = angle / density;

            for (double i = startAngle; i < endAngle - angleStep; i += angleStep)
            {
                double littleAngle = Math.Cos(angleStep * (Math.PI) / 180);
                double a = Math.Sin(4 * (i * (Math.PI) / 180));
                double b = Math.Sin(4 * (i + angleStep ) * (Math.PI) / 180);

                double d = Math.Pow(a, 2) + Math.Pow(b, 2) - ( 2 * a * b  * littleAngle);
                output += Math.Sqrt(d);
            }
            Console.WriteLine(output);            
        }

        // Funkcja d)
        private static void GetCurveLengthD(double startAngle, double endAngle, double density)
        {
            double output = 0;
            double angle = endAngle - startAngle;
            double angleStep = angle / density;

            // +0.000001 z powodu błędu pomiarowego związenego z precyzją typu float
            for (double i = startAngle; i < endAngle - angleStep + 0.000001; i += angleStep)
            {
                double littleAngle = Math.Cos(angleStep * (Math.PI) / 180);
                double a = Math.Pow(Math.PI, i * (Math.PI) / 180);
                double b = Math.Pow(Math.PI, (i + angleStep ) * (Math.PI) / 180);

                double d = Math.Pow(a, 2) + Math.Pow(b, 2) - ( 2 * a * b  * littleAngle);
                output += Math.Sqrt(d);
            }
            Console.WriteLine(output);            
        }
        private static void ChooseFunction(string input, double startAngle, double endAngle, double density)
        {

            switch(input) 
            {
                case "a":
                    GetCurveLengthA(startAngle, endAngle, density);   
                    break;
                case "b":
                    GetCurveLengthB(startAngle, endAngle, density);   
                    break;
                case "c":
                    GetCurveLengthC(startAngle, endAngle, density);   
                    break;
                case "d":
                    GetCurveLengthD(startAngle, endAngle, density);   
                    break;
                default:
                    Console.WriteLine("Funkcja oznaczona podaną literą nie istnieje");
                    break;
            }

        }

        private static string ChooseFunctionConsoleInput()
        {
            string input;
            Console.WriteLine("Wybierz funkcje (a-d): ");
            input = Console.ReadLine();

            return input;
        }

        private static void ExitGame()
        {
            Console.WriteLine("Czy chcesz opuścić program (wciśnij 0), czy chcesz wybrać zmienne jeszcze raz? (Wpisz jakąkolwiek inną liczbę)");
            string input = Console.ReadLine();
            double choice;
            
            ValidateDoubleInput(input, out choice);
            Console.Clear();
            if(choice == 0)
                Exit = false;
        }

        private static void ValidateDoubleInput(string input, out double opt)
        {
            double.TryParse(input, out opt);

            while(!double.TryParse(input, out opt))
            {
                Console.WriteLine("Proszę podać liczbę: ");
                input = Console.ReadLine();
            }
        }

        private static void ConsoleDoubleInput(string coordinateName, out double a)
        {
            Console.WriteLine("Podaj wartość {0}: ", coordinateName);
            string input = Console.ReadLine();
            ValidateDoubleInput(input, out a);
        }
    }
}
