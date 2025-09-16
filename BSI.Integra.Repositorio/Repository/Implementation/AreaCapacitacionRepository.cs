using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AreaCapacitacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_AreaCapacitacion
    /// </summary>
    public class AreaCapacitacionRepository : GenericRepository<TAreaCapacitacion>, IAreaCapacitacionRepository
    {
        private Mapper _mapper;

        public AreaCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAreaCapacitacion, AreaCapacitacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAreaParametroSeoPw, AreaParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAreaCapacitacion MapeoEntidad(AreaCapacitacion entidad)
        {
            try
            {
                //crea la entidad padre
                TAreaCapacitacion modelo = _mapper.Map<TAreaCapacitacion>(entidad);

                //mapea los hijos
                if (entidad.AreaParametroSeoPw != null && entidad.AreaParametroSeoPw.Count > 0)
                {
                    modelo.TAreaParametroSeoPws = _mapper.Map<List<TAreaParametroSeoPw>>(entidad.AreaParametroSeoPw);
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaCapacitacion Add(AreaCapacitacion entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                base.Insert(AreaCapacitacion);
                return AreaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaCapacitacion Update(AreaCapacitacion entidad)
        {
            try
            {
                var AreaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaCapacitacion);
                return AreaCapacitacion;
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


        public IEnumerable<TAreaCapacitacion> Add(IEnumerable<AreaCapacitacion> listadoEntidad)
        {
            try
            {
                List<TAreaCapacitacion> listado = new List<TAreaCapacitacion>();
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

        public IEnumerable<TAreaCapacitacion> Update(IEnumerable<AreaCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAreaCapacitacion> listado = new List<TAreaCapacitacion>();
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
        public AreaCapacitacion ObtenerPorId(int id)
        {
            try
            {
                AreaCapacitacion rpta = new();
                var query = @"
                    SELECT
	                    Id,Nombre,Descripcion,ImgPortada,ImgSecundaria,ImgPortadaAlt,ImgSecundariaAlt,EsVisibleWeb,IdArea,EsWeb,DescripcionHTML,
	                    IdAreaCapacitacionFacebook,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion
                    FROM pla.T_AreaCapacitacion
                    WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<AreaCapacitacion>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCapacitacion.
        /// </summary>
        /// <returns> List<AreaCapacitacionDTO> </returns>
        public IEnumerable<AreaCapacitacionDTO> Obtener()
        {
            try
            {
                IEnumerable<AreaCapacitacionDTO> rpta = new List<AreaCapacitacionDTO>();
                var query = @"SELECT Id,Nombre,
                                       Descripcion,
                                       ImgPortada,
                                       ImgSecundaria,
                                       ImgPortadaAlt,
                                       ImgSecundariaAlt,
                                       EsVisibleWeb,
                                       IdArea,
                                       EsWeb,
                                       DescripcionHTML,
                                       IdAreaCapacitacionFacebook,
                                       ColorArea,
                                       UrlIconoArea
                                FROM pla.T_AreaCapacitacion
                                WHERE Estado = 1
                                ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<AreaCapacitacionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.V_RegistrosFiltroAreaCapacitacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltro(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.V_RegistrosFiltroAreaCapacitacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltro(): {ex.Message}");
            }
        }
        /// <summary>
        /// Obtiene el Area Capacitacion Anterior segun el Id Area Capacitacion Actual
        /// </summary>
        /// <returns></returns>
        public int ObtenerAreaCapacitacionAnterior(int areaActual)
        {
            try
            {
                int area = 0;
                string _query = "SELECT IdActualArea,IdAnteriorArea as Valor FROM pla.V_ObtenerAreaCapacitacionAnterior WHERE IdActualArea = @IdActualArea";
                var registro = _dapperRepository.FirstOrDefault(_query, new { IdActualArea = areaActual });
                if (!registro.Equals("null"))
                {
                    area = JsonConvert.DeserializeObject<ValorDTO>(registro).Valor;
                }
                return area;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de AreaCapacitacion con los campos de Id, Nombre y IdAreaCapacitacionFacebook.
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<AreaCapacitacionFiltroDTO> ObtenerFiltro()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdAreaCapacitacionFacebook FROM pla.V_RegistrosFiltroAreaCapacitacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<AreaCapacitacionFiltroDTO>>(resultado)!;
                return new List<AreaCapacitacionFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltro(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de AreaCapacitacion con los campos de Id, Nombre y IdAreaCapacitacionFacebook.
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<AreaCapacitacionFiltroDTO>> ObtenerFiltroAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdAreaCapacitacionFacebook FROM pla.V_RegistrosFiltroAreaCapacitacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<AreaCapacitacionFiltroDTO>>(resultado)!;
                return new List<AreaCapacitacionFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAsync(): {ex.Message}");
            }
        }
    }
}
