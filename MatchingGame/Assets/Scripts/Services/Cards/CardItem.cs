using Configs.Cards;
using UniRx;

namespace Services.Cards
{
    public class CardItem : ICardItem
    {
        private CardConfigModel _cardConfigModel;

        private readonly int _staticId;
        private IReactiveProperty<CardStateType> _stateType = new ReactiveProperty<CardStateType>();
        private IReactiveProperty<string> _id = new ReactiveProperty<string>();

        public int StaticId => _staticId;
        public IReadOnlyReactiveProperty<string> Id => _id;
        public IReadOnlyReactiveProperty<CardStateType> StateType => _stateType;

        public CardItem(int staticId)
        {
            _staticId = staticId;
        }
        
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