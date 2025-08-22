using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    /// Repositorio: CampaniaGeneralDetalleRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralDetalle
    /// </summary>
    public class CampaniaGeneralDetalleRepository : GenericRepository<TCampaniaGeneralDetalle>, ICampaniaGeneralDetalleRepository
    {
        private Mapper _mapper;

        public CampaniaGeneralDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneralDetalle, CampaniaGeneralDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<TCampaniaGeneralDetalle> Add(IEnumerable<CampaniaGeneralDetalle> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneralDetalle> listado = new List<TCampaniaGeneralDetalle>();
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

        public IEnumerable<TCampaniaGeneralDetalle> Update(IEnumerable<CampaniaGeneralDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneralDetalle> listado = new List<TCampaniaGeneralDetalle>();
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


        #region Metodos Base
        private TCampaniaGeneralDetalle MapeoEntidad(CampaniaGeneralDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneralDetalle modelo = _mapper.Map<TCampaniaGeneralDetalle>(entidad);

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

        public TCampaniaGeneralDetalle Add(CampaniaGeneralDetalle entidad)
        {
            try
            {
                var CampaniaGeneralDetalle = MapeoEntidad(entidad);
                Insert(CampaniaGeneralDetalle);
                return CampaniaGeneralDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneralDetalle Update(CampaniaGeneralDetalle entidad)
        {
            try
            {
                var CampaniaGeneralDetalle = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CampaniaGeneralDetalle.RowVersion = entidadExistente.RowVersion;

                Update(CampaniaGeneralDetalle);
                return CampaniaGeneralDetalle;
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

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneralDetalle para la tabla filtro Mailing
        /// </summary>
        /// <returns> List<CampaniaGeneralDetalleDTO> </returns>
        public IEnumerable<CampaniaGeneralDetalleDTO> ObtenerCampaniaGeneralDetalle()
        {
            try
            {
                List<CampaniaGeneralDetalleDTO> rpta = new List<CampaniaGeneralDetalleDTO>();
                var query = @"select Id, Nombre, Prioridad, CantidadContactosMailing, UrlFormulario from mkt.T_CampaniaGeneralDetalle where estado = 1 order by FechaCreacion desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Eliminarrelacion(List<TCampaniaGeneralDetalleArea> listaBorrar, List<int> nuevos, string usuario)
        {
            try
            {
                foreach (var item in listaBorrar)
                {
                    Delete(item.Id, usuario);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<TCampaniaGeneralDetalle> ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(int IdcampaniaGeneral)
        {
            try
            {
                var datos = base.GetBy(x => x.IdCampaniaGeneral == IdcampaniaGeneral).ToList();
                return datos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public CampaniaGeneralDTO ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerlaCompleta(int IdcampaniaGeneral)
        {
            CampaniaGeneralDTO campania = new CampaniaGeneralDTO();
            string sql = @"select*from mkt.V_ObtenerCampaniaGeneralDetallePrioridades
                WHERE IdCampaniageneral = @IdcampaniaGeneral ";

            var respuesta = _dapperRepository.QueryDapper(sql, new
            {
                @IdcampaniaGeneral
            });
            if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
            {
                var res = JsonConvert.DeserializeObject<List<CmapnaiGeneralDetalleCompleto>>(respuesta);
                campania = res.GroupBy(x => new
                {
                    x.IdCampaniageneral,
                    x.Nombre,
                    x.IdCategoriaOrigen,
                    x.IdCategoriaObjetoFiltro,
                    x.FechaEnvio,
                    x.IdHoraEnvio_Mailing,
                    x.IdTipoAsociacion,
                    x.IdProbabilidadRegistro_PW,
                    x.NroMaximoSegmentos,
                    x.CantidadPeriodoSinCorreo,
                    x.IdTiempoFrecuencia,
                    x.IdFiltroSegmento,
                    x.IdPlantilla_Mailing,
                    x.IncluyeWhatsapp,
                    x.IdRemitenteMailing,
                    x.FechaInicioEnvioWhatsapp,
                    x.FechaFinEnvioWhatsapp,
                    x.NumeroMinutosPrimerEnvio,
                    x.IdHoraEnvio_Whatsapp,
                    x.DiasSinWhatsapp,
                    x.IdPlantilla_Whatsapp,
                    x.IncluirRebotes,
                    x.IdEstadoEnvio_Mailing,
                    x.IdEstadoEnvio_Whatsapp
                }).Select(g=> new CampaniaGeneralDTO
                {
                    Id=g.Key.IdCampaniageneral,
                    Nombre=g.Key.Nombre,
                    IdCategoriaOrigen=g.Key.IdCategoriaOrigen,
                    IdCategoriaObjetoFiltro=g.Key.IdCategoriaObjetoFiltro,
                    FechaEnvio=g.Key.FechaEnvio,
                    IdHoraEnvio_Mailing=g.Key.IdHoraEnvio_Mailing,
                    IdTipoAsociacion=g.Key.IdTipoAsociacion,
                    IdProbabilidadRegistroPw = g.Key.IdProbabilidadRegistro_PW,
                    NroMaximoSegmentos=g.Key.NroMaximoSegmentos,
                    CantidadPeriodoSinCorreo=g.Key.CantidadPeriodoSinCorreo,
                    IdTiempoFrecuencia=g.Key.IdTiempoFrecuencia,
                    IdFiltroSegmento=g.Key.IdFiltroSegmento,
                    IdPlantilla_Mailing=g.Key.IdPlantilla_Mailing,
                    IncluyeWhatsapp=g.Key.IncluyeWhatsapp,
                    IdRemitenteMailing=g.Key.IdRemitenteMailing,
                    FechaInicioEnvioWhatsapp=g.Key.FechaInicioEnvioWhatsapp,
                    FechaFinEnvioWhatsapp=g.Key.FechaFinEnvioWhatsapp,
                    NumeroMinutosPrimerEnvio=g.Key.NumeroMinutosPrimerEnvio,
                    IdHoraEnvio_Whatsapp=g.Key.IdHoraEnvio_Whatsapp,
                    DiasSinWhatsapp=g.Key.DiasSinWhatsapp,
                    IdPlantilla_Whatsapp=g.Key.IdPlantilla_Whatsapp,
                    ListaPrioridades= res.Where(i=> i.IdCampaniageneral == g.Key.IdCampaniageneral).GroupBy(r => new
                    {
                        r.Asunto,
                        r.EnEjecucion,
                        r.CantidadContactosMailing,
                        r.CantidadContactosWhatsapp,
                        r.idCampaniaGeneralDetalle,
                        r.IdCampaniageneral,
                        r.IdCentroCosto,
                        r.NoIncluyeWhatsaap,
                        r.IdConjuntoAnuncio,
                        r.IdPersonal,
                        r.urlFormulario,
                        r.NombreCampaniGeneralDetalle,
                        r.Prioridad
                    }).Select(x=> new PrioridadesCampaniaGeneralDetalleDTO
                    {
                        Asunto=x.Key.Asunto,
                        EnEjecucion=x.Key.EnEjecucion,
                        CantidadContactosMailing=x.Key.CantidadContactosMailing,
                        CantidadContactosWhatsapp=x.Key.CantidadContactosWhatsapp,
                        Id=Convert.ToInt32(x.Key.idCampaniaGeneralDetalle),
                        IdCampaniaGeneral=x.Key.IdCampaniageneral,
                        IdCentroCosto=x.Key.IdCentroCosto,
                        CantidadSubidosMailChimp=x.Key.CantidadContactosMailing,
                        NoIncluyeWhatsaap=x.Key.NoIncluyeWhatsaap,
                        IdConjuntoAnuncio=x.Key.IdConjuntoAnuncio,
                        IdPersonal=Convert.ToInt32(x.Key.IdPersonal),
                        Nombre= x.Key.NombreCampaniGeneralDetalle,
                        UrlFormulario=x.Key.urlFormulario,
                        Prioridad=Convert.ToInt32(x.Key.Prioridad),
                        Areas=res.Where(i=> i.IdCampaniageneral == g.Key.IdCampaniageneral && i.idCampaniaGeneralDetalle==x.Key.idCampaniaGeneralDetalle && i.IdAreaCapacitacion!=null).GroupBy(c=> new { c.IdAreaCapacitacion }).Select(f=>Convert.ToInt32(f.Key.IdAreaCapacitacion)).ToList(),
                        ProgramasFiltro=res.Where(i=> i.IdCampaniageneral == g.Key.IdCampaniageneral && i.idCampaniaGeneralDetalle == x.Key.idCampaniaGeneralDetalle && i.IdPrograma!=null).GroupBy(c=> new { 
                            c.Orden,
                            c.NombreProgramaGeneral,
                            c.IdPGeneral,
                            c.IdCampaniaGeneralDetallePrograma,
                            c.IdPrograma,
                        }).Select(f=>new CampaniaGeneralDetalleProgramaDTO
                        {
                            Id=f.Key.IdPrograma,
                            IdPgeneral=f.Key.IdPGeneral,
                            NombreProgramaGeneral=f.Key.NombreProgramaGeneral,
                            IdCampaniaGeneralDetalle=f.Key.IdCampaniaGeneralDetallePrograma,
                            Orden=f.Key.Orden
                        }).ToList(),
                        Responsables= res.Where(i => i.IdCampaniageneral == g.Key.IdCampaniageneral && i.idCampaniaGeneralDetalle == x.Key.idCampaniaGeneralDetalle && i.IdResponsable != null).GroupBy(c => new { c.IdResponsable }).Select(f => new CampaniaGeneralDetalleResponsableDTO{
                            IdResponsable=Convert.ToInt32(f.Key.IdResponsable) }
                        ).ToList(),
                        SubAreas =res.Where(i=> i.IdCampaniageneral == g.Key.IdCampaniageneral && i.idCampaniaGeneralDetalle == x.Key.idCampaniaGeneralDetalle && i.IdSubArea!=null).GroupBy(c=> new { c.IdSubArea }).Select(f=>Convert.ToInt32(f.Key.IdSubArea)).ToList(),
                    }).ToList(),
                }).FirstOrDefault();
            }
            return campania;
        }
        public List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn> ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(int idCampaniaGeneralDetalle, int idCamapniaGeneral)
        {
            List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn> retornar = new List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn>();
            string sql = "SELECT DISTINCT " +
                "mkt.T_CampaniaGeneralDetalleArea.IdAreaCapacitacion, " +
                "mkt.T_CampaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle AS 'IdCampaniaGeneralDetalle', " +
                "mkt.T_CampaniaGeneralDetalleSubArea.IdSubAreaCapacitacion, " +
                "mkt.T_CampaniaGeneralDetallePrograma.IdPGeneral, " +
                "mkt.T_CampaniaGeneralDetalle.IdCampaniaGeneral, " +
                "mkt.T_CampaniaGeneralDetalle.Nombre, " +
                "mkt.T_CampaniaGeneralDetalle.Prioridad, " +
                "mkt.T_CampaniaGeneralDetalle.IdCentroCosto, " +
                "mkt.T_CampaniaGeneralDetalle.CantidadContactosMailing, " +
                "mkt.T_CampaniaGeneralDetalle.CantidadContactosWhatsapp, " +
                "mkt.T_CampaniaGeneralDetalle.IdPersonal, " +
                "mkt.T_CampaniaGeneralDetalle.NoIncluyeWhatsaap, " +
                "mkt.T_CampaniaGeneralDetalle.Estado, " +
                "mkt.T_CampaniaGeneralDetalle.urlFormulario, " +
                "mkt.T_CampaniaGeneralDetalle.EnEjecucion, " +
                "mkt.T_CampaniaGeneralDetalle.Id," +
                "mkt.T_CampaniaGeneralDetallePrograma.NombreProgramaGeneral, " +
                "mkt.T_CampaniaGeneralDetallePrograma.Orden " +
                "FROM mkt.T_CampaniaGeneralDetalle " +
                "INNER JOIN mkt.T_CampaniaGeneralDetalleArea ON mkt.T_CampaniaGeneralDetalle.Id = mkt.T_CampaniaGeneralDetalleArea.IdCampaniaGeneralDetalle " +
                "LEFT OUTER JOIN mkt.T_CampaniaGeneralDetallePrograma ON mkt.T_CampaniaGeneralDetalle.Id = mkt.T_CampaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle " +
                "LEFT OUTER JOIN mkt.T_CampaniaGeneralDetalleSubArea ON mkt.T_CampaniaGeneralDetalle.Id = mkt.T_CampaniaGeneralDetalleSubArea.IdCampaniaGeneralDetalle " +
                "WHERE (mkt.T_CampaniaGeneralDetalle.Estado = 1) " +
                "AND (mkt.T_CampaniaGeneralDetalle.IdCampaniaGeneral = @idCamapniaGeneral) " +
                "AND (mkt.T_CampaniaGeneralDetalle.Id = @idCampaniaGeneralDetalle)";
            var respuesta = _dapperRepository.QueryDapper(sql, new
            {
                @idCamapniaGeneral,
                @idCampaniaGeneralDetalle
            });
            if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
            {
                var ret = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleAreaSubAreaPrograma>>(respuesta);
                retornar = ret.GroupBy(x => new
                {
                    x.IdCampaniaGeneral,
                    x.Nombre,
                    x.Prioridad,
                    x.IdCentroCosto,
                    x.CantidadContactosMailing,
                    x.CantidadContactosWhatsapp,
                    x.IdPersonal,
                    x.NoIncluyeWhatsaap,
                    x.Estado,
                    x.urlFormulario,
                    x.EnEjecucion,
                    x.Id
                }).Select(g => new CampaniaGeneralDetalleAreaSubAreaProgramaReturn
                {

                    IdCampaniaGeneral = g.Key.IdCampaniaGeneral,
                    Nombre = g.Key.Nombre,
                    Prioridad = g.Key.Prioridad,
                    IdCentroCosto = g.Key.IdCentroCosto,
                    CantidadContactosMailing = g.Key.CantidadContactosMailing,
                    CantidadContactosWhatsapp = g.Key.CantidadContactosWhatsapp,
                    IdPersonal = g.Key.IdPersonal,
                    NoIncluyeWhatsaap = g.Key.NoIncluyeWhatsaap,
                    Estado = g.Key.Estado,
                    UrlFormulario = g.Key.urlFormulario,
                    EnEjecucion = g.Key.EnEjecucion,
                    Id = g.Key.Id,
                    IdAreaCapacitacion = ret.Where(x => g.Key.Id == x.Id).GroupBy(x=> new
                    {
                        x.IdAreaCapacitacion
                    }).Select(j => j.Key.IdAreaCapacitacion).ToList(),
                    IdSubAreaCapacitacion = ret.Where(x => g.Key.Id == x.Id).GroupBy(x => new
                    {
                        x.IdSubAreaCapacitacion
                    }).Select(j => j.Key.IdSubAreaCapacitacion).ToList(),
                    IdCampaniaGeneralDetallePrograma = ret.Where(x => g.Key.Id == x.Id).GroupBy(x=> new
                    {
                        x.IdPGeneral,
                        x.IdCampaniaGeneralDetalle,
                        x.NombreProgramaGeneral,
                        x.Orden,
                    }).Select(j => new ProgramaGeneralReturnSqlList
                    {
                        idPgeneral=j.Key.IdPGeneral,
                        id= j.Key.IdCampaniaGeneralDetalle,
                        nombreProgramaGeneral = j.Key.NombreProgramaGeneral,
                        orden = j.Key.Orden,
                        idCampaniaGeneralDetalle = j.Key.IdCampaniaGeneralDetalle
                    }).ToList()
                }).ToList();
            }
            return retornar;
        }
        /// <summary> 
        /// Actualiza el estado EnEjecucion de la campania general detalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="flagEjecucion">Booleano para determinar si pasar a estado de ejecucion o reposo</param>
        /// <param name="usuario">Usuario que realiza la actualizacion</param>
        public void ActualizarEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, bool flagEjecucion, string usuario)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarEstadoEjecucionCampaniaGeneralDetalle]";
                var query = _dapperRepository.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, EnEjecucion = flagEjecucion, UsuarioModificacion = usuario });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AgregarUrl(string uri, int id,int count)
        {
            try
            {
                string sql = "UPDATE [mkt].[T_CampaniaGeneralDetalle] SET UrlFormulario = @uri, CantidadContactosMailing = @count where id= @id";
                var respuesta = _dapperRepository.FirstOrDefault(sql, new
                { uri, count, id });
            }catch(Exception e) { 
                throw e; 
            }
        }

        public void AgregarCantidad(int id, int count)
        {
            try
            {
                string sql = "UPDATE [mkt].[T_CampaniaGeneralDetalle] SET  CantidadContactosWhatsapp = @count where id= @id";
                var respuesta = _dapperRepository.FirstOrDefault(sql, new
                { count, id });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int AddSqlString(CampaniaGeneralDetalle entidad)
        {
            var CampaniaGeneralDetalle = MapeoEntidad(entidad);
            string sql = "INSERT INTO [mkt].[T_CampaniaGeneralDetalle] "+
           "([IdCampaniaGeneral] "+
           ",[Nombre] " +
           ",[Prioridad] " +
           ",[Asunto] " +
           ",[IdPersonal] " +
           ",[IdCentroCosto] " +
          " ,[CantidadContactosMailing] " +
           ",[CantidadContactosWhatsapp] " +
           ",[NoIncluyeWhatsaap] " +
           ",[UrlFormulario] " +
           ",[Estado] " +
           ",[UsuarioCreacion] " +
           ",[UsuarioModificacion] " +
           ",[FechaCreacion] " +
           ",[FechaModificacion] " +
           ",[IdConjuntoAnuncio] " +
           ",[EnEjecucion]) " +
                "VALUES " +
                "(@IdCampaniaGeneral " +
           ",@Nombre " +
           ",@Prioridad " +
           ",@Asunto " +
           ",@IdPersonal " +
           ",@IdCentroCosto " +
          " ,@CantidadContactosMailing " +
           ",@CantidadContactosWhatsapp " +
           ",@NoIncluyeWhatsaap " +
           ",@UrlFormulario " +
           ",@Estado " +
           ",@UsuarioCreacion " +
           ",@UsuarioModificacion " +
           ",GetDate() " +
           ",GetDate() " +
           ",@IdConjuntoAnuncio " +
           ",@EnEjecucion) SELECT SCOPE_IDENTITY() as ID";
            var respuesta = _dapperRepository.FirstOrDefault(sql, new
            {
                CampaniaGeneralDetalle.IdCampaniaGeneral,
                CampaniaGeneralDetalle.Nombre,
                CampaniaGeneralDetalle.Prioridad,
                CampaniaGeneralDetalle.Asunto,
                CampaniaGeneralDetalle.IdPersonal,
                CampaniaGeneralDetalle.IdCentroCosto,
                CampaniaGeneralDetalle.CantidadContactosMailing,
                CampaniaGeneralDetalle.CantidadContactosWhatsapp,
                CampaniaGeneralDetalle.NoIncluyeWhatsaap,
                CampaniaGeneralDetalle.UrlFormulario,
                CampaniaGeneralDetalle.Estado,
                CampaniaGeneralDetalle.UsuarioCreacion,
                CampaniaGeneralDetalle.UsuarioModificacion,
                CampaniaGeneralDetalle.IdConjuntoAnuncio,
                CampaniaGeneralDetalle.EnEjecucion
            });

            if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
            {
                var map = JsonConvert.DeserializeObject<MapeoParaConsultaSqlInsertId>(respuesta);
                return Convert.ToInt32(map.Id.Split(".")[0]);
            }
            return 0;
        }
        /// <summary>
        /// Obtiene las plantillas y la información del Asesor
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <param name="contexto">Contexto padre para evitar problemas de multiples transacciones</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleBO</returns>
        public CampaniaGeneralDetalle BuscarCampaniaGeneralDetallePorId(int idCampaniaGeneralDetalle)
        {
            var campaniaGeneralDetalleResultado = new CampaniaGeneralDetalle();
            var spBusqueda = "[mkt].[SP_BuscarCampaniaGeneralDetallePorId]";
            var resultadoSp = _dapperRepository.QuerySPFirstOrDefault(spBusqueda, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
            if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]") && resultadoSp != "null")
            {
                campaniaGeneralDetalleResultado = JsonConvert.DeserializeObject<CampaniaGeneralDetalle>(resultadoSp);
            }

            return campaniaGeneralDetalleResultado;
        }
        public List<AlumnoInformacionBasicaDTO> ObtenerAlumnosPorCampaniaGeneralDetalle(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<AlumnoInformacionBasicaDTO> alumnoResultado = new List<AlumnoInformacionBasicaDTO>();

                var spObtenerAlumnos = "[mkt].[SP_ObtenerAlumnosPorCampaniaGeneralDetalle]";
                var resultadoSp = _dapperRepository.QuerySPDapper(spObtenerAlumnos, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultadoSp) && !resultadoSp.Contains("[]"))
                {
                    alumnoResultado = JsonConvert.DeserializeObject<List<AlumnoInformacionBasicaDTO>>(resultadoSp);
                }

                return alumnoResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

