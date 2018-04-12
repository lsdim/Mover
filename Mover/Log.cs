using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mover
{
    class Log
    {
        private static NLog.Logger addlog = NLog.LogManager.GetCurrentClassLogger();

        public T GetForm<T>() where T : Form
        {
            return (T)Application.OpenForms[typeof(T).Name];
        }

        public Log()
        {

        }

        public void Info(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Info(message);
            frm.nI1.ShowBalloonTip(3000, "Mover", message, ToolTipIcon.Info);
        }

        public void Error(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Error(message);
            frm.nI1.ShowBalloonTip(3000, "Mover", message, ToolTipIcon.Error);
        }

        public void Warn(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Warn(message);
            frm.nI1.ShowBalloonTip(3000, "Mover", message, ToolTipIcon.Warning);
        }

    }
}
