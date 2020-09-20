using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HKiosk.Controls.Keyboard
{
    public class VirtualKeyboard : UserControl
    {
        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetDefaultIMEWnd(IntPtr hWnd);
        [DllImport("User32.dll")]
        static extern void keybd_event(byte vk, byte scan, int flags, int extra);
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr IParam);

        private const int WM_IME_CONTROL = 643;

        [System.Flags]
        public enum KeyFlag { KE_DOWN = 0, KE__EXTENDEDKEY = 1, KE_UP = 2 }

        public static readonly RoutedEvent VirtualKeyDownEvent =
            EventManager.RegisterRoutedEvent("VirtualKeyDown",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(VirtualKeyboard));

        public event RoutedEventHandler VirtualKeyDown
        {
            add { AddHandler(VirtualKeyDownEvent, value); }
            remove { RemoveHandler(VirtualKeyDownEvent, value); }
        }

        public bool IsPressedShift { get; private set; }
        public bool IsPressedHangul { get; private set; }
        public bool IsPressedCapsLock { get; }

        public VirtualKeyboard()
        {
            Loaded += KeyboardUserControl_Loaded;
            Unloaded += KeyboardUserControl_Unloaded;
            IsVisibleChanged += KeyboardUserControl_IsVisibleChanged;
        }

        protected void UpdateKeys()
        {
            var content = Content as Panel;
            UpdateKeys(content);
        }

        protected void UpdateKeys(Panel panel)
        {
            foreach (UIElement child in panel.Children)
            {
                if (child is Panel)
                {
                    var content = child as Panel;
                    UpdateKeys(content);
                }
                else if (child is KeyButton)
                {
                    var keyButton = child as KeyButton;
                    keyButton.UpdateKey(IsPressedShift, IsPressedCapsLock, IsPressedHangul);
                }
            }
        }

        private void KeyClick(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is KeyButton keyButton))
            {
                return;
            }

            SetKeyboardSync();

            if (keyButton.KeyCode == VirtualKeyCode.SHIFT)
            {
                IsPressedShift = !IsPressedShift;

                if (IsPressedShift)
                {
                    Keybd_event_KeyDown((int)keyButton.KeyCode);
                }
                else
                {
                    Keybd_event_KeyUp((int)keyButton.KeyCode);
                }
            }
            else if (keyButton.KeyCode == VirtualKeyCode.HANGUL)
            {
                IsPressedHangul = !IsPressedHangul;
                Keybd_event_KeyClick((int)keyButton.KeyCode);
            }
            else
            {
                Keybd_event_KeyClick((int)keyButton.KeyCode);
            }

            UpdateKeys();

            RaiseEvent(new RoutedEventArgs(VirtualKeyDownEvent, keyButton.KeyCode));
        }

        private void SetKeyboardSync()
        {
            SetKeyStateToDefault((int)VirtualKeyCode.CAPITAL);
            SetHangulSync();
        }

        private void SetHangulSync()
        {
            Process p = Process.GetCurrentProcess();
            if (p == null)
                return;

            IntPtr hwnd = p.MainWindowHandle;
            IntPtr hime = ImmGetDefaultIMEWnd(hwnd);
            IntPtr status = SendMessage(hime, WM_IME_CONTROL, new IntPtr(0x5), new IntPtr(0));

            int hangul = IsPressedHangul ? 1 : 0;

            if (status.ToInt32() != hangul)
            {
                Keybd_event_KeyClick((int)VirtualKeyCode.HANGUL);
            }
        }

        private void ReleaseKeyboard()
        {
            SetKeyStateToDefault((int)VirtualKeyCode.SHIFT);
            IsPressedShift = false;
        }

        private void SetKeyStateToDefault(int keycode)
        {
            if (GetKeyState(keycode & 0xffff) != 0)
            {
                Keybd_event_KeyClick(keycode);
            }
        }

        private void Keybd_event_KeyDown(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)(KeyFlag.KE_DOWN), 0);
        }

        private void Keybd_event_KeyUp(int keycode)
        {
            keybd_event((byte)keycode, 0, (int)(KeyFlag.KE_UP), 0);
        }

        private void Keybd_event_KeyClick(int keycode)
        {
            Keybd_event_KeyDown(keycode);
            Keybd_event_KeyUp(keycode);
        }

        private void KeyboardUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            RenderTransform = new TranslateTransform();
            AddHandler(KeyButton.ClickEvent, (RoutedEventHandler)KeyClick);

            IsPressedHangul = true;
            UpdateKeys();
        }

        private void KeyboardUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ReleaseKeyboard();
        }

        private void KeyboardUserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateKeys();
        }
    }
}
