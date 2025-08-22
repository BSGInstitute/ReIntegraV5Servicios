using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IConvocatoriaPersonalService
    {
        #region Metodos Base
        ConvocatoriaPersonalDTO Add(ConvocatoriaPersonalRecibidoDTO data, string Usuario);
        ConvocatoriaPersonalDTO Update(ConvocatoriaPersonalRecibidoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<ConvocatoriaPersonal> Add(List<ConvocatoriaPersonal> listadoEntidad);
        List<ConvocatoriaPersonal> Update(List<ConvocatoriaPersonal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public List<ConvocatoriaPersonalDTO> ObtenerConvocatoriasRegistradas();
        public object ObtenerTodosCombosConvotoriaPersonal();

        public DetalleConvocatoriaDTO ObtenerDetalleConvocatorias(int idConvocatoria);

        IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> ObtenerComboComvocatoriaPersonal();

    }
}
