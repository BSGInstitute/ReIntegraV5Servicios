using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaEncuestaCategoriaRepository : IGenericRepository<TPreguntaEncuestaCategorium>
    {
        #region Metodos Base
        TPreguntaEncuestaCategorium Add(PreguntaEncuestaCategoria entidad);
        TPreguntaEncuestaCategorium Update(PreguntaEncuestaCategoria entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaEncuestaCategorium> Add(IEnumerable<PreguntaEncuestaCategoria> listadoEntidad);
        IEnumerable<TPreguntaEncuestaCategorium> Update(IEnumerable<PreguntaEncuestaCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        PreguntaEncuestaCategoria ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();

        List<PreguntaEncuestaCategoriaDTO> ObtenerCategoriaEncuesta();
        List<PreguntaCategoriaAsincronicaDTO> ObtenerPreguntaCategoriaAsincronica();

    }
}
