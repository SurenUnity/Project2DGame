using Configs.Cards;
using UniRx;

namespace Services.Cards
{
    public enum CardStateType
    {
        Enable,
        Selected,
        Deselected,
        Matched,
        Disable
    }
    
    public interface ICardItem
    {
        IReadOnlyReactiveProperty<string> Id { get; }
        IReadOnlyReactiveProperty<CardStateType> StateType { get; }
        void Init(CardConfigModel cardConfigModel);
        void Select();
        void Deselect();
        void Match();
        void Enable();
        void Disable();
    }
    
    public class CardItem : ICardItem
    {
        private CardConfigModel _cardConfigModel;

        private IReactiveProperty<CardStateType> _stateType = new ReactiveProperty<CardStateType>();
        private IReactiveProperty<string> _id = new ReactiveProperty<string>();
        
        public IReadOnlyReactiveProperty<string> Id => _id;
        public IReadOnlyReactiveProperty<CardStateType> StateType => _stateType;

        public void Init(CardConfigModel cardConfigModel)
        {
            _cardConfigModel = cardConfigModel;
            _id.Value = cardConfigModel.id;
        }
        
        public void Select()
        {
            _stateType.Value = CardStateType.Selected;
        }

        public void Deselect()
        {
            _stateType.Value = CardStateType.Deselected;
        }

        public void Match()
        {
            _stateType.Value = CardStateType.Matched;
        }

        public void Enable()
        {
            _stateType.Value = CardStateType.Enable;
        }

        public void Disable()
        {
            _stateType.Value = CardStateType.Disable;
        }
    }
}