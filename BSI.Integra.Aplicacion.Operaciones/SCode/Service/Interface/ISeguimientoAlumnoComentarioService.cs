using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISeguimientoAlumnoComentarioService
    {
        SeguimientoAlumnoComentario Add(SeguimientoAlumnoComentarioDTO entidad);
    }
}
