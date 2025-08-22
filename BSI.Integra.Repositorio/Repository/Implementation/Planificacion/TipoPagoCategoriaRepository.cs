using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: TipoPagoCategoriaRepository
    /// Autor: Jonthan Caipo
    /// Fecha: 19/06/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_TipoPagoCategoria
    /// </summary>
    public class TipoPagoCategoriaRepository : GenericRepository<TTipoPagoCategorium>, ITipoPagoCategoriaRepository
    {
        private Mapper _mapper;
        public TipoPagoCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoPagoCategorium, TipoPagoCategoria>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoPagoCategorium MapeoEntidad(TipoPagoCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoPagoCategorium tipoPagoCategoria = _mapper.Map<TTipoPagoCategorium>(entidad);

                return tipoPagoCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoPagoCategorium Add(TipoPagoCategoria entidad)
        {
            try
            {
                var tipoPagoCategoria = MapeoEntidad(entidad);
                Insert(tipoPagoCategoria);
                return tipoPagoCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoPagoCategorium Update(TipoPagoCategoria entidad)
        {
            try
            {
                var tipoPagoCategoria = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                tipoPagoCategoria.RowVersion = entidadExistente.RowVersion;

                Update(tipoPagoCategoria);
                return tipoPagoCategoria;
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

        public IEnumerable<TTipoPagoCategorium> Add(IEnumerable<TipoPagoCategoria> listadoEntidad)
        {
            try
            {
                List<TTipoPagoCategorium> listado = new List<TTipoPagoCategorium>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TTipoPagoCategorium> Update(IEnumerable<TipoPagoCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoPagoCategorium> listado = new List<TTipoPagoCategorium>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Jonathan Caipo
        /// Fecha: 19/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de tipo pagos (activo) con Id, Nombre  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerComboPorIdCategoriaPrograma(int idCategoriaPrograma)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var query = @"
                            SELECT DISTINCT
                                Id, Nombre 
                            FROM 
                                pla.V_TTipoPagoCategoria_Filtro 
                            WHERE 
                                EstadoTipoPago = 1 AND EstadoCategoriaPrograma = 1 AND IdCategoriaPrograma = @idCategoriaPrograma";
                var resultado = _dapperRepository.QueryDapper(query, new { idCategoriaPrograma });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#TPCR-OTPPC-001@Error en ObtenerTipoPagoPorCategoria() {ex.Message}", ex);
            }
        }
    }
}
