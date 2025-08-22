using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: TipoMarcadorRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 12/07/2023
    /// <summary>
    /// Gestión general de T_TipoMarcador
    /// </summary>
    public class TipoMarcadorRepository : GenericRepository<TTipoMarcador>, ITipoMarcadorRepository
    {
        public TipoMarcadorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/07/2023
        /// <summary>
        /// Obtiene el combo
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            string _query = "Select Id,Nombre From ope.T_TipoMarcador Where Estado=1";
            string query = _dapperRepository.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Equals("[]"))
                return JsonConvert.DeserializeObject<List<ComboDTO>>(query);
            return new List<ComboDTO>();
        }
    }
}
