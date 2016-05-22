using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;
namespace White_Tiger
{
    [DataContract]
    
    public class DataInfo
    {
        
        [DataMember]
        public DataTable Table;
        [DataMember]
        public DataSet DataSet;
        [DataMember]
        public string dbname;
        [DataMember]
        public string tablename;
        [DataMember]
        public string[] TableCells;
        [DataMember]
        public string UserName;
        [DataMember]
        public string PassWord;
        [DataMember]
       public DataTable SearchResults;
        [DataMember ]
        public  Stream TableData;
        [DataMember]
        public Stream ResultsStrem;
        
      
        

    }
}
