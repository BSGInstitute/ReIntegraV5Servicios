using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing. FacebookAudiencia;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing. FacebookAudiencia
{
    public class FacebookAudienciaRepository : GenericRepository<TFacebookAudiencium>, IFacebookAudienciaRepository
    {
        public FacebookAudienciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository):base(context, connectionFactory, dapperRepository)
        {
        }


        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                var lista = new List<ComboDTO>();
                string _query = @"SELECT Id,Nombre FROM
                                  mkt.T_FacebookAudiencia WHERE ESTADO = 1";
                var queryRespuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<ComboDTO>>(queryRespuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FacebookAudienciaHistorialDTO> listaFacebookAudienciaHistorial = new List<FacebookAudienciaHistorialDTO>();
                var _query = "SELECT NombreCuentaPublicitaria, FacebookIdCuentaPublicitaria, FacebookIdAudiencia, Nombre, FechaModificacion, Subtipo FROM mkt.V_ObtenerAudienciaCuentaPublicitaria WHERE IdFiltroSegmento = @idFiltroSegmento AND EstadoFacebookAudiencia = 1 AND EstadoFacebookAudienciaCuentaPublicitaria = 1 AND Origen = 'Propio'";
                var respuestaQuery = _dapperRepository.QueryDapper(_query, new { idFiltroSegmento });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]") && !respuestaQuery.Contains("null"))
                {
                    listaFacebookAudienciaHistorial = JsonConvert.DeserializeObject<List<FacebookAudienciaHistorialDTO>>(respuestaQuery);
                }
                return listaFacebookAudienciaHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene todos los registros para combos.
        /// </summary>
        /// <returns></returns>
        public List<FacebookAudienciaComboDTO> ObtenerComboFacebookAudiencia()
        {
            try
            {
                List<FacebookAudienciaComboDTO> listaFacebookAudiencia = new List<FacebookAudienciaComboDTO>();
                listaFacebookAudiencia = GetBy(x => true, y => new FacebookAudienciaComboDTO
                {
                    FacebookIdAudiencia = y.FacebookIdAudiencia,
                    Id=y.Id,
                    Nombre = y.Nombre,
                    Descripcion = y.Descripcion 
                }).ToList();
                return listaFacebookAudiencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookAudienciaComboDTO> ObtenerComboListaPublico()
        {
            try
            {
                List<FacebookAudienciaComboDTO> listaFacebookAudiencia = new List<FacebookAudienciaComboDTO>();
                listaFacebookAudiencia = GetBy(x => true, y => new FacebookAudienciaComboDTO
                {
                    FacebookIdAudiencia = y.FacebookIdAudiencia,
                    Nombre = y.Nombre
                }).ToList();
                return listaFacebookAudiencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
