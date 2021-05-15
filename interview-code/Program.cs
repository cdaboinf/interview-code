using System;
using System.Collections.Generic;
using System.Linq;
using interview_code.CompanyTechInterviews;
using System;
using System.Collections;
using System.Threading.Tasks;
using interview_code.AlgoExpert;
using interview_code.DesingClassInterviews;
using NUnit.Framework;
using interview_code.LeetCode.Microsoft;

namespace interview_code
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var exe = new Test101(1);
            var res = exe.Sum(exe,2);
            Console.ReadKey();
        }
    }
}