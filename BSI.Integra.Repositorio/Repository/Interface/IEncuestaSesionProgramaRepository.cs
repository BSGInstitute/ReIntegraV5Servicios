using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEncuestaSesionProgramaRepository : IGenericRepository<TEncuestaSesionPrograma>
    {
        #region Metodos Base
        TEncuestaSesionPrograma Add(EncuestaSesionPrograma entidad);
        TEncuestaSesionPrograma Update(EncuestaSesionPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TEncuestaSesionPrograma> Add(IEnumerable<EncuestaSesionPrograma> listadoEntidad);
        IEnumerable<TEncuestaSesionPrograma> Update(IEnumerable<EncuestaSesionPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        EncuestaSesionPrograma ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();

        List<EncuestaProgramaDTO> ObtenerEncuestasPrograma(int idPespecifico);
        List<EncuestaSesionAsignadaDTO> ObtenerEncuestaAsignada(int idPespecificoSesion);
    }
}
