using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class EsquemaLecturaMensajeService : IEsquemaLecturaMensajeService
    {
        private readonly IEsquemaLecturaMensajeRepository _repository;

        public EsquemaLecturaMensajeService(IEsquemaLecturaMensajeRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.IdEsquemaWhatsAppAsignacion <= 0) throw new ArgumentException("El Id del esquema es requerido");
                if (string.IsNullOrWhiteSpace(entidad.ClasificacionTipoMensaje)) throw new ArgumentException("La clasificación del tipo de mensaje es requerida");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> ActualizarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id == null || entidad.Id <= 0) throw new ArgumentException("El Id de la lectura de mensaje es requerido");
                if (string.IsNullOrWhiteSpace(entidad.ClasificacionTipoMensaje)) throw new ArgumentException("La clasificación del tipo de mensaje es requerida");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> EliminarAsync(int id, string usuario)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("El Id de la lectura de mensaje es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.EliminarAsync(id, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<EsquemaLecturaMensajeDetalleDTO> ObtenerPorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("El Id de la lectura de mensaje es requerido");
                return await _repository.ObtenerPorIdAsync(id);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<EsquemaLecturaMensajeDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion)
        {
            try
            {
                if (idEsquemaWhatsAppAsignacion <= 0) throw new ArgumentException("El Id del esquema es requerido");
                return await _repository.ListarPorEsquemaAsync(idEsquemaWhatsAppAsignacion);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
