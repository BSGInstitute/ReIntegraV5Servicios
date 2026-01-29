using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class EsquemaActividadService : IEsquemaActividadService
    {
        private readonly IEsquemaActividadRepository _repository;

        public EsquemaActividadService(IEsquemaActividadRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(EsquemaActividadRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (string.IsNullOrWhiteSpace(entidad.Nombre)) throw new ArgumentException("El nombre de la actividad es requerido");
                if (entidad.IdAsistenteMarketingWhatsAppAsignacion <= 0) throw new ArgumentException("El número de WhatsApp es requerido");
                if (entidad.IdEsquemaWhatsAppAsignacion <= 0) throw new ArgumentException("El esquema es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> ActualizarAsync(EsquemaActividadRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id == null || entidad.Id <= 0) throw new ArgumentException("El Id de la actividad es requerido");
                if (string.IsNullOrWhiteSpace(entidad.Nombre)) throw new ArgumentException("El nombre de la actividad es requerido");
                if (entidad.IdAsistenteMarketingWhatsAppAsignacion <= 0) throw new ArgumentException("El número de WhatsApp es requerido");
                if (entidad.IdEsquemaWhatsAppAsignacion <= 0) throw new ArgumentException("El esquema es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("El Id de la actividad es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.EliminarAsync(id, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> ActualizarEstadoAsync(EsquemaActividadEstadoDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id <= 0) throw new ArgumentException("El Id de la actividad es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarEstadoAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<EsquemaActividadDTO>> ListarAsync()
        {
            try
            {
                return await _repository.ListarAsync();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
