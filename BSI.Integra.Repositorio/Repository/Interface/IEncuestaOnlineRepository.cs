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

    public interface IEncuestaOnlineRepository : IGenericRepository<TEncuestaOnline>
    {
        #region Metodos Base
        TEncuestaOnline Add(EncuestaOnline entidad);
        TEncuestaOnline Update(EncuestaOnline entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TEncuestaOnline> Add(IEnumerable<EncuestaOnline> listadoEntidad);
        IEnumerable<TEncuestaOnline> Update(IEnumerable<EncuestaOnline> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        EncuestaOnline ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        List<EncuestaRegistradaDTO> ObtenerEncuestaOnline();
        List<VersionEncuestaSincronicaDTO> ObtenerVersionEncuestaSincronico();
        //List<DatoEstructuraCursoAsincronicaDTO> ObtenerDatosSesionAsincronica(int IdPGeneral);
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
