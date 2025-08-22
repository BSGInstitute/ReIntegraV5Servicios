using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralPuntoCorteRepository : IGenericRepository<TProgramaGeneralPuntoCorte>
    {
        #region Metodos Base
        TProgramaGeneralPuntoCorte Add(ProgramaGeneralPuntoCorte entidad);
        TProgramaGeneralPuntoCorte Update(ProgramaGeneralPuntoCorte entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPuntoCorte> Add(IEnumerable<ProgramaGeneralPuntoCorte> listadoEntidad);
        IEnumerable<TProgramaGeneralPuntoCorte> Update(IEnumerable<ProgramaGeneralPuntoCorte> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> ObtenerListaProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteFiltroDTO filtroProgramaGeneralPuntoCorte);
        List<ProgramaGeneralPuntoCorteDTO> ObtenerPorIdPgeneral(int idProgramaGeneral);
        ProgramaGeneralPuntoCorteDTO? ObtenerPorIdPgeneralIdPais(int idProgramaGeneral, int idPais);
        ProgramaGeneralPuntoCorte? ObtenerPorId(int id);
    }
}
