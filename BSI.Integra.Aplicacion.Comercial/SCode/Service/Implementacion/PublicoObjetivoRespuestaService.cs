using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: PublicoObjetivoRespuestaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/08/2022
    /// <summary>
    /// Gestión general de T_PublicoObjetivoRespuesta
    /// </summary>
    public class PublicoObjetivoRespuestaService : IPublicoObjetivoRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PublicoObjetivoRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TPublicoObjetivoRespuestum, PublicoObjetivoRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PublicoObjetivoRespuestaDTO, PublicoObjetivoRespuesta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PublicoObjetivoRespuestaDTO, TPublicoObjetivoRespuestum>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PublicoObjetivoRespuesta
        /// </summary>
        /// <returns> List<PublicoObjetivoRespuestaDTO> </returns>
        public IEnumerable<PublicoObjetivoRespuesta> ObtenerPublicoObjetivoRespuesta()
        {
            try
            {
                return _unitOfWork.PublicoObjetivoRespuestaRepository.ObtenerPublicoObjetivoRespuesta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/03/2023
        /// Version: 1.0
        /// <summary>
        /// Registra el Programa General Motivacion Respuesta
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idMotivacion">Id de la Motivacion asociada a un Programa General</param>
        /// <returns> ProgramaGeneralMotivacionRespuesta </returns>
        public PublicoObjetivoRespuestaDTO GuardarCambios(PublicoObjetivoRespuestaDTO item, string userName)
        {
            try
            {
                PublicoObjetivoRespuesta publicoObjetivoRespuesta = _unitOfWork.PublicoObjetivoRespuestaRepository.ObtenerPorIdOportunidadIdDocumentoSeccion(item.IdOportunidad, item.IdDocumentoSeccionPw);
                TPublicoObjetivoRespuestum respuesta;
                if (publicoObjetivoRespuesta != null && publicoObjetivoRespuesta.Id != 0)
                {
                    publicoObjetivoRespuesta.NivelCumplimiento = item.NivelCumplimiento;
                    publicoObjetivoRespuesta.UsuarioModificacion = userName;
                    publicoObjetivoRespuesta.FechaModificacion = DateTime.Now;
                    respuesta = _unitOfWork.PublicoObjetivoRespuestaRepository.Update(publicoObjetivoRespuesta);
                }
                else
                {
                    publicoObjetivoRespuesta = new PublicoObjetivoRespuesta()
                    {
                        IdOportunidad = item.IdOportunidad,
                        IdDocumentoSeccionPw = item.IdDocumentoSeccionPw,
                        NivelCumplimiento = item.NivelCumplimiento,
                        Estado = true,
                        UsuarioCreacion = userName,
                        UsuarioModificacion = userName,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    respuesta = _unitOfWork.PublicoObjetivoRespuestaRepository.Add(publicoObjetivoRespuesta);
                }
                _unitOfWork.Commit();
                return _mapper.Map<PublicoObjetivoRespuestaDTO>(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
