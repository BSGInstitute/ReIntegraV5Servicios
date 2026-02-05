using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IRecomendacionRepository: IGenericRepository<TRecomendacionTranscripcion>
    {
        #region Metodos Base
        TRecomendacionTranscripcion Add(RecomendacionTranscripcion entidad);
        TRecomendacionTranscripcion Update(RecomendacionTranscripcion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRecomendacionTranscripcion> Add(IEnumerable<RecomendacionTranscripcion> listadoEntidad);
        IEnumerable<TRecomendacionTranscripcion> Update(IEnumerable<RecomendacionTranscripcion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
