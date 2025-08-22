using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TarifarioDetalleAlternoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión general de T_TarifarioDetalleAlterno
    /// </summary>
    public class TarifarioDetalleAlternoRepository : GenericRepository<TTarifarioDetalleAlterno>, ITarifarioDetalleAlternoRepository
    {
        private Mapper _mapper;

        public TarifarioDetalleAlternoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTarifarioDetalleAlterno, TarifarioDetalleAlterno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTarifarioDetalleAlterno MapeoEntidad(TarifarioDetalleAlterno entidad)
        {
            try
            {
                //crea la entidad padre
                TTarifarioDetalleAlterno modelo = _mapper.Map<TTarifarioDetalleAlterno>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTarifarioDetalleAlterno Add(TarifarioDetalleAlterno entidad)
        {
            try
            {
                var TarifarioDetalleAlterno = MapeoEntidad(entidad);
                base.Insert(TarifarioDetalleAlterno);
                return TarifarioDetalleAlterno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTarifarioDetalleAlterno Update(TarifarioDetalleAlterno entidad)
        {
            try
            {
                var TarifarioDetalleAlterno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TarifarioDetalleAlterno.RowVersion = entidadExistente.RowVersion;

                base.Update(TarifarioDetalleAlterno);
                return TarifarioDetalleAlterno;
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


        public IEnumerable<TTarifarioDetalleAlterno> Add(IEnumerable<TarifarioDetalleAlterno> listadoEntidad)
        {
            try
            {
                List<TTarifarioDetalleAlterno> listado = new List<TTarifarioDetalleAlterno>();
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

        public IEnumerable<TTarifarioDetalleAlterno> Update(IEnumerable<TarifarioDetalleAlterno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTarifarioDetalleAlterno> listado = new List<TTarifarioDetalleAlterno>();
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
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene una tupla de la tabla mkt.T_TarifarioDetalleAlterno por Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TarifarioDetalleAlterno ObtenerPorId(int id)
        {
            try
            {
                TarifarioDetalleAlterno respuesta = new TarifarioDetalleAlterno();
                var query = @"SELECT 
                                Id, IdTarifario, Concepto, IdPais, Monto, AplicaCuota, Descripcion, TipoCantidad, Estados, SubEstados, Estado, UsuarioCreacion, 
                                UsuarioModificacion, FechaCreacion, FechaModificacion, IdMigracion, IdMoneda, VisualizarPortalWeb
                            FROM 
                                mkt.T_TarifarioDetalleAlterno
                            WHERE 
                                Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<TarifarioDetalleAlterno>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
