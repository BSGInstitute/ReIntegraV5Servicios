using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IPreguntaEncuestaCategoriaService
    { 
        #region Metodos Base
        PreguntaEncuestaCategoria Add(PreguntaEncuestaCategoria entidad);
        PreguntaEncuestaCategoria Update(PreguntaEncuestaCategoria entidad);
        bool Delete(int id, string usuario);
        List<PreguntaEncuestaCategoria> Add(List<PreguntaEncuestaCategoria> listadoEntidad);
        List<PreguntaEncuestaCategoria> Update(List<PreguntaEncuestaCategoria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


        List<PreguntaCategoriaAsincronicaDTO> ObtenerPreguntaCategoriaAsincronica();
    }
}
