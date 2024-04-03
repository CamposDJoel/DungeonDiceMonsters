//Joel Campos
//10/10/2023
//Dimension Class

namespace DungeonDiceMonsters
{
    public class Dimension
    {
        public Dimension(Tile[] tiles, DimensionForms form, PlayerColor owner) 
        {
            _Tiles = tiles;
            _Form = form;
            _IsValid = IsThisDimensionValid(_Tiles, owner);
        }

        public int SummonTileID { get { return _Tiles[0].ID; } }
        public bool IsValid { get { return _IsValid; } }
        public DimensionForms Form { get { return _Form; } }

        public static bool IsThisDimensionValid(Tile[] tiles, PlayerColor owner)
        {
            bool validDimension = true;
            int adjencentToPlayerCount = 0;
            for (int x = 0; x < tiles.Length; x++)
            {
                //If ANY of the tiles is null, it is automatically invalid
                if (tiles[x] == null)
                {
                    validDimension = false; break;
                }
                else
                {
                    //If any of the tiles HAS an owner, then it is invalid
                    if (tiles[x].Owner != PlayerColor.NONE)
                    {
                        validDimension = false; break;
                    }
                    else
                    {
                        //check that at least ONE tile is adjecend to a own tile of the player
                        bool IsAdjecentToPlayer = tiles[x].HasAnAdjecentTileOwnBy(owner);
                        if (IsAdjecentToPlayer) { adjencentToPlayerCount++; }
                    }
                }
            }
            if (adjencentToPlayerCount == 0) { validDimension = false; }

            return validDimension;
        }

        private Tile[] _Tiles;
        private bool _IsValid = false;
        private DimensionForms _Form;
    }

    public enum DimensionForms
    {
        //CROSS FORMs
        CrossBase,
        CrossRight,
        CrossLeft,
        CrossUpSideDown,

        //LONG FORM
        LongBase,
        LongRight,
        LongLeft,
        LongUpSideDown,

        //LONG FLIPPED
        LongFlippedBase,
        LongFlippedRight,
        LongFlippedLeft,
        LongFlippedUpSideDown,

        //Z FORM
        ZBase,
        ZRight,
        ZLeft,
        ZUpSideDown,

        //Z FLIPPED
        ZFlippedBase,
        ZFlippedRight,
        ZFlippedLeft,
        ZFlippedUpSideDown,

        //T FORM
        TBase,
        TRight,
        TLeft,
        TUpSideDown,
    }
}
