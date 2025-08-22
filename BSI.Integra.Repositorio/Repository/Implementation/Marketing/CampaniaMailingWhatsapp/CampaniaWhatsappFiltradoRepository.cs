using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniaWhatsappFiltradoRepository : GenericRepository<TFiltradoDeDatosPorPrioridadWhatsApp>, ICampaniaWhatsappFiltradoRepository
    {
        public CampaniaWhatsappFiltradoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: esta funcion realiza el filtrado de la data de filtro para sendinblue el cual sera usado para las campanias masivas
        /// </summary>
        /// <param name="datosFiltro"> objeto que contiene infomracion necesaria para la ejecucon del filtrado de datos de wpp</param>
        /// <returns>retorna una variable booleana</returns>
        public async Task<bool> FiltradoDeDatosParaWhatsapp(CampaniaMailingWhatsAppFiltradoDTO.CampaniaWhatsAppFiltrado datosFiltro)
        {
            try
            {
                var query = "[mkt].[SP_FiltradoFinalDeDataWhatsApp]";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    datosFiltro.IdcampaniaGeneral,
                    datosFiltro.usuario,
                    datosFiltro.cantidadDeDias
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: obtiene los datos de la data filtrada pro una prioirdad, deveuvle a vista los valores de nombre y telefono pero pueden ser cambiados de acuerdo a solicitud
        /// </summary>
        /// <param name="IdcampaniaGeneral">identificador unico de campania general</param>
        /// <param name="Prioridad">numero de la prioridad indicada</param>
        /// <returns>List<CampaniaWhatsAppFiltroWhatsApp></returns>
        public List<CampaniaWhatsAppFiltroWhatsApp> FiltradoDeDatosParaWhatsappObtenerData(int IdcampaniaGeneral, int Prioridad)
        {
            try
            {
                List<CampaniaWhatsAppFiltroWhatsApp> datos = new List<CampaniaWhatsAppFiltroWhatsApp>();
                var sql = "select a.id,a.Nombre1 as nombre,fm.Movil as Telefono from mkt.T_FiltradoDeDatosPorPrioridadWhatsApp  as fm INNER JOIN MKT.T_Alumno as a ON  fm.idAlumno = a.Id where IdcampaniaGeneral=@IdcampaniaGeneral AND Prioridad=@Prioridad AND fm.estado=1";
                var resultado = _dapperRepository.QueryDapper(sql, new
                {
                    IdcampaniaGeneral,
                    Prioridad
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    datos = JsonConvert.DeserializeObject<List<CampaniaWhatsAppFiltroWhatsApp>>(resultado);
                }
                return datos;
            }catch(Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: Realiza la busqueda de una lista de datos filtrados para wpp
        /// </summary>
        /// <param name="IdcampaniaGeneral"></param>
        /// <returns>List<TFiltradoDeDatosPorPrioridadWhatsApp></returns>
        public List<TFiltradoDeDatosPorPrioridadWhatsApp> FiltradoDeDatosParaWhatsappObtenerAllData(int IdcampaniaGeneral)
        {
            var datos = base.GetBy(x => x.IdCampaniaGeneral == IdcampaniaGeneral && x.Estado==true).ToList();
            return datos;
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: obtiene la cantidad de contactos por prioridad y campania
        /// </summary>
        /// <param name="IdcampaniaGeneralDetalle">identificador unico de campania general</param>
        /// <param name="prioridad">que prioridad se esta buscando</param>
        /// <returns>la cantidad de contactos de wpp</returns>
        public int ObtenerCantidadDeDataPorPioridadYcampania(int IdcampaniaGeneralDetalle, int prioridad)
        {
            try
            {
                var resp = base.GetBy(x => x.IdCampaniaGeneralDetalle == IdcampaniaGeneralDetalle && x.Prioridad == prioridad && x.EsValidoParaWhatsApp==true && x.Estado==true).ToList();
                return resp.Count();
            }catch(Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos 
        /// Fecha: 21-03-2023
        /// Descipcion: Realiza el eliminado logico de los datos filtrados para wpp de una campania general
        /// </summary>
        /// <param name="IdcampaniaGeneral">identificador unico de una campania general</param>
        /// <param name="usuario">Nombre de usaurio que realizara la accion</param>
        /// <returns></returns>
        public bool EliminarFiltradoPasadoWhatsApp(int IdcampaniaGeneral, string usuario)
        {
            try
            {
                List<CampaniaMailingSendingBlueFiltroMailing> datos = new List<CampaniaMailingSendingBlueFiltroMailing>();
                var sql = "UPDATE mkt.T_FiltradoDeDatosPorPrioridadWhatsApp  set Estado=0, usuarioModificacion=@usuario where IdcampaniaGeneral=@IdcampaniaGeneral AND estado=1";
                var resultado = _dapperRepository.QueryDapper(sql, new
                {
                    usuario,
                    IdcampaniaGeneral,
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
