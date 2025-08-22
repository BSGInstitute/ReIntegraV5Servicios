using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: SeccionPwRepository
    /// Autor: Gilmer Qm
    /// Fecha: 23/06/2023
    /// <summary>
    /// Gestión general de T_SeccionPw
    /// </summary>
    public class SeccionPwRepository : GenericRepository<TSeccionPw>, ISeccionPwRepository
    {
        public SeccionPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de Secciones por el IdPlantillaPW.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SeccionPlantillaContenidoDTO> ObtenerSeccionesPorIdPlantillaPW(int idPlantillaPw)
        {
            try
            {
                var _query = @"SELECT Id,
                                   Nombre,
                                   Descripcion,
                                   Contenido,
                                   IdPlantillaPw,
                                   IdPlantilla,
                                   VisibleWeb,
                                   ZonaWeb,
                                   OrdenEeb,
                                   Titulo,
                                   Posicion,
                                   Tipo,
                                   IdSeccionTipoContenido,
                                   NombreSeccionTipoContenido,
                                   IdSeccionTipoDetallePw,
                                   NombreSubSeccion,
                                   IdSubSeccionTipoContenido
                            FROM pla.V_ObtenerSeccionesPlantillaPorIdPlantilla_PlantillaPw_Plantilla
                            WHERE Estado = 1
                                  AND IdPlantillaPw = @IdPlantillaPw;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<SeccionPlantillaContenidoDTO>>(respuestaDapper);

                return new List<SeccionPlantillaContenidoDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Seccion_PW Asociados al IdPlantilla.
        /// </summary>
        /// <param name="idPlantillaPw"> (PK) de T_Plantilla_PW </param>
        /// <returns> IEnumerable<PlantillaPais> </returns>
        public IEnumerable<SeccionPw> ObtenerPorIdPlantillaPw(int idPlantillaPw)
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Descripcion,
                                   Contenido,
                                   IdPlantillaPw,
                                   VisibleWeb,
                                   ZonaWeb,
                                   OrdenEeb,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdSeccionTipoContenido
                            FROM pla.T_Seccion_PW
                            WHERE Estado = 1
                                  AND IdPlantillaPw = @IdPlantillaPw;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SeccionPw>>(resultado);
                }
                return new List<SeccionPw>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Seccion_PW por el Id.
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> SeccionPw </returns>
        public SeccionPw ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Descripcion,
                                   Contenido,
                                   IdPlantillaPw,
                                   VisibleWeb,
                                   ZonaWeb,
                                   OrdenEeb,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdSeccionTipoContenido
                            FROM pla.T_Seccion_PW
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<SeccionPw>(resultado);
                }
                return new SeccionPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la plantilla por medio del idPlantilla
        /// </summary>
        /// <param name="idPlantillaPw"></param>
        /// <returns> Lista DTO - List<SeccionPwFiltroPlantillaPwDTO> </returns>
        public IEnumerable<SeccionPwFiltroPlantillaPwDTO> ObtenerPlantillaSeccionesPorIdPlantillaPW(int idPlantillaPw)
        {
            try
            {
                var query = @"SELECT 
                                Id, Nombre, Descripcion, Contenido, IdPlantillaPW, IdPlantilla, VisibleWeb, ZonaWeb, OrdenEeb, Titulo, Posicion, Tipo, 
                                IdSeccionTipoContenido, NombreSeccionTipoContenido, IdSeccionTipoDetallePw,NombreSubSeccion,IdSubSeccionTipoContenido 
                            FROM 
                                pla.V_ObtenerSeccionesPlantillaPorIdPlantilla 
                            WHERE Estado = 1 and IdPlantillaPW = @IdPlantillaPw";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SeccionPwFiltroPlantillaPwDTO>>(resultado)!;
                }

                return new List<SeccionPwFiltroPlantillaPwDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#SPwR-OPSPIPPw-001@Error en ObtenerPlantillaSeccionesPorIdPlantillaPW() {ex.Message}", ex);
            }
        }
    }
}
