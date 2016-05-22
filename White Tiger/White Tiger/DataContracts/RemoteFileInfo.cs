using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace White_Tiger.DataContracts
{
   [DataContract]
    public class RemoteFileInfo
    {
        [DataMember]
        public string FileName;
        [DataMember]
        public long Length;
       [DataMember] 
       public byte [] FileByteStream;
       [DataMember]
       public string error;
       
    }
}
