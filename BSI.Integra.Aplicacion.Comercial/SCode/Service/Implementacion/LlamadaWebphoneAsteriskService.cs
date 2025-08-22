using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: LlamadaWebphoneAsteriskService
    /// Autor: Jonathan Caipo
    /// Fecha: 19/10/2022
    /// <summary>
    /// Gestión general de T_LlamadaWebphoneAsteriskService
    /// </summary>
    public class LlamadaWebphoneAsteriskService : ILlamadaWebphoneAsteriskService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public LlamadaWebphoneAsteriskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TLlamadaWebphoneAsterisk, LlamadaWebphoneAsterisk>(MemberList.None).ReverseMap();
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
        /// <returns>ValorIntDTO</returns>
        public LlamadaWebphoneAsterisk Insertar(NuevaLlamadaActividadDTO obj, string url, string usuario)
        {
            try
            {
                LlamadaWebphoneAsterisk llamadaWebphoneAsterisk = new LlamadaWebphoneAsterisk()
                {
                    FechaInicio = obj.FechaInicio,
                    FechaFin = obj.FechaInicio.AddSeconds(obj.DuracionContesto),
                    Anexo = obj.Anexo3CX,
                    TelefonoDestino = obj.TelefonoDestino,
                    IdActividadDetalle = obj.IdActividadDetalle,
                    IdLlamadaWebphoneTipo = 2,
                    CdrId = _unitOfWork.LlamadaWebphoneAsteriskRepository.ObtenerCdrIdRegularizacion().Valor!.Value,
                    DuracionTimbrado = 10,
                    DuracionContesto = obj.DuracionContesto,
                    NombreGrabacion = obj.NombreArchivo,
                    IdProveedorNube = 1,
                    Url = url,
                    //EsEliminado = null,
                    //FechaEliminacion = null,
                    NroBytes = obj.NroBytes,
                    FechaSubida = DateTime.Now,
                    GrabacionContrato = obj.GrabacionContrato,
                    IdServidorAsterisk = 2,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                };

                var resultado = _unitOfWork.LlamadaWebphoneAsteriskRepository.Add(llamadaWebphoneAsterisk);
                _unitOfWork.Commit();
                return _mapper.Map<LlamadaWebphoneAsterisk>(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/08/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <returns>ValorIntDTO</returns>
        public void RegularizarContadorCdrId()
        {
            try
            {
                int cdrIdRegularizacion = 20000000;
                var lista = _unitOfWork.LlamadaWebphoneAsteriskRepository.ObtenerRegistrosCdrId();
                lista.ForEach(x =>
                {
                    x.CdrId = cdrIdRegularizacion;
                    cdrIdRegularizacion++;
                });
                _unitOfWork.LlamadaWebphoneAsteriskRepository.Update(lista);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/10/2022
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <param name="url"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="duracionContesto"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public ValorIntDTO ModificarLlamadaWebphone(int idLlamada, string url, string nombreUsuario, int duracionContesto, int nroBytes)
        {
            try
            {
                return _unitOfWork.LlamadaWebphoneAsteriskRepository.ModificarLlamadaWebphone(idLlamada, url, nombreUsuario, duracionContesto, nroBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
