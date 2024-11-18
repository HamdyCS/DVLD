using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    static class clsSetting
    {
        public struct stUserInfo
        {
            public int UserID;
            public int PersonID;

            public string UserName;
            public string Password;

            public bool IsActive;
        };

        public static stUserInfo UserInfo = new stUserInfo();

        public static bool CloseApp = true;

    }
}
