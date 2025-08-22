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
    public class EntidadSeguroSaludRepository : GenericRepository<TEntidadSeguroSalud>, IEntidadSeguroSaludRepository
    {
        private Mapper _mapper;

        public EntidadSeguroSaludRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntidadSeguroSalud, EntidadSeguroSalud>(MemberList.None).ReverseMap();
                cfg.CreateMap<EntidadSeguroSalud, EntidadSeguroSaludDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEntidadSeguroSalud MapeoEntidad(EntidadSeguroSalud entidad)
        {
            try
            {
                //crea la entidad padre
                TEntidadSeguroSalud modelo = _mapper.Map<TEntidadSeguroSalud>(entidad);

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

        public TEntidadSeguroSalud Add(EntidadSeguroSalud entidad)
        {
            try
            {
                var EntidadSeguroSalud = MapeoEntidad(entidad);
                base.Insert(EntidadSeguroSalud);
                return EntidadSeguroSalud;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntidadSeguroSalud Update(EntidadSeguroSalud entidad)
        {
            try
            {
                var EntidadSeguroSalud = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EntidadSeguroSalud.RowVersion = entidadExistente.RowVersion;

                base.Update(EntidadSeguroSalud);
                return EntidadSeguroSalud;
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


        public IEnumerable<TEntidadSeguroSalud> Add(IEnumerable<EntidadSeguroSalud> listadoEntidad)
        {
            try
            {
                List<TEntidadSeguroSalud> listado = new List<TEntidadSeguroSalud>();
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

        public IEnumerable<TEntidadSeguroSalud> Update(IEnumerable<EntidadSeguroSalud> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEntidadSeguroSalud> listado = new List<TEntidadSeguroSalud>();
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

        public IEnumerable<EntidadSeguroSaludDTO> Obtener()
        {
            try
            {
                List<EntidadSeguroSaludDTO> rpta = new List<EntidadSeguroSaludDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_EntidadSeguroSalud
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EntidadSeguroSaludDTO>>(resultado);

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
