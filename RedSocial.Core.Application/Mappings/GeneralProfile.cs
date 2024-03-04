using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Core.Application.Viewmodel.UserViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region "Account"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HassError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<RegistrerRequest, UserPostViewModel>()
                .ForMember(x => x.file, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<dbUserPostViewModel, UserPostViewModel>()
                .ForMember(x => x.file, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.publications, opt => opt.Ignore())
                .ForMember(x => x.friends, opt => opt.Ignore())
                .ForMember(x => x.comment, opt => opt.Ignore());
                    

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ReverseMap();
            #endregion

            #region "Comments"
            CreateMap<Comment, CommentsViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore()
                ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
                ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
                ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Comment, CommentsPostViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore()
               ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
               ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
               ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


            #endregion

            #region "Friends"
            CreateMap<Friends, FrinendsViewModel>()
                .ReverseMap()
                 .ForMember(dest => dest.Created, opt => opt.Ignore()
               ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
               ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
               ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Friends, FrinedsPostViewModel>()
              .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore()
             ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
             ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
             ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "Publications"
            CreateMap<Publication, PublicationViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore()
               ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
               ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
               ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Publication, PublicationPostViewModel>()
                .ForMember(dest => dest.IdenityUserId, opt => opt.Ignore())
                .ForMember(des => des.file, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(dest => dest.Created, opt => opt.Ignore()
             ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
             ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
             ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "Reply"
            CreateMap<Reply, ReplyViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.Ignore()
             ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
             ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
             ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Reply, ReplyPostViewModel>()
               .ReverseMap()
               .ForMember(dest => dest.Created, opt => opt.Ignore()
            ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
            ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
            ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region "User"
            CreateMap<User, dbUserPostViewModel>()
                .ReverseMap()
                        .ForMember(dest => dest.Created, opt => opt.Ignore()
            ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
            ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
            ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<User, dbUserViewModel>()
                .ReverseMap()
                        .ForMember(dest => dest.Created, opt => opt.Ignore()
            ).ForMember(dest => dest.CreatedBy, opt => opt.Ignore()
            ).ForMember(dest => dest.LasModified, opt => opt.Ignore()
            ).ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<UserPostViewModel, dbUserPostViewModel>()
              .ForMember(dest => dest.publications, opt => opt.Ignore())
              .ForMember(dest => dest.friends, opt => opt.Ignore())
              .ForMember(dest => dest.comment, opt => opt.Ignore())
              .ForMember(dest => dest.UserIdIndentity, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(dest => dest.HasError, opt => opt.Ignore())
              .ForMember(dest => dest.Error, opt => opt.Ignore())
              .ForMember(dest => dest.file, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.Ignore());
              
            #endregion;

        }
    }
}
