using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IEncuestaOnlineService
    {
        #region Metodos Base
        EncuestaOnline Add(EncuestaOnline entidad);
        EncuestaOnline Update(EncuestaOnline entidad);
        bool Delete(int id, string usuario);
        List<EncuestaOnline> Add(List<EncuestaOnline> listadoEntidad);
        List<EncuestaOnline> Update(List<EncuestaOnline> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<VersionEncuestaSincronicaDTO> ObtenerVersionEncuestaSincronico();

        //List<CapituloDTO> ObtenerDatosSesionAsincronica(int IdPGeneral);
        List<EncuestaEstructuraAsincronicaDTO> ObtenerEncuestaAsincronicaAsignada(int IdPGeneral);
        List<ComboDTO> ObtenerEncuestaAsincronica();
        bool InsertarEncuestaSesionProgramaAsincronica(EncuestaAsincronicaDTO encuesta);
        bool EliminarEncuestaAsincronicaAsignada(int Id, string Usuario);
        bool ExisteEncuestaOnlineTipoEncuestaVersion(int? idTipoEncuesta, int? version);
        List<PreguntaExamenAsincronicaDTO> InsertarEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica);
        bool InsertarListaPreguntaAsincronica(List<PreguntaExamenAsincronicaDTO> encuestaAsincronicaEntradaDTO);
        bool InsertarPreguntaEncuestaAsincronica(PreguntaExamenAsincronicaDTO encuestaAsincronicaEntradaDTO);
        bool DeletePreguntaEncuestaAsincronica(int id, string usuario);
        bool DeleteEncuestaAsincronica(int id, string usuario);
        bool UpdateEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica);


    }
}
