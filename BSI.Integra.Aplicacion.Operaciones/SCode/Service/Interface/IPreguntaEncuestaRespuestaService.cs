using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IPreguntaEncuestaRespuestaService
    {
        #region Metodos Base
        PreguntaEncuestaRespuesta Add(PreguntaEncuestaRespuesta entidad);
        PreguntaEncuestaRespuesta Update(PreguntaEncuestaRespuesta entidad);
        bool Delete(int id, string usuario);
        List<PreguntaEncuestaRespuesta> Add(List<PreguntaEncuestaRespuesta> listadoEntidad);
        List<PreguntaEncuestaRespuesta> Update(List<PreguntaEncuestaRespuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
