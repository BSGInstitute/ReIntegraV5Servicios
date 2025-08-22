using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ModalidadCursoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 05/08/2022
    /// <summary>
    /// Gestión general de T_ModalidadCurso
    /// </summary>
    public class ModalidadCursoRepository : GenericRepository<TModalidadCurso>, IModalidadCursoRepository
    {
        private Mapper _mapper;

        public ModalidadCursoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModalidadCurso, ModalidadCurso>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCurso>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralCodigoPartnerModalidadCurso, PgeneralCodigoPartnerModalidadCurso>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralConfiguracionPlantillaDetalle, PGeneralConfiguracionPlantillaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralModalidad, PgeneralModalidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModalidadCurso MapeoEntidad(ModalidadCurso entidad)
        {
            try
            {
                TModalidadCurso modelo = _mapper.Map<TModalidadCurso>(entidad);
                if (entidad.CriterioEvaluacionModalidadCursos != null && entidad.CriterioEvaluacionModalidadCursos.Count() > 0)
                {
                    modelo.TCriterioEvaluacionModalidadCursos = _mapper.Map<ICollection<TCriterioEvaluacionModalidadCurso>>(entidad.CriterioEvaluacionModalidadCursos);
                }
                if (entidad.EsquemaEvaluacionPgeneralModalidads != null && entidad.EsquemaEvaluacionPgeneralModalidads.Count() > 0)
                {
                    modelo.TEsquemaEvaluacionPgeneralModalidads = _mapper.Map<ICollection<TEsquemaEvaluacionPgeneralModalidad>>(entidad.EsquemaEvaluacionPgeneralModalidads);
                }
                if (entidad.PgeneralCodigoPartnerModalidadCursos != null && entidad.PgeneralCodigoPartnerModalidadCursos.Count() > 0)
                {
                    modelo.TPgeneralCodigoPartnerModalidadCursos = _mapper.Map<ICollection<TPgeneralCodigoPartnerModalidadCurso>>(entidad.PgeneralCodigoPartnerModalidadCursos);
                }
                if (entidad.PgeneralConfiguracionPlantillaDetalles != null && entidad.PgeneralConfiguracionPlantillaDetalles.Count() > 0)
                {
                    modelo.TPgeneralConfiguracionPlantillaDetalles = _mapper.Map<ICollection<TPgeneralConfiguracionPlantillaDetalle>>(entidad.PgeneralConfiguracionPlantillaDetalles);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModalidadCurso Add(ModalidadCurso entidad)
        {
            try
            {
                var ModalidadCurso = MapeoEntidad(entidad);
                base.Insert(ModalidadCurso);
                return ModalidadCurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModalidadCurso Update(ModalidadCurso entidad)
        {
            try
            {
                var ModalidadCurso = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ModalidadCurso.RowVersion = entidadExistente.RowVersion;

                base.Update(ModalidadCurso);
                return ModalidadCurso;
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
        public IEnumerable<TModalidadCurso> Add(IEnumerable<ModalidadCurso> listadoEntidad)
        {
            try
            {
                List<TModalidadCurso> listado = new List<TModalidadCurso>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TModalidadCurso> Update(IEnumerable<ModalidadCurso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModalidadCurso> listado = new List<TModalidadCurso>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ModalidadCurso por Id
        /// </summary>
        /// <returns> ModalidadCurso </returns>
        public ModalidadCurso? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        Estado,
                        Codigo,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                        FROM pla.T_ModalidadCurso
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModalidadCurso>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MCR-OPI001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ModalidadCurso para mostrarse en combo.
        /// </summary>
        /// <returns> Lista de modalidades combo </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.V_TModalidadCurso_Filtro WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MCR-OC001@Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ModalidadCurso para mostrarse en combo.
        /// </summary>
        /// <returns> Lista de modalidades combo </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.V_TModalidadCurso_Filtro WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MCR-OCA001@Error en ObtenerComboAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ModalidadCurso por Id
        /// </summary>
        /// <returns> ModalidadCurso </returns>
        public IEnumerable<ModalidadCurso>? ObtenerPorIds(List<int> ids)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        Estado,
                        Codigo,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                        FROM pla.T_ModalidadCurso
                    WHERE Estado = 1 AND Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModalidadCurso>>(resultado)!;
                }
                return new List<ModalidadCurso>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MCR-OPI001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ModalidadCurso por Id
        /// </summary>
        /// <returns> ModalidadCurso </returns>
        public IEnumerable<ModalidadCursoDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        Codigo
                        FROM pla.T_ModalidadCurso
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModalidadCursoDTO>>(resultado)!;
                }
                return new List<ModalidadCursoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MCR-OPI001@Error en Obtener: {ex.Message}", ex);
            }
        }
    }
}
