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
    /// Repositorio: TiempoLibreRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_TiempoLibre
    /// </summary>
    public class TiempoLibreRepository : GenericRepository<TTiempoLibre>, ITiempoLibreRepository
    {
        private Mapper _mapper;

        public TiempoLibreRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTiempoLibre, TiempoLibre>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTiempoLibre MapeoEntidad(TiempoLibre entidad)
        {
            try
            {
                //crea la entidad padre
                TTiempoLibre modelo = _mapper.Map<TTiempoLibre>(entidad);

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

        public TTiempoLibre Add(TiempoLibre entidad)
        {
            try
            {
                var TiempoLibre = MapeoEntidad(entidad);
                base.Insert(TiempoLibre);
                return TiempoLibre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTiempoLibre Update(TiempoLibre entidad)
        {
            try
            {
                var TiempoLibre = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TiempoLibre.RowVersion = entidadExistente.RowVersion;

                base.Update(TiempoLibre);
                return TiempoLibre;
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


        public IEnumerable<TTiempoLibre> Add(IEnumerable<TiempoLibre> listadoEntidad)
        {
            try
            {
                List<TTiempoLibre> listado = new List<TTiempoLibre>();
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

        public IEnumerable<TTiempoLibre> Update(IEnumerable<TiempoLibre> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTiempoLibre> listado = new List<TTiempoLibre>();
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TiempoLibre.
        /// </summary>
        /// <returns> List<TiempoLibreDTO> </returns>
        public IEnumerable<TiempoLibreDTO> ObtenerTiempoLibre()
        {
            try
            {
                List<TiempoLibreDTO> rpta = new List<TiempoLibreDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    TiempoMin,
	                    TIpo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_TiempoLibre
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TiempoLibreDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cantidad de minutos libre de Entrada.
        /// </summary>
        /// <returns> TiempoLibreRADTO </returns>
        public TiempoLibreRADTO ObtenerTiempoLibreTipoUno()
        {
            try
            {
                TiempoLibreRADTO rpta = new TiempoLibreRADTO();
                var query = @"SELECT TiempoMin FROM com.V_TTiempoLibre_FechaProgramacionAutomatica WHERE Tipo = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<TiempoLibreRADTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cantidad de minutos libres de Almuerzo.
        /// </summary>
        /// <returns> TiempoLibreRADTO </returns>
        public TiempoLibreRADTO ObtenerTiempoLibreTipoDos()
        {
            try
            {
                TiempoLibreRADTO rpta = new TiempoLibreRADTO();
                var query = @"SELECT TiempoMin FROM com.V_TTiempoLibre_FechaProgramacionAutomatica WHERE Tipo = 2";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<TiempoLibreRADTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los dias de reprogramacion manual
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerDiasReprogramacionAutomaticaOperaciones(int idOportunidad)
        {
            try
            {
                var Resultado = new ValorIntDTO();
                var registrosBD = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerDiasReprogramacionAutomaticaOperaciones", new { idOportunidad });
                Resultado = JsonConvert.DeserializeObject<ValorIntDTO>(registrosBD);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
