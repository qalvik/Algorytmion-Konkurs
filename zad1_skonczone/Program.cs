// Zadanie 1 - C# - MS VISUAL STUDIO 2017
using System;

namespace zad1
{
    class Program
    {
        private static bool Exit  = true;
        static void Main(string[] args)
        {
            do
            {
                Calculate();   
            }
            while(Exit);               
        }

        private static void ValidateInput(string input, out int opt)
        {
            int.TryParse(input, out opt);

            while(!int.TryParse(input, out opt))
            {
                Console.WriteLine("Proszę podać prawidłową liczbę całkowitą: ");
                input = Console.ReadLine();
            }
        }

        private static string Result(int n)
        {
            //Wzór = 3n + n/n+1 + s(n)

            string result;
            int integer = 3 * n;
            string fraction = n + "/" + (n + 1);
            string sum = "s(" + n + ")";
            
            result = integer + " + " + fraction + " + " + sum;
            return result;
        }

        private static void Calculate()
        {
            Console.WriteLine("Podaj liczbę n: ");
            string input = Console.ReadLine();
            int n = 0;
            
            ValidateInput(input, out n);
            Console.WriteLine("Wynik: " + Result(n));
            ExitGame();
        }
        private static void ExitGame()
        {
            Console.WriteLine("Czy chcesz opuścić program (wciśnij 0), czy chcesz wybrać zmienne jeszcze raz? (Wpisz jakąkolwiek inną liczbę)");
            string input = Console.ReadLine();
            int choice;
            
            ValidateInput(input, out choice);
            Console.Clear();
            if(choice == 0)
                Exit = false;
        }
    }
}
