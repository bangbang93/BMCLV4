using System;
using System.Globalization;

namespace BMCLV4.Windows
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow
    {
        public LogWindow()
        {
            InitializeComponent();
        }
        public void WriteLine(string log, Logger.LogType type = Logger.LogType.Info)
        {
            switch (type)
            {
                case Logger.LogType.Error:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "错误:" + log;
                    break;
                case Logger.LogType.Info:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息:" + log;
                    break;
                case Logger.LogType.Crash:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "崩溃:" + log;
                    break;
                case Logger.LogType.Exception:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "异常:" + log;
                    break;
                case Logger.LogType.Game:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "游戏:" + log;
                    break;
                case Logger.LogType.Fml:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "FML :" + log;
                    break;
                default:
                    log = DateTime.Now.ToString(CultureInfo.InvariantCulture) + "信息:" + log;
                    break;
            }
            Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate {ListLog.Items.Add(log); ListLog.ScrollIntoView(ListLog.Items[ListLog.Items.Count - 1]); }));
        }

    }
}
