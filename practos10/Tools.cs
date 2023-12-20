using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    static class Tools
    {
        static string validChars = "0123456789" +
                                   "-_. " +
                                   "abcdefghijklmnopqrstuvwxyz" +
                                   "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                   "абвгдеёжзийклмнопрстуфхцчшщъыьэюя" +
                                   "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        public static string readString(int lx, int ty, int maxLen, string oldValue, string passChar)
        {
            string res = oldValue;
            bool oldCursorVis = Console.CursorVisible;
            ConsoleKeyInfo choice;

            while (true)
            {
                Console.SetCursorPosition(lx, ty);
                if (passChar == "")
                    Console.Write(res);
                else
                    Console.Write(string.Join("", Enumerable.Repeat(passChar, res.Length)));

                choice = Console.ReadKey(true);
                if ((choice.Key == ConsoleKey.Backspace) && (res.Length > 0))
                {
                    res = res.Substring(0, res.Length - 1);
                    Console.SetCursorPosition(lx + res.Length, ty);
                    Console.Write(" ");
                }
                else if (choice.Key == ConsoleKey.Escape)
                {
                    Console.SetCursorPosition(lx, ty);
                    Console.Write(string.Join("", Enumerable.Repeat(" ", res.Length)));
                    Console.SetCursorPosition(lx, ty);
                    Console.Write(oldValue);
                    res = oldValue;
                    break;
                }
                else if (choice.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (validChars.Contains(choice.KeyChar))
                {
                    if (maxLen == 0 || (maxLen > 0 && res.Length < maxLen))
                        res += choice.KeyChar;
                }
            }

            Console.CursorVisible = oldCursorVis;
            return res;
        }

    }
}
