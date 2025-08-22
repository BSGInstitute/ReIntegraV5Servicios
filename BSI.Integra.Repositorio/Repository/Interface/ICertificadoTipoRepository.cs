using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public partial interface ICertificadoTipoRepository : IGenericRepository<TCertificadoTipo>
    {
        List<ComboDTO> ObtenerCombo();
    }
}
