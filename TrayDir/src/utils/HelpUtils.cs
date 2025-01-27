﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utils {
	internal class HelpUtils {
		private static string _helpPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\traydir.chm";
		private static void MakeHelp() {
			try {
				using (Stream input = new MemoryStream(TrayDir.Properties.Resource_Help.TrayDir))
				using (Stream output = File.Create(_helpPath)) {
					input.CopyTo(output);
				}
			}
			catch { }
		}
		public static void ShowHelp(Control control) {
			MakeHelp();
			Help.ShowHelp(control, _helpPath);
		}
		public static void ShowHelp(Control control, string topic) {
			MakeHelp();
			Help.ShowHelp(control, _helpPath, HelpNavigator.Topic, topic);
		}
	}
}
