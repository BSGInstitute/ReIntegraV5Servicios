using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMatriculaCabeceraBeneficiosService
    {
        #region Metodos Base
        MatriculaCabeceraBeneficios Add(MatriculaCabeceraBeneficios entidad);
        MatriculaCabeceraBeneficios Update(MatriculaCabeceraBeneficios entidad);
        bool Delete(int id, string usuario);

        List<MatriculaCabeceraBeneficios> Add(List<MatriculaCabeceraBeneficios> listadoEntidad);
        List<MatriculaCabeceraBeneficios> Update(List<MatriculaCabeceraBeneficios> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MatriculaCabeceraBeneficiosDTO> ObtenerMatriculaCabeceraBeneficios();
        IEnumerable<MatriculaCabeceraBeneficiosComboDTO> ObtenerCombo();
        IEnumerable<StringDTO> ObtenerBeneficiosPorMatriculaCabecera(int idMatriculaCabecera);
        IEnumerable<BeneficiosSolicitadosDTO> ObtenerBeneficiosSolicitadosSinRepetir();
    }
}
