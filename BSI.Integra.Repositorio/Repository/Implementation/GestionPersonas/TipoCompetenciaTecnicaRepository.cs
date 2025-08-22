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
    public class TipoCompetenciaTecnicaRepository : GenericRepository<TTipoCompetenciaTecnica>, ITipoCompetenciaTecnicaRepository
    {
        private Mapper _mapper;

        public TipoCompetenciaTecnicaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCompetenciaTecnica, TipoCompetenciaTecnica>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoCompetenciaTecnica, TipoCompetenciaTecnicaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoCompetenciaTecnica, TTipoCompetenciaTecnica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCompetenciaTecnica MapeoEntidad(TipoCompetenciaTecnica entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCompetenciaTecnica modelo = _mapper.Map<TTipoCompetenciaTecnica>(entidad);

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

        public TTipoCompetenciaTecnica Add(TipoCompetenciaTecnica entidad)
        {
            try
            {
                var TipoCompetenciaTecnica = MapeoEntidad(entidad);
                base.Insert(TipoCompetenciaTecnica);
                return TipoCompetenciaTecnica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCompetenciaTecnica Update(TipoCompetenciaTecnica entidad)
        {
            try
            {
                var TipoCompetenciaTecnica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCompetenciaTecnica.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCompetenciaTecnica);
                return TipoCompetenciaTecnica;
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


        public IEnumerable<TTipoCompetenciaTecnica> Add(IEnumerable<TipoCompetenciaTecnica> listadoEntidad)
        {
            try
            {
                List<TTipoCompetenciaTecnica> listado = new List<TTipoCompetenciaTecnica>();
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

        public IEnumerable<TTipoCompetenciaTecnica> Update(IEnumerable<TipoCompetenciaTecnica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCompetenciaTecnica> listado = new List<TTipoCompetenciaTecnica>();
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
        public IEnumerable<TipoCursoComplementarioDTO> ObtenerCombos()
        {
            try
            {
                List<TipoCursoComplementarioDTO> rpta = new List<TipoCursoComplementarioDTO>();
                var query = @"
                    SELECT
	                    Id as IdTipoCursoComplementario,Nombre
                    FROM gp.T_TipoCompetenciaTecnica
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCursoComplementarioDTO>>(resultado);

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
