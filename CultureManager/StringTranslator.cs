using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultureManager
{
    class StringTranslator
    {
        private static Dictionary<char, string> Chars;
        static StringTranslator()
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                Chars[c] = new(new[] { "𝓪"[0], (char)((int)"𝓪"[1] + (c - 'a')) });
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Chars[c] = new(new[] { "𝓐"[0], (char)((int)"𝓐"[1] + (c - 'A')) });
            }
        }




        string Translate(char c) => c switch
        {
            >= 'A' and <= 'Z' => Chars[c],
            >= 'a' and <= 'z' => Chars[c],
            >= '0' and <= '9' => $"{(char)((int)'⓪' + (c - '0'))}",
            _ => $"{c}",
        };

        string Translate(string s) => string.Concat(s.Select(Translate));

    }
}
