using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ICategoriaPreguntaService
    {
        IEnumerable<CategoriaPreguntaDTO> Obtener();
        CategoriaPreguntaDTO Insertar(CategoriaPreguntaDTO dto, string usuario);
        CategoriaPreguntaDTO Actualizar(CategoriaPreguntaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

    }
}
