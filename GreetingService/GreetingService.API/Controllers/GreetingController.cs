﻿using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreetingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingRepository _greetingRepository;

        public GreetingController(IGreetingRepository greetingRepository)
        {
            _greetingRepository = greetingRepository;
        }

        // GET: api/<GreetingController>
        [HttpGet]
        public IEnumerable<Greeting> Get()
        {
            return _greetingRepository.Get();
        }

        // GET api/<GreetingController>/5
        [HttpGet("{id}")]
        public Greeting Get(Guid id)
        {
            return _greetingRepository.Get(id);
        }

        // POST api/<GreetingController>
        [HttpPost]
        public void Post([FromBody] Greeting greeting)
        {
            _greetingRepository.Create(greeting);
        }

        // PUT api/<GreetingController>
        [HttpPut]
        public void Put([FromBody] Greeting greeting)
        {
            _greetingRepository.Update(greeting);
        }
    }
}
