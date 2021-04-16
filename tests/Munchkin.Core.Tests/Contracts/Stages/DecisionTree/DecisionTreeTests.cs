using MediatR;
using Moq;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Core.Tests.Contracts.Stages
{
    public class DecisionTreeTests
    {
        [Fact]
        public void From_WithNotNullParameter_ShouldntThrowArgumentException()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();

            // Act
            var exception = Record.Exception(() => DecisionTree.Empty());

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void From_WithNotNullParameter_ShouldReturnNotNullResult()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();

            // Act
            var result = DecisionTree.Empty().Then(step);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void From_WithNotNullParameter_ShouldReturnStepConfigurationTypeInstance()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();

            // Act
            var result = DecisionTree.Empty().Then(step);

            // Assert
            Assert.IsAssignableFrom<IDecisionTreeBuilder>(result);
        }

        [Fact]
        public async void ExecuteAsync_WithNullParameter_ShouldThrowArgumentNullException()
        {
            // Arrange
            var step1 = Mock.Of<IStep<Table>>();
            var step2 = Mock.Of<IStep<Table>>();
            var decisionTree = DecisionTree
                .Empty()
                .Then(step1)
                .Then(step2)
                .Build();

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => decisionTree.ExecuteAsync(null));
        }

        [Fact]
        public async void ExecuteAsync_WithNotNullParameter_ShouldntThrowArgumentNullException()
        {
            // Arrange
            var table = Table.Empty();
            var step1 = Mock.Of<IStep<Table>>();
            var step2 = Mock.Of<IStep<Table>>();
            var decisionTree = DecisionTree
                .Empty()
                .Then(step1)
                .Then(step2)
                .Build();

            // Act
            var exception = await Record.ExceptionAsync(() => decisionTree.ExecuteAsync(table));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void ExecuteAsync_WithNotNullParameter_ShouldResolveNestedSteps()
        {
            // Arrange
            var table = Table.Empty();
            var step1 = new Mock<IStep<Table>>();
            var step2 = new Mock<IStep<Table>>();
            var decisionTree = DecisionTree
                .Empty()
                .Then(step1.Object)
                .Then(step2.Object)
                .Build();

            // Act
            await decisionTree.ExecuteAsync(table);

            // Assert
            step1.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            step2.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
        }

        [Fact]
        public async void ExecuteAsync_WithStepsUnderCondition_ShouldResolve2OutOf3Steps()
        {
            // Arrange
            var table = Table.Empty();
            var step1 = new Mock<IStep<Table>>();
            var step2 = new Mock<IStep<Table>>();
            var step3 = new Mock<IStep<Table>>();

            step1.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));
            step2.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));
            step3.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));

            var condition = new Func<Table, Task<bool>>(table => Task.FromResult(true));
            var branch1Builder = new Func<IDecisionTreeContext, IDecisionTreeBuilder>(branch1 => branch1.Then(step2.Object));
            var branch2Builder = new Func<IDecisionTreeContext, IDecisionTreeBuilder>(branch2 => branch2.Then(step3.Object));

            var decisionTree = DecisionTree
                .Empty()
                .Then(step1.Object)
                .Condition(condition, branch1Builder, branch2Builder)
                .Build();

            // Act
            await decisionTree.ExecuteAsync(table);

            // Assert
            step1.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            step2.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            step3.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Never());
        }
        [Fact]
        public void Then_WithNullParameter_ShouldThrowArgumentNullException()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();

            // Act
            var builder = DecisionTree.Empty();

            // Assert
            Assert.Throws<ArgumentNullException>(() => builder.Then(null));
        }

        [Fact]
        public void Then_WithNonNullParameter_ShouldntThrowArrgumentException()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();
            var builder = DecisionTree.Empty();

            // Act
            var exception = Record.Exception(() => builder.Then(step));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Then_WithNonNullParameter_ShouldReturnNotNullResult()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();
            var builder = DecisionTree.Empty();

            // Act
            var result = builder.Then(step);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Then_WithNotNullParameter_ShouldReturnDecisionTreeBuilderTypeInstance()
        {
            // Arrange
            var step = Mock.Of<IStep<Table>>();
            var builder = DecisionTree.Empty();

            // Act
            var result = builder.Then(step);

            // Assert
            Assert.IsAssignableFrom<IDecisionTreeBuilder>(result);
        }

        [Fact]
        public void Build_ShouldReturnDecisionTreeInstance()
        {
            // Arrange
            var builder = DecisionTree.Empty();

            // Act
            var decisionTree = builder.Build();

            // Assert
            Assert.NotNull(decisionTree);
            Assert.IsType<DecisionTree>(decisionTree);
        }
    }
}
