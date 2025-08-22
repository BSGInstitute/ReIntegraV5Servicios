using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class TipoDocumentacionPersonalRepository : GenericRepository<TTipoDocumentacionPersonal>, ITipoDocumentacionPersonalRepository
    {
        private Mapper _mapper;
        public TipoDocumentacionPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentacionPersonal, TipoDocumentacionPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDocumentacionPersonal, TipoDocumentacionPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDocumentacionPersonal, TTipoDocumentacionPersonal>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoDocumentacionPersonal MapeoEntidad(TipoDocumentacionPersonal entidad)
        {
            try
            {
                TTipoDocumentacionPersonal modelo = _mapper.Map<TTipoDocumentacionPersonal>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentacionPersonal Add(TipoDocumentacionPersonal entidad)
        {
            try
            {
                var TipoDocumentacionPersonal = MapeoEntidad(entidad);
                base.Insert(TipoDocumentacionPersonal);
                return TipoDocumentacionPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentacionPersonal Update(TipoDocumentacionPersonal entidad)
        {
            try
            {
                var TipoDocumentacionPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDocumentacionPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDocumentacionPersonal);
                return TipoDocumentacionPersonal;
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
        public IEnumerable<TTipoDocumentacionPersonal> Add(IEnumerable<TipoDocumentacionPersonal> listadoEntidad)
        {
            try
            {
                List<TTipoDocumentacionPersonal> listado = new List<TTipoDocumentacionPersonal>();
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
        public IEnumerable<TTipoDocumentacionPersonal> Update(IEnumerable<TipoDocumentacionPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumentacionPersonal> listado = new List<TTipoDocumentacionPersonal>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_TipoDocumentacionPersonal por el Primary Key
        /// </summary>
        /// <returns>TipoDocumentacionPersonal o Nulo</returns>
        public TipoDocumentacionPersonal? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
		                Nombre,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_TipoDocumentacionPersonal
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoDocumentacionPersonal>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor: Eliot Arias Flores
        /// Fecha: 21/10/2024
        /// <summary>
        /// Obtiene la lista de nombres e ids para los combox
        /// </summary>
        /// <returns>TipoDocumentacionPersonal </returns>
        public List<FiltroCombosDTO> ObtenerIdYNombreParaCombo()
        {
            var lista = GetBy(x => true, y => new FiltroCombosDTO
            {
                Id = y.Id,
                Nombre = y.Nombre
            }).ToList();
            return lista;
        }
    }
}
