using APISorteio.Data.Repositories.Interfaces;
using APISorteio.DTOs;
using APISorteio.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipanteController : ControllerBase
    {
        private IParticipanteRepository ParticipanteRepository;
        private IEnderecoRepository EnderecoRepository;
        private IParticipanteSorteioRepository ParticipanteSorteioRepository;
        public ParticipanteController(IParticipanteRepository participanteRepository, IParticipanteSorteioRepository participanteSorteioRepository, 
            IEnderecoRepository enderecoRepository)
        {
            ParticipanteRepository = participanteRepository;
            ParticipanteSorteioRepository = participanteSorteioRepository;
            EnderecoRepository = enderecoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var participantes = await ParticipanteRepository.GetAll();

            if(participantes == null)
            {
                return NoContent();
            }

            var participanteDTOs = ConvertToListDTO(participantes);

            return Ok(participanteDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var participante = await ParticipanteRepository.Get(id);

            if (participante == null)
            {
                return NotFound();
            }

            var participanteDTO = ConvertToDTO(participante);

            return Ok(participanteDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParticipanteCadastroDTO participanteDTO)
        {
            var aux = ParticipanteRepository.GetParticipanteByCPF(participanteDTO.CPF);

            if(aux != null)
            {
                return Conflict();
            }

            try
            {
                var participante = ConvertToParticipante(participanteDTO);
                var endereco = participante.Endereco;

                endereco.EnderecoId = await EnderecoRepository.Add(endereco);
                participanteDTO.ParticipanteId = await ParticipanteRepository.Add(participante);

                return new CreatedAtRouteResult("Get",
                    new { id = participanteDTO.ParticipanteId }, participanteDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ParticipanteCompletoDTO participanteDTO)
        {
            if(id != participanteDTO.ParticipanteId)
            {
                return BadRequest();
            }

            try
            {
                var participante = ConvertToParticipante(participanteDTO);
                await ParticipanteRepository.Update(participante);
                return Ok(participanteDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var participante = await ParticipanteRepository.Get(id);
            if(participante == null)
            {
                return NotFound();
            }

            var participanteDto = ConvertToDTO(participante);
            await ParticipanteRepository.Delete(id);

            return Ok(participanteDto);
        }

        private List<ParticipanteDTO> ConvertToListDTO(IEnumerable<Participante> participantes)
        {
            List<ParticipanteDTO> listaParticipanteDTO = new List<ParticipanteDTO>();
            foreach (Participante p in participantes)
            {
                ParticipanteDTO auxDto = new ParticipanteDTO(p.Nome,p.Sobrenome, p.Email, p.Telefone);
                listaParticipanteDTO.Add(auxDto);
            }
            return listaParticipanteDTO;
        }

        private ParticipanteDTO ConvertToDTO(Participante participantes)
        {
            ParticipanteDTO auxDto = new ParticipanteDTO(participantes.Nome, participantes.Sobrenome, participantes.Email, participantes.Telefone);
            return auxDto;
        }

        private Participante ConvertToParticipante(ParticipanteCadastroDTO participante)
        {
            Endereco endereco = new Endereco(participante.EnderecoDTO.Logradouro, participante.EnderecoDTO.Bairro,
                participante.EnderecoDTO.Cidade, participante.EnderecoDTO.Estado, participante.EnderecoDTO.Pais);
            Participante aux = new Participante(participante.Nome, participante.Sobrenome, participante.CPF,
                participante.Email, participante.Telefone, endereco);
            return aux;
        }

        private Participante ConvertToParticipante(ParticipanteCompletoDTO participante)
        {
            Participante aux = new Participante(participante.Nome, participante.Sobrenome, participante.CPF,participante.Email, participante.Telefone);
            return aux;
        }
    }
}
