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
    /// Repositorio: TipoCategoriaOrigenRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 11/08//2022
    /// <summary>
    /// Gestión general de T_TipoCategoriaOrigen
    /// </summary>
    public class TipoCategoriaOrigenRepository : GenericRepository<TTipoCategoriaOrigen>, ITipoCategoriaOrigenRepository
    {
        private Mapper _mapper;

        public TipoCategoriaOrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCategoriaOrigen, TipoCategoriaOrigen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoCategoriaOrigen MapeoEntidad(TipoCategoriaOrigen entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCategoriaOrigen modelo = _mapper.Map<TTipoCategoriaOrigen>(entidad);

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

        public TTipoCategoriaOrigen Add(TipoCategoriaOrigen entidad)
        {
            try
            {
                var TipoCategoriaOrigen = MapeoEntidad(entidad);
                base.Insert(TipoCategoriaOrigen);
                return TipoCategoriaOrigen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCategoriaOrigen Update(TipoCategoriaOrigen entidad)
        {
            try
            {
                var TipoCategoriaOrigen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCategoriaOrigen.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCategoriaOrigen);
                return TipoCategoriaOrigen;
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


        public IEnumerable<TTipoCategoriaOrigen> Add(IEnumerable<TipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                List<TTipoCategoriaOrigen> listado = new List<TTipoCategoriaOrigen>();
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

        public IEnumerable<TTipoCategoriaOrigen> Update(IEnumerable<TipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCategoriaOrigen> listado = new List<TTipoCategoriaOrigen>();
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
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 11/08//2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_TipoCategoriaOrigen WHERE Estado = 1";
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
        /// Fecha: 06/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_TipoCategoriaOrigen WHERE Estado = 1";
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
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 11/08//2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<TipoCategoriaOrigenDTO> </returns>
        public IEnumerable<TipoCategoriaOrigenDTO> ObtenerTipoCategoriaOrigen()
        {
            try
            {
                List<TipoCategoriaOrigenDTO> rpta = new List<TipoCategoriaOrigenDTO>();
                var query = @"select Id, Nombre, Descripcion, Meta,Orden,OportunidadMaxima,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion from mkt.T_TipoCategoriaOrigen where Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCategoriaOrigenDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 11/08//2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<TipoCategoriaOrigenDTO> </returns>


        public IEnumerable<TipoCategoriaOrigenFiltroDTO> ObtenerFiltroTipoCategoriaOrigen()
        {
            try
            {
                List<TipoCategoriaOrigenFiltroDTO> rpta = new List<TipoCategoriaOrigenFiltroDTO>();
                var query = @"select Id, Nombre, Descripcion, Meta,OportunidadMaxima from mkt.T_TipoCategoriaOrigen where Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCategoriaOrigenFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO> ObtenerRemarketingTipoCategoriaOrigen()
        {
            throw new NotImplementedException();
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 03/11/2022
        /// <summary>
        /// Obtiene el tipo categoria origen para Filtro
        /// <returns>id, nombre</returns>
        public IEnumerable<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        ///  Obtiene todos los registros sin los campos de auditoria
        /// </summary>
        /// <param name="id">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <returns>Entero con el id tipo de categoria origen</returns>
        public int ObtenerTipoCategoriaOrigenID(int id)
        {
            try
            {
                Dictionary<string, int> respuesta = new Dictionary<string, int>();
                string queryTipoInteraccion = string.Empty;
                queryTipoInteraccion = " SELECT IdTipoCategoriaOrigen FROM mkt.V_TCategoriaOrigen_ObtenerIdTipoCategoriaOrigen WHERE Id = @Id";
                var tipoInteraccion = _dapperRepository.FirstOrDefault(queryTipoInteraccion, new { Id = id });
                if (!string.IsNullOrEmpty(tipoInteraccion) && !tipoInteraccion.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(tipoInteraccion);
                }
                //REVISAR CARLOS 17082019
                return respuesta["IdTipoCategoriaOrigen"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
