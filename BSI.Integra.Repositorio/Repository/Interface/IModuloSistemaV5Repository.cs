using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModuloSistemaV5Repository
    {
        #region Metodos Base
        public TModuloSistemaV5 Add(ModuloSistemaV5DTO entidad);
        public TModuloSistemaV5 Update(ModuloSistemaV5DTO entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TModuloSistemaV5> Add(IEnumerable<ModuloSistemaV5DTO> listadoEntidad);

        public IEnumerable<TModuloSistemaV5> Update(IEnumerable<ModuloSistemaV5DTO> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public List<ComboDTO> ObtenerCombo();
        IEnumerable<ModuloSistemaDTO> ObtenerListaModulos(int idUsuario);
        IEnumerable<ModuloSistemaDTO> ObtenerMisModulos(int idUsuario);
        bool AsignarModulo(AsignarModuloV5DTO dto, string usuario);
        bool DesasignarModulo(AsignarModuloV5DTO dto, string personal);
        public ModuloUrlDTO ObtenerNombreUrlModulos(string segmentoModulo);
        List<ModuloSistemaModuloGrupoDTO> ObtenerModulosGrupoModulo();
    }
}
