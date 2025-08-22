using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ModuloSistemaPuestoTrabajoRepository : GenericRepository<TModuloSistemaPuestoTrabajo>, IModuloSistemaPuestoTrabajoRepository
    {
        private Mapper _mapper;

        public ModuloSistemaPuestoTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModuloSistemaPuestoTrabajo, ModuloSistemaPuestoTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TModuloSistemaPuestoTrabajo MapeoEntidad(ModuloSistemaPuestoTrabajo entidad)
        {
            try
            {
                //crea la entidad padre
                TModuloSistemaPuestoTrabajo modelo = _mapper.Map<TModuloSistemaPuestoTrabajo>(entidad);

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

        public TModuloSistemaPuestoTrabajo Add(ModuloSistemaPuestoTrabajo entidad)
        {
            try
            {
                var ModuloSistemaPuestoTrabajo = MapeoEntidad(entidad);
                base.Insert(ModuloSistemaPuestoTrabajo);
                return ModuloSistemaPuestoTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModuloSistemaPuestoTrabajo Update(ModuloSistemaPuestoTrabajo entidad)
        {
            try
            {
                var ModuloSistemaPuestoTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ModuloSistemaPuestoTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(ModuloSistemaPuestoTrabajo);
                return ModuloSistemaPuestoTrabajo;
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


        public IEnumerable<TModuloSistemaPuestoTrabajo> Add(IEnumerable<ModuloSistemaPuestoTrabajo> listadoEntidad)
        {
            try
            {
                List<TModuloSistemaPuestoTrabajo> listado = new List<TModuloSistemaPuestoTrabajo>();
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

        public IEnumerable<TModuloSistemaPuestoTrabajo> Update(IEnumerable<ModuloSistemaPuestoTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModuloSistemaPuestoTrabajo> listado = new List<TModuloSistemaPuestoTrabajo>();
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

    }
}
