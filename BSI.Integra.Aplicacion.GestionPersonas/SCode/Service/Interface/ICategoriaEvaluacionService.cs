using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ICategoriaEvaluacionService
    {
        IEnumerable<CategoriaEvaluacionDTO> Obtener();
        CategoriaEvaluacionDTO Insertar(CategoriaEvaluacionDTO dto, string usuario);
        CategoriaEvaluacionDTO Actualizar(CategoriaEvaluacionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
