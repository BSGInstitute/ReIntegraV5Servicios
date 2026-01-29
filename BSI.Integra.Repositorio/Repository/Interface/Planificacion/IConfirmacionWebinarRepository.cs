using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfirmacionWebinarRepository
    {
        AsistenciaConfirmacionWebinarDTO? ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(int IdMatriculaCabecera, int IdPEspecificoSesion);
        bool Insertar(AsistenciaConfirmacionWebinarDTO obj);
        bool Actualizar(AsistenciaConfirmacionWebinarDTO obj);
    }
}
