using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ITipoVistumRepository : IGenericRepository<TTipoVistum>
    {
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
