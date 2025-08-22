using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConjuntoListaService
    {
        public List<ConjuntoListaGrillaDTO> Obtener();
        public ConjuntoListaDTO Obtener(int id);
        public ConjuntoListaDetalleCompletoListoDTO ObtenerDetalle(int IdConjuntoLista);
        public bool Insertar(ConjuntoListaDetalleCompletoListoDTO ConjuntoListaDetalleCompleto, string Usuario);
        public bool SubirLista(ConjuntoListaSubirDTO json, string usuario);
        public List<ComboDTO> ObtenerCombo();
        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario);




    }
}
