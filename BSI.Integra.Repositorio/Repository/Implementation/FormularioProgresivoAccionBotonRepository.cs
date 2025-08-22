using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoAccionBotonRepository
    /// Autor: Jorge Gamero
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoAccionBoton
    /// </summary>
    public class FormularioProgresivoAccionBotonRepository : GenericRepository<TFormularioProgresivoAccionBoton>, IFormularioProgresivoAccionBotonRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoAccionBotonRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivoAccionBoton, FormularioProgresivoAccionBoton>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFormularioProgresivoAccionBoton MapeoEntidad(FormularioProgresivoAccionBoton entidad)
        {
            try
            {
                TFormularioProgresivoAccionBoton modelo = _mapper.Map<TFormularioProgresivoAccionBoton>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFormularioProgresivoAccionBoton> Add(FormularioProgresivoAccionBoton entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoAccionBoton_Insertar";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioCreacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoAccionBoton>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TFormularioProgresivoAccionBoton> Update(FormularioProgresivoAccionBoton entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoAccionBoton_Actualizar";
                var queryUpdate = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioModificacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoAccionBoton>>(queryUpdate)!;
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
        /// Obtiene todos los campos de T_FormularioProgresivoAccionBoton por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoAccionBoton </returns>
        public FormularioProgresivoAccionBoton ObtenerPorId(int id)
        {
            try
            {
                var rpta = new FormularioProgresivoAccionBoton();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivoAccionBoton WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FormularioProgresivoAccionBoton>(resultado);
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
        public IEnumerable<FormularioProgresivoAccionBoton> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<FormularioProgresivoAccionBoton>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.T_FormularioProgresivoAccionBoton WHERE Estado = 1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoAccionBoton>>(resultado);
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
