using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class AuthRepository : IAuthRepository
{
    private DataContext _context;

    public AuthRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> Register(User user, string password)
    {
        byte[] passwordsalt, passwordhash;
        CreatePasswordHash(password, out passwordhash, out passwordsalt);

        user.PasswordHash = passwordhash;
        user.PasswordSalt = passwordsalt;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passwordsalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordsalt = hmac.Key;
            passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public async Task<User> Login(string userName, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null)
        {
            return null;
        }

        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return null;
        }

        return user;
    }

    private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordHash))
        {
            var computehash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computehash.Length; i++)
            {
                if (computehash[i] != userPasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public async Task<bool> UserExist(string userName)
    {
        if (await _context.Users.AnyAsync(x => x.UserName == userName))
        {
            return true;
        }
        return false;
    }
}    
