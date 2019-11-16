using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SwitchTo
{
	internal static class NativeMethods
	{
		public static class User32Helper
		{
			//private const int SW_SHOWNORMAL = 1;
			//private const int SW_SHOWMAXIMIZED = 3;
			//private const int SW_RESTORE = 9;

			//[DllImport("user32.dll")]
			//private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			//[DllImport("user32.dll")]
			//private static extern bool SetForegroundWindow(IntPtr hWnd);

			//[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
			//private static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

			//[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
			//private static extern int SetForegroundWindow(int hWnd);

			//[DllImport("user32.dll")]
			//private static extern IntPtr GetForegroundWindow();

			//[DllImport("user32.dll", SetLastError = true)]
			//private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

			private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

			[DllImport("User32.dll", SetLastError = true)]
			private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
			
			[DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
			private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
				IntPtr lParam);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool IsWindowVisible(IntPtr hWnd);

			[DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

			public class DesktopWindow
			{
				public IntPtr Handle { get; set; }
				public string Title { get; set; }
				public bool IsVisible { get; set; }
			}

			//------------------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Gets a list of all the desktop windows.
			/// </summary>
			/// <returns></returns>
			//------------------------------------------------------------------------------------------------------------------------
			public static IEnumerable<DesktopWindow> GetDesktopWindows()
			{
				var collection = new List<DesktopWindow>();
				bool Filter(IntPtr hWnd, int lParam)
				{
					var result = new StringBuilder(255);
					GetWindowText(hWnd, result, result.Capacity + 1);
					string title = result.ToString();
					bool isVisible = !string.IsNullOrEmpty(title) && IsWindowVisible(hWnd);
					collection.Add(new DesktopWindow { Handle = hWnd, Title = title, IsVisible = isVisible });
					return true;
				}
				EnumDesktopWindows(IntPtr.Zero, Filter, IntPtr.Zero);
				return collection;
			}

			//------------------------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Activates and restores a window by specifying its caption.
			/// </summary>
			/// <param name="caption">The caption.</param>
			/// <returns>true on success; false on failure.</returns>
			//------------------------------------------------------------------------------------------------------------------------
			public static bool ActivateWindowByCaption(string caption)
			{
				DesktopWindow x = GetDesktopWindows().FirstOrDefault(uh => uh.IsVisible && uh.Title.StartsWith(caption));
				if (x == null)
					return false;
				SwitchToThisWindow(x.Handle, true);
				return true;
			}

			////------------------------------------------------------------------------------------------
			///// <summary>
			///// Gets the process id corresponding to the specified application name.
			///// </summary>
			///// <param name="sAppl">Name of the application</param>
			///// <returns></returns>
			////------------------------------------------------------------------------------------------
			//public static int GetIPidFromAppName(string sAppl)
			//{
			//	int[] iPids = GetProcessIdsFromName(sAppl);
			//	try
			//	{
			//		if (iPids != null && iPids.Length > 0 && iPids[0] > 0)
			//		{
			//			if (iPids.Length == 1)
			//				return iPids[0];
			//			return (int)GetProcessIdFromForegroundWindow();
			//		}
			//	}
			//	catch
			//	{
			//	}
			//	return 0;
			//}

			////------------------------------------------------------------------------------------------
			///// <summary>
			///// Gets the name of the process ids from.
			///// </summary>
			///// <param name="processName">Name of the process.</param>
			///// <returns></returns>
			////------------------------------------------------------------------------------------------
			//public static int[] GetProcessIdsFromName(string processName)
			//{
			//	var processIds = new List<int>();
			//	Process[] processes = Process.GetProcesses();
			//	foreach (Process process in processes)
			//	{
			//		try
			//		{
			//			if (String.Equals(process.MainModule.ModuleName, processName.Trim(), StringComparison.CurrentCultureIgnoreCase))
			//				processIds.Add(process.Id);
			//		}
			//		catch (Exception ex)
			//		{
			//			Console.WriteLine(ex.Message);
			//		}
			//	}
			//	return processIds.ToArray();
			//}

			////------------------------------------------------------------------------------------------
			///// <summary>
			///// Gets the process identifier from foreground window.
			///// </summary>
			///// <returns></returns>
			////------------------------------------------------------------------------------------------
			//public static uint GetProcessIdFromForegroundWindow()
			//{
			//	uint processId;
			//	GetWindowThreadProcessId(GetForegroundWindow(), out processId);
			//	return processId;
			//}

			////------------------------------------------------------------------------------------------------------------------------
			///// <summary>
			///// Activates and restores a window by specifying its caption.
			///// </summary>
			///// <param name="caption">The caption.</param>
			///// <returns>true on success; false on failure.</returns>
			////------------------------------------------------------------------------------------------------------------------------
			//private static bool ActivateWindowByCaption(string caption)
			//{
			//	IntPtr handle = FindWindowByCaption(IntPtr.Zero, caption);
			//	if (handle == IntPtr.Zero)
			//		return false;
			//	// Switch to the window.
			//	SwitchToThisWindow(handle, true);
			//	return true;
			//}

			////------------------------------------------------------------------------------------------------------------------------
			///// <summary>
			///// Activates and restores a window by specifying its process name.
			///// </summary>
			///// <param name="processName">Name of the process.</param>
			///// <returns>true on success; false on failure.</returns>
			////------------------------------------------------------------------------------------------------------------------------
			//private static bool ActivateWindowByProcessName(string processName)
			//{
			//	Process[] p = Process.GetProcessesByName(processName);
			//	if (!p.Any())
			//		return false;
			//	// Get the handle for whatever is the first process encountered.
			//	IntPtr handle = p.First().MainWindowHandle;
			//	// Restore the window if it is minimized.
			//	ShowWindow(handle, SW_RESTORE);
			//	// Set the window into the foreground.
			//	return SetForegroundWindow(handle);
			//}

		}
	}
}

