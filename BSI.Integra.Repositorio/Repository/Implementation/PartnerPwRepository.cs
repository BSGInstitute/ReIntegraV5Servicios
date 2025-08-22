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
    /// Repositorio: PartnerPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_PartnerPw
    /// </summary>
    public class PartnerPwRepository : GenericRepository<TPartnerPw>, IPartnerPwRepository
    {
        private Mapper _mapper;

        public PartnerPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPartnerPw, PartnerPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPartnerPw MapeoEntidad(PartnerPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPartnerPw modelo = _mapper.Map<TPartnerPw>(entidad);

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

        public TPartnerPw Add(PartnerPw entidad)
        {
            try
            {
                var PartnerPw = MapeoEntidad(entidad);
                base.Insert(PartnerPw);
                return PartnerPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPartnerPw Update(PartnerPw entidad)
        {
            try
            {
                var PartnerPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PartnerPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PartnerPw);
                return PartnerPw;
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


        public IEnumerable<TPartnerPw> Add(IEnumerable<PartnerPw> listadoEntidad)
        {
            try
            {
                List<TPartnerPw> listado = new List<TPartnerPw>();
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

        public IEnumerable<TPartnerPw> Update(IEnumerable<PartnerPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPartnerPw> listado = new List<TPartnerPw>();
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
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PartnerPw.
        /// </summary>
        /// <returns> List<PartnerPwDTO> </returns>
        public IEnumerable<PartnerPwDTO> Obtener()
        {
            try
            {
                List<PartnerPwDTO> rpta = new List<PartnerPwDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,
                        ImgPrincipal,
                        ImgPrincipalAlf,
                        ImgSecundaria,
                        ImgSecundariaAlf,
                        Descripcion,
                        DescripcionCorta,
                        Preguntas,
                        Posicion,
                        IdPartner
                        EncabezadoCorreoPartner
                    FROM pla.T_Partner_PW
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PartnerPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PartnerPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Partner_PW WHERE Estado = 1";
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
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Partner_PW WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_PartnerPw asociado a un identificador.
        /// </summary>
        /// <param name="idPartner">Id de Partner</param>
        /// <returns> PartnerPwDTO </returns>
        public PartnerPw? ObtenerPorId(int idPartner)
        {
            try
            {
                PartnerPw rpta = new();
                var query = @"
                    SELECT
	                    Id,
						Nombre,
						ImgPrincipal,
						ImgPrincipalAlf,
						ImgSecundaria,
						ImgSecundariaAlf,
						Descripcion,
						DescripcionCorta,
						Preguntas,
						Posicion,
						IdPartner,
						EncabezadoCorreoPartner,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion
                    FROM pla.T_Partner_PW
                    WHERE Estado = 1 AND Id = @idPartner";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPartner });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PartnerPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }

        /// <summary>
        /// Obtiene el partner  Anterior por Id Actual Partner
        /// </summary>
        /// <returns></returns>
        public int ObtenerPartnerAnterior(int idPartner)
        {
            try
            {
                int partenerAnterior = 0;
                string _queryPartner = "SELECT IdActualPartner,IdAnteriorPartner as Valor FROM pla.V_ObtenerPartnerAnterior WHERE IdActualPartner= @idPartner";
                var queryPartner = _dapperRepository.FirstOrDefault(_queryPartner, new { idPartner });
                if (!queryPartner.Equals("null"))
                {
                    partenerAnterior = JsonConvert.DeserializeObject<ValorDTO>(queryPartner).Valor;
                }
                return partenerAnterior;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
