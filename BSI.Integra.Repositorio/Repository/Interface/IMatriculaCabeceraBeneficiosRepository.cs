using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMatriculaCabeceraBeneficiosRepository : IGenericRepository<TMatriculaCabeceraBeneficio>
    {
        #region Metodos Base
        TMatriculaCabeceraBeneficio Add(MatriculaCabeceraBeneficios entidad);
        TMatriculaCabeceraBeneficio Update(MatriculaCabeceraBeneficios entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMatriculaCabeceraBeneficio> Add(IEnumerable<MatriculaCabeceraBeneficios> listadoEntidad);
        IEnumerable<TMatriculaCabeceraBeneficio> Update(IEnumerable<MatriculaCabeceraBeneficios> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MatriculaCabeceraBeneficiosDTO> ObtenerMatriculaCabeceraBeneficios();
        IEnumerable<MatriculaCabeceraBeneficiosComboDTO> ObtenerCombo();
        IEnumerable<StringDTO> ObtenerBeneficiosPorMatriculaCabecera(int idMatriculaCabecera);
        MatriculaCabeceraBeneficios ObtenerPorId(int IdMatriculaCabeceraBeneficio);
        IEnumerable<BeneficiosSolicitadosDTO> ObtenerBeneficiosSolicitadosSinRepetir();
    }
}