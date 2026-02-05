using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface IRecomendacionService
    {
        #region Metodos Base
        RecomendacionTranscripcion Add(RecomendacionTranscripcion entidad);
        RecomendacionTranscripcion Update(RecomendacionTranscripcion entidad);
        bool Delete(int id, string usuario);
        List<RecomendacionTranscripcion> Add(List<RecomendacionTranscripcion> listadoEntidad);
        List<RecomendacionTranscripcion> Update(List<RecomendacionTranscripcion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
