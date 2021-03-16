//Zadanie 4 - C#- MS VISUAL STUDIO 2017
using System;

namespace zad4
{
    class Program
    {
        private static bool Exit  = true;
        static void Main(string[] args)
        {   
            do
            {
                GetCoordinates();
            }
            while(Exit);
            
        }
        private static void GetCoordinates()
        {
            int shots;
            double a,b,y1,y2;

            Input(out a, out b, out y1, out y2, out shots);

            double x = 0;
            double y = 0;

            double vectorX = 0;
            double vectorY = 0;
            
            CalculateCoordinates(a, b, out x, out y, y1, y2, 
                                out vectorX, out vectorY);
          
            int [,] coordinates = CreateArray(x,y);

            string input = ChooseFunctionConsoleInput();

            double sum = ChooseFunction(input, coordinates, vectorX, vectorY, shots);
            
            double result = (x * y * sum) / shots;

            Console.WriteLine("Pole wynosi " + result);
            ExitGame();


        }
        private static double FunctionA(int[,] arr, double vectorX, double vectorY, int shotsCount)
        {
            Random r = new Random();
            double sum = 0; 

            for (int i = 0; i < shotsCount; i++)
            {
                double y = r.Next(arr.GetLength(0));
                double x = r.Next(arr.GetLength(1));

                x = x / 1000;
                y = y / 1000;

                double e = 2.25 - (x + vectorX) + vectorY;
                double f = x * (2 - (x + vectorX)) + vectorY;
                if(y >= f && y <= e)
                    sum ++;                           
            }

            return sum;
        }

        private static double FunctionB(int[,] arr, double vectorX, double vectorY, int shotsCount)
        {
            Random r = new Random();
            double sum = 0; 

            for (int i = 0; i < shotsCount; i++)
            {
                double y = r.Next(arr.GetLength(0));
                double x = r.Next(arr.GetLength(1));

                x = x / 1000;
                y = y / 1000;

                double e = x + Math.Cos(x + vectorX) - 0.5 + vectorY;
                double f = (x - 1) * Math.Sin(x + vectorX) + vectorY;

                if(y >= f && y <= e)
                    sum ++;        
            }

            return sum;
        }

        private static double FunctionC(int[,] arr, double vectorX, double vectorY, int shotsCount)
        {
            Random r = new Random();
            double sum = 0; 

            for (int i = 0; i < shotsCount; i++)
            {
                double y = r.Next(arr.GetLength(0));
                double x = r.Next(arr.GetLength(1));

                x = x / 1000;
                y = y / 1000;

                double e = -Math.Sqrt(1-Math.Pow((x + vectorX), 2)) + vectorY; 
                double f = Math.Sqrt(1-Math.Pow((x + vectorX), 2)) + vectorY;

                if(y >= e && y <= f)
                    sum ++;           
            }

            return sum;
        }

        private static double FunctionD(int[,] arr, double vectorX, double vectorY, int shotsCount)
        {
            Random r = new Random();
            double sum = 0; 

            for (int i = 0; i < shotsCount; i++)
            {
                double y = r.Next(arr.GetLength(0));
                double x = r.Next(arr.GetLength(1));

                x = x / 1000;
                y = y / 1000;

                double e = Math.Pow(2, Math.Cos(x + vectorX)) + 1.15 + vectorY; 
                double f = Math.Pow(2, Math.Sin(x + vectorX)) + vectorY;

                if(y >= f && y <= e)
                    sum ++;        
            }

            return sum;
        }
        private static void Input(out double a, out double b, out double y1, out double y2, out int count)
        {
            ConsoleDoubleInput("a", out a);
            ConsoleDoubleInput("b", out b);
            ConsoleDoubleInput("y1", out y1);
            ConsoleDoubleInput("y2", out y2);

            ConsoleIntInput(out count);
        }

        private static void CalculateCoordinates(double a, double b, out double x, out double y, double y1, double y2,
                                                 out double vectorX, out double vectorY)
        {
            x = 0;
            y = 0;
            vectorX = 0;
            vectorY = 0;

            if(a == b)
            {
                Console.WriteLine("Wartość a i b nie może być taka sama!");
                ConsoleDoubleInput("a", out a);
                ConsoleDoubleInput("b", out b);
            }
            if(a > b)
            {
                x = a - b;
                vectorX = b;
            }      
            else if(a < b)
            {
                x = b - a;
                vectorX = a;
            }    

            if(y1 == y2)
            {
                Console.WriteLine("Wartość y1 i y2 nie może być taka sama!");
                ConsoleDoubleInput("y1", out y1);
                ConsoleDoubleInput("y2", out y2);
            } 
            if(y1 > y2)
            {
                y = y1 - y2;
                vectorY = 0 - y2;
            }            
            else if(y1 < y2)
            {
                y = y2 - y1;
                vectorY = 0 - y1;
            }               
        }

        private static int[,] CreateArray(double x, double y)
        {
            double arrX = y * 1000;
            double arrY = x * 1000; 

            int [,] arr = new int[(long)arrX, (long)arrY];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i,j] = j;
                }
            }
            return arr;
        }

        private static double ChooseFunction(string input, int[,] arr, double vectorX, double vectorY, int shotsCount)
        {
            double result = 0;

            switch(input) 
            {
            case "a":
                result = FunctionA(arr, vectorX, vectorY, shotsCount);   
                break;
            case "b":
                result = FunctionB(arr, vectorX, vectorY, shotsCount);   
                break;
            case "c":
                result = FunctionC(arr, vectorX, vectorY, shotsCount);   
                break;
            case "d":
                result = FunctionD(arr, vectorX, vectorY, shotsCount);   
                break;
            default:
                result = 0;
                Console.WriteLine("Funkcja oznaczona podaną literą nie istnieje");
                break;
            }

            return result;
        }

        private static string ChooseFunctionConsoleInput()
        {
            string input;
            Console.WriteLine("Wybierz funkcje (a-d): ");
            input = Console.ReadLine();

            return input;
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

        private static void ValidateIntInput(string input, out int opt)
        {
            int.TryParse(input, out opt);

            while(!int.TryParse(input, out opt))
            {
                Console.WriteLine("Proszę podać liczbę całkowitą: ");
                input = Console.ReadLine();
            }
        }

        private static void ConsoleDoubleInput(string coordinateName, out double a)
        {
            Console.WriteLine("Podaj wartość {0}: ", coordinateName);
            string input = Console.ReadLine();
            ValidateDoubleInput(input, out a);
        }

        private static void ConsoleIntInput(out int a)
        {
            Console.WriteLine("Podaj liczbę strzałów: ");
            string input = Console.ReadLine();
            ValidateIntInput(input, out a);
        }

        private static void ExitGame()
        {
            Console.WriteLine("Czy chcesz opuścić program (wciśnij 0), czy chcesz wybrać zmienne jeszcze raz? (Wpisz jakąkolwiek inną liczbę)");
            string input = Console.ReadLine();
            int choice;
            
            ValidateIntInput(input, out choice);
            Console.Clear();
            if(choice == 0)
                Exit = false;
        }

    }
}
