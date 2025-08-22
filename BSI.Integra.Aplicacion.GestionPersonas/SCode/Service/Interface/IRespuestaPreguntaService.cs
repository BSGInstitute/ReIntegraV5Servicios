using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IRespuestaPreguntaService
    {
        #region Metodos Base
        RespuestaPregunta Add(RespuestaPregunta entidad);
        RespuestaPregunta Update(RespuestaPregunta entidad);
        bool Delete(int id, string usuario);
        List<RespuestaPregunta> Add(List<RespuestaPregunta> listadoEntidad);
        List<RespuestaPregunta> Update(List<RespuestaPregunta> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RespuestaPreguntaFactorDesaprovatorioComboDTO> ObtenerFactorDesaprovatorio();
        List<PreguntaRespuestaAsincronicaDTO> ObtenerRespuestaPregunta(int idPregunta);
    }
}
