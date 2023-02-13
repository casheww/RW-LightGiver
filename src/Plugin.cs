using BepInEx;
using System.Security.Permissions;
using UnityEngine;

#pragma warning disable CS0618 // Do not remove the following line.
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace LightGiver;

[BepInPlugin("casheww.lightgiver", "LightGiver", "0.3.0")]
public sealed class Plugin : BaseUnityPlugin {
    public void OnEnable() {
        On.RainWorld.OnModsInit += (self, orig) => {
            self(orig);

            if (initialised) return;

            MachineConnector.SetRegisteredOI(Info.Metadata.GUID, new LightGiverConfig(this));
            On.Player.Update += Player_Update;


            initialised = true;
        };
    }

    private void Player_Update(On.Player.orig_Update orig, Player self, bool eu) {
        orig(self, eu);

        if ((self.objectInStomach?.type == AbstractPhysicalObject.AbstractObjectType.Lantern || !LightGiverConfig.LanternRequired.Value) &&
            HologramLight.Needed(self) > 0.4375f &&
            self.room != null && !self.room.abstractRoom.shelter &&
            self.room.game.IsStorySession) {
                HologramLight.TryCreate(self);
        }
    }

    private void HologramLight_DrawSprites(On.HologramLight.orig_DrawSprites orig, HologramLight self,
            RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos) {
        orig(self, sLeaser, rCam, timeStacker, camPos);

        self.lightsource.HardSetRad(LightGiverConfig.LightRadiusFactor.Value);

        self.lightsource.color = LightGiverConfig.LightColor.Value;
        Vector3 v3 = RWCustom.Custom.RGB2HSL(LightGiverConfig.LightColor.Value);
        
    }

    public BepInEx.Logging.ManualLogSource PubLog { get; private set; }
    private bool initialised = false;
}
