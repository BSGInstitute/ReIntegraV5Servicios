using AutoMapper;
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
    public class ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository : GenericRepository<TProgramaGeneralProblemaFactorSolucionRespuestum>, IProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucionRespuestum, ProgramaGeneralProblemaFactorSolucionRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactorSolucionRespuestum MapeoEntidad(ProgramaGeneralProblemaFactorSolucionRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactorSolucionRespuestum modelo = _mapper.Map<TProgramaGeneralProblemaFactorSolucionRespuestum>(entidad);

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

        public TProgramaGeneralProblemaFactorSolucionRespuestum Add(ProgramaGeneralProblemaFactorSolucionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSolucionRespuesta = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactorSolucionRespuesta);
                return ProgramaGeneralProblemaFactorSolucionRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorSolucionRespuestum Update(ProgramaGeneralProblemaFactorSolucionRespuesta entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSolucionRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactorSolucionRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactorSolucionRespuesta);
                return ProgramaGeneralProblemaFactorSolucionRespuesta;
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


        public IEnumerable<TProgramaGeneralProblemaFactorSolucionRespuestum> Add(IEnumerable<ProgramaGeneralProblemaFactorSolucionRespuesta> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactorSolucionRespuestum> listado = new List<TProgramaGeneralProblemaFactorSolucionRespuestum>();
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

        public IEnumerable<TProgramaGeneralProblemaFactorSolucionRespuestum> Update(IEnumerable<ProgramaGeneralProblemaFactorSolucionRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactorSolucionRespuestum> listado = new List<TProgramaGeneralProblemaFactorSolucionRespuestum>();
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


        public ProgramaGeneralProblemaFactorSolucionRespuesta ObtenerPorIdOportunidadIdProblemaFactorSolucion(int idOportunidad, int idProblemaFactorSolucion)
        {
            try
            {
                ProgramaGeneralProblemaFactorSolucionRespuesta rpta = new ProgramaGeneralProblemaFactorSolucionRespuesta();
                var query = @"
                           SELECT Id,
                               IdOportunidad,
                               IdProgramaGeneralProblemaFactorSolucion,
                               EsSolucionado,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion 
                            FROM pla.T_ProgramaGeneralProblemaFactorSolucionRespuesta
                            WHERE Estado = 1
                                AND IdOportunidad = @idOportunidad
                                AND IdProgramaGeneralProblemaFactorSolucion = @idProblemaFactorSolucion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOportunidad, idProblemaFactorSolucion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactorSolucionRespuesta>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
