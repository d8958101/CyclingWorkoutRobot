using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CyclingWorkoutRobot.Models;

namespace CyclingWorkoutRobot
{
    public static class Params
    {
        public static string langCodeId = "defaultlanguage";
        public static string langCodeType = "param";
        public static LanguageOption defaultLanguage =
           Common.LanguageOptionStringToEnum(System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"]);
        public static string codeTypeFTP = "UserFTP";
        public static string codeIdHideBrowser = "hideBrowserOption";
        public static string codeTypeHideBrowser = "param";
        public static string codeIdRememberIdPwd = "rememberIdPwdOption";
        public static string codeTypeRememberIdPwd = "param";
        public static string codeIdLoginIdPwd = "UserLoginIdPwd";
        public static string codeTypeAutoLogin = "AutoLogin";
    }
}
