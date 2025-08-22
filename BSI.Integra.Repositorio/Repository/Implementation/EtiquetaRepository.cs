using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EtiquetaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/08/2022
    /// <summary>
    /// Gestión general de T_Etiqueta
    /// </summary>
    public class EtiquetaRepository : GenericRepository<TEtiquetum>, IEtiquetaRepository
    {
        private Mapper _mapper;

        public EtiquetaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEtiquetum, Etiqueta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEtiquetum MapeoEntidad(Etiqueta entidad)
        {
            try
            {
                //crea la entidad padre
                TEtiquetum modelo = _mapper.Map<TEtiquetum>(entidad);

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

        public TEtiquetum Add(Etiqueta entidad)
        {
            try
            {
                var Etiqueta = MapeoEntidad(entidad);
                base.Insert(Etiqueta);
                return Etiqueta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEtiquetum Update(Etiqueta entidad)
        {
            try
            {
                var Etiqueta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Etiqueta.RowVersion = entidadExistente.RowVersion;

                base.Update(Etiqueta);
                return Etiqueta;
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


        public IEnumerable<TEtiquetum> Add(IEnumerable<Etiqueta> listadoEntidad)
        {
            try
            {
                List<TEtiquetum> listado = new List<TEtiquetum>();
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

        public IEnumerable<TEtiquetum> Update(IEnumerable<Etiqueta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEtiquetum> listado = new List<TEtiquetum>();
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
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Etiqueta.
        /// </summary>
        /// <returns> List<EtiquetaDTO> </returns>
        public IEnumerable<EtiquetaDTO> ObtenerEtiqueta()
        {
            try
            {
                List<EtiquetaDTO> rpta = new List<EtiquetaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    CampoDB,
	                    NodoPadre,
	                    IdNodoPadre,
	                    IdTipoEtiqueta,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_Etiqueta
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EtiquetaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Etiqueta para mostrarse en combo.
        /// </summary>
        /// <returns> List<EtiquetaComboDTO> </returns>
        public IEnumerable<EtiquetaComboDTO> ObtenerCombo()
        {
            try
            {
                List<EtiquetaComboDTO> rpta = new List<EtiquetaComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Etiqueta WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EtiquetaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener datos de Etiquetas asociados a un Nodo Padre especifico.
        /// </summary>
        /// <param name="idNodoPadre"> Id de Nodo Padre </param>
        /// <returns> List<EtiquetaDTO> </returns>
        public IEnumerable<Etiqueta> ObtenerPorIdNodoPadre(int idNodoPadre)
        {
            try
            {
                List<Etiqueta> rpta = new List<Etiqueta>();
                var query = @"
                    SELECT
	                    Id,
		                Nombre,
		                Descripcion,
		                CampoDB,
		                NodoPadre,
		                IdNodoPadre,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdTipoEtiqueta
                    FROM pla.T_Etiqueta
                    WHERE Estado = 1
                        AND IdNodoPadre = @idNodoPadre";
                var resultado = _dapperRepository.QueryDapper(query, new { idNodoPadre });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Etiqueta>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener datos de Etiquetas asociados a un Nodo Padre especifico.
        /// </summary>
        /// <param name="idNodoPadre"> Id de Nodo Padre </param>
        /// <returns> List<EtiquetaDTO> </returns>
        public async Task<IEnumerable<Etiqueta>> ObtenerPorIdNodoPadreAsync(int idNodoPadre)
        {
            try
            {
                List<Etiqueta> rpta = new List<Etiqueta>();
                var query = @"
                         SELECT
	                        Id,
		                    Nombre,
		                    Descripcion,
		                    CampoDB,
		                    NodoPadre,
		                    IdNodoPadre,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion,
		                    IdTipoEtiqueta
                        FROM pla.T_Etiqueta
                        WHERE Estado = 1
                            AND IdNodoPadre = @idNodoPadre";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { idNodoPadre });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Etiqueta>>(resultado);
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
