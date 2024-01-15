using AnnarComMICROSESV60.Forms;
using AnnarComMICROSESV60.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SerialPortTerminal
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      InterfaceConfig.InitializeConfig();
      Application.Run(new Dashboard());
    }
  }
}