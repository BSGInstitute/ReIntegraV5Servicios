using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoDatoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class TipoDatoRepository : GenericRepository<TTipoDato>, ITipoDatoRepository
    {
        private Mapper _mapper;

        public TipoDatoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDato, TipoDato>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoDato MapeoEntidad(TipoDato entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoDato modelo = _mapper.Map<TTipoDato>(entidad);

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

        public TTipoDato Add(TipoDato entidad)
        {
            try
            {
                var TipoDato = MapeoEntidad(entidad);
                base.Insert(TipoDato);
                return TipoDato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoDato Update(TipoDato entidad)
        {
            try
            {
                var TipoDato = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDato.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDato);
                return TipoDato;
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


        public IEnumerable<TTipoDato> Add(IEnumerable<TipoDato> listadoEntidad)
        {
            try
            {
                List<TTipoDato> listado = new List<TTipoDato>();
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

        public IEnumerable<TTipoDato> Update(IEnumerable<TipoDato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDato> listado = new List<TTipoDato>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_TipoDato WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_TipoDato WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<TipoDatoDTO> ObtenerTipoDato()
        {
            try
            {
                List<TipoDatoDTO> rpta = new List<TipoDatoDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Prioridad, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_TipoDato
                            WHERE Estado=1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDatoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez  Neyra. 
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDato
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<TipoDatoFiltroDTO> ObtenerFiltroTipoDato()
        {
            try
            {
                List<TipoDatoFiltroDTO> rpta = new List<TipoDatoFiltroDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, Prioridad FROM mkt.T_TipoDato
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDatoFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene Lista de Tipo de Datos para filtro en formularios (solo los que tienen por nombre 'lanzamiento')
		/// </summary>
		/// <returns></returns>
        public IEnumerable<ComboDTO> CargarTipoDatoChat()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM mkt.V_TTipoDato WHERE Nombre = 'lanzamiento' and estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
