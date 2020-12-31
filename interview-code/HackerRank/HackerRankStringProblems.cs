using System;
using System.Collections;

namespace interview_code
{
    public class HackerRankStringProblems
    {
        public int MakeAnagram(string a, string b) {
            var map1 = new Hashtable();
            var map2 = new Hashtable();

            foreach(char c in a){
                if(map1.ContainsKey(c)){
                    map1[c] = (int)map1[c] + 1;                
                }
                else{
                    map1.Add(c,1);            
                }
            }
            foreach(char c in b){
                if(map2.ContainsKey(c)){
                    map2[c] = (int)map2[c] + 1;                
                }
                else{
                    map2.Add(c,1);            
                }
            }

            var count = 0;
            foreach(char c in map1.Keys){
                if(!map2.ContainsKey(c)){
                    count+= (int)map1[c];
                }
                else{
                    count+= Math.Abs((int)map1[c] - (int)map2[c]);                
                }
            }
            foreach(char c in map2.Keys){
                if(!map1.ContainsKey(c)){
                    count+= (int)map2[c];
                }
            }
            return count;
        }
    }
}