using BSI.Integra.Persistencia.Entidades.IntegraDB;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPersonaService
    {
        #region Metodos Base
        Persona Add(Persona entidad);
        Persona Update(Persona entidad);
        bool Delete(int id, string usuario);

        List<Persona> Add(List<Persona> listadoEntidad);
        List<Persona> Update(List<Persona> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        Persona ObtenerPorId(int idPersona);

        int? InsertarPersona(int idTablaOriginal, TipoPersona tipoPersona, string usuario);

    }
}