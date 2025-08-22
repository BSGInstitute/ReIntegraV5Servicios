using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IPreguntumService
    {
        #region Metodos Base
        Preguntum Add(Preguntum entidad);
        Preguntum Update(Preguntum entidad);
        bool Delete(int id, string usuario);
        List<Preguntum> Add(List<Preguntum> listadoEntidad);
        List<Preguntum> Update(List<Preguntum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica();
    }
}
