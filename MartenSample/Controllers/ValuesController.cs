using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace MartenSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDocumentStore _documentStore;

        public ValuesController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        [HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            using (var session = _documentStore.QuerySession())
            {
                return session.Query<BlogPost>();
            }
        }

        [HttpGet("{id}")]
        public BlogPost Get(int id)
        {
            using (var session = _documentStore.QuerySession())
            {
                return session
                    .Query<BlogPost>()
                    .Where(post => post.Id == id)
                    .FirstOrDefault();

            }
        }

        // POST api/values
        [HttpPost]
        public BlogPost Post([FromBody] BlogPost value)
        {
            using (var session = _documentStore.LightweightSession())
            {
                session.Store(value);
                session.SaveChanges();
                return value;

            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
