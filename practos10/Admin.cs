using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    enum runModes
    {
        modeRead = 1,
        modeCreate = 2,
        modeSearch = 3,
        modeNone = 4
    }

    class tAdmin : tUser, iCRUDS
    {
        List<tUserData> userList = new List<tUserData>();
        List<string> menuItems = new List<string> { "ID     : ", "Логин  : ", "Пароль : ", "Роль   : " };
        tMenu usersMenu;
        runModes curMode;

        public tAdmin(tUserData _userData)
        {
            userData = _userData;
        }

        public void Create()
        {
            tUserData tmpUser = new tUserData();
            showUserInterface();
            curMode = runModes.modeCreate;
            setUserData(tmpUser);
        }

        public void Read()
        {
            showUserInterface();
            curMode = runModes.modeRead;
            setUserData(userList[usersMenu.curItem]);
        }

        public void Update()
        {
            switch (curMode)
            {
                case runModes.modeCreate:
                    userList.Add(userData);
                    break;
                case runModes.modeRead:
                    userList[usersMenu.curItem] = userData;
                    break;
            }
            Converter.doSerialize(userList, jsonFileName);
        }

        public void Delete()
        {
            userList.RemoveAt(usersMenu.curItem);
            Converter.doSerialize(userList, jsonFileName);
        }

        public void Search()
        {
            showSearchInterface();

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Выберите пункт, по которому Вы хотите произвести поиск:");
            tMenu searchMenu = new tMenu(0, 4, menuItems);

            int searchKey = searchMenu.runMenu();
            Console.SetCursorPosition(0, 9);
            Console.WriteLine("Введите значение для поиска:");
            string searchVlaue = Tools.readString(0, 10, 20, "", "");

            userData.ID = searchKey == 0 ? int.Parse(searchVlaue) : -1;
            userData.Login = searchKey == 1 ? searchVlaue : "";
            userData.Password = searchKey == 2 ? searchVlaue : "";
            userData.Role = searchKey == 3 ? (tUserRole)int.Parse(searchVlaue) : tUserRole.roleWrong;

            curMode = runModes.modeSearch;
        }

        public void showSearchInterface()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 27, i + 2);
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", 24)));
            }

            int rows = userList.Count < 5 ? 5 : userList.Count;
            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Console.WindowWidth - 30)));
            }
            Console.SetCursorPosition(0, 0);
        }

        public void showUserInterface()
        {
            string[] actionNames = { "1 - Администратор       ",
                                     "2 - Кассир              ",
                                     "3 - Кадровик            ",
                                     "4 - Склад-менеджер      ",
                                     "5 - Бухгалтер           ",
                                     "                        ",
                                     "S - Сохранить изменения ",
                                     "Esc - Выход             "
                                   };
            for (int i = 0; i < actionNames.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - 27, i + 2);
                Console.Write(actionNames[i]);
            }

            int rows = userList.Count < 5 ? 5 : userList.Count;
            for (int i = 0; i < rows; i++)
            {
                Console.SetCursorPosition(0, i + 2);
                Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Console.WindowWidth - 30)));
            }
            Console.SetCursorPosition(0, 0);
        }

        public void setUserData(tUserData _tmpUser)
        {
            int _key = -1;
            bool stop = false;
            tMenu fieldsMenu = new tMenu(0, 2, menuItems);

            do
            {
                _key = fieldsMenu.runMenu();
                switch (_key)
                {
                    case 0: // ID
                        try
                        {
                            _tmpUser.ID = int.Parse(Tools.readString(11, 2, 5, _tmpUser.ID.ToString(), ""));
                        }
                        catch { }
                        break;

                    case 1: // Логин
                        _tmpUser.Login = Tools.readString(11, 3, 20, _tmpUser.Login, "");
                        break;

                    case 2: // Пароль
                        _tmpUser.Password = Tools.readString(11, 4, 20, _tmpUser.Password, "");
                        break;

                    case 3: // Роль
                        try
                        {
                            _tmpUser.Role = (tUserRole)int.Parse(Tools.readString(11, 5, 5, ((int)_tmpUser.Role).ToString(), ""));
                        }
                        catch { }
                        break;

                    case (int)menuCodes.S:
                        userData = _tmpUser;
                        Update();
                        break;

                    case (int)menuCodes.Esc:
                        stop = true;
                        break;
                }
            } while (!stop);
        }

        public void makeUsersMenu()
        {
            userList = Converter.doDeserialize<List<tUserData>>(jsonFileName);
            List<string> usersMenuItems = new List<string> { };

            foreach (tUserData _user in userList)
            {
                if (
                    (curMode == runModes.modeSearch && userData.ID != -1 && userData.ID == _user.ID) ||
                    (curMode == runModes.modeSearch && userData.Login != "" && userData.Login == _user.Login) ||
                    (curMode == runModes.modeSearch && userData.Password != "" && userData.Password == _user.Password) ||
                    (curMode == runModes.modeSearch && userData.Role != tUserRole.roleWrong && userData.Role == _user.Role) ||
                    (curMode != runModes.modeSearch)
                   )
                {
                    string tmp = "\t" + _user.ID.ToString() +
                                 "\t\t" + _user.Login +
                                 "\t\t" + _user.Password + "\t\t";
                    switch (_user.Role)
                    {
                        case tUserRole.roleAdmin:
                            tmp += "Администратор";
                            break;
                        case tUserRole.roleKassir:
                            tmp += "Кассир";
                            break;
                        case tUserRole.roleManager:
                            tmp += "Кадровик";
                            break;
                        case tUserRole.roleSklad:
                            tmp += "Склад-менеджер";
                            break;
                        case tUserRole.roleBuh:
                            tmp += "Бухгалтер";
                            break;
                    }
                    usersMenuItems.Add(tmp);
                }
            }
            usersMenu = new tMenu(0, 3, usersMenuItems);
        }

        public void runActions()
        {
            int ch = -1;
            bool stop = false;

            do
            {
                showInterface();
                makeUsersMenu();

                Console.SetCursorPosition(0, 2);
                Console.WriteLine("\tID\t\tЛогин\t\tПароль\t\tРоль");

                ch = usersMenu.runMenu();

                if (ch >= 0 && ch < usersMenu.Items.Count)
                    Read();
                else if (ch == (int)menuCodes.F1)
                    Create();
                else if (ch == (int)menuCodes.F2)
                    Search();
                else if (ch == (int)menuCodes.Del)
                    Delete();
                else if (ch == (int)menuCodes.Esc)
                    if (curMode == runModes.modeSearch)
                        curMode = runModes.modeNone;
                    else
                        stop = true;

            } while (!stop);
        }
    }
}
