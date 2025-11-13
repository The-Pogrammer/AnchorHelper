using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.AnchorHelper
{
    [CustomEntity("AnchorHelper/AnchorManager")]
    [Tracked(true)]
    public class AnchorManager : Entity
    {
        public static AnchorManager Instance;

        public Vector2? AnchorPosition;
        private Image anchorImage;

        public bool HasAnchor => AnchorPosition != null;

        public AnchorManager() : base(Vector2.Zero)
        {
            Tag = Tags.Global;
            Depth = Depths.Top;
            Collidable = false;
            Visible = false;
            Instance = this;

            // Loads a static image from Graphics/Atlases/Gameplay/AnchorHelper/anchor.png
            anchorImage = new Image(GFX.Game["anchor"]);
            anchorImage.CenterOrigin();
            Add(anchorImage);
        }

        public override void Update()
        {
            base.Update();

            Player player = SceneAs<Level>().Tracker.GetEntity<Player>();
            if (AnchorHelperModule.Settings.AnchorAndRecall.Pressed && player != null)
            {
                AnchorHelperModule.Settings.AnchorAndRecall.ConsumePress();
                if (HasAnchor)
                {
                    Recall(player);
                    AnchorPosition = null;
                }
                else
                {
                    PlaceAnchor(player);
                }
                Visible = HasAnchor;
            }
        }

        public override void Added(Scene scene)
        {
            base.Added(scene);

            if (Instance != null && Instance != this)
            {
                RemoveSelf();
                return;
            }

            Instance = this;
        }

        public override void Removed(Scene scene)
        {
            base.Removed(scene);
            if (Instance == this)
                Instance = null;
        }

        public void PlaceAnchor(Player player)
        {
            AnchorPosition = player.Position; // bottom-middle of hitbox
            Position = player.Center;
            Logger.Log(AnchorHelperModule.LoggerTag, "anchor placed");
        }


        public void Recall(Player player)
        {
            if (AnchorPosition is Vector2 anchor) {
                player.Position = anchor;
                player.Hair.MoveHairBy(Position);
            }
        }


        public override void SceneEnd(Scene scene)
        {
            base.SceneEnd(scene);
            Instance = null;
        }

    }
}
