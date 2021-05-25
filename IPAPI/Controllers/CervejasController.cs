using IPAPI.Data;
using IPAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CervejasController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public CervejasController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<CervejasController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Cervejas.ToListAsync());
        }

        // GET api/<CervejasController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cerveja = await _dbContext.Cervejas.FindAsync(id);
            if (cerveja == null)
            {
                return NotFound("No record found against this Id");
            }
            return Ok(cerveja);
        }

        // POST api/<CervejasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cerveja cerveja)
        {
            await _dbContext.Cervejas.AddAsync(cerveja);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CervejasController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cerveja cervObj)
        {
            var cerveja = await _dbContext.Cervejas.FindAsync(id);
            if (cerveja == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                cerveja.Name = cervObj.Name;
                cerveja.Type = cervObj.Type;
                await _dbContext.SaveChangesAsync();
                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<CervejasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cerveja = await _dbContext.Cervejas.FindAsync(id);
            if (cerveja == null)
            {
                return NotFound("No record found against this Id");
            }
            else
            {
                _dbContext.Cervejas.Remove(cerveja);
                await _dbContext.SaveChangesAsync();
                return Ok("Record deleted");
            }
        }
    }
}
