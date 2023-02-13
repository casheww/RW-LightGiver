using Menu.Remix.MixedUI;
using UnityEngine;

namespace LightGiver;

public sealed class LightGiverConfig : OptionInterface {
    public LightGiverConfig(Plugin plugin) {
        this.plugin = plugin;
        LanternRequired = config.Bind<bool>("lanternRequired", true);
        LightRadiusFactor = config.Bind<float>("lightRadius", 1f);
        LightColor = config.Bind<Color>("color", new Color(0.87f, 0.72f, 0.35f));
    }

    public override void Initialize() {
        base.Initialize();

        Tabs = new OpTab[1] {new OpTab(this)};
        OpTab t = Tabs[0];

        // metadata
        t.AddItems(
            new OpLabel(new Vector2(150f, 550f), new Vector2(300f, 30f), plugin.Info.Metadata.Name, bigText:true),
            new OpLabel(new Vector2(150f, 520f), new Vector2(300f, 30f), plugin.Info.Metadata.Version.ToString())
        );

        t.AddItems(
            new OpCheckBox(LanternRequired, new Vector2(50f, 470f)),
            new OpLabel(75f, 475f, "Lantern required for guide light encouragement?"),
            new OpFloatSlider(LightRadiusFactor, new Vector2(50f, 440f), 100, 5),
            new OpLabel(100f, 440f, "Guide light radius multiplier"),
            new OpColorPicker(LightColor, new Vector2(50f, 260f))
        );
    }


    Plugin plugin;

    public static Configurable<bool> LanternRequired { get; private set; }
    public static Configurable<float> LightRadiusFactor { get; private set; }
    public static Configurable<Color> LightColor { get; private set; }
}
