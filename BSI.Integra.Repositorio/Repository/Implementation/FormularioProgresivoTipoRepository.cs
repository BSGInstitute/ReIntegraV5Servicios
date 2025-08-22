using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoRepository
    /// Autor: Jorge Gamero
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoTipo
    /// </summary>
    public class FormularioProgresivoTipoRepository : GenericRepository<TFormularioProgresivoTipo>, IFormularioProgresivoTipoRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivoTipo, FormularioProgresivoTipo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFormularioProgresivoTipo MapeoEntidad(FormularioProgresivoTipo entidad)
        {
            try
            {
                TFormularioProgresivoTipo modelo = _mapper.Map<TFormularioProgresivoTipo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFormularioProgresivoTipo> Add(FormularioProgresivoTipo entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoTipo_Insertar";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioCreacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoTipo>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TFormularioProgresivoTipo> Update(FormularioProgresivoTipo entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoTipo_Actualizar";
                var queryUpdate = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioModificacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoTipo>>(queryUpdate)!;
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
        /// Fecha: 29/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoTipo por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoTipo </returns>
        public FormularioProgresivoTipo ObtenerPorId(int id)
        {
            try
            {
                var rpta = new FormularioProgresivoTipo();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivoTipo WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FormularioProgresivoTipo>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoTipo> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<FormularioProgresivoTipo>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.T_FormularioProgresivoTipo WHERE Estado = 1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoTipo>>(resultado);
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
