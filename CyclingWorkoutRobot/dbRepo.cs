using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingWorkoutRobot
{
    public static class dbRepo
    {
        #region language related

        public static string GetDefaultLanguageFromDB()
        {
            string result = "";
            using (mydbEntities db = new mydbEntities())
            {
                var userSelectDefaultLanguageObj = db.tdcode
                    .Where(t => t.code_id == Params.langCodeId && t.code_type == Params.langCodeType).FirstOrDefault();
                if (userSelectDefaultLanguageObj != null)
                {
                    result = userSelectDefaultLanguageObj.code_value;

                }
            }
            return result;
        }

        public static void InserUpdateDefaultLanguage(string userSelectLanguage)
        {
            using (mydbEntities db = new mydbEntities())
            {
                //check if user select a default language
                var userSelectLanguageObj = db.tdcode
                    .Where(t => t.code_id == Params.langCodeId && t.code_type == Params.langCodeType).FirstOrDefault();
                if (userSelectLanguageObj != null)
                {
                    //data exist: update data
                    userSelectLanguageObj.code_value = userSelectLanguage;

                    //defaultLanguage = Common.LanguageOptionStringToEnum(userSelectLanguageObj.code_value);
                }
                else
                {
                    //data not exist: insert data
                    tdcode langSetting = new tdcode();
                    langSetting.code_id = Params.langCodeId;
                    langSetting.code_type = Params.langCodeType;
                    langSetting.code_value = userSelectLanguage;
                    db.tdcode.Add(langSetting);
                }
                db.SaveChanges();

            }
        }

        public static translate GetTranslateObj(string engMsg)
        {
            using (mydbEntities db = new mydbEntities())
            {
                return db.translate.Where(t => t.eng == engMsg).FirstOrDefault();
            }
        }

        #endregion

        #region FTP related 

        //public static string codeTypeFTP = "UserFTP";

        public static void InserUpdateUserFTP(string userId, string newFTP)
        {

            using (mydbEntities db = new mydbEntities())
            {
                var userFTP = db.tdcode.Where(t => t.code_id == userId && t.code_type == Params.codeTypeFTP).FirstOrDefault();
                if (userFTP == null)
                {
                    //insert user FTP
                    tdcode codeNew = new tdcode();
                    codeNew.code_id = userId;
                    codeNew.code_type = Params.codeTypeFTP;
                    codeNew.code_value = newFTP;

                    db.tdcode.Add(codeNew);
                    db.SaveChanges();
                }
                else
                {
                    //update user FTP
                    userFTP.code_value = newFTP;
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteUserFTP(string userId)
        {
            using (mydbEntities db = new mydbEntities())
            {
                var userFTP = db.tdcode.Where(t => t.code_id == userId && t.code_type == Params.codeTypeFTP).FirstOrDefault();
                if (userFTP != null)
                {
                    db.tdcode.Remove(userFTP);
                    db.SaveChanges();
                }

            }
        }

        public static int GetUserFTP(string userId)
        {
            int ftp = 0;
            using (mydbEntities db = new mydbEntities())
            {
                var userFTP = db.tdcode.Where(t => t.code_id == userId && t.code_type == Params.codeTypeFTP).FirstOrDefault();
                if (userFTP != null)
                {
                    ftp = Convert.ToInt16(userFTP.code_value);
                }

            }
            return ftp;
        }

        #endregion

        #region Hide Browser option related



        public static void InserUpdateHideBrowserOption(bool isHide)
        {


            using (mydbEntities db = new mydbEntities())
            {
                var hideBrowserOption = db.tdcode.Where(t => t.code_id == Params.codeIdHideBrowser
                && t.code_type == Params.codeTypeHideBrowser).FirstOrDefault();
                if (hideBrowserOption == null)
                {
                    //insert hide browser option
                    tdcode codeNew = new tdcode();
                    codeNew.code_id = Params.codeIdHideBrowser;
                    codeNew.code_type = Params.codeTypeHideBrowser;
                    codeNew.code_value = isHide.ToString();

                    db.tdcode.Add(codeNew);
                    db.SaveChanges();
                }
                else
                {
                    //update hide browser option
                    hideBrowserOption.code_value = isHide.ToString();
                    db.SaveChanges();
                }
            }
        }

        public static void InserRememberIdPwdOption(bool isRemember)
        {


            using (mydbEntities db = new mydbEntities())
            {
                var rememberIdPwdOption = db.tdcode.Where(t => t.code_id == Params.codeIdRememberIdPwd
               && t.code_type == Params.codeTypeRememberIdPwd).FirstOrDefault();
                if (rememberIdPwdOption == null)
                {
                    //insert hide browser option
                    tdcode codeNew = new tdcode();
                    codeNew.code_id = Params.codeIdRememberIdPwd;
                    codeNew.code_type = Params.codeTypeRememberIdPwd;
                    codeNew.code_value = isRemember.ToString();

                    db.tdcode.Add(codeNew);
                    db.SaveChanges();
                }
                else
                {
                    //update hide browser option
                    rememberIdPwdOption.code_value = isRemember.ToString();
                    db.SaveChanges();
                }
            }
        }

        public static bool GetHideBrowserOption()
        {
            bool isHide = false;
            using (mydbEntities db = new mydbEntities())
            {
                var hideBrowserOption = db.tdcode.Where(t => t.code_id == Params.codeIdHideBrowser
                && t.code_type == Params.codeTypeHideBrowser).FirstOrDefault();
                if (hideBrowserOption != null)
                {
                    isHide = Convert.ToBoolean(hideBrowserOption.code_value);
                }

            }
            return isHide;
        }

        public static bool GetRememberIdPwdOption()
        {
            bool isRemember = false;
            using (mydbEntities db = new mydbEntities())
            {
                var rememberIdPwdOption = db.tdcode.Where(t => t.code_id == Params.codeIdRememberIdPwd
                && t.code_type == Params.codeTypeRememberIdPwd).FirstOrDefault();
                if (rememberIdPwdOption != null)
                {
                    isRemember = Convert.ToBoolean(rememberIdPwdOption.code_value);
                }

            }
            return isRemember;
        }

        public static void InsertUserLoginIdPwdToDB(string unEncryptedId, string unEncryptedPwd)
        {
            string codeId = Params.codeIdLoginIdPwd;// "UserLoginIdPwd";
            string codeType = Params.codeTypeAutoLogin;// "AutoLogin";
            string notEncryptedStr = unEncryptedId + "," + unEncryptedPwd;
            string encryptedStr = Common.CryptEncryptString(notEncryptedStr);

            using (mydbEntities db = new mydbEntities())
            {
                var loginObj = db.tdcode.Where(t => t.code_id == codeId && t.code_type == codeType).FirstOrDefault();

                //if not exist => insert
                if (loginObj == null)
                {
                    tdcode param = new tdcode();
                    param.code_id = codeId;
                    param.code_type = codeType;
                    param.code_value = encryptedStr;

                    db.tdcode.Add(param);
                }
                else
                {
                    //if exists => update
                    loginObj.code_value = encryptedStr;

                }


                db.SaveChanges();
            }
        }

        public static void DeleteUserIdPwdFromDb()
        {
            string codeId = Params.codeIdLoginIdPwd;// "UserLoginIdPwd";
            string codeType = Params.codeTypeAutoLogin;// "AutoLogin";
           

            using (mydbEntities db = new mydbEntities())
            {
                var loginObj = db.tdcode.Where(t => t.code_id == codeId && t.code_type == codeType).FirstOrDefault();

               
                if (loginObj != null)
                {     
                    db.tdcode.Remove(loginObj);
                    db.SaveChanges();
                }
               
               
            }
        }

        #endregion
    }
}
