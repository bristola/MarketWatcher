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
    public static class AutoMapperFactory
    {
        public static MapperConfiguration Create()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<Condition, ConditionDTO>();
                c.CreateMap<ConditionToken, ConditionTokenDTO>();
                c.CreateMap<ConditionTokenType, ConditionTokenTypeDTO>();
                c.CreateMap<ConditionType, ConditionTypeDTO>();
                c.CreateMap<MarketDataType, MarketDataTypeDTO>();
                c.CreateMap<Product, ProductDTO>();
                c.CreateMap<ProductType, ProductTypeDTO>();
                c.CreateMap<Workflow, WorkflowDTO>();
                c.CreateMap<WorkflowStatus, WorkflowStatusDTO>();
                c.CreateMap<WorkflowAction, WorkflowActionDTO>();
                c.CreateMap<WorkflowActionType, WorkflowActionTypeDTO>();
            });
        }
    }
}
