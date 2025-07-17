using Configs.Cards;
using UniRx;

namespace Services.Cards
{
    public enum CardStateType
    {
        Selected,
        Deselected,
        Matched,
        Disable
    }
    
    public interface ICardItem
    {
        void Select();
        void Deselect();
        void Match();
        void Disable();
    }
    
    public class CardItem : ICardItem
    {
        private readonly CardConfigModel _cardConfigModel;

        private IReactiveProperty<CardStateType> _stateType = new ReactiveProperty<CardStateType>();
        
        public string Id => _cardConfigModel.id;
        public IReadOnlyReactiveProperty<CardStateType> StateType => _stateType;

        public CardItem(CardConfigModel cardConfigModel)
        {
            _cardConfigModel = cardConfigModel;
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

        public void Disable()
        {
            _stateType.Value = CardStateType.Disable;
        }
    }
}