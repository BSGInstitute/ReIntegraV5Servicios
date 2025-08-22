using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ProcesoSeleccionRangoRepository : GenericRepository<TProcesoSeleccionRango>, IProcesoSeleccionRangoRepository
    {
        private Mapper _mapper;

        public ProcesoSeleccionRangoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoSeleccionRango, ProcesoSeleccionRango>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProcesoSeleccionRango MapeoEntidad(ProcesoSeleccionRango entidad)
        {
            try
            {
                //crea la entidad padre
                TProcesoSeleccionRango modelo = _mapper.Map<TProcesoSeleccionRango>(entidad);

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

        public TProcesoSeleccionRango Add(ProcesoSeleccionRango entidad)
        {
            try
            {
                var ProcesoSeleccionRango = MapeoEntidad(entidad);
                base.Insert(ProcesoSeleccionRango);
                return ProcesoSeleccionRango;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcesoSeleccionRango Update(ProcesoSeleccionRango entidad)
        {
            try
            {
                var ProcesoSeleccionRango = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcesoSeleccionRango.RowVersion = entidadExistente.RowVersion;

                base.Update(ProcesoSeleccionRango);
                return ProcesoSeleccionRango;
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


        public IEnumerable<TProcesoSeleccionRango> Add(IEnumerable<ProcesoSeleccionRango> listadoEntidad)
        {
            try
            {
                List<TProcesoSeleccionRango> listado = new List<TProcesoSeleccionRango>();
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

        public IEnumerable<TProcesoSeleccionRango> Update(IEnumerable<ProcesoSeleccionRango> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcesoSeleccionRango> listado = new List<TProcesoSeleccionRango>();
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


        public IEnumerable<ProcesoSeleccionRangoDTO> Obtener()
        {
            try
            {
                List<ProcesoSeleccionRangoDTO> rpta = new List<ProcesoSeleccionRangoDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_ProcesoSeleccionRango
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcesoSeleccionRangoDTO>>(resultado);

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
