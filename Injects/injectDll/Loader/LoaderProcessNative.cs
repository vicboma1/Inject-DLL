using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using winApi;
using System.Diagnostics;
using Loader;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Loader
{
    public class LoaderProcessNative : ILoaderProcessNative
    {     
        public Boolean isLoaded { get; private set; }
        public Boolean isExec { get; private set; }
        public Kernel32Native.PROCESS_INFORMATION pi;
        public Kernel32Native.STARTUPINFOEX si;

        public static ILoaderProcessNative Create()
        {
            return new LoaderProcessNative();
        }

        public static ILoaderProcessNative Create(String name)
        {
            return new LoaderProcessNative(name);
        }

        LoaderProcessNative(String name, uint securityFlags) {
            this.isLoaded = false;
            this.isExec = false;
            this.pi = new Kernel32Native.PROCESS_INFORMATION();
            this.si = new Kernel32Native.STARTUPINFOEX();

            if(!String.IsNullOrEmpty(name))
                this.Create(name,securityFlags);
        }

        LoaderProcessNative() : this(null,0){}

        LoaderProcessNative(String name) : this(name,(uint)Kernel32Native.CreateProcessFlags.CREATE_SUSPENDED) {}

        public Boolean Terminate()
        {
            var res = false;
            if (isLoaded)   
                res = Kernel32Native.TerminateProcess(pi.hProcess, 0);
            
            if(res)
                Console.WriteLine("Terminando proceso");

            return res;
        }

        public int Open(String name)
        {
            var exitCode = -1;
            var proc = Process.Start(name);
            if (null == proc)
            {
                Console.WriteLine("Process failed");
                return exitCode;
            }

            Console.WriteLine("Process : " + toHex(proc.Handle));
            Console.WriteLine("Id : " + toHex(new IntPtr(proc.Id)));
            Console.WriteLine("Priority : " + proc.BasePriority);
            Console.WriteLine("");
            Console.WriteLine("Wait for exit");
            proc.WaitForExit();
            exitCode = proc.ExitCode;
            Console.WriteLine("Exit Code : " + exitCode);
            
        
            return exitCode;
        }

        public Task OpenAsync(String name)
        {
            Console.WriteLine("Async Execution");
            return Task.Factory.StartNew(() => Open(name) );  
        }

        public Boolean InjectDLL(String name)
        {
           [...]
	   
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

        public static IntPtr ResumeThread(IntPtr pthread) {
            return new IntPtr(Kernel32Native.ResumeThread(pthread));
        }

        public List<IntPtr> ResumeAllThreads() {
            if (this.isExec)
                return ResumeAllThreads(pi);
            else
            {
              [...]
            }
        }

        public static List<IntPtr> ResumeAllThreads(Kernel32Native.PROCESS_INFORMATION pi)
        {
            var threads = new List<IntPtr>();

            [...]
        }

        public Boolean isActive()
        {
            if (this.isLoaded)
                {
                    var res = (uint)0;
                    Kernel32Native.GetExitCodeProcess(pi.hProcess, out res);
                    if (Kernel32Native.STILL_ACTIVE == res)
                        return true;
                }

            return false;
        }

        public Kernel32Native.PROCESS_INFORMATION Create(String name, uint securityAttr)
        {
            [...]
        }

        public Boolean Attach(String name)
        {
            [...]
        }

        public static IntPtr SuspendThread(IntPtr pthread)
        {
            return new IntPtr(Kernel32Native.SuspendThread(pthread));
        }

        public static List<IntPtr> SuspendAllThreads(Kernel32Native.PROCESS_INFORMATION pi)
        {
           [...]
        }
        
        public List<IntPtr> SuspendAllThreads()
        {
            if (this.isExec)
                return SuspendAllThreads(pi);
            else
            {
               [...]
            }
        }

        private static String toHex(int i) { return toHex(new IntPtr(i)); }

        private static String toHex(IntPtr ptr){  return "0x" + ptr.ToString("X8"); }
    }
}
