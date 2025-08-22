using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPreguntumRepository : IGenericRepository<TPreguntum>
    {
        #region Metodos Base
        TPreguntum Add(Preguntum entidad);
        TPreguntum Update(Preguntum entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntum> Add(IEnumerable<Preguntum> listadoEntidad);
        IEnumerable<TPreguntum> Update(IEnumerable<Preguntum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Preguntum ObtenerPorId(int id);
        List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica();
    }
}
