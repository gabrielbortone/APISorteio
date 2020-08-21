using APISorteio.Data.Repositories.Interfaces;
using APISorteio.DTOs;
using APISorteio.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
        private ISorteioRepository SorteioRepository;
        private IMapper _mapper;
        public SorteioController(ISorteioRepository sorteioRepository, IMapper mapper)
        {
            SorteioRepository = sorteioRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var sorteios = SorteioRepository.GetAll();

            if(sorteios == null)
            {
                return NoContent();
            }

            var sorteioDTOs = _mapper.Map<List<SorteioDTO>>(sorteios);

            return Ok(sorteioDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var sorteio = SorteioRepository.Get(id);

            if (sorteio == null)
            {
                return NotFound();
            }

            var sorteioDTO = _mapper.Map<SorteioDTO>(sorteio);

            return Ok(sorteioDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SorteioDTO sorteioDto)
        {
            try
            {
                var sorteio = _mapper.Map<Sorteio>(sorteioDto);

                sorteio.SorteioId = await SorteioRepository.Add(sorteio);

                return new CreatedAtRouteResult("Get",
                    new { id = sorteio.SorteioId }, sorteioDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SorteioDTO sorteioDto)
        {
            if(id != sorteioDto.SorteioId)
            {
                return BadRequest();
            }

            try
            {
                var sorteio = _mapper.Map<Sorteio>(sorteioDto);
                SorteioRepository.Update(sorteio);
                return Ok(sorteioDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sorteio = SorteioRepository.Get(id);
            if(sorteio == null)
            {
                return NotFound();
            }

            var sorteioDto = _mapper.Map<SorteioDTO>(sorteio);
            SorteioRepository.Delete(id);

            return Ok(sorteioDto);
        }

    }
}
