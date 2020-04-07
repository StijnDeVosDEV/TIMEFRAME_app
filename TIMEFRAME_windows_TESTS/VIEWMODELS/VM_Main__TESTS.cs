using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TIMEFRAME_windows_TESTS.VIEWMODELS
{
    [TestClass]
    public class VM_Main__TESTS
    {
        [TestMethod]
        public void CalculateDuration_timeentry_addedit_DateStop_GT_timeentry_addedit_DateStart__IsExpectedDuration()
        {
            // Arrange
            TIMEFRAME_windows.VIEWMODELS.VM_Main vm_main = new TIMEFRAME_windows.VIEWMODELS.VM_Main();
            TimeSpan duration = new TimeSpan(1, 0, 29, 15);

            // Act
            vm_main.timeentry_addedit_DateTimeStart = new DateTime(2020, 3, 5, 13, 30, 45);
            vm_main.timeentry_addedit_DateTimeStop = new DateTime(2020, 3, 6, 14, 0, 0);

            // Assert
            Assert.AreEqual<TimeSpan>(duration, vm_main.timeentry_addedit_Duration);
        }

        [TestMethod]
        public void CalculateDuration_timeentry_edit_DateStop_GT_timeentry_edit_DateStart__IsExpectedDuration()
        {
            // Arrange
            TIMEFRAME_windows.VIEWMODELS.VM_Main vm_main = new TIMEFRAME_windows.VIEWMODELS.VM_Main();
            TimeSpan duration = new TimeSpan(1, 0, 29, 15);

            // Act
            vm_main.timeentry_edit_DateTimeStart = new DateTime(2020, 3, 5, 13, 30, 45);
            vm_main.timeentry_edit_DateTimeStop = new DateTime(2020, 3, 6, 14, 0, 0);

            // Assert
            Assert.AreEqual<TimeSpan>(duration, vm_main.timeentry_edit_Duration);
        }
    }
}
