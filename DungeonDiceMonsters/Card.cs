//Joel Campos
//9/12/2023
//Card Class

namespace DungeonDiceMonsters
{
    public class Card
    {
        public Card(int id, CardInfo info, PlayerOwner owner) 
        {
            _id = id;
            _cardInfo = info;
            _Owner = owner;
        }

        public int OnBoardID { get { return _id; } }
        public int CardID { get { return _cardInfo.ID; } }
        public string Name { get { return _cardInfo.Name; } }
        public CardInfo Info { get { return _cardInfo; } }
        public PlayerOwner Owner { get { return _Owner; } }

        private int _id = -1;
        private CardInfo _cardInfo;
        private PlayerOwner _Owner;
        private bool _IsDiscardted = false;
    }
}
