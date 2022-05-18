namespace Munchkin.Core.Model.Phases.Events
{
    public record CharityGivenAwayEvent(
        string GiverNickname,
        string TakerNickname,
        string CardId);
}
