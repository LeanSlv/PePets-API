using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Post>> GetById(Guid id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return NotFound();

            return post;
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
        public async Task<IActionResult> Update(Guid id, [FromBody]Post post)
        {
            if (id != post.Id)
                return BadRequest();

            await _postRepository.UpdateAsync(post);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return NotFound();

            await _postRepository.DeleteAsync(post);

            return NoContent();
        }
    }
}
