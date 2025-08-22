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
    /// Repositorio: RecuperacionSesionRepository
    /// Autor: Daniel Huaita.
    /// Fecha: 14/08/2021
    /// <summary>
    /// Gestión general de T_RecuperacionSesion
    /// </summary>
    public class RecuperacionSesionRepository : GenericRepository<TRecuperacionSesion>, IRecuperacionSesionRepository
    {
        private Mapper _mapper;

        private TRecuperacionSesion MapeoEntidad(RecuperacionSesion entidad)
        {
            try
            {
                //crea la entidad padre
                TRecuperacionSesion modelo = _mapper.Map<TRecuperacionSesion>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RecuperacionSesionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRecuperacionSesion, RecuperacionSesion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Daniel H.
        /// Fecha: 14/02/2023
        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionRecuperacionDTO> obtenerSesionPorPEspecifico = new List<PEspecificoSesionRecuperacionDTO>();
                var obtenerSesionPorPEspecificoDB = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerSesionesPorPEspecifico]", new { idPespecifico = idPespecifico, idMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(obtenerSesionPorPEspecificoDB) && !obtenerSesionPorPEspecificoDB.Contains("[]"))
                {
                    obtenerSesionPorPEspecifico = JsonConvert.DeserializeObject<List<PEspecificoSesionRecuperacionDTO>>(obtenerSesionPorPEspecificoDB);
                }
                return obtenerSesionPorPEspecifico;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public RecuperacionSesion ObtenerPorId(int Id)
        {
            try
            {
                RecuperacionSesion recuperacionSesion = new RecuperacionSesion();
                var query = @"
                SELECT 
                Id, IdMatriculaCabecera, IdPEspecificoSesion, Estado, 
                UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion,
                RowVersion, IdMigracion 
                FROM pla.T_RecuperacionSesion
                WHERE Estado=1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != null)
                {
                    recuperacionSesion = JsonConvert.DeserializeObject<RecuperacionSesion>(resultado);
                }
                return recuperacionSesion;
            }
            catch (Exception ex)
            {
                throw ex; 
            }

        }
        public TRecuperacionSesion Update(RecuperacionSesion entidad)
        {
            try
            {
                var RecuperacionSesion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new {s.RowVersion});
                RecuperacionSesion.RowVersion = entidadExistente.RowVersion;
                base.Update(RecuperacionSesion);
                return RecuperacionSesion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRecuperacionSesion Add(RecuperacionSesion entidad)
        {
            try
            {
                var RecuperacionSesion = MapeoEntidad(entidad);
                base.Insert(RecuperacionSesion);
                return RecuperacionSesion;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Exist(int id)
        {
            try
            {
            return _entities.Any(w=>w.Id == id && w.Estado == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
