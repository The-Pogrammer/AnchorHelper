using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AnchorHelper
{
    [CustomEntity("AnchorHelper/DeleteAnchor")]
    public class DeleteAnchor : Trigger
    {
        public DeleteAnchor(EntityData data, Vector2 offset) : base(data, offset) { }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            AnchorHelperModule.Session.ManagerDeleted = true;

            var manager = SceneAs<Level>().Tracker.GetEntity<AnchorManager>();
            if (manager != null)
            {
                manager.Visible = false;
                manager.AnchorPosition = null;
            }
        }
    }
}
