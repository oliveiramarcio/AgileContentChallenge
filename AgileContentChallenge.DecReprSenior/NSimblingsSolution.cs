using System;

namespace AgileContentChallenge.DecReprSenior
{
    public static class NSimblingsSolution
    {
        public static int Solution(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n");
            }
            else if (n > 100000000)
            {
                return -1;
            }

            char[] nArray = n.ToString().ToCharArray();
            Array.Sort(nArray);
            Array.Reverse(nArray);
            return Convert.ToInt32(string.Join("", nArray));
        }
    }
}