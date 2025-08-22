using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteProcesoSeleccionRepository : IGenericRepository<TPostulanteProcesoSeleccion>
    {
        #region Metodos Base
        TPostulanteProcesoSeleccion Add(PostulanteProcesoSeleccion entidad);
        TPostulanteProcesoSeleccion Update(PostulanteProcesoSeleccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteProcesoSeleccion> Add(IEnumerable<PostulanteProcesoSeleccion> listadoEntidad);
        IEnumerable<TPostulanteProcesoSeleccion> Update(IEnumerable<PostulanteProcesoSeleccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteProcesoSeleccion? ObtenerPorId(int id);
        PostulanteAccesoProcesoSeleccionDTO? VerificacionTokenPresenteInactivo(int idPostulanteProcesoSeleccion);
        PostulanteAccesoProcesoSeleccionDTO? ObtenerPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion);
        PostulanteProcesoSeleccion? ObtenerPorIdPostulante(int idPostulante);
        List<ProcesoSeleccionInscritoDTO> ObtenerProcesoSeleccionInscrito(int idPostulante);
        bool EliminarProcesoSeleccionAsociado(int idPostulante, int idProcesoSeleccion);

        //p
        PostulanteAccesoProcesoSeleccionDTO? VerificacionTokenPresente(int idPostulanteProcesoSeleccion);
    }
}
