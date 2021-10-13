using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contex.Models;

namespace Contex.Infrastructure
{
    public interface IUserService
    {
        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns>IEnumerable</returns>
        public Task<IEnumerable<User>> GetUserAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <returns>User</returns>
        public Task<User> GetUserAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">UserId</param>
        public Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken);
        /// <summary>
        /// Add new user
        /// </summary>
        public Task<bool> AddUserAsync(CancellationToken cancellationToken);
    }
}
