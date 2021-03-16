//Zadanie 3 - C# - MS VISUAL STUDIO 2017
//Do działania programu potrzebny jest pakiet NuGet - System.Text.Encoding.CodePages
//Aby program działał prawidłowo, w jego lokalizacji musi być umieszczony plik 'slownik.txt'

using System;
using System.Collections.Generic;
using System.Linq;

namespace zad3
{
    class Program
    {      
        private static bool Exit = true;
        private static bool FileFound = true;
        private static string Word;
        private static HashSet<string> Words;
        private static bool IsFinished = false;
        private static int PlayerMoveCount = 0;
        private static char[] alpha = "aąbcćdeęfghijklłmnńoóprsśtuwyzźż".ToCharArray();
        
        static void Main(string[] args)
        {   
            LoadWords();
            do
            {
                if(FileFound)
                {
                    StartGame();
                    ExitGame();
                }                   
                else
                    Console.ReadLine();    
            }
            while(Exit);                    
        }
        private static void FirstPlayerMove()
        {      
            Console.WriteLine("Podaj pierwszą literę:" );
            Word = Console.ReadLine();
            Console.WriteLine();
            ValidateStringInput(Word);
            PlayerMoveCount++;  
        }

        private static void FirstComputerMove()
        {
            Random random = new Random();
            int start = random.Next(0, alpha.Length);
            Word = alpha[start].ToString();
            Console.WriteLine(alpha[start] + "\n");         
        }

        private static void ComputerMove()
        {   
            Random rnd = new Random();
           
            int start = rnd.Next(0, alpha.Length);
            int sides = rnd.Next(0,2);

            HashSet<string> lines = new HashSet<string>(Words);
            bool brk = true;
           
            if(lines.Contains(Word))
            {
                Console.WriteLine("Słowo " + Word + " istnieje w słowniku. Komputer wygrywa!");  
                IsFinished = true;  
            }
            else if(!lines.Any(x => x.Contains(Word)))
            {
                Console.WriteLine("Podaj wyraz, który zawiera zbitkę " + Word);
                string input = Console.ReadLine();
                if(!lines.Contains(input))
                    Console.WriteLine("Wyraz " + Word + " nie istnieje w słowniku. Komputer wygrywa!");
                IsFinished = true;
            }
            else
            {
                while(brk)
                {
                    int after = 0;
                    int length = Word.Length;  
                    string word = "";
                    bool matchNotFound = true;
                    string temp = lines.FirstOrDefault(x => x.Contains(Word));

                    //Sprawdza, czy w liście są jakieś pasujące słowa
                    if(temp != null)
                    {
                        int index = temp.IndexOf(Word);
                        after = length + index;

                        //Wpisuje literę przez zbtiką
                        if(after < temp.Length)
                        {   
                            word = Word + temp[after];
                            if(!Words.Contains(word))
                            {
                                Word = word;
                                matchNotFound = false;
                            }             
                        }
                        //Wpisuje literę za zbitką
                        if(index > 0 && matchNotFound)
                        {
                            word = temp[index-1] + Word;
                            if(!Words.Contains(word))
                            {
                                Word = word;
                            }    
                        }
                        //Sprawdza, czy zbitka nie jest słowem instniejącym już w słowniku
                        if(!Words.Contains(word))
                        {     
                            brk = false;
                            Console.WriteLine("Ruch komputera");
                            Console.WriteLine(Word);
                            Console.WriteLine();   
                        } 
                        else
                            lines.Remove(temp);
                    }    
                    // Kiedy nie znajdzie trafu, losowa litera wpisywana jest z losowej strony
                    else
                    {
                        if(sides == 1)
                            Word += alpha[start];
                        else
                            Word = alpha[start] + Word;

                        Console.WriteLine("Ruch komputera");
                        Console.WriteLine(Word);
                        Console.WriteLine();  
                        brk = false;
                    }                
                }
            }     
        }

        private static void PlayerMove()
        {                        
            if(PlayerMoveCount == 0)
            {
                AddLetter();
            }
            else
            {
                int opt = 0;            
                Console.WriteLine("Podaj literę (wpisz 0), sprawdź, czy komputer nie blefuje (wpisz 1) lub zgłoś wyraz (wpisz 2)");
                string input = Console.ReadLine();
                ValidateInput(input, out opt);

                while(!(opt == 0 || opt == 1 || opt == 2))
                {
                    Console.WriteLine("Prosze podać prawidłową liczbę (0 - 2)");
                    input = Console.ReadLine();     
                    ValidateInput(input, out opt);                    
                }

                if(opt == 0)
                {
                    AddLetter();
                }
                else if(opt == 1)
                {                   
                    if(!Words.Any(x => x.Contains(Word)))
                    {
                        Console.WriteLine("Słowo ze zbitką " + Word + " nie istnieje w słowniku. Gracz wygrywa!");
                        IsFinished = true;
                    }       
                    else
                    {
                        Console.WriteLine("Słowo ze zbitką " + Word + " istnieje w słowniku. Komputer wygrywa!"); 
                        IsFinished = true;
                    }                                           
                }
                else if(opt == 2)
                {
                    if(Words.Contains(Word))
                    {
                        Console.WriteLine("Słowo " + Word + " istnieje w słowniku. Gracz wygrywa!");
                        IsFinished = true;
                    }       
                    else
                    {
                        Console.WriteLine("Słowo " + Word + " nie istnieje w słowniku. Komputer wygrywa!"); 
                        IsFinished = true;
                    }                       
                }        
            }                                 
        }  

        private static void AddLetter()
        {   
            PlayerMoveCount = 1;
            string character = "";
            int side = 0;
            string input;
            
            Console.WriteLine("Podaj literę: ");
            character = Console.ReadLine();

            ValidateStringInput(character);

            Console.WriteLine("Wybierz stronę dodania litery (lewa - wpisz 0 || prawa - wpisz 1): ");
            input = Console.ReadLine();   
            ValidateInput(input, out side);
            
            while(!(side == 0 || side == 1))
            {
                Console.WriteLine("Proszę podać prawidłową liczbę (0 - 1)");
                input = Console.ReadLine();   
                ValidateInput(input, out side);
            }
            
            if(side == 0)
                Word = character + Word; 
            else if(side == 1)
                Word += character;
            
            Console.WriteLine(Word + "\n");
        }

        private static void StartGame()
        {
            Random rnd = new Random();
            bool isPlayerMove = false;
            
            Console.WriteLine("Witaj w grze! Naciśnij dowolny klawisz aby wylosować, kto zaczyna");
            Console.ReadLine();

            // 0 gracz 1 komputer
            int start = rnd.Next(0,2);
            if(start == 0)
            {
                Console.WriteLine("Zaczyna gracz");
                FirstPlayerMove(); 
                isPlayerMove = false;
            }  
            else if(start == 1)
            {
                Console.WriteLine("Zaczyna komputer");
                FirstComputerMove();
                isPlayerMove = true;
            }

            while(IsFinished == false)
            {
                if(isPlayerMove == true)
                {
                    PlayerMove();
                    isPlayerMove = false;
                }
                else
                {
                    ComputerMove();
                    isPlayerMove = true;
                }
            }

            Console.WriteLine("******************KONIEC GRY******************" + "\n");
        }

        private static void LoadWords()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string fileName = "slownik.txt";
            string[] arr; 

            try
            {
                arr = System.IO.File.ReadAllLines(fileName, System.Text.Encoding.GetEncoding(1250));
                Words = new HashSet<string>(arr);
            }
            catch (System.Exception)
            {
                FileFound = false;
                Console.WriteLine("Nie odnaleziono pliku 'slownik.txt' w lokalizacji aplikacji");
            }
        }

        private static void ValidateInput(string input, out int opt)
        {
            Int32.TryParse(input, out opt);

            while(!int.TryParse(input, out opt))
            {
                Console.WriteLine("Proszę podać prawidłową liczbę: ");
                input = Console.ReadLine();
            }
        }

        private static void ValidateStringInput(string input)
        {
            while(!input.All(char.IsLetter))
            {           
                Console.WriteLine("Proszę podać prawidłową literę: ");
                input = Console.ReadLine();
            }
        }
        private static void ExitGame()
        {
            Console.WriteLine("Czy chcesz opuścić program (wciśnij 0), czy chcesz zagrać jeszcze raz? (Wpisz jakąkolwiek inną liczbę)");
            string input = Console.ReadLine();
            int choice;
            
            ValidateInput(input, out choice);
            Console.Clear();
            if(choice == 0)
                Exit = false;
        }
    }
}