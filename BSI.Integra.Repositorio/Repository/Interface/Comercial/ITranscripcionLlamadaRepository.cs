using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ITranscripcionLlamadaRepository: IGenericRepository<TTranscripcionLlamadum>
    {
        #region Metodos Base
        TTranscripcionLlamadum Add(TranscripcionLlamada entidad);
        TTranscripcionLlamadum Update(TranscripcionLlamada entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTranscripcionLlamadum> Add(IEnumerable<TranscripcionLlamada> listadoEntidad);
        IEnumerable<TTranscripcionLlamadum> Update(IEnumerable<TranscripcionLlamada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Task<IEnumerable<TranscripcionDetalleDTO>> ObtenerTranscripcion(int idLlamada);
    }
}
