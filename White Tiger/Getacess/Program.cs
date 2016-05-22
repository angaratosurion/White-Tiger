using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Getacess
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Process.Start("netsh http add urlacl url=http://+:80/White_Tiger/ user=\"NetworkService\"");
        }
    }
}
