using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerAPI
{
    public class RevisionParameters : Parameters
    {
        public RevisionParameters(int revisionId)
        {
            Roid = revisionId;
        }
        public int Roid { get; set; }
    }
    public class RevisionProjectParameters : Parameters
    {
        public RevisionProjectParameters(int projectId)
        {
            Poid = projectId;
        }
        public int Poid { get; set; }
    }


    //Responses
    public class RevisionResponse
    {
        public ProjectData Response { get; set; }
    }

    public class RevisionData
    {
        public RevisionResult Result { get; set; }
    }

    public class RevisionResult
    {
        public string __type { get; set; }
        public object bmi { get; set; }
        public List<object> checkouts { get; set; }
        public string comment { get; set; }
        public List<int> concreteRevisions { get; set; }
        public long date { get; set; }
        public List<object> extendedData { get; set; }
        public bool hasGeometry { get; set; }
        public int id { get; set; }
        public int lastConcreteRevisionId { get; set; }
        public object lastError { get; set; }
        public List<int> logs { get; set; }
        public int oid { get; set; }
        public int projectId { get; set; }
        public int rid { get; set; }
        public int serviceId { get; set; }
        public int size { get; set; }
        public object tag { get; set; }
        public int userId { get; set; }
    }
}
