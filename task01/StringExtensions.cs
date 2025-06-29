using System;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            input = input.ToLower();
            string str_new = string.Empty;

            foreach (char chr in input)
            {
                if (!char.IsPunctuation(chr) && !char.IsWhiteSpace(chr))
                    str_new += chr;
            }

            string str_compare = new string(str_new.Reverse().ToArray());
            return str_compare == str_new;
        }
    }
}