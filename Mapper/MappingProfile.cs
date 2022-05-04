using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newton_FTP_API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Log, DTO.Log>();
            CreateMap<DTO.Log, Models.Log>();

            CreateMap<DAO.Log, Models.Log>();
            CreateMap<Models.Log, DAO.Log>();

            CreateMap<Models.FTP, DTO.FTP>();
            CreateMap<DTO.FTP, Models.FTP>();
        }
    }
}
