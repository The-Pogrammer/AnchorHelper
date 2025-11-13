using Celeste.Mod;
using Microsoft.Xna.Framework.Input;

namespace Celeste.Mod.AnchorHelper;

public class AnchorHelperModuleSettings : EverestModuleSettings
{
    [DefaultButtonBinding(
        button: Buttons.RightStick,
        key: Keys.V
    )]
    public ButtonBinding AnchorAndRecall { get; set; }
}
