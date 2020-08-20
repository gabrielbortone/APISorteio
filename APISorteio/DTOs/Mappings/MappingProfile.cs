using APISorteio.Models;
using AutoMapper;

namespace APISorteio.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Participante, ParticipanteDTO>().ReverseMap();
            CreateMap<Sorteio, SorteioDTO>().ReverseMap();
        }
    }
}
