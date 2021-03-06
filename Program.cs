using System;
using System.Linq;

namespace zad2
{
    class Program
    {
        private static int n;
        private static string anteeneEye;
        private static string side = "|";
        private static string empty = " ";    
        private static string empty1;          
        private static string empty2;
        private static string empty3;
        private static string empty8;
        private static string anteeneLine;
        private static int x = 0;
        private static int y = 24;
        static void Main(string[] args)
        {

            //       ------------------------ 24 n = 3
           //       |                        |
           //       |    ----------------    |      
           //       |   |                |   |
           //       |   |    --------    |   |
           //       |   |   |        |   |   |   o    o
           //       |   |   |        |   |   |   |    |
           //       |   |   |   -----    |   |   |    | 
           //       |   |   |            |   |   |    |
           //       |   |    ------------    |   |    |
           //       |   |                    |   |    |
           //       |    ---------------------------------
           //       |                            o     o  |
           //       |------------------------------------- 

           //      ------------------------ 24
           //     |                        |                s1
           //     |                        |   o   o        s2 
           //     |                        |   |   |
           //     |                        |   |   |                     
           //     |            ------------------------ 12  s3
           //     |                            o   o   |    s4
           //     |                            _   _   | 
           //     |                             -_-    |    s5
           //     |                                    |    s6
           //      ------------------------------------
           //
           //       
           //      ------------------------------------------------ 48
           //     |                                                |         
           //     |                                                | 
           //     |                                                | 
           //     |                                                |
           //     |            ------------------------            |       ooo     ooo
           //     |           |                        |           |      ooooo   ooooo 
           //     |           |                        |           |       ooo     ooo
           //     |           |                        |           |        |       |
           //     |           |                        |           |        |       |
           //     |           |            ------------            |        |       |
           //     |           |                                    |        |       | 
           //     |           |                                    |        |       |
           //     |           |                                    |        |       |
           //     |           |                                    |        |       |                   
           //     |            -------------------------------------------------------------  
           //     |                                                         o       o       |
           //     |                                                         __     __       |
           //     |                                                           --_--         |
           //     |                                                                         |
           //      -------------------------------------------------------------------------
           // 
           //    1 1 
           //    2 4
           //
          
        Console.WriteLine("Podaj n:");
        string input = Console.ReadLine();
 
        ValidateInput(input, out n);

        empty1 = EmptySpace(n * 24);
        empty2 = EmptySpace(12); 
        empty3 = EmptySpace(11);
        empty8 = EmptySpace(n * 4 - 1);
        anteeneLine = empty8 + side + empty8 + side;

        //Change eye size
        ChangeEyeSize(n);

        //Draw first line of shell
        FirstLineOfShell(n);

        //Write first 4 lines of shell
        WriteFirstLineOfShell(n);
                   
        // Write shell from first four lines to the middle line
        WriteTopHalfOfShell(n);

        //Write the middle line of shell
        WriteMiddleOfShell(n);
        
        //Write shell from middle line to the head
        WriteBottomOfShell(n);

        //Write head
        WriteHead(n);
        
        }
        private static string EmptySpace(int count)
        {   
            string emptyString = new string(' ', count);
            return emptyString;
        }
        private static string DrawLine(int count)
        {
            string line = new string('-', count);
            return line;
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
        private static void ChangeEyeSize(int n)
        {
            string anteneeEyeSmall = "o";
            string anteneeEyeMedium = "0";
            string anteneeEyeLarge = "O";

            anteeneEye = anteneeEyeSmall;

            if(n > 2)
                anteeneEye = anteneeEyeMedium;
            if(n > 4)
                anteeneEye = anteneeEyeLarge;
        }
        private static void FirstLineOfShell(int n)
        {
            string basicShell = DrawLine(n * 24);
            Console.WriteLine(empty + basicShell);
        }
        private static void WriteFirstLineOfShell(int n)
        {
            string shorterShell = DrawLine(n * 24 - 24);

            if(n > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(side + empty1 + side);       
                } 
                Console.WriteLine(side + empty2 + shorterShell + empty2 + side);
            }
            else
            {   
                Console.WriteLine(side + empty1 + side);
                Console.WriteLine(side + empty1 + side + empty8 + anteeneEye + empty8 + anteeneEye);
                Console.WriteLine(side + empty1 + side + anteeneLine);
                Console.WriteLine(side + empty1 + side + anteeneLine);    
            }
        }

        private static void WriteTopHalfOfShell(int n)
        {
            for (int i = 0; i < n - 1; i++)
            {                  
                string shellInside = DrawLine(n * 24 - y);
                string sideAndEmpty = side + empty3; 
                string sideInside = side + empty3;
                string emptySpace = EmptySpace((n * 24) - y); 

                sideAndEmpty = String.Concat(Enumerable.Repeat(sideAndEmpty, x + 1));
                sideInside = String.Concat(Enumerable.Repeat(sideInside, x));    

                if(x > 0)
                    Console.WriteLine(sideInside + side + empty2 + shellInside + empty2 +  sideInside + side);

                if(i >= n - 2)
                {
                    Console.WriteLine(sideAndEmpty + side + emptySpace + sideAndEmpty + side);  
                    Console.WriteLine(sideAndEmpty + side + emptySpace + sideAndEmpty + side + empty8 + anteeneEye + empty8 + anteeneEye); 
                    Console.WriteLine(sideAndEmpty + side + emptySpace + sideAndEmpty + side + anteeneLine); 
                    Console.WriteLine(sideAndEmpty + side + emptySpace + sideAndEmpty + side + anteeneLine); 
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {   
                        Console.WriteLine(sideAndEmpty + side + emptySpace + sideAndEmpty + side);      
                    }
                }            
                
                y += 24;  
                x++;     
            }
        }

        private static void WriteMiddleOfShell(int n)
        {
            if(n > 1)
            {
                string middle = new string('-', 12);
                string middleLine =  empty3 + side ;
                middleLine = String.Concat(Enumerable.Repeat(middleLine, x));   
                Console.WriteLine(side + middleLine + empty3 + empty + middle + empty + middleLine + anteeneLine);   
            }
        }

        private static void WriteBottomOfShell(int n)
        {
            for (int i = 0; i < n - 1; i++)
            {   
                y -= 24;
                x--;

                string empty11;
                string sideAndEmpty = side + empty3; 
                string sideInside = side + empty3;

                string emptySpace = EmptySpace((n * 24) - y + 12); 
                sideAndEmpty = String.Concat(Enumerable.Repeat(sideAndEmpty, x + 1));
                sideInside = String.Concat(Enumerable.Repeat(sideInside, x));  
                string shellInside = DrawLine(n * 24 - y + 12);

                for (int j = 0; j < 4; j++)
                {                  
                    Console.WriteLine(sideAndEmpty + side + emptySpace + sideInside + side + anteeneLine);
                }
                if(i < n-2)

                    if(n > 3)
                    {
                        empty11 = EmptySpace((n - 3) * 4);
                        Console.WriteLine(sideInside + side + empty2 + shellInside + empty2 + sideInside + empty11 + side + empty8 + side);     
                    }
                    else
                        Console.WriteLine(sideInside + side + empty2 + shellInside + empty2 + sideInside + side + empty8 + side);
                    // 4 4 
                    // 5 8 
                    // 6 12  
                    // 7 16  
            }
        }
        private static void WriteHead(int n)
        {
            string longShell = DrawLine(n * 36 - 12);
            Console.WriteLine(side + empty2 + longShell);
        
            string longEmpty = EmptySpace(n * 36);
            string empty7 = EmptySpace(n * 24 + n * 4);
        
            string empty9 = EmptySpace(n * 4 - 2 * (n - 1) - 1);
            string empty10 = EmptySpace(n);
            string under = "_";
            string eye = "o";
            string underscore = new string('_', n);
            string dash = DrawLine(n);

            Console.WriteLine(side + empty7 + eye + empty8 + eye + empty8 + side);
            Console.WriteLine(side + empty7 + underscore + empty9 + underscore + empty8 + side);
            Console.WriteLine(side + empty7 + empty10 + dash +  under + dash + empty10 + empty8 + side);
            Console.WriteLine(side + longEmpty + side);

            string finalShell = DrawLine(n * 36);
            Console.WriteLine(empty + finalShell);

            Console.ReadLine();
        }
    }  
}
