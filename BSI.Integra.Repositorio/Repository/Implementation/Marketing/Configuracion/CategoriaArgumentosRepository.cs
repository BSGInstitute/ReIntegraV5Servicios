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

        public List<CategoriaArgumento> ObtenerListadoCategoriaArgumento()
        {
            try
            {
                List<CategoriaArgumento> resultado = new List<CategoriaArgumento>();

                var query = "SELECT Id, Nombre, UsuarioModificacion, FechaModificacion FROM [mkt].[T_ConfiguracionCategoriaArgumento] WHERE Estado = 1";

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

                var query = "INSERT INTO [mkt].[T_ConfiguracionCategoriaArgumento] " +
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

                var query = "UPDATE [mkt].[T_ConfiguracionCategoriaArgumento] " +
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
                var query = "UPDATE [mkt].[T_ConfiguracionCategoriaArgumento] " +
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
