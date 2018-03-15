using RLNET;

namespace GoreLike.Core {
    public class Colours {
        // Tiles
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = Swatch.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Swatch.DbDark;
        public static RLColor FloorFov = Swatch.Alternate;

        public static RLColor WallBackground = Swatch.SecondaryDarkest;
        public static RLColor Wall = Swatch.Secondary;
        public static RLColor WallBackgroundFov = Swatch.SecondaryDarker;
        public static RLColor WallFov = Swatch.SecondaryLighter;

        // Text
        public static RLColor TextHeading = Swatch.DbLight;
        public static RLColor Text = Swatch.DbLight;

        // Misc
        public static RLColor Gold = Swatch.DbSun;

        // Actors
        public static RLColor Player = Swatch.DbLight;

        public static RLColor ZombieColor = Swatch.DbVegetation;
    }
}