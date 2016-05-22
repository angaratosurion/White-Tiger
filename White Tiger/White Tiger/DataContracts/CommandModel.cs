using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace White_Tiger.DataContracts
{
    [DataContract]
   public class CommandModel
    {
        /* args[0] root
                           args[1] record tag
                           * args[2] username
                           * args[3] password
                           * args[4] db name
                           * args[5] tablename
                 [DataMember]         */
        [DataMember]
        public string commandname { get; set; }
        [DataMember]
       public string root { get; set; }
        [DataMember]
       public string recordtag { get; set; }
        [DataMember]
       public string username { get; set; }
        [DataMember]
       public string password { get; set; }
        [DataMember]
       public string dbname { get; set; }
        [DataMember]
       public string tablename { get; set; }
        [DataMember]
       public string data { get; set; }
        [DataMember]
       public string algorith { get; set; }
        [DataMember]
       public string hashalg
       {
           get;
           set;
       }
        [DataMember]
       public string passphrase { get; set; }
        [DataMember]
       public string friendlyerrormsg { get; set; }
        [DataMember]
       public string exception { get; set; }
        [DataMember]
       public string dataformat { get; set; }
   }
}
