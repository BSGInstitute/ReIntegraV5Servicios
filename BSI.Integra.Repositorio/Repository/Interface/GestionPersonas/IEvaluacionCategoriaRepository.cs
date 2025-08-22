using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IEvaluacionCategoriaRepository : IGenericRepository<TEvaluacionCategorium>
    {
        public List<FiltroDTO> ObtenerCategoriasEvaluacionRegistradas();

    }
}
