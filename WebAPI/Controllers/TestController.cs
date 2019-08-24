using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public String Get(string name)
        {
            return "gat" +name;
        }

        [HttpPost]
        public string login()
        {
            return "123";
        }

        public Model.ResponeData getUser(string name)
        {
            return new Model.ResponeData() { code = 1, message = name, data = "" };
        }

        [HttpPost]
        public HttpResponseMessage login2([FromBody] userInfo userInfo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { errorCode = "0", value = userInfo.name });
        }

        [HttpGet]
        public HttpResponseMessage login3([FromBody] string name)
        {            
            return Request.CreateResponse(HttpStatusCode.OK, new { errorCode = "1", value = name });
        }

        [HttpPost]
        public Model.ResponeData login4([FromBody] userInfo userInfo)
        {
            return new Model.ResponeData() { code = 1, message = userInfo.name, data = "" };
        }

        [HttpPost]
        public Model.ResponeData login6()
        {
            ///List<Model.UserItem>
            var user = BLL.APIUserService.UserLogOn2();
            return new Model.ResponeData() { code = 1, message = "xxx", data = user };
        }
    }

    public class userInfo
    {
        public string name { get; set; }

        //public string pwd { get; set; }
    }
}
