using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Api.api.v1.Models;
using DbUser = Context.Models.User;

namespace Api.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            AllowNullCollections = true;

            CreateMap<DbUser, UserResponse>();
            CreateMap<UserResponse, DbUser>();
        }
    }
}
