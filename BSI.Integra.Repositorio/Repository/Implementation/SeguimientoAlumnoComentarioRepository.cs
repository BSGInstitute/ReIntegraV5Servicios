using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class SeguimientoAlumnoComentarioRepository : GenericRepository<TSeguimientoAlumnoComentario>, ISeguimientoAlumnoComentarioRepository
    {
        private Mapper _mapper;
        public SeguimientoAlumnoComentarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeguimientoAlumnoComentario, SeguimientoAlumnoComentario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TSeguimientoAlumnoComentario MapeoEntidad(SeguimientoAlumnoComentario entidad)
        {
            try
            {
                TSeguimientoAlumnoComentario modelo = _mapper.Map<TSeguimientoAlumnoComentario>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSeguimientoAlumnoComentario Add(SeguimientoAlumnoComentario entidad)
        {
            try
            {
                var SeguimientoAlumnoComentario = MapeoEntidad(entidad);
                base.Insert(SeguimientoAlumnoComentario);
                return SeguimientoAlumnoComentario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 23/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_Solicitud por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> Solicitud </returns>
        public TipoSeguimientoDTO ObtenerPorId(int id)
        {
            try
            {
                var rpta = new TipoSeguimientoDTO();
                var query = @"SELECT Id,
                               Nombre,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaModificacion  
                               FROM ope.V_TipoSeguimientoAlumnoCategoria WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoSeguimientoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public List<TipoSeguimientoDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<TipoSeguimientoDTO>();
                var query = @"SELECT Id,
                               Nombre,UsuarioCreacion,UsuarioModificacion,FechaModificacion  FROM ope.V_TipoSeguimientoAlumnoCategoria WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<TipoSeguimientoDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public TipoSeguimientoDTO Update(TipoSeguimientoDTO tipoSeguimientoEntrada)
        {
            try
            {
                var comboDTOs = new TipoSeguimientoDTO();
                var query = @"ope.SP_ActualizarTipoSeguimientoCategoria";
                var resultado = _dapperRepository.QuerySPDapper(query, new { Id = tipoSeguimientoEntrada.Id, Nombre = tipoSeguimientoEntrada.Nombre, UsuarioModificacion = tipoSeguimientoEntrada.UsuarioModificacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<TipoSeguimientoDTO>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo Registro en TipoSeguimientoCategoria
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public bool InsertarTipoSeguimiento(TipoSeguimientoEntradaDTO tipoSeguimientoEntrada)
        {
            try
            {
                var comboDTOs = new TipoSeguimientoDTO();
                var query = @"ope.SP_InsertarTipoSeguimientoCategoria";
                var resultado = _dapperRepository.QuerySPDapper(query, new { Nombre = tipoSeguimientoEntrada.Nombre, UsuarioCreacion = tipoSeguimientoEntrada.Usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return true;
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
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina logicamente un registro
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public bool EliminarTipoSeguimiento(int id, string usuario)
        {
            try
            {
                var query = @"ope.SP_EliminarTipoSeguimientoCategoria";
                var resultado = _dapperRepository.QuerySPDapper(query, new { Id = id, UsuarioModificacion = usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return true;
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
