using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace interview_code
{
    public class UnityTest
    {
        public UnityTest()
        {
        }
        
        public bool Solution(int[] A)
        {
            var start = 0;
            var end = A.Length - 1;

            var lowSum = 0;
            var highSum = 0;

            // determine two workers tasks
            for (var i = 0; i < A.Length; i++)
            {
                lowSum += A[start];
                highSum += A[end];

                if (lowSum > highSum)
                {
                    end--;
                }
                else if (lowSum < highSum)
                {
                    start++;
                }
                else
                {
                    break;
                }
            }

            // third worker task
            for (var j = start + 1; j < end + 1; j++)
            {
                if (A[j] + A[j + 1] == lowSum)
                {
                    return true;
                }
            }

            return false;
        }

        public int solution1(int[,] A)
        {
            // keep track of existing countries
            var countries = new HashSet<int>();
            var totalCountryCount = 0;

            // getting upper bound
            int uBound0 = A.GetUpperBound(0);
            int uBound1 = A.GetUpperBound(1);

            for (int row = 0; row < uBound0; row++)
            {
                for (int col = 0; col < uBound1; col++)
                {
                    if ((col + 1 < uBound0) && A[row, col] != A[row, col + 1]
                        || (row - 1 >= 0) && A[row, col] != A[row - 1, col]
                        || (row + 1 < A.Length) && A[row, col] != A[row + 1, col]
                        || (col - 1 >= 0) && A[row, col] != A[row, col - 1])
                    {
                        // increase count only for new a new country
                        if (!countries.Contains(A[row, col]))
                        {
                            totalCountryCount++;
                        }
                        else
                        {
                            countries.Add(A[row, col]);
                        }
                    }

                }
            }
            return totalCountryCount;
        }
    }
}