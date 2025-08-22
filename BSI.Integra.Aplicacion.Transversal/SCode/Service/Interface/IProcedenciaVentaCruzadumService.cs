namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProcedenciaVentaCruzadumService
    {
        bool InsertarProcedenciaVentaCruzada(int IdOportunidadActual, int IdOportunidadNueva);
    }
}
