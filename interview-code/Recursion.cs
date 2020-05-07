using System;

namespace interview_code
{
    public class Recursion{

        public int GetFibNumberAtIndex(int index){            
            if(index == 0 || index == 1){
                return 1;
            }
            var result = GetFibNumberAtIndex(index - 2) + GetFibNumberAtIndex(index -1);
            
            return result;
        }

        public int PositivePower(int n, int power){
            if(n == 0){
                return 0;
            }
            if(power == 0){
                return 1;
            }

            var halfPower = power/2;
            var power1 = PositivePower(n, (int)Math.Floor((double)halfPower));

            if(power%2==0){
                return power1 * power1;                
            }
            else{
                return power1 * power1 * n;
            }                   
        }
    }
}