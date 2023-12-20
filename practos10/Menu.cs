using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    enum menuCodes
    {
        Esc = -1,
        F1 = -2,
        F2 = -3,
        F10 = -4,
        Del = -5,
        S = -6
    }

    class tMenu
    {
        public List<string> Items = new List<string>();
        public int curItem = 0;
        public int lx = 0, ty = 0;

        public tMenu(int _lx, int _ty, List<string> _Items)
        {
            Items = _Items;
            lx = _lx;
            ty = _ty;
        }

        void showCursor()
        {
            Console.SetCursorPosition(lx, ty + curItem);
            Console.Write("->");
        }

        void hideCursor()
        {
            Console.SetCursorPosition(lx, ty + curItem);
            Console.Write("  ");
        }

        void nextItem()
        {
            hideCursor();
            if (curItem < Items.Count - 1)
                curItem++;
            else
                curItem = 0;
            showCursor();
        }

        void prevItem()
        {
            hideCursor();
            if (curItem > 0)
                curItem--;
            else
                curItem = Items.Count - 1;
            showCursor();
        }

        void firstItem()
        {
            hideCursor();
            curItem = 0;
            showCursor();
        }

        void lastItem()
        {
            hideCursor();
            curItem = Items.Count - 1;
            showCursor();
        }

        public int runMenu()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Console.SetCursorPosition(lx, ty + i);
                Console.Write("  " + Items[i]);
            }

            ConsoleKeyInfo choice;
            bool stop = false;
            int res = curItem;
            bool cursVis = Console.CursorVisible;
            Console.CursorVisible = false;

            showCursor();
            do
            {
                choice = Console.ReadKey(true);
                switch (choice.Key)
                {
                    case ConsoleKey.UpArrow:
                        prevItem();
                        break;
                    case ConsoleKey.DownArrow:
                        nextItem();
                        break;
                    case ConsoleKey.Home:
                        firstItem();
                        break;
                    case ConsoleKey.End:
                        lastItem();
                        break;
                    case ConsoleKey.Enter:
                        res = curItem;
                        stop = true;
                        break;
                    case ConsoleKey.Escape:
                        res = -1;
                        stop = true;
                        break;
                    case ConsoleKey.F1:
                        res = (int)menuCodes.F1;
                        stop = true;
                        break;
                    case ConsoleKey.F2:
                        res = (int)menuCodes.F2;
                        stop = true;
                        break;
                    case ConsoleKey.F10:
                        res = (int)menuCodes.F10;
                        stop = true;
                        break;
                    case ConsoleKey.Delete:
                        res = (int)menuCodes.Del;
                        stop = true;
                        break;
                    case ConsoleKey.S:
                        res = (int)menuCodes.S;
                        stop = true;
                        break;
                }

            } while (!stop);

            Console.CursorVisible = cursVis;
            return res;
        }

    }
}
