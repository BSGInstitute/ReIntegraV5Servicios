using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoCambioColRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_TipoCambioCol
    /// </summary>
    public class TipoCambioColRepository : GenericRepository<TTipoCambioCol>, ITipoCambioColRepository
    {
        private Mapper _mapper;

        public TipoCambioColRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCambioCol, TipoCambioCol>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCambioCol MapeoEntidad(TipoCambioCol entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCambioCol modelo = _mapper.Map<TTipoCambioCol>(entidad);

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

        public TTipoCambioCol Add(TipoCambioCol entidad)
        {
            try
            {
                var TipoCambioCol = MapeoEntidad(entidad);
                base.Insert(TipoCambioCol);
                return TipoCambioCol;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCambioCol Update(TipoCambioCol entidad)
        {
            try
            {
                var TipoCambioCol = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCambioCol.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCambioCol);
                return TipoCambioCol;
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


        public IEnumerable<TTipoCambioCol> Add(IEnumerable<TipoCambioCol> listadoEntidad)
        {
            try
            {
                List<TTipoCambioCol> listado = new List<TTipoCambioCol>();
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

        public IEnumerable<TTipoCambioCol> Update(IEnumerable<TipoCambioCol> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCambioCol> listado = new List<TTipoCambioCol>();
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
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos  Tipos de Cambios Col
        /// </summary>
        /// <returns>Nuevo objeto : Id, PesosDolares, DolaresPesos, Fecha, IdMoneda</returns>
        public object Obtener()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new { x.Id, x.PesosDolares, x.DolaresPesos, x.Fecha, x.IdMoneda }).OrderByDescending(x => x.Fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo tipo cambio colombiano
        /// </summary>
        /// <returns>Valor double Pesos Dolares</returns>
        public double ObtenerPesosDolaresUltimoTipoCambioColombia()
        {
            try
            {
                var query = @" 
                        SELECT TOP 1 
                        PesosDolares 
                        FROM fin.T_TipoCambioCol 
                        ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                return JsonConvert.DeserializeObject<double>(resultado); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ultimo tipo cambio colombiano
        /// </summary>
        /// <returns>Valor double Pesos Dolares</returns>
        public TipoCambioColombiaDTO ObtenerPesosDolaresTipoCambioColombia(string fecha)
        {
            try
            {
                var rpta = new TipoCambioColombiaDTO();
                var query = @" SELECT PesosDolares FROM fin.T_TipoCambioCol WHERE Fecha = @Fecha";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Fecha = fecha });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoCambioColombiaDTO>(resultado);
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
