using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;
using System.Threading;

namespace Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = LoaderProcessNative.Create(@"dummy.exe");
            var isInject = loader.InjectDLL(@"bridgeC.dll");
            var resume = loader.Resume();
            Thread.Sleep(2000);
        }
    }
}
