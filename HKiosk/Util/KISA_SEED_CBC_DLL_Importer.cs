using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HKiosk.Util
{
    public static class KISA_SEED_CBC_DLL_Importer
    {
        private static readonly string pbszUserKey = "BBMC!!@*5218998h";
        private static readonly string pbszIV = "4421673257160032";

        [DllImport("Libs/KISA_SEED_CBC_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SEED_CBC_Encrypt")]
        public static unsafe extern int SEED_CBC_Encrypt(byte[] pbszUserKey, byte[] pbszIV, byte[] pbszPlainText, int nPlainTextLen, byte[] pbszCipherText);

        [DllImport("Libs/KISA_SEED_CBC_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SEED_CBC_Decrypt")]
        public static unsafe extern int SEED_CBC_Decrypt(byte[] pbszUserKey, byte[] pbszIV, byte[] pbszCipherText, int nPlainTextLen, byte[] pbszPlainText);

        [DllImport("Libs/KISA_SEED_CBC_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SEED_CBC_Encrypt_String")]
        public static unsafe extern int SEED_CBC_Encrypt_String(string pbszUserKey, string pbszIV, string pbszPlainText, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pbszCipherText);

        [DllImport("Libs/KISA_SEED_CBC_DLL.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SEED_CBC_Decrypt_String")]
        public static unsafe extern int SEED_CBC_Decrypt_String(string pbszUserKey, string pbszIV, string pbszCipherText, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pbszPlainText);

        public static string Encrypt(string pbszPlainText)
        {
            StringBuilder pbszCipherText = new StringBuilder(pbszPlainText.Length * 2);

            SEED_CBC_Encrypt_String(pbszUserKey, pbszIV, pbszPlainText, pbszCipherText);

            return pbszCipherText.ToString();
        }

        public static string Decrypt(string pbszCipherText)
        {
            StringBuilder pbszPlainText = new StringBuilder(pbszCipherText.Length * 2);

            SEED_CBC_Decrypt_String(pbszUserKey, pbszIV, pbszCipherText, pbszPlainText);

            return pbszPlainText.ToString();
        }
    }
}
