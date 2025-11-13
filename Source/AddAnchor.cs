using Monocle;
using Microsoft.Xna.Framework;
using Celeste;
using Celeste.Mod.Entities;

namespace Celeste.Mod.AnchorHelper
{
    [CustomEntity("AnchorHelper/AddAnchor")]
    public class AddAnchor : Trigger
    {
        public AddAnchor(EntityData data, Vector2 offset)
            : base(data, offset)
        {
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            if (AnchorManager.Instance == null)
            {
                var manager = new AnchorManager();
                SceneAs<Level>().Add(manager);
            }
        }
    }
}
