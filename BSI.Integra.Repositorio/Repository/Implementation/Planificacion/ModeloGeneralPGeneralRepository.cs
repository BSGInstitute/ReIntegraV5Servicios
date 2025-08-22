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
    /// Repositorio: ModeloGeneralPGeneralRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloGeneralPGeneral
    /// </summary>
    public class ModeloGeneralPGeneralRepository : GenericRepository<TModeloGeneralPgeneral>, IModeloGeneralPGeneralRepository
    {
        private Mapper _mapper;

        public ModeloGeneralPGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloGeneralPgeneral, ModeloGeneralPGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloGeneralPgeneral MapeoEntidad(ModeloGeneralPGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloGeneralPgeneral modeloGeneralPgeneral = _mapper.Map<TModeloGeneralPgeneral>(entidad);

                return modeloGeneralPgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloGeneralPgeneral Add(ModeloGeneralPGeneral entidad)
        {
            try
            {
                var modeloGeneralPgeneral = MapeoEntidad(entidad);
                base.Insert(modeloGeneralPgeneral);
                return modeloGeneralPgeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloGeneralPgeneral Update(ModeloGeneralPGeneral entidad)
        {
            try
            {
                var modeloGeneralPgeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modeloGeneralPgeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(modeloGeneralPgeneral);
                return modeloGeneralPgeneral;
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

        public IEnumerable<TModeloGeneralPgeneral> Add(IEnumerable<ModeloGeneralPGeneral> listadoEntidad)
        {
            try
            {
                List<TModeloGeneralPgeneral> listado = new List<TModeloGeneralPgeneral>();
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

        public IEnumerable<TModeloGeneralPgeneral> Update(IEnumerable<ModeloGeneralPGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloGeneralPgeneral> listado = new List<TModeloGeneralPgeneral>();
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
        /// Obtiene toda información de T_ModeloGeneralPGeneral por medio del Id
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <returns> Entidad - ModeloGeneralPGeneral </returns>
        public ModeloGeneralPGeneral? ObtenerPorIdPGeneral(int idProgramaGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdProgramaGeneral,
                        IdModeloGeneral,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloGeneralPGeneral
                    WHERE
                        Estado = 1 AND IdProgramaGeneral = @idProgramaGeneral";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloGeneralPGeneral>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MGPGR-OPI-001@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
    }
}
