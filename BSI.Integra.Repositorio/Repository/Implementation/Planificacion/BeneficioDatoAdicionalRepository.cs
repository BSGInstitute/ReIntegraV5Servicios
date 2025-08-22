using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: BeneficioDatoAdicionalRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 09/06/2023
    /// <summary>
    /// Gestión general de T_BeneficioDatoAdicional
    /// </summary>
    public class BeneficioDatoAdicionalRepository : GenericRepository<TBeneficioDatoAdicional>, IBeneficioDatoAdicionalRepository
    {
        public BeneficioDatoAdicionalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de T_ParametroSeoPw
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string _query = @"SELECT Id,
                                       Nombre
                                FROM pla.T_BeneficioDatoAdicional
                                WHERE Estado = 1;";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(queryRespuesta)!;
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
