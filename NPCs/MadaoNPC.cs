using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Hardmedcore.NPCs
{
    // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class MadaoNPC : ModNPC
    {
        private bool chat2 = false;
        private bool chat2prev = false;
        public override string Texture => "Hardmedcore/NPCs/MADAO";
        public override string[] AltTextures => new[] { "Hardmedcore/NPCs/MADAO" };    
        public override bool Autoload(ref string name)
        {
            name = "Madao";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Madao");
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 5;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            //npc.homeless = true;
            animationType = NPCID.Merchant;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = npc.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("Sparkle"));
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            bool returnVal = false;
            if (money > 10)
            {
                return true;

            }

            return returnVal;
        }

        public override string TownNPCName()
        {
            string[] nameList = { "Jibin", "Jay", "Hasegawa", "Jibin", "Jay", "Jacob", "Jibin", "Jacob" };
            int rand = WorldGen.genRand.Next(nameList.Length-1);
            return nameList[rand];
        }



        public override string GetChat()
        {
           
            string[] speechList = {
                "Nani?!?", 
                "OIII",
                "OHHHH nooOOOOo",
                "KAAA MEE HAAA MEEE...",
                "Got to eat when you can",
                "Please spare me a coin.",
                "Let's go to the pachinko!",
                "Do you know if Heidi exists?",
                "Give me sake!",
                "...",
                "It's me your brother",
                "Dio",
                "Do you have rope?",
                "I'm hungry",
                "I need a home",
                "This place is spooky",
                "I lost my job,",
                "Have your tried dirt? It's pretty good!",
                "I found 1 copper a few days ago",
                "I used to be an adventurer like you until I lost my job",
                "Hi",
                "Ugghhh",
                "I lost my wallet at a tower once!",
                "Have you tried slime? It's a delicacy",
                "k.",
                "Have you tried eyeballs? It's pretty gooey.",
                "Alms for the poor",
                "Have you seen my cardboard box?",
                "I'm a max level monkey hunter!",
                "My wife left me",
                "Ay",
                "I'll give you dirt for 1 copper!",
                "I saw an alien the other day!",
                "To be continued...", 
                "Have you been to the dirt district often? What am I saying of course you haven't",
                "Sandstorm is coming!",
                "I made an armstrong cannon",
                "I got fired from a justaway factory",
                "",
                "Ahhh",
                "Let's swap pants!",
                "It's 2020!",
                "I've been practicing my naruto run!",
                ""
            };
            int rand = Main.rand.Next(speechList.Length);
            if(chat2 == true)
            {
                chat2 = false;
                return "You got " + Main.LocalPlayer.GetModPlayer<PlayerMods.PlayerMod>().reviveable.ToString() + " lives!";
            }
            else
            {
                return speechList[rand];
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Check fortune";
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.CopperCoin);
            shop.item[nextSlot].shopCustomPrice = 2;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.MagicalPumpkinSeed);
            shop.item[nextSlot].shopCustomPrice = 10000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.GoodieBag);
            shop.item[nextSlot].shopCustomPrice = 10000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.Present);
            shop.item[nextSlot].shopCustomPrice = 10000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.MolotovCocktail);
            shop.item[nextSlot].shopCustomPrice = 2500;
            nextSlot++;
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = firstButton;
            }
            else
            {
                chat2 = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

    }
}
