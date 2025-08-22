using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CargoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 12/07/2023
    /// <summary>
    /// Gestión general de T_TipoVista
    /// </summary>
    public class TipoVistumRepository : GenericRepository<TTipoVistum>, ITipoVistumRepository
    {
        private IUnitOfWork _unitOfWork;

        public TipoVistumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/07/2023
        /// <summary>
        /// Obtiene el combo de T_TipoVista
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            string _query = "Select Id,Nombre From pla.T_TipoVista Where Estado=1";
            string query = _dapperRepository.QueryDapper(_query, null);
            if (!string.IsNullOrEmpty(query) && !query.Equals("[]"))
                return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(query);

            return new List<ComboDTO>();

        }
    }
}
