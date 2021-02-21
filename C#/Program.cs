using System;

namespace primeNumbers
{
     class primeNumbers
    {
        public static void Main()
        {
            int start_number, end_number;
            Console.Write("Input starting number of range: ");
            start_number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input ending number of range : ");
            end_number = Convert.ToInt32(Console.ReadLine());

            Console.Write("The prime numbers between {0} and {1} are : \n", start_number, end_number);


            for (int number= start_number; number<= end_number; number++)
            {
                int count = 0;
                for(int i=1;i<=number;i++)
                {
                    if (number%i==0)
                    {
                        count++;
                    }
                }
                if(count==2)
                {
                    Console.WriteLine(number);
                }
            }
            Console.ReadLine();
            
        }
    }
}