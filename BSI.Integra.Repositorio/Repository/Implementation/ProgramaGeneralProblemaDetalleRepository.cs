using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
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
    public class ProgramaGeneralProblemaDetalleRepository : GenericRepository<TProgramaGeneralProblemaDetalle>, IProgramaGeneralProblemaDetalleRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaDetalle, ProgramaGeneralProblemaDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaDetalle MapeoEntidad(ProgramaGeneralProblemaDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaDetalle modelo = _mapper.Map<TProgramaGeneralProblemaDetalle>(entidad);

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

        public TProgramaGeneralProblemaDetalle Add(ProgramaGeneralProblemaDetalle entidad)
        {
            try
            {
                var ProgramaGeneralProblemaDetalle = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaDetalle);
                return ProgramaGeneralProblemaDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaDetalle Update(ProgramaGeneralProblemaDetalle entidad)
        {
            try
            {
                var ProgramaGeneralProblemaDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaDetalle);
                return ProgramaGeneralProblemaDetalle;
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


        public IEnumerable<TProgramaGeneralProblemaDetalle> Add(IEnumerable<ProgramaGeneralProblemaDetalle> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaDetalle> listado = new List<TProgramaGeneralProblemaDetalle>();
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

        public IEnumerable<TProgramaGeneralProblemaDetalle> Update(IEnumerable<ProgramaGeneralProblemaDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaDetalle> listado = new List<TProgramaGeneralProblemaDetalle>();
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



        public ProgramaGeneralProblemaDetalle? ObtenerPorId(int id)
        {
            try
            {
                ProgramaGeneralProblemaDetalle rpta = new();
                var query = @"
                    SELECT
	                    Id,
                        IdPGeneral,
                        IdProgramaGeneralProblemaFactor,
                        IdProgramaGeneralProblemaFactorDetalle,
	                    AplicaDescripcionSolucion,
                        AplicaNombreDetalle,
                        AplicaPieDePagina,
                        AplicaSubTituloSolucion,
                        AplicaTituloDetalle,
                        AplicaTituloSolucion,
                        Estado,
                        FechaCreacion,
                        FechaModificacion,
	                    UsuarioCreacion,
                        UsuarioModificacion,
                        RowVersion
                    FROM pla.T_ProgramaGeneralProblemaDetalle
                    WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }


        public IEnumerable<ProblemaClienteByPGeneral> Obtener(int idPGeneral)
        {
            try
            {
                List<ProblemaClienteByPGeneral> rpta = new List<ProblemaClienteByPGeneral>();
                var query = @"
                    SELECT AplicaDescripcionSolucion,
                       AplicaNombreDetalle,
                       AplicaPieDePagina,
                       AplicaSubTituloSolucion,
                       AplicaTituloDetalle,
                       AplicaTituloSolucion,
                       IdPGeneral,
                       Id,
                       IdProgramaGeneralProblemaFactorDetalle,
                       IdProgramaGeneralProblemaFactor,
                       IdProgramaGeneralProblemaFactorSolucion,
                       IdProgramaGeneralProblemaFactorSubSolucion,
                       IdProgramaGeneralProblemaFactorSubSolucionAsignada
                    FROM pla.V_ObtenerConfiguracionProblemaFactoByPGeneral where IdPGeneral=@idPGeneral ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProblemaClienteByPGeneral>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProblemaClienteByPGeneral> ObtenerProblemaCliente(int idPGeneral)
        {
            try
            {
                List<ProblemaClienteByPGeneral> rpta = new List<ProblemaClienteByPGeneral>(idPGeneral);
                var query = @"
                    SELECT AplicaDescripcionSolucion,
                       AplicaNombreDetalle,
                       AplicaPieDePagina,
                       AplicaSubTituloSolucion,
                       AplicaTituloDetalle,
                       AplicaTituloSolucion,
                       IdPGeneral,
                       Id,
                       IdProgramaGeneralProblemaFactorDetalle,
                       IdProgramaGeneralProblemaFactor,
                       IdProgramaGeneralProblemaFactorSolucion,
                       IdProgramaGeneralProblemaFactorSubSolucion,
                       IdProgramaGeneralProblemaFactorSubSolucionAsignada
                    FROM pla.V_ObtenerConfiguracionProblemaFactoByPGeneral where IdPGeneral=@idPGeneral ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProblemaClienteByPGeneral>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProblemaAgendaRow> ObtenerProblemasClienteAgendaV6(int idPGeneral, int idOportundad)
        {
            try
            {
                List<ProblemaAgendaRow> rpta = new List<ProblemaAgendaRow>();

                var query = "pla.SP_TProgramaGeneralProblemaDetalle_ObtenerPorIdPGeneralYOportunidad";
                var parametros = new
                {
                   IdPGeneral = idPGeneral,
                   IdOportunidad = idOportundad
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProblemaAgendaRow>>(resultado);
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
