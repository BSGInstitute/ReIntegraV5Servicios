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
    public class PaqueteTutorVirtualRepository : GenericRepository<TPaqueteTutorVirtual>, IPaqueteTutorVirtualRepository
    {
        private Mapper _mapper;

        public PaqueteTutorVirtualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPaqueteTutorVirtual, PaqueteTutorVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtual, PaqueteTutorVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PaqueteTutorVirtual, TPaqueteTutorVirtual>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPaqueteTutorVirtual MapeoEntidad(PaqueteTutorVirtual entidad)
        {
            try
            {
                //crea la entidad padre
                TPaqueteTutorVirtual modelo = _mapper.Map<TPaqueteTutorVirtual>(entidad);

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

        public TPaqueteTutorVirtual Add(PaqueteTutorVirtual entidad)
        {
            try
            {
                var PaqueteTutorVirtual = MapeoEntidad(entidad);
                base.Insert(PaqueteTutorVirtual);
                return PaqueteTutorVirtual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPaqueteTutorVirtual Update(PaqueteTutorVirtual entidad)
        {
            try
            {
                var PaqueteTutorVirtual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PaqueteTutorVirtual.RowVersion = entidadExistente.RowVersion;

                base.Update(PaqueteTutorVirtual);
                return PaqueteTutorVirtual;
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


        public IEnumerable<TPaqueteTutorVirtual> Add(IEnumerable<PaqueteTutorVirtual> listadoEntidad)
        {
            try
            {
                List<TPaqueteTutorVirtual> listado = new List<TPaqueteTutorVirtual>();
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

        public IEnumerable<TPaqueteTutorVirtual> Update(IEnumerable<PaqueteTutorVirtual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPaqueteTutorVirtual> listado = new List<TPaqueteTutorVirtual>();
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
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PaqueteTutorVirtual.
        /// </summary>
        /// <returns> List<PaqueteTutorVirtualDTO> </returns>
        public IEnumerable<PaqueteTutorVirtualDTO> Obtener()
        {
            try
            {
                List<PaqueteTutorVirtualDTO> rpta = new List<PaqueteTutorVirtualDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_PaqueteTutorVirtual
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaqueteTutorVirtualDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >PaqueteTutorVirtual || null</returns>
        public PaqueteTutorVirtual? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PaqueteTutorVirtual
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PaqueteTutorVirtual>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

    }
}
