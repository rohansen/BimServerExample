using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerAPI
{
    public class ProjectParameters : Parameters
    {
        public ProjectParameters(string projectname)
        {
            Name = projectname;
        }
        public string Name { get; set; }
    }


    //Responses
    public class ProjectResponse
    {
        public ProjectData Response { get; set; }
    }

    public class ProjectData
    {
        public List<ProjectDataResult> Result { get; set; }
    }

    public class ProjectDataResult
    {
        public string __type { get; set; }
        public List<object> checkouts { get; set; }
        public List<int> concreteRevisions { get; set; }
        public int createdById { get; set; }
        public long createdDate { get; set; }
        public string description { get; set; }
        public string exportLengthMeasurePrefix { get; set; }
        public List<object> extendedData { get; set; }
        public int geoTagId { get; set; }
        public List<int> hasAuthorizedUsers { get; set; }
        public int id { get; set; }
        public int lastConcreteRevisionId { get; set; }
        public int lastRevisionId { get; set; }
        public List<int> logs { get; set; }
        public List<object> modelCheckers { get; set; }
        public string name { get; set; }
        public int oid { get; set; }
        public int parentId { get; set; }
        public List<int> revisions { get; set; }
        public int rid { get; set; }
        public string schema { get; set; }
        public bool sendEmailOnNewRevision { get; set; }
        public List<object> services { get; set; }
        public string state { get; set; }
        public List<object> subProjects { get; set; }

    }
}
