using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace White_Tiger.Commands
{
    interface ICommand
    {
         string Name();
        string Execute(string [] args);
         string Version();
      string Help();
        CommandType Type();
         
      


    }
}
