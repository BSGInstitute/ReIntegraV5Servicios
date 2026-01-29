using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class EsquemaRespuestaService : IEsquemaRespuestaService
    {
        private readonly IEsquemaRespuestaRepository _repository;

        public EsquemaRespuestaService(IEsquemaRespuestaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InsertarAsync(EsquemaRespuestaRequestDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.IdEsquemaWhatsAppAsignacionLecturaMensaje <= 0) throw new ArgumentException("El Id de la lectura de mensaje es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.InsertarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<int> ActualizarAsync(EsquemaRespuestaActualizarDTO entidad, string usuario)
        {
            try
            {
                if (entidad == null) throw new ArgumentNullException(nameof(entidad));
                if (entidad.Id <= 0) throw new ArgumentException("El Id de la respuesta es requerido");
                if (string.IsNullOrWhiteSpace(usuario)) throw new ArgumentException("El usuario es requerido");

                return await _repository.ActualizarAsync(entidad, usuario);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<List<EsquemaRespuestaDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion)
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
