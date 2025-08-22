using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: IProgramaGeneralPuntoCorteConfiguracionService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorteConfiguracion
    /// </summary>
    public class ProgramaGeneralPuntoCorteConfiguracionService : IProgramaGeneralPuntoCorteConfiguracionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
    }
}



