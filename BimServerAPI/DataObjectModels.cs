using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerAPI
{

    public class DataObjectParameters : Parameters
    {
        public DataObjectParameters(int revisionId)
        {
            roid = revisionId;
        }
        public int roid { get; set; }
    }
    public class DataObjectByTypeParameters : Parameters
    {
        public DataObjectByTypeParameters(int revisionId, string type)
        {
            roid = revisionId;
            className = type;
            packageName = "ifc4";
            flat = false;
        }
        public int roid { get; set; }
        public string className { get; set; }
        public string packageName { get; set; }
        public bool flat { get; set; }
    }


    //Responses
    public class DataObjectResponse
    {
        public DataObjectData Response { get; set; }
    }

    public class DataObjectData
    {
        public List<DataObjectResult> Result { get; set; }
    }

    public class DataObjectResult
    {
        public List<DataValues> Values { get; set; }

        public string __type { get; set; }
        public string guid { get; set; }
        public string name { get; set; }
        public int oid { get; set; }
        public int rid { get; set; }
        public string type { get; set; }
    }

    public class DataValues
    {
        public string __type { get; set; }
        public string fieldName { get; set; }
        public int oid { get; set; }
        public int rid { get; set; }
        public string stringValue { get; set; }
        public object guid { get; set; }
        public string typeName { get; set; }
        public List<DataValues> values { get; set; }
    }

  
}
