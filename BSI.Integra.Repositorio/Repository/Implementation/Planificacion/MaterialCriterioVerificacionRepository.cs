using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialCriterioVerificacionRepository
    /// Autor: Gretel Canasa.
    /// Fecha: 25/04/2023
    /// <summary>
    /// Gestión general de T_MaterialCriterioVerificacion
    /// </summary>           
    public class MaterialCriterioVerificacionRepository : GenericRepository<TMaterialCriterioVerificacion>, IMaterialCriterioVerificacionRepository
    {
        private Mapper _mapper;

        public MaterialCriterioVerificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialCriterioVerificacion, MaterialCriterioVerificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialCriterioVerificacion MapeoEntidad(MaterialCriterioVerificacion entidad)
        {
            try
            {
                TMaterialCriterioVerificacion modelo = _mapper.Map<TMaterialCriterioVerificacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialCriterioVerificacion Add(MaterialCriterioVerificacion entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                base.Insert(MaterialAccion);
                return MaterialAccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialCriterioVerificacion Update(MaterialCriterioVerificacion entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialAccion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialAccion);
                return MaterialAccion;
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


        public IEnumerable<TMaterialCriterioVerificacion> Add(IEnumerable<MaterialCriterioVerificacion> listadoEntidad)
        {
            try
            {
                List<TMaterialCriterioVerificacion> listado = new List<TMaterialCriterioVerificacion>();
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

        public IEnumerable<TMaterialCriterioVerificacion> Update(IEnumerable<MaterialCriterioVerificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialCriterioVerificacion> listado = new List<TMaterialCriterioVerificacion>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public IEnumerable<MaterialCriterioVerificacion> ObtenerTodo()
        {
            try
            {
                List<MaterialCriterioVerificacion> rpta = new();
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
                        FROM ope.T_MaterialCriterioVerificacion
                        WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialCriterioVerificacion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public MaterialCriterioVerificacion ObtenerPorId(int id)
        {
            try
            {
                MaterialCriterioVerificacion rpta = new();
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
                        FROM ope.T_MaterialCriterioVerificacion
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MaterialCriterioVerificacion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public List<MaterialCriterioVerificacion> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<MaterialCriterioVerificacion> rpta = new();
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
                        FROM ope.T_MaterialCriterioVerificacion
                        WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MaterialCriterioVerificacion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Alex Quispe Mamani.
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialCriterioVerificacion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
            var query = "SELECT Id, Nombre FROM ope.T_MaterialCriterioVerificacion WHERE Estado = 1";
            var resultado = _dapperRepository.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
            }
            return rpta;
        }
    }
}



