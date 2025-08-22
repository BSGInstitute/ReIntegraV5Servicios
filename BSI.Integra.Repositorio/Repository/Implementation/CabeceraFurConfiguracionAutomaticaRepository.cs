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
    /// Repositorio: CabeceraFurConfiguracionAutomaticaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CabeceraFurConfiguracionAutomatica
    /// </summary>
    public class CabeceraFurConfiguracionAutomaticaRepository : GenericRepository<TCabeceraFurConfiguracionAutomatica>, ICabeceraFurConfiguracionAutomaticaRepository
    {
        private Mapper _mapper;

        public CabeceraFurConfiguracionAutomaticaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCabeceraFurConfiguracionAutomatica, CabeceraFurConfiguracionAutomatica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCabeceraFurConfiguracionAutomatica MapeoEntidad(CabeceraFurConfiguracionAutomatica entidad)
        {
            try
            {
                //crea la entidad padre
                TCabeceraFurConfiguracionAutomatica modelo = _mapper.Map<TCabeceraFurConfiguracionAutomatica>(entidad);

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

        public TCabeceraFurConfiguracionAutomatica Add(CabeceraFurConfiguracionAutomatica entidad)
        {
            try
            {
                var CabeceraFurConfiguracionAutomatica = MapeoEntidad(entidad);
                base.Insert(CabeceraFurConfiguracionAutomatica);
                return CabeceraFurConfiguracionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCabeceraFurConfiguracionAutomatica Update(CabeceraFurConfiguracionAutomatica entidad)
        {
            try
            {
                var CabeceraFurConfiguracionAutomatica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CabeceraFurConfiguracionAutomatica.RowVersion = entidadExistente.RowVersion;

                base.Update(CabeceraFurConfiguracionAutomatica);
                return CabeceraFurConfiguracionAutomatica;
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


        public IEnumerable<TCabeceraFurConfiguracionAutomatica> Add(IEnumerable<CabeceraFurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                List<TCabeceraFurConfiguracionAutomatica> listado = new List<TCabeceraFurConfiguracionAutomatica>();
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

        public IEnumerable<TCabeceraFurConfiguracionAutomatica> Update(IEnumerable<CabeceraFurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCabeceraFurConfiguracionAutomatica> listado = new List<TCabeceraFurConfiguracionAutomatica>();
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


        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por id
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public CabeceraFurConfiguracionAutomatica ObtenerCabeceraFurConfiguracionAutomaticaById(int Id)
        {
            try
            {
                CabeceraFurConfiguracionAutomatica rpta = new CabeceraFurConfiguracionAutomatica();
                var query = @"SELECT * FROM fin.T_CabeceraFurConfiguracionAutomatica
                            WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new {Id= Id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    rpta = JsonConvert.DeserializeObject<CabeceraFurConfiguracionAutomatica>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por idArea que esta en proceso
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public bool ValidarCabeceraFurConfiguracionAutomaticaEnProcesoByIdArea(int IdArea)
        {
            try
            {
                var query = @"SELECT * FROM fin.T_CabeceraFurConfiguracionAutomatica
                            WHERE Estado=1 and IdArea=@IdArea and (IdEstadoProyeccionFur=2 or IdEstadoProyeccionFur=1 )";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdArea = IdArea });
                if (!string.IsNullOrEmpty(resultado) && resultado!="null") return false; 
                else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros que se van a proyectar para realizar el congelamiento
        /// </summary>
        /// <returns> CabeceraFurConfiguracionAutomatica </returns>
        public List<DetalleProyeccionFur> ObtenerCabeceraFurConfiguracionAutomaticaEnRevisionByIdAreas(string IdAreas)
        {
            try
            {
                List<DetalleProyeccionFur> rpta = new List<DetalleProyeccionFur>();
                var query = @"SELECT Id AS IdCabeceraConfiguracion, IdArea FROM fin.T_CabeceraFurConfiguracionAutomatica
                              WHERE Estado = 1 AND IdEstadoProyeccionFur = 2 AND IdArea in (select item from conf.F_Splitstring(@IdAreas,','))";
                var resultado = _dapperRepository.QueryDapper(query, new { IdAreas = IdAreas });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetalleProyeccionFur>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CabeceraFurConfiguracionAutomatica
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<CabeceraFurConfiguracionAutomaticaDTO> ObtenerCabeceraFurConfiguracionAutomatica(FiltroBusquedaCabeceraFCADTO filtro)
        {
            try
            {
                List<CabeceraFurConfiguracionAutomaticaDTO> rpta = new List<CabeceraFurConfiguracionAutomaticaDTO>();

                var condiciones = " WHERE Id is not null ";
                if(filtro.IdArea!=null && filtro.IdArea != 0) condiciones += " AND IdArea=@IdArea ";
                if(filtro.IdEstadoSolicitud != null && filtro.IdEstadoSolicitud != 0) condiciones += " AND IdEstadoProyeccionFur=@IdEstadoProyeccionFur ";
               
                var query = @"SELECT [Id]
                                  ,[Nombre]
                                  ,[Codigo]
                                  ,[IdConfiguracionProyeccionFur]   
                                  ,[IdPeriodoProyeccion]
                                  ,[IdEstadoProyeccionFur]
                                  ,[IdArea]
                                  ,[Observacion]
                              FROM [fin].[V_ObtenerCabeceraFurConfiguracionAutomatica] " + condiciones + " ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdArea=filtro.IdArea, IdEstadoProyeccionFur = filtro.IdEstadoSolicitud });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CabeceraFurConfiguracionAutomaticaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// cONGELA LA CONFIGURACION Y DETALLE de proyeccion , cambia el estado a proyectado
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public bool CongelarProyeccionYCambiarEstadoProyeccionFur(string data, string configuracion, string Usuario)
        {
            try
            {
                var query = @"[fin].[SP_CongelarProyeccionYCambiarEstadoProyeccionFur]";
                var resultado = _dapperRepository.QuerySPDapper(query, new { Json = data, Configuracion = configuracion, Usuario = Usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Prepara el detalle de la configuracion para una nueva proyeccion.
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public bool PrepararConfiguracionFurProyeccion(int IdConfiguracion, int IdArea, string Usuario)
        {
            try
            {
                var query = @"[fin].[SP_PrepararConfiguracionFurProyeccion]";
                var resultado = _dapperRepository.QuerySPDapper(query, new { 
                    IdConfiguracion = IdConfiguracion, 
                    IdArea = IdArea, 
                    Usuario = Usuario 
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
