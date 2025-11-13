using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWavixRepository
    {
        WavixPersonalDTO? GetUserAccess(int idPersonal);
        List<NumeroAsesorWavixDTO>? GetNumberByUser(int idPersonal);
        EstadoLlamadaDTO? ObtenerEstadoUltimaLlamada(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada);

    }
}
