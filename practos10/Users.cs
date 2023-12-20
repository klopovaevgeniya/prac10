using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    enum tUserRole
    {
        roleAdmin = 1,
        roleKassir = 2,
        roleManager = 3,
        roleSklad = 4,
        roleBuh = 5,
        roleWrong = -1
    };

    class tUserData
    {
        public int ID;
        public string Login;
        public string Password;
        public tUserRole Role;

        public tUserData()
        {
            ID = 0;
            Login = "";
            Password = "";
            Role = tUserRole.roleAdmin;
        }
    }

    class tUser
    {
        public tUserData userData;
        public static string jsonFileName = "users.json";

        public void showInterface()
        {
            Console.Clear();
            Console.WriteLine($"\t\tДобро пожаловать, {userData.Login}!\t\t\tРоль: Администратор");
            Console.Write(string.Join("", Enumerable.Repeat("-", Console.WindowWidth)));
            for (int i = 0; i < 10; i++)
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Console.WindowWidth - 30)) + "|");

            string[] actionNames = { "  Возможные действия:   ",
                                     "                        ",
                                     "F1    - Добавить запись ",
                                     "F2    - Найти запись    ",
                                     "Enter - Читать/Изменить ",
                                     "Del   - Удалить запись  ",
                                     "Esc   - Выход           ",
                                     "                        "
                                   };

            for (int i = 0; i < actionNames.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 27, i + 2);
                Console.Write(actionNames[i]);
            }
        }
    }
}
