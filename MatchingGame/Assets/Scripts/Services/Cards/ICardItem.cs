using Configs.Cards;
using UniRx;

namespace Services.Cards
{
    public interface ICardItem
    {
        int StaticId { get; }
        IReadOnlyReactiveProperty<string> Id { get; }
        IReadOnlyReactiveProperty<CardStateType> StateType { get; }
        void Init(CardConfigModel cardConfigModel);
        void Select();
        void Deselect();
        void Match();
        void Enable();
        void Disable();
    }
}