using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{

    class Program
    {
        public static void showHeader()
        {
            Console.Clear();
            string title = "Добро пожаловать в магазин!";
            Console.WriteLine(title.PadLeft(Console.WindowWidth / 2 + title.Length / 2));
            Console.Write(string.Join("", Enumerable.Repeat("-", Console.WindowWidth)));
        }

        static void Main(string[] args)
        {
            bool stop = false;
            int key = -1;
            string curLogin = "";
            string curPassword = "";

            List<string> mainMenuItems = new List<string> { "Логин  : ",
                                                            "Пароль : ",
                                                            "Авторизоваться",
                                                            "Выход"};
            tMenu mainMenu = new tMenu(0, 2, mainMenuItems);
            Authorization.init();

            showHeader();

            do // Главный цикл программы
            {
                key = mainMenu.runMenu();
                switch (key)
                {
                    case 0:
                        curLogin = Authorization.getLogin(2 + mainMenu.Items[key].Length,
                                                          mainMenu.ty + key, 20, curLogin);
                        break;

                    case 1:
                        curPassword = Authorization.getPassword(2 + mainMenu.Items[key].Length,
                                                                mainMenu.ty + key, 20, curPassword);
                        break;

                    case 2:
                        tUserData curUserData = Authorization.chkUserParams(curLogin, curPassword);
                        if (curUserData.Role == tUserRole.roleWrong)
                        {
                            Console.SetCursorPosition(0, 10);
                            Console.Write("  Неверное имя пользователя или пароль.\n" +
                                          "  Нажмите любую клавишу для продолжения...");
                            Console.ReadKey(true);
                            Console.SetCursorPosition(0, 10);
                            Console.Write("                                       \n" +
                                          "                                          ");
                        }
                        else
                        {
                            switch (curUserData.Role)
                            {
                                case tUserRole.roleAdmin:
                                    tAdmin adm = new tAdmin(curUserData);
                                    adm.runActions();
                                    showHeader();
                                    break;
                                case tUserRole.roleKassir:
                                    break;
                                case tUserRole.roleManager:
                                    break;
                                case tUserRole.roleSklad:
                                    break;
                                case tUserRole.roleBuh:
                                    break;
                            }
                            curLogin = "";
                            curPassword = "";
                        }
                        break;

                    case 3:
                        stop = true;
                        break;
                }
            } while (!stop);
        }
    }
}
