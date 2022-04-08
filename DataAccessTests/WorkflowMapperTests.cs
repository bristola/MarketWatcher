using AutoMapper;
using Data.context;
using Data.data;
using DataAccess;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessTests
{
    public class WorkflowMapperTests
    {
        private IMapper mapper = null;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperProfile>();
            });

            mapper = config.CreateMapper();
        }

        [Test]
        public void ConditionTokenTypeTest()
        {
            var source = new ConditionTokenType
            {
                Id = 3,
                Name = "Name1",
                Code = "Code1"
            };

            var result = mapper.Map<ConditionTokenType, ConditionTokenTypeDTO>(source);

            Assert.AreEqual(source.Id, result.ConditionTokenTypeId);
            Assert.AreEqual(source.Name, result.Name);
            Assert.AreEqual(source.Code, result.Code);
        }

        [Test]
        public void ConditionTest()
        {
            var leftTokenId = 11;
            var rightTokenId = 22;

            var source = new Condition
            {
                Id = 3,
                ConditionType = new ConditionType
                {
                    Id = 5,
                    Name = "Name 1",
                    Code = "Code 1"
                },
                Tokens = new List<ConditionToken>
                {
                    new ConditionToken
                    {
                        Id = leftTokenId,
                        IsLeftExpression = true
                    },
                    new ConditionToken
                    {
                        Id = rightTokenId,
                        IsLeftExpression = false
                    },
                }
            };

            var result = mapper.Map<Condition, ConditionDTO>(source);

            Assert.AreEqual(source.Id, result.ConditionId);
            Assert.AreEqual(source.ConditionType.Id, result.ConditionType.ConditionTypeId);
            Assert.AreEqual(source.ConditionType.Name, result.ConditionType.Name);
            Assert.AreEqual(source.ConditionType.Code, result.ConditionType.Code);
            Assert.AreEqual(leftTokenId, result.LeftTokens.First().ConditionTokenId);
            Assert.AreEqual(rightTokenId, result.RightTokens.First().ConditionTokenId);
        }
    }
}