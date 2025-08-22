using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IBeneficioAlumnoPEspecificoService
    {
        BeneficioAlumnoPEspecifico InsertarBeneficios(OportunidadCodigoMatriculaDTO oportunidadVerificada);
    }
}
