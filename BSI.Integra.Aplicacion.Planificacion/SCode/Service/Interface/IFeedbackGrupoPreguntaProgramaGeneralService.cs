using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFeedbackGrupoPreguntaProgramaGeneralService
    {
        void EliminacionLogicoPorIdGrupoPreguntaPGeneral(int idGrupoPreguntaPG, string usuario, IEnumerable<int> nuevos);
    }
}
