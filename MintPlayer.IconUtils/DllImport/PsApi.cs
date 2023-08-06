using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace MintPlayer.IconUtils.DllImport
{
    internal static class PsApi
    {
        [DllImport("psapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public static extern int GetMappedFileName(IntPtr hProcess, IntPtr lpv, StringBuilder lpFilename, int nSize);
    }
}
