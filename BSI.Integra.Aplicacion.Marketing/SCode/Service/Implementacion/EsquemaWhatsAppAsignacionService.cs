using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class EsquemaWhatsAppAsignacionService : IEsquemaWhatsAppAsignacionService
    {
        private readonly IEsquemaWhatsAppAsignacionRepository _repository;

        public EsquemaWhatsAppAsignacionService(IEsquemaWhatsAppAsignacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (string.IsNullOrWhiteSpace(entidad.Nombre)) throw new ArgumentException("El nombre del esquema es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> ActualizarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id == null || entidad.Id <= 0) throw new ArgumentException("El Id del esquema es requerido");
                if (string.IsNullOrWhiteSpace(entidad.Nombre)) throw new ArgumentException("El nombre del esquema es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("El Id del esquema es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.EliminarAsync(id, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<EsquemaWhatsAppAsignacionDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("El Id del esquema es requerido");
                return await _repository.ObtenerPorIdAsync(id);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<EsquemaWhatsAppAsignacionDTO>> ListarAsync()
        {
            try
            {
                return await _repository.ListarAsync();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
