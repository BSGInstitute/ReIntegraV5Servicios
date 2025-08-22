using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: LlamadaWebphoneCruceCentralService
    /// Autor: Jonathan Caipo
    /// Fecha: 19/10/2022
    /// <summary>
    /// Gestión general de T_LlamadaWebphoneCruceCentralService
    /// </summary>
    public class LlamadaWebphoneCruceCentralService : ILlamadaWebphoneCruceCentralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public LlamadaWebphoneCruceCentralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TLlamadaWebphoneCruceCentral, LlamadaWebphoneCruceCentral>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public LlamadaWebphoneCruceCentral Insertar(NuevaLlamadaActividadDTO obj, LlamadaWebphoneAsterisk llamadaWebphone, string usuario)
        {
            try
            {
                LlamadaWebphoneCruceCentral llamadaWebphoneCruceCentral = new LlamadaWebphoneCruceCentral()
                {
                    IdLlamadaWebphone = llamadaWebphone.Id,
                    IdLlamadaCentral = 0,
                    FechaIncioLlamadaWebphone = llamadaWebphone.FechaInicio.Value,
                    FechaFinLlamadaWebphone = llamadaWebphone.FechaFin.Value,
                    FechaIncioLlamadaCentral = llamadaWebphone.FechaInicio.Value,
                    FechaFinLlamadaCentral = llamadaWebphone.FechaFin.Value,
                    AnexoWebphone = llamadaWebphone.Anexo,
                    AnexoCentral = llamadaWebphone.Anexo,
                    DuracionTimbradoWebPhone = llamadaWebphone.DuracionTimbrado,
                    DuracionContestoWebPhone = llamadaWebphone.DuracionTimbrado,
                    DuracionTimbradoCentral = llamadaWebphone.DuracionTimbrado,
                    DuracionContestoCentral = llamadaWebphone.DuracionContesto,
                    IdAlumno = 0,
                    IdActividadDetalle = obj.IdActividadDetalle,
                    TelefonoDestinoWebPhone = llamadaWebphone.TelefonoDestino.Substring(4),
                    TelefonoDestinoCentral = llamadaWebphone.TelefonoDestino.Substring(4),
                    IdLlamadaWebPhoneEstado = 1,
                    EstadoLlamadaCentral = "Llamada Exitosa",
                    SubEstadoLlamadaCentral = "Contestado terminado por el asesor",
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    //UrlAudio = null,
                    Troncal = "No Definido"
                };
                var resultado = _unitOfWork.LlamadaWebphoneCruceCentralRepository.Add(llamadaWebphoneCruceCentral);
                _unitOfWork.Commit();
                return _mapper.Map<LlamadaWebphoneCruceCentral>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
