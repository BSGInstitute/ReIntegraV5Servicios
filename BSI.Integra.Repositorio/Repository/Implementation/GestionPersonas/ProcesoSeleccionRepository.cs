using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: ProcesoSeleccionRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ProcesoSeleccion
    /// </summary>
    public class ProcesoSeleccionRepository : GenericRepository<TProcesoSeleccion>, IProcesoSeleccionRepository
    {
        private Mapper _mapper;

        public ProcesoSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoSeleccion, ProcesoSeleccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProcesoSeleccion MapeoEntidad(ProcesoSeleccion entidad)
        {
            try
            {
                //crea la entidad padre
                TProcesoSeleccion modelo = _mapper.Map<TProcesoSeleccion>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcesoSeleccion Add(ProcesoSeleccion entidad)
        {
            try
            {
                var ProcesoSeleccion = MapeoEntidad(entidad);
                Insert(ProcesoSeleccion);
                return ProcesoSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcesoSeleccion Update(ProcesoSeleccion entidad)
        {
            try
            {
                var ProcesoSeleccion = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcesoSeleccion.RowVersion = entidadExistente.RowVersion;

                Update(ProcesoSeleccion);
                return ProcesoSeleccion;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TProcesoSeleccion> Add(IEnumerable<ProcesoSeleccion> listadoEntidad)
        {
            try
            {
                List<TProcesoSeleccion> listado = new List<TProcesoSeleccion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TProcesoSeleccion> Update(IEnumerable<ProcesoSeleccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcesoSeleccion> listado = new List<TProcesoSeleccion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Repositorio: ProcesoSeleccionRepositorio
        /// Autor: Griselberto Huaman
        /// Fecha: 24/05/2021
        /// <summary>
        /// Obtiene lista de proceso de seleccion junto a convocatorias asociadas
        /// </summary>
        /// <returns> List<ProcesoSeleccionConvocatoriaDTO> </returns>
        public List<ProcesoSeleccionConvocatoriaDTO> ObtenerProcesosSeleccionConvocatoria()
        {
            try
            {
                List<ProcesoSeleccionConvocatoriaDTO> rpta = new List<ProcesoSeleccionConvocatoriaDTO>();
                var query = "SELECT Id, Nombre, Codigo, IdConvocatoriaPersonal, CodigoConvocatoriaPersonal FROM [gp].[V_TProcesoSeleccion_ObtenerProcesoSeleccionConvocatorias] WHERE Estado = 1 AND Activo = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionConvocatoriaDTO>>(res);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ProcesoSeleccionRepositorio
        /// Autor: Eliot Arias
        /// Fecha: 21/10/2024
        /// <summary>
        /// Obtiene lista de procesos de seleccion
        /// </summary>
        /// <returns> List<ProcesoSeleccionConvocatoriaDTO> </returns>
        public List<ProcesoSeleccionDTO> ObtenerProcesoSeleccionTotal()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdPuestoTrabajo, PuestoTrabajo, Codigo, Url, Activo, IdSede, Sede FROM [gp].[V_TProcesoSelecciom_ObtenerProcesoSeleccion] WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var res = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ProcesoSeleccionDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        /// Obtiene el codigo nombre de proceso de seleccion para combo
        /// </summary>
        /// <returns> Lista de ComboDTO </returns>
        public IEnumerable<ComboDTO> ObtenerCodigoNombre()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, CONCAT(Codigo, ' - ', Nombre) AS Nombre FROM gp.V_ObtenerProcesoSeleccion WHERE Activo=1;";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(res)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        /// Obtiene el codigo nombre de proceso de seleccion para combo
        /// </summary>
        /// <returns> Lista de ComboDTO </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerCodigoNombreAsync()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, CONCAT(Codigo, ' - ', Nombre) AS Nombre FROM gp.V_ObtenerProcesoSeleccion WHERE Activo=1;";
                var res = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(res)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 24/10/2024
        /// <summary>
        /// Obtiene lista de procesos de seleccion activos
        /// </summary>
        /// <returns> Lista de ProcesoSeleccionDTO </returns>
        public List<ProcesoSeleccionDTO> ObtenerProcesosSeleccion()
        {
            try
            {
                List<ProcesoSeleccionDTO> rpta = new List<ProcesoSeleccionDTO>();
                var query = "SELECT Id, Nombre, IdPuestoTrabajo, PuestoTrabajo, Codigo, Url, Activo, IdSede, Sede FROM [gp].[V_TProcesoSelecciom_ObtenerProcesoSeleccion] WHERE Estado = 1 AND Activo = 1 ORDER BY FechaCreacion DESC";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionDTO>>(res);
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 24/10/2024
        /// <summary>
        /// Obtiene lista de Estados de los procesos de seleccion
        /// </summary>
        /// <returns> Lista de ProcesoSeleccionDTO </returns>
        public IEnumerable<ProcesoSeleccionEstadoFiltroDTO> ObtenerEstadoProcesoSeleccion()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM GP.V_TEstadoProcesoSeleccion_ObtenerEstadoProcesoSeleccion WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ProcesoSeleccionEstadoFiltroDTO>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ConfigurarProcesoSeleccionDTO> ObtenerConfiguracionProcesoSeleccion()
        {
            try
            {
                List<ConfigurarProcesoSeleccionDTO> rpta = new List<ConfigurarProcesoSeleccionDTO>();
                var query = "SELECT Id,Nombre,IdPuestoTrabajo,PuestoTrabajo,IdSede,Sede,Codigo,Url,Activo,FechaInicioProceso,FechaFinProceso FROM GP.V_ObtenerProcesoSeleccion Order By Id DESC";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfigurarProcesoSeleccionDTO>>(res)!;
                }
                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 06/11/2024
        /// <summary>
        /// Obtiene el estado de un proceso seleccion
        /// </summary>
        /// <returns> ProcesoSeleccionDTO </returns>
        public ProcesoSeleccionEstadoFiltroDTO ObtenerEstadoProcesoSeleccionPorId(int id)
        {
            try
            {
                var query = "SELECT Id, Nombre FROM GP.V_TEstadoProcesoSeleccion_ObtenerEstadoProcesoSeleccion WHERE Id = @Id AND Estado = 1";
                var res = _dapperRepository.FirstOrDefault(query, new { Id = id });
                return JsonConvert.DeserializeObject<ProcesoSeleccionEstadoFiltroDTO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ProcesoSeleccion? ObtenerPorId(int id)
        {
            try
            {
                ProcesoSeleccion rpta = new();
                var query = @"
                            SELECT  Id,
                                    Nombre ,
                                    IdPuestoTrabajo,
                                    Codigo,
                                    Url,
                                    Activo,
                                    IdSede,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    FechaInicioProceso,
                                    FechaFinProceso
                            FROM gp.T_ProcesoSeleccion WHERE Estado=1 AND  Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
        public List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion(FiltroAnalisisProcesoSeleccionDTO filtro)
        {
            try
            {

                var filtros = new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion,
                };

                List<ReporteAnalisisProcesoSeleccionDTO> analisisProcesoSeleccion = new List<ReporteAnalisisProcesoSeleccionDTO>();
                string query = string.Empty;
                query = "gp.SP_ReporteAnalisisProcesoSeleccion";
                var PostulanteDB = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
                {
                    analisisProcesoSeleccion = JsonConvert.DeserializeObject<List<ReporteAnalisisProcesoSeleccionDTO>>(PostulanteDB);
                }
                return analisisProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReporteAnalisisProcesoSeleccionDTO> ObtenerReporteAnalisisProcesoSeleccion_V2(FiltroAnalisisProcesoSeleccionDTO filtro)
        {
            try
            {

                var filtros = new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdProcesoSeleccion = filtro.IdProcesoSeleccion,
                };

                List<ReporteAnalisisProcesoSeleccionDTO> analisisProcesoSeleccion = new List<ReporteAnalisisProcesoSeleccionDTO>();
                string query = string.Empty;
                query = "gp.SP_ReporteAnalisisProcesoSeleccion_V2";
                var PostulanteDB = _dapperRepository.QuerySPDapper(query, filtros);
                if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
                {
                    analisisProcesoSeleccion = JsonConvert.DeserializeObject<List<ReporteAnalisisProcesoSeleccionDTO>>(PostulanteDB);
                }
                return analisisProcesoSeleccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<ProcesoSeleccionComboReporteDTO> ObtenerCombo()
        {
            try
            {
                List<ProcesoSeleccionComboReporteDTO> rpta = new List<ProcesoSeleccionComboReporteDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,IdPuestoTrabajo
                    FROM gp.T_ProcesoSeleccion 
                    WHERE Estado = 1 and Activo = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionComboReporteDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
