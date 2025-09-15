using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ITransicionCalificacionFaseService
    {
        TransicionCalificacionFaseCreateDTO InsertarTransicionCalificacionFase(TransicionCalificacionFaseCreateDTO request, string usuario);
        TransicionCalificacionFase ActualizarTransicionCalificacionFase(TransicionCalificacionFase entidad);
        bool Delete(int id, string usuario);
        List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase();
        TransicionCalificacionFase ObtenerTransicionCalificacionFasePorId(int id);
        Task<Dictionary<string, object>> ObtenerFasesOportunidad();
        IEnumerable<TransicionCalificacionFaseDTO> ObtenerCombo();
    }
}