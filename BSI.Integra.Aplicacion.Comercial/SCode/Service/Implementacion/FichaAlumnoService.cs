using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: FichaAlumnoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// </summary>
    public class FichaAlumnoService : IFichaAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        public FichaAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene FichaAlumno por usuario
        /// </summary>
        /// <param name="usuario"> Nombre de usuario </param>
        /// <returns> FichaAlumnoCaracteristicaAgrupadoDTO </returns>
        public ActividadAgendaDTO CrearOportunidadFicha(OportunidadFichaDTO dto)
        {
            try
            {
                var reprogramacionService = new AgendaReprogramacionService(_unitOfWork);
                reprogramacionService.FlagValidacionVentaCruzada = false;
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(dto.IdOportunidadRN2);
                if (oportunidad == null)
                {
                    throw new BadRequestException("No existe la oportunidad");
                }
                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto.Value);
                if (pespecifico == null)
                {
                    throw new BadRequestException("No existe el Programa Especifico");
                }
                var alummno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);
                //var alummno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno);
                if (alummno == null)
                {
                    throw new BadRequestException("El alumno no existe");
                }
                var idProgramaGeneral = pespecifico.IdProgramaGeneral.Value == 7794 ? 604 : pespecifico.IdProgramaGeneral.Value;

                var pgeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idProgramaGeneral);
                //var alummno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno);
                if (pgeneral == null || pgeneral.Id == 0)
                {
                    throw new BadRequestException($"El Programa no existe, {dto.IdPgeneral}");
                }
                var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdAlumno(oportunidad.IdAlumno.Value);

                var centroCostos = _unitOfWork.PGeneralRepository.ObtenerCentroCostoPorIdPgeneralFicha(idProgramaGeneral, alummno.IdCodigoPais, null);
                if (centroCostos.Count() == 0)
                {
                    centroCostos = _unitOfWork.PGeneralRepository.ObtenerCentroCostoPorIdPgeneralFicha(idProgramaGeneral, null, null);
                }
                //var centroCostos = _unitOfWork.PGeneralRepository.ObtenerCentroCostoPorIdPgeneralFicha(dto.IdPgeneral, idPais, idCiudad);
                if (centroCostos.Count() == 0)
                {
                    throw new BadRequestException("No se encontro centro de costo asociado al programa y pais");
                }
                ReprogramacionDTO oportunidadReprogramacionNueva = new ReprogramacionDTO();

                oportunidadReprogramacionNueva.Oportunidad = new Oportunidad();
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            //Logica Nueva Oportunidad Actividad
                            oportunidadReprogramacionNueva.Oportunidad.IdFaseOportunidad = dto.IdFaseOportunidad;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAsignado = dto.IdPersonalAsignado;
                            oportunidadReprogramacionNueva.Oportunidad.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                            oportunidadReprogramacionNueva.Oportunidad.IdOrigen = 1396; //Marcador Predictivo Bases Propias
                            oportunidadReprogramacionNueva.Oportunidad.IdAlumno = oportunidad.IdAlumno.Value;
                            oportunidadReprogramacionNueva.Oportunidad.UltimaFechaProgramada = null;
                            oportunidadReprogramacionNueva.Oportunidad.UltimoComentario = "Predictivo";
                            oportunidadReprogramacionNueva.IdTipoInteraccion = 15;
                            oportunidadReprogramacionNueva.Oportunidad.IdCategoriaOrigen = 623;//Marcador Predictivo Bases Propias
                            oportunidadReprogramacionNueva.Oportunidad.IdSubCategoriaDato = 2610;
                            oportunidadReprogramacionNueva.Oportunidad.IdCentroCosto = centroCostos.FirstOrDefault()!.Id;
                            oportunidadReprogramacionNueva.Oportunidad.FechaRegistroCampania = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.Estado = true;
                            oportunidadReprogramacionNueva.Oportunidad.FechaCreacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.FechaModificacion = DateTime.Now;
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioCreacion = dto.Usuario + "_Predictivo";
                            oportunidadReprogramacionNueva.Oportunidad.UsuarioModificacion = dto.Usuario;
                            oportunidadReprogramacionNueva.Oportunidad.IdClasificacionPersona = clasificacionPersona.Id;
                            oportunidadReprogramacionNueva.Oportunidad.IdPersonalAreaTrabajo = ValorEstatico.IdPersonalAreaTrabajoVentas;

                            //SE CREA UNA NUEVA OPORTUNIDAD
                            reprogramacionService.CrearOportunidad(ref oportunidadReprogramacionNueva, false, TipoPersona.Alumno);

                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            // scope.Dispose();
                            List<string> correos = new List<string>
                            {
                                "sistemas@bsginstitute.com"
                            };
                            TMKMailDataDTO mailData = new TMKMailDataDTO();
                            mailData.Sender = "jcayo@bsginstitute.com";
                            mailData.Recipient = string.Join(",", correos);
                            mailData.Subject = "Error Creacion Oportunidad Transaction";
                            mailData.Message = "IdAlumno: " + oportunidad.IdAlumno.Value.ToString() + "<br/>Asesor:" + dto.Usuario == null ? "" : dto.Usuario + "<br/><br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                            mailData.Cc = "";
                            mailData.Bcc = "";
                            mailData.AttachedFiles = null;
                            try
                            {
                                TMK_MailService mailService = new TMK_MailService();
                                mailService.SetData(mailData);
                                mailService.SendMessageTask();
                            }
                            catch { }
                            throw;
                        }
                    }
                    ///01/02/2021
                    ///Calculo nuevo modelo predictivo
                    ///Carlos Crispin Riquelme
                    try
                    {
                        var nuevaProbabilidad = _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivo(oportunidadReprogramacionNueva.Oportunidad.Id);
                    }
                    catch (Exception e)
                    {
                    }
                    return _unitOfWork.OportunidadRepository.ObtenerDatosOportunidad(oportunidadReprogramacionNueva.Oportunidad!.Id)!;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdOportunidadRN2(int idOportunidadRN2)
        {
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidadRN2);
            if (oportunidad == null)
            {
                throw new BadRequestException("No existe la oportunidad");
            }
            return _unitOfWork.AlumnoRepository.ObtenerInformacionAlumnoPorIdAlumno(oportunidad.IdAlumno.Value);
        }
        public ResultadoFinalDTO ActualizarContestoPredictivo(int idOportunidadRN2)
        {
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidadRN2);
            if (oportunidad == null)
            {
                throw new BadRequestException("No existe la oportunidad");
            }
            return _unitOfWork.AlumnoRepository.ActualizarContestoPredictivo(oportunidad.IdAlumno.Value);
        }
        public ResultadoFinalDTO ActualizarCreoOportunidadPredictivo(int idOportunidadRN2, int idOportunidadCreada)
        {
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidadRN2);
            if (oportunidad == null)
            {
                throw new BadRequestException("No existe la oportunidad");
            }
            return _unitOfWork.AlumnoRepository.ActualizarCreoOportunidadPredictivo(oportunidad.IdAlumno.Value, idOportunidadCreada);
        }
        public (IEnumerable<ComboDTO> programas, IEnumerable<FaseOportunidadComboDTO> fasesOportunidad) ObtenerCombos()
        {
            var programas = _unitOfWork.PGeneralRepository.ObtenerPGeneralLanzamientoPorEjecucion();
            var fasesOportunidad = _unitOfWork.FaseOportunidadRepository.ObtenerCombo();
            return (programas, fasesOportunidad);
        }
        public ActividadAgendaDTO? ObtenerOportunidadPredictivo(int idOportunidadRN2)
        {
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidadRN2);
            if (oportunidad == null)
            {
                throw new BadRequestException("No existe la oportunidad");
            }
            return _unitOfWork.OportunidadRepository.ObtenerOportunidadPredictivo(oportunidad.IdAlumno.Value);
        }
        public ComboDTO? ObtenerProgramaGeneralPredictivo(int idOportunidadRN2)
        {
            return _unitOfWork.OportunidadRepository.ObtenerProgramaGeneralPredictivo(idOportunidadRN2);
        }
    }
}
