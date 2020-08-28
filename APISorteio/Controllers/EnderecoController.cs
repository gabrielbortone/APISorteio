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
        public EnderecoController(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            EnderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var enderecos = await EnderecoRepository.GetAll();

            if (enderecos == null)
            {
                return NoContent();
            }

            var enderecoDTOs = ConvertToDTOs(enderecos);

            return Ok(enderecoDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var endereco = await EnderecoRepository.Get(id);

            if (endereco == null)
            {
                return NotFound();
            }

            var enderecoDTO = ConvertToDTO(endereco);

            return Ok(enderecoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnderecoDTO enderecoDTO)
        {
            try
            {
                var endereco = ConvertToEndereco(enderecoDTO);
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
        public async Task<IActionResult> PutAsync(int id, [FromBody] EnderecoDTO enderecoDTO)
        {
            if (id != enderecoDTO.EnderecoId)
            {
                return BadRequest();
            }

            try
            {
                var endereco = ConvertToEndereco(enderecoDTO);
                await EnderecoRepository.Update(endereco);
                return Ok(enderecoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var endereco = await EnderecoRepository.Get(id);
            if (endereco == null)
            {
                return NotFound();
            }

            var enderecoDto = ConvertToDTO(endereco);
            await EnderecoRepository.Delete(id);

            return Ok(enderecoDto);
        }

        private List<EnderecoDTO> ConvertToDTOs(IEnumerable<Endereco> enderecos)
        {
            List<EnderecoDTO> ListaEnderecosDtos = new List<EnderecoDTO>();
            foreach(Endereco e in enderecos)
            {
                EnderecoDTO aux = new EnderecoDTO(e.Logradouro, e.Bairro, e.Cidade, e.Estado, e.Pais);
                ListaEnderecosDtos.Add(aux);
            }
            return ListaEnderecosDtos;
        }

        private EnderecoDTO ConvertToDTO(Endereco endereco)
        {
            EnderecoDTO aux = new EnderecoDTO(endereco.Logradouro, endereco.Bairro, endereco.Cidade, endereco.Estado, endereco.Pais);
            return aux;
        }

        private Endereco ConvertToEndereco(EnderecoDTO enderecoDTO)
        {
            Endereco aux = new Endereco(enderecoDTO.Logradouro, enderecoDTO.Bairro, enderecoDTO.Cidade,
                enderecoDTO.Estado, enderecoDTO.Pais);
            return aux;
        }
    }
}
