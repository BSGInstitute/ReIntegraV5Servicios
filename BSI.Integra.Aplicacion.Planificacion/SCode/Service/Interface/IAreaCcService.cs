using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAreaCcService
    {
        AreaCcDTO Actualizar(AreaCcDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        AreaCcDTO Insertar(AreaCcDTO dto, string usuario);
        AreaCcDTO ObtenerPorId(int id);

        List<AreaCcDTO> Obtener();
    }
}
