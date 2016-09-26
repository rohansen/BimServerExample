using BimServerAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Request for login info
            var serializerSettings = new JsonSerializerSettings { ContractResolver = new LowercaseContractResolver() };
            RestClient client = new RestClient("http://localhost:8082/json");
            RestRequest req = new RestRequest(Method.POST);
            var loginRequest = GetLoginTokenRequest("hansen.ronni@gmail.com", "rONNI4865");

            var reqjson = JsonConvert.SerializeObject(loginRequest, serializerSettings);
            req.AddParameter("application/json", reqjson, ParameterType.RequestBody);
            IRestResponse<LoginResponse> response = client.Execute<LoginResponse>(req);
            string token = response.Data.Response.Result;


            //request for project info
            //new Request
            req = new RestRequest(Method.POST);
            var projectReqest = GetProjectReqest(token, "BizagiEntry");

            var prJson = JsonConvert.SerializeObject(projectReqest, serializerSettings);
            req.AddParameter("application/json", prJson, ParameterType.RequestBody);
            IRestResponse<ProjectResponse> projectResponse = client.Execute<ProjectResponse>(req);
            var projectInfo = projectResponse.Data.Response.Result;


            //request for Revision Info
            //new Request
            req = new RestRequest(Method.POST);
            var revisionRequest = GetRevisionRequest(token, projectInfo.First().lastRevisionId);

            var revJson = JsonConvert.SerializeObject(revisionRequest, serializerSettings);
            req.AddParameter("application/json", revJson, ParameterType.RequestBody);
            IRestResponse<RevisionResponse> revisionResponse = client.Execute<RevisionResponse>(req);
            var revisionInfo = projectResponse.Data.Response.Result;


            //Get all Resources on revision
            //new request again
            req = new RestRequest(Method.POST);
            var dataObjectRequest = GetDataObjectsRequest(token, projectInfo.First().lastRevisionId);

            var doJson = JsonConvert.SerializeObject(dataObjectRequest, serializerSettings);
            req.AddParameter("application/json", doJson, ParameterType.RequestBody);
            IRestResponse<DataObjectResponse> dataObjectResponse = client.Execute<DataObjectResponse>(req);
            var rooms = dataObjectResponse.Data.Response.Result.Where(x => x.type == "IfcSpace" && x.Values.Any(y=>y.stringValue=="Room"));
           //get room properties
           

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


        public static BimServerRequest GetRevisionRequest(string token, int revisionId)
        {
            RevisionParameters parameters = new RevisionParameters(revisionId);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getRevision";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);
            return bmr;
        }
        public static BimServerRequest GetProjectReqest(string token, string projectName)
        {
            ProjectParameters parameters = new ProjectParameters(projectName);
            Request req = new Request();
            req.Interface = "Bimsie1ServiceInterface";
            req.Method = "getProjectsByName";
            req.Parameters = parameters;
            BimServerRequest bmr = new BimServerRequest(token, req);
            return bmr;
        }
        public static BimServerRequest GetLoginTokenRequest(string username, string password)
        {
            LoginParameters loginParams = new LoginParameters(username, password);
            Request req = new Request();
            req.Interface = "Bimsie1AuthInterface";
            req.Method = "login";
            req.Parameters = loginParams;
            BimServerRequest bmr = new BimServerRequest("", req);
            return bmr;
        }
    }
    





    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
