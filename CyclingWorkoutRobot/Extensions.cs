using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static CyclingWorkoutRobot.Models;

namespace CyclingWorkoutRobot
{
    public static class Extensions
    {
      
   

        public static string ToDefaultLanguage(this string engMsg)
        {
            
            string defaultLangFromDB = dbRepo.GetDefaultLanguageFromDB();
            if (defaultLangFromDB != "")
            {
                Params.defaultLanguage = Common.LanguageOptionStringToEnum(defaultLangFromDB);
            }
            return engMsg.ToLanguage(Params.defaultLanguage);

        }

        public static string ToLanguage(this string engMsg, LanguageOption langOption)
        {
          
            string result = "";
            using (mydbEntities db = new mydbEntities())
            {
                var msgObj = db.translate.Where(t => t.eng == engMsg).FirstOrDefault();
                if (langOption == LanguageOption.English)
                {
                    result = msgObj.eng;
                }
                else if (langOption == LanguageOption.日本語)
                {
                    result = msgObj.jpn;
                }
                else if (langOption == LanguageOption.简体中文)
                {
                    result = msgObj.chs;
                }
                else if (langOption == LanguageOption.繁體中文)
                {
                    result = msgObj.cht;
                }
            }


            return result;
        }

       

    }
}
