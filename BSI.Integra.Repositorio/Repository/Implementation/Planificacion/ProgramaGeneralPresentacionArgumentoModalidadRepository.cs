using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoModalidadRepository : GenericRepository<TProgramaGeneralPresentacionArgumentoModalidad>, IProgramaGeneralPresentacionArgumentoModalidadRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPresentacionArgumentoModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoModalidad, ProgramaGeneralPresentacionArgumentoModalidad>(MemberList.None).ReverseMap();
        
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPresentacionArgumentoModalidad MapeoEntidad(ProgramaGeneralPresentacionArgumentoModalidad entidad)
        {
            try
            {
                TProgramaGeneralPresentacionArgumentoModalidad modelo = _mapper.Map<TProgramaGeneralPresentacionArgumentoModalidad>(entidad);


                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumentoModalidad Add(ProgramaGeneralPresentacionArgumentoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumentoModalidad = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPresentacionArgumentoModalidad);
                return ProgramaGeneralPresentacionArgumentoModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumentoModalidad Update(ProgramaGeneralPresentacionArgumentoModalidad entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumentoModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPresentacionArgumentoModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPresentacionArgumentoModalidad);
                return ProgramaGeneralPresentacionArgumentoModalidad;
            }
            catch (Exception ex)
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


        public IEnumerable<TProgramaGeneralPresentacionArgumentoModalidad> Add(IEnumerable<ProgramaGeneralPresentacionArgumentoModalidad> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPresentacionArgumentoModalidad> listado = new List<TProgramaGeneralPresentacionArgumentoModalidad>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TProgramaGeneralPresentacionArgumentoModalidad> Update(IEnumerable<ProgramaGeneralPresentacionArgumentoModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPresentacionArgumentoModalidad> listado = new List<TProgramaGeneralPresentacionArgumentoModalidad>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<ProgramaGeneralPresentacionArgumentoModalidadDTO> ObtenerProgramaGeneralPresentacionArgumentoModalidad()
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoModalidadDTO> rpta = new List<ProgramaGeneralPresentacionArgumentoModalidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralPresentacionArgumento,
	                    IdModalidadCurso,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralPresentacionArgumentoModalidad
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoModalidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProgramaGeneralPresentacionArgumentoModalidadDTO> ObtenerModalidadPorIdPresentacionArgumento(int id)
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoModalidadDTO> modalidades = new List<ProgramaGeneralPresentacionArgumentoModalidadDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralPresentacionArgumento,
	                    IdModalidadCurso,
	                    Nombre,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralPresentacionArgumentoModalidad
                    WHERE Estado = 1
                        AND IdProgramaGeneralPresentacionArgumento = @id";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    modalidades = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoModalidadDTO>>(resultadoQuery);
                }
                return modalidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
