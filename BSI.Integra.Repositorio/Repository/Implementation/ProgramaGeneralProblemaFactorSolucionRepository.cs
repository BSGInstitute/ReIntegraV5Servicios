using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
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
    public class ProgramaGeneralProblemaFactorSolucionRepository : GenericRepository<TProgramaGeneralProblemaFactorSolucion>, IProgramaGeneralProblemaFactorSolucionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSolucionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSolucion, ProgramaGeneralProblemaFactorSolucion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactorSolucion MapeoEntidad(ProgramaGeneralProblemaFactorSolucion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactorSolucion modelo = _mapper.Map<TProgramaGeneralProblemaFactorSolucion>(entidad);

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

        public TProgramaGeneralProblemaFactorSolucion Add(ProgramaGeneralProblemaFactorSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSolucion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactorSolucion);
                return ProgramaGeneralProblemaFactorSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorSolucion Update(ProgramaGeneralProblemaFactorSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSolucion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactorSolucion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactorSolucion);
                return ProgramaGeneralProblemaFactorSolucion;
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


        public IEnumerable<TProgramaGeneralProblemaFactorSolucion> Add(IEnumerable<ProgramaGeneralProblemaFactorSolucion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactorSolucion> listado = new List<TProgramaGeneralProblemaFactorSolucion>();
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

        public IEnumerable<TProgramaGeneralProblemaFactorSolucion> Update(IEnumerable<ProgramaGeneralProblemaFactorSolucion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactorSolucion> listado = new List<TProgramaGeneralProblemaFactorSolucion>();
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
        /// Fecha: 17/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de ProgramaGeneralProblemaFactorSolucion.
        /// </summary>
        /// <returns> List<TipoFormacionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralProblemaFactorSolucionDTO> rpta = new List<ProgramaGeneralProblemaFactorSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,Descripcion,Titulo,SubTitulo
                    FROM pla.T_ProgramaGeneralProblemaFactorSolucion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaFactorSolucionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO>> ObtenerAsync()
        {
            try
            {
                var query = @"
                SELECT Id, Descripcion, Titulo, SubTitulo
                FROM pla.T_ProgramaGeneralProblemaFactorSolucion
                WHERE Estado = 1
                ORDER BY Id DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO>>(resultado)!;
                }
                return new List<ProgramaGeneralProblemaFactorSolucionDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerAsync() ProgramaGeneralProblemaFactorSolucionRepository, {ex.Message}");
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        ///  Fecha: 17/10/2025
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >TipoFormacion || null</returns>
        public ProgramaGeneralProblemaFactorSolucion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Descripcion,
                        Titulo,
                        SubTitulo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactorSolucion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactorSolucion>(resultado)!;
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
