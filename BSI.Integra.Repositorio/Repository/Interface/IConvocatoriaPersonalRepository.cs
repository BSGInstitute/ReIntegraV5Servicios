using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConvocatoriaPersonalRepository : IGenericRepository<TConvocatoriaPersonal>
    {
        #region Metodos Base
        TConvocatoriaPersonal Add(ConvocatoriaPersonal entidad);
        TConvocatoriaPersonal Update(ConvocatoriaPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConvocatoriaPersonal> Add(IEnumerable<ConvocatoriaPersonal> listadoEntidad);
        IEnumerable<TConvocatoriaPersonal> Update(IEnumerable<ConvocatoriaPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public List<ConvocatoriaPersonalDTO> ObtenerConvocatoriasRegistradas();
        public ConvocatoriaPersonalDTO ObtenerConvocatoriasRegistradaById(int Id);
        public List<int> ObtenerIdsIdioma(int IdConvocatoria);
        public List<int> ObtenerIdsNivelEstudio(int IdConvocatoria);
        public List<int> ObtenerIdsExperiencia(int IdConvocatoria);
        public bool InsertarDetalleConvocatorias(int IdConvocatoriaPersonal, string Usuario, string? IdsNivelEstudio, string? IdsExperiencia, string? IdsIdioma);
        IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> ObtenerComboComvocatoriaPersonal();
    }
}
