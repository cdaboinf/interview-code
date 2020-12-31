namespace interview_code
{
    public class LeetCodeStringTest_
    {
        /*
         * Reverse String
         *
         * Write a function that reverses a string. The input string is given as an array of characters char[].
         * Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.
         * You may assume all the characters consist of printable ascii characters.
         */
        public void ReverseString(char[] s)
        {
            var start = 0;
            var end = s.Length - 1;

            while (start < end)
            {
                var temp = s[end];
                s[end] = s[start];
                s[start] = temp;

                start++;
                end--;
            }
        }

        /*
         * Reverse Integer
         *
         * Given a 32-bit signed integer, reverse digits of an integer.
         */
        public int Reverse(int x)
        {
            long reverse = 0;
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
                x = x / 10;
                reverse = reverse * 10 + pop;
            }

            reverse = isNegative ? -1 * reverse : reverse;

            if (reverse > int.MaxValue || reverse < int.MinValue)
            {
                return 0;
            }

            return (int)reverse;
        }

        /*
         * String to Integer (atoi)
         *
         *  Implement atoi which converts a string to an integer.
    
            The function first discards as many whitespace characters as necessary until the first non-whitespace character is found. 
            Then, starting from this character, takes an optional initial plus or minus sign followed by as many numerical digits as possible, and interprets them as a numerical value.
    
            The string can contain additional characters after those that form the integral number, which are ignored and have no effect on the behavior of this function.
    
            If the first sequence of non-whitespace characters in str is not a valid integral number, 
            or if no such sequence exists because either str is empty or it contains only whitespace characters, no conversion is performed.
    
            If no valid conversion could be performed, a zero value is returned.
    
            Note:
    
            Only the space character ' ' is considered as whitespace character.
            Assume we are dealing with an environment which could only store integers within the 32-bit signed integer range: [−231,  231 − 1]. 
            If the numerical value is out of the range of representable values, INT_MAX (231 − 1) or INT_MIN (−231) is returned.
         */
        public int MyAtoi(string str)
        {
            if (str == null)
            {
                return 0;
            }

            var trimmed = str.Trim();
            if (trimmed.Length == 0)
            {
                return 0;
            }

            var isNegative = trimmed[0] == '-';
            var isPositive = trimmed[0] == '+';

            double integer = 0;
            if (trimmed.Length == 1
                && ((int)char.GetNumericValue(trimmed[0]) < 0
                    || (int)char.GetNumericValue(trimmed[0]) > 9))
            {
                return 0;
            }

            var initChar = char.GetNumericValue(trimmed[isNegative || isPositive ? 1 : 0]);
            if (isNegative && initChar > 9 || initChar < 0)
            {
                return 0;
            }

            for (var i = isNegative || isPositive ? 1 : 0; i < trimmed.Length; i++)
            {
                var numChar = char.GetNumericValue(trimmed[i]);
                integer = integer * 10 + numChar;
            }

            return isNegative ? -1 * (int)integer : (int)integer;
        }
    }
}