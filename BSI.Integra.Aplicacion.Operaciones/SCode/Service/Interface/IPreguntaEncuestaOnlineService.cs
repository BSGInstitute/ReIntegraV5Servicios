using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IPreguntaEncuestaOnlineService
    {
        #region Metodos Base
        PreguntaEncuestaOnline Add(PreguntaEncuestaOnline entidad);
        PreguntaEncuestaOnline Update(PreguntaEncuestaOnline entidad);
        bool Delete(int id, string usuario);
        List<PreguntaEncuestaOnline> Add(List<PreguntaEncuestaOnline> listadoEntidad);
        List<PreguntaEncuestaOnline> Update(List<PreguntaEncuestaOnline> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
