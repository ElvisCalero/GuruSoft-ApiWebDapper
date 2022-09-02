using ApiPostgresDapper.Domain.DTOs;
using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiPostgresDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeticionController : ControllerBase
    {
        private readonly IPeticionRepository _peticionRepository;
        public PeticionController(IPeticionRepository peticionRepository) => _peticionRepository = peticionRepository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _peticionRepository.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Peticion entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string problem = "";
            string cadena = entity.Request;

            int posicion = cadena.Length / 2;
            if (cadena.Length % 2 == 0)
                problem = cadena.Substring(posicion - 1, 2);
            else
                problem = cadena.Substring(posicion, 1);

            entity.Fecha_Request = DateTime.Now;
            entity.Response = problem;
            entity.Fecha_Response = DateTime.Now;

            var result = await _peticionRepository.Add(entity);
            return Created("Created", result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _peticionRepository.GetById(id));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Peticion entity)
        {
            if (id != entity.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string problem = "";
            string cadena = entity.Request;

            int posicion = cadena.Length / 2;
            if (cadena.Length % 2 == 0)
                problem = cadena.Substring(posicion - 1, 2);
            else
                problem = cadena.Substring(posicion, 1);
            
            entity.Response = problem;

            await _peticionRepository.Update(entity);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _peticionRepository.Delete(id);
            return NoContent();
        }        
    }
}


