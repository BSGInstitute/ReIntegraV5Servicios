using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBeneficioAlumnoPEspecificoRepository
    {

        #region Metodos Base
        TBeneficiosAlumnoPespecifico Add(BeneficioAlumnoPEspecifico entidad);
        TBeneficiosAlumnoPespecifico Update(BeneficioAlumnoPEspecifico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TBeneficiosAlumnoPespecifico> Add(IEnumerable<BeneficioAlumnoPEspecifico> listadoEntidad);
        IEnumerable<TBeneficiosAlumnoPespecifico> Update(IEnumerable<BeneficioAlumnoPEspecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<BeneficioAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatricula(string codigoMatricula);
        List<BeneficioAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatriculaSinPaquete(string codigoMatricula);
        List<BeneficiosProgramaTipo1DTO> ObtenerBeneficiosProgramaTipo1(int idPGeneral, int idPais, int? paquete);
        List<BeneficiosProgramaTipo2DTO> ObtenerBeneficiosProgramaTipo2(int idPGeneral);

    }
}
