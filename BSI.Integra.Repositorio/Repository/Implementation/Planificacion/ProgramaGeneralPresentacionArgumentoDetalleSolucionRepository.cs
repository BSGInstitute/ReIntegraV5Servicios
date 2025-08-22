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
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository : GenericRepository<TProgramaGeneralPresentacionArgumentoDetalleSolucion>, IProgramaGeneralPresentacionArgumentoDetalleSolucionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPresentacionArgumentoDetalleSolucion, ProgramaGeneralPresentacionArgumentoDetalleSolucion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }



        #region Metodos Base
        private TProgramaGeneralPresentacionArgumentoDetalleSolucion MapeoEntidad(ProgramaGeneralPresentacionArgumentoDetalleSolucion entidad)
        {
            try
            {
                TProgramaGeneralPresentacionArgumentoDetalleSolucion modelo = _mapper.Map<TProgramaGeneralPresentacionArgumentoDetalleSolucion>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumentoDetalleSolucion Add(ProgramaGeneralPresentacionArgumentoDetalleSolucion entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumentoDetalleSolucion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPresentacionArgumentoDetalleSolucion);
                return ProgramaGeneralPresentacionArgumentoDetalleSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPresentacionArgumentoDetalleSolucion Update(ProgramaGeneralPresentacionArgumentoDetalleSolucion entidad)
        {
            try
            {
                var ProgramaGeneralPresentacionArgumentoDetalleSolucion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPresentacionArgumentoDetalleSolucion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPresentacionArgumentoDetalleSolucion);
                return ProgramaGeneralPresentacionArgumentoDetalleSolucion;
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


        public IEnumerable<TProgramaGeneralPresentacionArgumentoDetalleSolucion> Add(IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPresentacionArgumentoDetalleSolucion> listado = new List<TProgramaGeneralPresentacionArgumentoDetalleSolucion>();
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

        public IEnumerable<TProgramaGeneralPresentacionArgumentoDetalleSolucion> Update(IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPresentacionArgumentoDetalleSolucion> listado = new List<TProgramaGeneralPresentacionArgumentoDetalleSolucion>();
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

        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucion()
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> rpta = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralPresentacionArgumento,
	                    Detalle,
	                    Solucion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ProgramaGeneralPresentacionArgumentoDetalleSolucion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO> ObtenerCombo()
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO> rpta = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO>();
                var query = @"
                    SELECT Id,IdProgramaGeneralPresentacionArgumento,Detalle,Solucion
                    FROM pla.T_ProgramaGeneralPresentacionArgumentoDetalleSolucion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralPresentacionArgumentoDetalleSolucion ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id	,
                        IdProgramaGeneralPresentacionArgumento,
                        Detalle,
                        Solucion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion
                        FROM pla.T_ProgramaGeneralPresentacionArgumentoDetalleSolucion
                        WHERE Estado = 1 AND Id=@id
                        ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralPresentacionArgumentoDetalleSolucion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(int idProgramaGeneralPresentacionArgumento, int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> rpta = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerArgumentoPresentacionArgumentoProgramaGeneral", new { idOportunidad, idProgramaGeneralPresentacionArgumento });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> ObtenerPresentacionArgumentoDetalleSolucionPorIdPresentacionArgumento(int idPresentacionArgumento)
        {
            try
            {
                List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> detalleSolucion = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralPresentacionArgumento,
	                    Detalle,
	                    Solucion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
                        RowVersion
                    FROM pla.T_ProgramaGeneralPresentacionArgumentoDetalleSolucion
                    WHERE Estado = 1
                        AND IdProgramaGeneralPresentacionArgumento = @idPresentacionArgumento";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPresentacionArgumento });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    detalleSolucion = JsonConvert.DeserializeObject<List<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO>>(resultadoQuery);
                }
                return detalleSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
