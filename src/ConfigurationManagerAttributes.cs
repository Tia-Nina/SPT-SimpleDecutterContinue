// Shim of BepInEx.ConfigurationManager's attributes class.
// Vendored so the plugin can tag config entries with Order/etc. without a hard
// dependency on the ConfigurationManager plugin being installed.
namespace BepInEx.Configuration
{
#pragma warning disable 0649
    public sealed class ConfigurationManagerAttributes
    {
        public bool? ShowRangeAsPercent;
        public System.Action<ConfigEntryBase> CustomDrawer;
        public bool? Browsable;
        public bool? IsAdvanced;
        public int? Order;
        public bool? HideSettingName;
        public bool? HideDefaultButton;
        public bool? HideRange;
        public string EntryColor;
        public string CategoryColor;
        public object KeyValue;
        public string DispName;
        public string Description;
    }
#pragma warning restore 0649
}
