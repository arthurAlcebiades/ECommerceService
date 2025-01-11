using System.Data;
using Dapper;
using IdentityService.Model.Entities;
using IdentityService.Repository.Interface;

namespace IdentityService.Repository.Implementation;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _connection;

    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<User> GetUserByEmail(string email)
    {
        var sql = @"
            SELECT 
                id as Id, 
                email as Email, 
                password_hash as PasswordHash, 
                username as Username 
            FROM 
                users 
            WHERE 
                email = @Email";

        var args = new DynamicParameters();
        args.Add("@Email", email);

        var user = _connection.Query<User>(sql, args).FirstOrDefault();
        if (user is null)
            throw new Exception("Não foi possível encontrar nenhum usuário cadastrado.");

        return user;
    }

    public async Task<bool> RegisterUser(User user)
    {
        int rowsAffected;
        var sql = @"
            INSERT INTO users (username, password_hash, email, phone_number, cpf, cnpj, created_at, is_active) 
            VALUES(@Name, @Psswrd, @Email, @Phone, @Cpf, @Cnpj, @CreatedAt, @IsActive)
        ";
        
        var args = new DynamicParameters();
        args.Add("@Name", user.Username);
        args.Add("@Psswrd", user.PasswordHash);
        args.Add("@Email", user.Email);
        args.Add("@Phone", user.PhoneNumber);
        args.Add("@Cpf", user.Cpf);
        args.Add("@Cnpj", user.Cnpj);
        args.Add("@CreatedAt", user.CreeatedAt);
        args.Add("@IsActive", user.IsActive);

        try
        {
            rowsAffected = await _connection.ExecuteAsync(sql, args);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro na tentativa de registrar novo usuário | Detalhes: {e}");
        }

        return rowsAffected > 0;
    }

    public async Task<bool> VerifyUserExist(string email)
    {
        int countSql;
        var sql = @"
            SELECT 1
                FROM users
            WHERE email = @Email";

        var args = new DynamicParameters();
        args.Add("@Email", email);

        try
        {
            countSql = _connection.Query<int>(sql, args).FirstOrDefault();
        }
        catch (Exception e)
        {
            throw new Exception($"Erro na tentativa de verificar existência do usuário | Detalhes: {e}");
        }

        return countSql > 0;
    }
}