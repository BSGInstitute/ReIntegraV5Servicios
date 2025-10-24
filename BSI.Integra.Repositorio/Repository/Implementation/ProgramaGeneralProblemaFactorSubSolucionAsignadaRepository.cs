using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
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
    public class ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository : GenericRepository<TProgramaGeneralProblemaFactorSubSolucionAsignadum>, IProgramaGeneralProblemaFactorSubSolucionAsignadaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSubSolucionAsignadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucionAsignadum, ProgramaGeneralProblemaFactorSubSolucionAsignada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactorSubSolucionAsignadum MapeoEntidad(ProgramaGeneralProblemaFactorSubSolucionAsignada entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactorSubSolucionAsignadum modelo = _mapper.Map<TProgramaGeneralProblemaFactorSubSolucionAsignadum>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorSubSolucionAsignadum Add(ProgramaGeneralProblemaFactorSubSolucionAsignada entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSubSolucionAsignada = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactorSubSolucionAsignada);
                return ProgramaGeneralProblemaFactorSubSolucionAsignada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorSubSolucionAsignadum Update(ProgramaGeneralProblemaFactorSubSolucionAsignada entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSubSolucionAsignada = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactorSubSolucionAsignada.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactorSubSolucionAsignada);
                return ProgramaGeneralProblemaFactorSubSolucionAsignada;
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


        public IEnumerable<TProgramaGeneralProblemaFactorSubSolucionAsignadum> Add(IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactorSubSolucionAsignadum> listado = new List<TProgramaGeneralProblemaFactorSubSolucionAsignadum>();
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

        public IEnumerable<TProgramaGeneralProblemaFactorSubSolucionAsignadum> Update(IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactorSubSolucionAsignadum> listado = new List<TProgramaGeneralProblemaFactorSubSolucionAsignadum>();
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

        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> ObtenerPorIdProblemaDetalle(int idProblemaDetalle)
        {
            try
            {
                var query = @"
                        SELECT 
                           Id,
                           IdProgramaGeneralProblemaDetalle,
                           IdProgramaGeneralProblemaFactorSubSolucion,
	                       Estado,
                           FechaCreacion,
                           FechaModificacion,
                           UsuarioCreacion,
                           UsuarioModificacion,
	                       RowVersion
                        FROM pla.T_ProgramaGeneralProblemaFactorSubSolucionAsignada
                        WHERE Estado = 1 AND IdProgramaGeneralProblemaDetalle=@idProblemaDetalle";
                var resultado = _dapperRepository.QueryDapper(query, new { idProblemaDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada>>(resultado)!;
                }
                return new List<ProgramaGeneralProblemaFactorSubSolucionAsignada>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PBPwR-OPIP-001@Error en ObtenerPorIdPartner(), {ex.Message}");
            }
        }

        public ProgramaGeneralProblemaFactorSubSolucionAsignada? ObtenerPorId(int id)
        {
            try
            {
                ProgramaGeneralProblemaFactorSubSolucionAsignada rpta = new();
                var query = @"
                    SELECT
	                       Id,
                           IdProgramaGeneralProblemaDetalle,
                           IdProgramaGeneralProblemaFactorSubSolucion,
	                       Estado,
                           FechaCreacion,
                           FechaModificacion,
                           UsuarioCreacion,
                           UsuarioModificacion,
	                       RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactorSubSolucionAsignada
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactorSubSolucionAsignada>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }


    }
}
