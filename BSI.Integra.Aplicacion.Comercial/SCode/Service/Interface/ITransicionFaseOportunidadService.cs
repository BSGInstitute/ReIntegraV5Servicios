using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ITransicionFaseOportunidadService
    {
        #region Metodos Base
        TransicionFaseOportunidad Add(TransicionFaseOportunidad entidad);
        TransicionFaseOportunidad Update(TransicionFaseOportunidad entidad);
        bool Delete(int id, string usuario);
        TransicionFaseOportunidad InsertTransicion(TransicionFaseOportunidadDTO transicionFaseOportunidadDTO);
        TransicionFaseOportunidad UpdateTransicion(TransicionFaseOportunidadDTO dto);
        #endregion
        List<TransicionFaseOportunidadPlanoDto> Obtener();
        TransicionFaseOportunidad ObtenerPorId(int id);
    }
}