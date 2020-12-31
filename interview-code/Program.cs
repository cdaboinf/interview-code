using System;
using System.Collections.Generic;
using System.Linq;
using interview_code.CompanyTechInterviews;
using System;
using interview_code.AlgoExpert;
using NUnit.Framework;

namespace interview_code
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //var inp = Console.ReadLine();
            //Console.WriteLine(inp);
            
            var practice = new TestPrep();
            var input = new List<int> {1,2,3};

            practice.Powerset(input);

            Console.ReadKey();
        }
    }
}
