using Celeste;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AnchorHelper
{

    [CustomEntity("AnchorHelper/AnchorManager")]
    [Tracked(true)]
    public class AnchorManager : Entity
    {
        private Image anchorImage;

        public bool HasAnchor => AnchorHelperModule.Session.AnchorPosition != null;

        public Vector2? AnchorPosition
        {
            get => AnchorHelperModule.Session.AnchorPosition;
            set => AnchorHelperModule.Session.AnchorPosition = value;
        }

        public AnchorManager() : base(Vector2.Zero)
        {
            Tag = Tags.Global;
            Depth = Depths.Top;
            Collidable = false;
            Visible = false;

            anchorImage = new Image(GFX.Game["anchor"]);
            anchorImage.CenterOrigin();
            Add(anchorImage);
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);

            if (AnchorPosition is Vector2 pos)
            {
                anchorImage.Position = pos;
                Visible = true;
            }
        }

        public override void Update()
        {
            base.Update();

            if (AnchorHelperModule.Session.ManagerDeleted)
                return;

            Player player = SceneAs<Level>().Tracker.GetEntity<Player>();
            if (player == null) return;

            if (AnchorHelperModule.Settings.AnchorAndRecall.Pressed)
            {
                AnchorHelperModule.Settings.AnchorAndRecall.ConsumePress();

                if (HasAnchor)
                {
                    Recall(player);
                    AnchorPosition = null;
                    Visible = false;
                }
                else if (player.OnSafeGround)
                {
                    PlaceAnchor(player);
                }
            }
        }

        public void PlaceAnchor(Player player)
        {
            Vector2 bottomCenter = player.Position + new Vector2(player.Width / 2f, player.Height);
            AnchorPosition = bottomCenter;

            anchorImage.Position = player.Center;
            Visible = true;
        }

        public void Recall(Player player)
        {
            if (AnchorPosition is Vector2 feetPos)
            {
                player.Position = feetPos - new Vector2(player.Width / 2f, player.Height);

                player.Hair.MoveHairBy(Vector2.Zero);
                player.Leader.PastPoints.Clear();
                for (int i = 0; i < 3; i++)
                {
                    player.Leader.PastPoints.Add(player.TopCenter + Vector2.UnitX * -16 * ((int)player.Facing));
                }
            }
        }

    }
}
