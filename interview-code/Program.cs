using System;
using System.Collections.Generic;
using System.Linq;
using interview_code.CompanyTechInterviews;
using System;
using System.Collections;
using interview_code.AlgoExpert;
using NUnit.Framework;
using interview_code.LeetCode.Microsoft;

namespace interview_code
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var problem = new Blackbaud();

            //var noSpaces = problem.IsPalindrome("abcdcba");
            //var withSpaces = problem.IsPalindromeWithSpaces("ab cdcb a ");
            //var longest = problem.LongestPalindromeSubstring("abcba");

            var apps = new Dictionary<string, string[]>();
            apps.Add("a", new string[] { });
            apps.Add("b", new string[] {"c"});
            apps.Add("c", new string[] {"a"});
            apps.Add("d", new string[] {"b", "c"});
            apps.Add("e", new string[] {"d"}); // a,c,b,d,e
            var dependencies = problem.DependencyList(apps);

            Console.ReadKey();
        }
    }
}