using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyEcom.Common.RedisDto;
using MyEcom.Common.RoomDto;
using MyEcom.Core.Service;

namespace MyEcommerce.Controllers.Api
{
    public class MyApiController : ApiController
    {
        // GET: api/MyApi
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET: api/MyApi/5
        public RedisDto Get(int id)
        {
            var model= Services.RedisService.GetHotel(id);
            return model;
        }

        // POST: api/MyApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MyApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MyApi/5
        public void Delete(int id)
        {
        }
    }
}
