using BepInEx;
using System.Security.Permissions;

#pragma warning disable CS0618 // Do not remove the following line.
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace LightGiver;

[BepInPlugin("casheww.lightgiver", "LightGiver", "0.2.0")]
sealed class Plugin : BaseUnityPlugin {
    public void OnEnable() {
        On.RainWorld.OnModsInit += (self, orig) => {
            self(orig);

            if (initialised) return;
            On.Player.Update += Player_Update;
            initialised = true;
        };
    }

    private void Player_Update(On.Player.orig_Update orig, Player self, bool eu) {
        orig(self, eu);

        if (self.objectInStomach?.type == AbstractPhysicalObject.AbstractObjectType.Lantern &&
            HologramLight.Needed(self) > 0.4375f &&
            self.room != null && !self.room.abstractRoom.shelter &&
            self.room.game.IsStorySession) {
                HologramLight.TryCreate(self);
        }
    }

    public BepInEx.Logging.ManualLogSource PubLog { get; private set; }
    private bool initialised = false;
}
