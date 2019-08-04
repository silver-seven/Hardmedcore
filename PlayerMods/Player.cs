using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.DataStructures;

namespace Hardmedcore.PlayerMods
{
    public class PlayerMod : ModPlayer
    {
        int maxHPLoss = 20;
        int maxManaLoss = 10;
        int invLossChance = 2;
        int armorLossChance = 5;
        int accLossChance = 4;
        public override void OnRespawn(Player player)
        {
            System.Random rand = new System.Random();

            /*clear inventory*/
            for (int i = 0; i < player.inventory.Length; i++)  
            {
                int num = rand.Next(100);
                if (num % invLossChance == 0) 
                {
                    player.inventory[i] = new Item();
                }
            }

            for (int i = 0; i < player.armor.Length; i++)
            {
                int num = rand.Next(100);
                if (num % armorLossChance == 0)
                {
                    player.armor[i] = new Item();
                }
            }

            for (int i = 0; i < player.miscEquips.Length; i++) 
            {
                int num = rand.Next(100);
                if (num % accLossChance == 0)
                {
                    player.miscEquips[i] = new Item();
                }
            }

            /*add starter item to prevent hardlock*/
            int[] startIds = {3509, 3506};
            for(int i = 0; i < startIds.Length; i++)
            {
                Item item = new Item();
                int idIndex = startIds[i];
                item.SetDefaults(idIndex);
                player.inventory[i] = item;
            }
        }

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            /*subtract max health*/
            if (player.statLifeMax >= maxHPLoss)
            {
                player.statLifeMax = player.statLifeMax - maxHPLoss;
            }

            if (player.statManaMax >= maxManaLoss)
            {
                player.statManaMax = player.statManaMax - maxManaLoss;
            }

            if (player.statManaMax < 0)
            {
                player.statManaMax = 0;
            }

            if (player.statLifeMax < maxHPLoss)
            {
                player.KillMeForGood();
                player.dead = true;
                player.ghost = true;
            }
        }
    }
}