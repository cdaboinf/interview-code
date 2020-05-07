using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace interview_code
{
    public class Test
    {
        /*
        Given two arrays A and B of length N, determine if there is a way to make A equal to B by reversing any subarrays from array B any number of times.
        Signature
        bool areTheyEqual(int[] arr_a, int[] arr_b)
        Input
        All integers in array are in the range [0, 1,000,000,000].
        Output
        Return true if B can be made equal to A, return false otherwise.
        Example
        A = [1, 2, 3, 4]
        B = [1, 4, 3, 2]
        output = true
        After reversing the subarray of B from indices 1 to 3, array B will equal array A.
        */
        public static bool areTheyEqual(int[] arr_a, int[] arr_b)
        {
            // Write your code here
            Console.WriteLine("areTheyEqual");
            var flag = false;
            for (var i = 0; i < arr_a.Length; i++)
            {
                if (arr_a[i] != arr_b[i])
                {
                    for (var j = i; j < arr_b.Length; j++)
                    {
                        if (arr_a[i] == arr_b[j])
                        {
                            flag = true;
                            var temp = arr_b[i];
                            arr_b[i] = arr_b[j];
                            arr_b[j] = temp;
                        }
                    }
                    if (!flag)
                    {
                        return false;
                    }
                }
            }
            return flag;
        }
        /*
        You are given an array a of N integers. For each index i, you are required to determine the number of contiguous subarrays that fulfills the following conditions:
        The value at index i must be the maximum element in the contiguous subarrays, and
        These contiguous subarrays must either start from or end on index i.
        Signature
        int[] countSubarrays(int[] arr)
        Input
        Array a is a non-empty list of unique integers that range between 1 to 1,000,000,000
        Size N is between 1 and 1,000,000
        Output
        An array where each index i contains an integer denoting the maximum number of contiguous subarrays of a[i]
        Example:
        a = [3, 4, 1, 6, 2]
        output = [1, 3, 1, 5, 1]
        Explanation:
        For index 0 - [3] is the only contiguous subarray that starts (or ends) with 3, and the maximum value in this subarray is 3.
        For index 1 - [4], [3, 4], [4, 1]
        For index 2 -[1]
        For index 3 - [6], [6, 2], [1, 6], [4, 1, 6], [3, 4, 1, 6]
        For index 4 - [2]
        So, the answer for the above input is [1, 3, 1, 5, 1]
        */
        public int[] CountSubarrays(int[] arr)
        {
            // Write your code here
            Console.WriteLine("countSubarrays");
            var result = new int[arr.Length];
            var counter = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                for (var j = i; j < arr.Length; j++)
                {
                    if (arr[j] <= arr[i])
                    {
                        counter++;
                        result[i] = result[i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (var z = 0; z < i; z++)
                {
                    if (arr[z] <= arr[i])
                    {
                        counter++;
                        result[i] = result[i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return result;
        }

        /*
            Rotational Cipher
            One simple way to encrypt a string is to "rotate" every alphanumeric character by a certain amount. 
            Rotating a character means replacing it with another character that is a certain number of steps away in normal alphabetic or numerical order.
            For example, if the string "Zebra-493?" is rotated 3 places, the resulting string is "Cheud-726?". 
            Every alphabetic character is replaced with the character 3 letters higher (wrapping around from Z to A), and every numeric character replaced with the character 3 digits higher (wrapping around from 9 to 0). 
            Note that the non-alphanumeric characters remain unchanged.
            Given a string and a rotation factor, return an encrypted string.
            Signature
            string rotationalCipher(string input, int rotationFactor)
            Input
            1 <= |input| <= 1,000,000
            0 <= rotationFactor <= 1,000,000
            Output
            Return the result of rotating input a number of times equal to rotationFactor.
            
            Example 1
            input = Zebra-493?
            rotationFactor = 3
            output = Cheud-726?
            
            Example 1
            input = abcdefghijklmNOPQRSTUVWXYZ0123456789
            rotationFactor = 39
            output = nopqrstuvwxyzABCDEFGHIJKLM9012345678
        */
        public string rotationalCipher(string input, int rotationFactor)
        {
            // Write your code here
            var letters = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            var numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            var encrypt = new StringBuilder("");

            for (var i = 0; i < input.Length; i++)
            {
                var val = input[i].ToString();
                if (letters.Contains(val.ToUpper()))
                {
                    var index = letters.IndexOf(val.ToUpper());
                    var newIndex = index + rotationFactor;
                    if (newIndex >= letters.Count)
                    {
                        newIndex = newIndex - letters.Count;
                    }
                    encrypt.Append(char.IsUpper(input[i]) ? letters[newIndex] : letters[newIndex].ToLower());
                }
                else if (numbers.Contains(val))
                {
                    var index = numbers.IndexOf(val);
                    var newIndex = index + rotationFactor;
                    if (newIndex >= numbers.Count)
                    {
                        newIndex = newIndex - numbers.Count;
                    }
                    encrypt.Append(numbers[newIndex]);
                }
                else
                {
                    encrypt.Append(val);
                }
            }
            return encrypt.ToString();
        }
    }
}