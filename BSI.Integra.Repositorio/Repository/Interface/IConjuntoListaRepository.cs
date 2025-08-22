using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoListaRepository : IGenericRepository<TConjuntoListum>
    {
        public TConjuntoListum Add(ConjuntoLista entidad);
        public TConjuntoListum Update(ConjuntoLista entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TConjuntoListum> Add(IEnumerable<ConjuntoLista> listadoEntidad);
        public IEnumerable<TConjuntoListum> Update(IEnumerable<ConjuntoLista> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);


        public List<ConjuntoListaGrillaDTO> Obtener();
        public ConjuntoListaDTO Obtener(int id);
        public List<ConjuntoListaCompuestoDTO> ObtenerResultado(int id, int idFiltroSegmentoTipoContacto);
        public TConjuntoListum Insertar(ConjuntoListaEnvioDTO objeto);
        public bool SubirLista(ConjuntoListaSubirDTO json, string usuario);
        public List<ComboDTO> ObtenerCombo();
        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario);

    }
}
