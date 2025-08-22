using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Servicio: ReporteConsultasForoAulaVirtualService
    /// Autor: Edmundo Llaza
    /// Fecha: 2023-07-31
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de foro aula virtual
    /// </summary>
    public class ReporteConsultasForoAulaVirtualService: IReporteConsultasForoAulaVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteConsultasForoAulaVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-31
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de atención de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool ActualizarEstadoAtencionForo(int idForo, bool estadoAtendido, string usuarioModificacion)
        {
            try
            {
                var actualizados = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.ActualizarEstadoAtencionForo(idForo, estadoAtendido, usuarioModificacion);
                return actualizados;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado para eliminación de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool EliminarForo(int idForo, string usuarioModificacion)
        {
            try
            {
                var eliminados = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.EliminarForo(idForo, usuarioModificacion);
                return eliminados;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de abierto o cerrado de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool ActualizarAperturaForo(int idForo, bool estadoForo, string usuarioModificacion)
        {
            try
            {
                var apertura = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.ActualizarAperturaForo(idForo, estadoForo, usuarioModificacion);
                return apertura;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Genera combo programa, docente, curso
        /// </summary>
        /// <returns></returns>
        public ComboReporteForoDTO ObtenerCombosModulo()
        {
            var comboReporteForoDTO = new ComboReporteForoDTO();
            comboReporteForoDTO.Programa = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro()
                .Where(p => !p.Nombre.Contains("Webinar")).OrderByDescending(x=>x.Id);
            comboReporteForoDTO.Docente = _unitOfWork.ProveedorRepository.ObtenerNombreProveedorParaHonorario();
            comboReporteForoDTO.Curso = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro()
                .Where(p => !p.Nombre.Contains("Webinar")).OrderByDescending(x => x.Id);
            return comboReporteForoDTO;
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-08
        /// Versión: 1.0
        /// <summary>
        /// Generar el reporte de consultas foro
        /// </summary>
        /// <param name="filtroReporte">Filtro para el reporte de de consultas foro  </param>
        /// <returns>El reporte retorna una Lista List<ReporteConsultasForoAulaVirtualDTO></returns>
        public List<ReporteConsultasForoAulaVirtualDTO> GenerarReporteConsultasForoAulaVirtual(ReporteConsultasForoFiltroDTO filtroReporte)
        {
            try
            {
                var reporte = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.GenerarReporteConsultasForoAulaVirtual(filtroReporte);
                return reporte;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el encargado de revisión del foro 
        /// </summary>
        /// <param name="Datos">Datos para acutalizar </param>
        /// <returns></returns>
        public PersonalAsignadoDocenteDTO ActualizarPersonaRevisionForo(int idForo, int idProveedor)
        {
            try
            {
                var lista = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.ActualizarEncargadoForo(idForo, idProveedor);
                var personalAsignado = _unitOfWork.ProveedorRepository.PersonalAsignadoDocente(idProveedor);
                return personalAsignado;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de consultas del deralle del Foro de AulaVirtual.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteConsultasForoDetalleAulaVirtualDTO> </returns>
        public List<ReporteConsultasForoDetalleAulaVirtualDTO> ObtenerDetalleForo(int idForoCurso)
        {
            try
            {
                var detalleForo = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.ObtenerDetalleForo(idForoCurso);
                return detalleForo;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza 
        /// Fecha: 2023-08-08
        /// <summary>
        /// Llama a SP para cambiar estado de foro
        /// </summary>
        /// <param name="idForoRespuesta"></param>
        /// <param name="usuarioModificacion"></param>
        /// <returns>bool</returns>
        public bool EliminarForoRespuesta(int idForoRespuesta, string usuarioModificacion)
        {
            try
            {
                var eliminacion = _unitOfWork.ReporteConsultasForoAulaVirtualRepository.EliminarForoRespuesta(idForoRespuesta, usuarioModificacion);
                return eliminacion;
            }
            catch { throw; }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-09
        /// <summary>
        /// Genera el formato para enviar correo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns>bool</returns>

        public bool EnvioCorreoAsignacionForoDocente(ForosCorreoDTO datos, int perId)
        {
            var nombreProveedor = _unitOfWork.ProveedorRepository.ObtenerNombreProveedorPorId(datos.IdProveedor);
            var correoProveedor = _unitOfWork.ProveedorRepository.ObtenerEmail(datos.IdProveedor);
            var correoCoordinadora = _unitOfWork.IntegraAspNetUserRepository.ObtenerPorIdPersonal(perId).Email;
            ForoCorreoDetalleDTO correo = new ForoCorreoDetalleDTO();
            correo.NombreProveedor = nombreProveedor;
            correo.NombreCurso = datos.Curso;
            correo.Asunto = datos.Titulo;
            correo.Consulta = datos.Contenido;
            correo.FechaConsulta = datos.FechaCreacion.ToString("dd MMMM yyyy");
            var fechaLimite = DateTime.Now.AddDays(7);
            correo.FechaLimite = fechaLimite.ToString("dd MMMM yyyy");
            correo.Email = correoCoordinadora;
            
            var idPlantilla = 1511;
            var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
            var valores = _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoAsignacionForoDocente(correo, idPlantilla);
            var emailCalculado = valores.EmailReemplazado;

            List<string> correosPersonalizadosCopiaOculta = new List<string>
            {
                "mmantilla@bsginstitute.com",
                "gcanasac@bsginstitute.com"
            };

            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
            {
                Sender = correoCoordinadora,
                //Sender = personal.Email,
                Recipient = correoProveedor,
                Subject = emailCalculado.Asunto,
                Message = emailCalculado.CuerpoHTML,
                Cc = "",
                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct())
            };
            var mailServie = new TMK_MailService();

            mailServie.SetData(mailDataPersonalizado);
            mailServie.SendMessageTask();

            return true;
        }
    }
}
