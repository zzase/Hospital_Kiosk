using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Util
{
    public static class Mode 
    {
        public enum ModeFlag
        {
            DEPLOY = 0x01,
            NO_CHECK_PRINT = 0x02,
            NO_CHECK_ONS = 0x04,
            NO_PAYMENT = 0x08,
            NO_CHECK_TERMINAL = 0x10,
        }

        public static bool Equals(ModeFlag modeFlag)
        {
            try
            {
                int mode = Properties.Settings.Default.Mode;
                int flag = (int)modeFlag;

                return (mode & flag) > 0;
            }
            catch (Exception e)
            {
                Log.Write($"[Mode-Equals()] 예외발생 : {e}");
                return false;
            }
        }
    }
}
