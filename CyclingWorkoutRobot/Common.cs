using System;
using System.IO;
using System.Security.Cryptography;
using static CyclingWorkoutRobot.Models;


namespace CyclingWorkoutRobot
{
    public static class Common
    {
       
        public static LanguageOption LanguageOptionStringToEnum(string inputEnum)
        {
            foreach (LanguageOption val in Enum.GetValues(typeof(LanguageOption)))
            {
                if (inputEnum == val.ToString())
                {
                    return val;

                }
            }
            return LanguageOption.English;

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
