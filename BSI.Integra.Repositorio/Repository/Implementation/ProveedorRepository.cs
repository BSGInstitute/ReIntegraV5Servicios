using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_Proveedor
    /// </summary>
    public class ProveedorRepository : GenericRepository<TProveedor>, IProveedorRepository
    {
        private Mapper _mapper;

        public ProveedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedor, Proveedor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedor MapeoEntidad(Proveedor entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedor modelo = _mapper.Map<TProveedor>(entidad);

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

        public TProveedor Add(Proveedor entidad)
        {
            try
            {
                var Proveedor = MapeoEntidad(entidad);
                base.Insert(Proveedor);
                return Proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedor Update(Proveedor entidad)
        {
            try
            {
                var Proveedor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Proveedor.RowVersion = entidadExistente.RowVersion;

                base.Update(Proveedor);
                return Proveedor;
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


        public IEnumerable<TProveedor> Add(IEnumerable<Proveedor> listadoEntidad)
        {
            try
            {
                List<TProveedor> listado = new List<TProveedor>();
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

        public IEnumerable<TProveedor> Update(IEnumerable<Proveedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedor> listado = new List<TProveedor>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la sede en formato de html
        /// </summary>
        /// <param name="id">Id de la sesion del PEspecifico (PK de la tabla pla.T_PEspecificoSesion)</param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public Proveedor? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT 
                                Id,
                                IdTipoContribuyente,
                                IdDocumentoIdentidad,
                                NroDocIdentidad,
                                RazonSocial,
                                Nombre1,
                                Nombre2,
                                ApePaterno,
                                ApeMaterno,
                                Direccion,
                                Descripcion,
                                IdCiudad,
                                Telefono,
                                Email,
                                Celular1,
                                Celular2,
                                Contacto1,
                                Contacto2,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdPrestacionRegistro,
                                EsPersonaValida,
                                IdTipoImpuesto_Preferido AS IdTipoImpuestoPreferido,
                                IdRetencion_Preferido AS IdRetencionPreferido,
                                IdDetraccion_Preferido AS IdDetraccionPreferido,
                                IdPersonal_Asignado AS IdPersonalAsignado,
                                Alias 
                            FROM fin.T_Proveedor 
                            WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Proveedor>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Se obtiene el Id y el Nombre de Proveedor filtrado por Producto.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="idProducto"/>Identificador del producto
        public IEnumerable<ProveedorComboDTO> ObtenerProveedorPorProducto(int idProducto)
        {
            try
            {
                List<ProveedorComboDTO> proveedorPorProducto = new List<ProveedorComboDTO>();
                var proveedorPorProductoDB = _dapperRepository.QuerySPDapper("pla.SP_ObtenerProveedorPorProducto", new { idProducto });
                if (!string.IsNullOrEmpty(proveedorPorProductoDB) && !proveedorPorProductoDB.Contains("[]"))
                {
                    proveedorPorProducto = JsonConvert.DeserializeObject<List<ProveedorComboDTO>>(proveedorPorProductoDB);
                }
                return proveedorPorProducto;
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
        /// Se obtiene el Id y el Nombre de Proveedor filtrado por Producto.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="valor"/>Nombre de proveedorHistorico.
        public IEnumerable<ProveedorComboDTO> ObtenerNombreProveedorAutocomplete(string valor)
        {
            try
            {
                List<ProveedorComboDTO> proveedor = new List<ProveedorComboDTO>();
                var _query = string.Empty;
                _query = "SELECT Id ,Nombre FROM FIN.V_ObtenerProveedorHistorico WHERE Nombre LIKE CONCAT('%',@valor,'%') ORDER By Nombre ASC";
                var proveedorDB = _dapperRepository.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<ProveedorComboDTO>>(proveedorDB);
                }
                return proveedor;
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
        /// Se obtiene el Id,NroDocIdentidad,RazonSocial,IdTipoImpuesto,IdDetraccion,IdDetraccion y el IdPais 
        /// de fin.V_ObtenerProveedorRazonSocialRucNombres filtrado por NroDocIdentidad.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="valor"/>NroDocIdentidad del Proveedor.
        public IEnumerable<ProveedorRucRazonSocialDTO> ObtenerProveedorPorRuc(string valor)
        {
            try
            {
                List<ProveedorRucRazonSocialDTO> proveedor = new List<ProveedorRucRazonSocialDTO>();
                var _query = string.Empty;
                _query = "SELECT Id , NroDocIdentidad, RazonSocial, IdTipoImpuesto,IdDetraccion, IdDetraccion,IdPais FROM fin.V_ObtenerProveedorRazonSocialRucNombres WHERE NroDocIdentidad LIKE '%" + valor + "%' and Estado=1";
                var proveedorDB = _dapperRepository.QueryDapper(_query, new { valor });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<ProveedorRucRazonSocialDTO>>(proveedorDB);
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Autor Modificación: Jonathan Caipo
        /// Fecha Modificación: 05/05/2023
        /// Version: 1.1
        /// <summary>
        /// Se obtiene el Nombre del ProveedorHistorico
        /// Se Modificó el nombre de la función, de ObtenerNombreProveedorById a ObtenerNombreProveedorPorId.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="id"/>Id del ProveedorHistorico.
        public string ObtenerNombreProveedorPorId(int id)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"
                            SELECT 
                                Nombre AS Valor 
                            FROM 
                                FIN.V_ObtenerProveedorHistorico 
                            WHERE 
                                Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-ONPPI-001@Error en ObtenerNombreProveedorPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre y Ruc de todos los proveedores, que esten activos segun la descripcion que se ingreso ya sea Ruc o Nombres del Proveedor
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="valor"/>Nombre o Documento
        public IEnumerable<FiltroRucProveedorDTO> ObtenerProveedorRucAutocomplete()
        {
            try
            {
                List<FiltroRucProveedorDTO> proveedor = new List<FiltroRucProveedorDTO>();
                var _query = string.Empty;
                _query = "SELECT Id , concat('(',Documento,')',' ',Nombre) as Nombre,Documento as Ruc FROM FIN.V_ObtenerProveedor  ORDER By Nombre ASC";
                var proveedorDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]"))
                {
                    proveedor = JsonConvert.DeserializeObject<List<FiltroRucProveedorDTO>>(proveedorDB);
                }
                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de productos y proveedores
        /// </summary>
        /// <returns> Lista de ProveedorProductoDTO </returns>
        public IEnumerable<ProveedorProductoDTO> ObtenerInformacionProductoProveedor()
        {
            try
            {
                string query = "SELECT Id, Nombre, Simbolo, NombreMoneda, Precio, IdProducto, Presentacion, IdHistorico, Version FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ProveedorProductoDTO>>(resultado)!;
                return new List<ProveedorProductoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerInformacionProductoProveedor(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 19/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de productos y proveedores
        /// </summary>
        /// <returns> Lista de ProveedorProductoDTO </returns>
        public async Task<IEnumerable<ProveedorProductoDTO>> ObtenerInformacionProductoProveedorAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre, Simbolo, NombreMoneda, Precio, IdProducto, Presentacion, IdHistorico, Version FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ProveedorProductoDTO>>(resultado)!;
                return new List<ProveedorProductoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerInformacionProductoProveedorAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de Proveedores para filtro
        /// </summary>
        /// <returns> List<ProveedorProductoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerProveedorFiltro()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.V_ProveedorFiltro WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProveedorFiltro(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la informacion de Proveedores para filtro
        /// </summary>
        /// <returns> List<ProveedorProductoDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerProveedorFiltroAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM fin.V_ProveedorFiltro WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProveedorFiltroAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de proveedores para combo de programa especifico FUR
        /// </summary>
        /// <returns> List<ProveedorDTO> </returns>
        /// <paramref name="Id"/>Identificador del Proveedor.
        public IEnumerable<ProveedorDTO> ObtenerTodoProveedorById(int? Id)
        {
            try
            {
                var camposTabla = "SELECT Id,IdTipoContribuyente,TipoContribuyente,IdDocumentoIdentidad,DocumentoIdentidad,NroDocumento,Proveedor,RazonSocial,ApePaterno,ApeMaterno,Nombre1,Nombre2,Descripcion,Direccion,IdPais,Pais,IdCiudad,Ciudad,Telefono,Email,Celular1,Celular2,Contacto1,Contacto2,IdPrestacionRegistro,Criterio1,Criterio2,Criterio3,Criterio4,Criterio5,FechaModificacion,UsuarioModificacion, IdImpuesto,IdRetencion,IdDetraccion,IdPersonalAsignado,Alias,EsDocente ";
                List<ProveedorDTO> Proveedor = new List<ProveedorDTO>();
                var _query = camposTabla + "FROM  [fin].[V_ObtenerDatosProveedor] where Estado=1 order by Id desc";
                if (Id != null && Id != 0)
                {
                    _query = camposTabla + "FROM  [fin].[V_ObtenerDatosProveedor] where Estado=1 And Id=" + Id + " ORDER BY Id DESC";
                }
                var ProveedorDB = _dapperRepository.QueryDapper(_query, null);
                if (!ProveedorDB.Contains("[]") && !string.IsNullOrEmpty(ProveedorDB))
                {
                    Proveedor = JsonConvert.DeserializeObject<List<ProveedorDTO>>(ProveedorDB);
                }
                return Proveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// <summary>
        /// Genera Reporte de Revision de Docentes para Foro
        /// </summary>
        /// <param name="condicion">Cadena de condiciones</param>
        /// <returns> List<RespuestaReporteRevisionDocenteDTO> </returns>
        public List<RespuestaReporteRevisionDocenteDTO> GenerarReporteRevisionForo(string condicion)
        {
            try
            {
                List<RespuestaReporteRevisionDocenteDTO> listaReporteForo = new List<RespuestaReporteRevisionDocenteDTO>();
                var query = string.Empty;
                if (condicion.Length > 0)
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPGeneralForoAsignacionProveedor_GenerarReporte WHERE " + condicion;
                }
                else
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPGeneralForoAsignacionProveedor_GenerarReporte";
                }
                var respuesta = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaReporteForo = JsonConvert.DeserializeObject<List<RespuestaReporteRevisionDocenteDTO>>(respuesta);
                }
                return listaReporteForo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: ProveedorRepositorio
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// <summary>
        /// Genera Reporte de Revision de Docentes para Proyectos
        /// </summary>
        /// <param name="condicion">Cadena de condiciones</param>
        /// <returns> List<RespuestaReporteRevisionDocenteDTO> </returns>
        public List<RespuestaReporteRevisionDocenteDTO> GenerarReporteProyecto(string condicion)
        {
            try
            {
                List<RespuestaReporteRevisionDocenteDTO> listaReporteProyecto = new List<RespuestaReporteRevisionDocenteDTO>();
                var query = string.Empty;
                if (condicion.Length > 0)
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPgeneralProyectoAplicacionProveedor_GenerarReporte WHERE " + condicion;
                }
                else
                {
                    query = "SELECT IdArea, Area, IdSubArea, SubArea, IdPGeneral, PGeneral, IdProveedor, Nombre, CategoriaRevision, IdPersonalAsignado, PersonalAsignado, IdModalidadCurso, ModalidadCurso FROM pla.V_TPgeneralProyectoAplicacionProveedor_GenerarReporte";
                }
                var respuesta = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    listaReporteProyecto = JsonConvert.DeserializeObject<List<RespuestaReporteRevisionDocenteDTO>>(respuesta);
                }
                return listaReporteProyecto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de proveedores para combo
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        public IEnumerable<ProveedorComboDTO> ObtenerProveedorCombo(string Texto)
        {
            try
            {
                var query = "SELECT Id, Nombre FROM fin.V_ObtenerInformacionProductoProveedor WHERE Estado = 1 AND Nombre like '%" + Texto + "%'";
                var proveedores = _dapperRepository.QueryDapper(query, null);

                return JsonConvert.DeserializeObject<List<ProveedorComboDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene La lista de Proveedores con estado de Cuenta Pagado o Pendiente (Usado para combobox)
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        public IEnumerable<ProveedorComboDTO> ObtenerProveedoresConEstadoCuentaPagadoPendiente()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM fin.V_ProveedorConEstadoCuentaPagadoPendiente";
                var proveedores = _dapperRepository.QueryDapper(query, null);

                return JsonConvert.DeserializeObject<List<ProveedorComboDTO>>(proveedores);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Los registros eliminados Logicamente de fin.T_Proveedor
        /// </summary>
        /// <returns> int </returns>
        /// <paramref name="email"/>
        public int? ObtenerProveedorEliminadoEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> proveedor = new Dictionary<string, int>();
                var _query = "SELECT Id FROM fin.T_Proveedor where estado = 0 and Email=@Email";
                var proveedorDB = _dapperRepository.FirstOrDefault(_query, new { Email = email });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]") && proveedorDB != "null")
                {
                    proveedor = JsonConvert.DeserializeObject<Dictionary<string, int>>(proveedorDB);
                }
                return proveedor.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Los registros eliminados Logicamente de fin.T_Proveedor
        /// </summary>
        /// <returns> int </returns>
        /// <paramref name="email"/>
        public int? ObtenerProveedorEmailRepetido(string email)
        {
            try
            {
                Dictionary<string, int> proveedor = new Dictionary<string, int>();
                var _query = "SELECT Id FROM fin.T_Proveedor where estado = 1 and Email=@Email";
                var proveedorDB = _dapperRepository.FirstOrDefault(_query, new { Email = email });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]") && proveedorDB != "null")
                {
                    proveedor = JsonConvert.DeserializeObject<Dictionary<string, int>>(proveedorDB);
                }
                return proveedor.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de fin.T_Proveedor 
        /// </summary>
        /// <returns> true, correcto  </returns>
        /// <paramref name="Id"/>Identificador de fin.T_Proveedor
        public bool ActivarProveedor(int Id)
        {
            try
            {
                _dapperRepository.QueryDapper("UPDATE fin.T_Proveedor set Estado=1 where Id=@Id", new { Id = Id });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para un combo
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerNombreProveedorParaHonorario()
        {
            try
            {
                var query = $@"
                        SELECT 
                            Id, Nombre
                        FROM 
                            fin.V_Obtener_ProveedorParaHonorario
                        ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-ONPPH-001@Error en ObtenerNombreProveedorParaHonorario() {ex.Message}", ex);
            }
        }

        ///Repositorio: ProveedorRepositorio
        ///Autor: Griselberto Huaman.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtiene el email1 del Proveedor
        /// </summary>
        /// <param name="id"> Id Proveedor </param>
        /// <returns>Email 1 del Proveedor</returns>
        public string ObtenerEmail(int id)
        {
            try
            {
                return this.GetBy(x => x.Id == id).FirstOrDefault().Email;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los coordinadores para ser mostrados en una grilla (para CRUD Propio)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerTodoCoordinadoresDocentes()
        {
            try
            {
                List<ComboDTO> coordinadores = new List<ComboDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_Coordinador_NombreCompleto WHERE Estado = 1 and Id in (17,4215,4108,4661)";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                coordinadores = JsonConvert.DeserializeObject<List<ComboDTO>>(respuesta);
                return coordinadores;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de Proveedor por su Id
        /// </summary>
        /// <param name="idProveedor"> Id de Expositor </param>
        /// <returns> ProveedorDTO </returns>
        public ProveedorDTO ObtenerProveedorPorId(int idProveedor)
        {
            try
            {
                var rpta = new ProveedorDTO();
                var query = @"SELECT Id,IdTipoContribuyente,IdDocumentoIdentidad,NroDocIdentidad,RazonSocial,
                                       Nombre1,Nombre2,ApePaterno,ApeMaterno,Direccion,Descripcion,IdCiudad,
                                       Telefono,Email,Celular1,Celular2,Contacto1,Contacto2,IdPrestacionRegistro,
                                       EsPersonaValida,IdTipoImpuesto_Preferido IdTipoImpuestoPreferido,IdRetencion_Preferido IdRetencionPreferido,
	                                   IdDetraccion_Preferido IdDetraccionPreferido,IdPersonal_Asignado IdPersonalAsignado,Alias 
	                           FROM fin.T_Proveedor 
	                           WHERE Estado = 1 AND Id=@idProveedor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProveedor });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ProveedorDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Repositorio: ProveedorPersonalRepositorio
        /// Autor: Luis Huallpa - Britsel Calluchi.
        /// Fecha: 24/04/2021
        /// <summary>
        /// Obtiene lista de proveedores para Filtro
        /// </summary>
        /// <returns> List<FiltroConvocatoriaPersonalDTO> </returns>
        public List<FiltroConvocatoriaPersonalDTO> ObtenerProveedoresConvocatoriaPersonal()
        {
            try
            {
                List<FiltroConvocatoriaPersonalDTO> listaProveedoresConvocatorias = new List<FiltroConvocatoriaPersonalDTO>();
                var query = "SELECT Id, IdProveedor, IdTipoServicio, EstadoPTS, EstadoP, RazonSocial FROM fin.V_TProveedorTipoServicio_ObtenerProveedorAvisoLaboral WHERE IdTipoServicio = 47 AND EstadoP = 1";
                var res = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaProveedoresConvocatorias = JsonConvert.DeserializeObject<List<FiltroConvocatoriaPersonalDTO>>(res);
                }
                return listaProveedoresConvocatorias;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener la lista de docentes 
        /// </summary>
        /// <returns>Lista del docentes para el filtro en un List<ItemComboAutocompleDTO></returns>
        public IEnumerable<ComboDTO> ObtenerListaDocentes()
        {
            try
            {
                IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                            SELECT 
                                Id, CONCAT(ApePaterno,' ',ApeMaterno,', ',Nombre1,' ',Nombre2) AS Nombre 
                            FROM 
                                fin.T_Proveedor 
                            WHERE 
                                Estado = 1 AND CONCAT(ApePaterno,' ',ApeMaterno,', ',Nombre1,' ',Nombre2) <> ' ,  '";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OLD-001@ Error en ObtenerListaDocentes() {ex.Message}", ex);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// <summary>
        /// Obtiene el IdDocente, IdPersonalAsignado y PersonalAsignado por IdProveedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PersonalAsignadoDocenteDTO PersonalAsignadoDocente(int id)
        {
            try
            {

                PersonalAsignadoDocenteDTO personalAsignado = new PersonalAsignadoDocenteDTO();
                var _query = string.Empty;
                _query = "SELECT IdDocente,Docente, IdPersonalAsignado, PersonalAsignado FROM mkt.V_PersonalAsignadoDocente WHERE IdDocente=@Id";
                var proveedorDB = _dapperRepository.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(proveedorDB) && !proveedorDB.Contains("[]") && proveedorDB != "null")
                {
                    personalAsignado = JsonConvert.DeserializeObject<PersonalAsignadoDocenteDTO>(proveedorDB);
                }
                return personalAsignado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 2024-10-25
        /// <summary>
        /// Retorna la lista de proveedores PAGINAS RECLUTADORAS DE CV 
        /// </summary>
        /// <returns>IEnumerable<ProveedorComboDTO></returns>
        public IEnumerable<ProveedorComboDTO> ObtenerProveedoresPaginasReclutadoras()
        {
            try
            {
                var queryDapper = @"SELECT
                                        IdProveedor AS Id,
                                        RazonSocial AS Nombre
                                    FROM
                                        fin.V_TProveedorTipoServicio_ObtenerProveedorAvisoLaboral
                                    WHERE
                                        IdTipoServicio = 47
                                        AND EstadoP = 1;";
                var res = _dapperRepository.QueryDapper(queryDapper, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProveedorComboDTO>>(res);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 2024-11-06
        /// <summary>
        /// Retorna un proveedor de PAGINA RECLUTADORAS DE CV 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProveedorComboDTO</returns>
        public ProveedorComboDTO ObtenerProveedoresPaginasReclutadorasPorId(int id)
        {
            try
            {
                var queryDapper = @"SELECT
                                        IdProveedor AS Id,
                                        RazonSocial AS Nombre
                                    FROM
                                        fin.V_TProveedorTipoServicio_ObtenerProveedorAvisoLaboral
                                    WHERE
                                        IdTipoServicio = 47
                                        AND Id = @Id
                                        AND EstadoP = 1;";
                var res = _dapperRepository.FirstOrDefault(queryDapper, new { Id = id });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<ProveedorComboDTO>(res);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
