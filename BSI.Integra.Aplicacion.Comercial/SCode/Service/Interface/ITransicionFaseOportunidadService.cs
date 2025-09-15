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
    public interface ITransicionFaseOportunidadService
    {
        TransicionFaseOportunidad Update(TransicionFaseOportunidad entidad);
        bool Delete(int id, string usuario);
        List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase();
        TransicionFaseOportunidad ObtenerTransicionCalificacionFasePorId(int id);
    }
}