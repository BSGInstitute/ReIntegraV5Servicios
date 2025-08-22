using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoCambioRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_TipoCambio
    /// </summary>
    public class TipoCambioRepository : GenericRepository<TTipoCambio>, ITipoCambioRepository
    {
        private Mapper _mapper;

        public TipoCambioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCambio, TipoCambio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCambio MapeoEntidad(TipoCambio entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCambio modelo = _mapper.Map<TTipoCambio>(entidad);

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

        public TTipoCambio Add(TipoCambio entidad)
        {
            try
            {
                var TipoCambio = MapeoEntidad(entidad);
                base.Insert(TipoCambio);
                return TipoCambio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCambio Update(TipoCambio entidad)
        {
            try
            {
                var TipoCambio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCambio.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCambio);
                return TipoCambio;
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


        public IEnumerable<TTipoCambio> Add(IEnumerable<TipoCambio> listadoEntidad)
        {
            try
            {
                List<TTipoCambio> listado = new List<TTipoCambio>();
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

        public IEnumerable<TTipoCambio> Update(IEnumerable<TipoCambio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCambio> listado = new List<TTipoCambio>();
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
        /// Obtiene todos los registros de T_TipoCambio.
        /// </summary>
        /// <returns> List<TipoCambioDTO> </returns>
        public IEnumerable<TipoCambioObtenerDTO> Obtener()
        {
            try
            {
                List<TipoCambioObtenerDTO> rpta = new List<TipoCambioObtenerDTO>();
                var query = @"
                    SELECT 
                       [Id]
                      ,[SolesDolares]
                      ,[DolaresSoles]
                      ,[Fecha]
                FROM [fin].[T_TipoCambio]
                WHERE Estado = 1 Order by Fecha Desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCambioObtenerDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        /// <paramref name="tipoCambio"/> Tipo de cambio 1 aoles, 2 Dolares
        public TipoCambioFechaDTO ObtenerTipoCambio(int tipoCambio)
        {
            try
            {
                TipoCambioFechaDTO _item = new TipoCambioFechaDTO();
                var fecha = DateTime.Now;

                if (tipoCambio == 1)
                {
                    var temp = GetBy(x => x.Fecha == fecha.Date, x => new TipoCambioFechaDTO { Cambio = x.SolesDolares, Fecha = x.Fecha });
                    foreach (var item in temp)
                    {
                        _item = item;
                    }
                }
                else if (tipoCambio == 2)
                {
                    var temp = GetBy(x => x.Fecha == fecha.Date, x => new TipoCambioFechaDTO { Cambio = x.DolaresSoles, Fecha = x.Fecha });
                    foreach (var item in temp)
                    {
                        _item = item;
                    }
                }

                return _item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoContribuyente para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContribuyenteComboDTO> </returns>
        /// <paramref name="tipoCambio"/> Tipo de cambio
        /// <paramref name="fecha"/> Fecha
        public IEnumerable<TipoCambioReporteDTO> ObtenerTipoCambioFiltro(TipoCambioFiltroDTO filtro)
        {
            try
            {
                List<TipoCambioReporteDTO> lista = new List<TipoCambioReporteDTO>();
                var query = _dapperRepository.QuerySPDapper("[fin].[SP_ReporteTasasCambio]", new { idMoneda = filtro.IdMoneda, Fecha = filtro.Fecha });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<TipoCambioReporteDTO>>(query);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}
