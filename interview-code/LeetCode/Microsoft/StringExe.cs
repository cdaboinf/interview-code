using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace interview_code.LeetCode.Microsoft
{
    public class StringExe
    {
        public StringExe()
        {
        }

        public int Exe1(int[] A)
        {
            var maxSum = int.MinValue;
            for (var i = 0; i < A.Length; i++)
            {
                for (var j = i+1; j < A.Length; j++)
                {
                    var digitSum1 = GetDigitsSum(A[i]);
                    var digitSum2 = GetDigitsSum(A[j]);
                    if (digitSum1 == digitSum2)
                    {
                        var numbersSum = A[i] + A[j];
                        if (numbersSum > maxSum)
                        {
                            maxSum = numbersSum;
                        }
                    }
                }
            }
            
            return maxSum == int.MinValue ? -1 : maxSum;
        }

        public int GetDigitsSum(int number)
        {
            var digits = new List<int>();
            while (number >0)
            {
                digits.Add(number%10);
                number = number / 10;
            }

            return digits.Count > 0 ? digits.Sum() : number;
        }

        public int Exe2(string s)
        {
            var operators = new Stack<int>();
            var instructions = s.Split(' ');
            
            // loop over the instructions
            for (var i = 0; i < instructions.Length; i++)
            {
                // validate if the element is valid instruction, then execute instruction
                if (instructions[i] == "POP")
                {
                    if (operators.Count > 0)
                    {
                        operators.Pop();
                    } 
                }
                if (instructions[i] == "DUP")
                {
                    if (operators.Count > 0)
                    {
                        var topMost = operators.Peek();
                        operators.Push(topMost);
                    } 
                }
                if (instructions[i] == "+")
                {
                    if (operators.Count > 2)
                    {
                        var num1 = operators.Pop();
                        var num2 = operators.Pop();
                        operators.Push(num1+num2);
                    }
                    else
                    {
                        return -1;
                    }
                }
                if (instructions[i] == "-")
                {
                    if (operators.Count > 2)
                    {
                        var num1 = operators.Pop();
                        var num2 = operators.Pop();
                        operators.Push(num1-num2);
                    }
                    else
                    {
                        return -1;
                    }
                }

                int number = 0;
                if (int.TryParse(instructions[i], out number))
                {
                    operators.Push(number);
                }
            }

            return operators.Pop();
        }

        // *** samples *** //
        /*public static void generateMaxPossibleValue(int num)
        {
            String numString = String.valueOf(num);
            String result = null;

            // Negative number
            if (num < 0)
            {
                numString = numString.substring(1);

                for (int i = 0; i < numString.length(); i++)
                {
                    if (numString.charAt(i) - '0' > 5)
                    {
                        result = "-" + numString.substring(0, i) + "5" + numString.substring(i);
                        break;
                    }

                    if (result == null)
                    {
                        result = "-" + numString + "5";
                    }
                }
                System.out.println(result);
                return;
            }

            for (int i = 0; i < numString.length(); i++)
            {
                if (numString.charAt(i) - '0' < 5)
                {
                    result = numString.substring(0, i) + "5" + numString.substring(i);
                    break;
                }
            }

            System.out.println(result);
        }*/
    }
}
