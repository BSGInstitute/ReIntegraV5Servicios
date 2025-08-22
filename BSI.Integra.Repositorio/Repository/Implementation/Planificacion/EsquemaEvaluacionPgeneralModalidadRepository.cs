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
    /// Repositorio: EsquemaEvaluacionPGeneralModalidadRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacionPGeneralModalidad
    /// </summary>
    public class EsquemaEvaluacionPgeneralModalidadRepository : GenericRepository<TEsquemaEvaluacionPgeneralModalidad>, IEsquemaEvaluacionPgeneralModalidadRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionPgeneralModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionPgeneralModalidad, EsquemaEvaluacionPgeneralModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEsquemaEvaluacionPgeneralModalidad MapeoEntidad(EsquemaEvaluacionPgeneralModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneralModalidad esquemaEvaluacionPGeneralModalidad = _mapper.Map<TEsquemaEvaluacionPgeneralModalidad>(entidad);

                return esquemaEvaluacionPGeneralModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacionPgeneralModalidad Add(EsquemaEvaluacionPgeneralModalidad entidad)
        {
            try
            {
                var esquemaEvaluacionPGeneralModalidad = MapeoEntidad(entidad);
                base.Insert(esquemaEvaluacionPGeneralModalidad);
                return esquemaEvaluacionPGeneralModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacionPgeneralModalidad Update(EsquemaEvaluacionPgeneralModalidad entidad)
        {
            try
            {
                var esquemaEvaluacionPGeneralModalidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                esquemaEvaluacionPGeneralModalidad.RowVersion = entidadExistente.RowVersion;

                base.Update(esquemaEvaluacionPGeneralModalidad);
                return esquemaEvaluacionPGeneralModalidad;
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

        public IEnumerable<TEsquemaEvaluacionPgeneralModalidad> Add(IEnumerable<EsquemaEvaluacionPgeneralModalidad> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacionPgeneralModalidad> listado = new List<TEsquemaEvaluacionPgeneralModalidad>();
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

        public IEnumerable<TEsquemaEvaluacionPgeneralModalidad> Update(IEnumerable<EsquemaEvaluacionPgeneralModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacionPgeneralModalidad> listado = new List<TEsquemaEvaluacionPgeneralModalidad>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<EsquemaEvaluacionPGeneralModalidad>() </returns>
        public IEnumerable<EsquemaEvaluacionPgeneralModalidad> ObtenerPorIdEsquemaEvaluacionPGeneral(int idEsquemaEvaluacionPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdEsquemaEvaluacionPGeneral,
                        IdModalidadCurso,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_EsquemaEvaluacionPGeneralModalidad
                    WHERE
                        Estado = 1 AND IdEsquemaEvaluacionPGeneral = @idEsquemaEvaluacionPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idEsquemaEvaluacionPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EsquemaEvaluacionPgeneralModalidad>>(resultado)!;
                }
                return new List<EsquemaEvaluacionPgeneralModalidad>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#EEPGMR-OPIPG-002@Error en ObtenerPorIdEsquemaEvaluacionPGeneral() {ex.Message}", ex);
            }
        }
    }
}
