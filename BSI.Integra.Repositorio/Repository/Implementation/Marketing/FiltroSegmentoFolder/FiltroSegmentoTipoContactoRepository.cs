using AutoMapper;
using BSI.Integra.Aplicacion.DTO; 
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing. FiltroSegmentoTipoContacto;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing. FiltroSegmentoTipoContacto
{
    public class FiltroSegmentoTipoContactoRepository : GenericRepository<TSendinblueContacto>, IFiltroSegmentoTipoContactoRepository
    {
        public FiltroSegmentoTipoContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository):base(context, connectionFactory, dapperRepository)
        {
        }

        public List<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT Id, Nombre FROM conf.T_FiltroSegmentoTipoContacto where Estado = 1 and id <>2";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
