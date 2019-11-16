using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SwitchTo.Properties;

namespace SwitchTo
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			if (!args.Any())
			{
				MessageBox.Show(Resources.Program_Main_Help, Resources.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
				Environment.Exit(1);
			}
			if (!NativeMethods.User32Helper.ActivateWindowByCaption(args[0]))
			{
				MessageBox.Show(string.Format(Resources.Program_Main_window_could_not_be_found, args[0]), Resources.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Environment.Exit(1);
			}
			if (args.Length > 1)
			{
				Thread.Sleep(90);
				SendKeys.SendWait(args[1]);
				SendKeys.SendWait(args[1]);
			}
			Environment.Exit(0);
		}
	}
}
