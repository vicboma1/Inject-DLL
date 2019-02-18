using System;
using winApi;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace Loader
{

    public interface ILoaderProcessNative
    {
        Boolean isLoaded { get; }
        Boolean isActive();

        int Open(String name);
        Task OpenAsync(String name);

        Kernel32Native.PROCESS_INFORMATION Create(String name, uint securityAttr);

        Boolean Attach(String name);
        Boolean InjectDLL(String name);

        //static IntPtr Suspend(IntPtr process);
        //static IntPtr Suspend();
        List<IntPtr> SuspendAllThreads();
          
        Boolean ReadMemory(IntPtr processID, IntPtr address, int numOfBytes, byte[] buffer, out int bytesRead);
        Boolean WriteMemory(IntPtr hProc, IntPtr address, byte [] buffer, out int bytesWrited);

        //static IntPtr ResumeThread(IntPtr pthread);
        //static List<IntPtr> ResumeAllThreads(Kernel32Native.PROCESS_INFORMATION pi);
        List<IntPtr> ResumeAllThreads();

        Boolean Terminate();
    }
}
