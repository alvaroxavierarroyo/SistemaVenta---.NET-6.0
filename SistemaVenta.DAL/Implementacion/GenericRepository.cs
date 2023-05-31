using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.DbVentaContext;
using SistemaVenta.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Implementacion
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Dbventa2Context _dbventa2Context;

        public GenericRepository(Dbventa2Context dbventa2Context)
        {
            _dbventa2Context = dbventa2Context;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entidad = await _dbventa2Context.Set<TEntity>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TEntity> Crear(TEntity entidad)
        {
            try
            {
                _dbventa2Context.Set<TEntity>().Add(entidad);
                await _dbventa2Context.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(TEntity entidad)
        {
            try
            {
                _dbventa2Context.Update(entidad);
                await _dbventa2Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entidad)
        {
            try
            {
                _dbventa2Context.Remove(entidad);
                await _dbventa2Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
           IQueryable<TEntity> queryEntidad = filtro == null ? _dbventa2Context.Set<TEntity>() : _dbventa2Context.Set<TEntity>().Where(filtro);
            return queryEntidad;
        }
    }
}
