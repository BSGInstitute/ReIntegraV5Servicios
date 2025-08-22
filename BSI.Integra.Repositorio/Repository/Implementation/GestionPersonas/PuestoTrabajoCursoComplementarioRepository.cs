using AutoMapper;
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
    public class PuestoTrabajoCursoComplementarioRepository : GenericRepository<TPuestoTrabajoCursoComplementario>, IPuestoTrabajoCursoComplementarioRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoCursoComplementarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoCursoComplementario, PuestoTrabajoCursoComplementario>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoCursoComplementario MapeoEntidad(PuestoTrabajoCursoComplementario entidad)
        {
            try
            {
                TPuestoTrabajoCursoComplementario modelo = _mapper.Map<TPuestoTrabajoCursoComplementario>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoCursoComplementario Add(PuestoTrabajoCursoComplementario entidad)
        {
            try
            {
                var PuestoTrabajoCursoComplementario = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoCursoComplementario);
                return PuestoTrabajoCursoComplementario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoCursoComplementario Update(PuestoTrabajoCursoComplementario entidad)
        {
            try
            {
                var PuestoTrabajoCursoComplementario = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoCursoComplementario.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoCursoComplementario);
                return PuestoTrabajoCursoComplementario;
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
        public IEnumerable<TPuestoTrabajoCursoComplementario> Add(IEnumerable<PuestoTrabajoCursoComplementario> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoCursoComplementario> listado = new List<TPuestoTrabajoCursoComplementario>();
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
        public IEnumerable<TPuestoTrabajoCursoComplementario> Update(IEnumerable<PuestoTrabajoCursoComplementario> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoCursoComplementario> listado = new List<TPuestoTrabajoCursoComplementario>();
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


        public List<PuestoTrabajoCursoComplementarioDTO> ObtenerPuestoTrabajoCursoComplementario(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoCursoComplementarioDTO> lista = new List<PuestoTrabajoCursoComplementarioDTO>();
                var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdTipoCompetenciaTecnica, IdCompetenciaTecnica, IdNivelCompetenciaTecnica, TipoCompetenciaTecnica, CompetenciaTecnica, NivelCompetenciaTecnica FROM [gp].[V_TPuestoTrabajoCursoComplementario_ObtenerListaCursosComplementarios] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoCursoComplementarioDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
