using AutoMapper;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CronogramaCabeceraCambioService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class CronogramaCabeceraCambioService : ICronogramaCabeceraCambioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CronogramaCabeceraCambioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCronogramaCabeceraCambio , CronogramaCabeceraCambio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CronogramaCabeceraCambio Add(CronogramaCabeceraCambio entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaCabeceraCambioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaCabeceraCambio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CronogramaCabeceraCambio Update(CronogramaCabeceraCambio entidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaCabeceraCambioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CronogramaCabeceraCambio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.CronogramaCabeceraCambioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaCabeceraCambio> Add(List<CronogramaCabeceraCambio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaCabeceraCambioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaCabeceraCambio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CronogramaCabeceraCambio> Update(List<CronogramaCabeceraCambio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CronogramaCabeceraCambioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CronogramaCabeceraCambio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.CronogramaCabeceraCambioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

       
        public object ObtenerSolicitudesCambios(int idPersonal)
        {
          
            try
            {
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                //return Ok(_repMatriculaCabecera.ObtenerSolicitudesCambioCronograma(idPersonal));
                var listaRpta = _repMatriculaCabecera.ObtenerSolicitudesCambioCronograma(idPersonal);
            
                return (listaRpta);
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }


        public object Aprobar( CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            
            try
            {
                CronogramaCabeceraCambioService cronogramaCabeceraCambioBO = new CronogramaCabeceraCambioService(_unitOfWork);
                List<string> listaCorreos = new List<string>
                {
                    //"bamontoya@bsginstitute.com",
                    "ccrispin@bsginstitute.com",
                    //"mapaza@bsginstitute.com"
                };
                var _repPersonal = _unitOfWork.PersonalRepository;
                var personalAprobador = _repPersonal.GetBy(x => x.Id == cronogramaDTO.IdPersonalAprobador, x => new { NombreCompleto = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();
                var rpta = 0;
                cronogramaDTO.IdsCambios = cronogramaDTO.IdsCambios.Replace(" ", "");
                string[] cambiosTemp = cronogramaDTO.IdsCambios.Split(',');
                var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repCronogramaDetalleCambioRepositorio = _unitOfWork.CronogramaDetalleCambioRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == cronogramaDTO.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                List<AprobarRechazarCronogramaDTO> ObjetoTemporalSincronizacion = new List<AprobarRechazarCronogramaDTO>();
                for (int i = 0; i < cambiosTemp.Length; i++)
                {
                    rpta = _repCronogramaCabeceraCambio.AprobarRechazarCambios(matriculaCabeceraTemp.Id, Convert.ToInt32(cambiosTemp[i]), cronogramaDTO.Version, true, false).Valor;
                    //rpta = 1;
                    ObjetoTemporalSincronizacion.Add(new AprobarRechazarCronogramaDTO { IdMatriculaCabecera = matriculaCabeceraTemp.Id, IdCambio = cambiosTemp[i], Version = cronogramaDTO.Version, Aprobado = true, Cancelado = false });
                }
                var datosMatricula = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id);
                var listaCambios = _repCronogramaDetalleCambioRepositorio.ObtenerCambiosPendientes(matriculaCabeceraTemp.Id, cronogramaDTO.Version);
                var centrocostoMatricula = _repMatriculaCabecera.ObtenerCentroCostoPorMatricula(matriculaCabeceraTemp.Id);
                var cambios = "";
                int cont = 1;
                foreach (var item in listaCambios)
                {
                    cambios = cambios + cont + ". " + item.TipoModificacion + " (" + item.SubTipo + "<br>";
                    cont++;
                }
                string mensaje = cronogramaCabeceraCambioBO.GenerarMensajeRespuestaCambioCronograma(cronogramaDTO.CodigoMatricula, datosMatricula[0].NombreCompleto, datosMatricula[0].PEspecifico, centrocostoMatricula.NombreCentroCosto, cronogramaDTO.Version, cronogramaDTO.NombreSolicitante, cambios, true, personalAprobador.NombreCompleto);
                HelperCorreo helperCorreo = new HelperCorreo();
                helperCorreo.envio_email("aportillavi@bsginstitute.com", "Integra Cronograma", "Respuesta de Cambio de Cronograma " + cronogramaDTO.CodigoMatricula, mensaje, listaCorreos);
                return (new { rpta, ObjetoTemporalSincronizacion });
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
        public string GenerarMensajeRespuestaCambioCronograma(string codigoMatricula, string alumno, string pEspecifico, string centroCosto, int version, string solicitadoPorNombre, string cambios, bool aprobar, string usuario)
        {
            var msmEstado = "";
            var UMsm = "";
            if (aprobar == true) { msmEstado = "Se aprobo el cambio"; UMsm = "Aprobado"; }
            else { msmEstado = "Se Rechazo el cambio"; UMsm = "Rechazado"; }
            string mensaje = string.Empty;
            mensaje += "<p>Estimad@ " + solicitadoPorNombre + " </p>";
            mensaje += "<p style='font-size:10pt;'><b>CODIGO DE MATRICULA :</b> " + codigoMatricula + "</p>";
            mensaje += "<p><b>Datos de la Matricula</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Alumno :</b> " + alumno + "</li>";
            mensaje += "<li><b>Programa:</b> " + pEspecifico + "</li>";
            mensaje += "<li><b>Centro Costo:</b> " + centroCosto + "</li>";
            mensaje += "</ul>";
            mensaje += "<p><b>Datos del Cambio Solicitado</b></p>";
            mensaje += "<ul>";
            mensaje += "<li><b>Estado: </b> " + msmEstado + "</li>";
            mensaje += "<li><b>" + UMsm + " Por: </b>" + usuario + "";
            mensaje += "<li><b>Version:</b> " + version + "</li>";
            mensaje += "<li><b>Cambios:</b> <br/>" + cambios + "</li>";
            mensaje += "</ul>";
            if (aprobar == true) { mensaje += "<p style='font-size:10pt;'><b>Ya se puede generar el CREP</b>" + "</p>"; }
            mensaje += "<br/>";
            mensaje += "<p>Saludos,</p>";
            return mensaje;
        }


        public object Rechazar( CronogramaCabeceraCambioAprobarDTO cronogramaDTO)
        {
            List<string> listaCorreos = new List<string>
            {
                "bamontoya@bsginstitute.com",
                "ccrispin@bsginstitute.com",
                "mapaza@bsginstitute.com"
            };
            try
            {
                var _repPersonal = _unitOfWork.PersonalRepository;
                var personalAprobador = _repPersonal.GetBy(x => x.Id == cronogramaDTO.IdPersonalAprobador, x => new { NombreCompleto = string.Concat(x.Nombres, " ", x.Apellidos) }).FirstOrDefault();

                var rpta = 0;
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repCronogramaDetalleCambioRepositorio = _unitOfWork.CronogramaDetalleCambioRepository;
                var _repCronogramaCabeceraCambio = _unitOfWork.CronogramaCabeceraCambioRepository;
                var matriculaCabeceraTemp = _repMatriculaCabecera.GetBy(x => x.CodigoMatricula == cronogramaDTO.CodigoMatricula, x => new { x.Id }).FirstOrDefault();
                var listaCambios = _repCronogramaDetalleCambioRepositorio.ObtenerCambiosPendientes(matriculaCabeceraTemp.Id, cronogramaDTO.Version);
                //var listaCambios = _tcrm_CentroCostoService.GetCambiosCronogramas(matricula, Convert.ToInt32(version));
                cronogramaDTO.IdsCambios = cronogramaDTO.IdsCambios.Replace(" ", "");
                string[] cambiosTemp = cronogramaDTO.IdsCambios.Split(',');
                List<AprobarRechazarCronogramaDTO> ObjetoTemporalSincronizacion = new List<AprobarRechazarCronogramaDTO>();
                for (int i = 0; i < cambiosTemp.Length; i++)
                {
                    //rpta = _tcrm_CentroCostoService.AprobarRechazarCambios(matricula, cambiostemp[i], Convert.ToInt32(version), 0, 1);
                    rpta = _repCronogramaCabeceraCambio.AprobarRechazarCambios(matriculaCabeceraTemp.Id, Convert.ToInt32(cambiosTemp[i]), cronogramaDTO.Version, false, true).Valor;
                    _unitOfWork.Commit();
                    //rpta = 1;
                    ObjetoTemporalSincronizacion.Add(new AprobarRechazarCronogramaDTO { IdMatriculaCabecera = matriculaCabeceraTemp.Id, IdCambio = cambiosTemp[i], Version = cronogramaDTO.Version, Aprobado = false, Cancelado = true });
                    _unitOfWork.Commit();
                }
                var datosMatricula = _repMatriculaCabecera.ObtenerAlumnoProgramaEspecifico(matriculaCabeceraTemp.Id);
                //var datosM = _tcrm_CentroCostoService.GetPEspecificoAlumnoMatriculas(matricula);
                var centrocostoMatricula = _repMatriculaCabecera.ObtenerCentroCostoPorMatricula(matriculaCabeceraTemp.Id);
                //var centrocostoM = _tcrm_CentroCostoService.GetCentroCostoByMatricula(matricula);
                var cambios = "";
                int cont = 1;
                foreach (var item in listaCambios)
                {
                    cambios =               cambios + cont + ". " + item.TipoModificacion + " (" + item.SubTipo + "<br>";
                    cont++;
                }
                var cronogramaCabeceraCambioBO = new CronogramaCabeceraCambioService(_unitOfWork);
                string mensaje = cronogramaCabeceraCambioBO.GenerarMensajeRespuestaCambioCronograma(cronogramaDTO.CodigoMatricula, datosMatricula[0].NombreCompleto, datosMatricula[0].PEspecifico, centrocostoMatricula.NombreCentroCosto, cronogramaDTO.Version, cronogramaDTO.NombreSolicitante, cambios, false, personalAprobador.NombreCompleto);
                HelperCorreo helperCorreo = new HelperCorreo();
                helperCorreo.envio_email("aportillavi@bsginstitute.com", "Integra Cronograma", "Respuesta de Cambio de Cronograma " + cronogramaDTO.CodigoMatricula, mensaje, listaCorreos);
                return (new { Records = rpta, ObjetoTemporalSincronizacion });
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
    }
}
