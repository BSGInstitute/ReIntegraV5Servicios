using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoCambioMonedumRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_TipoCambioMonedum
    /// </summary>
    public class TipoCambioMonedumRepository : GenericRepository<TTipoCambioMonedum>, ITipoCambioMonedumRepository
    {
        private Mapper _mapper;

        public TipoCambioMonedumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCambioMonedum, TipoCambioMonedum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCambioMonedum MapeoEntidad(TipoCambioMonedum entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCambioMonedum modelo = _mapper.Map<TTipoCambioMonedum>(entidad);

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

        public TTipoCambioMonedum Add(TipoCambioMonedum entidad)
        {
            try
            {
                var TipoCambioMonedum = MapeoEntidad(entidad);
                base.Insert(TipoCambioMonedum);
                return TipoCambioMonedum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCambioMonedum Update(TipoCambioMonedum entidad)
        {
            try
            {
                var TipoCambioMonedum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCambioMonedum.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCambioMonedum);
                return TipoCambioMonedum;
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


        public IEnumerable<TTipoCambioMonedum> Add(IEnumerable<TipoCambioMonedum> listadoEntidad)
        {
            try
            {
                List<TTipoCambioMonedum> listado = new List<TTipoCambioMonedum>();
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

        public IEnumerable<TTipoCambioMonedum> Update(IEnumerable<TipoCambioMonedum> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCambioMonedum> listado = new List<TTipoCambioMonedum>();
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

        //public IEnumerable<TipoCambioMonedum> GetBy(Expression<Func<TTipoCambioMonedum, bool>> filter)
        //{
        //    IEnumerable<TTipoCambioMonedum> listado = base.GetBy(filter);
        //    List<TipoCambioMonedum> listadoBO = new List<TipoCambioMonedum>();
        //    foreach (var itemEntidad in listado)
        //    {
        //        TipoCambioMonedum objetoBO = Mapper.Map<TTipoCambioMonedum, TipoCambioMonedum>(itemEntidad, ope => ope.CreateMap(MemberList.None));
        //        listadoBO.Add(objetoBO);
        //    }

        //    return listadoBO;
        //}
        #endregion
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCambioMonedum.
        /// </summary>
        /// <returns> List<TipoCambioMonedumDTO> </returns>
        public IEnumerable<TipoCambioMonedumDTO> ObtenerTipoCambioMonedum()
        {
            try
            {
                List<TipoCambioMonedumDTO> rpta = new List<TipoCambioMonedumDTO>();
                var query = @"
                    SELECT 
                        Id, 
                        NombreMoneda, 
                        IdMoneda, 
                        DolarAMoneda, 
                        MonedaADolar, 
                        Fecha, 
                        FechaCreacion 
                    FROM fin.V_ObtenerTipoCambioMoneda 
                    WHERE EstadoTipoCambioMoneda = 1 and EstadoMoneda = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCambioMonedumDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCambioFechaDTO ObtenerTasaCambioMoneda(int idMoneda)
        {
            try
            {
                TipoCambioFechaDTO _item = new TipoCambioFechaDTO();
                _item.Cambio = -1;
                var fecha = DateTime.Now;

                var temp = GetBy(x => x.Fecha == fecha.Date && x.IdMoneda == idMoneda, x => new TipoCambioFechaDTO { Cambio = x.MonedaAdolar, Fecha = x.Fecha });
                foreach (var item in temp)
                {
                    _item = item;
                }
                return _item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCambioMonedum ObtenerPorFecha(int idMoneda)
        {
            try
            {
                TTipoCambioMonedum rpta = new TTipoCambioMonedum();
                var query = @"
                    SELECT 
                        *
                    FROM fin.T_TipoCambioMoneda 
                    WHERE Estado = 1 and IdMoneda = @idMoneda AND fecha=cast(getdate() as date)";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMoneda });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<TTipoCambioMonedum>(resultado);
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
