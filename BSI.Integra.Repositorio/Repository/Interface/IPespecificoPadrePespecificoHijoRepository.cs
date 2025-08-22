using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPespecificoPadrePespecificoHijoRepository : IGenericRepository<TPespecificoPadrePespecificoHijo>
    {
        #region Metodos Base
        TPespecificoPadrePespecificoHijo Add(PespecificoPadrePespecificoHijo entidad);
        TPespecificoPadrePespecificoHijo Update(PespecificoPadrePespecificoHijo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoPadrePespecificoHijo> Add(IEnumerable<PespecificoPadrePespecificoHijo> listadoEntidad);
        IEnumerable<TPespecificoPadrePespecificoHijo> Update(IEnumerable<PespecificoPadrePespecificoHijo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DatosPEspecificoHijoDTO> ObtenerDetallePEspecificoHijosPorIdPespecificoPadre(int idPespecifico);
        IEnumerable<InformacionPespecificoHijoDTO> ObtenerPespecificosRelacionados(int idPespecifico);
        PespecificoPadrePespecificoHijo? ObtenerPorIdPadreIdHijo(int pespecificoPadreId, int pespecificoHijoId);
        PespecificoPadrePespecificoHijo? ObtenerPorPEspecificoHijoId(int idPespecificoHijoId);
        IEnumerable<PespecificoPadrePespecificoHijo> ObtenerPorPEspecificoPadreId(int idPespecificoPadreId);
        PespecificoPadrePespecificoHijo? ObtenerPespecificoPadrePorId(int idPespecificoPadre);
        IEnumerable<InformacionPespecificoHijoDTO> ObtenerInformacionPespecificoSesion(int idPespecifico);
        IEnumerable<InformacionPespecificoHijoDTO> ObtenerInformacionPespecificosHijos(int idPespecifico);
    }
}
