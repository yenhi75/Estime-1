using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using ESTIME.BusinessLibrary;
using ESTIME.BusinessLibrary.Object;

namespace ESTIME.RESTfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CodeSetController : ControllerBase
    {
        //protected readonly IConfiguration config;
        private CodeSetManager codesetManager;
        public CodeSetController(CodeSetManager manager)
        {
            //this.config = config;
            codesetManager = manager;
        }
        // GET: api/CodeSet
        [HttpGet]
        public List<CodeSet> GetCodeSetList()
        {
            return codesetManager.GetCodesetList();
        }

        // GET: api/CodeSet/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CodeSet
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/CodeSet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
