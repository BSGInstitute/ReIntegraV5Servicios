using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ITranscripcionLlamadaService
    {
        #region Metodos Base
        TranscripcionLlamada Add(TranscripcionLlamada entidad);
        TranscripcionLlamada Update(TranscripcionLlamada entidad);
        bool Delete(int id, string usuario);
        List<TranscripcionLlamada> Add(List<TranscripcionLlamada> listadoEntidad);
        List<TranscripcionLlamada> Update(List<TranscripcionLlamada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
