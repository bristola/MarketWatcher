using AutoMapper;
using Data.context;
using Data.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Condition, ConditionDTO>()
                .ForMember(a => a.ConditionId, c => c.MapFrom(u => u.Id))
                .ForMember(c => c.LeftTokens, c => c.MapFrom(u => u.Tokens.Where(a => a.IsLeftExpression).ToList()))
                .ForMember(c => c.RightTokens, c => c.MapFrom(u => u.Tokens.Where(a => !a.IsLeftExpression).ToList()));

            CreateMap<ConditionToken, ConditionTokenDTO>()
                .ForMember(a => a.ConditionTokenId, c => c.MapFrom(u => u.Id));

            CreateMap<ConditionTokenType, ConditionTokenTypeDTO>()
                .ForMember(c => c.ConditionTokenTypeId, c => c.MapFrom(c => c.Id));

            CreateMap<ConditionType, ConditionTypeDTO>()
                .ForMember(c => c.ConditionTypeId, c => c.MapFrom(c => c.Id));

            CreateMap<MarketDataType, MarketDataTypeDTO>()
                .ForMember(c => c.MarketDataTypeId, c => c.MapFrom(c => c.Id));

            CreateMap<Product, ProductDTO>()
                .ForMember(c => c.ProductId, c => c.MapFrom(c => c.Id));

            CreateMap<ProductType, ProductTypeDTO>()
                .ForMember(c => c.ProductTypeId, c => c.MapFrom(c => c.Id));

            CreateMap<Workflow, WorkflowDTO>()
                .ForMember(c => c.WorkflowId, c => c.MapFrom(c => c.Id));

            CreateMap<WorkflowStatus, WorkflowStatusDTO>()
                .ForMember(c => c.WorkflowStatusId, c => c.MapFrom(c => c.Id));

            CreateMap<WorkflowAction, WorkflowActionDTO>()
                .ForMember(c => c.WorkflowActionId, c => c.MapFrom(c => c.Id));

            CreateMap<WorkflowActionType, WorkflowActionTypeDTO>()
                .ForMember(c => c.WorkflowActionTypeId, c => c.MapFrom(c => c.Id));
        }
    }
}
