using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GmailClienteRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GmailCliente
    /// </summary>
    public class GmailClienteRepository : GenericRepository<TGmailCliente>, IGmailClienteRepository
    {
        private Mapper _mapper;

        public GmailClienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGmailCliente, GmailCliente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGmailCliente MapeoEntidad(GmailCliente entidad)
        {
            try
            {
                //crea la entidad padre
                TGmailCliente modelo = _mapper.Map<TGmailCliente>(entidad);

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

        public TGmailCliente Add(GmailCliente entidad)
        {
            try
            {
                var GmailCliente = MapeoEntidad(entidad);
                base.Insert(GmailCliente);
                return GmailCliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGmailCliente Update(GmailCliente entidad)
        {
            try
            {
                var GmailCliente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GmailCliente.RowVersion = entidadExistente.RowVersion;

                base.Update(GmailCliente);
                return GmailCliente;
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


        public IEnumerable<TGmailCliente> Add(IEnumerable<GmailCliente> listadoEntidad)
        {
            try
            {
                List<TGmailCliente> listado = new List<TGmailCliente>();
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

        public IEnumerable<TGmailCliente> Update(IEnumerable<GmailCliente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGmailCliente> listado = new List<TGmailCliente>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GmailCliente.
        /// </summary>
        /// <returns> List<GmailClienteDTO> </returns>
        public IEnumerable<GmailClienteDTO> ObtenerGmailCliente()
        {
            try
            {
                List<GmailClienteDTO> rpta = new List<GmailClienteDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdAsesor,
	                    EmailAsesor,
	                    PasswordCorreo,
	                    NombreAsesor,
	                    IdClient,
	                    ClientSecret,
	                    AliasEmailAsesor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_GmailCliente
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GmailClienteDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GmailCliente para mostrarse en combo.
        /// </summary>
        /// <returns> List<GmailClienteComboDTO> </returns>
        public IEnumerable<GmailClienteComboDTO> ObtenerCombo()
        {
            try
            {
                List<GmailClienteComboDTO> rpta = new List<GmailClienteComboDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NombreAsesor
                    FROM mkt.T_GmailCliente
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GmailClienteComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las credenciales del Asesor, para conectarse al Servicio Imap.
        /// </summary>
        /// <param name="idAsesor">Id del Asesor</param>
        /// <returns> List<CorreoClienteCredencialDTO> </returns>
        public CorreoClienteCredencialDTO? ObtenerClienteCredencial(int idAsesor)
        {
            try
            {
                var query = @"SELECT IdAsesor, EmailAsesor, PasswordCorreo FROM mkt.V_TGmailCliente_CredencialAsesor Where Estado=1 AND IdAsesor=@IdAsesor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<CorreoClienteCredencialDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GmailCliente ObtenerPorIdAsesor(int idAsesor)
        {
            try
            {
                var query = @"SELECT AliasEmailAsesor,
                               ClientSecret,
                               EmailAsesor,
                               Estado,
                               FechaCreacion,
                               FechaModificacion,
                               Id,
                               IdAsesor,
                               IdClient,
                               IdMigracion,
                               NombreAsesor,
                               PasswordCorreo,
                               RowVersion,
                               UsuarioCreacion,
                               UsuarioModificacion FROM mkt.T_GmailCliente WHERE IdAsesor=@idAsesor";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idAsesor });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<GmailCliente>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TGmailCliente> ObtenerGmailClientePorAreaTrabajo(int idPersonalAreaTrabajo)
        {
            try
            {
                var query = @"
                    SELECT gc.*
                    FROM mkt.T_GmailCliente gc
                    INNER JOIN gp.T_Personal p ON gc.IdAsesor = p.Id AND p.Estado = 1
                    WHERE gc.Estado = 1
                      AND p.IdPersonalAreaTrabajo = @IdPersonalAreaTrabajo
                      AND p.Activo = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPersonalAreaTrabajo = idPersonalAreaTrabajo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<TGmailCliente>>(resultado)!;
                }
                return new List<TGmailCliente>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
