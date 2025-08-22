using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAgendaTipoUsuarioRepository
    {
        IEnumerable<AgendaTipoUsuarioDTO> ObtenerTipoUsuarioFiltro();
    }
}
