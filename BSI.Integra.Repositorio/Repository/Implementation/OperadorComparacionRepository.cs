using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OperadorComparacionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_OperadorComparacion
    /// </summary>
    public class OperadorComparacionRepository : GenericRepository<TOperadorComparacion>, IOperadorComparacionRepository
    {
        private Mapper _mapper;

        public OperadorComparacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOperadorComparacion, OperadorComparacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOperadorComparacion MapeoEntidad(OperadorComparacion entidad)
        {
            try
            {
                //crea la entidad padre
                TOperadorComparacion modelo = _mapper.Map<TOperadorComparacion>(entidad);

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

        public TOperadorComparacion Add(OperadorComparacion entidad)
        {
            try
            {
                var OperadorComparacion = MapeoEntidad(entidad);
                base.Insert(OperadorComparacion);
                return OperadorComparacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOperadorComparacion Update(OperadorComparacion entidad)
        {
            try
            {
                var OperadorComparacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OperadorComparacion.RowVersion = entidadExistente.RowVersion;

                base.Update(OperadorComparacion);
                return OperadorComparacion;
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


        public IEnumerable<TOperadorComparacion> Add(IEnumerable<OperadorComparacion> listadoEntidad)
        {
            try
            {
                List<TOperadorComparacion> listado = new List<TOperadorComparacion>();
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

        public IEnumerable<TOperadorComparacion> Update(IEnumerable<OperadorComparacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOperadorComparacion> listado = new List<TOperadorComparacion>();
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OperadorComparacion.
        /// </summary>
        /// <returns> List<OperadorComparacionDTO> </returns>
        public IEnumerable<OperadorComparacionDTO> ObtenerOperadorComparacion()
        {
            try
            {
                List<OperadorComparacionDTO> rpta = new List<OperadorComparacionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Simbolo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_OperadorComparacion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OperadorComparacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de T_OperadorComparacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<OperadorComparacionComboNombreDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM mkt.T_OperadorComparacion WHERE Estado = 1";
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
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,Nombre FROM mkt.T_OperadorComparacion WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        /// Autor:Margiory Ramirez Neyra
        /// Fecha: 03/11/2022
        /// <summary>
        /// Obtiene un listado de operador comparacion retornando el simbolo
        /// </summary>
        /// <returns> List<OperadorComparacionDTO> </returns>
        public List<OperadoresComparacionDTO> ObtenerListado()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new OperadoresComparacionDTO { Id = x.Id, Simbolo = x.Simbolo }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public IEnumerable<ComboDTO> ObtenerComboParaFilroSegmento()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"   SELECT* FROM mkt.T_OperadorComparacion WHERE id <= 5 and Estado = 1";
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
