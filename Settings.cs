using BepInEx.Configuration;

namespace RainVolumeControl
{
    public static class Settings
    {
        public static ConfigEntry<float> RainVolumeMultiplier { get; private set; }
        public static ConfigEntry<float> BTRVolumeMultiplier { get; private set; }
        public static ConfigEntry<float> AirdropVolumeMultiplier { get; private set; }
        
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
            BTRVolumeMultiplier = config.Bind(
                "General",
                "BTR Volume Multiplier",
                1f,
                new ConfigDescription(
                    "Multiplier applied to BTR movement transition volume (1 is same as vanilla)",
                    new AcceptableValueRange<float>(0f, 2f)
                )
            );
            AirdropVolumeMultiplier = config.Bind(
                "General",
                "Aidrop Volume Multiplier",
                1f,
                new ConfigDescription(
                    "Multiplier applied to the airdrop volume (1 is same as vanilla)",
                    new AcceptableValueRange<float>(0f, 2f)
                )
            );
        }
    }
}
