using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _repository;

        public PerfilService(IPerfilRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(PerfilRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad));

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new ArgumentException("El nombre del perfil es requerido");

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido");

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ActualizarAsync(PerfilRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad));

                if (entidad.Id == null || entidad.Id <= 0)
                    throw new ArgumentException("El Id del perfil es requerido");

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new ArgumentException("El nombre del perfil es requerido");

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarAsync(entidad, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El Id del perfil es requerido");

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido");

                return await _repository.EliminarAsync(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PerfilDTO>> ListarAsync()
        {
            try
            {
                return await _repository.ListarAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
