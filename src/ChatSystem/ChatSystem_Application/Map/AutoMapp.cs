using AutoMapper;
using ChatSystem_Application.Dto.Base;
using ChatSystem_Application.Dto.Message.command;
using ChatSystem_Application.Dto.Message.Queries;
using ChatSystem_Domain.Model.Base;
using ChatSystem_Domain.Model.message;

namespace ChatSystem_Application.Map
{
    public class AutoMapp : Profile
    {
        public AutoMapp()
        {
            #region base
            CreateMap<Base_E, Base_D>().ReverseMap();
            #endregion

            #region message
            CreateMap<Message_E, Message_G_D>().ReverseMap();
            CreateMap<Message_E, Message_C_D>().ReverseMap();
            CreateMap<Message_E, Message_U_D>().ReverseMap();
            #endregion
        }
    }
}
