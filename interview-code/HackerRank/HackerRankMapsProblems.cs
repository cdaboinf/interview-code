using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    /*
     * FreqQuery
     *
     * You are given  queries. Each query is of the form two integers described below:
     * - 1 x: Insert x in your data structure.
     * - 2 y: Delete one occurence of y from your data structure, if present.
     * - 3 z: Check if any integer is present whose frequency is exactly . If yes, print 1 else 0.
     * The queries are given in the form of a 2-D array queries of size q where queries[i][0]
     * contains the operation and queri[i][1], contains the data element.
     */
    public class HackerRankMapsProblems
    {
        //[i][0] = operation
        //[i][1] = element
        public List<int> FreqQuery(List<List<int>> queries)
        {
            var data = new List<int>();
            var result = new List<int>();
            var elements = new Hashtable();

            if (queries.Count > Math.Pow(10, 5))
            {
                return result;
            }

            foreach (var query in queries)
            {
                var operation = query[0];
                var operand = query[1];

                if (operand > Math.Pow(10, 9) || operand < 1 || operation < 1 || operation > 3)
                {
                    continue;
                }

                switch (operation)
                {
                    case 1:
                        data.Add(operand);
                        if (elements.Count > 0 && elements.ContainsKey(operand))
                        {
                            elements[operand] = (int) elements[operand] + 1;
                        }
                        else
                        {
                            elements.Add(operand, 1);
                        }

                        break;
                    case 2:
                        if (data.Contains(operand))
                        {
                            data.Remove(operand);

                            if (elements.Count > 0 && elements.ContainsKey(operand))
                            {
                                var value = (int) elements[operand] - 1;
                                if (value == 0)
                                {
                                    elements.Remove(operand);
                                }
                                else
                                {
                                    elements[operand] = (int) elements[operand] - 1;
                                }
                            }
                        }

                        break;
                    case 3:
                        result.Add(elements.ContainsValue(operand) ? 1 : 0);
                        break;
                }
            }

            return result;
        }
    }
}