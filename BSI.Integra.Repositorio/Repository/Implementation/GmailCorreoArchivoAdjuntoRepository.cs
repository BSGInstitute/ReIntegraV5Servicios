using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GmailCorreoArchivoAdjuntoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_GmailCorreoArchivoAdjunto
    /// </summary>
    public class GmailCorreoArchivoAdjuntoRepository : GenericRepository<TGmailCorreoArchivoAdjunto>, IGmailCorreoArchivoAdjuntoRepository
    {
        private Mapper mapper;
        public GmailCorreoArchivoAdjuntoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjunto>(MemberList.None).ReverseMap();
            });
            mapper = new Mapper(config);
        }

        public List<GmailCorreoArchivoAdjuntoDTO> obtenerCorreoArchivoAdjuntoPorId(int idCorreoArchivo)
        {
            try
            {
                List<GmailCorreoArchivoAdjuntoDTO> rpta = new List<GmailCorreoArchivoAdjuntoDTO>();
                var query = @"SELECT Id, Nombre, UrlArchivoRepositorio 
                            FROM  mkt.T_GmailCorreoArchivoAdjunto
                            WHERE Estado = 1 AND IdGmailCorreo = " + idCorreoArchivo;
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GmailCorreoArchivoAdjuntoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
