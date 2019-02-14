using System;

namespace Loader
{

    public interface ILoaderProcessNative
    {
        Boolean isLoaded { get; }
        Boolean InjectDLL(String name);
       
        Boolean ReadMemory(IntPtr processID, IntPtr address, int numOfBytes, byte[] buffer, out int bytesRead);
        Boolean WriteMemory(IntPtr hProc, IntPtr address, byte [] buffer, out int bytesWrited);

        uint Resume();
    }
}