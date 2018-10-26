using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mover;

namespace MoverWorkTest
{
    [TestClass]
    public class MoverWorkTest
    {
        [TestMethod]
        public void Test_InTime()
        {
            string d_from = "D:\\Upd_Arm\\!Send\\k7\\";
            string d_dist = "d:\\123\\";
            string mask = "*<04:11><TCP>";
            MoverWork.Operation oper = MoverWork.Operation.CopyRepl;

            MoverWork mw = new MoverWork(d_from, d_dist, mask, oper);

            bool rez = mw.InTime(MoverWork.Mask(mask));

            Assert.IsTrue(rez, "err");

        }
    }
}


