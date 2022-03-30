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
            CreateMap<Condition, ConditionDTO>();
            CreateMap<ConditionToken, ConditionTokenDTO>();
            CreateMap<ConditionTokenType, ConditionTokenTypeDTO>();
            CreateMap<ConditionType, ConditionTypeDTO>();
            CreateMap<MarketDataType, MarketDataTypeDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductType, ProductTypeDTO>();
            CreateMap<Workflow, WorkflowDTO>();
            CreateMap<WorkflowStatus, WorkflowStatusDTO>();
            CreateMap<WorkflowAction, WorkflowActionDTO>();
            CreateMap<WorkflowActionType, WorkflowActionTypeDTO>();
        }
    }
}
