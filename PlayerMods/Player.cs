using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
namespace Hardmedcore.PlayerMods
{
    public class PlayerMod : ModPlayer
    {

        int maxHPLoss = 20;
        int maxManaLoss = 10;
        public int invLossChance = 2;
        public int armorLossChance = 5;
        public int accLossChance = 4;
        public int statLossChance = 1;
        public int reviveable = 0;
        public List<bool> reviveKey = new List<bool>()
        {
          false, //NPC.downedBoss1,
          false, //NPC.downedBoss2,
          false, //NPC.downedBoss3,
          false, //NPC.downedQueenBee,
          false, //NPC.downedSlimeKing,
          false, //NPC.downedGoblins,
          false, //NPC.downedFrost,
          false, //NPC.downedPirates,
          false, //NPC.downedClown,
          false, //NPC.downedPlantBoss,
          false, //NPC.downedGolemBoss,
          false, //NPC.downedMartians,
          false, //NPC.downedFishron,
          false, //NPC.downedHalloweenTree,
          false, //NPC.downedHalloweenKing,
          false, //NPC.downedChristmasIceQueen,
          false, //NPC.downedChristmasTree,
          false, //NPC.downedChristmasSantank,
          false, //NPC.downedAncientCultist,
          false, //NPC.downedMoonlord,
          false, //NPC.downedTowerSolar,
          false, //NPC.downedTowerVortex,
          false, //NPC.downedTowerNebula,
          false, //NPC.downedTowerStardust,
          false, //NPC.downedMechBoss1,
          false, //NPC.downedMechBoss2,
          false, //NPC.downedMechBoss3,
          false //NPC.downedTowers
        };
        public bool[] revivePrevKey =
        {
           NPC.downedBoss1,
           NPC.downedBoss2,
           NPC.downedBoss3,
           NPC.downedQueenBee,
           NPC.downedSlimeKing,
           NPC.downedGoblins,
           NPC.downedFrost,
           NPC.downedPirates,
           NPC.downedClown,
           NPC.downedPlantBoss,
           NPC.downedGolemBoss,
           NPC.downedMartians,
           NPC.downedFishron,
           NPC.downedHalloweenTree,
           NPC.downedHalloweenKing,
           NPC.downedChristmasIceQueen,
           NPC.downedChristmasTree,
           NPC.downedChristmasSantank,
           NPC.downedAncientCultist,
           NPC.downedMoonlord,
           NPC.downedTowerSolar,
           NPC.downedTowerVortex,
           NPC.downedTowerNebula,
           NPC.downedTowerStardust,
           NPC.downedMechBoss1,
           NPC.downedMechBoss2,
           NPC.downedMechBoss3,
           NPC.downedTowers
        };
        public override void OnRespawn(Player player)
        {
            /*RemoveItems(player);*/
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            /*LoseMaxStats(player);*/
            UpdateReviveCount(player);
            CheckReviveCount(player);
        }

        public override TagCompound Save()
        {
            TagCompound saveData = new TagCompound();
            saveData.Add("bossKilled", reviveKey);
            saveData.Add("reviveCount", reviveable);
            saveData.Add("isDead", player.dead);
            saveData.Add("isGhost", player.ghost);
            return saveData;
        }

        public override void Load(TagCompound tag)
        {
            reviveKey = tag.Get<List<bool>>("bossKilled");
            reviveable = tag.GetInt("reviveCount");
            player.dead = tag.GetBool("isDead");
            player.ghost = tag.GetBool("isGhost");
        }

        public override void OnEnterWorld(Player player)
        {
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write((byte)player.whoAmI);
            packet.Write(reviveable);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            PlayerMod clone = clientPlayer as PlayerMod;
            if (clone.reviveable != reviveable)
            {
                var packet = mod.GetPacket();
                packet.Write((byte)player.whoAmI);
                packet.Write(reviveable);
                packet.Send();
            }
        }

        private void RemoveItems(Player player)
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
            /*clear armor*/
            for (int i = 0; i < player.armor.Length; i++)
            {
                int num = rand.Next(100);
                if (num % armorLossChance == 0)
                {
                    player.armor[i] = new Item();
                }
            }
            /*clear misc*/
            for (int i = 0; i < player.miscEquips.Length; i++)
            {
                int num = rand.Next(100);
                if (num % accLossChance == 0)
                {
                    player.miscEquips[i] = new Item();
                }
            }

            /*add starter item to prevent hardlock*/
            int[] startIds = { 3509, 3506 };
            for (int i = 0; i < startIds.Length; i++)
            {
                int idIndex = startIds[i];
                player.PutItemInInventory(idIndex);
            }
        }

        private void LoseMaxStats(Player player)
        {
            System.Random rand = new System.Random();
            int num = rand.Next(100);
            if (num % statLossChance == 0)
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

        private void CheckReviveCount(Player player)
        {
            if (reviveable > 0)
            {
                reviveable--;
            }
            else
            {
                if(player == Main.LocalPlayer)
                {
                    /*player.KillMeForGood();*/
                }
                player.dead = true;
                player.ghost = true;
                reviveable = 0;
            }
        }

        private void UpdateReviveCount(Player player)
        {
            revivePrevKey[0] = NPC.downedBoss1;
            revivePrevKey[1] = NPC.downedBoss2;
            revivePrevKey[2] = NPC.downedBoss3;
            revivePrevKey[3] = NPC.downedQueenBee;
            revivePrevKey[4] = NPC.downedSlimeKing;
            revivePrevKey[5] = NPC.downedGoblins;
            revivePrevKey[6] = NPC.downedFrost;
            revivePrevKey[7] = NPC.downedPirates;
            revivePrevKey[8] = NPC.downedClown;
            revivePrevKey[9] = NPC.downedPlantBoss;
            revivePrevKey[10] = NPC.downedGolemBoss;
            revivePrevKey[11] = NPC.downedMartians;
            revivePrevKey[12] = NPC.downedFishron;
            revivePrevKey[13] = NPC.downedHalloweenTree;
            revivePrevKey[14] = NPC.downedHalloweenKing;
            revivePrevKey[15] = NPC.downedChristmasIceQueen;
            revivePrevKey[16] = NPC.downedChristmasTree;
            revivePrevKey[17] = NPC.downedChristmasSantank;
            revivePrevKey[18] = NPC.downedAncientCultist;
            revivePrevKey[19] = NPC.downedMoonlord;
            revivePrevKey[20] = NPC.downedTowerSolar;
            revivePrevKey[21] = NPC.downedTowerVortex;
            revivePrevKey[22] = NPC.downedTowerNebula;
            revivePrevKey[23] = NPC.downedTowerStardust;
            revivePrevKey[24] = NPC.downedMechBoss1;
            revivePrevKey[25] = NPC.downedMechBoss2;
            revivePrevKey[26] = NPC.downedMechBoss3;
            revivePrevKey[27] = NPC.downedTowers;

            for (int i = 0; i < revivePrevKey.Length; i++)
            {
                if((revivePrevKey[i] == true) && (reviveKey[i] == false))
                {
                    reviveable++;
                }
            }

            reviveKey[0] = NPC.downedBoss1;
            reviveKey[1] = NPC.downedBoss2;
            reviveKey[2] = NPC.downedBoss3;
            reviveKey[3] = NPC.downedQueenBee;
            reviveKey[4] = NPC.downedSlimeKing;
            reviveKey[5] = NPC.downedGoblins;
            reviveKey[6] = NPC.downedFrost;
            reviveKey[7] = NPC.downedPirates;
            reviveKey[8] = NPC.downedClown;
            reviveKey[9] = NPC.downedPlantBoss;
            reviveKey[10] = NPC.downedGolemBoss;
            reviveKey[11] = NPC.downedMartians;
            reviveKey[12] = NPC.downedFishron;
            reviveKey[13] = NPC.downedHalloweenTree;
            reviveKey[14] = NPC.downedHalloweenKing;
            reviveKey[15] = NPC.downedChristmasIceQueen;
            reviveKey[16] = NPC.downedChristmasTree;
            reviveKey[17] = NPC.downedChristmasSantank;
            reviveKey[18] = NPC.downedAncientCultist;
            reviveKey[19] = NPC.downedMoonlord;
            reviveKey[20] = NPC.downedTowerSolar;
            reviveKey[21] = NPC.downedTowerVortex;
            reviveKey[22] = NPC.downedTowerNebula;
            reviveKey[23] = NPC.downedTowerStardust;
            reviveKey[24] = NPC.downedMechBoss1;
            reviveKey[25] = NPC.downedMechBoss2;
            reviveKey[26] = NPC.downedMechBoss3;
            reviveKey[27] = NPC.downedTowers;
        } 
    }
}