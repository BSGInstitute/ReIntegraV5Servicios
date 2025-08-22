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
    /// Repositorio: TipoReclamoAlumnoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/12/2022
    /// <summary>
    /// Gestión general de T_TipoReclamoAlumno
    /// </summary>
    public class TipoReclamoAlumnoRepository : GenericRepository<TTipoReclamoAlumno>, ITipoReclamoAlumnoRepository
    {
        private Mapper _mapper;

        public TipoReclamoAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoReclamoAlumno, TipoReclamoAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todo el registro de Tipo Reclamo de Alumno
        /// </summary>
        /// <returns> List<ComboFiltroDTO> </returns> 
        public IEnumerable<ComboFiltroDTO> ObtenerCombo()
        {
            try
            {
                var programasGenerales = new List<ComboFiltroDTO>();
                var _query = @"SELECT Id,Nombre
                            FROM mkt.T_TipoReclamoAlumno
                            WHERE Estado = 1;";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ComboFiltroDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
