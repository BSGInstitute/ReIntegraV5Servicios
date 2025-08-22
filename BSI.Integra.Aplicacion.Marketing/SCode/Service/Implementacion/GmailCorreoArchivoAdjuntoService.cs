using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: GmailCorreoArchivoAdjuntoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_GmailCorreoArchivoAdjunto
    /// </summary>
    public class GmailCorreoArchivoAdjuntoService : IGmailCorreoArchivoAdjuntoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public GmailCorreoArchivoAdjuntoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjunto>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public List<GmailCorreoArchivoAdjuntoDTO> obtenerCorreoArchivoAdjuntoPorId(int idCorreoArchivo)
        {
            try
            {
                return _unitOfWork.GmailCorreoArchivoAdjuntoRepository.obtenerCorreoArchivoAdjuntoPorId(idCorreoArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
