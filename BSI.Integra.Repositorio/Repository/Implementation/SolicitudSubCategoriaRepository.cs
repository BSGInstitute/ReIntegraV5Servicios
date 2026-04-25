using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Repositorio: SolicitudSubCategoriaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudSubCategoria
    /// </summary>
    public class SolicitudSubCategoriaRepository : GenericRepository<TSolicitudSubCategorium>, ISolicitudSubCategoriaRepository
    {
        private Mapper _mapper;

        public SolicitudSubCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudSubCategorium, SolicitudSubCategoria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSolicitudSubCategorium MapeoEntidad(SolicitudSubCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudSubCategorium modelo = _mapper.Map<TSolicitudSubCategorium>(entidad);

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

        public TSolicitudSubCategorium Add(SolicitudSubCategoria entidad)
        {
            try
            {
                var SolicitudSubCategoria = MapeoEntidad(entidad);
                base.Insert(SolicitudSubCategoria);
                return SolicitudSubCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudSubCategorium Update(SolicitudSubCategoria entidad)
        {
            try
            {
                var SolicitudSubCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudSubCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudSubCategoria);
                return SolicitudSubCategoria;
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


        public IEnumerable<TSolicitudSubCategorium> Add(IEnumerable<SolicitudSubCategoria> listadoEntidad)
        {
            try
            {
                List<TSolicitudSubCategorium> listado = new List<TSolicitudSubCategorium>();
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

        public IEnumerable<TSolicitudSubCategorium> Update(IEnumerable<SolicitudSubCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudSubCategorium> listado = new List<TSolicitudSubCategorium>();
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
        /// Autor: Gilmer Quispe
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudSubCategoria </returns>
        public SolicitudSubCategoria ObtenerPorId(int id)
        {
            try
            {
                var rpta = new SolicitudSubCategoria();
                var query = @"SELECT Id,
                                       Nombre,
                                       IdSolicitudCategoria,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM ope.T_SolicitudSubCategoria 
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<SolicitudSubCategoria>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla con Estado = 1 
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboSubCategoriaDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboSubCategoriaDTO>();
                var query = @"SELECT Id,Descripcion AS Nombre,IdSolicitudCategoria,DescripcionSolucion, Titulo
                                FROM ope.T_SolicitudProblema 
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboSubCategoriaDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudSubCategoria </returns>
        public bool InsertarProblema(SolicitudProblemaEntradaDTO solicitudProblemaEntradaDTO)
        {
            try
            {

            var query = "[ope].[SP_InsertSolicitudProblema]";

            var parameters = new
            {
                Descripcion = solicitudProblemaEntradaDTO.Descripcion,
                Titulo = solicitudProblemaEntradaDTO.Titulo,
                DescripcionSolucion = solicitudProblemaEntradaDTO.DescripcionSolucion,
                Prioridad = solicitudProblemaEntradaDTO.Prioridad,
                IdSolicitudCategoria = solicitudProblemaEntradaDTO.IdSolicitudCategoria,
                IdPersonal_Revision = solicitudProblemaEntradaDTO.IdPersonalRevision,
                IdPersonal_Solucion = solicitudProblemaEntradaDTO.IdPersonalSolucion,
                Estado = 1,
                UsuarioCreacion = solicitudProblemaEntradaDTO.Usuario,
                UsuarioModificacion = solicitudProblemaEntradaDTO.Usuario
            };

            var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parameters);

                if (resultado != null && int.TryParse(resultado.ToString(), out int id))
                {
                    return id > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudSubCategoria </returns>
        public bool ActualizarProblema(SolicitudProblemaEntradaDTO solicitudProblemaEntradaDTO)
        {
            try
            {
                var query = "[ope].[SP_UpdateSolicitudProblema]";

                var parameters = new
                {
                    Id = solicitudProblemaEntradaDTO.Id,
                    Descripcion = solicitudProblemaEntradaDTO.Descripcion,
                    Titulo = solicitudProblemaEntradaDTO.Titulo,
                    DescripcionSolucion = solicitudProblemaEntradaDTO.DescripcionSolucion,
                    Prioridad = solicitudProblemaEntradaDTO.Prioridad,
                    IdSolicitudCategoria = solicitudProblemaEntradaDTO.IdSolicitudCategoria,
                    IdPersonal_Revision = solicitudProblemaEntradaDTO.IdPersonalRevision,
                    IdPersonal_Solucion = solicitudProblemaEntradaDTO.IdPersonalSolucion,
                    Estado = 1,
                    UsuarioModificacion = solicitudProblemaEntradaDTO.Usuario
                };

                // Ejecutar el stored procedure y obtener el resultado
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parameters);

                // Verificar si el resultado no es nulo y convertirlo a int
                if (resultado != null && int.TryParse(resultado.ToString(), out int updateResult))
                {
                    return updateResult == 1;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudSubCategoria </returns>
        public bool EliminarProblema(SolicitudProblemaEntradaDTO solicitudProblemaEntradaDTO)
        {
            try
            {
                var query = "[ope].[SP_DeleteSolicitudProblema]";

                var parameters = new
                {
                    Id = solicitudProblemaEntradaDTO.Id,
                    UsuarioModificacion = solicitudProblemaEntradaDTO.Usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parameters);

                if (resultado != null)
                {
                    return  true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
