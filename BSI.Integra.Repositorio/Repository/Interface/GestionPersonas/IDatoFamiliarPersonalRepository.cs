using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IDatoFamiliarPersonalRepository : IGenericRepository<TDatoFamiliarPersonal>
    {
        #region Metodos Base
        TDatoFamiliarPersonal Add(DatoFamiliarPersonal entidad);
        TDatoFamiliarPersonal Update(DatoFamiliarPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDatoFamiliarPersonal> Add(IEnumerable<DatoFamiliarPersonal> listadoEntidad);
        IEnumerable<TDatoFamiliarPersonal> Update(IEnumerable<DatoFamiliarPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<DatoFamiliarPersonalDTO> ObtenerListaFamiliarPersonal(int idPersonal);
        DatoFamiliarPersonal? ObtenerPorId(int Id);

    }
}
