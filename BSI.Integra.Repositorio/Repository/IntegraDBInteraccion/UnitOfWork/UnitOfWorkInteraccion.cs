using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Implementacion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface;
using Microsoft.EntityFrameworkCore;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork
{
    public class UnitOfWorkInteraccion : IUnitOfWorkInteraccion, IDisposable
    {
        private IConnectionFactoryInteraccion _connectionFactory;
        private IntegraDBInteraccionContext _context;
        private bool _disposed;
        private IDapperRepositoryInteraccion _dapperRepository;

        public UnitOfWorkInteraccion(IntegraDBInteraccionContext context, IConnectionFactoryInteraccion connectionFactory, IDapperRepositoryInteraccion dapperRepository)
        {
            _context = context;
            _connectionFactory = connectionFactory;
            _dapperRepository = dapperRepository;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Rollback()
        {
            try
            {
                // Descartar los cambios no guardados
                foreach (var entry in _context.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DetachAll()
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries().ToArray())
                {
                    entry.State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Repositorios

        private IRegistroInicioSesionRepository _IRegistroInicioSesionRepository;

        IRegistroInicioSesionRepository IUnitOfWorkInteraccion.RegistroInicioSesionRepository
        {
            get
            {
                return _IRegistroInicioSesionRepository ?? new RegistroInicioSesionRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IRegistroInicioSesionEstadoRepository _IRegistroInicioSesionEstadoRepository;

        IRegistroInicioSesionEstadoRepository IUnitOfWorkInteraccion.RegistroInicioSesionEstadoRepository
        {
            get
            {
                return _IRegistroInicioSesionEstadoRepository ?? new RegistroInicioSesionEstadoRepository(_context, _connectionFactory, _dapperRepository);
            }
        }

        private IInteraccionModuloRepository _IInteraccionModuloRepository;

        IInteraccionModuloRepository IUnitOfWorkInteraccion.InteraccionModuloRepository
        {
            get
            {
                return _IInteraccionModuloRepository ?? new InteraccionModuloRepository(_context, _connectionFactory, _dapperRepository);
            }
        }
    }
}