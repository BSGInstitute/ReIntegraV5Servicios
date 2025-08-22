using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MoodleCategoriaTipoRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión general de T_MoodleCategoriaTipo
    /// </summary>
    public class MoodleCategoriaTipoRepository : GenericRepository<TMoodleCategoriaTipo>, IMoodleCategoriaTipoRepository
    {
        private Mapper _mapper;
        public MoodleCategoriaTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCategoriaTipo, MoodleCategoriaTipo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el combo de T_MoodleCategoriaTipo
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> comboDTOs = new List<ComboDTO>();
                var _query = @"SELECT Id,Nombre
                                FROM ope.T_MoodleCategoriaTipo
                                WHERE Estado = 1";
                var dapperResultado = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(dapperResultado) && !dapperResultado.Contains("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(dapperResultado);
                }
                return comboDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
