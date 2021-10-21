using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.Models;
using Context.Models;
using System.Text.Json;

namespace Api.Infrastructure.Extensions
{
    public static class ModelConvertExtensions
    {
        /// <summary>
        /// Converts to userapimodel.
        /// </summary>
        /// <param name="user">The userList.</param>
        /// <returns>UserApiModel</returns>
        public static UserApiModel ToUserApiModel(this User user)
        {
            var result = user == null ? null: new UserApiModel()
            {
                Age = user.Age,
                CurrentTime = user.CurrentTime,
                Id = user.Id,
                Name = user.Name,
                Operations = user.Operations,
                Surname = user.Surname
            };
            return result;
        }
        /// <summary>
        /// Converts to userList.
        /// </summary>
        /// <param name="user">The userList.</param>
        /// <returns>User</returns>
        public static User ToUser(this UserApiModel user)
        {
           var result = user == null ? null :new User()
            {
                Age = user.Age,
                CurrentTime = user.CurrentTime,
                Id = user.Id,
                Name = user.Name,
                Operations = user.Operations,
                Surname = user.Surname
            };
           return result;
        }

        /// <summary>
        /// Converts to userlistapimodel.
        /// </summary>
        /// <param name="userList">The user list.</param>
        /// <returns></returns>
        public static IEnumerable<UserApiModel> ToUserListApiModel(this IEnumerable<User> userList)
        {
            var list = new List<UserApiModel>();
            foreach (var user in userList)
                list.Add(user.ToUserApiModel());
            return list;
        }

        /// <summary>
        /// Converts to userlist.
        /// </summary>
        /// <param name="userList">The user list.</param>
        /// <returns></returns>
        public static IEnumerable<User> ToUserList(this IEnumerable<UserApiModel> userList)
        {
            var list = new List<User>();
            foreach (var userApiModel in userList)
                list.Add(userApiModel.ToUser());
            return list;
        }

        public static string ToStringJson(this UserApiModel model) => JsonSerializer.Serialize(model);

        public static UserApiModel ToUserApiModel(this string str) => JsonSerializer.Deserialize<UserApiModel>(str);


        /// <summary>
        /// Converts to operationapimodel.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>OperationApiModel</returns>
        public static OperationApiModel ToOperationApiModel(this Operation operation)
        {
            return new OperationApiModel()
            {
                Id = operation.Id,
                Name = operation.Name,
                DateTime=operation.DateTime,
                User=operation.User
            };
        }
    }
}
