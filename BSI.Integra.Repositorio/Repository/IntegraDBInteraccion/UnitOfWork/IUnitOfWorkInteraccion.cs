using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork
{
    public interface IUnitOfWorkInteraccion
    {
        void Commit();
        Task CommitAsync();
        void Rollback();
        void Dispose();
        void DetachAll();
        IRegistroInicioSesionRepository RegistroInicioSesionRepository { get; }
        IRegistroInicioSesionEstadoRepository RegistroInicioSesionEstadoRepository { get; }
        IInteraccionModuloRepository InteraccionModuloRepository { get; }
    }
}
