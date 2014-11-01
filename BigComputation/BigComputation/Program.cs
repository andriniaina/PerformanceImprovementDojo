using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BigComputation
{
    class Program
    {
        const int N = 10;

        //The goal of the exercise is to optimize the Main function to compute the result faster
        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            const string uriBase = "http://localhost:9006//givemeanumber/";
            var bigNumbers = new List<long>();
            for (int i = 0; i < N; i++)
            {
                var uri = new Uri(uriBase + i);
                var client = new WebClient();
                string response = client.DownloadString(uri);
                var bigNumber = long.Parse(response.Split(' ').Last());
                Console.WriteLine("{0} is a big number !", bigNumber);
                bigNumbers.Add(bigNumber);
            }

            long totalSumOfDivisors = 0;
            foreach(var bigNumber in bigNumbers)
            {
                totalSumOfDivisors += SumOfDivisors(bigNumber);
            }
            timer.Stop();
            Console.WriteLine("Sum of divisors for N = {0} is {1}", N, totalSumOfDivisors);
            Console.WriteLine("Should be 1948659880 for N = 10");
            Console.WriteLine("Result found in {0} ms", timer.ElapsedMilliseconds);
            Console.ReadLine();
        }

        //This function uses the CPU intensively on purpose
        //Altough it may be interesting to optimize this function, it's not the purpose of the exercise
        //So, don't touch it !
        private static long SumOfDivisors(long bigNumber)
        {
            long sumOfDivisors = 0;
            Console.WriteLine("Summing divisors of {0}...", bigNumber);
            for (long probableDivisor = 1; probableDivisor <= bigNumber; probableDivisor++)
            {
                if (bigNumber % probableDivisor == 0)
                {
                    sumOfDivisors += probableDivisor;
                }
            }
            return sumOfDivisors;
        }
    }
}
