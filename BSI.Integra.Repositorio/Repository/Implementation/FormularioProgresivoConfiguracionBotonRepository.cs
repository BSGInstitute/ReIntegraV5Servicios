using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoConfiguracionBotonRepository
    /// Autor: Jorge Gamero
    /// Fecha: 03/03/2025
    /// <summary>
    /// Gestión general de T_FormularioProgresivoConfiguracionBoton
    /// </summary>
    public class FormularioProgresivoConfiguracionBotonRepository : GenericRepository<TFormularioProgresivoConfiguracionBoton>, IFormularioProgresivoConfiguracionBotonRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoConfiguracionBotonRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivoConfiguracionBoton, FormularioProgresivoConfiguracionBoton>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFormularioProgresivoConfiguracionBoton MapeoEntidad(FormularioProgresivoConfiguracionBoton entidad)
        {
            try
            {
                TFormularioProgresivoConfiguracionBoton modelo = _mapper.Map<TFormularioProgresivoConfiguracionBoton>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFormularioProgresivoConfiguracionBoton> Add(FormularioProgresivoConfiguracionBoton entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoConfiguracionBoton_Insertar";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    IdFormularioProgresivo = entidad.IdFormularioProgresivo,
                    IdFormularioProgresivoSeccionPortal = entidad.IdFormularioProgresivoSeccionPortal,
                    IdFormularioProgresivoAccionBoton = entidad.IdFormularioProgresivoAccionBoton,
                    IdRegistroArchivoStorage = entidad.IdRegistroArchivoStorage,
                    IdentificadorFilaGrilla = entidad.IdentificadorFilaGrilla,
                    TextoBoton = entidad.TextoBoton,
                    Usuario = entidad.UsuarioCreacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoConfiguracionBoton>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TFormularioProgresivoConfiguracionBoton> Update(FormularioProgresivoConfiguracionBoton entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoConfiguracionBoton_Actualizar";
                var queryUpdate = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = entidad.Id,
                    IdFormularioProgresivoSeccionPortal = entidad.IdFormularioProgresivoSeccionPortal,
                    IdFormularioProgresivoAccionBoton = entidad.IdFormularioProgresivoAccionBoton,
                    IdRegistroArchivoStorage = entidad.IdRegistroArchivoStorage,
                    TextoBoton = entidad.TextoBoton,
                    Usuario = entidad.UsuarioModificacion,
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoConfiguracionBoton>>(queryUpdate)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        #endregion

        /// Autor: Jorge Gamero
        /// Fecha: 03/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoConfiguracionBoton por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoConfiguracionBoton </returns>
        public FormularioProgresivoConfiguracionBoton ObtenerPorId(int id)
        {
            try
            {
                var rpta = new FormularioProgresivoConfiguracionBoton();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivoConfiguracionBoton WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FormularioProgresivoConfiguracionBoton>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 04/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoConfiguracionBoton por IdFormularioProgresivo.
        /// </summary>
        /// <param name="idFormularioProgresivo"> IdFormularioProgresivo de la entidad </param>
        /// <returns> FormularioProgresivoConfiguracionBoton </returns>
        public IEnumerable<FormularioProgresivoConfiguracionBoton> ObtenerPorIdFormularioProgresivo(int idFormularioProgresivo)
        {
            try
            {
                var rpta = new List<FormularioProgresivoConfiguracionBoton>();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivoConfiguracionBoton WHERE Estado = 1 AND IdFormularioProgresivo = @idFormularioProgresivo";
                var resultado = _dapperRepository.QueryDapper(query, new { IdFormularioProgresivo = idFormularioProgresivo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoConfiguracionBoton>>(resultado);
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
