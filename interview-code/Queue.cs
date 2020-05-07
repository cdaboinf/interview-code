using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace interview_code
{
    public class Queue
    {
        public Queue(int lenght)
        {
            Container = new int[lenght];
            Lenght = lenght;

            List = new System.Collections.Generic.Queue<Price>();
        }
        public void SimulateArrayQueueEnqueue(int n)
        {
            if (Container.Length == Lenght)
            {
                throw new Exception("queue is full");
            }
            Container[Back] = n;
            Back = (Back + 1) % Container.Length;
            Lenght++;
        }

        public int SimulateArrayQueueDequeue()
        {
            if (Lenght == 0)
            {
                throw new Exception("queue is empty");
            }
            var value = Container[Front];
            Front = (Front + 1) % Container.Length;
            Lenght--;

            return value;
        }

        public void GetSumOfSlindingWindow(int[] arr, int windowSize)
        {
            // queue to keep track of elements of the window
            var queue = new System.Collections.Queue();
            var sum = 0;
            for (var i = 0; i < windowSize; i++)
            {
                queue.Enqueue(arr[i]);
                sum = sum + arr[i];
            }

            Console.Write(sum + ", ");

            for (var i = windowSize; i < arr.Length; i++)
            {
                var val = queue.Dequeue();
                sum = sum - (int)val;
                queue.Enqueue(arr[i]);
                sum = sum + arr[i];
                Console.Write(sum + ", ");
            }

            /*
                if (a == null || k == 0 || a.length == 0)        
                    return; // confirm with interviewer what to do for this case    
                // LinkedList implements Queue interface in Java    
                Queue<Integer> q = new LinkedList<>();    
                int sum = 0;    
                for (int i = 0; i < a.length; i++) {        
                    if (q.size() == k) {            
                        int last = q.remove();            
                        sum -= last;        
                    }        
                    q.add(a[i]);        
                    sum += a[i];        
                    if (q.size() == k) {            
                        System.out.println(sum);        
                    }    
                }
            */
        }

        public void AddPrice(int day, int price)
        {
            while (List.Count != 0 && List.Peek().getDay() < (day - Lenght + 1))
                List.Dequeue();
            List.Enqueue(new Price(price, day));
        }

        public int GetMax()
        {
            int maxPrice = 0;
            foreach (var item in List)
            {
                int price = item.getPrice();
                if (price > maxPrice)
                    maxPrice = price;
            }
            return maxPrice;
        }

        private int[] Container;

        private System.Collections.Generic.Queue<Price> List;

        private int Lenght;

        private int Front;

        private int Back;
    }

    public class Price
    {
        int price;
        int day;
        public Price(int price, int day)
        {
            this.price = price;
            this.day = day;
        }
        public int getPrice()
        {
            return price;
        }
        public int getDay()
        {
            return day;
        }
    }
}