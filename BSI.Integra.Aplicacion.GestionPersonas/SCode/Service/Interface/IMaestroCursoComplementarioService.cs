using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IMaestroCursoComplementarioService 
    {
        IEnumerable<CursoComplementarioDTO> Obtener();
        IEnumerable<TipoCursoComplementarioDTO> ObtenerCombos();
        CursoComplementarioFiltroDTO Insertar(CursoComplementarioFiltroDTO dto, string usuario);
        CursoComplementarioFiltroDTO Actualizar(CursoComplementarioFiltroDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
