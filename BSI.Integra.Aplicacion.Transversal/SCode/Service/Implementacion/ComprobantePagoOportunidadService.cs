using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ComprobantePagoOportunidadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ComprobantePagoOportunidad
    /// </summary>

    public class ComprobantePagoOportunidadService : IComprobantePagoOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ComprobantePagoOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TComprobantePagoOportunidad, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<ComprobantePagoOportunidadBaseObjectDTO, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ComprobantePagoOportunidad Add(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoOportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePagoOportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ComprobantePagoOportunidad Update(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoOportunidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePagoOportunidad>(modelo);
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
                _unitOfWork.ComprobantePagoOportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePagoOportunidad> Add(List<ComprobantePagoOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoOportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePagoOportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePagoOportunidad> Update(List<ComprobantePagoOportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoOportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePagoOportunidad>>(modelo);
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
                _unitOfWork.ComprobantePagoOportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public ComprobantePagoOportunidadBaseObjectDTO MapeoEntidad(ComprobantePagoOportunidad entidad)
        {
            try
            {
                var retorno = _mapper.Map<ComprobantePagoOportunidadBaseObjectDTO>(entidad);
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ComprobantePagoOportunidad
        /// </summary>
        /// <returns> List<ComprobantePagoOportunidadDTO> </returns>
        public IEnumerable<ComprobantePagoOportunidadDTO> ObtenerComprobantePagoOportunidad()
        {
            try
            {
                return _unitOfWork.ComprobantePagoOportunidadRepository.ObtenerComprobantePagoOportunidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ComprobantePagoOportunidad.
        /// </summary>
        /// <returns> List<ComprobantePagoAlumnoDTO> </returns>
        public List<ComprobantePagoAlumnoDTO> ObtenerReporteComprobanteAlumno(filtroReporteComprobanteDTO filtro)
        {
            try
            {
                var repPeriodo = _unitOfWork.PeriodoRepository;
                if (filtro.TipoFechaPago == 0)// no hay fechas ingresadas ni periodos seleccionados
                {
                    filtro.FechaInicial = new DateTime(1999, 1, 1, 0, 0, 0);
                    filtro.FechaFin = DateTime.Now;
                }
                if (filtro.TipoFechaPago == 1)// periodo
                {
                    var inicial = repPeriodo.ObtenerFechaInicialNulo(filtro.IdPeriodo);
                    var fin = repPeriodo.ObtenerFechaFinalNulo(filtro.IdPeriodo);
                    filtro.FechaInicial = new DateTime(inicial.Year, inicial.Month, inicial.Day, 0, 0, 0);
                    filtro.FechaFin = new DateTime(fin.Year, fin.Month, fin.Day, 23, 59, 59);
                }
                if (filtro.TipoFechaPago == 2)// rango de fechas
                {
                    filtro.FechaInicial = new DateTime(filtro.FechaInicial.Year, filtro.FechaInicial.Month, filtro.FechaInicial.Day, 0, 0, 0);
                    filtro.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);

                }
                return _unitOfWork.ComprobantePagoOportunidadRepository.ObtenerReporteComprobanteAlumno(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cadena a enviar en correo relacionado a Comprobante Pago
        /// </summary>
        /// <param name="comprobante">Entidad con datos de Comprobante</param>
        /// <returns> string </returns>
        public string MensajeEmailComprobantePago(ComprobantePagoOportunidad comprobantePagoOportunidad)
        {
            string mensaje = string.Empty;

            comprobantePagoOportunidad.Dni = comprobantePagoOportunidad.Dni ?? "";
            comprobantePagoOportunidad.Direccion = comprobantePagoOportunidad.Direccion ?? "";
            comprobantePagoOportunidad.NroDocumento = comprobantePagoOportunidad.NroDocumento ?? "";
            comprobantePagoOportunidad.NombreRazonSocial = comprobantePagoOportunidad.NombreRazonSocial ?? "";
            comprobantePagoOportunidad.Comentario = comprobantePagoOportunidad.Comentario ?? "";
            comprobantePagoOportunidad.Apellidos = comprobantePagoOportunidad.Apellidos ?? "No se ingreso apellido";

            mensaje += "<p><b>Datos del Alumno</b></p>";
            mensaje += "<ul>";
            mensaje += $"<li><b>Nombres:</b> {comprobantePagoOportunidad.Nombres}</li>";
            mensaje += $"<li><b>Apellidos:</b> {comprobantePagoOportunidad.Apellidos}</li>";
            mensaje += $"<li><b>Tipo comprobante:</b> {comprobantePagoOportunidad.TipoComprobante}</li>";

            if (comprobantePagoOportunidad.BitComprobante != 0)
            {
                if (comprobantePagoOportunidad.IdPais == 51)
                    mensaje += $"<li><b>RUC:</b> {comprobantePagoOportunidad.NroDocumento}</li>";
                else
                    mensaje += $"<li><b>RUT:</b> {comprobantePagoOportunidad.NroDocumento}</li>";

                mensaje += $"<li><b>Razón Social:</b> {comprobantePagoOportunidad.NombreRazonSocial}</li>";
            }

            mensaje += $"<li><b>País:</b> {comprobantePagoOportunidad.NombrePais}</li>";
            mensaje += $"<li><b>Ciudad:</b> {comprobantePagoOportunidad.NombreCiudad}</li>";
            mensaje += $"<li><b>Correo:</b> {comprobantePagoOportunidad.Correo}</li>";
            mensaje += $"<li><b>Direccion:</b> {comprobantePagoOportunidad.Direccion}</li>";
            mensaje += $"<li><b>Documento:</b> {comprobantePagoOportunidad.Dni}</li>";
            mensaje += $"<li><b>Celular:</b> {comprobantePagoOportunidad.Celular}</li>";
            mensaje += $"<li><b>Comentario de Asesor:</b> {comprobantePagoOportunidad.Comentario}</li>";
            mensaje += $"</ul>";
            return mensaje;
        }
    }
}
