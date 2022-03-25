using Data.constants;
using Data.context;
using DataAccess.contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using WorkflowProcessor.contracts;
using WorkflowProcessor.expressions;

namespace WorkflowProcessorTests.expressions
{
    public class ExpressionCalculatorTests
    {
        private IExpressionCalculator expressionCalculator;
        private Mock<IWorkflowQueries> workflowQueriesMock = new Mock<IWorkflowQueries>();

        [SetUp]
        public void Setup()
        {
            workflowQueriesMock
                .Setup(w => w.GetMarketData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(5);
            workflowQueriesMock
                .Setup(w => w.GetMarketData("BTC", "PRICE", It.IsAny<int>()))
                .Returns(6);
            workflowQueriesMock
                .Setup(w => w.GetMarketData("LTC", "SIZE", It.IsAny<int>()))
                .Returns(7);
            expressionCalculator = new ExpressionCalculator(workflowQueriesMock.Object);
        }

        [Test]
        public void SimpleAdditionTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 8
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Addition,
                        ConstantValue = "+"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void SimpleSubtractionTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 8
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Subtraction,
                        ConstantValue = "-"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void SimpleMultiplicationTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 8
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Multiplication,
                        ConstantValue = "*"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(32, result);
        }
        
        [Test]
        public void SimpleDivisionTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 8
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Division,
                        ConstantValue = "/"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void MarketDataAdditionTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.MarketValue
                    },
                    MarketDataType = new MarketDataType
                    {
                        Code = "PRICE",
                        ExpirationMinutes = 5
                    },
                    Product = new Product
                    {
                        Code = "BTC"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Addition,
                        ConstantValue = "+"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void MarketDataMultiplicationTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.MarketValue
                    },
                    MarketDataType = new MarketDataType
                    {
                        Code = "SIZE",
                        ExpirationMinutes = 5
                    },
                    Product = new Product
                    {
                        Code = "LTC"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Multiplication,
                        ConstantValue = "*"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 4
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(28, result);
        }

        [Test]
        public void SimpleParenthesisTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.OpenParenthesis,
                        ConstantValue = "("
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 13
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Addition,
                        ConstantValue = "+"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 6
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.CloseParenthesis,
                        ConstantValue = ")"
                    }
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(19, result);
        }

        [Test]
        public void ComplexParenthesisTest()
        {
            // Arrange
            var input = new List<ConditionToken>
            {
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 3
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Multiplication,
                        ConstantValue = "*"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.OpenParenthesis,
                        ConstantValue = "("
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 13
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Addition,
                        ConstantValue = "+"
                    }
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.Constant
                    },
                    ConstantValue = 6
                },
                new ConditionToken
                {
                    ConditionTokenType = new ConditionTokenType
                    {
                        Code = WorkflowConstants.ConditionTokenTypes.CloseParenthesis,
                        ConstantValue = ")"
                    }
                }
            };

            // Act
            var result = expressionCalculator.Calculate(input);

            // Assert
            Assert.AreEqual(57, result);
        }
    }
}
