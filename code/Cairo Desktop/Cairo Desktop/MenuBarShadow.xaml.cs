﻿using CairoDesktop.SupportingClasses;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace CairoDesktop
{
    /// <summary>
    /// Interaction logic for MenuBarShadow.xaml
    /// </summary>
    public partial class MenuBarShadow : Window
    {
        public MenuBarShadow()
        {
            InitializeComponent();

            this.Width = Startup.MenuBarWindow.ActualWidth;

            DispatcherTimer autoResize = new DispatcherTimer(new TimeSpan(0, 0, 2), DispatcherPriority.Normal, delegate
            {
                this.Width = Startup.MenuBarWindow.ActualWidth;
                this.Top = Startup.MenuBarWindow.ActualHeight;
                this.Left = 0;
            }, this.Dispatcher);
        }

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            int WM_NCHITTEST = 0x0084;
            int HTTRANSPARENT = (-1);
            if (msg == WM_NCHITTEST)
            {
                handled = true;
                return (IntPtr)HTTRANSPARENT;
            }
            else
            {
                handled = false;
            }
            return IntPtr.Zero;
        }

        private void MenuBarShadow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);

            source.AddHook(new HwndSourceHook(WndProc));
        }
    }


}
