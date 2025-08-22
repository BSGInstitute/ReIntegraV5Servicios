using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AsignacionAutomaticaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomatica
    /// </summary>
    public class AsignacionAutomaticaRepository : GenericRepository<TAsignacionAutomatica>, IAsignacionAutomaticaRepository
    {
        private Mapper _mapper;

        public AsignacionAutomaticaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomatica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAsignacionAutomatica MapeoEntidad(AsignacionAutomatica entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomatica modelo = _mapper.Map<TAsignacionAutomatica>(entidad);

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

        public TAsignacionAutomatica Add(AsignacionAutomatica entidad)
        {
            try
            {
                var AsignacionAutomatica = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomatica);
                return AsignacionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionAutomatica Update(AsignacionAutomatica entidad)
        {
            try
            {
                var AsignacionAutomatica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomatica.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomatica);
                return AsignacionAutomatica;
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


        public IEnumerable<TAsignacionAutomatica> Add(IEnumerable<AsignacionAutomatica> listadoEntidad)
        {
            try
            {
                List<TAsignacionAutomatica> listado = new List<TAsignacionAutomatica>();
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

        public IEnumerable<TAsignacionAutomatica> Update(IEnumerable<AsignacionAutomatica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionAutomatica> listado = new List<TAsignacionAutomatica>();
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

        public AsignacionAutomatica ObtenerPorId(int idAsignacionAutomatica)
        {
            try
            {
                AsignacionAutomatica asignacionAutomatica = new AsignacionAutomatica();
                var query = @"
                                SELECT
                                    Id,
                                    Nombre1,
                                    Nombre2,
                                    ApellidoPaterno,
                                    ApellidoMaterno,
                                    Telefono,
                                    Celular,
                                    Email,
                                    IdCentroCosto,
                                    NombrePrograma,
                                    IdTipoDato,
                                    IdOrigen,
                                    IdFaseOportunidad,
                                    IdAreaFormacion,
                                    IdAreaTrabajo,
                                    IdIndustria,
                                    IdCargo,
                                    IdPais,
                                    IdCiudad,
                                    Validado,
                                    Corregido,
                                    OrigenCampania,
                                    IdConjuntoAnuncio,
                                    IdCategoriaOrigen,
                                    IdAsignacionAutomaticaOrigen,
                                    IdCampaniaScoring,
                                    FechaRegistroCampania,
                                    IdFaseOportunidadPortal,
                                    IdOportunidad,
                                    IdPersonal,
                                    IdTiempoCapacitacion,
                                    IdCategoriaDato,
                                    IdTipoInteraccion,
                                    IdSubCategoriaDato,
                                    IdInteraccionFormulario,
                                    UrlOrigen,
                                    ProbabilidadActual,
                                    ProbabilidadActualDesc,
                                    IdPagina,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    FechaProgramada,
                                    IdAlumno,
                                    IdAnuncioFacebook,
                                    IdAsignacionAutomaticaTemp,
                                    AptoProcesamiento
                            FROM mkt.T_AsignacionAutomatica  
                            WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAsignacionAutomatica });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    asignacionAutomatica = JsonConvert.DeserializeObject<AsignacionAutomatica>(resultado)!;
                }
                return asignacionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomatica ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            try
            {
                AsignacionAutomatica asignacionAutomatica = new AsignacionAutomatica();
                var query = @"
                                SELECT
                                    Id,
                                    Nombre1,
                                    Nombre2,
                                    ApellidoPaterno,
                                    ApellidoMaterno,
                                    Telefono,
                                    Celular,
                                    Email,
                                    IdCentroCosto,
                                    NombrePrograma,
                                    IdTipoDato,
                                    IdOrigen,
                                    IdFaseOportunidad,
                                    IdAreaFormacion,
                                    IdAreaTrabajo,
                                    IdIndustria,
                                    IdCargo,
                                    IdPais,
                                    IdCiudad,
                                    Validado,
                                    Corregido,
                                    OrigenCampania,
                                    IdConjuntoAnuncio,
                                    IdCategoriaOrigen,
                                    IdAsignacionAutomaticaOrigen,
                                    IdCampaniaScoring,
                                    FechaRegistroCampania,
                                    IdFaseOportunidadPortal,
                                    IdOportunidad,
                                    IdPersonal,
                                    IdTiempoCapacitacion,
                                    IdCategoriaDato,
                                    IdTipoInteraccion,
                                    IdSubCategoriaDato,
                                    IdInteraccionFormulario,
                                    UrlOrigen,
                                    ProbabilidadActual,
                                    ProbabilidadActualDesc,
                                    IdPagina,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    FechaProgramada,
                                    IdAlumno,
                                    IdAnuncioFacebook,
                                    IdAsignacionAutomaticaTemp,
                                    AptoProcesamiento
                                    FROM mkt.T_AsignacionAutomatica  
                            WHERE Estado = 1 AND IdFaseOportunidadPortal = @idFaseOportunidadPortal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idFaseOportunidadPortal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    asignacionAutomatica = JsonConvert.DeserializeObject<AsignacionAutomatica>(resultado)!;
                }
                return asignacionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 03/22/22
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista registros importados según el filtro
        /// </summary>
        /// <param></param>
        /// <returns> IEnumerable<AsignacionAutomaticaCompuestoImportadosDTO> </returns>
        public IEnumerable<AsignacionAutomaticaCompuestoImportadosDTO> ObtenerRegistrosImportados(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
        {
            try
            {
                var filtros = new object();
                string _queryCondicion = "";
                string[] IdCategoriaOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdProbabilidad = new string[6];
                string[] IdPais = new string[6];
                string[] IdIndustria = new string[6];
                string[] IdFormacion = new string[6];
                string[] IdCargo = new string[6];
                string[] IdTrabajo = new string[6];

                DateTime FechaFin = DateTime.Parse(paginador.filtroRegistros.FechaFin);
                DateTime fechaInicio = DateTime.Parse(paginador.filtroRegistros.FechaInicio);

                if (paginador.filtroRegistros.IdCentroCosto != "")
                {
                    _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCosto ";
                    IdCentroCosto = paginador.filtroRegistros.IdCentroCosto.Split(",");
                }
                if (paginador.filtroRegistros.IdCategoriaDato != "")
                {
                    _queryCondicion = _queryCondicion + "And IdCategoria in @IdCategoriaOrigen ";
                    IdCategoriaOrigen = paginador.filtroRegistros.IdCategoriaDato.Split(",");
                }
                if (paginador.filtroRegistros.IdProbabilidad != "")
                {
                    _queryCondicion = _queryCondicion + "And IdProbabilidad in @IdProbabilidad ";
                    IdProbabilidad = paginador.filtroRegistros.IdProbabilidad.Split(",");
                }
                if (paginador.filtroRegistros.IdPais != "")
                {
                    _queryCondicion = _queryCondicion + "And IdPais in @IdPais ";
                    IdPais = paginador.filtroRegistros.IdPais.Split(",");
                }
                if (paginador.filtroRegistros.IdIndustria != "")
                {
                    _queryCondicion = _queryCondicion + "And IdIndustria in @IdIndustria ";
                    IdIndustria = paginador.filtroRegistros.IdIndustria.Split(",");
                }
                if (paginador.filtroRegistros.IdCargo != "")
                {
                    _queryCondicion = _queryCondicion + "And IdCargo in @IdCargo ";
                    IdCargo = paginador.filtroRegistros.IdCargo.Split(",");
                }
                if (paginador.filtroRegistros.IdAreaFormacion != "")
                {
                    _queryCondicion = _queryCondicion + "And IdAreaFormacion in @IdFormacion ";
                    IdFormacion = paginador.filtroRegistros.IdAreaFormacion.Split(",");
                }
                if (paginador.filtroRegistros.IdAreaTrabajo != "")
                {
                    _queryCondicion = _queryCondicion + "And IdAreaTrabajo in @IdTrabajo ";
                    IdTrabajo = paginador.filtroRegistros.IdAreaTrabajo.Split(",");
                }

                string _queryRegistro = "SELECT Alumno,Telefono,Celular,Email,CentroCosto,TipoDato,Origen,CodigoFase,AreaFormacion,AreaTrabajo,Industria,Cargo,NombrePais,NombreCiudad," +
                                        " origenCampania,FechaCreacion,ProbabilidadActual,NombreProbabilidadActual From com.V_TAsignacionAutomatica_RegistrosImportados" +
                                        " WHERE FechaCreacion Between @FechaInicio And @FechaFin And Corregido=1 and Validado=1 " + _queryCondicion + "order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                string queryRegistro = _dapperRepository.QueryDapper(_queryRegistro, new { fechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<AsignacionAutomaticaCompuestoImportadosDTO>>(queryRegistro);
                string _queryCantidad = "SELECT Count(*) From com.V_TAsignacionAutomatica_RegistrosImportados WHERE FechaCreacion Between @FechaInicio And @FechaFin And Corregido=1 and Validado=1  " + _queryCondicion + "";
                string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { fechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                if (rpta.Count() > 0)
                {
                    rpta.FirstOrDefault().TotalRegistros = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                    //foreach (var item in rpta)
                    //{
                    //    item.Email = EncriptarStringCorreo(item.Email);
                    //    item.Celular = EncriptarStringNumero(item.Email);
                    //}
                }

                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte Landing Page Portal
        /// </summary>
        /// <param Entidad="filtros">filtros/param>
        /// <returns> List<ReporteLandingPagePortalDTO> </returns>
        public List<ReporteLandingPagePortalDTO> ObtenerReporteLandingPagePortal(FiltroLandingPagePortalDTO filtros)
        {
            try
            {
                List<ReporteLandingPagePortalDTO> listadoRegistrosLandingPage = new List<ReporteLandingPagePortalDTO>();
                var _query = "";
                string registroLandingPageDB = "";
                if (!filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    //_query = @"
                    //SELECT top 1000 Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    //HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    //Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    //FROM mkt.V_ObtenerReporteLandingPagePortal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = "";
                }
                else if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    _query += @"
                    SELECT Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPagePortal WHERE Convert(Date, FechaCreacion) >= @fechaInicial and Convert(Date, FechaCreacion)  <= @fechaFinal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else if (filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    filtros.FechaFinal = DateTime.Now;

                    _query += @"
                    SELECT Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPagePortal WHERE Convert(Date, FechaCreacion) >= @fechaInicial and Convert(Date, FechaCreacion)  <= @fechaFinal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else if (!filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    string fecha = "2023-01-01";
                    DateTime dt = DateTime.Parse(fecha);
                    filtros.FechaInicial = dt;

                    _query += @"
                    SELECT Id,IdAlumno, Nombres, Apellidos, Correo1, Fijo, Movil, Formacion, Trabajo, Cargo, Industria, Pais, Region, Ip, FechaCreacion, 
                    HoraCreacion, NombrePrograma, CentroCosto, IdCentroCosto, Origen, Campanha, Proveedor, Procesado, 
                    Formulario, IdCategoriaDato, IdCampania, CategoriaDato, EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPagePortal WHERE Convert(Date, FechaCreacion) >= @fechaInicial and Convert(Date, FechaCreacion)  <= @fechaFinal ORDER BY CONVERT(DATE, FechaCreacion ) DESC ";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                if (!string.IsNullOrEmpty(registroLandingPageDB) && !registroLandingPageDB.Contains("[]"))
                {
                    listadoRegistrosLandingPage = JsonConvert.DeserializeObject<List<ReporteLandingPagePortalDTO>>(registroLandingPageDB);
                }

                foreach (var item in listadoRegistrosLandingPage)
                {
                    item.Movil = EncriptarStringNumero(item.Movil);
                    item.Correo1 = EncriptarStringCorreo(item.Correo1);
                }
                return listadoRegistrosLandingPage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte Landing Page Portal Facebook
        /// </summary>
        /// <param Entidad="filtros">filtros/param>
        /// <returns> List<ReporteLandingPagePortalDTO> </returns>


        public List<ReporteLandingPagePortalFacebookDTO> ObtenerReporteLandingPagePortalFacebook(FiltroLandingPagePortalFacebookDTO filtros)
        {
            try
            {
                List<ReporteLandingPagePortalFacebookDTO> listadoRegistrosLandingPageFacebook = new List<ReporteLandingPagePortalFacebookDTO>();
                var _query = "";
                string registroLandingPageDB = "";
                if (!filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    registroLandingPageDB = "";
                }
                else if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    _query += @"
                    SELECT Id, IdAlumno, Nombres, Correo, Movil, AreaFormacion,   Cargo,  AreaTrabajo,  Industria,  Region,  FechaRegistro,  HoraRegistro,
                    NombrePrograma,  CentroCosto,  Categoria,  Campania,  Procesado,  Formulario,  EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPageFacebook WHERE Convert(Date, FechaRegistro) >= Convert(Date,@fechaInicial) and Convert(Date, FechaRegistro)  <= Convert(Date,@fechaFinal) Order by FechaRegistro Desc";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else if (filtros.FechaInicial.HasValue && !filtros.FechaFinal.HasValue)
                {
                    filtros.FechaFinal = DateTime.Now;

                    _query += @"
                    SELECT Id, IdAlumno, Nombres, Correo, Movil, AreaFormacion,   Cargo,  AreaTrabajo,  Industria,  Region,  FechaRegistro,  HoraRegistro,
                    NombrePrograma,  CentroCosto,  Categoria,  Campania,  Procesado,  Formulario,  EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPageFacebook WHERE Convert(Date, FechaRegistro) >= Convert(Date,@fechaInicial) and Convert(Date, FechaRegistro)  <= Convert(Date,@fechaFinal) Order by FechaRegistro Desc";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                else if (!filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    string fecha = "2023-01-01";
                    DateTime dt = DateTime.Parse(fecha);
                    filtros.FechaInicial = dt;

                    _query += @"
                    SELECT Id, IdAlumno, Nombres, Correo, Movil, AreaFormacion,   Cargo,  AreaTrabajo,  Industria,  Region,  FechaRegistro,  HoraRegistro,
                    NombrePrograma,  CentroCosto,  Categoria,  Campania,  Procesado,  Formulario,  EstadoOportunidad 
                    FROM mkt.V_ObtenerReporteLandingPageFacebook WHERE Convert(Date, FechaRegistro) >= Convert(Date,@fechaInicial) and Convert(Date, FechaRegistro)  <= Convert(Date,@fechaFinal) Order by FechaRegistro Desc";
                    registroLandingPageDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = filtros.FechaInicial.Value.Date, fechaFinal = filtros.FechaFinal.Value.Date });
                }
                if (!string.IsNullOrEmpty(registroLandingPageDB) && !registroLandingPageDB.Contains("[]"))
                {
                    listadoRegistrosLandingPageFacebook = JsonConvert.DeserializeObject<List<ReporteLandingPagePortalFacebookDTO>>(registroLandingPageDB);
                }
                foreach (var item in listadoRegistrosLandingPageFacebook)
                {
                    item.Movil = EncriptarStringNumero(item.Movil);
                    item.Correo = EncriptarStringCorreo(item.Correo);
                }

                return listadoRegistrosLandingPageFacebook;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




        /// <summary>
        /// Encripta correos
        /// </summary>
        /// <param name="parametro"> Parametro </param>
        /// <returns> String </returns>
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Encripta numeros.
        /// </summary>
        /// <param name="parametro"> Parametro </param>
        /// <returns> String </returns>
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    respuesta = parametro.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 03/22/22
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista registros erroneos según el filtro
        /// </summary>
        /// <param> </param>
        /// <returns> IEnumerable<AsignacionAutomaticaCompuestoErroneosDTO> </returns>
        public IEnumerable<AsignacionAutomaticaCompuestoErroneosDTO> ObtenerRegistrosErroneos(FiltroBusquedaAsignacionAutomaticaCompuestoDTO paginador)
        {
            try
            {
                var filtros = new object();
                string _queryCondicion = "";
                string[] IdCategoriaOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdProbabilidad = new string[6];
                string[] IdPais = new string[6];
                string[] IdIndustria = new string[6];
                string[] IdFormacion = new string[6];
                string[] IdCargo = new string[6];
                string[] IdTrabajo = new string[6];
                DateTime FechaInicio = new DateTime();
                DateTime FechaFin = new DateTime();

                if (paginador.filtroRegistros != null)
                {
                    FechaFin = DateTime.Parse(paginador.filtroRegistros.FechaFin);
                    FechaInicio = DateTime.Parse(paginador.filtroRegistros.FechaInicio);

                    if (paginador.filtroRegistros.IdCentroCosto != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCentroCosto in @IdCentroCosto ";
                        IdCentroCosto = paginador.filtroRegistros.IdCentroCosto.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCategoriaDato != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCategoria in @IdCategoriaOrigen ";
                        IdCategoriaOrigen = paginador.filtroRegistros.IdCategoriaDato.Split(",");
                    }
                    if (paginador.filtroRegistros.IdProbabilidad != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdProbabilidad in @IdProbabilidad ";
                        IdProbabilidad = paginador.filtroRegistros.IdProbabilidad.Split(",");
                    }
                    if (paginador.filtroRegistros.IdPais != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdPais in @IdPais ";
                        IdPais = paginador.filtroRegistros.IdPais.Split(",");
                    }
                    if (paginador.filtroRegistros.IdIndustria != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdIndustria in @IdIndustria ";
                        IdIndustria = paginador.filtroRegistros.IdIndustria.Split(",");
                    }
                    if (paginador.filtroRegistros.IdCargo != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdCargo in @IdCargo ";
                        IdCargo = paginador.filtroRegistros.IdCargo.Split(",");
                    }
                    if (paginador.filtroRegistros.IdAreaFormacion != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdAreaFormacion in @IdFormacion ";
                        IdFormacion = paginador.filtroRegistros.IdAreaFormacion.Split(",");
                    }
                    if (paginador.filtroRegistros.IdAreaTrabajo != "")
                    {
                        _queryCondicion = _queryCondicion + "And IdAreaTrabajo in @IdTrabajo ";
                        IdTrabajo = paginador.filtroRegistros.IdAreaTrabajo.Split(",");
                    }
                }

                string _queryRegistro = "SELECT Id,IdAlumno,ApellidoPaterno,ApellidoMaterno,nombre1,nombre2,Telefono,Celular,Email,CentroCosto,IdCentroCosto,NombrePrograma,TipoDato,IdTipoDato,Origen,IdOrigen,CodigoFase," +
                                        " IdFaseOportunidad,Formacion,IdAreaFormacion,Trabajo,IdAreaTrabajo,Industria,IdIndustria,Cargo,IdCargo,NombrePais,IdPais,NombreCiudad,IdCiudad,OrigenCampania,IdCampaniaScoring,IdCategoriaOrigen," +
                                        " IdAsignacionAutomaticaOrigen,FechaProgramada,FechaRegistroCampania,IdFaseOportunidadPortal,FechaCreacion,IdPersonal,IdCategoriaDato,IdTipoInteraccion,IdSubCategoriaDato,IdInteraccionFormulario " +
                                        " From mkt.V_TAsignacionAutomatica_RegistroError Where IdAsignacionAutomaticaTipoError=1 and EStadoAsignacion=1 and EstadoError=1 and FechaRegistroCampania>='2018-22-09 00:00:00.00' " + _queryCondicion + "order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                string queryRegistro = _dapperRepository.QueryDapper(_queryRegistro, new { FechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<AsignacionAutomaticaCompuestoErroneosDTO>>(queryRegistro);
                string _queryCantidad = "SELECT Count(*) From mkt.V_TAsignacionAutomatica_RegistroError WHERE IdAsignacionAutomaticaTipoError=1 and EStadoAsignacion=1 and EstadoError=1 and FechaRegistroCampania>='2018-22-09 00:00:00.00' " + _queryCondicion + "";
                string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { FechaInicio, FechaFin, IdCentroCosto, IdProbabilidad, IdPais, IdCategoriaOrigen, IdIndustria, IdCargo, IdFormacion, IdTrabajo, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                if (rpta.Count() > 0)
                {
                    rpta.FirstOrDefault().TotalRegistros = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 03/22/22
        /// Version: 1.0
        /// <summary>
        /// Verifica si existe el registro de Asignación Automática según su Id
        /// </summary>
        /// <param name="Id">Id del Error </param>
        /// <returns> bool </returns>
        public bool ExisteAsignacionAutomatica(int Id)
        {
            try
            {
                var query = @"SELECT Id FROM mkt.T_AsignacionAutomatica WHERE Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]") && !resultado.Equals("null"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 03/22/22
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro de Asignación Automática ségún su Id
        /// </summary>
        /// <param name="Id">Id de Asignación Automática </param>
        /// <returns> AsignacionAutomatica </returns>
        public AsignacionAutomatica ObtenerAsignacionAutomaticaPorId(int Id)
        {
            try
            {
                AsignacionAutomatica respuesta = new AsignacionAutomatica();
                var query = @"SELECT Id,IdAlumno,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Telefono,Celular,Email,IdCentroCosto,  
                            NombrePrograma,IdTipoDato,IdOrigen,IdFaseOportunidad,IdAreaFormacion,IdAreaTrabajo,IdIndustria,IdCargo, 
                            IdPais,IdCiudad,Validado,Corregido,OrigenCampania,IdConjuntoAnuncio,IdCategoriaOrigen,IdAsignacionAutomaticaOrigen,  
                            IdCampaniaScoring,FechaRegistroCampania,IdFaseOportunidadPortal,IdOportunidad,IdPersonal,IdTiempoCapacitacion, 
                            IdCategoriaDato,IdTipoInteraccion,IdSubCategoriaDato,IdInteraccionFormulario,UrlOrigen,ProbabilidadActual,  
                            ProbabilidadActualDesc,IdPagina,FechaProgramada,FechaCreacion,UsuarioCreacion,AptoProcesamiento,Estado FROM mkt.T_AsignacionAutomatica WHERE Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != null && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<AsignacionAutomatica>(resultado);
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

