using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Repositories
{
    public class PeticionRepository : IPeticionRepository
    {
        private readonly string _connection;
        protected NpgsqlConnection DbConnection() => new(_connection);
        public PeticionRepository(IConfiguration configuration) => _connection = configuration.GetConnectionString("postgres");
        public async Task<IEnumerable<Peticion>> GetAll()
        {
            var db = DbConnection();
            var sql = @"SELECT id, request, fecha_request, response, fecha_response, username 
                        FROM public.peticion";
            var result = await db.QueryAsync<Peticion>(sql);
            return result;
        }
        public async Task<bool> Add(Peticion entity)
        {
            var db = DbConnection();
            var sql = @"INSERT INTO public.peticion (request, fecha_request, response, fecha_response, username)
                        VALUES (@request, @fecha_request, @response, @fecha_response, @username)";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<Peticion> GetById(int id)
        {
            var db = DbConnection();
            var sql = @"SELECT id, request, fecha_request, response, fecha_response, username 
                        FROM public.peticion
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.QueryFirstOrDefaultAsync<Peticion>(sql, parameters);
            return result;
        }
        public async Task<bool> Update(Peticion entity)
        {
            var db = DbConnection();
            var sql = @"UPDATE public.peticion 
                        SET request=@request, fecha_request=@fecha_request, response=@response, fecha_response=@fecha_response, username=@username 
                        WHERE id=@id";
            var result = await db.ExecuteAsync(sql, entity);
            return result > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var db = DbConnection();
            var sql = @"DELETE FROM public.peticion 
                        WHERE id=@id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            var result = await db.ExecuteAsync(sql, parameters);
            return result > 0;
        }
    }
}
