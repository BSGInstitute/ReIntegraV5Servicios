using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IClasificacionPersonaService
    {
        #region Metodos Base
        ClasificacionPersona Add(ClasificacionPersona entidad);
        ClasificacionPersona Update(ClasificacionPersona entidad);
        bool Delete(int id, string usuario);

        List<ClasificacionPersona> Add(List<ClasificacionPersona> listadoEntidad);
        List<ClasificacionPersona> Update(List<ClasificacionPersona> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ClasificacionPersonaDTO> ObtenerClasificacionPersona();
        IEnumerable<ClasificacionPersonaComboDTO> ObtenerCombo();
        ValorIntDTO IdClasificacionPersonaPorTipoYIdAlumno(int tipo, int idAlumno);
    }
}
