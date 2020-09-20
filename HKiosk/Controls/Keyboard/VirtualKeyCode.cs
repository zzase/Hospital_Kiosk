using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKiosk.Controls.Keyboard
{
    public enum VirtualKeyCode
    {
        //
        // 요약:
        //     Left mouse button
        LBUTTON = 1,
        //
        // 요약:
        //     Right mouse button
        RBUTTON = 2,
        //
        // 요약:
        //     Control-break processing
        CANCEL = 3,
        //
        // 요약:
        //     Middle mouse button (three-button mouse) - NOT contiguous with LBUTTON and RBUTTON
        MBUTTON = 4,
        //
        // 요약:
        //     Windows 2000/XP: X1 mouse button - NOT contiguous with LBUTTON and RBUTTON
        XBUTTON1 = 5,
        //
        // 요약:
        //     Windows 2000/XP: X2 mouse button - NOT contiguous with LBUTTON and RBUTTON
        XBUTTON2 = 6,
        //
        // 요약:
        //     BACKSPACE key
        BACK = 8,
        //
        // 요약:
        //     TAB key
        TAB = 9,
        //
        // 요약:
        //     CLEAR key
        CLEAR = 12,
        //
        // 요약:
        //     ENTER key
        RETURN = 13,
        //
        // 요약:
        //     SHIFT key
        SHIFT = 16,
        //
        // 요약:
        //     CTRL key
        CONTROL = 17,
        //
        // 요약:
        //     ALT key
        MENU = 18,
        //
        // 요약:
        //     PAUSE key
        PAUSE = 19,
        //
        // 요약:
        //     CAPS LOCK key
        CAPITAL = 20,
        //
        // 요약:
        //     Input Method Editor (IME) Kana mode
        KANA = 21,
        //
        // 요약:
        //     IME Hanguel mode (maintained for compatibility; use HANGUL)
        HANGEUL = 21,
        //
        // 요약:
        //     IME Hangul mode
        HANGUL = 21,
        //
        // 요약:
        //     IME Junja mode
        JUNJA = 23,
        //
        // 요약:
        //     IME final mode
        FINAL = 24,
        //
        // 요약:
        //     IME Hanja mode
        HANJA = 25,
        //
        // 요약:
        //     IME Kanji mode
        KANJI = 25,
        //
        // 요약:
        //     ESC key
        ESCAPE = 27,
        //
        // 요약:
        //     IME convert
        CONVERT = 28,
        //
        // 요약:
        //     IME nonconvert
        NONCONVERT = 29,
        //
        // 요약:
        //     IME accept
        ACCEPT = 30,
        //
        // 요약:
        //     IME mode change request
        MODECHANGE = 31,
        //
        // 요약:
        //     SPACEBAR
        SPACE = 32,
        //
        // 요약:
        //     PAGE UP key
        PRIOR = 33,
        //
        // 요약:
        //     PAGE DOWN key
        NEXT = 34,
        //
        // 요약:
        //     END key
        END = 35,
        //
        // 요약:
        //     HOME key
        HOME = 36,
        //
        // 요약:
        //     LEFT ARROW key
        LEFT = 37,
        //
        // 요약:
        //     UP ARROW key
        UP = 38,
        //
        // 요약:
        //     RIGHT ARROW key
        RIGHT = 39,
        //
        // 요약:
        //     DOWN ARROW key
        DOWN = 40,
        //
        // 요약:
        //     SELECT key
        SELECT = 41,
        //
        // 요약:
        //     PRINT key
        PRINT = 42,
        //
        // 요약:
        //     EXECUTE key
        EXECUTE = 43,
        //
        // 요약:
        //     PRINT SCREEN key
        SNAPSHOT = 44,
        //
        // 요약:
        //     INS key
        INSERT = 45,
        //
        // 요약:
        //     DEL key
        DELETE = 46,
        //
        // 요약:
        //     HELP key
        HELP = 47,
        //
        // 요약:
        //     0 key
        VK_0 = 48,
        //
        // 요약:
        //     1 key
        VK_1 = 49,
        //
        // 요약:
        //     2 key
        VK_2 = 50,
        //
        // 요약:
        //     3 key
        VK_3 = 51,
        //
        // 요약:
        //     4 key
        VK_4 = 52,
        //
        // 요약:
        //     5 key
        VK_5 = 53,
        //
        // 요약:
        //     6 key
        VK_6 = 54,
        //
        // 요약:
        //     7 key
        VK_7 = 55,
        //
        // 요약:
        //     8 key
        VK_8 = 56,
        //
        // 요약:
        //     9 key
        VK_9 = 57,
        //
        // 요약:
        //     A key
        VK_A = 65,
        //
        // 요약:
        //     B key
        VK_B = 66,
        //
        // 요약:
        //     C key
        VK_C = 67,
        //
        // 요약:
        //     D key
        VK_D = 68,
        //
        // 요약:
        //     E key
        VK_E = 69,
        //
        // 요약:
        //     F key
        VK_F = 70,
        //
        // 요약:
        //     G key
        VK_G = 71,
        //
        // 요약:
        //     H key
        VK_H = 72,
        //
        // 요약:
        //     I key
        VK_I = 73,
        //
        // 요약:
        //     J key
        VK_J = 74,
        //
        // 요약:
        //     K key
        VK_K = 75,
        //
        // 요약:
        //     L key
        VK_L = 76,
        //
        // 요약:
        //     M key
        VK_M = 77,
        //
        // 요약:
        //     N key
        VK_N = 78,
        //
        // 요약:
        //     O key
        VK_O = 79,
        //
        // 요약:
        //     P key
        VK_P = 80,
        //
        // 요약:
        //     Q key
        VK_Q = 81,
        //
        // 요약:
        //     R key
        VK_R = 82,
        //
        // 요약:
        //     S key
        VK_S = 83,
        //
        // 요약:
        //     T key
        VK_T = 84,
        //
        // 요약:
        //     U key
        VK_U = 85,
        //
        // 요약:
        //     V key
        VK_V = 86,
        //
        // 요약:
        //     W key
        VK_W = 87,
        //
        // 요약:
        //     X key
        VK_X = 88,
        //
        // 요약:
        //     Y key
        VK_Y = 89,
        //
        // 요약:
        //     Z key
        VK_Z = 90,
        //
        // 요약:
        //     Left Windows key (Microsoft Natural keyboard)
        LWIN = 91,
        //
        // 요약:
        //     Right Windows key (Natural keyboard)
        RWIN = 92,
        //
        // 요약:
        //     Applications key (Natural keyboard)
        APPS = 93,
        //
        // 요약:
        //     Computer Sleep key
        SLEEP = 95,
        //
        // 요약:
        //     Numeric keypad 0 key
        NUMPAD0 = 96,
        //
        // 요약:
        //     Numeric keypad 1 key
        NUMPAD1 = 97,
        //
        // 요약:
        //     Numeric keypad 2 key
        NUMPAD2 = 98,
        //
        // 요약:
        //     Numeric keypad 3 key
        NUMPAD3 = 99,
        //
        // 요약:
        //     Numeric keypad 4 key
        NUMPAD4 = 100,
        //
        // 요약:
        //     Numeric keypad 5 key
        NUMPAD5 = 101,
        //
        // 요약:
        //     Numeric keypad 6 key
        NUMPAD6 = 102,
        //
        // 요약:
        //     Numeric keypad 7 key
        NUMPAD7 = 103,
        //
        // 요약:
        //     Numeric keypad 8 key
        NUMPAD8 = 104,
        //
        // 요약:
        //     Numeric keypad 9 key
        NUMPAD9 = 105,
        //
        // 요약:
        //     Multiply key
        MULTIPLY = 106,
        //
        // 요약:
        //     Add key
        ADD = 107,
        //
        // 요약:
        //     Separator key
        SEPARATOR = 108,
        //
        // 요약:
        //     Subtract key
        SUBTRACT = 109,
        //
        // 요약:
        //     Decimal key
        DECIMAL = 110,
        //
        // 요약:
        //     Divide key
        DIVIDE = 111,
        //
        // 요약:
        //     F1 key
        F1 = 112,
        //
        // 요약:
        //     F2 key
        F2 = 113,
        //
        // 요약:
        //     F3 key
        F3 = 114,
        //
        // 요약:
        //     F4 key
        F4 = 115,
        //
        // 요약:
        //     F5 key
        F5 = 116,
        //
        // 요약:
        //     F6 key
        F6 = 117,
        //
        // 요약:
        //     F7 key
        F7 = 118,
        //
        // 요약:
        //     F8 key
        F8 = 119,
        //
        // 요약:
        //     F9 key
        F9 = 120,
        //
        // 요약:
        //     F10 key
        F10 = 121,
        //
        // 요약:
        //     F11 key
        F11 = 122,
        //
        // 요약:
        //     F12 key
        F12 = 123,
        //
        // 요약:
        //     F13 key
        F13 = 124,
        //
        // 요약:
        //     F14 key
        F14 = 125,
        //
        // 요약:
        //     F15 key
        F15 = 126,
        //
        // 요약:
        //     F16 key
        F16 = 127,
        //
        // 요약:
        //     F17 key
        F17 = 128,
        //
        // 요약:
        //     F18 key
        F18 = 129,
        //
        // 요약:
        //     F19 key
        F19 = 130,
        //
        // 요약:
        //     F20 key
        F20 = 131,
        //
        // 요약:
        //     F21 key
        F21 = 132,
        //
        // 요약:
        //     F22 key
        F22 = 133,
        //
        // 요약:
        //     F23 key
        F23 = 134,
        //
        // 요약:
        //     F24 key
        F24 = 135,
        //
        // 요약:
        //     NUM LOCK key
        NUMLOCK = 144,
        //
        // 요약:
        //     SCROLL LOCK key
        SCROLL = 145,
        //
        // 요약:
        //     Left SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        LSHIFT = 160,
        //
        // 요약:
        //     Right SHIFT key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        RSHIFT = 161,
        //
        // 요약:
        //     Left CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        LCONTROL = 162,
        //
        // 요약:
        //     Right CONTROL key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        RCONTROL = 163,
        //
        // 요약:
        //     Left MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        LMENU = 164,
        //
        // 요약:
        //     Right MENU key - Used only as parameters to GetAsyncKeyState() and GetKeyState()
        RMENU = 165,
        //
        // 요약:
        //     Windows 2000/XP: Browser Back key
        BROWSER_BACK = 166,
        //
        // 요약:
        //     Windows 2000/XP: Browser Forward key
        BROWSER_FORWARD = 167,
        //
        // 요약:
        //     Windows 2000/XP: Browser Refresh key
        BROWSER_REFRESH = 168,
        //
        // 요약:
        //     Windows 2000/XP: Browser Stop key
        BROWSER_STOP = 169,
        //
        // 요약:
        //     Windows 2000/XP: Browser Search key
        BROWSER_SEARCH = 170,
        //
        // 요약:
        //     Windows 2000/XP: Browser Favorites key
        BROWSER_FAVORITES = 171,
        //
        // 요약:
        //     Windows 2000/XP: Browser Start and Home key
        BROWSER_HOME = 172,
        //
        // 요약:
        //     Windows 2000/XP: Volume Mute key
        VOLUME_MUTE = 173,
        //
        // 요약:
        //     Windows 2000/XP: Volume Down key
        VOLUME_DOWN = 174,
        //
        // 요약:
        //     Windows 2000/XP: Volume Up key
        VOLUME_UP = 175,
        //
        // 요약:
        //     Windows 2000/XP: Next Track key
        MEDIA_NEXT_TRACK = 176,
        //
        // 요약:
        //     Windows 2000/XP: Previous Track key
        MEDIA_PREV_TRACK = 177,
        //
        // 요약:
        //     Windows 2000/XP: Stop Media key
        MEDIA_STOP = 178,
        //
        // 요약:
        //     Windows 2000/XP: Play/Pause Media key
        MEDIA_PLAY_PAUSE = 179,
        //
        // 요약:
        //     Windows 2000/XP: Start Mail key
        LAUNCH_MAIL = 180,
        //
        // 요약:
        //     Windows 2000/XP: Select Media key
        LAUNCH_MEDIA_SELECT = 181,
        //
        // 요약:
        //     Windows 2000/XP: Start Application 1 key
        LAUNCH_APP1 = 182,
        //
        // 요약:
        //     Windows 2000/XP: Start Application 2 key
        LAUNCH_APP2 = 183,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the ';:' key
        OEM_1 = 186,
        //
        // 요약:
        //     Windows 2000/XP: For any country/region, the '+' key
        OEM_PLUS = 187,
        //
        // 요약:
        //     Windows 2000/XP: For any country/region, the ',' key
        OEM_COMMA = 188,
        //
        // 요약:
        //     Windows 2000/XP: For any country/region, the '-' key
        OEM_MINUS = 189,
        //
        // 요약:
        //     Windows 2000/XP: For any country/region, the '.' key
        OEM_PERIOD = 190,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the '/?' key
        OEM_2 = 191,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the '`~' key
        OEM_3 = 192,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the '[{' key
        OEM_4 = 219,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the '\|' key
        OEM_5 = 220,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the ']}' key
        OEM_6 = 221,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard. Windows 2000/XP:
        //     For the US standard keyboard, the 'single-quote/double-quote' key
        OEM_7 = 222,
        //
        // 요약:
        //     Used for miscellaneous characters; it can vary by keyboard.
        OEM_8 = 223,
        //
        // 요약:
        //     Windows 2000/XP: Either the angle bracket key or the backslash key on the RT
        //     102-key keyboard
        OEM_102 = 226,
        //
        // 요약:
        //     Windows 95/98/Me, Windows NT 4.0, Windows 2000/XP: IME PROCESS key
        PROCESSKEY = 229,
        //
        // 요약:
        //     Windows 2000/XP: Used to pass Unicode characters as if they were keystrokes.
        //     The PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard
        //     input methods. For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN,
        //     and WM_KEYUP
        PACKET = 231,
        //
        // 요약:
        //     Attn key
        ATTN = 246,
        //
        // 요약:
        //     CrSel key
        CRSEL = 247,
        //
        // 요약:
        //     ExSel key
        EXSEL = 248,
        //
        // 요약:
        //     Erase EOF key
        EREOF = 249,
        //
        // 요약:
        //     Play key
        PLAY = 250,
        //
        // 요약:
        //     Zoom key
        ZOOM = 251,
        //
        // 요약:
        //     Reserved
        NONAME = 252,
        //
        // 요약:
        //     PA1 key
        PA1 = 253,
        //
        // 요약:
        //     Clear key
        OEM_CLEAR = 254
    }
}
