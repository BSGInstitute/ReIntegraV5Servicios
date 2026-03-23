namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// Interface: IValidacionRn2Service
    /// Autor: (pendiente)
    /// Fecha: 2026-02-23
    /// <summary>
    /// Contrato para la validación de la Regla de Negocio 2 de leads/oportunidades
    /// </summary>
    public interface IValidacionRn2Service
    {
        bool ValidarLeadRn2Async(int idOportunidad, int idPersonalAsignado);
    }
}
