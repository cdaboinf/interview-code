using System;
using System.Collections.Generic;

namespace interview_code
{
    public class Strings
    {
        public Strings() { }
        
        public int ReverseInteger(int x)
        {
            if (x > int.MaxValue || x < int.MinValue)
            {
                return 0;
            }

            int reverse = 0;
            int pop = 0;
            var isNegative = false;

            if (x < 0)
            {
                x = x * -1;
                isNegative = true;
            }

            while (x != 0)
            {
                pop = x % 10;
                //Console.Write($"pop=> x({x}) % 10 = {pop}, ");

                //Console.Write($"x=> x({x}) / 10 ");
                x = x / 10;
                //Console.Write($"= {x}, ");

                //Console.Write($"reverse=> reverse({reverse}) * 10 + pop({pop}) ");
                reverse = reverse * 10 + pop;
                //Console.Write($"= {reverse}");

                //Console.WriteLine();
            }

            reverse = isNegative ? -1 * reverse : reverse;

            if (reverse >= int.MaxValue - 1 || reverse < int.MinValue)
            {
                return 0;
            }

            return reverse;
        }

        public List<string> StringPermutations(string value)
        {
            Permutations(
                0,
                value.Length,
                value,
                new List<string>());

            return null;
        }
        
        public List<List<int> > NumberPermutations(List<int> array) {
            var permutations = new List<List<int>>();
            Permutations(0, array.Count, array, permutations);
            return permutations;
        }
        
        private void Permutations(int l, int r, string value, List<string> permutations)
        {
            Console.WriteLine($" L: {l}, R: {r}");
            if (l == r)
            {
                Console.WriteLine(value); 
                permutations.Add(value);
            }
            else
            {
                for (var x = l; x < r; x++)
                {
                    //Console.WriteLine($"Swap1({l}, {x}, {value})");
                    value = Swap(l, x, value);
                    //Console.WriteLine($"value1={value}");
                    
                    Console.WriteLine($"Permutations({l+1}, {r}, {value})");
                    Permutations(l + 1, r, value, permutations);
                    
                    //Console.WriteLine($"Swap2({l}, {x}, {value})");
                    value = Swap(l, x, value);
                    //Console.WriteLine($"value2={value}");
                }
            }
        }
        
        private static void Permutations(int l, int r, List<int> array, List<List<int>> permutations)
        {
            //Console.WriteLine($" L: {l}, R: {r}");
            if (l == r)
            {
                Console.WriteLine($"Permutations({l+1}, {r}, array: {array[0]}, {array[1]}, {array[2]})");
                permutations.Add(new List<int>(array));
            }
            else
            {
                for (var x = l; x < r; x++)
                {
                    //Console.WriteLine($"Swap1({l}, {x}, {value})");
                    array = Swap(l, x, array);
                    //Console.WriteLine($"value1={value}");

                    //Console.WriteLine($"Permutations({l+1}, {r}, array: {array[0]}, {array[1]}, {array[2]})");
                    Permutations(l + 1, r, array, permutations);

                    //Console.WriteLine($"Swap2({l}, {x}, {value})");
                    array = Swap(l, x, array);
                    //Console.WriteLine($"value2={value}");
                }
            }
        }

        private string Swap(int i, int j, string value)
        {
            var characters = value.ToCharArray();
            var temp = characters[i];
            characters[i] = characters[j];
            characters[j] = temp;
            
            return new string(characters);
        }
        
        private static List<int> Swap(int i, int j, List<int> array)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;

            return array;
        }
    }
}