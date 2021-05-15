namespace interview_code.DesingClassInterviews
{ 
    public class Test101
    {
        private int _value;

        public Test101(int value)
        {
            _value = value;
        }

        public int Sum(Test101 obj, int newValue)
        {
            InternalSum(newValue);
            return obj._value;
        }

        private void InternalSum(int val)
        {
            this._value += val;
        }
    }
}