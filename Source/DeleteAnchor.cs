using Monocle;
using Microsoft.Xna.Framework;
using Celeste;
using Celeste.Mod.Entities;

namespace Celeste.Mod.AnchorHelper
{
    [CustomEntity("AnchorHelper/DeleteAnchor")]
    public class DeleteAnchor : Trigger
    {
        public DeleteAnchor(EntityData data, Vector2 offset)
            : base(data, offset)
        {
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            if (AnchorManager.Instance != null)
            {
                AnchorManager.Instance.RemoveSelf();
                AnchorManager.Instance = null;
            }
        }
    }
}
