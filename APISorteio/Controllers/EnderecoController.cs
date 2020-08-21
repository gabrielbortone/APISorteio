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
    public class EnderecoController : ControllerBase
    {
        private IEnderecoRepository EnderecoRepository;
        private IMapper _mapper;
        public EnderecoController(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            EnderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var enderecos = EnderecoRepository.GetAll();

            if (enderecos == null)
            {
                return NoContent();
            }

            var enderecoDTOs = _mapper.Map<List<EnderecoDTO>>(enderecos);

            return Ok(enderecoDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var endereco = EnderecoRepository.Get(id);

            if (endereco == null)
            {
                return NotFound();
            }

            var enderecoDTO = _mapper.Map<EnderecoDTO>(endereco);

            return Ok(enderecoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnderecoDTO enderecoDTO)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(enderecoDTO);
                endereco.EnderecoId = await EnderecoRepository.Add(endereco);

                return new CreatedAtRouteResult("Get",
                    new { id = endereco.EnderecoId }, enderecoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EnderecoDTO enderecoDTO)
        {
            if (id != enderecoDTO.EnderecoId)
            {
                return BadRequest();
            }

            try
            {
                var endereco = _mapper.Map<Endereco>(enderecoDTO);
                EnderecoRepository.Update(endereco);
                return Ok(enderecoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var endereco = EnderecoRepository.Get(id);
            if (endereco == null)
            {
                return NotFound();
            }

            var enderecoDto = _mapper.Map<EnderecoDTO>(endereco);
            EnderecoRepository.Delete(id);

            return Ok(enderecoDto);
        }
    }
}
