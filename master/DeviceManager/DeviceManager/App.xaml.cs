using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows;

namespace DeviceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            (Current as App).InvokeErrorWindow(e.ExceptionObject as Exception);
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            InvokeErrorWindow(e.Exception);
            e.Handled = true;
        }

        void InvokeErrorWindow(Exception ex)
        {
            //Trace.WriteLine(">> Unhandled Exception: " + (ex is PublicException ? ex.Message : ex.ToString()));
            //Trace.Flush();

            //ExceptionHandler.HandleException("Unhandled Exception!", ex);
        }


    }


}
