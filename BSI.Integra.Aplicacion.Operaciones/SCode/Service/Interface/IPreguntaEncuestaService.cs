using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IPreguntaEncuestaService
    {
        #region Metodos Base
        PreguntaEncuesta Add(PreguntaEncuesta entidad);
        PreguntaEncuesta Update(PreguntaEncuesta entidad);
        bool Delete(int id, string usuario);
        List<PreguntaEncuesta> Add(List<PreguntaEncuesta> listadoEntidad);
        List<PreguntaEncuesta> Update(List<PreguntaEncuesta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        List<BancoPreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronicaPorId(int idEncuesta);
        List<PreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronica();
    }
}
