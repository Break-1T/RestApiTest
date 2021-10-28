using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.api.v1.Models;
using Context.Models;
using System.Text.Json;
using AutoMapper;
using Operation = Api.api.v1.Models.Operation;

namespace Api.Infrastructure.Extensions
{
    public static class ModelConvertExtensions
    {
        /// <summary>
        /// Converts to userapimodel.
        /// </summary>
        /// <param name="user">The userList.</param>
        /// <returns>UserResponse</returns>
        public static UserResponse ToUserApiModel(this Context.Models.User user,IMapper mapper)
        {
            var result = user == null ? null : mapper.Map<UserResponse>(user);
            return result;
        }
        /// <summary>
        /// Converts to userList.
        /// </summary>
        /// <param name="userResponse">The userList.</param>
        /// <returns>UserResponse</returns>
        public static Context.Models.User ToUser(this UserResponse userResponse)
        {
           var result = userResponse == null ? null :new Context.Models.User()
            {
                Age = userResponse.Age,
                CurrentTime = userResponse.CurrentTime,
                Id = userResponse.Id,
                Name = userResponse.Name,
                //Operations = userResponse.Operations,
                Surname = userResponse.Surname
            };
           return result;
        }

        /// <summary>
        /// Converts to userlistapimodel.
        /// </summary>
        /// <param name="userList">The userResponse list.</param>
        /// <returns></returns>
        public static IEnumerable<UserResponse> ToUserListApiModel(this IEnumerable<Context.Models.User> userList)
        {
            var list = new List<UserResponse>();
            //foreach (var userResponse in userList)
                //list.Add(userResponse.ToUserApiModel());
            return list;
        }

        /// <summary>
        /// Converts to userlist.
        /// </summary>
        /// <param name="userList">The userResponse list.</param>
        /// <returns></returns>
        public static IEnumerable<Context.Models.User> ToUserList(this IEnumerable<UserResponse> userList)
        {
            var list = new List<Context.Models.User>();
            foreach (var userApiModel in userList)
                list.Add(userApiModel.ToUser());
            return list;
        }

        public static string ToStringJson(this UserResponse model) => JsonSerializer.Serialize(model);

        public static UserResponse ToUserApiModel(this string str) => JsonSerializer.Deserialize<UserResponse>(str);


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
                //UserResponse=operation.UserResponse
            };
        }
    }
}
