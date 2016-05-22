using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
namespace testclient
{
  public   class Streamhandler
    {
      public Stream  StringToStream(string text)
      {
          try
          {
              Stream str = null;
              if (text != null)
              {
                  byte[] bitar = Encoding.UTF8.GetBytes(text);
                  MemoryStream ms = new MemoryStream(bitar);
                  str = ms;
                
                
              }
              return str;

          }
          catch (Exception ex)
          {
              return null;

          }


      }

      
    }
}
