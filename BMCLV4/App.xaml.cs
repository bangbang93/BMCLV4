﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using BMCLV4.Language;
using BMCLV4.Windows;

namespace BMCLV4
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        private static FileStream _appLock;
        private static bool _skipPlugin;

        public static bool SkipPlugin
        {
            get { return _skipPlugin; }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length == 0)   // 判断debug模式
                Logger.Debug = false;
            else
                if (Array.IndexOf(e.Args, "-Debug") != -1)
                    Logger.Debug = true;
            Logger.Start();
#if DEBUG
#else
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

#endif
            if (Array.IndexOf(e.Args, "-Update") != -1)
            {
                var index = Array.IndexOf(e.Args, "-Update");
                if (index < e.Args.Length - 1)
                {
                    if (!e.Args[index + 1].StartsWith("-"))
                    {
                        DoUpdate(e.Args[index + 1]);
                    }
                    else
                    {
                        DoUpdate();
                    }
                }
            }
            if (Array.IndexOf(e.Args, "-SkipPlugin") != -1)
            {
                App._skipPlugin = true;
            }
            try
            {
                _appLock = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "BMCL.lck", FileMode.Create);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture));
                _appLock.Write(buffer, 0, buffer.Length);
                _appLock.Close();
                _appLock = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "BMCL.lck", FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show(LangManager.GetLangFromResource("StartupDuplicate"));
                MessageBox.Show(LangManager.GetLangFromResource("StartupDuplicate"));
                Environment.Exit(3);
            }
            WebRequest.DefaultWebProxy = null;  //禁用默认代理
            base.OnStartup(e);
        }



        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Stop();
        }

        public static void AboutToExit()
        {
            _appLock.Close();
            Logger.Stop();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            var crash = new CrashWindow(e.Exception);
            crash.Show();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var crash = new CrashWindow(e.Exception);
            crash.Show();
        }

        private void DoUpdate()
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            var time = 0;
            while (time < 10)
            {
                try
                {
                    File.Copy(processName, "BMCL.exe", true);
                    Process.Start("BMCL.exe", "-Update " + processName);
                    Application.Current.Shutdown(0);
                    return;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                finally
                {
                    time++;
                }
            }
            MessageBox.Show("自动升级失败，请手动使用" + processName + "替代旧版文件");
            MessageBox.Show("自动升级失败，请手动使用" + processName + "替代旧版文件");
        }

        private void DoUpdate(string fileName)
        {
            File.Delete(fileName);
        }
    }
}
