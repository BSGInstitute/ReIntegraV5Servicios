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
    public class EntidadSistemaPensionarioRepository : GenericRepository<TEntidadSistemaPensionario>, IEntidadSistemaPensionarioRepository
    {
        private Mapper _mapper;

        public EntidadSistemaPensionarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntidadSistemaPensionario, EntidadSistemaPensionario>(MemberList.None).ReverseMap();
                cfg.CreateMap<EntidadSistemaPensionario, EntidadSistemaPensionarioDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEntidadSistemaPensionario MapeoEntidad(EntidadSistemaPensionario entidad)
        {
            try
            {
                //crea la entidad padre
                TEntidadSistemaPensionario modelo = _mapper.Map<TEntidadSistemaPensionario>(entidad);

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

        public TEntidadSistemaPensionario Add(EntidadSistemaPensionario entidad)
        {
            try
            {
                var EntidadSistemaPensionario = MapeoEntidad(entidad);
                base.Insert(EntidadSistemaPensionario);
                return EntidadSistemaPensionario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntidadSistemaPensionario Update(EntidadSistemaPensionario entidad)
        {
            try
            {
                var EntidadSistemaPensionario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EntidadSistemaPensionario.RowVersion = entidadExistente.RowVersion;

                base.Update(EntidadSistemaPensionario);
                return EntidadSistemaPensionario;
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


        public IEnumerable<TEntidadSistemaPensionario> Add(IEnumerable<EntidadSistemaPensionario> listadoEntidad)
        {
            try
            {
                List<TEntidadSistemaPensionario> listado = new List<TEntidadSistemaPensionario>();
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

        public IEnumerable<TEntidadSistemaPensionario> Update(IEnumerable<EntidadSistemaPensionario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEntidadSistemaPensionario> listado = new List<TEntidadSistemaPensionario>();
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

        public IEnumerable<EntidadSistemaPensionarioDTO> Obtener()
        {
            try
            {
                List<EntidadSistemaPensionarioDTO> rpta = new List<EntidadSistemaPensionarioDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,IdSistemaPensionario
                    FROM gp.T_EntidadSistemaPensionario
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EntidadSistemaPensionarioDTO>>(resultado);

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
