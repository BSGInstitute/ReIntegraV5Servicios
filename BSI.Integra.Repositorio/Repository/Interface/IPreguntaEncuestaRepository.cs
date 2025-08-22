using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaEncuestaRepository : IGenericRepository<TPreguntaEncuestum>
    {
        #region Metodos Base
        TPreguntaEncuestum Add(PreguntaEncuesta entidad);
        TPreguntaEncuestum Update(PreguntaEncuesta entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaEncuestum> Add(IEnumerable<PreguntaEncuesta> listadoEntidad);
        IEnumerable<TPreguntaEncuestum> Update(IEnumerable<PreguntaEncuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        PreguntaEncuesta ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        List<BancoPreguntaEncuestaDTO> ObtenerPreguntaEncuesta();
        List<PreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronica();
        List<BancoPreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronicaPorId(int idEncuesta);

    }
}
