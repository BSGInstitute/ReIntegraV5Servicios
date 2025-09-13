using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ILineamientoCalificacionFaseRepository
    {
        TLineamientoCalificacionFase Add(LineamientoCalificacionFase entidad);
        LineamientoCalificacionFase ObtenerPorId(int id);
        TLineamientoCalificacionFase Update(LineamientoCalificacionFase entidad);
    }
}
