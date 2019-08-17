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

        //[HttpPost]
        //public Model.ResponeData login5([FromBody] Model.UserItem userInfo)
        //{
        //    ///登录方法 Model.UserItem
        //    var user = BLL.LoginService.UserLogOn(userInfo.Account, userInfo.Password);
        //  //  var userList = BLL.UserService.GetUserList();
        //    return new Model.ResponeData() { code = 1, message = "xxx", data = user }; 
        //}

        [HttpPost]
        public Model.ResponeData login6([FromBody] Model.UserItem userInfo)
        {
            ///List<Model.UserItem>
            var user = BLL.UserAPIService.UserLogOn2(userInfo.Account, userInfo.Password);
            return new Model.ResponeData() { code = 1, message = "xxx", data = user };
        }
    }

    public class userInfo
    {
        public string name { get; set; }

        //public string pwd { get; set; }
    }
}
