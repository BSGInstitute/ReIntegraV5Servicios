using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeguimientoAlumnoComentarioRepository : IGenericRepository<TSeguimientoAlumnoComentario>
    {
        TSeguimientoAlumnoComentario Add(SeguimientoAlumnoComentario entidad);
        List<TipoSeguimientoDTO> ObtenerCombo();
        TipoSeguimientoDTO ObtenerPorId(int Id);
        TipoSeguimientoDTO Update(TipoSeguimientoDTO tipoSeguimientoEntrada);
        bool InsertarTipoSeguimiento(TipoSeguimientoEntradaDTO tipoSeguimientoEntrada);
        bool EliminarTipoSeguimiento(int id, string usuario);

    }


}
