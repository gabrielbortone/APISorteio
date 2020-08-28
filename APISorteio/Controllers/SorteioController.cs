using APISorteio.Data.Repositories.Interfaces;
using APISorteio.DTOs;
using APISorteio.Models;
using APISorteio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISorteio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SorteioController : ControllerBase
    {
        private ISorteioRepository SorteioRepository;
        private readonly UserManager<Administrador> _userManager;
        private SorteioService SorteioService;

        public SorteioController(ISorteioRepository sorteioRepository, UserManager<Administrador> userManager,
            SorteioService sorteioService)
        {
            SorteioRepository = sorteioRepository;
            _userManager = userManager;
            SorteioService = sorteioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sorteios = await SorteioRepository.GetAll();

            if(sorteios == null)
            {
                return NoContent();
            }

            var sorteioDTOs = ConvertToDTOs(sorteios);

            return Ok(sorteioDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sorteio = await SorteioRepository.Get(id);

            if (sorteio == null)
            {
                return NotFound();
            }

            var sorteioDTO = ConvertToDTO(sorteio);

            return Ok(sorteioDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SorteioDTO sorteioDto)
        {
            try
            {
                var sorteio = await ConvertToSorteio(sorteioDto);

                sorteio.SorteioId = await SorteioRepository.Add(sorteio);

                return new CreatedAtRouteResult("Get",
                    new { id = sorteio.SorteioId }, sorteioDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("GetVencedoresSorteio")]
        public async Task<IActionResult> PostRealizarSorteio([FromBody] SorteioDTO sorteioDto)
        {
            try
            {
                var sorteio = await ConvertToSorteio(sorteioDto);

                var vencedores = await SorteioService.GetVencedoresSorteio(sorteio);

                return Ok(vencedores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SorteioDTO sorteioDto)
        {
            if(id != sorteioDto.SorteioId)
            {
                return BadRequest();
            }

            try
            {
                var sorteio = await ConvertToSorteio(sorteioDto);
                await SorteioRepository.Update(sorteio);
                return Ok(sorteioDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sorteio = await SorteioRepository.Get(id);
            if(sorteio == null)
            {
                return NotFound();
            }

            var sorteioDto = ConvertToDTO(sorteio);
            await SorteioRepository.Delete(id);

            return Ok(sorteioDto);
        }

        private List<SorteioDTO> ConvertToDTOs(IEnumerable<Sorteio> sorteios)
        {
            List<SorteioDTO> sorteioDTOs = new List<SorteioDTO>();
            foreach(Sorteio s in sorteios)
            {
                SorteioDTO aux = new SorteioDTO(s.Titulo,s.Descricao,s.Premio, s.NumeroDeGanhadores,
                    new DataCompletaDTO(s.DataFinalizacaoCadastro.Year, s.DataFinalizacaoCadastro.Month,
                    s.DataFinalizacaoCadastro.Day, s.DataFinalizacaoCadastro.Hour, s.DataFinalizacaoCadastro.Minute), 
                    new DataCompletaDTO(s.DataSorteio.Year, s.DataSorteio.Month, s.DataSorteio.Day,
                    s.DataSorteio.Hour, s.DataSorteio.Minute));
            }
            return sorteioDTOs;
        }

        private SorteioDTO ConvertToDTO(Sorteio sorteio)
        {
            SorteioDTO aux = new SorteioDTO(sorteio.Titulo, sorteio.Descricao, sorteio.Premio,
                sorteio.NumeroDeGanhadores, new DataCompletaDTO(sorteio.DataFinalizacaoCadastro.Year,
                sorteio.DataFinalizacaoCadastro.Month, sorteio.DataFinalizacaoCadastro.Day, sorteio.DataFinalizacaoCadastro.Hour, sorteio.DataFinalizacaoCadastro.Minute),
                new DataCompletaDTO(sorteio.DataSorteio.Year, sorteio.DataSorteio.Month, sorteio.DataSorteio.Day, sorteio.DataSorteio.Hour, sorteio.DataSorteio.Minute));
            return aux;
        }

        private async Task<Sorteio> ConvertToSorteio(SorteioDTO sorteioDto)
        {
            DateTime dataFinalizacao = new DateTime(sorteioDto.DataFinalizacaoCadastro.Ano, sorteioDto.DataFinalizacaoCadastro.Mes,
                sorteioDto.DataFinalizacaoCadastro.Dia, sorteioDto.DataFinalizacaoCadastro.Hora, sorteioDto.DataFinalizacaoCadastro.Minuto, 0, 0);
            DateTime dataSorteio = new DateTime(sorteioDto.DataSorteio.Ano, sorteioDto.DataSorteio.Mes, sorteioDto.DataSorteio.Dia, 
                sorteioDto.DataSorteio.Hora, sorteioDto.DataSorteio.Minuto, 0, 0);
            Sorteio sorteio = new Sorteio(sorteioDto.Titulo, sorteioDto.Descricao, sorteioDto.Premio,
                sorteioDto.NumeroDeGanhadores, await _userManager.GetUserAsync(User), dataFinalizacao, dataSorteio);
            return sorteio;
        }

    }
}
