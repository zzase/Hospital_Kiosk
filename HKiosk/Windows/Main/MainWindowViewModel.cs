﻿using HKiosk.Base;
using HKiosk.Controls.NavigationBar;
using HKiosk.Controls.Popup;
using HKiosk.Controls.Timer;
using HKiosk.Manager.Data;
using HKiosk.Manager.Navigation;
using HKiosk.Manager.Popup;
using HKiosk.Manager.Timer;
using System.Windows;
using System.Windows.Input;

namespace HKiosk.Windows.Main
{
    public class MainWindowViewModel : PropertyChange
    {
        private WindowState curWindowState;
        private WindowStyle curWindowStyle;
        private ResizeMode curResizeMode;
        private readonly CommandBindingCollection _CommandBindings = new CommandBindingCollection();

        public bool IsMaximizedWindow
        {
            get => CurWindowState == WindowState.Maximized;
        }

        public WindowState CurWindowState
        {
            get => curWindowState;
            set => SetProperty(ref curWindowState, value);
        }

        public WindowStyle CurWindowStyle
        {
            get => curWindowStyle;
            set => SetProperty(ref curWindowStyle, value);
        }

        public ResizeMode CurResizeMode
        {
            get => curResizeMode;
            set => SetProperty(ref curResizeMode, value);
        }

        public CommandBindingCollection CommandBindings
        {
            get
            {
                return _CommandBindings;
            }
        }

        public NavigationBarViewModel NavigationBarViewModel { get; }

        public PopupViewModel AlertViewModel { get; }

        public PopupViewModel ConfirmViewModel { get; }

        public PopupViewModel LodingViewModel { get; }

        public ICommand CreateClickEffectCommand { get; }

        public static DependencyProperty RegisterCommandBindingsProperty =
           DependencyProperty.RegisterAttached("RegisterCommandBindings", typeof(CommandBindingCollection),
               typeof(MainWindowViewModel), new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        public MainWindowViewModel(INavigation navigation)
        {
            CurWindowState = WindowState.Maximized;
            CurWindowStyle = WindowStyle.None;
            CurResizeMode = ResizeMode.NoResize;

            BindWindowModeTogleCommand();

            AlertViewModel = new PopupViewModel();
            ConfirmViewModel = new PopupViewModel();
            LodingViewModel = new PopupViewModel();

            PopupManager.Instance.Add(PopupElement.Alert, AlertViewModel);
            PopupManager.Instance.Add(PopupElement.Confirm, ConfirmViewModel);
            PopupManager.Instance.Add(PopupElement.Loding, LodingViewModel);
            NavigationBarViewModel = new NavigationBarViewModel();
            TimerManager.Timer = new TimerViewModel();

            NavigationManager.Navigation = navigation;
        }

        public void MoveNavigationBar(NaviElement navi)
        {
            if (NaviElement.Main == navi)
                NavigationBarViewModel.SetVisibility(System.Windows.Visibility.Collapsed);
            else
                NavigationBarViewModel.SetVisibility(System.Windows.Visibility.Visible);

            NavigationBarViewModel.ActivateNaviPart(navi);
        }

        private void BindWindowModeTogleCommand()
        {
            RoutedCommand routedCommand = new RoutedCommand();
            routedCommand.InputGestures.Add(new KeyGesture(Key.Enter, ModifierKeys.Alt));

            CommandBinding toggleWindowCommandBinding = new CommandBinding(routedCommand, (sender, e) => ToggleWindow());
            CommandManager.RegisterClassCommandBinding(typeof(MainWindowViewModel), toggleWindowCommandBinding);
            CommandBindings.Add(toggleWindowCommandBinding);
        }

        private void ToggleWindow()
        {
            if (IsMaximizedWindow)
            {
                CurWindowStyle = WindowStyle.SingleBorderWindow;
                CurResizeMode = ResizeMode.CanResize;
                CurWindowState = WindowState.Normal;
            }
            else
            {
                CurWindowStyle = WindowStyle.None;
                CurResizeMode = ResizeMode.NoResize;
                CurWindowState = WindowState.Maximized;
            }
        }

        public static void SetRegisterCommandBindings(UIElement element, CommandBindingCollection value)
        {
            if (element != null)
                element.SetValue(RegisterCommandBindingsProperty, value);
        }

        public static CommandBindingCollection GetRegisterCommandBindings(UIElement element)
        {
            return (element != null ? (CommandBindingCollection)element.GetValue(RegisterCommandBindingsProperty) : null);
        }

        private static void OnRegisterCommandBindingChanged
        (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                CommandBindingCollection bindings = e.NewValue as CommandBindingCollection;
                if (bindings != null)
                {
                    element.CommandBindings.AddRange(bindings);
                }
            }
        }
    }
}
