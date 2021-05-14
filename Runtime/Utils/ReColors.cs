using UnityEngine;

namespace ReGizmo.Drawing
{
    public static class ReColors
    {
        static ReColors()
        {
        }


        /// <summary>
        /// Returns a copy of color with the given alpha value
        /// </summary>
        /// <returns>A COPY of the color</returns>
        public static Color WithAlpha(this Color self, float alpha)
        {
            Color color = new Color(self.r, self.g, self.b, alpha);
            return color;
        }

        public static Color Random()
        {
            return new Color(
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f),
                1f
            );
        }

        public static Vector3 ToVector3(this Color color)
        {
            return new Vector3(color.a, color.g, color.b);
        }

        public static readonly Color AMARANTH = new Color(229f / 255f, 43f / 255f, 80f / 255f);
        public static readonly Color AMBER = new Color(255f / 255f, 191f / 255f, 0);
        public static readonly Color AMETHYST = new Color(153f / 255f, 102f / 255f, 204f / 255f);
        public static readonly Color APRICOT = new Color(251f / 255f, 206f / 255f, 177f / 255f);
        public static readonly Color AQUAMARINE = new Color(127f / 255f, 255f / 255f, 212f / 255f);
        public static readonly Color AZURE = new Color(0, 127f / 255f, 255f / 255f);
        public static readonly Color BABY_BLUE = new Color(137f / 255f, 207f / 255f, 240f / 255f);
        public static readonly Color BEIGE = new Color(245f / 255f, 245f / 255f, 220f / 255f);
        public static readonly Color BRICK_RED = new Color(203f / 255f, 65f / 255f, 84f / 255f);
        public static readonly Color BLACK = new Color(0, 0, 0);
        public static readonly Color BLUE = new Color(0, 0, 255f / 255f);
        public static readonly Color BLUE_GREEN = new Color(0, 149f / 255f, 182f / 255f);
        public static readonly Color BLUE_VIOLET = new Color(138f / 255f, 43f / 255f, 226f / 255f);
        public static readonly Color BLUSH = new Color(222f / 255f, 93f / 255f, 131f / 255f);
        public static readonly Color BRONZE = new Color(205f / 255f, 127f / 255f, 50f / 255f);
        public static readonly Color BROWN = new Color(150f / 255f, 75f / 255f, 0);
        public static readonly Color BURGUNDY = new Color(128f / 255f, 0, 32f / 255f);
        public static readonly Color BYZANTIUM = new Color(112f / 255f, 41f / 255f, 99f / 255f);
        public static readonly Color CARMINE = new Color(150f / 255f, 0, 24f / 255f);
        public static readonly Color CERISE = new Color(222f / 255f, 49f / 255f, 99f / 255f);
        public static readonly Color CERULEAN = new Color(0, 123f / 255f, 167f / 255f);
        public static readonly Color CHAMPAGNE = new Color(247f / 255f, 231f / 255f, 206f / 255f);
        public static readonly Color CHARTREUSE_GREEN = new Color(127f / 255f, 255f / 255f, 0);
        public static readonly Color CHOCOLATE = new Color(123f / 255f, 63f / 255f, 0);
        public static readonly Color COBALT_BLUE = new Color(0, 71f / 255f, 171f / 255f);
        public static readonly Color COFFEE = new Color(111f / 255f, 78f / 255f, 55f / 255f);
        public static readonly Color COPPER = new Color(184f / 255f, 115f / 255f, 51f / 255f);
        public static readonly Color CORAL = new Color(255f / 255f, 127f / 255f, 80f / 255f);
        public static readonly Color CRIMSON = new Color(220f / 255f, 20f / 255f, 60f / 255f);
        public static readonly Color CYAN = new Color(0, 255f / 255f, 255f / 255f);
        public static readonly Color DESERT_SAND = new Color(237f / 255f, 201f / 255f, 175f / 255f);
        public static readonly Color ELECTRIC_BLUE = new Color(125f / 255f, 249f / 255f, 255f / 255f);
        public static readonly Color EMERALD = new Color(80f / 255f, 200f / 255f, 120f / 255f);
        public static readonly Color ERIN = new Color(0, 255f / 255f, 63f / 255f);
        public static readonly Color GOLD = new Color(255f / 255f, 215f / 255f, 0);
        public static readonly Color GRAY = new Color(128f / 255f, 128f / 255f, 128f / 255f);
        public static readonly Color GREEN = new Color(0, 128f / 255f, 0);
        public static readonly Color HARLEQUIN = new Color(63f / 255f, 255f / 255f, 0);
        public static readonly Color INDIGO = new Color(75f / 255f, 0, 130f / 255f);
        public static readonly Color IVORY = new Color(255f / 255f, 255f / 255f, 240f / 255f);
        public static readonly Color JADE = new Color(0, 168f / 255f, 107f / 255f);
        public static readonly Color JUNGLE_GREEN = new Color(41f / 255f, 171f / 255f, 135f / 255f);
        public static readonly Color LAVENDER = new Color(181f / 255f, 126f / 255f, 220f / 255f);
        public static readonly Color LEMON = new Color(255f / 255f, 247f / 255f, 0);
        public static readonly Color LILAC = new Color(200f / 255f, 162f / 255f, 200f / 255f);
        public static readonly Color LIME = new Color(191f / 255f, 255f / 255f, 0);
        public static readonly Color MAGENTA = new Color(255f / 255f, 0, 255f / 255f);
        public static readonly Color MAGENTA_ROSE = new Color(255f / 255f, 0, 175f / 255f);
        public static readonly Color MAROON = new Color(128f / 255f, 0, 0);
        public static readonly Color MAUVE = new Color(224f / 255f, 176f / 255f, 255f / 255f);
        public static readonly Color NAVY_BLUE = new Color(0, 0, 128f / 255f);
        public static readonly Color OCHRE = new Color(204f / 255f, 119f / 255f, 34f / 255f);
        public static readonly Color OLIVE = new Color(128f / 255f, 128f / 255f, 0);
        public static readonly Color ORANGE = new Color(255f / 255f, 102f / 255f, 0);
        public static readonly Color ORANGE_RED = new Color(255f / 255f, 69f / 255f, 0);
        public static readonly Color ORCHID = new Color(218f / 255f, 112f / 255f, 214f / 255f);
        public static readonly Color PEACH = new Color(255f / 255f, 229f / 255f, 180f / 255f);
        public static readonly Color PEAR = new Color(209f / 255f, 226f / 255f, 49f / 255f);
        public static readonly Color PERIWINKLE = new Color(204f / 255f, 204f / 255f, 255f / 255f);
        public static readonly Color PERSIAN_BLUE = new Color(28f / 255f, 57f / 255f, 187f / 255f);
        public static readonly Color PINK = new Color(253f / 255f, 108f / 255f, 158f / 255f);
        public static readonly Color PLUM = new Color(142f / 255f, 69f / 255f, 133f / 255f);
        public static readonly Color PRUSSIAN_BLUE = new Color(0, 49f / 255f, 83f / 255f);
        public static readonly Color PUCE = new Color(204f / 255f, 136f / 255f, 153f / 255f);
        public static readonly Color PURPLE = new Color(128f / 255f, 0, 128f / 255f);
        public static readonly Color RASPBERRY = new Color(227f / 255f, 11f / 255f, 92f / 255f);
        public static readonly Color RED = new Color(255f / 255f, 0, 0);
        public static readonly Color RED_VIOLET = new Color(199f / 255f, 21f / 255f, 133f / 255f);
        public static readonly Color ROSE = new Color(255f / 255f, 0, 127f / 255f);
        public static readonly Color RUBY = new Color(224f / 255f, 17f / 255f, 95f / 255f);
        public static readonly Color SALMON = new Color(250f / 255f, 128f / 255f, 114f / 255f);
        public static readonly Color SANGRIA = new Color(146f / 255f, 0, 10f / 255f);
        public static readonly Color SAPPHIRE = new Color(15f / 255f, 82f / 255f, 186f / 255f);
        public static readonly Color SCARLET = new Color(255f / 255f, 36f / 255f, 0);
        public static readonly Color SILVER = new Color(192f / 255f, 192f / 255f, 192f / 255f);
        public static readonly Color SLATE_GRAY = new Color(112f / 255f, 128f / 255f, 144f / 255f);
        public static readonly Color SPRING_BUD = new Color(167f / 255f, 252f / 255f, 0);
        public static readonly Color SPRING_GREEN = new Color(0, 255f / 255f, 127f / 255f);
        public static readonly Color TAN = new Color(210f / 255f, 180f / 255f, 140f / 255f);
        public static readonly Color TAUPE = new Color(72f / 255f, 60f / 255f, 50f / 255f);
        public static readonly Color TEAL = new Color(0, 128f / 255f, 128f / 255f);
        public static readonly Color TURQUOISE = new Color(64f / 255f, 224f / 255f, 208f / 255f);
        public static readonly Color ULTRAMARINE = new Color(63f / 255f, 0, 255f / 255f);
        public static readonly Color VIOLET = new Color(127f / 255f, 0, 255f / 255f);
        public static readonly Color VIRIDIAN = new Color(64f / 255f, 130f / 255f, 109f / 255f);
        public static readonly Color WHITE = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        public static readonly Color YELLOW = new Color(255f / 255f, 255f / 255f, 0);
    }
}