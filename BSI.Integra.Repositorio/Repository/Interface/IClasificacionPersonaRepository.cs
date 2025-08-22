using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IClasificacionPersonaRepository : IGenericRepository<TClasificacionPersona>
    {
        #region Metodos Base
        TClasificacionPersona Add(ClasificacionPersona entidad);
        TClasificacionPersona Update(ClasificacionPersona entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TClasificacionPersona> Add(IEnumerable<ClasificacionPersona> listadoEntidad);
        IEnumerable<TClasificacionPersona> Update(IEnumerable<ClasificacionPersona> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ClasificacionPersonaDTO> ObtenerClasificacionPersona();
        IEnumerable<ClasificacionPersonaComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerIdClasificacionPersonaPorTipoYIdAlumno(int tipo, int idAlumno);
        bool ExistePorIdPersonaTipoPersona(int idPersona, TipoPersona tipoPersona);
        ClasificacionPersona ObtenerPorIdPersonaTipoPersona(int idPersona, TipoPersona tipoPersona);
        ValorIntDTO ObtenerPorTablaOriginalPorTipoPersonaPorPersona(int idTablaOriginal, TipoPersona tipoPersona, int idPersona);
        ClasificacionPersona ObtenerPorIdAlumno(int idAlumno);
        ClasificacionPersona ObtenerPorIdTablaOriginalIdPersonaTipoPersona(int idTablaOriginal, TipoPersona tipoPersona, int idPersona);
        public ClasificacionPersona ObtenerPorIdAlumnoYTipoPersona(int idTablaOriginal, TipoPersona tipoPersona);
        ClasificacionPersona? ObtenerPorIdPersonaTipoAlumno(int idPersona);
        ClasificacionPersona? ObtenerPorPersonalYTipoPersona(int idPersona, int tipoPersona);
        ClasificacionPersona ObtenerPorId(int id);
    }
}