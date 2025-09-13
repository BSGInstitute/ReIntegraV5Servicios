using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ILineamientoCalificacionFaseService
    {
        LineamientoCalificacionFase Add(LineamientoCalificacionFase entidad);

        LineamientoCalificacionFase ObtenerPorId(int id);
        LineamientoCalificacionFase Update(LineamientoCalificacionFase entidad);
    }
}
