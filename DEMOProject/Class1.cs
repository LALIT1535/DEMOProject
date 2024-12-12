using System;

class Program
{
    static void Main(string[] args)
    {

        int[] numbers = { 2, 4, 6, 8 };
        int[] repeatCounts = { 2, 3, 4, 5 };


        for (int i = 0; i < numbers.Length; i++)
        {
            int number = numbers[i];
            int count = repeatCounts[i];

            for (int j = 0; j < count; j++)
            {
                Console.Write(number);
            }

            Console.WriteLine();
        }

        Console.ReadLine();
    }
}
