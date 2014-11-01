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

            var totalSumOfDivisors = Enumerable.Range(0, N).AsParallel().WithDegreeOfParallelism(N).Select(i => SumOfDivisors(GetBigNumber(i))).Sum();
            
            timer.Stop();
            Console.WriteLine("Sum of divisors for N = {0} is {1}", N, totalSumOfDivisors);
            Console.WriteLine("Should be 1948659880 for N = 10");
            Console.WriteLine("Result found in {0} ms", timer.ElapsedMilliseconds);
            Console.ReadLine();
        }

        private static int GetBigNumber(int i)
        {
            const string uriBase = "http://localhost:9006//givemeanumber/";
            var uri = new Uri(uriBase + i);
            var client = new WebClient();
            string response = client.DownloadString(uri);
            var bigNumber = int.Parse(response.Split(' ').Last());
            Console.WriteLine("{0} is a big number !", bigNumber);
            return bigNumber;
        }

        //This function uses the CPU intensively on purpose
        //Altough it may be interesting to optimize this function, it's not the purpose of the exercise
        //So, don't touch it !
        private static Dictionary<int, int> computedSumOfDivisors = new Dictionary<int, int>();

        private static int SumOfDivisors(int bigNumber)
        {
            int computedResult;
            if(computedSumOfDivisors.TryGetValue(bigNumber, out computedResult))
            {
                return computedResult;
            }
            Console.WriteLine("Summing divisors of {0}...", bigNumber);
            int sumOfDivisors = 1 + bigNumber;
            int maxValue = bigNumber;
            for (int probableDivisor = 2; probableDivisor < maxValue; probableDivisor++)
            {
                if (bigNumber % probableDivisor == 0)
                {
                    var biggerValue = bigNumber / probableDivisor;
                    maxValue = biggerValue;

                    sumOfDivisors += probableDivisor;
                    if (probableDivisor != biggerValue)
                        sumOfDivisors += biggerValue;
                }
            }
            computedSumOfDivisors[bigNumber] = sumOfDivisors;
            return sumOfDivisors;
        }
    }
}
