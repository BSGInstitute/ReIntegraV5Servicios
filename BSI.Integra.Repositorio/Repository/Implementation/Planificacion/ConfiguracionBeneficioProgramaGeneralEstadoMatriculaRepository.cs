using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository
    /// Autor:  Gilmer Qm.
    /// Fecha: 18/07/2023
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneralEstadoMatricula
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository : GenericRepository<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>, IConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository
    {
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralEstadoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneralEstadoMatricula, ConfiguracionBeneficioProgramaGeneralEstadoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TConfiguracionBeneficioProgramaGeneralEstadoMatricula MapeoEntidad(ConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneralEstadoMatricula modelo = _mapper.Map<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>(entidad);

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

        public TConfiguracionBeneficioProgramaGeneralEstadoMatricula Add(ConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralEstadoMatricula = MapeoEntidad(entidad);
                base.Insert(ConfiguracionBeneficioProgramaGeneralEstadoMatricula);
                return ConfiguracionBeneficioProgramaGeneralEstadoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionBeneficioProgramaGeneralEstadoMatricula Update(ConfiguracionBeneficioProgramaGeneralEstadoMatricula entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralEstadoMatricula = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionBeneficioProgramaGeneralEstadoMatricula.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionBeneficioProgramaGeneralEstadoMatricula);
                return ConfiguracionBeneficioProgramaGeneralEstadoMatricula;
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


        public IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> listadoEntidad)
        {
            try
            {
                List<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> listado = new List<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>();
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

        public IEnumerable<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionBeneficioProgramaGeneralEstadoMatricula> listado = new List<TConfiguracionBeneficioProgramaGeneralEstadoMatricula>();
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
        /// Autor:  Gilmer Qm.
        /// Fecha: 18/07/2023
        /// Version: 1.0 
        /// <param name="idConfiguracionBeneficioPgneral"> (PK) de T_ConfiguracionBeneficioProgramaGeneral </param>
        /// <summary>
        /// Obtiene todos los registros asociados al IdConfiguracionBeneficioPgneral
        /// </summary>
        /// <returns> List<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> </returns>
        public IEnumerable<ConfiguracionBeneficioProgramaGeneralEstadoMatricula> ObtenerPorIdConfiguracionBeneficioPgneral(int idConfiguracionBeneficioPgneral)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdConfiguracionBeneficioPGneral IdConfiguracionBeneficioPgneral,
                                       IdEstadoMatricula,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM pla.T_ConfiguracionBeneficioProgramaGeneralEstadoMatricula
                                WHERE Estado = 1
                                      AND IdConfiguracionBeneficioPGneral = @IdConfiguracionBeneficioPGneral;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdConfiguracionBeneficioPGneral = idConfiguracionBeneficioPgneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                    return JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralEstadoMatricula>>(resultado)!;
                return new List<ConfiguracionBeneficioProgramaGeneralEstadoMatricula>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
