using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: AdicionalProgramaGeneralRepository
    /// Autor: GIlmer Quispe.
    /// Fecha: 19/06/2023
    /// <summary>
    /// Gestión general de T_AdicionalProgramaGeneral
    /// </summary>
    public class AdicionalProgramaGeneralRepository : GenericRepository<TAdicionalProgramaGeneral>, IAdicionalProgramaGeneralRepository
    {
        private Mapper _mapper;

        public AdicionalProgramaGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista Descripciones Adicionales  para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns></returns>
        public IEnumerable<AdicionalProgramaGeneralDTO> ObtenerAdicionalProgramaGeneralPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = "SELECT Id,IdPGeneral AS IdPgeneral,Descripcion,NombreImagen,IdTitulo,NombreTitulo FROM pla.V_TAdicionalProgramaGeneral WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<AdicionalProgramaGeneralDTO>>(resultado)!;
                }
                return new List<AdicionalProgramaGeneralDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
