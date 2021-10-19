using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Context.Infrastructure;
using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Context
{
    public class UserService : IUserService
    {
        public UserService(){ }
        public UserService(RestApiContext dbContext, ILogger<UserService> logger)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private readonly RestApiContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public virtual async Task<IEnumerable<User>> GetUserAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._dbContext.Users.ToListAsync(cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error,ex, $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}");
                return null;
            }
        }

        public virtual async Task<User> GetUserAsync(int id,CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}");
                return null;
            }
        }

        public virtual async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (user != null)
                {
                    _dbContext.Users.Remove(user.Result);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> AddUserAsync(CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Users.Add(new User() { Age = new Random().Next(0, 50), CurrentTime = DateTime.Now, Name = "user", Surname = "user" });
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}");
                return false;
            }
        }
    }
}
