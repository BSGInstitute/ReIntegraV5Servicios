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
    /// Repositorio: EsquemaEvaluacionPgeneralProveedorRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacionPGeneralProveedor
    /// </summary>
    public class EsquemaEvaluacionPgeneralProveedorRepository : GenericRepository<TEsquemaEvaluacionPgeneralProveedor>, IEsquemaEvaluacionPgeneralProveedorRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionPgeneralProveedorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionPgeneralProveedor, EsquemaEvaluacionPgeneralProveedor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEsquemaEvaluacionPgeneralProveedor MapeoEntidad(EsquemaEvaluacionPgeneralProveedor entidad)
        {
            try
            {
                //crea la entidad padre
                TEsquemaEvaluacionPgeneralProveedor perfilAtrabajoCoeficiente = _mapper.Map<TEsquemaEvaluacionPgeneralProveedor>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacionPgeneralProveedor Add(EsquemaEvaluacionPgeneralProveedor entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEsquemaEvaluacionPgeneralProveedor Update(EsquemaEvaluacionPgeneralProveedor entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
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

        public IEnumerable<TEsquemaEvaluacionPgeneralProveedor> Add(IEnumerable<EsquemaEvaluacionPgeneralProveedor> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacionPgeneralProveedor> listado = new List<TEsquemaEvaluacionPgeneralProveedor>();
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

        public IEnumerable<TEsquemaEvaluacionPgeneralProveedor> Update(IEnumerable<EsquemaEvaluacionPgeneralProveedor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacionPgeneralProveedor> listado = new List<TEsquemaEvaluacionPgeneralProveedor>();
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
        /// <returns> Lista Entidad - List<EsquemaEvaluacionPGeneralProveedor>() </returns>
        public IEnumerable<EsquemaEvaluacionPgeneralProveedor> ObtenerPorIdEsquemaEvaluacionPGeneral(int idEsquemaEvaluacionPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdEsquemaEvaluacionPGeneral,
                        IdProveedor,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_EsquemaEvaluacionPGeneralProveedor
                    WHERE
                        Estado = 1 AND IdEsquemaEvaluacionPGeneral = @idEsquemaEvaluacionPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idEsquemaEvaluacionPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EsquemaEvaluacionPgeneralProveedor>>(resultado)!;
                }
                return new List<EsquemaEvaluacionPgeneralProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPCDR-OPIPG-002@Error en ObtenerPorIdEsquemaEvaluacionPGeneral() {ex.Message}", ex);
            }
        }
    }
}
