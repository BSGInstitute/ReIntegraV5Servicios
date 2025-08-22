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
    /// Repositorio: OrigenRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Origen
    /// </summary>
    public class OrigenRepository : GenericRepository<TOrigen>, IOrigenRepository
    {
        private Mapper _mapper;

        public OrigenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigen, Origen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOrigen MapeoEntidad(Origen entidad)
        {
            try
            {
                //crea la entidad padre
                TOrigen modelo = _mapper.Map<TOrigen>(entidad);

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

        public TOrigen Add(Origen entidad)
        {
            try
            {
                var Origen = MapeoEntidad(entidad);
                base.Insert(Origen);
                return Origen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOrigen Update(Origen entidad)
        {
            try
            {
                var Origen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Origen.RowVersion = entidadExistente.RowVersion;

                base.Update(Origen);
                return Origen;
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


        public IEnumerable<TOrigen> Add(IEnumerable<Origen> listadoEntidad)
        {
            try
            {
                List<TOrigen> listado = new List<TOrigen>();
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

        public IEnumerable<TOrigen> Update(IEnumerable<Origen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOrigen> listado = new List<TOrigen>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Origen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_Origen WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Origen
        /// </summary>
        /// <returns> List<OrigenDTO> </returns>
        public IEnumerable<OrigenDTO> ObtenerOrigen()
        {
            try
            {
                List<OrigenDTO> rpta = new List<OrigenDTO>();
                var query = @"SELECT Id, Nombre, Descripcion, IdTipodato, Prioridad, IdCategoriaOrigen, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_Origen
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OrigenDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe Pari.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Tarifario Detalle de Agenda
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> List<TarifarioDetalleAgendaDTO> </returns>
        public List<TarifarioDetalleAgendaDTO> ObtenerTarifariosDetallesAgenda(int idMatriculaCabecera)
        {
            try
            {
                List<TarifarioDetalleAgendaDTO> rpta = new List<TarifarioDetalleAgendaDTO>();
                var query = @"
                    SELECT
	                    TARD.Id,
	                    TARD.IdTarifario,
	                    TARD.Concepto,
	                    TARD.Descripcion,
	                    CASE
		                    WHEN TARD.TipoCantidad = '1'
			                    THEN concat('S/ ',TARD.MontoPeru)
		                    ELSE concat(TARD.MontoPeru,' %')
	                    END AS MontoPeru,
	                    CASE
		                    WHEN TARD.TipoCantidad = '1'
			                    THEN concat('COP ',TARD.MontoColombia)
		                    ELSE concat(TARD.MontoColombia,' %')
	                    END AS MontoColombia,
	                    CASE
		                    WHEN TARD.TipoCantidad = '1'
			                    THEN concat('Bs ',TARD.MontoBolivia)
		                    ELSE concat(TARD.MontoBolivia,' %')
	                    END AS MontoBolivia,
	                    CASE
		                    WHEN TARD.TipoCantidad = '1'
			                    THEN concat('MXN  ',TARD.MontoMexico)
		                    ELSE concat(TARD.MontoMexico,' %')
	                    END AS MontoMexico,
	                    CASE
		                    WHEN TARD.TipoCantidad = '1'
			                    THEN concat('$ ',TARD.MontoExtranjero)
		                    ELSE concat(TARD.MontoExtranjero,' %')
	                    END AS MontoExtranjero
                    FROM ope.T_OportunidadClasificacionOperaciones AS OPE
                    INNER JOIN mkt.t_tarifario AS TAR ON OPE.idtarifario = TAR.id
                    INNER JOIN mkt.t_tarifariodetalle AS TARD ON TAR.id = TARD.idtarifario
                    WHERE TAR.Estado = 1
	                    AND TARD.Estado = 1
	                    AND IdMatriculaCabecera = @idMatriculaCabecera
                    ORDER BY Id ASC;";
                var resultado = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TarifarioDetalleAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe Pari.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Tarifario Detalle de Agenda
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> List<TarifarioDetalleAgendaDTO> </returns>
        public List<VersionprogramaDTO> obtenerversionAlumno(int idMatriculaCabecera)
        {
            try
            {
                List<VersionprogramaDTO> rpta = new List<VersionprogramaDTO>();
                var query = @" SELECT VP.Id,VP.Nombre FROM fin.T_MatriculaCabecera AS MAT
                         INNER JOIN pla.T_VersionPrograma AS VP ON MAT.IdPaquete = VP.Id 
                         WHERE MAT.Id = @idMatriculaCabecera
                    ";
                var resultado = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<VersionprogramaDTO>>(resultado);
                }
                rpta.Add(new VersionprogramaDTO() { Nombre = "Sin Version", Id = 4 });
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de categoria origen por idOrigen
        /// </summary>
        /// <param name="idOrigen">Id del origen</param>
        /// <returns>Objeto de clase OrigenIdCategoriaOrigenDTO</returns>
        public OrigenIdCategoriaOrigenDTO ObtenerIdCategoriaOrigenPorOrigen(int idOrigen)
        {
            try
            {
                var idCategoriaOrigen = new OrigenIdCategoriaOrigenDTO()
                {
                    Id = 0,
                    IdCategoriaOrigen = 0
                };
                var query = "SELECT Id, IdCategoriaOrigen FROM mkt.V_TOrigen_ObtenerCategoriaOrigen WHERE Id = @idOrigen AND Estado = 1";
                var idCategoriaorigenDB = _dapperRepository.FirstOrDefault(query, new { idOrigen });
                if (!string.IsNullOrEmpty(idCategoriaorigenDB) && idCategoriaorigenDB != "null")
                {
                    idCategoriaOrigen = JsonConvert.DeserializeObject<OrigenIdCategoriaOrigenDTO>(idCategoriaorigenDB);
                }
                return idCategoriaOrigen;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna una lista de los Origenes para ser usados en los filtros  "RegistrarOportunidad"
        /// </summary>
        /// <param name="Area"></param>
        /// <returns>Id, Nombre</returns>
		public List<ComboFiltroDTO> ObtenerOrigeneParaRegistrarOportunidad(string Area)
        {
            if (Area.ToUpper() == "MKT" || Area.ToUpper() == "MK")
            {
                List<ComboFiltroDTO> origenes = new List<ComboFiltroDTO>();
                var _query = "SELECT Id, Nombre FROM mkt.T_Origen WHERE Estado=1";
                var origenesDB = _dapperRepository.QueryDapper(_query, null);
                origenes = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(origenesDB)!;
                return origenes;
            }
            else
            {
                List<ComboFiltroDTO> origenes = new List<ComboFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.T_Origen WHERE Nombre in('Referido','Visita Oficina','Llamada Telefonica','Correo Electronico','In House','WhatsApp chat','Otros')";
                var origenesDB = _dapperRepository.QueryDapper(_query, null);
                origenes = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(origenesDB)!;
                return origenes;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Origenes Por CategoriaOrigen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerOrigenPorCategoriaOrigen(int idCategoriaOrigenInbox, int idCategoriaOrigenCorreo, int idCategoriaOrigenComentarios)
        {
            try
            {
                List<ComboFiltroDTO> respuesta = new List<ComboFiltroDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_Origen WHERE IdCategoriaOrigen = @IdCategoriaOrigenInbox OR IdCategoriaOrigen = @IdCategoriaOrigenCorreo OR IdCategoriaOrigen = @IdCategoriaOrigenComentarios AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCategoriaOrigenInbox = idCategoriaOrigenInbox, IdCategoriaOrigenCorreo = idCategoriaOrigenCorreo, IdCategoriaOrigenComentarios = idCategoriaOrigenComentarios });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(resultado)!;
                }
                return respuesta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerOrigenChat(string nombre)
        {
            try
            {
                List<ComboFiltroDTO> origenes = new List<ComboFiltroDTO>();
                var query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre Where Estado = 1 AND Nombre = @nombre";
                var origenDB = _dapperRepository.QueryDapper(query, new { nombre });
                if (!string.IsNullOrEmpty(origenDB) && !origenDB.Contains("[]"))
                {
                    origenes = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(origenDB)!;
                }
                return origenes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); ;
            }
        }

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/11//2022
        /// <summary>
        /// Retorna  lista  de Origen Filtro de  <ComboDTO>
        /// </summary>
        /// <returns>Id, Nombre</returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ComboDTO> origenes = new List<ComboDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_TOrigen_ObtenerIdNombre WHERE Estado = 1";
                var origenesDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(origenesDB) && !origenesDB.Contains("[]"))
                {
                    origenes = JsonConvert.DeserializeObject<List<ComboDTO>>(origenesDB);
                }
                return origenes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de la T_Tarifario
        /// </summary>
        /// <returns></returns>
        /// <exception>List<TarifarioDTO></exception>
        public List<TarifarioDTO> ObtenerTarifarios()
        {
            try
            {
                var data = new List<TarifarioDTO>();
                var _query = @"SELECT 
                                Id,Nombre,FechaInicio,FechaFin,VisiblePortalWeb,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,Estado  
                            FROM 
                                mkt.t_tarifario 
                            WHERE 
                                Estado = 1";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tarifario de tallado en lista por idTarifario
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <returns></returns>
        /// <exception>List<TarifarioDetalleConfiguracionDTO></exception>
        public List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetalles(int idTarifario)
        {
            try
            {
                var data = new List<TarifarioDetalleConfiguracionDTO>();
                var _query = @"SELECT 
                                Id, IdTarifario, Concepto, Descripcion, Monto, IdPais, NombrePais, IdMoneda, NombrePlural, Simbolo, TipoCantidad, Estados, SubEstados 
                            FROM 
                                [mkt].[V_ObtenerTarifarioDetalle] 
                            WHERE 
                                Estado = 1  AND IdTarifario=@idTarifario";
                var respuesta = _dapperRepository.QueryDapper(_query, new { idTarifario });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleConfiguracionDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un Tarifario
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        /// <exception">List<TarifarioDTO></exception>
        public List<TarifarioDTO> InsertarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_InsertarTarifario";
                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    NombreTarifario = objeto.Nombre,
                    FechaInicioTarifario = objeto.FechaInicio,
                    FechaFinTarifario = objeto.FechaFin,
                    VisiblePortalWebTarifario = objeto.VisiblePortalWeb,
                    UsuarioTarifario = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Tarifario mediante TarifarioNuevoDTO objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public List<TarifarioDTO> ActualizarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                string queryInsertar = "mkt.SP_ActualizarTarifario";
                var query = _dapperRepository.QuerySPDapper(queryInsertar, new
                {
                    IdTarifario = objeto.Id,
                    NombreTarifario = objeto.Nombre,
                    FechaInicioTarifario = objeto.FechaInicio,
                    FechaFinTarifario = objeto.FechaFin,
                    VisiblePortalWebTarifario = objeto.VisiblePortalWeb,
                    UsuarioTarifario = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(query)!;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario identificado por idTarifario, usuario
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<TarifarioDTO> EliminarTarifario(int idTarifario, string usuario)
        {
            try
            {
                string _queryInsertar = "mkt.SP_EliminarTarifario";
                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    IdTarifario = idTarifario,
                    UsuarioTarifario = usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDTO>>(queryInsert)!;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de los origenes y los nombres de sus categoria origen
        /// </summary>
        /// <returns>Lista de objetos de clase OrigenIdCategoriaOrigenDTO</returns>
        public List<OrigenesCategoriaOrigenDTO> ObtenerOrigenesCategoriasOrigen()
        {
            try
            {
                var query = "SELECT Id,Nombre,NombreCategoria FROM mkt.V_ObtenerOrigenesCategoriaOrigen";
                var OrigenDB = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<OrigenesCategoriaOrigenDTO>>(OrigenDB);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina el Tarifario País según Id y Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<TarifarioDetalleDTO> EliminarTarifarioDetallePais(int id, string usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarTarifarioDetallePais";
                var queryEliminar = _dapperRepository.QuerySPDapper(_queryEliminar, new
                {
                    Id = id,
                    UsuarioTarifario = usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryEliminar)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario Detalle por concepto y usuario
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="usuario"></param>
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<TarifarioDetalleDTO> EliminarTarifarioDetalle(string concepto, string usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarTarifarioDetalle";
                var queryEliminar = _dapperRepository.QuerySPDapper(_queryEliminar, new
                {
                    Concepto = concepto,
                    UsuarioTarifario = usuario
                });
                return JsonConvert.DeserializeObject<List<TarifarioDetalleDTO>>(queryEliminar)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de Origen Filtro de reclamo
        /// </summary> 
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<ComboFiltroDTO> ObtenerCombosOrigen()
        {
            try
            {
                var data = new List<ComboFiltroDTO>();
                var _query = "SELECT Id,Nombre FROM mkt.V_OrigenFiltroReclamo";
                var respuesta = _dapperRepository.QueryDapper(_query, new { });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Gilmer Quispe
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        ///Inserta tasas Academicas
        /// </summary> 
        /// <returns> List<TarifarioDetalleDTO> </returns>

        public List<TasasAcademicasDetalleDTO> AgregarTasasAcademicasProcedimiento(string CodigoMatricula, int IdConcepto, float Monto, string Moneda, string Usuario, DateTime Fecha)
        {
            try
            {
                string _queryInsertar = "fin.SP_AgregarTasasAcademicas";
                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    CodigoMatricula = CodigoMatricula,
                    IdConcepto = IdConcepto,
                    Monto = Monto,
                    Moneda = Moneda,
                    Usuario = Usuario,
                    Fecha = Fecha,
                });
                return JsonConvert.DeserializeObject<List<TasasAcademicasDetalleDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
         ///Autor:Margiory Ramirez.
         ///Fecha: 19/01/2023
         ///<summary>
         /// Obtener Tarifario Detalle para modulo Cronograma Pagos
         /// </summary>
         /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
         /// <returns> Lista Tarifario Detalle Agenda: List<TarifarioDetalleAgendaDTO></returns>
        public List<TarifarioDetalleMontoDTO> ObtenerTarifariosDetallesMonto(string nombre)
        {
            try
            {
                var data = new List<TarifarioDetalleMontoDTO>();
                var _query = "SELECT Id,IdTarifario,CONCAT(Concepto, ' - ', Monto, ' ', NombrePlural) as Detalle FROM [mkt].[V_ObtenerTarifarioDetalle] WHERE Concepto LIKE CONCAT('%',@nombre,'%') AND Estado = 1";
                var respuesta = _dapperRepository.QueryDapper(_query, new { nombre });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<TarifarioDetalleMontoDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public int ObtenerIdPorNombre(string nombre)
        {
            try
            {
                string query = "SELECT Id as Valor FROM mkt.T_Origen WHERE Estado = 1 AND Nombre = @Nombre";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Nombre = nombre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor ?? 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
