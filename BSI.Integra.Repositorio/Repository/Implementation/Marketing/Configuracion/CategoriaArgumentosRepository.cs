using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Configuracion
{
    public class CategoriaArgumentosRepository : ICategoriaArgumentosRepository
    {
        private IDapperRepository _dapperRepository;

        public CategoriaArgumentosRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de programas configurados
        /// </summary>
        /// <returns>Listado de programas configurados</returns>
        public List<ProgramaConfigurado> ObtenerListadoProgramaConfigurado()
        {
            try
            {
                List<ProgramaConfigurado> resultado = new List<ProgramaConfigurado>();

                var query = "SELECT Id, Nombre, UsuarioModificacion, FechaModificacion FROM [mkt].[T_ProgramaGeneralConfigurado] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ProgramaConfigurado>>(jsonResult);

                return resultado.OrderByDescending(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo registro para programa configurado
        /// </summary>
        /// <param name="request">Cuerpo para crear nuevo programa</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado creacion</returns>
        public int CrearProgramaConfigurado(CrearProgramaGeneralConfiguradoDTO request, string usuario)
        {
            try
            {
                var query = "INSERT INTO [mkt].[T_ProgramaGeneralConfigurado] " +
                                "([Nombre],[IdPGeneral],[Estado],[FechaCreacion],[FechaModificacion],[UsuarioCreacion],[UsuarioModificacion]) " +
                                "VALUES (@Nombre, @IdPGeneral , 1, GETDATE() , GETDATE(), @Usuario, @Usuario); " +
                                "SELECT CAST(SCOPE_IDENTITY() as int) as NewId;";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Nombre = request.Nombre, IdPGeneral = request.IdProgramaGeneral, Usuario = usuario });

                var resultado = JsonConvert.DeserializeObject<dynamic>(jsonResult);

                if (resultado != null && resultado.Count > 0)
                    return (int)resultado[0].NewId;

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina un programa configurado y sus detalles configurados
        /// </summary>
        /// <param name="id">id del programa</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado eliminacion</returns>
        public bool EliminarProgramaConfigurado(int id, string usuario)
        {
            try
            {
                var query = "[mkt].[SP_EliminarDetalleProgramaConfigurado]";

                var jsonResult = _dapperRepository.QuerySPDapper(query, new { IdProgramaConfigurado = id, UsuarioModificacion = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene un el detalle con jerarquia de una programa (programa, categorias y argumentos)
        /// </summary>
        /// <param name="id">Id del programa</param>
        /// <returns>Objeto programa con detalles</returns>
        public ProgramaConfiguradoDetalleDTO ObtenerProgramaConfiguradoDetalle(int id)
        {
            try
            {
                var query = "[mkt].[SP_ObtenerDetalleProgramaConfigurado]";

                var jsonResult = _dapperRepository.QuerySPDapper(query, new { IdProgramaGeneralConfigurado = id });

                // Deserializa resultado plano
                var flatResult = JsonConvert.DeserializeObject<List<ProgramaDetallePlano>>(jsonResult);

                if (flatResult == null || !flatResult.Any())
                    return null;

                // Construcción jerarquica
                var programa = new ProgramaConfiguradoDetalleDTO
                {
                    Id = flatResult.First().ProgramaId,
                    IdProgramaGeneral = flatResult.First().IdProgramaGeneral,
                    NombrePrograma = flatResult.First().NombrePrograma,
                    CategoriasArgumento = flatResult
                        .GroupBy(x => new { x.CategoriaId, x.NombreCategoria })
                        .Select(categoria => new CategoriaArgumentoPorProgramaDTO
                        {
                            Id = categoria.Key.CategoriaId,
                            Nombre = categoria.Key.NombreCategoria,
                            Argumentos = categoria
                                .Where(a => a.ArgumentoId.HasValue)
                                .OrderBy(a => a.PrioridadArgumento)
                                .Select(a => new ArgumentoDTO
                                {
                                    Id = a.ArgumentoId.Value,
                                    Nombre = a.NombreArgumento,
                                    Descripcion = a.DescripcionArgumento,
                                    Prioridad = a.PrioridadArgumento ?? 0
                                })
                                .ToList()
                        })
                        .ToList()
                };

                return programa;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo registro para argumento pro categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para crear nuevo argumento</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado creacion</returns>
        public bool AgregarArgumentoPorCategoria(CrearArgumentoPorCategoriaDTO request, string usuario)
        {
            try
            {
                request.UsuarioCreacion = usuario;
                var query = "[mkt].[SP_InsertarConfiguracionCategoriaArgumento]";

                var jsonResult = _dapperRepository.QuerySPDapper(query, request);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Editar un registro para argumento por categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para editar argumento</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado edicion</returns>
        public bool EditarArgumentoPorCategoria(EditarArgumentoPorCategoriaDTO request, string usuario)
        {
            try
            {
                request.UsuarioModificacion = usuario;
                var query = "UPDATE [mkt].[T_RemarketingArgumento] " +
                    "SET Nombre = @Nombre, Descripcion = @Descripcion, Prioridad = @Prioridad, " +
                        "UsuarioModificacion = @UsuarioModificacion, FechaModificacion = GETDATE()" +
                    "WHERE Id = @IdArgumento";

                var jsonResult = _dapperRepository.QueryDapper(query, request);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 09/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro para argumento por categoria especifica
        /// </summary>
        /// <param name="request">Cuerpo para eliminar argumento</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado eliminacion</returns>
        public bool EliminarArgumentoPorCategoria(EliminarArgumentoPorCategoriaDTO request, string usuario)
        {
            try
            {
                request.UsuarioModificacion = usuario;
                var query = "[mkt].[SP_EliminarArgumentoPorCategoria]";

                var jsonResult = _dapperRepository.QuerySPDapper(query, request);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 08/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado programas generales validos para crear registros programa
        /// </summary>
        /// <returns>Listado de programas generasles validos</returns>
        public List<ProgramaGeneralComboDTO> ObtenerListadoProgramaGeneralValido()
        {
            try
            {
                List<ProgramaGeneralComboDTO> resultado = new List<ProgramaGeneralComboDTO>();

                var query = "SELECT IdProgramaGeneral AS Id , Nombre FROM mkt.V_ObtenerPGeneralUrlVersion";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ProgramaGeneralComboDTO>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de categorias de argumento creadas
        /// </summary>
        /// <returns>Listado de categorias</returns>
        public List<CategoriaArgumento> ObtenerListadoCategoriaArgumento()
        {
            try
            {
                List<CategoriaArgumento> resultado = new List<CategoriaArgumento>();

                var query = "SELECT Id, Nombre, UsuarioModificacion, FechaModificacion FROM [mkt].[T_CategoriaArgumentoConfigurado] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<CategoriaArgumento>>(jsonResult);

                return resultado.OrderByDescending(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Crea una categoria argumento
        /// </summary>
        /// <param name="request">Cuerpo para crear nueva categoria</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado Creacion</returns>
        public bool CrearCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario)
        {
            try
            {
                List<CategoriaArgumento> resultado = new List<CategoriaArgumento>();

                var query = "INSERT INTO [mkt].[T_CategoriaArgumentoConfigurado] " +
                                "([Nombre],[Estado],[FechaCreacion],[FechaModificacion],[UsuarioCreacion],[UsuarioModificacion])" +
                                "VALUES (@Nombre , 1,GETDATE() , GETDATE(), @Usuario, @Usuario)";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Nombre = request.Nombre, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Edita una categoria argumento
        /// </summary>
        /// <param name="request">Cuerpo para edicion de categoria</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado edicion</returns>
        public bool EditarCategoriaArgumento(CrearEditarCategoriaArgumentoDTO request, string usuario)
        {
            try
            {
                List<CategoriaArgumento> resultado = new List<CategoriaArgumento>();

                var query = "UPDATE [mkt].[T_CategoriaArgumentoConfigurado] " +
                                "SET [Nombre] = @Nombre, [FechaModificacion] = GETDATE(), [UsuarioModificacion] = @Usuario " +
                                "WHERE Id = @Id";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Id = request.Id, Nombre = request.Nombre, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 07/01/2026
        /// Version: 1.0
        /// <summary>
        /// Elimina una categoria argumento
        /// </summary>
        /// <param name="id">id de la categoria</param>
        /// <param name="usuario">usuario edicion</param>
        /// <returns>Estado eliminacion</returns>
        public bool EliminarCategoriaArgumento(int id, string usuario)
        {
            try
            {
                var query = "UPDATE [mkt].[T_CategoriaArgumentoConfigurado] " +
                                "SET Estado = 0, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()" +
                                "WHERE Id = @Id";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Id = id, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
