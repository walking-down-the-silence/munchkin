using Moq;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Primitives;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Munchkin.Core.Tests.Primitives
{
    public class DecisionGraphTests
    {
        [Fact]
        public void Transition_WithNullParameter_SholdThrowArgumentNullExpection()
        {
            // Arrange
            var decitionTreeBuilder = DecisionGraph.Empty();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => decitionTreeBuilder.Transition(null));
        }

        [Fact]
        public void Transition_WithNotNullParameter_SholdNotThrowArgumentNullExpection()
        {
            // Arrange
            var decitionTreeBuilder = DecisionGraph.Empty();
            var transitionConfig = new Action<ITransitionFromContext>(x => { });

            // Act
            var exception = Record.Exception(() => decitionTreeBuilder.Transition(transitionConfig));

            // Act, Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Transition_WithNotNullParameter_ShouldReturnTransitionBuilder()
        {
            // Arrange
            var decitionTreeBuilder = DecisionGraph.Empty();
            var transitionConfig = new Action<ITransitionFromContext>(x => { });

            // Act
            var transitionBuilder = decitionTreeBuilder.Transition(transitionConfig);

            // Assert
            Assert.NotNull(transitionBuilder);
            Assert.IsAssignableFrom<ITransitionGraphContext>(transitionBuilder);
        }

        [Fact]
        public void Transition_WithConfiguredStepTransition_ShouldReturnTransitionBuilder()
        {
            // Arrange
            var decitionTreeBuilder = DecisionGraph.Empty();
            var transitionConfig = new Action<ITransitionFromContext>(x => x
                    .From<KickOpenTheDoorStep>(StepNames.KickOpenTheDoor)
                    .To<CombatRoomStep>(
                        configCreation: s => null,
                        configCondition: s => true)
                    .To<CurseStep>(
                        configCreation: s => null,
                        configCondition: s => true));

            // Act
            var transitionBuilder = decitionTreeBuilder.Transition(transitionConfig);

            // Assert
            Assert.NotNull(transitionBuilder);
            Assert.IsAssignableFrom<ITransitionGraphContext>(transitionBuilder);
        }

        [Fact]
        public void Build_WithConfiguredStepTransition_ShouldReturnDecisionTree()
        {
            // Arrange
            var decitionTreeBuilder = DecisionGraph.Empty();
            var transitionConfig = new Action<ITransitionFromContext>(x => x
                    .From<KickOpenTheDoorStep>(StepNames.KickOpenTheDoor)
                    .To<CombatRoomStep>(
                        configCreation: s => null,
                        configCondition: s => true)
                    .To<CurseStep>(
                        configCreation: s => null,
                        configCondition: s => true));

            // Act
            var decisionTree = decitionTreeBuilder
                .Transition(transitionConfig)
                .Build();

            // Assert
            Assert.NotNull(decisionTree);
            Assert.IsType<DecisionGraph>(decisionTree);
        }

        [Fact]
        public async void Build_WithTruthfulConditionInTransition_ShouldCallResolveOnEachStep()
        {
            // Arrange
            var table = Table.Empty();
            var player = new Player("Johny Cash", EGender.Male);

            var decitionTreeBuilder = DecisionGraph.Empty();
            var startStep = new Mock<StartStep>();
            var endStep = new Mock<EndStep>();

            startStep.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));

            var transitionConfig = new Action<ITransitionFromContext>(x => x
                    .From<StartStep>(nameof(StartStep))
                    .To(
                        configCreation: s => endStep.Object,
                        configCondition: s => true));

            // Act
            var decisionTree = decitionTreeBuilder.Transition(transitionConfig).Build();
            var result = await decisionTree.Resolve(table, startStep.Object);

            // Assert
            startStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            endStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
        }

        [Fact]
        public async void Build_WithFalseConditionInTransition_ShouldCallResolveOnFirstStepOnly()
        {
            // Arrange
            var table = Table.Empty();
            var player = new Player("Johny Cash", EGender.Male);

            var decitionTreeBuilder = DecisionGraph.Empty();
            var startStep = new Mock<StartStep>();
            var endStep = new Mock<EndStep>();

            startStep.Setup(x => x.Resolve(It.IsAny<Table>())).Returns(Task.FromResult(table));

            var transitionConfig = new Action<ITransitionFromContext>(x => x
                    .From<StartStep>(nameof(StartStep))
                    .To(
                        configCreation: s => endStep.Object,
                        configCondition: s => false));

            // Act
            var decisionTree = decitionTreeBuilder.Transition(transitionConfig).Build();
            var result = await decisionTree.Resolve(table, startStep.Object);

            // Assert
            startStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Once());
            endStep.Verify(x => x.Resolve(It.IsAny<Table>()), Times.Never());
        }
    }

    public class StartStep : IStep<Table>
    {
        public string Name => nameof(StartStep);

        public virtual Task<Table> Resolve(Table context) => Task.FromResult<Table>(null);
    }

    public class EndStep : IStep<Table>
    {
        public string Name => nameof(EndStep);

        public virtual Task<Table> Resolve(Table context) => Task.FromResult<Table>(null);
    }
}
