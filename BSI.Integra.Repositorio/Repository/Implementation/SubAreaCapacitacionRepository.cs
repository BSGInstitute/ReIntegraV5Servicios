using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SubAreaCapacitacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_SubAreaCapacitacion
    /// </summary>
    public class SubAreaCapacitacionRepository : GenericRepository<TSubAreaCapacitacion>, ISubAreaCapacitacionRepository
    {
        private Mapper _mapper;

        public SubAreaCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubAreaCapacitacion, SubAreaCapacitacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSubAreaParametroSeoPw, SubAreaParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSubAreaCapacitacion MapeoEntidad(SubAreaCapacitacion entidad)
        {
            try
            {
                //crea la entidad padre
                TSubAreaCapacitacion modelo = _mapper.Map<TSubAreaCapacitacion>(entidad);

                //mapea los hijos
                if (entidad.SubAreaParametroSeoPws != null && entidad.SubAreaParametroSeoPws.Count > 0)
                {
                    modelo.TSubAreaParametroSeoPws = _mapper.Map<List<TSubAreaParametroSeoPw>>(entidad.SubAreaParametroSeoPws);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubAreaCapacitacion Add(SubAreaCapacitacion entidad)
        {
            try
            {
                var SubAreaCapacitacion = MapeoEntidad(entidad);
                base.Insert(SubAreaCapacitacion);
                return SubAreaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubAreaCapacitacion Update(SubAreaCapacitacion entidad)
        {
            try
            {
                var SubAreaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SubAreaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(SubAreaCapacitacion);
                return SubAreaCapacitacion;
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


        public IEnumerable<TSubAreaCapacitacion> Add(IEnumerable<SubAreaCapacitacion> listadoEntidad)
        {
            try
            {
                List<TSubAreaCapacitacion> listado = new List<TSubAreaCapacitacion>();
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

        public IEnumerable<TSubAreaCapacitacion> Update(IEnumerable<SubAreaCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSubAreaCapacitacion> listado = new List<TSubAreaCapacitacion>();
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SubAreaCapacitacion.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionDTO> </returns>
        public IEnumerable<SubAreaCapacitacion> Obtener()
        {
            try
            {
                List<SubAreaCapacitacion> rpta = new List<SubAreaCapacitacion>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    IdAreaCapacitacion,
	                    EsVisibleWeb,
	                    IdSubArea,
	                    DescripcionHTML,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    AliasFacebook
                    FROM pla.T_SubAreaCapacitacion
                    WHERE Estado = 1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SubAreaCapacitacion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubAreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public IEnumerable<SubAreaCapacitacionFiltroDTO> ObtenerFiltro()
        {
            try
            {
                var query = @"SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1 ORDER BY id asc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<SubAreaCapacitacionFiltroDTO>>(resultado)!;
                return new List<SubAreaCapacitacionFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltro(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubAreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public async Task<IEnumerable<SubAreaCapacitacionFiltroDTO>> ObtenerFiltroAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<SubAreaCapacitacionFiltroDTO>>(resultado)!;
                return new List<SubAreaCapacitacionFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAsync(): {ex.Message}");
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubAreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public IEnumerable<SubAreaCapacitacionAlternoDTO> ObtenerAlterno()
        {
            try
            {
                List<SubAreaCapacitacionAlternoDTO> rpta = new List<SubAreaCapacitacionAlternoDTO>();
                var query = @"SELECT Id,
                                       Nombre,
                                       Descripcion,
                                       IdAreaCapacitacion,
                                       EsVisibleWeb,
                                       IdSubArea,
                                       Estado,
                                       DescripcionHTML,
                                       AliasFacebook,
                                       NombreAreaCapacitacion
                                FROM [pla].[V_RegistrosFiltroSubAreaCapacitacion]
                                Where Estado=1
                                ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<SubAreaCapacitacionAlternoDTO>>(resultado);

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene SubArea Capacitacion Anterior segun el Id Area Capacitacion Actual
        /// </summary>
        /// <returns></returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerPorIdAreaCapacitacion(int idAreaCapacitacion)
        {
            try
            {
                List<SubAreaCapacitacionFiltroDTO> subAreasCapacitacionFiltro = new List<SubAreaCapacitacionFiltroDTO>();
                var query = "SELECT Id, Nombre, IdAreaCapacitacion FROM pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1 AND IdAreaCapacitacion = @idAreaCapacitacion";
                var resultado = _dapperRepository.QueryDapper(query, new { idAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    subAreasCapacitacionFiltro = JsonConvert.DeserializeObject<List<SubAreaCapacitacionFiltroDTO>>(resultado);

                return subAreasCapacitacionFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene SubArea Capacitacion Anterior segun el Id Area Capacitacion Actual
        /// </summary>
        /// <returns></returns>
        public int ObtenerSubAreaCapacitacionAnterior(int idActualSubArea)
        {
            try
            {
                int subArea = 0;
                var query = "SELECT IdActualSubArea,IdAnteriorSubArea as Valor FROM pla.V_ObtenerSubAreaCapacitacionAnterior WHERE IdActualSubArea = @IdActualSubArea";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdActualSubArea = idActualSubArea });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    subArea = JsonConvert.DeserializeObject<ValorDTO>(resultado).Valor;
                return subArea;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion del dato por el Id
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SubAreaCapacitacion </returns>
        public SubAreaCapacitacion ObtenerPorId(int id)
        {
            try
            {
                SubAreaCapacitacion rpta = new SubAreaCapacitacion();
                var query = @"SELECT Id,
                                       Nombre,
                                       Descripcion,
                                       IdAreaCapacitacion,
                                       EsVisibleWeb,
                                       IdSubArea,
                                       DescripcionHTML,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       AliasFacebook
                                FROM pla.T_SubAreaCapacitacion
                                WHERE Estado = 1
                                      AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<SubAreaCapacitacion>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Verifica si existe el dato
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> bool </returns>
        public bool ExistePorId(int id)
        {
            try
            {
                var query = @"SELECT Id 
                                FROM pla.T_SubAreaCapacitacion
                                WHERE Estado = 1
                                      AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de SubArea filtrado por idArea
        /// </summary>
        /// <param name="idArea"></param>
        /// <returns> Lista DTO - ComboDTO - rpta </returns>
        public IEnumerable<ComboDTO> ObtenerSubAreaPorIdDeAreaLista(List<int> IdAreaCapacitacion)
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre from pla.V_RegistrosFiltroSubAreaCapacitacion WHERE Estado = 1 AND IdAreaCapacitacion IN @IdAreaCapacitacion";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAreaCapacitacion = IdAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerSubAreaPorIdDeAreaLista()", ex);
            }
        }

    }
}
