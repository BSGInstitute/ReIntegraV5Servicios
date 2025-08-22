using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaEncuestaOnlineRepository : IGenericRepository<TPreguntaEncuestaOnline>
    {
        #region Metodos Base
        TPreguntaEncuestaOnline Add(PreguntaEncuestaOnline entidad);
        TPreguntaEncuestaOnline Update(PreguntaEncuestaOnline entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaEncuestaOnline> Add(IEnumerable<PreguntaEncuestaOnline> listadoEntidad);
        IEnumerable<TPreguntaEncuestaOnline> Update(IEnumerable<PreguntaEncuestaOnline> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        PreguntaEncuestaOnline ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();

        List<PreguntaAsociadaEncuestaOnlineDTO> ObtenerPreguntaEncuestaOnline(int idEncuestaOnline);
    }
}
