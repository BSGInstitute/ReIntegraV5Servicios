using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// Service: ReporteTasaConversionConsolidadaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class ReporteTasaConversionConsolidadaService : IReporteTasaConversionConsolidadaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AgendaDTO agendaBo = new AgendaDTO();
        public ReporteTasaConversionConsolidadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, Entity>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }

        public TCCReporteDTO GenerarReporte(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            try
            {
                IReporteService reporteService = new ReporteService(_unitOfWork);
                var consolidadoAsesores = ReporteTasaConversionConsolidadoAsesores(filtros);
                var centroCostoPorAsesor = ObtenerCentroCostoPorAsesor(filtros);

                List<ConsolidadoDTO> consolidado = (
                    from p in consolidadoAsesores.Consolidado
                    group p by p.probabilidadDesc
                    into grupo
                    select new ConsolidadoDTO { Grupo = grupo.Key, Data = grupo }).ToList();

                List<DesagregadoDTO> desagregado = (
                    from p in consolidadoAsesores.Desagregado
                    group p by p.ProbabilidadDesc into grupo
                    select new DesagregadoDTO { Grupo = grupo.Key, Data = grupo }).ToList();

                List<TCRM_CentroCostoPorAsesorAgrupadoDTO> centroCostoAsesorAgrupados = (
                    from p in centroCostoPorAsesor
                    group p by new { p.idasesor } into grupo
                    select new TCRM_CentroCostoPorAsesorAgrupadoDTO
                    {
                        IdAsesor = grupo.Key.idasesor,
                        IngresoReal = grupo.Sum(w => w.ingresoReal),
                        IngresoMes = grupo.Sum(w => w.ingresoMes),
                        DescuentoPromedio = grupo.Sum(w => w.oportunidadesOCAnyIS) == 0 ? 0 : grupo.Sum(w => w.Descuento) / grupo.Sum(w => w.oportunidadesOCAnyIS),
                        PrecioPromedio = grupo.Sum(w => w.precioListaFinal) / grupo.Sum(w => w.oportunidadesOCTotal),
                        OportunidadesOCAnyIS = grupo.Sum(w => w.oportunidadesOCAnyIS),
                        OportunidadesOCTotal = grupo.Sum(w => w.oportunidadesOCTotal),
                        EstadoAsesor = grupo.Max(w => w.estadoAsesor)
                    }).ToList();

                var resultado = new TCCReporteDTO
                {
                    Consolidados = consolidado,
                    Desagregados = desagregado,
                    CentroCostoAsesorAgrupados = centroCostoAsesorAgrupados,
                    CentroCostoAsesor = centroCostoPorAsesor
                };
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de la Tasa Conversión de cada Asesor
        /// </summary>
        /// <param filtros></param>
        /// <returns>ReporteTasaConversionConsolidadaAsesoresDTO</returns>
        /// <exception cref="Exception"></exception>
        public ReporteTasaConversionConsolidadaAsesoresDTO ReporteTasaConversionConsolidadoAsesores(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            try
            {
                var param = CargarCentroCostoPorAsesorDTO(filtros);
                var rpta = new ReporteTasaConversionConsolidadaAsesoresDTO();
                rpta.Consolidado = _unitOfWork.ReportesRepository.ReporteTasaConversionConsolidadoAsesores(param);
                rpta.Desagregado = new List<TCRM_TasaConversionPorCategoriaDatoPaisDTO>();
                var result = (from p in rpta.Consolidado
                              group p by new
                              {
                                  p.probabilidadDesc,
                                  p.ordenp,
                                  p.grupo,
                                  p.nombreGrupo,
                                  p.tcMeta
                              } into g
                              select new TCRM_TasaConversionPorCategoriaDatoPaisDTO
                              {
                                  ProbabilidadDesc = g.Key.probabilidadDesc,
                                  Orden = g.Key.ordenp,
                                  Grupo = g.Key.grupo.ToString(),
                                  TCMeta = g.Key.tcMeta,
                                  NombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo,
                                  Pais = Guid.Empty.ToString(),
                                  ListaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                                  {
                                      orden = o.orden,
                                      probabilidadDesc = o.probabilidadDesc,
                                      pais = o.pais,
                                      idCoordinador = o.idCoordinador,
                                      nombreCoordinador = o.nombreCoordinador,
                                      idasesor = o.idasesor,
                                      nombre = o.nombre,
                                      idcategoriaOrigen = o.idcategoriaOrigen,
                                      ca_nombre = o.ca_nombre,
                                      inscritosMatriculados = o.inscritosMatriculados,
                                      oportunidadesCerradas = o.oportunidadesCerradas,
                                      idSub = o.idSub,
                                      nombreSub = o.nombreSub,
                                      tcMeta = o.tcMeta
                                  }).ToList()
                              });
                var result2 = (from p in rpta.Consolidado
                               group p by new
                               {
                                   p.probabilidadDesc,
                                   p.ordenp,
                                   p.grupo,
                                   p.nombreGrupo,
                                   p.pais,
                                   p.nombrePais,
                                   p.tcMeta,
                               } into g
                               select new TCRM_TasaConversionPorCategoriaDatoPaisDTO
                               {
                                   ProbabilidadDesc = g.Key.probabilidadDesc,
                                   Orden = g.Key.ordenp,
                                   Grupo = g.Key.grupo.ToString(),
                                   TCMeta = g.Key.tcMeta,
                                   NombreGrupo = g.Key.probabilidadDesc + " " + g.Key.nombreGrupo + " " + g.Key.nombrePais,
                                   Pais = g.Key.pais.ToString(),
                                   NombrePais = g.Key.nombrePais,
                                   ListaMuyAlta = g.Select(o => new TCRM_ConsolidadTCAsesores
                                   {
                                       orden = o.orden,
                                       probabilidad = o.probabilidadDesc,
                                       pais = o.pais,
                                       idasesor = o.idasesor,
                                       nombre = o.nombre,
                                       idCoordinador = o.idCoordinador,
                                       nombreCoordinador = o.nombreCoordinador,
                                       idcategoriaOrigen = o.idcategoriaOrigen,
                                       ca_nombre = o.ca_nombre,
                                       inscritosMatriculados = o.inscritosMatriculados,
                                       oportunidadesCerradas = o.oportunidadesCerradas,
                                       idSub = o.idSub,
                                       nombreSub = o.nombreSub,
                                       tcMeta = o.tcMeta
                                   }).ToList()
                               });

                rpta.Desagregado = result2.ToList();
                rpta.Desagregado.AddRange(result.ToList());
                rpta.Desagregado = rpta.Desagregado.OrderBy(x => x.Orden).ThenByDescending(x => x.TCMeta).ThenBy(w => w.Grupo).ThenBy(w => w.NombreGrupo).ToList();
                return rpta;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros de Centro Costo de cada Asesor
        /// </summary>
        /// <param filtros></param>
        /// <returns>List<TCRM_CentroCostoPorAsesorDTO></returns>
        /// <exception cref="Exception"></exception>
        public List<TCRM_CentroCostoPorAsesorDTO> ObtenerCentroCostoPorAsesor(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            try
            {
                var param = CargarCentroCostoPorAsesorDTO(filtros);
                return _unitOfWork.ReportesRepository.ObtenerCentroCostoPorAsesor(param);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        private static CentroCostoPorAsesorDTO CargarCentroCostoPorAsesorDTO(ReporteTasaConversionConsolidadaFiltroDTO filtros)
        {
            var param = new CentroCostoPorAsesorDTO
            {
                Area = string.Join(",", filtros.Areas ?? new List<int>()),
                SubArea = string.Join(",", filtros.SubAreas ?? new List<int>()),
                PGeneral = string.Join(",", filtros.PGeneral ?? new List<int>()),
                PEspecifico = string.Join(",", filtros.PEspecifico ?? new List<int>()),
                Modalidades = string.Join(",", filtros.Modalidades ?? new List<string>()),
                Ciudades = string.Join(",", filtros.Ciudades ?? new List<string>()),
                Coordinadores = string.Join(",", filtros.Coordinadores ?? new List<int>()),
                Asesores = string.Join(",", filtros.Asesores ?? new List<int>()),
                Fecha = filtros.Fecha,
                FechaInicio = Convert.ToDateTime(filtros.FechaInicio).Date,
                FechaFin = Convert.ToDateTime(filtros.FechaFin).Date.AddHours(23).AddMinutes(59).AddSeconds(59)

            };

            if (string.IsNullOrEmpty(param.Area)) param.Area = "_";
            if (string.IsNullOrEmpty(param.SubArea)) param.SubArea = "_";
            if (string.IsNullOrEmpty(param.PGeneral)) param.PGeneral = "_";
            if (string.IsNullOrEmpty(param.PEspecifico)) param.PEspecifico = "_";
            if (string.IsNullOrEmpty(param.Modalidades)) param.Modalidades = "_";
            if (string.IsNullOrEmpty(param.Ciudades)) param.Ciudades = "_";
            if (string.IsNullOrEmpty(param.Coordinadores)) param.Coordinadores = "_";
            if (string.IsNullOrEmpty(param.Asesores)) param.Asesores = "_";

            return param;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de Tasa Conversión Consolidada mediante el Id Personal
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO - ReporteTasaConversionConsolidadasComboDTO - combos </returns>
        public ReporteTasaConversionConsolidadasComboDTO ObtenerCombos(int idPersonal)
        {
            try
            {
                var combos = new ReporteTasaConversionConsolidadasComboDTO();
                combos.Coordinadores = _unitOfWork.PersonalRepository.ObtenerCoordinadoresVentasOficialTCC(idPersonal).Where(w=>w.TipoPersonal=="Coordinador").ToList();
                combos.Asesores = _unitOfWork.PersonalRepository.ObtenerCoordinadoresVentasOficialTCC(idPersonal).Where(w => w.TipoPersonal == "Asesor" || w.TipoPersonal == "otro").ToList();
                combos.AreasCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerCombo().ToList();
                combos.SubAreasCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().ToList();
                combos.ProgramasGenerales = _unitOfWork.PGeneralRepository.ObtenerCombo().ToList();
                combos.ProgramasEspecificos = _unitOfWork.PEspecificoRepository.ObtenerCombo();
                return combos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
