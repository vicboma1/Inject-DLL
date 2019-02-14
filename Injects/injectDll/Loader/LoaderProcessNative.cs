using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using winApi;
using System.Diagnostics;
using Loader;
using System.Threading;


namespace Loader
{
    public class LoaderProcessNative : ILoaderProcessNative
    {
        
        public Boolean isLoaded { get; private set; }
        public Kernel32Native.PROCESS_INFORMATION pi;
        public Kernel32Native.STARTUPINFOEX si;

        public static ILoaderProcessNative Create( String name)
        {
            return new LoaderProcessNative(name);
        }

        LoaderProcessNative(String name)
        {
	
	    }
        
        public Boolean InjectDLL(String name)
        {
            var _lproc = IntPtr.Zero;
            var _pbase = IntPtr.Zero;
	         
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            
            if (Environment.Is64BitProcess)
                throw new Exception(String.Format("No puedes cargar la libería {0} porque es un proceso de 64 bits", name));

            //Empty

            return true;
        }

        public Boolean ReadMemory(IntPtr processID, IntPtr address, int numOfBytes, byte[] buffer, out int bytesRead)
        {
            return Kernel32Native.ReadProcessMemory(processID, address, buffer, numOfBytes, out bytesRead);
        }

        public Boolean WriteMemory(IntPtr hProc, IntPtr address, byte[] buffer, out int bytesWrited)
        {
            return Kernel32Native.WriteProcessMemory(hProc, address, buffer, (uint)buffer.Length, out bytesWrited);
        }

        public uint Resume(){
            return Kernel32Native.ResumeThread(pi.hThread);
        }

        private String toHex(IntPtr ptr){
             return "0x" + ptr.ToString("X8");
        }
    }
}
