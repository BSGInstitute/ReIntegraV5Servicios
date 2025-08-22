using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProcesoSeleccionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ProcesoSeleccion
    /// </summary>
    public class ProcesoSeleccionService : IProcesoSeleccionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProcesoSeleccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoSeleccion, ProcesoSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProcesoSeleccion
        /// </summary>
        /// <returns> List<ProcesoSeleccionDTO> </returns>
        public List<ProcesoSeleccionCompuestoDTO> ObtenerProcesoSeleccionConvocatoria()
        {
            try
            {
                var listaProceso = _unitOfWork.ProcesoSeleccionRepository.ObtenerProcesosSeleccionConvocatoria();
                var agrupado = listaProceso.GroupBy(x => new { x.Id, x.Nombre, x.Codigo }).Select(x => new ProcesoSeleccionCompuestoDTO
                {
                    Id = x.Key.Id,
                    Nombre = x.Key.Nombre,
                    Codigo = x.Key.Codigo,
                    DetalleConvocatoria = x.GroupBy(y => new { y.IdConvocatoriaPersonal, y.CodigoConvocatoriaPersonal }).Select(y => new ConvocatoriaPersonalDetalleDTO
                    {
                        IdConvocatoriaPersonal = y.Key.IdConvocatoriaPersonal,
                        CodigoConvocatoriaPersonal = y.Key.CodigoConvocatoriaPersonal,
                        UltimaSecuencia = y.Key.CodigoConvocatoriaPersonal != null ? (Convert.ToInt32(Regex.Match(y.Key.CodigoConvocatoriaPersonal, @"-\d+").Value) * -1) : null
                    }).OrderByDescending(y => y.UltimaSecuencia).ToList()
                }).ToList();
                return agrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Eliot Arias F.
        /// Fecha: 24/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de procesos de seleccion activos
        /// </summary>
        /// <returns> List<ProcesoSeleccionDTO> </returns>
        public List<ProcesoSeleccionDTO> ObtenerProcesosSeleccion()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionRepository.ObtenerProcesosSeleccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 24/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de procesos de seleccion totales activos y no activos
        /// </summary>
        /// <returns> List<ProcesoSeleccionDTO> </returns>
        public List<ProcesoSeleccionDTO> ObtenerProcesoSeleccionTotal()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionRepository.ObtenerProcesoSeleccionTotal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Eliot Arias F.
        /// Fecha: 24/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de Estados de los procesos de seleccion
        /// </summary>
        /// <returns> IEnumerable<FiltroDTO> </returns>
        public IEnumerable<ProcesoSeleccionEstadoFiltroDTO> ObtenerEstadoProcesoSeleccion()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionRepository.ObtenerEstadoProcesoSeleccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProcesoSeleccionComboReporteDTO> ObtenerCombo()
        {
            return _unitOfWork.ProcesoSeleccionRepository.ObtenerCombo();
        }

    }

}
