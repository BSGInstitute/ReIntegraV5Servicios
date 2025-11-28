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
    public class PaqueteTutorVirtualBeneficioRepository : GenericRepository<TPaqueteTutorVirtualPaisBeneficio>, IPaqueteTutorVirtualBeneficioRepository
    {
        private Mapper _mapper;

        public PaqueteTutorVirtualBeneficioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPaqueteTutorVirtualPaisBeneficio, PaqueteTutorVirtualBeneficio>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualBeneficio, PaqueteTutorVirtualBeneficioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtualBeneficio, TPaqueteTutorVirtualPaisBeneficio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPaqueteTutorVirtualPaisBeneficio MapeoEntidad(PaqueteTutorVirtualBeneficio entidad)
        {
            try
            {
                //crea la entidad
                TPaqueteTutorVirtualPaisBeneficio modelo = _mapper.Map<TPaqueteTutorVirtualPaisBeneficio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPaqueteTutorVirtualPaisBeneficio Add(PaqueteTutorVirtualBeneficio entidad)
        {
            try
            {
                var beneficio = MapeoEntidad(entidad);
                base.Insert(beneficio);
                return beneficio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPaqueteTutorVirtualPaisBeneficio Update(PaqueteTutorVirtualBeneficio entidad)
        {
            try
            {
                var beneficio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                beneficio.RowVersion = entidadExistente.RowVersion;

                base.Update(beneficio);
                return beneficio;
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


        public IEnumerable<TPaqueteTutorVirtualPaisBeneficio> Add(IEnumerable<PaqueteTutorVirtualBeneficio> listadoEntidad)
        {
            try
            {
                List<TPaqueteTutorVirtualPaisBeneficio> listado = new List<TPaqueteTutorVirtualPaisBeneficio>();
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

        public IEnumerable<TPaqueteTutorVirtualPaisBeneficio> Update(IEnumerable<PaqueteTutorVirtualBeneficio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPaqueteTutorVirtualPaisBeneficio> listado = new List<TPaqueteTutorVirtualPaisBeneficio>();
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
        /// Obtiene todos los registros de T_PaqueteTutorVirtualPaisBeneficio.
        /// </summary>
        /// <returns> List<PaqueteTutorVirtualBeneficioDTO> </returns>
        public IEnumerable<PaqueteTutorVirtualBeneficioDTO> Obtener()
        {
            try
            {
                List<PaqueteTutorVirtualBeneficioDTO> rpta = new List<PaqueteTutorVirtualBeneficioDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtualPais,
	                    Nombre
                    FROM pla.T_PaqueteTutorVirtualPaisBeneficio
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaqueteTutorVirtualBeneficioDTO>>(resultado);

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
        /// <returns>PaqueteTutorVirtualBeneficio || null</returns>
        public PaqueteTutorVirtualBeneficio? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtualPais,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_PaqueteTutorVirtualPaisBeneficio
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PaqueteTutorVirtualBeneficio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OBI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 27/11/2024
        /// <param name="idPaqueteTutorVirtualPais"> Id del paquete tutor virtual país </param> 
        /// <summary>
        /// Obtiene los registros por el IdPaqueteTutorVirtualPais
        /// </summary>
        /// <returns>List<PaqueteTutorVirtualBeneficioDTO></returns>
        public IEnumerable<PaqueteTutorVirtualBeneficioDTO> ObtenerPorIdPaquetePais(int idPaqueteTutorVirtualPais)
        {
            try
            {
                List<PaqueteTutorVirtualBeneficioDTO> rpta = new List<PaqueteTutorVirtualBeneficioDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPaqueteTutorVirtualPais,
	                    Nombre
                    FROM pla.T_PaqueteTutorVirtualPaisBeneficio
                    WHERE IdPaqueteTutorVirtualPais = @idPaqueteTutorVirtualPais AND Estado = 1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idPaqueteTutorVirtualPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaqueteTutorVirtualBeneficioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OBIPP-001@Error en ObtenerPorIdPaquetePais(), {ex.Message}");
            }
        }

    }
}
