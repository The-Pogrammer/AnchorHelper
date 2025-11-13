using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AnchorHelper
{
    [CustomEntity("AnchorHelper/AddAnchor")]
    public class AddAnchor : Trigger
    {
        public AddAnchor(EntityData data, Vector2 offset) : base(data, offset) { }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);

            AnchorHelperModule.Session.ManagerDeleted = false;

            var manager = SceneAs<Level>().Tracker.GetEntity<AnchorManager>();
            if (manager == null)
            {
                SceneAs<Level>().Add(new AnchorManager());
            }
        }
    }
}
