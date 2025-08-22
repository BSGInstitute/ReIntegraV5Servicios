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
    /// Repositorio: PuntoCorteRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/10/2022
    /// <summary>
    /// Gestión general de T_PuntoCorte
    /// </summary>
    public class PuntoCorteRepository : GenericRepository<TPuntoCorte>, IPuntoCorteRepository
    {
        private Mapper _mapper;

        public PuntoCorteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuntoCorte, PuntoCorte>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPuntoCorte MapeoEntidad(PuntoCorte entidad)
        {
            try
            {
                //crea la entidad padre
                TPuntoCorte modelo = _mapper.Map<TPuntoCorte>(entidad);

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

        public TPuntoCorte Add(PuntoCorte entidad)
        {
            try
            {
                var PuntoCorte = MapeoEntidad(entidad);
                base.Insert(PuntoCorte);
                return PuntoCorte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuntoCorte Update(PuntoCorte entidad)
        {
            try
            {
                var PuntoCorte = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuntoCorte.RowVersion = entidadExistente.RowVersion;

                base.Update(PuntoCorte);
                return PuntoCorte;
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


        public IEnumerable<TPuntoCorte> Add(IEnumerable<PuntoCorte> listadoEntidad)
        {
            try
            {
                List<TPuntoCorte> listado = new List<TPuntoCorte>();
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

        public IEnumerable<TPuntoCorte> Update(IEnumerable<PuntoCorte> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuntoCorte> listado = new List<TPuntoCorte>();
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

        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rtpa = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM pla.T_PuntoCorte WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rtpa = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rtpa;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}