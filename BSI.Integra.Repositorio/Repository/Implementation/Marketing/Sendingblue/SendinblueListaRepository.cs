using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Sendingblue
{
    public class SendinblueListaRepository : GenericRepository<TSendinblueListum>, ISendinblueListaRepository
    {
        private Mapper _mapper;
        public SendinblueListaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSendinblueListum, CrearSendingblueListaDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSendinblueListum MapeoEntidad(CrearSendingblueListaDTO entidad)
        {
            try
            {
                TSendinblueListum modelo = _mapper.Map<TSendinblueListum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueListum Add(CrearSendingblueListaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                base.Insert(LandingPage);
                return LandingPage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSendinblueListum Update(CrearSendingblueListaDTO entidad)
        {
            try
            {
                var LandingPage = MapeoEntidad(entidad);
                int idenntidad = Convert.ToInt32(entidad.Id);
                var entidadExistente = base.FirstBy(w => w.Id == idenntidad, s => new { s.RowVersion });
                LandingPage.RowVersion = entidadExistente.RowVersion;

                base.Update(LandingPage);
                return LandingPage;
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


        public IEnumerable<TSendinblueListum> Add(IEnumerable<CrearSendingblueListaDTO> listadoEntidad)
        {
            try
            {
                List<TSendinblueListum> listado = new List<TSendinblueListum>();
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

        public IEnumerable<TSendinblueListum> Update(IEnumerable<CrearSendingblueListaDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSendinblueListum> listado = new List<TSendinblueListum>();
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
        public IEnumerable<TSendinblueListum> ObtenerTodaslasLista()
        {
            try
            {
                var rpta = base.GetBy(x => x.Estado == true);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueListum ObtenerListaPorId(int id)
        {
            try
            {
                var rpta = base.FirstBy(x => x.Id == id);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueListum ObtenerListaPorIdSendinBlue(int id)
        {
            try
            {
                var rpta = base.FirstBy(x => x.IdSendinblueLista == id);
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSendinblueListum UpdateObjetoTabla(TSendinblueListum entidad)
        {
            try
            {
                base.Update(entidad);
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los alumnos para subir lista a sendinblue
        /// </summary>
        /// <param Entidad="lista">ListaIdsDtos</param>
        /// <returns> List<AlumnoDTO> </returns>

        public List<AlumnoDTO> ObtenerAlumnosParaSubirALista(ListaIdsDtos lista)
        {
            try
            {
                var ListaEnvio = lista.listIds;
                List<AlumnoDTO> listaAlumnos = new List<AlumnoDTO>();
                var query = string.Empty;
                query = "EXEC mkt.Sp_BuscarAlumnosPorId @ListaEnvio";
                var resultado = _dapperRepository.QueryDapper(query, new { ListaEnvio });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<AlumnoDTO>>(resultado);
                    return listaAlumnos;
                }
                else
                {
                    throw new Exception("Error");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool agregarListaAlumnos(agregarListaContactosDTO lista)
        {
            try
            {
                var listaEmails = lista.listaEmails;
                var nuevoIdLista = lista.nuevoIdLista;
                string _queryInsertar = "mkt.SP_AgregarListaContactosSendinblue";

                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    listaEmails = listaEmails,
                    nuevoIdLista = nuevoIdLista
                });

                if (queryInsert != null && queryInsert.Any())
                {
                    return true;
                }
                else
                {
                    throw new Exception("Error al agregar la lista de alumnos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método AgregarListaAlumnos.", ex);
            }
        }


    }
}
