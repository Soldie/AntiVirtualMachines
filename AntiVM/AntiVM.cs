namespace AntiVM
{
    using System;
    using System.Diagnostics;
    using System.Management;
    using System.Windows.Forms;

    internal class AntiVM
    {
        private static bool GetDetectVirtualMachine()
        {
            using (ManagementObjectCollection managementObject = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get())
            {
                foreach (ManagementBaseObject managementBaseObject in managementObject)
                {
                    try
                    {
                        string str = managementBaseObject["Manufacturer"].ToString().ToLower();
                        bool strTo = managementBaseObject["Model"].ToString().ToLower().Contains("virtual");
                        if ((str.Equals("microsoft corporation") && strTo) || str.Contains("vmware") || managementBaseObject["Model"].ToString().Equals("VirtualBox"))
                        {
                            return true;
                        }
                    }
                    catch (Exception) { return false; }
                }
            }
            return false;
        }

        private static bool IsDebuggerAttached(Process process)
        {
            try
            {
                bool isDebuggerAttached = false;
                NativeMethods.CheckRemoteDebuggerPresent(process.Handle, ref isDebuggerAttached);
                return isDebuggerAttached;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsRdpAvailable => SystemInformation.TerminalServerSession == true ? true : false;
        private static bool SBieDLL() => Process.GetProcessesByName("wsnm").Length <= 0 && NativeMethods.GetModuleHandle("SbieDll.dll").ToInt32() == 0 ? false : true;
        public static bool GetCheckVMBot() => IsDebuggerAttached(Process.GetCurrentProcess()) || SBieDLL() || IsRdpAvailable || GetDetectVirtualMachine() ? true : false;
    }
}
