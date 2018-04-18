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

        private Version vers;

        public T GetForm<T>() where T : Form
        {
            return (T)Application.OpenForms[typeof(T).Name];
        }

        public Log()
        {
            vers = new Version(Application.ProductVersion);
        }

        public void Info(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Info(message);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), message, ToolTipIcon.Info);
        }

        public void Info(string message, params object[] argg)
        {
            var frm = GetForm<MainF>();
            addlog.Info(message,argg);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), String.Format(message, argg), ToolTipIcon.Info);
        }

        public void Error(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Error(message);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), message, ToolTipIcon.Error);
        }

        public void Error(string message, params object[] argg)
        {
            var frm = GetForm<MainF>();
            addlog.Error(message,argg);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), String.Format(message, argg), ToolTipIcon.Error);
        }

        public void Warn(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Warn(message);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), message, ToolTipIcon.Warning);
        }

        public void Warn(string message, params object[] argg)
        {
            var frm = GetForm<MainF>();
            addlog.Warn(message,argg);
            if (frm.chBbaloon.Checked)
                frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), String.Format(message,argg), ToolTipIcon.Warning);
        }

        public void Debug(string message)
        {
            var frm = GetForm<MainF>();
            addlog.Debug(message);
            //frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), message, ToolTipIcon.Warning);
        }

        public void Debug(string message, params object[] argg)
        {
            var frm = GetForm<MainF>();
            addlog.Debug(message, argg);
            //frm.nI1.ShowBalloonTip(3000, "Mover v" + vers.ToString(), message, ToolTipIcon.Warning);
        }

    }
}
