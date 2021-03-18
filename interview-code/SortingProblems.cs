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
        public int BinarySearch(int[] arr, int l, int r, int x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2; // handle overflow when left and right are large numbers

                // If the element is present at the 
                // middle itself 
                if (arr[mid] == x)
                    return mid;

                // If element is smaller than mid, then 
                // it can only be present in left subarray 
                if (arr[mid] > x)
                    return BinarySearch(arr, l, mid - 1, x);

                // Else the element can only be present 
                // in right subarray 
                return BinarySearch(arr, mid + 1, r, x);
            }

            // We reach here when element is not present 
            // in array 
            return -1;
        }
        
        /*
         * sort by using a pivot, left elements are smaller than pivot
         * right element are bigger than pivot
         */
        public void QuickSort(int[] arr, int start, int end)
        {
            if (start < end)
            {
                //stores the position of pivot element
                int piv_pos = Partition(arr, start, end);
                QuickSort(arr, start, piv_pos - 1); //sorts the left side of pivot.
                QuickSort(arr, piv_pos + 1, end); //sorts the right side of pivot.
            }
        }

        private int Partition(int[] arr, int start, int end)
        {
            // make the first element as pivot element.
            int piv = arr[start];

            // i pointer does not include the pivot
            int i = start + 1;

            for (int j = start + 1; j <= end; j++)
            {
                /* rearrange the array by putting elements which are less than pivot
                 * on one side and which are greater that on other. 
                 */

                if (arr[j] < piv)
                {
                    var temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    i += 1;
                }
            }

            // put the pivot element in its proper place.
            var temp1 = arr[i - 1];
            arr[i - 1] = arr[start];
            arr[i - 1] = temp1;

            // return the position of the pivot
            return i - 1;
        }
        
        private int PartitionMiddle(int[] arr, int left, int right)
        {
            int i = left, j = right;
            int tmp;
            int pivot = arr[(left + right) / 2];
    
            while (i <= j)
            {
                while (arr[i] < pivot)
                    i++;

                while (arr[j] > pivot)
                    j--;

                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
            
                    i++;
                    j--;
                }
            }
            return i;
        }
    }
}