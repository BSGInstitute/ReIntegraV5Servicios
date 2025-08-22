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
    public interface ITipoPagoRemuneracionRepository : IGenericRepository<TTipoPagoRemuneracion>
    {
        #region Metodos Base
        TTipoPagoRemuneracion Add(TipoPagoRemuneracion entidad);
        TTipoPagoRemuneracion Update(TipoPagoRemuneracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoPagoRemuneracion> Add(IEnumerable<TipoPagoRemuneracion> listadoEntidad);
        IEnumerable<TTipoPagoRemuneracion> Update(IEnumerable<TipoPagoRemuneracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoPagoRemuneracionDTO> Obtener();
    }
}
