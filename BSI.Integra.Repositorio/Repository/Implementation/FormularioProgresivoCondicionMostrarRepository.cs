using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoCondicionMostrarRepository
    /// Autor: Jorge Gamero
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoCondicionMostrar
    /// </summary>
    public class FormularioProgresivoCondicionMostrarRepository : GenericRepository<TFormularioProgresivoCondicionMostrar>, IFormularioProgresivoCondicionMostrarRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoCondicionMostrarRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivoCondicionMostrar, FormularioProgresivoCondicionMostrar>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFormularioProgresivoCondicionMostrar MapeoEntidad(FormularioProgresivoCondicionMostrar entidad)
        {
            try
            {
                TFormularioProgresivoCondicionMostrar modelo = _mapper.Map<TFormularioProgresivoCondicionMostrar>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFormularioProgresivoCondicionMostrar> Add(FormularioProgresivoCondicionMostrar entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoCondicionMostrar_Insertar";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioCreacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoCondicionMostrar>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TFormularioProgresivoCondicionMostrar> Update(FormularioProgresivoCondicionMostrar entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivoCondicionMostrar_Actualizar";
                var queryUpdate = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Usuario = entidad.UsuarioModificacion
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivoCondicionMostrar>>(queryUpdate)!;
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
        /// Obtiene todos los campos de T_FormularioProgresivoCondicionMostrar por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoCondicionMostrar </returns>
        public FormularioProgresivoCondicionMostrar ObtenerPorId(int id)
        {
            try
            {
                var rpta = new FormularioProgresivoCondicionMostrar();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivoCondicionMostrar WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FormularioProgresivoCondicionMostrar>(resultado);
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
        public IEnumerable<FormularioProgresivoCondicionMostrar> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<FormularioProgresivoCondicionMostrar>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.T_FormularioProgresivoCondicionMostrar WHERE Estado = 1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoCondicionMostrar>>(resultado);
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
