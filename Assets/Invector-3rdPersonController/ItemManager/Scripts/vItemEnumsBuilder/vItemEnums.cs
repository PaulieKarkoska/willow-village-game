using System.ComponentModel;
namespace Invector.vItemManager {
     public enum vItemType {
       [Description("")] Consumable=0,
       [Description("Melee")] MeleeWeapon=1,
       [Description("")] Money=2,
       [Description("")] Builder=3,
       [Description("")] Defense=4
     }
     public enum vItemAttributes {
       [Description("")] Health=0,
       [Description("")] Stamina=1,
       [Description("<i>Damage</i> : <color=red>(VALUE)</color>")] Damage=2,
       [Description("")] StaminaCost=3,
       [Description("")] DefenseRate=4,
       [Description("")] DefenseRange=5,
       [Description("")] MaxHealth=6,
       [Description("")] MaxStamina=7
     }
}
