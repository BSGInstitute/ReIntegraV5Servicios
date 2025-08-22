using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
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
    public class TipoPagoRemuneracionRepository : GenericRepository<TTipoPagoRemuneracion>, ITipoPagoRemuneracionRepository
    {
        private Mapper _mapper;

        public TipoPagoRemuneracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoPagoRemuneracion, TipoPagoRemuneracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoPagoRemuneracion, TipoPagoRemuneracionDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoPagoRemuneracion MapeoEntidad(TipoPagoRemuneracion entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoPagoRemuneracion modelo = _mapper.Map<TTipoPagoRemuneracion>(entidad);

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

        public TTipoPagoRemuneracion Add(TipoPagoRemuneracion entidad)
        {
            try
            {
                var TipoPagoRemuneracion = MapeoEntidad(entidad);
                base.Insert(TipoPagoRemuneracion);
                return TipoPagoRemuneracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoPagoRemuneracion Update(TipoPagoRemuneracion entidad)
        {
            try
            {
                var TipoPagoRemuneracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoPagoRemuneracion.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoPagoRemuneracion);
                return TipoPagoRemuneracion;
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


        public IEnumerable<TTipoPagoRemuneracion> Add(IEnumerable<TipoPagoRemuneracion> listadoEntidad)
        {
            try
            {
                List<TTipoPagoRemuneracion> listado = new List<TTipoPagoRemuneracion>();
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

        public IEnumerable<TTipoPagoRemuneracion> Update(IEnumerable<TipoPagoRemuneracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoPagoRemuneracion> listado = new List<TTipoPagoRemuneracion>();
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

        public IEnumerable<TipoPagoRemuneracionDTO> Obtener()
        {
            try
            {
                List<TipoPagoRemuneracionDTO> rpta = new List<TipoPagoRemuneracionDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_TipoPagoRemuneracion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoPagoRemuneracionDTO>>(resultado);

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
