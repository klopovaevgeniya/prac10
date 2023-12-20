using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ca_csIS
{
    static class Authorization
    {
        static string jsonFileName = tUser.jsonFileName;

        static Authorization()
        {
            if (!File.Exists(Converter.dataPath + jsonFileName))
                createDefaultAdmin();
        }

        public static void init() { }

        public static string getLogin(int lx, int ty, int maxLen, string oldValue)
        {
            string login = Tools.readString(lx, ty, maxLen, oldValue, "");
            return login;
        }

        public static string getPassword(int lx, int ty, int maxLen, string oldValue)
        {
            string password = Tools.readString(lx, ty, maxLen, oldValue, "*");
            return password;
        }

        public static void createDefaultAdmin()
        {
            tUserData defAccount = new tUserData();
            defAccount.ID = 0;
            defAccount.Login = "admin";
            defAccount.Password = "admin";
            defAccount.Role = tUserRole.roleAdmin;

            Converter.doSerialize(new List<tUserData>() { defAccount }, jsonFileName);
        }

        public static tUserData chkUserParams(string _login, string _password)
        {
            tUserData res = new tUserData();
            res.Role = tUserRole.roleWrong;

            List<tUserData> userList = new List<tUserData>();
            userList = Converter.doDeserialize<List<tUserData>>(jsonFileName);

            foreach (tUserData curAcc in userList)
                if (curAcc.Login == _login && curAcc.Password == _password)
                {
                    res = curAcc;
                    break;
                }
            return res;
        }
    }
}
