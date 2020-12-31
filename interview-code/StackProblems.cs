using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace interview_code
{
    public class StackProblems
    {
        public int? FindNumber(Stack<int> stack, int number)
        {
            var tempStack = new Stack<int>();
            int? result = null;
            while (stack.Count != 0)
            {
                var val = stack.Pop();
                if (val == number)
                {
                    result = val;
                    break;
                }
                tempStack.Push(val);
            }

            while (tempStack.Count != 0)
            {
                var val = tempStack.Pop();
                stack.Push(val);
            }

            return result;
        }

        public int EvaluateArithmeticOperation(string[] operation)
        {
            var operators = new Stack<char>();
            var operands = new Stack<int>();
            // loop over the operation
            for (var i = 0; i < operation.Length; i++)
            {
                // validate if the element is operator
                if (operation[i] == "*" || operation[i] == "/" || operation[i] == "+" || operation[i] == "-")
                {
                    while (operators.Count != 0 && GetPrecedence(operators.Peek()) >= GetPrecedence(char.Parse(operation[i])))
                    {
                        Process(ref operands, ref operators);
                    }
                    operators.Push(char.Parse(operation[i]));
                }
                // push into an operand stack
                else
                {
                    operands.Push(int.Parse(operation[i]));
                }
            }
            while (operators.Count != 0)
            {
                Process(ref operands, ref operators);
            }
            return operands.Pop();
        }

        // validate if the element is an operator
        // --- push into operators stack
        // ------ if operator >= than operator top on stack
        //           operate pop(2) operands stack, and apply operand
        //           push result into operans stack
        private void Process(ref Stack<int> operands, ref Stack<char> operators)
        {

            var op = operators.Pop();
            var operand2 = operands.Pop();
            var operand1 = operands.Pop();
            var result = ExecuteOperation(op, operand1, operand2);
            operands.Push(result);
        }

        private int GetPrecedence(char o)
        {
            if (o == '*' || o == '/')
            {
                return 2;
            }
            if (o == '+' || o == '-')
            {
                return 1;
            }
            return -1;
        }

        private int ExecuteOperation(char o, int op1, int op2)
        {
            var result = -1;
            switch (o)
            {
                case '*':
                    result = op1 * op2;
                    break;
                case '/':
                    result = op1 / op2;
                    break;
                case '+':
                    result = op1 + op2;
                    break;
                case '-':
                    result = op1 - op2;
                    break;
            }
            return result;
        }

    }

    public class StackQueue
    {
        private Stack<int> s1;
        private Stack<int> s2;

        public StackQueue()
        {
            s1 = new Stack<int>();
            s2 = new Stack<int>();
        }

        public void Enqueue(int item)
        {
            s1.Push(item);
        }

        public int Dequeue()
        {
            if (s2.Count == 0)
            {
                flushToS2();
            }
            if (s2.Count == 0)
            {
                throw new Exception("Empty queue");
            }
            return s2.Pop();
        }

        private void flushToS2()
        {
            while (s1.Count!=0)
            {
                var val = s1.Pop();
                s2.Push(val);
            }
        }
    }
}