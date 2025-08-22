using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialPespecificoDetalleRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_MaterialPespecificoDetalle
    /// </summary>
    public class MaterialPespecificoDetalleRepository : GenericRepository<TMaterialPespecificoDetalle>, IMaterialPespecificoDetalleRepository
    {
        private Mapper _mapper;

        public MaterialPespecificoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialPespecificoDetalle, MaterialPespecificoDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMaterialPespecificoDetalle MapeoEntidad(MaterialPespecificoDetalle entidad)
        {
            try
            {
                TMaterialPespecificoDetalle modelo = _mapper.Map<TMaterialPespecificoDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialPespecificoDetalle Add(MaterialPespecificoDetalle entidad)
        {
            try
            {
                var MaterialPespecificoDetalle = MapeoEntidad(entidad);
                base.Insert(MaterialPespecificoDetalle);
                return MaterialPespecificoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMaterialPespecificoDetalle Update(MaterialPespecificoDetalle entidad)
        {
            try
            {
                var MaterialPespecificoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialPespecificoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialPespecificoDetalle);
                return MaterialPespecificoDetalle;
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


        public IEnumerable<TMaterialPespecificoDetalle> Add(IEnumerable<MaterialPespecificoDetalle> listadoEntidad)
        {
            try
            {
                List<TMaterialPespecificoDetalle> listado = new List<TMaterialPespecificoDetalle>();
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

        public IEnumerable<TMaterialPespecificoDetalle> Update(IEnumerable<MaterialPespecificoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialPespecificoDetalle> listado = new List<TMaterialPespecificoDetalle>();
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
        /// Fecha: 03/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialPespecificoDetalle.
        /// </summary>
        /// <returns> List<MaterialPespecificoDetalleDTO> </returns>
        public MaterialPespecificoDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        IdMaterialPEspecifico AS IdMaterialPespecifico,
                        IdMaterialVersion,
                        IdMaterialEstado,
                        NombreArchivo,
                        UrlArchivo,
                        UsuarioEnvio,
                        FechaEnvio,
                        UsuarioAprobacion,
                        FechaAprobacion,
                        UsuarioSubida,
                        FechaSubida,
                        ComentarioSubida,
                        IdFur,
                        FechaEntrega,
                        DireccionEntrega,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdEstadoRegistroMaterial 
                    FROM 
                        ope.T_MaterialPespecificoDetalle
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MaterialPespecificoDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPED-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 08/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialPespecificoDetalle por la lista de ids
        /// </summary>
        /// <returns> List<MaterialPespecificoDetalleDTO> </returns>
        public List<MaterialPespecificoDetalle> ObtenerPorIds(List<int> ids)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        IdMaterialPEspecifico AS IdMaterialPespecifico,
                        IdMaterialVersion,
                        IdMaterialEstado,
                        NombreArchivo,
                        UrlArchivo,
                        UsuarioEnvio,
                        FechaEnvio,
                        UsuarioAprobacion,
                        FechaAprobacion,
                        UsuarioSubida,
                        FechaSubida,
                        ComentarioSubida,
                        IdFur,
                        FechaEntrega,
                        DireccionEntrega,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdEstadoRegistroMaterial 
                    FROM 
                        ope.T_MaterialPespecificoDetalle
                
                WHERE Estado = 1 AND Id IN @ids";


                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<MaterialPespecificoDetalle>>(resultado)!;
                }
                return new List<MaterialPespecificoDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPED-OPIs-002@Error en ObtenerPorIds() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de materialpespecifico
        /// </summary>
        /// <param name="idMaterialPEspecifico">Id del material del PEspecifico (PK de la tabla ope.T_MaterialPEspecifico)</param>
        /// <param name="idMaterialAccion">Id de la accion material(PK de la tabla ope.T_MaterialAccion)</param>
        /// <param name="idMaterialVersion">Id de la version del material (PK de la tabla ope.T_MaterialVersion)</param>
        /// <returns>Lista de objetos de clase MaterialPEspecificoDetalleCriteriosDTO</returns>
        public IEnumerable<MaterialPEspecificoDetalleCriterioDTO> ObtenerDetalleMaterialPEspecifico(int idMaterialPEspecifico, int idMaterialAccion, int idMaterialVersion)
        {
            try
            {
                var query = @"SELECT IdMaterialPEspecificoDetalle,
                                       IdMaterialPEspecifico,
                                       IdMaterialAccion,
                                       IdMaterialVersion
                                FROM ope.V_TMaterialPEspecificoDetalle_ObtenerMaterialPEsécificoDetalle
                                WHERE Estado = 1
                                      AND IdMaterialPEspecifico = @IdMaterialPEspecifico
                                      AND IdMaterialAccion = @IdMaterialAccion
                                      AND IdMaterialVersion = @IdMaterialVersion;";
                var resultadoDB = _dapperRepository.QueryDapper(query, new { IdMaterialPEspecifico = idMaterialPEspecifico, IdMaterialAccion = idMaterialAccion, IdMaterialVersion = idMaterialVersion });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<MaterialPEspecificoDetalleCriterioDTO>>(resultadoDB);

                return new List<MaterialPEspecificoDetalleCriterioDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-07
        /// <summary>
        /// Obtiene de T_MaterialPespecificoDetalle filtrado por mateial pespecifico version y estado
        /// </summary>
        /// <param name="idMaterialPEspecifico"></param>
        /// <param name="idMaterialVersion"></param>
        /// <param name="idMaterialEstado"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<MaterialPespecificoDetalle> ObtenerPorMaterial (List<int> idMaterialPEspecifico, List<int> idMaterialVersion, List<int> idMaterialEstado)
        {
            try
            {
                var materialPEspecifico = idMaterialPEspecifico;
                var materialVersion = idMaterialVersion;
                var materialEstado = idMaterialEstado;

                var query = @"
                    SELECT
                        Id,
                        IdMaterialPEspecifico AS IdMaterialPespecifico,
                        IdMaterialVersion,
                        IdMaterialEstado,
                        NombreArchivo,
                        UrlArchivo,
                        UsuarioEnvio,
                        FechaEnvio,
                        UsuarioAprobacion,
                        FechaAprobacion,
                        UsuarioSubida,
                        FechaSubida,
                        ComentarioSubida,
                        IdFur,
                        FechaEntrega,
                        DireccionEntrega,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdEstadoRegistroMaterial 
                    FROM 
                        ope.T_MaterialPespecificoDetalle
                    WHERE IdMaterialPEspecifico in @MaterialPEspecifico
                      AND IdMaterialVersion in @MaterialVersion
                      AND IdMaterialEstado in @MaterialEstado
                      AND UrlArchivo is not null and UrlArchivo != ''";

                var parametros = new
                {
                    MaterialPEspecifico = materialPEspecifico,
                    MaterialVersion = materialVersion,
                    MaterialEstado = materialEstado
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<MaterialPespecificoDetalle>>(resultado)!;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPED-OPI-001@Error en ObtenerPorMaterial() {ex.Message}", ex);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-08
        /// <summary>
        /// Obtiene detalle de materialpespecifico
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        public MaterialPEspecificoDetalleEnvioProveedorDTO ObtenerDetalleMaterialPEspecificoEnviarProveedor(int id)
        {
            try
            {
                MaterialPEspecificoDetalleEnvioProveedorDTO valor = new MaterialPEspecificoDetalleEnvioProveedorDTO();
                var query = "ope.SP_ObtenerDetalleEnvioProveedorImpresion";
                var resultadoDB = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMaterialPEspecificoDetalle = id });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    valor = JsonConvert.DeserializeObject<MaterialPEspecificoDetalleEnvioProveedorDTO>(resultadoDB);
                }
                return valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-09-14
        /// <summary>
        /// Obtiene el fur asociado
        /// </summary>
        /// <param name="idMaterialPEspecificoDetalle"></param>
        /// <returns></returns>
        public AsociarActualizarFurMaterialVersionDTO ObtenerFurAsociadoPorIdPEspecificoDetalle(int idMaterialPEspecificoDetalle)
        {
            try
            {
                var _resultado = new AsociarActualizarFurMaterialVersionDTO();
                var query = $@"ope.SP_ObtenerFurAsociadoMaterialProgramaEspecifico";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<AsociarActualizarFurMaterialVersionDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
		/// Obtiene detalle de fur de un materialpespecifico detalle
		/// </summary>
		/// <param name="idMaterialPEspecificoDetalle"></param>
		/// <returns></returns>
		public MaterialPEspecificoDetalleFurDTO ObtenerDetalleFur(int idMaterialPEspecificoDetalle)
        {
            try
            {
                MaterialPEspecificoDetalleFurDTO lista = new MaterialPEspecificoDetalleFurDTO();
                var query = "SELECT IdMaterialPEspecificoDetalle,IdFur,IdProveedor,IdProducto,Monto,Cantidad,NombrePlural,Simbolo,FechaEntrega,DireccionEntrega FROM ope.V_TMaterialPEspecificoDetalle_ObtenerDetalleFur WHERE Estado = 1 AND IdMaterialPEspecificoDetalle = @IdMaterialPEspecificoDetalle";
                var resultadoDB = _dapperRepository.FirstOrDefault(query, new { IdMaterialPEspecificoDetalle = idMaterialPEspecificoDetalle });
                if (!string.IsNullOrEmpty(resultadoDB) && !resultadoDB.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<MaterialPEspecificoDetalleFurDTO>(resultadoDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}



