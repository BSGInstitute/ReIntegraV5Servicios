using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Configuracion
{
    public class CategoriaArgumentosRepository : ICategoriaArgumentosRepository
    {
        private IDapperRepository _dapperRepository;

        public CategoriaArgumentosRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

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

        public bool EditarProgramaConfigurado(EditarProgramaConfiguradoDTO request, string usuario)
        {
            try
            {
                List<ProgramaConfigurado> resultado = new List<ProgramaConfigurado>();

                var query = "UPDATE [mkt].[T_ProgramaGeneralConfigurado] " +
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

        public bool EliminarProgramaConfigurado(int id, string usuario)
        {
            try
            {
                var query = "UPDATE [mkt].[T_ProgramaGeneralConfigurado] " +
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

                // Construcción jerárquica
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
                                .OrderBy(a => a.PrioridadArgumento) // orden en backend
                                .Select(a => new ArgumentoDTO
                                {
                                    Id = a.ArgumentoId,
                                    Nombre = a.NombreArgumento,
                                    Descripcion = a.DescripcionArgumento,
                                    Prioridad = a.PrioridadArgumento
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

        public bool EditarArgumentoPorCategoria(CrearArgumentoPorCategoriaDTO request, string usuario)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EliminarArgumentoPorCategoria(int id, string usuario)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }











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
