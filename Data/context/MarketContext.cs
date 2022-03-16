using Data.constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.context
{
    public class MarketContext : DbContext
    {
        public DbSet<MarketDataEntry> MarketDataEntries { get; set; }
        public DbSet<MarketDataValue> MarketDataValues { get; set; }
        public DbSet<MarketDataType> MarketDataTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowActionType> WorkflowActionTypes { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ConditionType> ConditionTypes { get; set; }
        public DbSet<Expression> Expressions { get; set; }
        public DbSet<ExpressionType> ExpressionTypes { get; set; }

        public MarketContext() { }

        public MarketContext(DbContextOptions<MarketContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MarketWatcher;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarketDataType>(type =>
            {
                type.HasData(
                    new MarketDataType { Id = 1, Code = MarketDataTypeConstants.BidPrice, Name = "Bid Price" },
                    new MarketDataType { Id = 2, Code = MarketDataTypeConstants.AskPrice, Name = "Ask Price" },
                    new MarketDataType { Id = 3, Code = MarketDataTypeConstants.BidSize, Name = "Bid Size" },
                    new MarketDataType { Id = 4, Code = MarketDataTypeConstants.AskSize, Name = "Ask Size" },
                    new MarketDataType { Id = 5, Code = MarketDataTypeConstants.FiveMinutePriceChange, Name = "Five Minute Price Change" },
                    new MarketDataType { Id = 6, Code = MarketDataTypeConstants.FifteenMinutePriceChange, Name = "Ten Minute Price Change" },
                    new MarketDataType { Id = 10, Code = MarketDataTypeConstants.BidAskSpread, Name = "Bid Ask Spread" }
                );
            });

            modelBuilder.Entity<ProductType>(type =>
            {
                type.HasData(
                    new ProductType { Id = 1, Code = ProductTypeConstants.Crypto, Name = "Crypto Currency"}
                );
            });

            modelBuilder.Entity<Product>(type =>
            {
                type.HasData(
                    new Product { Id = 1, Code = "BTC-USD", ProductTypeId = 1 },
                    new Product { Id = 2, Code = "ETC-USD", ProductTypeId = 1 }
                );
            });

            modelBuilder.Entity<WorkflowActionType>(type =>
            {
                type.HasData(
                    new WorkflowActionType { Id = 1, Code = WorkflowConstants.Actions.Buy, Name = "Buy" },
                    new WorkflowActionType { Id = 2, Code = WorkflowConstants.Actions.Sell, Name = "Sell" },
                    new WorkflowActionType { Id = 3, Code = WorkflowConstants.Actions.Email, Name = "Email" },
                    new WorkflowActionType { Id = 4, Code = WorkflowConstants.Actions.None, Name = "Nothing" },
                    new WorkflowActionType { Id = 5, Code = WorkflowConstants.Actions.Condition, Name = "Condition" }
                );
            });

            modelBuilder.Entity<ConditionType>(type => {
                type.HasData(
                    new ConditionType { Id = 1, Code = WorkflowConstants.ConditionTypes.GreaterThan, Name = "Greater Than" },
                    new ConditionType { Id = 2, Code = WorkflowConstants.ConditionTypes.GreaterThanOrEqual, Name = "Greater Than or Equal To" },
                    new ConditionType { Id = 3, Code = WorkflowConstants.ConditionTypes.LessThan, Name = "Less Than" },
                    new ConditionType { Id = 4, Code = WorkflowConstants.ConditionTypes.LessThanOrEqual, Name = "Less Than or Equal To" },
                    new ConditionType { Id = 5, Code = WorkflowConstants.ConditionTypes.Equal, Name = "Equal To"}
                );
            });
        }
    }
}
