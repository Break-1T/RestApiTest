using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contex.Infrastructure;
using Contex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contex
{
    public class UserService : IUserService
    {
        private readonly ApplicationContex _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationContex dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUserAsync(CancellationToken cancellationToken)
        {
            string resultMessage = $"Success: {MethodInfo.GetCurrentMethod().Name}";
            try
            {
                var result = await this._dbContext.Users.ToListAsync(cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                resultMessage = $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}";
                return null;
            }
            finally
            {
                _logger.LogWarning(resultMessage);
            }

        }

        public async Task<User> GetUserAsync(int id,CancellationToken cancellationToken)
        {
            string resultMessage = $"Success: {MethodInfo.GetCurrentMethod().Name}";

            try
            {
                var result = await this._dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                resultMessage = $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}";
                return null;
            }
            finally
            {
                _logger.LogWarning(resultMessage);
            }

        }

        public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
        {
            string resultMessage = $"Success: {MethodInfo.GetCurrentMethod().Name}";
            try
            {
                var user = _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (user != null)
                {
                    _dbContext.Users.Remove(user.Result);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return true;
            }
            catch(Exception ex)
            {
                resultMessage = $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}";
                return false;
            }
            finally
            {
                _logger.LogWarning(resultMessage);
            }

        }

        public async Task<bool> AddUserAsync(CancellationToken cancellationToken)
        {
            string resultMessage = $"Success: {MethodInfo.GetCurrentMethod().Name}";
            try
            {
                _dbContext.Users.Add(new User() { Age = new Random().Next(0, 50), CurrentTime = DateTime.Now, Name = "user", Surname = "user" });
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch(Exception ex)
            {
                resultMessage = $"Error in method{MethodInfo.GetCurrentMethod().Name}\n {ex.Message}";
                return false;
            }
            finally
            {
                _logger.LogWarning(resultMessage);
            }

        }
    }
}
