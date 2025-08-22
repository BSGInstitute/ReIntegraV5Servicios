using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Data;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AreaTrabajoRepository
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_AreaTrabajo
    /// </summary>
    public class AreaTrabajoRepository : GenericRepository<TAreaTrabajo>, IAreaTrabajoRepository
    {
        private Mapper _mapper;

        public AreaTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAreaTrabajo, AreaTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAreaTrabajo MapeoEntidad(AreaTrabajo entidad)
        {
            try
            {
                return _mapper.Map<TAreaTrabajo>(entidad); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAreaTrabajo Add(AreaTrabajo entidad)
        {
            try
            {
                var AreaTrabajo = MapeoEntidad(entidad);
                base.Insert(AreaTrabajo);
                return AreaTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TAreaTrabajo Update(AreaTrabajo entidad)
        {
            try
            {
                var AreaTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaTrabajo);
                return AreaTrabajo;
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
        public IEnumerable<TAreaTrabajo> Add(IEnumerable<AreaTrabajo> listadoEntidad)
        {
            try
            {
                List<TAreaTrabajo> listado = new List<TAreaTrabajo>();
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
        public IEnumerable<TAreaTrabajo> Update(IEnumerable<AreaTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAreaTrabajo> listado = new List<TAreaTrabajo>();
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
        /// Autor Modificacion: Flavio R. Mamani Fabian
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaTrabajo para mostrarse en combo.
        /// </summary>
        /// <returns> Lista de Area Trabajo para combo</returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_AreaTrabajo
                            WHERE Estado = 1
                            ORDER BY FechaCreacion DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo: {ex.Message}", ex);
            }
        }
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_AreaTrabajo
                            WHERE Estado = 1
                            ORDER BY FechaCreacion DESC;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerComboAsync: {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public AreaTrabajo ObtenerPorId(int id)
        {
            try
            {
                AreaTrabajo rpta = new();
                var query = @"
                        SELECT Estado,
                            FechaCreacion,
                            FechaModificacion,
                            Id,
                            IdMigracion,
                            Nombre,
                            RowVersion,
                            UsuarioCreacion,
                            UsuarioModificacion FROM pla.T_AreaTrabajo 
                        WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<AreaTrabajo>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor Modificacion: Griselberto Huaman
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de Areas en Agenda.
        /// </summary>
        /// <returns> List<PersonalAreaTrabajoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAreaAgenda()
        {
            try
            {
                List<ComboDTO> areas = new List<ComboDTO>();
                var query = string.Empty;
                query = "SELECT * FROM gp.V_ObtenerAreaAgenda ";
                var queryResultado = _dapperRepository.QueryDapper(query, null);
                if (!queryResultado.Contains("[]") && !string.IsNullOrEmpty(queryResultado))
                {
                    areas = JsonConvert.DeserializeObject<List<ComboDTO>>(queryResultado);
                }
                return areas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ComboDTO> ObtenerTodoAreaTrabajoFiltro()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                   SELECT Id,Nombre FROM pla.V_TAreaTrabajo_Filtro Where Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

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
