using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.DataStructures;

namespace Hardmedcore.Prefix
{
    class ItemLossChance : ModPrefix
    {

        public override float RollChance(Item item)
        {
            return 0.1f;
        }

        public override bool CanRoll(Item item)
        {
            return this.RollChance(item) > 0f;
        }

        /// <summary>
        /// Allows you to set the prefix's name/translations and to set its category.
        /// </summary>
        public override void SetDefaults()
        {

        }

        /// <summary>
        /// Sets the stat changes for this prefix. If data is not already pre-stored, it is best to store custom data changes to some static variables.
        /// </summary>
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
        }

        /// <summary>
        /// Validates whether this prefix with the custom data stats set from SetStats is allowed on the given item.
        /// It is not allowed if one of the stat changes do not cause any change (eg. percentage being too small to make a difference).
        /// </summary>
        public override void ValidateItem(Item item, ref bool invalid)
        {
        }

        /// <summary>
        /// Applies the custom data stats set in SetStats to the given item.
        /// </summary>
        /// <param name="item"></param>
        public override void Apply(Item item)
        {
        }

        /// <summary>
        /// Allows you to modify the sell price of the item based on the prefix or changes in custom data stats. This also influences the item's rarity.
        /// </summary>
        public override void ModifyValue(ref float valueMult)
        {
        }
    }
}
