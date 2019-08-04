using Terraria.ID;
using Terraria.ModLoader;

namespace Hardmedcore.Items
{
    public class BrokenSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("It's broken.");
        }

        public override void SetDefaults()
        {
            item.damage = 1;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 20;
            item.value = 1;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 999);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}