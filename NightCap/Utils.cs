using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace NightCap
{
    public static class Utils
    {
        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            List<string> parts = new List<string>(result.Split(' '));
            parts.RemoveAt(0);
            return string.Join(" ", parts);
        }

        public static void CheckSystem()
        {
            if (Environment.OSVersion.Version.Major < 10)
            {
                MessageBox.Show($"Current version: {GetOSFriendlyName()}", "Not supported on your Windows.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(-1);
            }
        }
    }
}
