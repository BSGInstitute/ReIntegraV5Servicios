using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ContenidoCertificadoIrcaService
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_ContenidoCertificadoIrca
    /// </summary>
    public class ContenidoCertificadoIrcaService : IContenidoCertificadoIrcaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ContenidoCertificadoIrcaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContenidoCertificadoIrca, ContenidoCertificadoIrca>(MemberList.None).ReverseMap();
                cfg.CreateMap<TContenidoCertificadoIrca, ContenidoCertificadoIrcaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ContenidoCertificadoIrca, ContenidoCertificadoIrcaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public void InsertarListaContenidoCertificadoIrca(List<ContenidoCertificadoIrcaDTO> objs)
        {
            try
            {

                _unitOfWork.ContenidoCertificadoIrcaRepository.InsertarListaContenidoCertificadoIrca(objs);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
