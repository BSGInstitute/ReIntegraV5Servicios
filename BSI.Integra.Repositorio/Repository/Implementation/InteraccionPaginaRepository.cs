using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: InteraccionPaginaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 27/12/2022
    /// <summary>
    /// Gestión general de T_InteraccionPagina
    /// </summary>
    public class InteraccionPaginaRepository : GenericRepository<TInteraccionPagina>, IInteraccionPaginaRepository
    {
        private Mapper _mapper;

        public InteraccionPaginaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInteraccionPagina, InteraccionPagina>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obteniene todas las Interacciones del Alumno
        /// </summary>
        /// <param name="idAlumno"> Id del alumno </param>
        /// <returns> List<InteraccionAlumnoDTO> </returns>
        public List<InteraccionAlumnoDTO> ObtenerInteraccionesPorAlumno(int idAlumno)
        {
            try
            {
                string registrosDB = _dapperRepository.QuerySPDapper("com.SP_ObtenerInteraccionesPorAlumno", new { IdAlumno = idAlumno });
                if (!string.IsNullOrEmpty(registrosDB))
                {
                    return JsonConvert.DeserializeObject<List<InteraccionAlumnoDTO>>(registrosDB);
                }
                return new List<InteraccionAlumnoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
