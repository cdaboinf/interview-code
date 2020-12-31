using System;
namespace interview_code
{
    public class Sorting
    {
        public Sorting()
        {
        }

        /*
         * Sort array ascending
         */
        public void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        // swap temp and arr[i] 
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }

        /*
         * MergeSort(arr[], l,  r)
            If r > l
            1. Find the middle point to divide the array into two halves:  
                    middle m = (l+r)/2
            2. Call mergeSort for first half:   
                    Call mergeSort(arr, l, m)
            3. Call mergeSort for second half:
                    Call mergeSort(arr, m+1, r)
            4. Merge the two halves sorted in step 2 and 3:
                    Call merge(arr, l, m, r)

            The merge() function is used for merging two halves.
         */
        public void MergeSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        // swap temp and arr[i] 
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }

        // Returns index of x if it is present in 
        // arr[l..r], else return -1 
        static int binarySearch(int[] arr, int l, int r, int x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;

                // If the element is present at the 
                // middle itself 
                if (arr[mid] == x)
                    return mid;

                // If element is smaller than mid, then 
                // it can only be present in left subarray 
                if (arr[mid] > x)
                    return binarySearch(arr, l, mid - 1, x);

                // Else the element can only be present 
                // in right subarray 
                return binarySearch(arr, mid + 1, r, x);
            }

            // We reach here when element is not present 
            // in array 
            return -1;
        }
    }
}
