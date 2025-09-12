using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITransicionCalificacionFaseRepository : IGenericRepository<TTransicionFase>
    {
        #region Metodos Base
        TTransicionFase Add(TransicionCalificacionFase entidad);
        TTransicionFase Update(TransicionCalificacionFase entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTransicionFase> Add(IEnumerable<TransicionCalificacionFase> listadoEntidad);
        IEnumerable<TTransicionFase> Update(IEnumerable<TransicionCalificacionFase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase();
        TransicionCalificacionFase ObtenerTransicionCalificacionFasePorId(int idTransicionCalificacionFase);
        TransicionCalificacionFase ObtenerPorId(int id);
        IEnumerable<TransicionCalificacionFaseDTO> ObtenerCombo();
    }
}