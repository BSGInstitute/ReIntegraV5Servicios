using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// <summary>
    /// Servicio para Mensajes Exactos (catálogo global)
    /// </summary>
    public class MensajeExactoService : IMensajeExactoService
    {
        private readonly IMensajeExactoRepository _repository;

        public MensajeExactoService(IMensajeExactoRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(MensajeExactoRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad), "La entidad no puede ser nula");

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new ArgumentException("El nombre del mensaje exacto es requerido", nameof(entidad.Nombre));

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido", nameof(usuario));

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ActualizarAsync(MensajeExactoRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null)
                    throw new ArgumentNullException(nameof(entidad), "La entidad no puede ser nula");

                if (entidad.Id == null || entidad.Id <= 0)
                    throw new ArgumentException("El Id del mensaje exacto es requerido", nameof(entidad.Id));

                if (string.IsNullOrWhiteSpace(entidad.Nombre))
                    throw new ArgumentException("El nombre del mensaje exacto es requerido", nameof(entidad.Nombre));

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido", nameof(usuario));

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
                    throw new ArgumentException("El Id del mensaje exacto es requerido", nameof(id));

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new ArgumentException("El usuario es requerido", nameof(usuario));

                return await _repository.EliminarAsync(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MensajeExactoDTO>> ListarAsync()
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
