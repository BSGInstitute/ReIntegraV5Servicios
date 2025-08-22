using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoComprobanteRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoComprobante
    /// </summary>
    public class TipoComprobanteRepository : GenericRepository<TTipoComprobante>, ITipoComprobanteRepository
    {
        private Mapper _mapper;

        public TipoComprobanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoComprobante, TipoComprobante>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoComprobante MapeoEntidad(TipoComprobante entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoComprobante modelo = _mapper.Map<TTipoComprobante>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoComprobante Add(TipoComprobante entidad)
        {
            try
            {
                var TipoComprobante = MapeoEntidad(entidad);
                base.Insert(TipoComprobante);
                return TipoComprobante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoComprobante Update(TipoComprobante entidad)
        {
            try
            {
                var TipoComprobante = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoComprobante.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoComprobante);
                return TipoComprobante;
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


        public IEnumerable<TTipoComprobante> Add(IEnumerable<TipoComprobante> listadoEntidad)
        {
            try
            {
                List<TTipoComprobante> listado = new List<TTipoComprobante>();
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

        public IEnumerable<TTipoComprobante> Update(IEnumerable<TipoComprobante> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoComprobante> listado = new List<TTipoComprobante>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoComprobante.
        /// </summary>
        /// <returns> List<TipoComprobanteDTO> </returns>
        public IEnumerable<TipoComprobanteDTO> ObtenerListaTipoComprobante()
        {
            try
            {
                List<TipoComprobanteDTO> respuesta = new List<TipoComprobanteDTO>();
                var _queryfiltro = @"
                        SELECT [Id]
                              ,[IdPais]
                              ,[Nombre]
                         FROM [fin].[T_TipoComprobante] where estado=1 order by Id ";
                var resultado = _dapperRepository.QueryDapper(_queryfiltro, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<TipoComprobanteDTO>>(resultado);
                }
                return respuesta;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

    }
}
