using System;

namespace Configs.Cards
{
    [Serializable]
    public class CardsConfigModel
    {
        public CardConfigModel[] cards;
    }

    [Serializable]
    public class CardConfigModel
    {
        public string id;
    }
}