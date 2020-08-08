using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PePets_API.Models;
using PePets_API.Repositories;

namespace PePets_API.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Post> GetById(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Post>> Create([FromBody]Post post)
        {
            if (ModelState.IsValid == false)
                return BadRequest();

            await _postRepository.CreateAsync(post);

            return CreatedAtAction(nameof(Create), new { id = post.Id }, post);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
