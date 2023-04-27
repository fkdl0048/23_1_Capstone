namespace Enums
{
    //test
    enum GameState
    {
        None,
        Start,
        Playing,
        Pause,
        End,
    }

    enum ItemType
    {
        Material,
        Furniture,
    }
}

namespace Structs
{
    //test
    struct GameData
    {
        public int score;
        public int level;
    }
}

namespace Const
{
    //test
    static class Consts
    {
        public const int MAX_SCORE = 100;
        public const int MAX_LEVEL = 10;

        public const int SCENE = 0;
        public const int BRANCH = 1;
        public const int NAME = 2;
        public const int SCRIPT = 3;
    }
}
