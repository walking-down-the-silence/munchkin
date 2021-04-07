using MediatR;
using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Stages;
using Munchkin.Engine.Original.Doors;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Core.Tests.Contracts.Stages
{
    public class DecisionTreeBuilderTests
    {
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

        [Fact]
        public void Transition_WithNullParameter_SholdThrowArgumentNullExpection()
        {
            // Arrange
            var decitionTreeBuilder = DecisionTree.Empty();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => decitionTreeBuilder.Transition(null));
        }

        [Fact]
        public void Transition_WithNotNullParameter_SholdNotThrowArgumentNullExpection()
        {
            // Arrange
            var decitionTreeBuilder = DecisionTree.Empty();
            var transitionConfig = new Action<ITransitionBuilder>(x => { });

            // Act
            var exception = Record.Exception(() => decitionTreeBuilder.Transition(transitionConfig));

            // Act, Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Transition_WithNotNullParameter_ShouldReturnTransitionBuilder()
        {
            // Arrange
            var decitionTreeBuilder = DecisionTree.Empty();
            var transitionConfig = new Action<ITransitionBuilder>(x => { });

            // Act
            var transitionBuilder = decitionTreeBuilder.Transition(transitionConfig);

            // Assert
            Assert.NotNull(transitionBuilder);
            Assert.IsAssignableFrom<ITransitionGraphBuilder>(transitionBuilder);
        }

        [Fact]
        public void Transition_WithConfiguredStepTransition_ShouldReturnTransitionBuilder()
        {
            // Arrange
            var decitionTreeBuilder = DecisionTree.Empty();
            var transitionConfig = new Action<ITransitionBuilder>(x => x
                    .From<KickOpenTheDoorStep>()
                    .To<CombatRoomStep>(
                        configCreation: s => null,
                        configCondition: s => true)
                    .To<CursedRoomStage>(
                        configCreation: s => null,
                        configCondition: s => true));

            // Act
            var transitionBuilder = decitionTreeBuilder.Transition(transitionConfig);

            // Assert
            Assert.NotNull(transitionBuilder);
            Assert.IsAssignableFrom<ITransitionGraphBuilder>(transitionBuilder);
        }

        [Fact]
        public void Build_WithConfiguredStepTransition_ShouldReturnDecisionTree()
        {
            // Arrange
            var decitionTreeBuilder = DecisionTree.Empty();
            var transitionConfig = new Action<ITransitionBuilder>(x => x
                    .From<KickOpenTheDoorStep>()
                    .To<CombatRoomStep>(
                        configCreation: s => null,
                        configCondition: s => true)
                    .To<CursedRoomStage>(
                        configCreation: s => null,
                        configCondition: s => true));

            // Act
            var decisionTree = decitionTreeBuilder
                .Transition(transitionConfig)
                .Build();

            // Assert
            Assert.NotNull(decisionTree);
            Assert.IsType<DecisionTree>(decisionTree);
        }

        [Fact]
        public async void Build_WithTruthfulConditionInTransition_ShouldCallResolveOnEachStep()
        {
            // Arrange
            var table = new Table(Mock.Of<IMediator>());
            var player = new Player("Johny Cash", Core.Model.Enums.EGender.Male);

            var decitionTreeBuilder = DecisionTree.Empty();
            var startStep = new Mock<IStartStep>();
            var endStep = new Mock<IEndStep>();

            startStep.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));

            var transitionConfig = new Action<ITransitionBuilder>(x => x
                    .From<IStartStep>()
                    .To<IEndStep>(
                        configCreation: s => endStep.Object,
                        configCondition: s => true));

            // Act
            var decisionTree = decitionTreeBuilder.Transition(transitionConfig).Build();
            var result = await decisionTree.Resolve(startStep.Object);

            // Assert
            startStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            endStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
        }

        [Fact]
        public async void Build_WithFalseConditionInTransition_ShouldCallResolveOnFirstStepOnly()
        {
            // Arrange
            var table = new Table(Mock.Of<IMediator>());
            var player = new Player("Johny Cash", Core.Model.Enums.EGender.Male);

            var decitionTreeBuilder = DecisionTree.Empty();
            var startStep = new Mock<IStartStep>();
            var endStep = new Mock<IEndStep>();

            startStep.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));

            var transitionConfig = new Action<ITransitionBuilder>(x => x
                    .From<IStartStep>()
                    .To<IEndStep>(
                        configCreation: s => endStep.Object,
                        configCondition: s => false));

            // Act
            var decisionTree = decitionTreeBuilder.Transition(transitionConfig).Build();
            var result = await decisionTree.Resolve(startStep.Object);

            // Assert
            startStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            endStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Never());
        }

        private interface IStartStep : IStep<Table>
        {
        }

        private interface IEndStep : IStep<Table>
        {
        }
    }
}
