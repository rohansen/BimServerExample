using BimServerAPI;
using BusinessLogic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class BimServerController
    {
        private RestClient client;
        private JsonSerializerSettings serializerSettings;
        public BimServerController()
        {
            serializerSettings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
            client = new RestClient("http://localhost:8082/json");
        }

        public List<Project> GetAllProjects(string token)
        {
            //Build request structure
            ProjectParameters parameters = new ProjectParameters(null);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getAllProjectsSmall";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);

            //Issue request using RestSharp

            RestRequest restRequest = new RestRequest(Method.POST);

            var prJson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", prJson, ParameterType.RequestBody);
            IRestResponse<ProjectResponse> projectResponse = client.Execute<ProjectResponse>(restRequest);
            var projectInfo = projectResponse.Data.Response.Result;
            List<Project> projects = new List<Project>();
            foreach (var project in projectInfo)
            {
                projects.Add(new Project { Id = project.oid, Title = project.name });
            }

            return projects;
        }

        public string GetLoginToken(string username, string password)
        {
            //Build Request Structure
            LoginParameters loginParams = new LoginParameters(username, password);
            Request req = new Request();
            req.Interface = "Bimsie1AuthInterface";
            req.Method = "login";
            req.Parameters = loginParams;
            BimServerRequest bmr = new BimServerRequest("", req);

            //Issue Request using RestSharp
            RestRequest restRequest = new RestRequest(Method.POST);

            var reqjson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", reqjson, ParameterType.RequestBody);
            IRestResponse<LoginResponse> response = client.Execute<LoginResponse>(restRequest);
            string token = response.Data.Response.Result;
            return token;
        }
        //didnt get time to finish this guy TODO: FInish
        //public object GetSpaceProperties(int spaceId, int revisionId, string token)
        //{
        //    //call getDataObjectsByType with IfcRelDefinesByProperties, this will return a collection of this type
        //    //select the objects that have the value oid=id in their values collection 
        //    //from the remaining elements, get their ElementQuantities   (IfcElementQuantity) and their oid
        //    //from this data, we can get the base quantities
        //    //

            
        //    DataObjectByTypeParameters parameters = new DataObjectByTypeParameters(revisionId, "IfcRelDefinesByProperties");
        //    Request req = new Request();
        //    req.Interface = "Bimsie1LowLevelInterface";
        //    req.Method = "getDataObjectsByType";//get by IfcSpace
        //    req.Parameters = parameters;
        //    BimServerRequest bmr = new BimServerRequest(token, req);

        //    RestRequest restRequest = new RestRequest(Method.POST);

        //    var revJson = JsonConvert.SerializeObject(bmr, serializerSettings);
        //    restRequest.AddParameter("application/json", revJson, ParameterType.RequestBody);
        //    IRestResponse<DataObjectResponse> propertyResponse = client.Execute<DataObjectResponse>(restRequest);
        //    //now we have all data objects on a certain revision of type IfcRelDefinesByProperties
        //    var propData = propertyResponse.Data.Response.Result;

        //    //select the objects that have the value oid=id in their values collection 
        //    var related = propData.Select(x => x.Values).Where(x => x.values.Any(y => y.oid ==spaceId));//.Where(x=>x.Select(y=>y.values.())); (x => x.Values.Select(x=>x.values).Any(y=>y));//x.Values.Any(y => y.oid == spaceId)
        //    foreach (var item in related)
        //    {
        //        item.Where(x => x.values.Any(y => y.oid ==spaceId));
        //    }



        //    List<Space> spaces = new List<Space>();

        //    foreach (var item in related)
        //    {
        //        spaces.Add(new Space { Id = item.oid, Name = item.Values.FirstOrDefault(x => x.fieldName == "LongName").stringValue });
        //    }


        //    return spaces;
        //}

        public Project GetProjectByName(string projectName, string token)
        {
            //Build request structure
            ProjectParameters parameters = new ProjectParameters(projectName);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getProjectsByName";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);

            //Issue request using RestSharp

            RestRequest restRequest = new RestRequest(Method.POST);

            var prJson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", prJson, ParameterType.RequestBody);
            IRestResponse<ProjectResponse> projectResponse = client.Execute<ProjectResponse>(restRequest);
            var projectInfo = projectResponse.Data.Response.Result;

            return new Project { Id = projectInfo.First().oid, Title = projectInfo.First().name };
        }

        public Revision GetRevisionById(int revisionId, int projectId, string token)
        {
            RevisionParameters parameters = new RevisionParameters(revisionId);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getRevision";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);


            RestRequest restRequest = new RestRequest(Method.POST);

            var revJson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", revJson, ParameterType.RequestBody);
            IRestResponse<RevisionResponse> revisionResponse = client.Execute<RevisionResponse>(restRequest);
            var revisionInfo = revisionResponse.Data.Response.Result;
            return new Revision { Id = revisionInfo.First().oid };

        }
        public List<Revision> GetAllRevisionsByProject(int projectId, string token)
        {
            RevisionProjectParameters parameters = new RevisionProjectParameters(projectId);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getAllRevisionsOfProject";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);


            RestRequest restRequest = new RestRequest(Method.POST);

            var revJson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", revJson, ParameterType.RequestBody);
            IRestResponse<RevisionResponse> revisionResponse = client.Execute<RevisionResponse>(restRequest);
            var revisionInfo = revisionResponse.Data.Response.Result;

            List<Revision> revisions = new List<Revision>();
            foreach (var item in revisionInfo)
            {
                revisions.Add(new Revision { Id = item.oid });
            }

            return revisions;

        }




        public List<Space> GetSpace(int revisionId, string token)
        {
            DataObjectByTypeParameters parameters = new DataObjectByTypeParameters(revisionId,"IfcSpace");
            Request req = new Request();
            req.Interface = "Bimsie1LowLevelInterface";
            req.Method = "getDataObjectsByType";//get by IfcSpace
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);

            RestRequest restRequest = new RestRequest(Method.POST);

            var revJson = JsonConvert.SerializeObject(bmr, serializerSettings);
            restRequest.AddParameter("application/json", revJson, ParameterType.RequestBody);
            IRestResponse<DataObjectResponse> revisionResponse = client.Execute<DataObjectResponse>(restRequest);
            //now we have all data objects on a certain revision of type IfcSpace
            var revisionInfo = revisionResponse.Data.Response.Result;

            List<Space> spaces = new List<Space>();

            foreach (var item in revisionInfo)
            {
                spaces.Add(new Space { Id = item.oid, Name = item.Values.FirstOrDefault(x=>x.fieldName=="LongName").stringValue });
            }


            return spaces;


        }


        //public void Exe()
        //{
        //    //Request for login info
        //    RestRequest req = new RestRequest(Method.POST);
        //    var loginRequest = GetLoginTokenRequest("hansen.ronni@gmail.com", "rONNI4865");

        //    var reqjson = JsonConvert.SerializeObject(loginRequest, serializerSettings);
        //    req.AddParameter("application/json", reqjson, ParameterType.RequestBody);
        //    IRestResponse<LoginResponse> response = client.Execute<LoginResponse>(req);
        //    string token = response.Data.Response.Result;


        //    //request for project info
        //    //new Request
        //    req = new RestRequest(Method.POST);
        //    var projectReqest = GetProjectReqest(token, "BizagiEntry");

        //    var prJson = JsonConvert.SerializeObject(projectReqest, serializerSettings);
        //    req.AddParameter("application/json", prJson, ParameterType.RequestBody);
        //    IRestResponse<ProjectResponse> projectResponse = client.Execute<ProjectResponse>(req);
        //    var projectInfo = projectResponse.Data.Response.Result;


        //    //request for Revision Info
        //    //new Request
        //    req = new RestRequest(Method.POST);
        //    var revisionRequest = GetRevisionRequest(token, projectInfo.First().lastRevisionId);

        //    var revJson = JsonConvert.SerializeObject(revisionRequest, serializerSettings);
        //    req.AddParameter("application/json", revJson, ParameterType.RequestBody);
        //    IRestResponse<RevisionResponse> revisionResponse = client.Execute<RevisionResponse>(req);
        //    var revisionInfo = projectResponse.Data.Response.Result;


        //    //Get all Resources on revision
        //    //new request again
        //    req = new RestRequest(Method.POST);
        //    var dataObjectRequest = GetDataObjectsRequest(token, projectInfo.First().lastRevisionId);

        //    var doJson = JsonConvert.SerializeObject(dataObjectRequest, serializerSettings);
        //    req.AddParameter("application/json", doJson, ParameterType.RequestBody);
        //    IRestResponse<DataObjectResponse> dataObjectResponse = client.Execute<DataObjectResponse>(req);

        //    var objsWeNeed = dataObjectResponse.Data.Response.Result.Where(x => x.type == "IfcQuantityLength" && x.Values.Select(y => y.fieldName == "Height").Any() && x.oid == 198022).Select(x => x.Values);

        //}


        public BimServerRequest GetRoomParameter(string token, int revisionId)
        {
            //Step 1. get IfcRelDefinesByProperties from revision. It will have a reference to the IfcSpace id that a IfcElement Quantity relates to
            //Step 1. will give you a collection of SDataObjects (IfcRelDefinesByProperties)
            //Their values contains a values object with a subobject called "RelatedObjects".
            //This object has an object with the Type "IfcElementQuantity" that points to an Object that has a collection of properties called QuantityArea,Length asf.


            //Step 1
            RevisionParameters parameters = new RevisionParameters(revisionId);
            Request req = new Request();
            req.Interface = "Bimsie1LowLevelInterface";
            req.Method = "getDataObjects";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);
            return bmr;


        }

        public static BimServerRequest GetDataObjectsRequest(string token, int revisionId)
        {
            RevisionParameters parameters = new RevisionParameters(revisionId);
            Request req = new Request();
            req.Interface = "Bimsie1LowLevelInterface";
            req.Method = "getDataObjects";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);
            return bmr;
        }


        #region saved code



        //    public static BimServerRequest GetRevisionRequest(string token, int revisionId)
        //    {
        //        RevisionParameters parameters = new RevisionParameters(revisionId);
        //        Request req = new Request();
        //        req.Interface = "Bimsie1ServiceInterface";
        //        req.Method = "getRevision";
        //        req.Parameters = parameters;
        //        BimServerRequest bmr = new BimServerRequest(token, req);
        //        return bmr;
        //    }
        //    public static BimServerRequest GetProjectReqest(string token, string projectName)
        //    {
        //        ProjectParameters parameters = new ProjectParameters(projectName);
        //        Request req = new Request();
        //        req.Interface = "Bimsie1ServiceInterface";
        //        req.Method = "getProjectsByName";
        //        req.Parameters = parameters;
        //        BimServerRequest bmr = new BimServerRequest(token, req);
        //        return bmr;
        //    }
        //    public static BimServerRequest GetLoginTokenRequest(string username, string password)
        //    {
        //        LoginParameters loginParams = new LoginParameters(username, password);
        //        Request req = new Request();
        //        req.Interface = "Bimsie1AuthInterface";
        //        req.Method = "login";
        //        req.Parameters = loginParams;
        //        BimServerRequest bmr = new BimServerRequest("", req);
        //        return bmr;
        //    }

        //}

        #endregion

        public class LowercaseContractResolver : DefaultContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                if(propertyName== "packageName" || propertyName== "className")
                {
                    return propertyName;
                }
                return propertyName.ToLower();
            }
        }
    }
}
