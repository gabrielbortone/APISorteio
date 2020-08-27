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
    public class ParticipanteController : ControllerBase
    {
        private IParticipanteRepository ParticipanteRepository;
        private IEnderecoRepository EnderecoRepository;
        private IParticipanteSorteioRepository ParticipanteSorteioRepository;
        private IMapper _mapper;
        public ParticipanteController(IParticipanteRepository participanteRepository, IParticipanteSorteioRepository participanteSorteioRepository, 
            IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            ParticipanteRepository = participanteRepository;
            ParticipanteSorteioRepository = participanteSorteioRepository;
            EnderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var participantes = ParticipanteRepository.GetAll();

            if(participantes == null)
            {
                return NoContent();
            }

            var participanteDTOs = _mapper.Map<List<ParticipanteDTO>>(participantes);

            return Ok(participanteDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var participante = ParticipanteRepository.Get(id);

            if (participante == null)
            {
                return NotFound();
            }

            var participanteDTO = _mapper.Map<ParticipanteDTO>(participante);

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
                var participante = _mapper.Map<Participante>(participanteDTO);
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
        public IActionResult Put(int id, [FromBody] ParticipanteDTO participanteDTO)
        {
            if(id != participanteDTO.ParticipanteId)
            {
                return BadRequest();
            }

            try
            {
                var participante = _mapper.Map<Participante>(participanteDTO);
                ParticipanteRepository.Update(participante);
                return Ok(participanteDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var participante = ParticipanteRepository.Get(id);
            if(participante == null)
            {
                return NotFound();
            }

            var participanteDto = _mapper.Map<ParticipanteDTO>(participante);
            ParticipanteRepository.Delete(id);

            return Ok(participanteDto);
        }

    }
}
