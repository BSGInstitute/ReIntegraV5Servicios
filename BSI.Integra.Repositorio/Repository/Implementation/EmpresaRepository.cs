using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EmpresaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Empresa
    /// </summary>
    public class EmpresaRepository : GenericRepository<TEmpresa>, IEmpresaRepository
    {
        private Mapper _mapper;

        public EmpresaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEmpresa, Empresa>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEmpresa MapeoEntidad(Empresa entidad)
        {
            try
            {
                //crea la entidad padre
                TEmpresa modelo = _mapper.Map<TEmpresa>(entidad);

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

        public TEmpresa Add(Empresa entidad)
        {
            try
            {
                var Empresa = MapeoEntidad(entidad);
                base.Insert(Empresa);
                return Empresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEmpresa Update(Empresa entidad)
        {
            try
            {
                var Empresa = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Empresa.RowVersion = entidadExistente.RowVersion;

                base.Update(Empresa);
                return Empresa;
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


        public IEnumerable<TEmpresa> Add(IEnumerable<Empresa> listadoEntidad)
        {
            try
            {
                List<TEmpresa> listado = new List<TEmpresa>();
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

        public IEnumerable<TEmpresa> Update(IEnumerable<Empresa> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEmpresa> listado = new List<TEmpresa>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public IEnumerable<EmpresaDTO> ObtenerEmpresa()
        {
            try
            {
                List<EmpresaDTO> rpta = new List<EmpresaDTO>();
                var query = @"
                    SELECT
	                    Id, Nombre, RUC, IdTipoIdentificador ,Direccion, Telefono, PaginaWeb, Email, Trabajadores, NivelFacturacion, IdPais, Pais, IdRegion, IdCiudad, IdIndustria, IdTipoEmpresa, IdTamanio, Ciiu, IdCodigoCiiuIndustria, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, Estado
                    FROM fin.V_Empresa WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EmpresaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public List<EmpresaObtenerDTO> ObtenerEmpresas()
        {
            try
            {
                List<EmpresaObtenerDTO> rpta = new List<EmpresaObtenerDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    RUC,
	                    IdTipoIdentificador,
	                    Direccion,
	                    Telefono,
	                    PaginaWeb,
	                    Email,
	                    Trabajadores,
	                    NivelFacturacion,
	                    IdPais,
	                    IdRegion,
	                    IdCiudad,
	                    IdIndustria,
	                    IdTipoEmpresa,
	                    IdTamanio,
	                    IdCodigoCiiuIndustria,
                        IdMunicipioMexico,
                        IdAsentamientoMexico,
                        IdCiudadMexico,
                        CodigoPostal,
	                    FechaCreacion
                    FROM pla.T_Empresa WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EmpresaObtenerDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Empresa para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre
                    FROM pla.T_Empresa
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#ER-OC-001@Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public IEnumerable<EmpresaDTO> ObtenerEmpresa2()
        {
            try
            {
                List<EmpresaDTO> rpta = new List<EmpresaDTO>();
                var query = @"
                    SELECT Id, Nombre, RUC, IdTipoIdentificador, Direccion, Telefono, PaginaWeb, Email, Trabajadores, NivelFacturacion, IdPais, IdRegion, IdCiudad, IdTipoEmpresa, IdTamanio, IdCodigoCiiuIndustria, FechaCreacion FROM pla.T_Empresa WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EmpresaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto huaman
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoIdentificador para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<TipoIdentificadorComboDTO> ObtenerComboTipoIdentificador()
        {
            try
            {
                List<TipoIdentificadorComboDTO> rpta = new List<TipoIdentificadorComboDTO>();
                var query = @"
                    SELECT [Id]
                          ,[Nombre]
                          ,[IdPais]
                      FROM [fin].[T_TipoIdentificador]
                      WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoIdentificadorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto huaman
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TamanioEmpresa para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboTamanioEmpresa()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM pla.T_TamanioEmpresa WHERE Estado = 1";
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

        /// Autor: Griselberto huaman
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TamanioEmpresa para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<CodigoCiiuIndustriaComboDTO> ObtenerComboCodigoCiiuIndustria()
        {
            try
            {
                List<CodigoCiiuIndustriaComboDTO> rpta = new List<CodigoCiiuIndustriaComboDTO>();
                var query = @"SELECT [Id]
                                  ,[CIIU]
                                  ,[Nombre]
                                  ,[IdIndustria]
                              FROM [pla].[T_CodigoCiiuIndustria]
                              WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CodigoCiiuIndustriaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de Empresas relacionadas al Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de la Empresa</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAutocomplete(string nombreParcial)
        {
            try
            {
                nombreParcial = $"%{nombreParcial}%";
                List<ComboDTO> empresas = new List<ComboDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM pla.T_Empresa
                    WHERE Estado = 1 AND Nombre LIKE @nombreParcial
                    ORDER BY Nombre ASC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    empresas = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoQuery);
                }
                return empresas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el tamanio de la Empresa asociado a un Id Empresa.
        /// </summary>
        /// <param name="idEmpresa">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerIdTamanioEmpresaPorIdEmpresa(int idEmpresa)
        {
            try
            {
                ValorIntDTO rpta = new ValorIntDTO();
                var query = @"SELECT IdTamanio AS Valor FROM pla.T_Empresa WHERE Id = @idEmpresa AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idEmpresa });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de empresas competidoras
        /// IdTipoEmpresa = 1: COMPETIDOR";
        /// IdTipoEmpresa = 0: NO_COMPETIDOR";
        /// </summary>
        public List<ComboDTO> ObtenerTodoCompetidores()
        {
            try
            {
                List<ComboDTO> empresa = new List<ComboDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Estado = 1 AND IdTipoEmpresa = 1";
                var empresasCompetidorasDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(empresasCompetidorasDB) && !empresasCompetidorasDB.Contains("[]"))
                {
                    empresa = JsonConvert.DeserializeObject<List<ComboDTO>>(empresasCompetidorasDB);
                }
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 11/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una empresa para usado para filtro por id 
        /// </summary>
        public ComboDTO ObtenerEmpresaPorId(int id)
        {
            try
            {
                ComboDTO empresa = new ComboDTO();
                var query = string.Empty;
                query = "SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Id = @id AND Estado = 1";
                var cargoDB = _dapperRepository.FirstOrDefault(query, new { id });
                empresa = JsonConvert.DeserializeObject<ComboDTO>(cargoDB)!;
                return empresa;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/311/2022
        /// <summary>
        /// Obtiene las empresas que contengan el valor nombre.
        /// </summary>
        /// <param name="nombre"> Nombre de Empresa</param>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> CargarEmpresaAutoComplete(string nombre)
        {
            try
            {
                string query = @"SELECT Id, Nombre from pla.V_TEmpresa_ObtenerIdNombre
                                 WHERE Nombre LIKE CONCAT('%',@nombre,'%') AND Estado = 1 ORDER BY Nombre ASC";
                string queryRespuesta = _dapperRepository.QueryDapper(query, new { nombre });
                return JsonConvert.DeserializeObject<List<ComboDTO>>(queryRespuesta)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de empresa.
        /// </summary>
        /// <param name="id">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        /// 
        /// Modificacion:
        /// Autor: Juan D. Huanaco Quispe
        /// Se agrego el campo CodigoPostal
        public Empresa ObtenerPorId(int id)
        {
            try
            {
                var rpta = new Empresa();
                var query = @"SELECT Id,
                                       Nombre,
                                       RUC,
                                       IdTipoIdentificador,
                                       Direccion,
                                       Telefono,
                                       PaginaWeb,
                                       Email,
                                       Trabajadores,
                                       NivelFacturacion,
                                       IdPais,
                                       IdRegion,
                                       IdCiudad,
                                       IdIndustria,
                                       IdTipoEmpresa,
                                       IdTamanio,
                                       Ciiu,
                                       Municipio,
                                       EstadoLugar,
                                       CodigoPostal,
                                       Colonia,
                                       IdCodigoCiiuIndustria,
                                       Estado,
                                       CodigoPostal,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                             IdMigracion FROM pla.T_Empresa WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Empresa>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public EmpresaFiltroDTO ObtenerEmpresaFiltro(FiltroKendoGridDTO gridState)
        {
            try
            {
                string condicion = "";
                string nombre = "";
                string ruc = "";
                string direccion = "";
                int idPais = 0;
                string paginacion = "";
                if (gridState.Take != 0)
                    paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                if (gridState.Filter != null)
                {
                    foreach (var item in gridState.Filter.Filters)
                    {
                        if (item.Field.ToLower() == "nombre" && item.Value.Contains(""))
                        {
                            condicion += " AND Nombre LIKE @Nombre ";
                            nombre = item.Value;
                        }
                        else if (item.Field.ToLower() == "ruc" && item.Value.Contains(""))
                        {
                            condicion += " AND RUC LIKE @RUC ";
                            ruc = item.Value;
                        }
                        else if (item.Field.ToLower() == "direccion" && item.Value.Contains(""))
                        {
                            condicion += " AND Direccion LIKE @Direccion ";
                            direccion = item.Value;
                        }
                        else if (item.Field.ToLower() == "idPais" && item.Value.Contains(""))
                        {
                            condicion += " AND IdPais = @idPais ";
                            idPais = int.Parse(item.Value);
                        }
                    }
                }

                List<EmpresaObtenerDTO> rpta = new List<EmpresaObtenerDTO>();
                var query = $@"
                    SELECT
	                    Id,
	                    Nombre,
	                    RUC,
	                    IdTipoIdentificador,
	                    Direccion,
	                    Telefono,
	                    PaginaWeb,
	                    Email,
	                    Trabajadores,
	                    NivelFacturacion,
	                    IdPais,
	                    IdRegion,
	                    IdCiudad,
	                    IdIndustria,
	                    IdTipoEmpresa,
	                    IdTamanio,
	                    IdCodigoCiiuIndustria,
                        Municipio,
                        Colonia,
                        EstadoLugar,
                        IdMunicipioMexico,
                        IdAsentamientoMexico,
                        IdCiudadMexico,
                        CodigoPostal,
	                    FechaCreacion
                    FROM pla.T_Empresa WHERE Estado = 1 {condicion} ORDER BY FechaCreacion DESC {paginacion}";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    Nombre = nombre,
                    Ruc = ruc,
                    Direccion = direccion,
                    IdPais = idPais,
                    gridState.Skip,
                    gridState.Take,
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EmpresaObtenerDTO>>(resultado)!;
                }
                IntDTO cantidad = new IntDTO();
                query = $@"SELECT COUNT(*) AS Valor FROM pla.T_Empresa WHERE Estado = 1 {condicion}";
                var resultado2 = _dapperRepository.FirstOrDefault(query, new
                {
                    Nombre = nombre,
                    Ruc = ruc,
                    Direccion = direccion,
                    IdPais = idPais
                });
                if (!string.IsNullOrEmpty(resultado2) && resultado2 != "null")
                {
                    cantidad = JsonConvert.DeserializeObject<IntDTO>(resultado2)!;
                }
                var respuesta = new EmpresaFiltroDTO()
                {
                    Data = rpta,
                    Total = cantidad.Valor!.Value
                };
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerTodoEmpresasFiltro()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT Id,Nombre FROM pla.V_TEmpresa_ObtenerIdNombre WHERE Estado = 1";
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
    }
}
