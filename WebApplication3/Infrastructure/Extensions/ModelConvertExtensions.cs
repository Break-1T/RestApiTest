using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.v1.Models;
using Context.Models;
using System.Text.Json;
using AutoMapper;
using Operation = Api.v1.Models.Operation;
using User = Api.v1.Models.User;

namespace Api.Infrastructure.Extensions
{
    public static class ModelConvertExtensions
    {
        /// <summary>
        /// Converts to userapimodel.
        /// </summary>
        /// <param name="user">The userList.</param>
        /// <returns>User</returns>
        public static User ToUserApiModel(this Context.Models.User user,IMapper mapper)
        {
            var result = user == null ? null : mapper.Map<User>(user);
            return result;
        }
        /// <summary>
        /// Converts to userList.
        /// </summary>
        /// <param name="user">The userList.</param>
        /// <returns>User</returns>
        public static Context.Models.User ToUser(this User user)
        {
           var result = user == null ? null :new Context.Models.User()
            {
                Age = user.Age,
                CurrentTime = user.CurrentTime,
                Id = user.Id,
                Name = user.Name,
                //Operations = user.Operations,
                Surname = user.Surname
            };
           return result;
        }

        /// <summary>
        /// Converts to userlistapimodel.
        /// </summary>
        /// <param name="userList">The user list.</param>
        /// <returns></returns>
        public static IEnumerable<User> ToUserListApiModel(this IEnumerable<Context.Models.User> userList)
        {
            var list = new List<User>();
            //foreach (var user in userList)
                //list.Add(user.ToUserApiModel());
            return list;
        }

        /// <summary>
        /// Converts to userlist.
        /// </summary>
        /// <param name="userList">The user list.</param>
        /// <returns></returns>
        public static IEnumerable<Context.Models.User> ToUserList(this IEnumerable<User> userList)
        {
            var list = new List<Context.Models.User>();
            foreach (var userApiModel in userList)
                list.Add(userApiModel.ToUser());
            return list;
        }

        public static string ToStringJson(this User model) => JsonSerializer.Serialize(model);

        public static User ToUserApiModel(this string str) => JsonSerializer.Deserialize<User>(str);


        /// <summary>
        /// Converts to operationapimodel.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>Operation</returns>
        public static Operation ToOperationApiModel(this Context.Models.Operation operation)
        {
            return new Operation()
            {
                Id = operation.Id,
                Name = operation.Name,
                DateTime=operation.DateTime,
                //User=operation.User
            };
        }
    }
}
