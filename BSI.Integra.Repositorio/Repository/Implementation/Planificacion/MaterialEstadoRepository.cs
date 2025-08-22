using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialEstadoRepository
    /// Autor:Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_MaterialEstado
    /// </summary>
    public class MaterialEstadoRepository : GenericRepository<TMaterialEstado>, IMaterialEstadoRepository
    {
        private Mapper _mapper;

        public MaterialEstadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialEstado, MaterialEstado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialEstado MapeoEntidad(MaterialEstado entidad)
        {
            try
            {
                TMaterialEstado modelo = _mapper.Map<TMaterialEstado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialEstado Add(MaterialEstado entidad)
        {
            try
            {
                var MaterialEstado = MapeoEntidad(entidad);
                base.Insert(MaterialEstado);
                return MaterialEstado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialEstado Update(MaterialEstado entidad)
        {
            try
            {
                var MaterialEstado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialEstado.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialEstado);
                return MaterialEstado;
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


        public IEnumerable<TMaterialEstado> Add(IEnumerable<MaterialEstado> listadoEntidad)
        {
            try
            {
                List<TMaterialEstado> listado = new List<TMaterialEstado>();
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

        public IEnumerable<TMaterialEstado> Update(IEnumerable<MaterialEstado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialEstado> listado = new List<TMaterialEstado>();
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
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialEstado.
        /// </summary>
        /// <returns> List<MaterialEstadoDTO> </returns>
        public IEnumerable<MaterialEstado> ObtenerTodo()
        {
            try
            {
                List<MaterialEstado> rpta = new();
                var query = @"
                        SELECT 
                            Id,
		                    Nombre,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM ope.T_MaterialEstado
                        WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialEstado>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialEstado.
        /// </summary>
        /// <returns> List<MaterialEstadoDTO> </returns>
        public MaterialEstado ObtenerPorId(int id)
        {
            try
            {
                MaterialEstado rpta = new();
                var query = @"
                        SELECT 
                            Id,
		                    Nombre,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM ope.T_MaterialEstado
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MaterialEstado>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialEstado.
        /// </summary>
        /// <returns> List<MaterialEstadoDTO> </returns>
        public List<MaterialEstado> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<MaterialEstado> rpta = new();
                var query = @"
                        SELECT 
                            Id,
		                    Nombre,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM ope.T_MaterialEstado
                        WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialEstado>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialEstado para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM ope.T_MaterialEstado WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MER-OC-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
    }
}



