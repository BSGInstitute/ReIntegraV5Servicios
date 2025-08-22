using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioSolicitudTextoBotonRepository
    /// Autor: Margiory ramirez Neyra .
    /// Fecha: 13/09/2022
    /// <summary>
    /// Gestión general de T_FormularioSolicitudTextoBoton
    /// </summary>
    public class FormularioSolicitudTextoBotonRepository : GenericRepository<TFormularioSolicitudTextoBoton>, IFormularioSolicitudTextoBotonRepository
    {
        private Mapper _mapper;

        public FormularioSolicitudTextoBotonRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioSolicitudTextoBoton, FormularioSolicitudTextoBoton>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFormularioSolicitudTextoBoton MapeoEntidad(FormularioSolicitudTextoBoton entidad)
        {
            try
            {
                //crea la entidad padre
                TFormularioSolicitudTextoBoton modelo = _mapper.Map<TFormularioSolicitudTextoBoton>(entidad);

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

        public TFormularioSolicitudTextoBoton Add(FormularioSolicitudTextoBoton entidad)
        {
            try
            {
                var FormularioSolicitudTextoBoton = MapeoEntidad(entidad);
                base.Insert(FormularioSolicitudTextoBoton);
                return FormularioSolicitudTextoBoton;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFormularioSolicitudTextoBoton Update(FormularioSolicitudTextoBoton entidad)
        {
            try
            {
                var FormularioSolicitudTextoBoton = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FormularioSolicitudTextoBoton.RowVersion = entidadExistente.RowVersion;

                base.Update(FormularioSolicitudTextoBoton);
                return FormularioSolicitudTextoBoton;
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


        public IEnumerable<TFormularioSolicitudTextoBoton> Add(IEnumerable<FormularioSolicitudTextoBoton> listadoEntidad)
        {
            try
            {
                List<TFormularioSolicitudTextoBoton> listado = new List<TFormularioSolicitudTextoBoton>();
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

        public IEnumerable<TFormularioSolicitudTextoBoton> Update(IEnumerable<FormularioSolicitudTextoBoton> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFormularioSolicitudTextoBoton> listado = new List<TFormularioSolicitudTextoBoton>();
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
        /// Autor: Margiory Ramirez Neyra .
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormularioSolicitudTextoBoton para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM  mkt.T_FormularioSolicitudTextoBoton WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra .
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre   Texto Boton  para los registros  de T_FormularioSolicitudTextoBoton 
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public IEnumerable<FormularioSolicitudTextoBotonFiltroDTO> ObtenerFiltroFormularioSolicitudTextoBoton()
        {
            try
            {
                List<FormularioSolicitudTextoBotonFiltroDTO> rpta = new List<FormularioSolicitudTextoBotonFiltroDTO>();
                var query = @"SELECT Id,TextoBoton From   mkt.T_FormularioSolicitudTextoBoton
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioSolicitudTextoBotonFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// Autor: Margiory Ramirez Neyra .
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  todos los registros de T_FormularioSolicitudTextoBoton .
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public IEnumerable<FormularioSolicitudTextoBotonDTO> ObtenerFormularioSolicitudTextoBoton()
        {
            try
            {
                List<FormularioSolicitudTextoBotonDTO> rpta = new List<FormularioSolicitudTextoBotonDTO>();
                var query = @"SELECT Id, TextoBoton, Descripcion, PorDefecto, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM  mkt.T_FormularioSolicitudTextoBoton
                            WHERE Estado=1 order by FechaModificacion desc ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioSolicitudTextoBotonDTO>>(resultado);
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
