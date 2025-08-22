using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OrigenSectorRepository
    /// Autor: Edson Daniel Mayta Escobedo
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class OrigenSectorRepository : GenericRepository<TOrigenSector>, IOrigenSectorRepository
    {
        private Mapper _mapper;

        public OrigenSectorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigenSector, OrigenSector>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOrigenSector MapeoEntidad(OrigenSector entidad)
        {
            try
            {
                //crea la entidad padre
                TOrigenSector modelo = _mapper.Map<TOrigenSector>(entidad);

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

        public TOrigenSector Add(OrigenSector entidad)
        {
            try
            {
                var OrigenSector = MapeoEntidad(entidad);
                base.Insert(OrigenSector);
                return OrigenSector;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOrigenSector Update(OrigenSector entidad)
        {
            try
            {
                var OrigenSector = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OrigenSector.RowVersion = entidadExistente.RowVersion;

                base.Update(OrigenSector);
                return OrigenSector;
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


        public IEnumerable<TOrigenSector> Add(IEnumerable<OrigenSector> listadoEntidad)
        {
            try
            {
                List<TOrigenSector> listado = new List<TOrigenSector>();
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

        public IEnumerable<TOrigenSector> Update(IEnumerable<OrigenSector> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigenSector> listado = new List<TOrigenSector>();
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
        /// Obtiene los proveedores de origenes asignados
        /// </summary>
        /// <returns> List<OrigenSectorDTO> </returns>
        public List<OrigenSectorDTO> ObtenerOrigenConfigurado()
        {
            try
            {
                List<OrigenSectorDTO> rpta = new List<OrigenSectorDTO>();
                var query = @"SELECT 
                                ODC.IdProveedorCampaniaIntegra,
                                PCI.Nombre 
                            FROM mkt.T_OrigenDatoCalidad AS	ODC
                            INNER JOIN mkt.T_ProveedorCampaniaIntegra AS PCI
                            ON PCI.Id = ODC.IdProveedorCampaniaIntegra
                            WHERE ODC.IdOrigenSector != 0 
                                AND ODC.IdOrigenSector IS NOT NULL
                                AND ODC.Estado = 1
                                AND PCI.Estado = 1
                            GROUP BY ODC.IdProveedorCampaniaIntegra,PCI.Nombre
                            ORDER BY PCI.Nombre desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenSectorDTO>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los proveedores de origenes no asignados
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public List<OrigenSectorDTO> ObtenerOrigenNoConfigurado()
        {
            try
            {
                List<OrigenSectorDTO> rpta = new List<OrigenSectorDTO>();
                var query = @"SELECT 
	                            ODC.IdProveedorCampaniaIntegra,
	                            PCI.Nombre 
                            FROM mkt.T_OrigenDatoCalidad AS	ODC
                            INNER JOIN mkt.T_ProveedorCampaniaIntegra AS PCI
                            ON PCI.Id = ODC.IdProveedorCampaniaIntegra
                            WHERE ODC.IdOrigenSector = 0 
	                            OR ODC.IdOrigenSector IS  NULL
	                            AND ODC.Estado = 1
	                            AND PCI.Estado = 1
                            GROUP BY ODC.IdProveedorCampaniaIntegra,PCI.Nombre
                            ORDER BY PCI.Nombre DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenSectorDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Edson Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los origenes sector configurados
        /// </summary>
        /// <returns> List<OrigenSectorConfiguradoDTO> </returns>
        public List<OrigenSectorConfiguradoDTO> ObtenerOrigenSectorConfigurado()
        {
            try
            {
                List<OrigenSectorConfiguradoDTO> rpta = new List<OrigenSectorConfiguradoDTO>();
                var query = @"SELECT ODC.Id,
	                                ODC.Nombre,
	                                ODC.Descripcion,
	                                ODC.Orden,
	                            (SELECT COUNT(*) AS ContadorCantidadOportunidad
	                            FROM mkt.T_OrigenDatoCalidad AS ODCS
	                            WHERE ODCS.Estado = 1 
		                        AND ODC.Estado = 1
		                            AND ODCS.IdOrigenSector = ODC.Id
	                            )AS CantidadOportunidad,
	                            IIF( 
		                            (
			                            SELECT COUNT(*) AS Contador
			                            FROM (SELECT ODCA.IdProveedorCampaniaIntegra
				                            FROM mkt.T_OrigenDatoCalidad AS ODCA
				                            WHERE ODCA.Estado = 1
					                            AND ODCA.IdOrigenSector = ODC.Id
				                            GROUP BY ODCA.IdProveedorCampaniaIntegra
				                            )AS ContadorTabla
		                            )>1,1 ,0 ) AS EsAgrupado
                            FROM mkt.T_OrigenSector AS ODC
                            WHERE ODC.Estado = 1
                            ORDER BY ODC.Orden ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenSectorConfiguradoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Edson Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los id del origenDatoCalidadDetalle para su configuracion
        /// </summary>
        /// <returns> List<ListaIdCategoriaOrigenDTO> </returns>
        public List<ListaIdCategoriaOrigenDTO> ObtenerOrigenDatoCalidadDetalle(int? idOrigensector)
        {
            try
            {
                List<ListaIdCategoriaOrigenDTO> rpta = new List<ListaIdCategoriaOrigenDTO>();
                var query = @"SELECT ODC.Id 
                                 FROM mkt.T_OrigenSector AS OS
                                 INNER JOIN mkt.T_OrigenDatoCalidad AS ODC
                                 ON OS.ID = ODC.IdOrigenSector
                                 INNER JOIN mkt.T_OrigenDatoCalidadDetalle AS ODCD
                                 ON ODC.Id = ODCD.IdOrigenDatoCalidad
                                 WHERE ODC.Estado = 1
	                                AND ODCD.Estado = 1
	                                AND OS.Estado = 1
	                                AND ODC.AgruparCategoriaOrigen = 1
	                                AND os.Id = @idOrigensector";
                var resultado = _dapperRepository.QueryDapper(query, new { idOrigensector });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ListaIdCategoriaOrigenDTO>>(resultado);
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
        /// Elimina las configuraciones de un sector y asigna las configuraciones por defecto
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarOrigenSector(int IdOrigenSector, string UsuarioModificacion)
        {
            try
            {
                bool rpta = new bool();
                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_EliminarOrigenSector", new { IdOrigenSector, UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la configuracion de una lista de categoria origen
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizarDatosDeConfiguracion(List<ActualizarDatosDeConfiguracionDTO> ListaConfiguracionActualizada)
        {
            try
            {
                List<int> Valor = new List<int>();
                bool InsertExitoso = false;

                foreach (ActualizarDatosDeConfiguracionDTO item in ListaConfiguracionActualizada)
                {
                    var resultado = _dapperRepository.QuerySPDapper("mkt.[SP_ActualizarConfiguracionesOrigenDatoCalidadDetalleV2]", new { item.idorigendatoCalidad, item.DatosCalidad,item.DatoCalidadWhatsapp,item.DatoCalidadMailing, item.UsuarioModificacion, item.AltaAd, item.AltaAr, item.MediaAd, item.MediaAr, item.MuyAltaAr, item.MuyAltaAd });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Edson Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la configuracion de una lista de categoria origen
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarOportunidadConfiguracion()
        {
            try
            {
                EstadoActualizacionDTO? EstadoActualizacion = new EstadoActualizacionDTO();


                var resultado = _dapperRepository.QuerySPDapper("mkt.SP_EliminarOportunidadConfiguracion", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    EstadoActualizacion = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                }

                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Edson Mayta Escobedo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Insertar origenSector
        /// </summary>
        /// <returns> bool </returns>
        public bool? InsertarOrigenSector(string Nombre, string Descripcion, int Orden, String UsuarioCreacion)
        {
            try
            {
                EstadoActualizacionDTO RespuestaBool = new EstadoActualizacionDTO();
                RespuestaBool.Valor = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_InsertarOrigenSector", new { Nombre, Descripcion, Orden, UsuarioCreacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    RespuestaBool = JsonConvert.DeserializeObject<EstadoActualizacionDTO>(resultado);
                    return RespuestaBool.Valor;
                }
                return RespuestaBool.Valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
