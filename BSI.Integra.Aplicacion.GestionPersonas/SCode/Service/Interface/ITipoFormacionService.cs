using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ITipoFormacionService
    {
        IEnumerable<TipoFormacionDTO> Obtener();
        TipoFormacionDTO Insertar(TipoFormacionDTO dto, string usuario);
        TipoFormacionDTO Actualizar(TipoFormacionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
