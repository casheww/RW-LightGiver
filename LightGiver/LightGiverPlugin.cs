using BepInEx;

namespace LightGiver
{
    [BepInPlugin("casheww.light_giver", "LightGiver", "0.0.1.0")]
    public class LightGiverPlugin : BaseUnityPlugin
    {
        private void OnEnable()
        {
            On.Player.Update += Player_Update;
        }

        private void OnDisable()
        {
            On.Player.Update -= Player_Update;
        }

        private void Player_Update(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);

            if (self.objectInStomach != null && self.objectInStomach.type == AbstractPhysicalObject.AbstractObjectType.Lantern &&
                HologramLight.Needed(self) > 0.4375f &&
                self.room != null && !self.room.abstractRoom.shelter && !self.room.abstractRoom.gate &&
                self.room.game.IsStorySession)
            {
                HologramLight.TryCreate(self);
            }
        }
    }
}
