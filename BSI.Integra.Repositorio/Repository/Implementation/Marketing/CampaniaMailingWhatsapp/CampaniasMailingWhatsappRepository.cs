using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniasMailingWhatsappRepository : GenericRepository<TCampaniaMailingDetalle>, ICampaniasMailingWhatsappRepository
    {
        public CampaniasMailingWhatsappRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        public List<CampaniaMailingDTO> ObtenerListaCampaniaMailing()
        {
            try
            {
                string query = @"SELECT Id, Nombre, PrincipalValor, PrincipalValorTiempo, SecundarioValor, SecundarioValorTiempo, ActivaValor, ActivaValorTiempo, IdCategoriaOrigen, FechaCreacion, FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal, FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal
                            FROM mkt.V_TCampaniaMailing_DatosCampania
                            WHERE Estado = 1; ";
                var respuestaQuery = _dapperRepository.QueryDapper(query, null);
                List<CampaniaMailingDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<CampaniaMailingDTO>>(respuestaQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CategoriaOrigenFiltroDTO> ObtenerListaCategoriaOrigen()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_Nombre WHERE Estado = 1";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                List<CategoriaOrigenFiltroDTO> listaCateogriaOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(responseQuery);
                return listaCateogriaOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CampaniaMailingValorTipoDTO> ObtenerPorIdCampaniaMailing(int idCampaniaMailing)
        {
            try
            {
                return null;
                //return this.GetBy(x => x.IdCampaniaMailing == idCampaniaMailing && x.Estado == true, x => new CampaniaMailingValorTipoDTO { Id = x.Id, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro, Valor = x.Valor }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CategoriaOrigenFiltroDTO> ObtenerListaCampaniaMailingDetalle(int Id)
        {
            try
            {
                string query = "SELECT Id, Nombre FROM mkt.V_TCategoriaOrigen_Nombre WHERE Estado = 1";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                List<CategoriaOrigenFiltroDTO> listaCateogriaOrigen = JsonConvert.DeserializeObject<List<CategoriaOrigenFiltroDTO>>(responseQuery);
                return listaCateogriaOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor()
        {
            try
            {
                string query = "SELECT IdRemitenteMailing, IdPersonal, NombreCompleto FROM mkt.V_TRemitenteMailingAsesor_NombreCompleto";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                List<RemitenteMailingAsesorDTO> listaRemitenteMailingAsesor = JsonConvert.DeserializeObject<List<RemitenteMailingAsesorDTO>>(responseQuery);
                return listaRemitenteMailingAsesor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
		/// Obtiene lista de centro de costos para filtro de formulario
		/// </summary>
		/// <returns>Lista de objetos de clase FiltroDTO</returns>
        public List<FiltroDTO> ObtenerParaFiltro()
        {
            try
            {
                string _queryCentroCosto = "SELECT Id, Nombre FROM pla.V_ObtenerCentroCostoParaFiltro WHERE Estado = 1";
                var queryCentroCosto = _dapperRepository.QueryDapper(_queryCentroCosto, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Carlos Crispin R.
        ///Fecha: 17/03/2021
        /// <summary>
        /// Obtiene Personal responable para marketing por Filtro
        /// </summary>
        /// <returns>Lista de Personal de Marketing</returns>
        public List<FiltroCombosDTO> ObtenerPersonalMarketingFiltro()
        {
            try
            {
                return null;
                //return GetBy(x => x.Estado == true && x.Activo == true && x.Rol == "Marketing", x => new FiltroCombosDTO { Id = x.Id, Nombre = string.Concat(x.Nombres, " ", x.Apellidos) }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Repositorio: ProbabilidadRegistroPwRepositorio
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// <summary>
        /// Obtiene información para combo box
        /// </summary>
        /// <param></param>
        /// <returns> Lista de ObjetosDTO: List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ComboDTO> probabilidadesRegistro = new List<ComboDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_TProbabilidadRegistro_ParaFiltro WHERE estado = 1";
                var probabilidadRegistroPwDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(probabilidadRegistroPwDB) && !probabilidadRegistroPwDB.Contains("[]"))
                {
                    probabilidadesRegistro = JsonConvert.DeserializeObject<List<ComboDTO>>(probabilidadRegistroPwDB);
                }
                return probabilidadesRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los registros de CampaniaMailingDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaMailing">Id de la campania mailing (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>Lista de objetos de clase PrioridadesDTO</returns>
        public List<PrioridadesDTO> ObtenerListaCampaniaMailingDetalleConProgramas(int idCampaniaMailing)
        {
            try
            {
                string query = @"
                  SELECT Id, 
                       Prioridad, 
                       Tipo, 
                       IdRemitenteMailing, 
                       IdPersonal, 
                       Subject, 
                       FechaEnvio, 
                       IdHoraEnvio, 
                       Proveedor, 
                       EstadoEnvio, 
                       IdFiltroSegmento, 
                       IdPlantilla, 
                       IdConjuntoAnuncio, 
                       Campania, 
                       CodMailing, 
                       CantidadContactos, 
                       RowVersion, 
                       IdCampaniaMailingDetallePrograma, 
                       IdPGeneral, 
                       Nombre, 
                       TipoPrograma, 
                       IdArea, 
                       IdSubArea, 
                       CantidadSubidosMailChimp,
                       IdCentroCosto
                FROM mkt.V_ObtenerCampaniaMailingDetalle
                WHERE EstadoCampaniaMailingDetalle = 1
                      AND IdCampaniaMailing = @idCampaniaMailing
                      AND (EstadoCampaniaMailingDetallePrograma IS NULL
                           OR EstadoCampaniaMailingDetallePrograma = 1)
                      AND (EstadoArea IS NULL
                           OR EstadoArea = 1)
                      AND (EstadoSubArea IS NULL
                           OR EstadoSubArea = 1)
                ORDER BY Id;
                ";
                var respuestaQuery = _dapperRepository.QueryDapper(query, new { idCampaniaMailing });
                IEnumerable<CampaniaMailingDetalleConProgramasDTO> listaCampaniaMailingDetalle = JsonConvert.DeserializeObject<List<CampaniaMailingDetalleConProgramasDTO>>(respuestaQuery);

                var listaPrioridades = (from p in listaCampaniaMailingDetalle
                                        group p by new
                                        {
                                            p.Id,
                                            p.Prioridad,
                                            p.Tipo,
                                            p.IdRemitenteMailing,
                                            p.IdPersonal,
                                            p.Subject,
                                            p.Campania,
                                            p.CodMailing,
                                            p.IdConjuntoAnuncio,
                                            p.FechaEnvio,
                                            p.IdHoraEnvio,
                                            p.Proveedor,
                                            p.IdFiltroSegmento,
                                            p.IdPlantilla,
                                            p.EstadoEnvio,
                                            p.CantidadContactos,
                                            p.CantidadSubidosMailChimp,
                                            p.IdCentroCosto
                                        } into g
                                        select new PrioridadesDTO
                                        {
                                            Id = g.Key.Id,
                                            Prioridad = g.Key.Prioridad,
                                            Tipo = g.Key.Tipo,
                                            IdRemitenteMailing = g.Key.IdRemitenteMailing,
                                            IdPersonal = g.Key.IdPersonal,
                                            Subject = g.Key.Subject,
                                            Campania = g.Key.Campania,
                                            CodMailing = g.Key.CodMailing,
                                            IdConjuntoAnuncio = g.Key.IdConjuntoAnuncio,
                                            FechaEnvio = g.Key.FechaEnvio,
                                            IdHoraEnvio = g.Key.IdHoraEnvio,
                                            Proveedor = g.Key.Proveedor,
                                            EstadoEnvio = g.Key.EstadoEnvio,
                                            IdFiltroSegmento = g.Key.IdFiltroSegmento,
                                            IdPlantilla = g.Key.IdPlantilla,
                                            CantidadContactos = g.Key.CantidadContactos,
                                            CantidadSubidosMailChimp = g.Key.CantidadSubidosMailChimp,
                                            IdCentroCosto = g.Key.IdCentroCosto,
                                            ProgramasPrincipales = g.Where(o => o.TipoPrograma == "Principales").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            ProgramasSecundarios = g.Where(o => o.TipoPrograma == "Secundarios").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            ProgramasFiltro = g.Where(o => o.TipoPrograma == "Filtro").Select(o => new CampaniaMailingDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaMailingDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                Nombre = o.Nombre,
                                                Tipo = o.TipoPrograma
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                            Areas = g.Where(x => x.IdArea != null).Select(o => o.IdArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            SubAreas = g.Where(x => x.IdSubArea != null).Select(o => o.IdSubArea.Value).GroupBy(i => i).Select(i => i.First()).ToList()

                                        }).ToList();

                return listaPrioridades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
