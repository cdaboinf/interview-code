using System;

namespace interview_code
{
    public class Strings
    {
        public Strings()
        {
        }

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
                Console.Write($"pop=> x({x}) % 10 = {pop}, ");

                Console.Write($"x=> x({x}) / 10 ");
                x = x / 10;
                Console.Write($"= {x}, ");

                Console.Write($"reverse=> reverse({reverse}) * 10 + pop({pop}) ");
                reverse = reverse * 10 + pop;
                Console.Write($"= {reverse}");

                Console.WriteLine();
            }

            reverse = isNegative ? -1 * reverse : reverse;

            if (reverse >= int.MaxValue - 1 || reverse < int.MinValue)
            {
                return 0;
            }

            return reverse;
        }
    }
}