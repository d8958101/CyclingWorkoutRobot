using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CyclingWorkoutRobot.Extensions;
using static CyclingWorkoutRobot.Models;

namespace CyclingWorkoutRobot
{
    public static class Common
    {
        #region params

        //public static string codeIdHideBrowser = "hideBrowserOption";
        //public static string codeTypeHideBrowser = "param";
        //public static string codeIdRememberIdPwd = "rememberIdPwdOption";
        //public static string codeTypeRememberIdPwd = "param";
        #endregion


        public static LanguageOption LanguageOptionStringToEnum(string inputEnum)
        {
            foreach (LanguageOption val in Enum.GetValues(typeof(LanguageOption)))
            {
                if (inputEnum == val.ToString())
                {
                    return val;


                }

            }
            return LanguageOption.English;//發生錯誤找不到的話，就回傳英文的吧

        }

       

        public static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;
            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;
            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

               
        #region encryption decrption

        public static byte[] photo_encryption(byte[] photoBytes)
        {
            //宣告AES 256加密演算法
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            //key值通常是base64格式的字串, 因為一般人在定義key, iv的時候
            //都是任意輸入中文或是英文的字串
            //所以大多時候，key, iv都是將此中文或是英文字串先轉成base64格式
            //才儲存於檔案或是資料庫裡面

            //QVRNMzkzOTg4OQ== Convert出來是ATM3939889
            //ComputeHash()會幫ATM3939889湊滿長度=64的字串, 並轉成byte[]
            //為什麼要執行ComputeHash()呢？因為AES演算法的key必須由長度=64的字串
            //轉成byte[]才能進行
            //ps.如果把ATM3939889字串手動湊成剛好長度=64，即不用執行ComputeHash()
            //直接convert.tobyte[]即可
            byte[] key = sha256.ComputeHash(Convert.FromBase64String("QVRNMzkzOTg4OQ=="));
            //ODgyNTI1Mg== Convert出來是8825252
            //AES 256加密演算法的iv的道理類似於他的key
            //只是iv只需要長度16就好
            //ps.如果把8825252字串手動湊成剛好長度=16，即不用執行ComputeHash()
            //直接convert.tobyte[]即可
            byte[] iv = sha256.ComputeHash(Convert.FromBase64String("ODgyNTI1Mg=="));
            byte[] xx = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                xx[i] = iv[i];
            }

            aes.Key = key;
            aes.IV = xx;

            byte[] encryptPhotoDataBytes;
            using (MemoryStream msPhotoData = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(msPhotoData, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(photoBytes, 0, photoBytes.Length);
                cs.FlushFinalBlock();
                encryptPhotoDataBytes = new byte[msPhotoData.Length];
                msPhotoData.Position = 0;
                msPhotoData.Read(encryptPhotoDataBytes, 0, (int)msPhotoData.Length);
                msPhotoData.Close();
            }

            //Console.WriteLine("執行完畢!");
            return encryptPhotoDataBytes;

        }

        //由於加密跟解密的原理一樣，所以註解都寫在加密那邊，而這邊的解密沒有加註解
        //看完加密那邊的註解之後，解密這邊就會都瞭解囉
        public static byte[] photo_Decrypt(byte[] encryptBytes)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Convert.FromBase64String("QVRNMzkzOTg4OQ=="));
            byte[] iv = sha256.ComputeHash(Convert.FromBase64String("ODgyNTI1Mg=="));
            byte[] xx = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                xx[i] = iv[i];
            }

            aes.Key = key;
            aes.IV = xx;

            using (MemoryStream ms = new MemoryStream())
            {

                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))

                {
                    cs.Write(encryptBytes, 0, encryptBytes.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }

            }

        }

        public static string CryptEncryptString(string src)
        {
            string result = "";
            try
            {
                byte[] aesBytes = photo_encryption(System.Text.Encoding.UTF8.GetBytes(src));
                result = Convert.ToBase64String(aesBytes);
            }
            catch
            {
                result = "";
            }

            return result;
        }

        public static string CryptDecryptString(string src)
        {
            string result = "";
            try
            {
                byte[] noAesBytes = photo_Decrypt(Convert.FromBase64String(src));
                result = System.Text.Encoding.UTF8.GetString(noAesBytes);
            }
            catch
            {
                result = "";
            }

            return result;
        }

        #endregion


    }
}
