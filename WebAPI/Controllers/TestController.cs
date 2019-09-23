using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{/// <summary>
/// 
/// </summary>
    public class TestController : ApiController
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="name"></param>
     /// <returns></returns>
        public String Get(string name)
        {
            return "gat" + name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string login()
        {
            return "123";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Model.ResponeData getUser(string name)
        {
            return new Model.ResponeData() { code = 1, message = name, data = "" };
        }

        [HttpPost]
        public HttpResponseMessage login2([FromBody] userInfo userInfo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { errorCode = "0", value = userInfo.name });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage login3([FromBody] string name)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { errorCode = "1", value = name });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData login4([FromBody] userInfo userInfo)
        {
            return new Model.ResponeData() { code = 1, message = userInfo.name, data = "" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Model.ResponeData login6()
        {
            //List<Model.UserItem>
            var user = BLL.APIUserService.UserLogOn2();
            return new Model.ResponeData() { code = 1, message = "xxx", data = user };
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class userInfo
    {
        public string name { get; set; }

        //public string pwd { get; set; }
    }
}
