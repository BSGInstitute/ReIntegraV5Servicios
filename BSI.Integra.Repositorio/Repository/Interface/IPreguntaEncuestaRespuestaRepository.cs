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
    public interface IPreguntaEncuestaRespuestaRepository : IGenericRepository<TPreguntaEncuestaRespuestum>
    {
        #region Metodos Base
        TPreguntaEncuestaRespuestum Add(PreguntaEncuestaRespuesta entidad);
        TPreguntaEncuestaRespuestum Update(PreguntaEncuestaRespuesta entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaEncuestaRespuestum> Add(IEnumerable<PreguntaEncuestaRespuesta> listadoEntidad);
        IEnumerable<TPreguntaEncuestaRespuestum> Update(IEnumerable<PreguntaEncuestaRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        PreguntaEncuestaRespuesta ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();

        List<PreguntaEncuestaRespuestaDTO> ObtenerPreguntaEncuestaRespuesta();
        List<PreguntaRespuestaDTO> ObtenerRespuestaPregunta(int idPregunta);
    }
}
