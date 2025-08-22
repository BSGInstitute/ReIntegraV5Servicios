using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IActividadCabeceraDiaSemanaRepository
    {
        #region Metodos Base
        TActividadCabeceraDiaSemana Add(ActividadCabeceraDiaSemana entidad);
        ActividadCabeceraDiaSemana FirstById(int id);
        TActividadCabeceraDiaSemana Update(ActividadCabeceraDiaSemana entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TActividadCabeceraDiaSemana> Add(IEnumerable<ActividadCabeceraDiaSemana> listadoEntidad);
        IEnumerable<TActividadCabeceraDiaSemana> Update(IEnumerable<ActividadCabeceraDiaSemana> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public bool Insert(ActividadCabeceraDiaSemana objetoBO);
        public IEnumerable<TActividadCabeceraDiaSemana> GetBy(Expression<Func<TActividadCabeceraDiaSemana, bool>> filter);
        public List<ActividadCabeceraDiaSemana> ObtenerActividadDiaPorID(int idActividadCabecera);


    }
}
