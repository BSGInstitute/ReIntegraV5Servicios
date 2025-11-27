using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class PaqueteTutorVirtualPaisRepository : GenericRepository<TPaqueteTutorVirtualPai>, IPaqueteTutorVirtualPaisRepository
    {
        private Mapper _mapper;

        public PaqueteTutorVirtualPaisRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPaqueteTutorVirtualPai, PaqueteTutorVirtualPais>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualPais, PaqueteTutorVirtualPaisDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualPais, TPaqueteTutorVirtualPai>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPaqueteTutorVirtualPaisBeneficio, PaqueteTutorVirtualBeneficio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPaqueteTutorVirtualPai MapeoEntidad(PaqueteTutorVirtualPais entidad)
        {
            try
            {
                //crea la entidad padre
                TPaqueteTutorVirtualPai modelo = _mapper.Map<TPaqueteTutorVirtualPai>(entidad);

                //mapea los hijos (beneficios)
                if (entidad.ListadoBeneficios != null && entidad.ListadoBeneficios.Count > 0)
                {
                    var listadoBeneficios = _mapper.Map<List<TPaqueteTutorVirtualPaisBeneficio>>(entidad.ListadoBeneficios);
                    foreach (var beneficio in listadoBeneficios)
                    {
                        modelo.TPaqueteTutorVirtualPaisBeneficios.Add(beneficio);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPaqueteTutorVirtualPai Add(PaqueteTutorVirtualPais entidad)
        {
            try
            {
                var paqueteTutorVirtualPais = MapeoEntidad(entidad);
                base.Insert(paqueteTutorVirtualPais);
                return paqueteTutorVirtualPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPaqueteTutorVirtualPai Update(PaqueteTutorVirtualPais entidad)
        {
            try
            {
                var paqueteTutorVirtualPais = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                paqueteTutorVirtualPais.RowVersion = entidadExistente.RowVersion;

                base.Update(paqueteTutorVirtualPais);
                return paqueteTutorVirtualPais;
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


        public IEnumerable<TPaqueteTutorVirtualPai> Add(IEnumerable<PaqueteTutorVirtualPais> listadoEntidad)
        {
            try
            {
                List<TPaqueteTutorVirtualPai> listado = new List<TPaqueteTutorVirtualPai>();
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

        public IEnumerable<TPaqueteTutorVirtualPai> Update(IEnumerable<PaqueteTutorVirtualPais> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPaqueteTutorVirtualPai> listado = new List<TPaqueteTutorVirtualPai>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 27/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PaqueteTutorVirtualPais.
        /// </summary>
        /// <returns> List<PaqueteTutorVirtualPaisDTO> </returns>
        public IEnumerable<PaqueteTutorVirtualPaisDTO> Obtener()
        {
            try
            {
                List<PaqueteTutorVirtualPaisDTO> rpta = new List<PaqueteTutorVirtualPaisDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtual,
	                    IdPais,
	                    IdMoneda,
	                    CostoIndividual,
	                    CostoPaquete
                    FROM gp.T_PaqueteTutorVirtualPais
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaqueteTutorVirtualPaisDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 27/11/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>PaqueteTutorVirtualPais || null</returns>
        public PaqueteTutorVirtualPais? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtual,
	                    IdPais,
	                    IdMoneda,
	                    CostoIndividual,
	                    CostoPaquete,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM gp.T_PaqueteTutorVirtualPais
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PaqueteTutorVirtualPais>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 27/11/2024
        /// <param name="idPaqueteTutorVirtual"> Id del paquete tutor virtual </param> 
        /// <summary>
        /// Obtiene los registros por el IdPaqueteTutorVirtual
        /// </summary>
        /// <returns>List<PaqueteTutorVirtualPaisDTO></returns>
        public IEnumerable<PaqueteTutorVirtualPaisDTO> ObtenerPorIdPaquete(int idPaqueteTutorVirtual)
        {
            try
            {
                List<PaqueteTutorVirtualPaisDTO> rpta = new List<PaqueteTutorVirtualPaisDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtual,
	                    IdPais,
	                    IdMoneda,
	                    CostoIndividual,
	                    CostoPaquete
                    FROM gp.T_PaqueteTutorVirtualPais
                    WHERE IdPaqueteTutorVirtual = @idPaqueteTutorVirtual AND Estado = 1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPaqueteTutorVirtual });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaqueteTutorVirtualPaisDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPIP-001@Error en ObtenerPorIdPaquete(), {ex.Message}");
            }
        }

    }
}
