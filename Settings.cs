using BepInEx.Configuration;

namespace RainVolumeControl
{
    public static class Settings
    {
        public static ConfigEntry<float> RainVolumeMultiplier { get; private set; }

        public static void Init(ConfigFile config)
        {
            RainVolumeMultiplier = config.Bind(
                "General",
                "Rain Volume Multiplier",
                1f,
                new ConfigDescription(
                    "Multiplier applied to the rain volume (1 is same as vanilla)",
                    new AcceptableValueRange<float>(0f, 2f)
                )
            );
        }
    }
}