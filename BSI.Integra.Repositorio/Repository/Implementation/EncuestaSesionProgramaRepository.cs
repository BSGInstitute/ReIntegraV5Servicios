using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
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

    /// Repositorio: EncuestaSesionProgramaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_EncuestaSesionPrograma
    /// </summary>
    public class EncuestaSesionProgramaRepository : GenericRepository<TEncuestaSesionPrograma>, IEncuestaSesionProgramaRepository
    {
        private Mapper _mapper;

        public EncuestaSesionProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEncuestaSesionPrograma, EncuestaSesionPrograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEncuestaSesionPrograma MapeoEntidad(EncuestaSesionPrograma entidad)
        {
            try
            {
                //crea la entidad padre
                TEncuestaSesionPrograma modelo = _mapper.Map<TEncuestaSesionPrograma>(entidad);

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

        public TEncuestaSesionPrograma Add(EncuestaSesionPrograma entidad)
        {
            try
            {
                var EncuestaSesionPrograma = MapeoEntidad(entidad);
                base.Insert(EncuestaSesionPrograma);
                return EncuestaSesionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEncuestaSesionPrograma Update(EncuestaSesionPrograma entidad)
        {
            try
            {
                var EncuestaSesionPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EncuestaSesionPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(EncuestaSesionPrograma);
                return EncuestaSesionPrograma;
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


        public IEnumerable<TEncuestaSesionPrograma> Add(IEnumerable<EncuestaSesionPrograma> listadoEntidad)
        {
            try
            {
                List<TEncuestaSesionPrograma> listado = new List<TEncuestaSesionPrograma>();
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

        public IEnumerable<TEncuestaSesionPrograma> Update(IEnumerable<EncuestaSesionPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEncuestaSesionPrograma> listado = new List<TEncuestaSesionPrograma>();
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


        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public EncuestaSesionPrograma ObtenerPorId(int id)
        {
            try
            {
                var rpta = new EncuestaSesionPrograma();
                var query = @"SELECT Id,
                                    IdPGeneral,
                                    IdPEspecifico,
                                    IdPEspecificoSesion,
                                    IdEncuestaOnline,
                                    EncuestaObligatoria,
                                    EncuestaActiva,
                                    AsignadoPara,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion FROM pla.T_EncuestaSesionPrograma
                                WHERE
                                	Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EncuestaSesionPrograma>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_PreguntaEncuestaCategoria WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public List<EncuestaProgramaDTO> ObtenerEncuestasPrograma(int idPespecifico)
        {
            try
            {
                var rpta = new List<EncuestaProgramaDTO>();
                var query = @"SELECT IdPEspecifico,
                                           IdPEspecificoSesion,
                                           FechaHoraInicio,
                                           NumeroEncuestas         
                                FROM [pla].[V_EncuestaSesionProgramaAsignada] 
                                WHERE IdPEspecifico=@idPespecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico=idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EncuestaProgramaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public List<EncuestaSesionAsignadaDTO> ObtenerEncuestaAsignada(int idPespecificoSesion)
        {
            try
            {
                var rpta = new List<EncuestaSesionAsignadaDTO>();
                var query = @"
                            SELECT IdEncuestaSesionPrograma,
                                   IdPGeneral,
                                   IdPEspecifico,
                                   IdPEspecificoSesion,
                                   IdEncuestaOnline,
                                   NombreEncuesta,
                                   EncuestaObligatoria,
                                   EncuestaActiva,
                                   AsignadoPara 
                            FROM [pla].[V_EncuestaSesionAsignada] WHERE IdPEspecificoSesion=@idPespecificoSesion";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecificoSesion = idPespecificoSesion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EncuestaSesionAsignadaDTO>>(resultado);
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
