using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OrigenDatoCalidadDetalleRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenDatoCalidadDetalle
    /// </summary>
    public class OrigenDatoCalidadDetalleRepository : GenericRepository<TOrigenDatoCalidadDetalle>, IOrigenDatoCalidadDetalleRepository
    {
        private Mapper _mapper;

        public OrigenDatoCalidadDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenDatoCalidadDetalle, OrigenDatoCalidadDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOrigenDatoCalidadDetalle MapeoEntidad(OrigenDatoCalidadDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TOrigenDatoCalidadDetalle modelo = _mapper.Map<TOrigenDatoCalidadDetalle>(entidad);

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

        public TOrigenDatoCalidadDetalle Add(OrigenDatoCalidadDetalle entidad)
        {
            try
            {
                var OrigenDatoCalidadDetalle = MapeoEntidad(entidad);
                base.Insert(OrigenDatoCalidadDetalle);
                return OrigenDatoCalidadDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOrigenDatoCalidadDetalle Update(OrigenDatoCalidadDetalle entidad)
        {
            try
            {
                var OrigenDatoCalidadDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OrigenDatoCalidadDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(OrigenDatoCalidadDetalle);
                return OrigenDatoCalidadDetalle;
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


        public IEnumerable<TOrigenDatoCalidadDetalle> Add(IEnumerable<OrigenDatoCalidadDetalle> listadoEntidad)
        {
            try
            {
                List<TOrigenDatoCalidadDetalle> listado = new List<TOrigenDatoCalidadDetalle>();
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

        public IEnumerable<TOrigenDatoCalidadDetalle> Update(IEnumerable<OrigenDatoCalidadDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigenDatoCalidadDetalle> listado = new List<TOrigenDatoCalidadDetalle>();
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
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las configuraciones de las categoria origen de un sector determinado
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public List<OrigenDatoCalidadDetalleDTO> ObtenerOrigenSectorConfigurado(int IdOrigenSector)
        {
            try
            {
                List<OrigenDatoCalidadDetalleDTO> rpta = new List<OrigenDatoCalidadDetalleDTO>();
                var resultadoQuery = _dapperRepository.QueryDapper(@"SELECT ODCD.IdOrigenDatoCalidad,
	                                                                    CO.IdProveedorCampaniaIntegra,
	                                                                    CO.Id AS IdCategoriaOrigen,
	                                                                    CO.Nombre,
	                                                                    ODCD.DatosCalidad,
                                                                        ODCD.DatoCalidadWhatsapp,
																		ODCD.DatoCalidadMailing,
	                                                                    ODCD.MuyAltaAr,
	                                                                    ODCD.MuyAltaAd,
	                                                                    ODCD.AltaAd,
	                                                                    ODCD.AltaAr,
	                                                                    ODCD.MediaAd,
	                                                                    ODCD.MediaAr,
	                                                                    ODC.AgruparCategoriaOrigen
                                                                    FROM mkt.T_OrigenDatoCalidadDetalle AS ODCD
                                                                    INNER JOIN mkt.T_OrigenDatoCalidad AS ODC
                                                                    ON ODC.Id = ODCD.IdOrigenDatoCalidad
                                                                    INNER JOIN mkt.T_OrigenSector AS OS
                                                                    ON OS.Id = ODC.IdOrigenSector
                                                                    INNER JOIN mkt.T_CategoriaOrigen AS CO
                                                                    ON CO.Id = ODC.IdCategoriaOrigen
                                                                    WHERE ODC.Estado = 1
	                                                                    AND ODCD.Estado = 1
	                                                                    AND OS.Estado = 1
                                                                        AND CO.Estado = 1
	                                                                    AND ODC.AgruparCategoriaOrigen = 0
	                                                                    AND OS.Id = @IdOrigenSector", new { IdOrigenSector });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenDatoCalidadDetalleDTO>>(resultadoQuery);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Trae las configuraciones de las categoria origen agrupadas de un sector determinado
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public origenDatoCalidadDetalleConfiguracionAgrupadoDTO ObtenerOrigenSectorConfiguradoCategoriaAgrupado(int IdOrigenSector)
        {
            try
            {
                origenDatoCalidadDetalleConfiguracionAgrupadoDTO rpta = new origenDatoCalidadDetalleConfiguracionAgrupadoDTO();
                var resultadoQuery = _dapperRepository.FirstOrDefault(@"SELECT 
	                                                                ODCD.DatosCalidad,
                                                                    ODCD.DatoCalidadWhatsapp,
                                                                    ODCD.DatoCalidadMailing,
	                                                                ODCD.MuyAltaAr,
	                                                                ODCD.MuyAltaAd,
	                                                                ODCD.AltaAd,
	                                                                ODCD.AltaAr,
	                                                                ODCD.MediaAd,
	                                                                ODCD.MediaAr,
	                                                                ODC.AgruparCategoriaOrigen
                                                                FROM mkt.T_OrigenDatoCalidadDetalle AS ODCD
                                                                INNER JOIN mkt.T_OrigenDatoCalidad AS ODC
                                                                ON ODC.Id = ODCD.IdOrigenDatoCalidad
                                                                INNER JOIN mkt.T_OrigenSector AS OS
                                                                ON OS.Id = ODC.IdOrigenSector
                                                                INNER JOIN mkt.T_CategoriaOrigen AS CO
                                                                ON CO.Id = ODC.IdCategoriaOrigen
                                                                WHERE ODC.Estado = 1
	                                                                AND ODCD.Estado = 1
	                                                                AND OS.Estado = 1
                                                                    AND CO.Estado = 1
	                                                                AND ODC.AgruparCategoriaOrigen = 1
                                                                    AND OS.Id = @IdOrigenSector
                                                                GROUP BY 
	                                                                ODCD.DatosCalidad,
                                                                    ODCD.DatoCalidadWhatsapp,
                                                                    ODCD.DatoCalidadMailing,
	                                                                ODCD.MuyAltaAr,
	                                                                ODCD.MuyAltaAd,
	                                                                ODCD.AltaAd,
	                                                                ODCD.AltaAr,
	                                                                ODCD.MediaAd,
	                                                                ODCD.MediaAr,
	                                                                ODC.AgruparCategoriaOrigen", new { IdOrigenSector });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<origenDatoCalidadDetalleConfiguracionAgrupadoDTO>(resultadoQuery);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Trae el nombre y la cantidad de las categoria origen agrupadas de un sector determinado
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public NombreCantidadAgrupadoDTO ObtenerNombreOrigenDatoCalidadDetalleAgrupado(int IdOrigenSector)
        {
            try
            {
                NombreCantidadAgrupadoDTO rpta = new NombreCantidadAgrupadoDTO();
                var resultadoQuery = _dapperRepository.FirstOrDefault(@"SELECT 
	                                                                    'Agrupados' AS Nombre,
	                                                                    COUNT(*) AS CantidadAgrupados
                                                                    FROM mkt.T_OrigenDatoCalidadDetalle AS ODCD
                                                                    INNER JOIN mkt.T_OrigenDatoCalidad AS ODC
                                                                    ON ODC.Id = ODCD.IdOrigenDatoCalidad
                                                                    INNER JOIN mkt.T_OrigenSector AS OS
                                                                    ON OS.Id = ODC.IdOrigenSector
                                                                    INNER JOIN mkt.T_CategoriaOrigen AS CO
                                                                    ON CO.Id = ODC.IdCategoriaOrigen
                                                                    WHERE ODC.Estado = 1
	                                                                    AND ODCD.Estado = 1
	                                                                    AND OS.Estado = 1
                                                                        AND CO.Estado = 1
	                                                                    AND ODC.AgruparCategoriaOrigen = 1
                                                                    AND OS.Id = @IdOrigenSector", new { IdOrigenSector });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<NombreCantidadAgrupadoDTO>(resultadoQuery);
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
