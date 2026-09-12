using BepInEx.Configuration;
using System;


namespace SimpleDeclutterContinue
{
    internal class Settings
    {
        public static ConfigEntry<bool> declutterEnabledConfig;
        public static ConfigEntry<bool> declutterGarbageEnabledConfig;
        public static ConfigEntry<bool> declutterHeapsEnabledConfig;
        public static ConfigEntry<bool> declutterSpentCartridgesEnabledConfig;
        public static ConfigEntry<bool> declutterFakeFoodEnabledConfig;
        public static ConfigEntry<bool> declutterDecalsEnabledConfig;
        public static ConfigEntry<bool> declutterPuddlesEnabledConfig;
        public static ConfigEntry<bool> declutterShardsEnabledConfig;
        public static ConfigEntry<float> declutterScaleOffsetConfig;
        public static ConfigEntry<bool> EnableFactory;
        public static ConfigEntry<bool> EnableLighthouse;
        public static ConfigEntry<bool> EnableShoreline;
        public static ConfigEntry<bool> EnableReserve;
        public static ConfigEntry<bool> EnableWoods;
        public static ConfigEntry<bool> EnableInterchange;
        public static ConfigEntry<bool> EnableCustoms;
        public static ConfigEntry<bool> EnableStreets;
        public static ConfigEntry<bool> EnableGroundZero;
        public static ConfigEntry<bool> EnableLab;
        public static ConfigEntry<bool> framesaverPotatoShadow;

        private const string CategoryGeneral = "1 - General";
        private const string CategoryClutter = "2 - Clutter Types";
        private const string CategoryFramesaver = "3 - Frame Savers";
        private const string CategoryMaps = "4 - Maps";

        private static int _order = 100;

        private static ConfigurationManagerAttributes Attr(bool advanced = false)
        {
            return new ConfigurationManagerAttributes { Order = _order--, IsAdvanced = advanced };
        }

        public static void Init(ConfigFile Config)
        {
            declutterEnabledConfig = Config.Bind(CategoryGeneral, "Enable De-Clutterer", true,
                new ConfigDescription(
                    "Master switch of the mod. When enabled, clutter matching the rules in '2 - Clutter Types' is hidden " +
                    "shortly after a raid starts, but only on the maps enabled in '4 - Maps'. " +
                    "You can also toggle this during a raid to hide or restore clutter on the fly.",
                    null, Attr()));

            declutterScaleOffsetConfig = Config.Bind(CategoryGeneral, "Removal Size Scaler", 1f,
                new ConfigDescription(
                    "Controls how large a flat object may be before it is kept. An object is only removed when its mesh " +
                    "height (Y size) is below 2 x this value, in meters. 1.0 = default. Increase to also remove bigger flat " +
                    "debris, decrease to only remove the smallest litter. Changing it takes effect the next time clutter is " +
                    "applied (toggle the master switch in-raid to re-apply).",
                    new AcceptableValueRange<float>(0.5f, 2f), Attr()));

            declutterGarbageEnabledConfig = Config.Bind(CategoryClutter, "Garbage & Litter", true,
                new ConfigDescription(
                    "Hides small ground litter identified by internal object names: paper sheets, cardboard, trays, " +
                    "styrofoam, plastic bags, trash bags, bottles, books, folders, magazines, posters, pans, cables, " +
                    "leaves and similar small props.",
                    null, Attr()));

            declutterHeapsEnabledConfig = Config.Bind(CategoryClutter, "Heaps & Piles", true,
                new ConfigDescription(
                    "Hides larger debris: trash piles, crushed concrete, brick piles, rubble, scattered debris, " +
                    "broken tiles and floor sets.",
                    null, Attr()));

            declutterSpentCartridgesEnabledConfig = Config.Bind(CategoryClutter, "Spent Cartridges", true,
                new ConfigDescription(
                    "Hides pre-placed spent ammunition casings and shotgun shells lying on the ground. " +
                    "Only map-generated props are affected - cartridges fired by players are not touched.",
                    null, Attr()));

            declutterFakeFoodEnabledConfig = Config.Bind(CategoryClutter, "Fake Food", true,
                new ConfigDescription(
                    "Hides decorative, non-lootable food props: cans, jars, juice cartons, crackers, chocolate, biscuits, " +
                    "oat flakes, plastic cups and packaged goods. Real lootable items are never touched.",
                    null, Attr()));

            declutterDecalsEnabledConfig = Config.Bind(CategoryClutter, "Decal De-Clutter", true,
                new ConfigDescription(
                    "Hides static deferred decals: blood splatter, graffiti, dirt and sand marks, drips and similar " +
                    "ground/wall projections. The game's decal renderer buffers are rebuilt after hiding, so this also " +
                    "works on SPT 4.1.",
                    null, Attr()));

            declutterPuddlesEnabledConfig = Config.Bind(CategoryClutter, "Puddles", true,
                new ConfigDescription(
                    "Hides fake reflective water puddles (simple quads pretending to be water on the ground).",
                    null, Attr()));

            declutterShardsEnabledConfig = Config.Bind(CategoryClutter, "Glass & Tile Shards", true,
                new ConfigDescription(
                    "Hides broken glass and ceramic tile shards, including the crunchy pieces you step on.",
                    null, Attr()));

            framesaverPotatoShadow = Config.Bind(CategoryFramesaver, "Disable All Shadows", false,
                new ConfigDescription(
                    "Frame saver: sets Unity's global shadow quality to Disabled for the duration of the raid, so " +
                    "characters, weapons and props cast no shadows. Your original shadow quality is restored when you " +
                    "toggle this off or leave the raid.",
                    null, Attr()));

            EnableFactory = Config.Bind(CategoryMaps, "Factory", true,
                new ConfigDescription("Apply the De-Clutterer on Factory (day and night).", null, Attr()));
            EnableLighthouse = Config.Bind(CategoryMaps, "Lighthouse", true,
                new ConfigDescription("Apply the De-Clutterer on Lighthouse.", null, Attr()));
            EnableShoreline = Config.Bind(CategoryMaps, "Shoreline", true,
                new ConfigDescription("Apply the De-Clutterer on Shoreline (and Resort).", null, Attr()));
            EnableReserve = Config.Bind(CategoryMaps, "Reserve", true,
                new ConfigDescription("Apply the De-Clutterer on Reserve.", null, Attr()));
            EnableWoods = Config.Bind(CategoryMaps, "Woods", true,
                new ConfigDescription("Apply the De-Clutterer on Woods.", null, Attr()));
            EnableInterchange = Config.Bind(CategoryMaps, "Interchange", true,
                new ConfigDescription("Apply the De-Clutterer on Interchange.", null, Attr()));
            EnableCustoms = Config.Bind(CategoryMaps, "Customs", true,
                new ConfigDescription("Apply the De-Clutterer on Customs.", null, Attr()));
            EnableStreets = Config.Bind(CategoryMaps, "Streets", true,
                new ConfigDescription("Apply the De-Clutterer on Streets of Tarkov.", null, Attr()));
            EnableGroundZero = Config.Bind(CategoryMaps, "Ground Zero", true,
                new ConfigDescription("Apply the De-Clutterer on Ground Zero (normal and high versions).", null, Attr()));
            EnableLab = Config.Bind(CategoryMaps, "The Lab", false,
                new ConfigDescription("Apply the De-Clutterer on The Lab. Disabled by default because the Lab relies " +
                                      "heavily on small props for cover and readability.", null, Attr()));
        }
    }
}
