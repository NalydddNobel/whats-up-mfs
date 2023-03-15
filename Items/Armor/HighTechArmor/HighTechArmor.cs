using Polarities.Items.Materials;
using Polarities.Items.Placeable.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Polarities.Items.Armor.HighTechArmor {
    public abstract class HiTechArmorBase : ModItem {
        public int DefaultEquipTexture;
        public int ElectrisEquipTexture;
        public int MagnetonEquipTexture;
        public abstract EquipType Equip { get; }
        public abstract ref int EquipField { get; }

        public override void SetStaticDefaults() {
            var equip = Equip;
            string equipName = equip.ToString();
            ElectrisEquipTexture = EquipLoader.AddEquipTexture(Mod, $"{Texture}_{equipName}_MaskA", equip);
            MagnetonEquipTexture = EquipLoader.AddEquipTexture(Mod, $"{Texture}_{equipName}_MaskB", equip);
        }

        public override void SetDefaults() {
            var loadedVersion = (HiTechArmorBase)ItemLoader.GetItem(Type);
            DefaultEquipTexture = EquipField;
            ElectrisEquipTexture = loadedVersion.ElectrisEquipTexture;
            MagnetonEquipTexture = loadedVersion.MagnetonEquipTexture;
        }

        public override void UpdateVanity(Player player) {
            int setType = player.Polarities().conductiveArmorSetType;

            EquipField = setType switch {
                1 => ElectrisEquipTexture,
                2 => MagnetonEquipTexture,
                _ => DefaultEquipTexture,
            };
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class ConductiveArmor : HiTechArmorBase {
        public override EquipType Equip => EquipType.Body;
        public override ref int EquipField => ref Item.bodySlot;

        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("High-Tech Armor");
            // Tooltip.SetDefault("15% increased damage"+"\n7% increased critical strike chance");
        }

        public override void SetDefaults() {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 14);
            Item.rare = ItemRarityID.Red;
            Item.defense = 20;
        }

        public override void UpdateEquip(Player player) {
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.GetCritChance(DamageClass.Generic) += 7;
            player.GetCritChance(DamageClass.Magic) += 7;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetCritChance(DamageClass.Throwing) += 7;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient<PolarizedBar>(18);
            recipe.AddIngredient<SmiteSoul>(25);
            recipe.AddIngredient(ItemID.Silk, 20);
            recipe.AddIngredient(ItemID.Wire, 30);
            recipe.Register();
        }

        //public override void UpdateVanity(Player player, EquipType type)
        //{
        //	player.GetModPlayer<PolaritiesPlayer>().customDrawcodeBody = Item.ModItem;
        //}

        //public bool Override()
        //      {
        //	return false;
        //      }

        //public void Render(Player player, PlayerDrawSet drawInfo, PlayerLayer layerInfo)
        //{
        //	if (layerInfo == PlayerLayer.Body)
        //	{
        //		// We don't want the glowmask to draw if the player is cloaked or dead
        //		if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        //		{
        //			return;
        //		}

        //		Player drawPlayer = drawInfo.drawPlayer;
        //		Mod mod = ModLoader.GetMod("Polarities");

        //		// Because we have a breastplate glowmask, and breastplates have different textures depending on if the player is male or female, we have to use two textures here
        //		string gender = "";
        //		if (!drawPlayer.Male)
        //		{
        //			gender = "Female";
        //		}

        //		// The texture we want to display on our player
        //		if (drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType != 0)
        //		{
        //			string glowType = drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType == 1 ? "A" : "B";
        //			Texture2D texture = mod.GetTexture("Items/Armor/ConductiveArmor/ConductiveArmor_Body_Mask" + glowType);

        //			float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        //			float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
        //			Vector2 origin = drawInfo.bodyVect;
        //			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
        //			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
        //			Color color = Color.White * drawPlayer.stealth;
        //			Rectangle frame = drawPlayer.bodyFrame;
        //			float rotation = drawPlayer.bodyRotation;
        //			SpriteEffects spriteEffects = drawInfo.playerEffect;

        //			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
        //			drawData.shader = drawInfo.cBody;
        //			Main.playerDrawData.Add(drawData);
        //		}
        //	}
        //	else
        //          {
        //		// We don't want the glowmask to draw if the player is cloaked or dead
        //		if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        //		{
        //			return;
        //		}

        //		Player drawPlayer = drawInfo.drawPlayer;
        //		Mod mod = ModLoader.GetMod("Polarities");

        //		// The texture we want to display on our player
        //		if (drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType != 0)
        //		{
        //			string glowType = drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType == 1 ? "A" : "B";
        //			Texture2D texture = mod.GetTexture("Items/Armor/ConductiveArmor/ConductiveArmor_Arms_Mask" + glowType);

        //			float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        //			float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
        //			Vector2 origin = drawInfo.bodyVect;
        //			Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
        //			float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
        //			Color color = Color.White * drawPlayer.stealth;
        //			Rectangle frame = drawPlayer.bodyFrame;
        //			float rotation = drawPlayer.bodyRotation;
        //			SpriteEffects spriteEffects = drawInfo.playerEffect;

        //			DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
        //			drawData.shader = drawInfo.cBody;
        //			Main.playerDrawData.Add(drawData);
        //		}
        //	}
        //}
    }

    [AutoloadEquip(EquipType.Legs)]
    public class ConductiveLeggings : HiTechArmorBase {
        public override EquipType Equip => EquipType.Legs;
        public override ref int EquipField => ref Item.legSlot;


        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("High-Tech Leggings");
            // Tooltip.SetDefault("10% increased critical strike chance" + "\n10% increased movement speed");
        }

        public override void SetDefaults() {
            base.SetDefaults();
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player) {
            player.moveSpeed += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient<PolarizedBar>(12);
            recipe.AddIngredient<SmiteSoul>(15);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.Register();
        }

        //public override void UpdateVanity(Player player, EquipType type)
        //{
        //	player.GetModPlayer<PolaritiesPlayer>().customDrawcodeLegs = Item.ModItem;
        //}

        //public bool Override()
        //{
        //	return false;
        //}

        //public void Render(Player player, PlayerDrawSet drawInfo, PlayerLayer layerInfo)
        //{// We don't want the glowmask to draw if the player is cloaked or dead
        //	if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        //	{
        //		return;
        //	}

        //	Player drawPlayer = drawInfo.drawPlayer;
        //	Mod mod = ModLoader.GetMod("Polarities");

        //	// The texture we want to display on our player
        //	if (drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType != 0)
        //	{
        //		string glowType = drawPlayer.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType == 1 ? "A" : "B";
        //		Texture2D texture = mod.GetTexture("Items/Armor/ConductiveArmor/ConductiveLeggings_Legs_Mask" + glowType);

        //		float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        //		float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.legFrame.Height / 2 + 4f;
        //		Vector2 origin = drawInfo.bodyVect;
        //		Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
        //		float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
        //		Color color = Color.White * drawPlayer.stealth;
        //		Rectangle frame = drawPlayer.legFrame;
        //		float rotation = drawPlayer.bodyRotation;
        //		SpriteEffects spriteEffects = drawInfo.playerEffect;

        //		DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
        //		drawData.shader = drawInfo.cLegs;
        //		Main.playerDrawData.Add(drawData);
        //	}
        //}
    }

    [AutoloadEquip(EquipType.Head)]
    public class ElectrisConductiveHelmet : ModItem/*, IVanityMaskRender*/ {
        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Electric Visor");
            // Tooltip.SetDefault("16% increased damage"+"\n6% increased crit chance");
        }

        public override void SetDefaults() {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Red;
            Item.defense = 15;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) {
            return body.type == ItemType<ConductiveArmor>() && legs.type == ItemType<ConductiveLeggings>();
        }

        public override void UpdateEquip(Player player) {
            player.GetDamage(DamageClass.Generic) += 0.16f;
            player.GetCritChance(DamageClass.Generic) += 6;
            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetCritChance(DamageClass.Ranged) += 6;
            player.GetCritChance(DamageClass.Throwing) += 6;

            player.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType = 1;
        }

        //public override void UpdateVanity(Player player)
        //{
        //	player.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType = 1;

        //	player.GetModPlayer<PolaritiesPlayer>().customDrawcodeHead = Item.ModItem;
        //}

        public override void UpdateArmorSet(Player player) {
            player.setBonus = "You emit rings of damaging sparks on striking enemies";

            player.GetModPlayer<PolaritiesPlayer>().electricSetBonus = true;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient<PolarizedBar>(5);
            recipe.AddIngredient<SmiteSoul>(30);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.Register();
        }

        //public bool Override()
        //{
        //	return false;
        //}

        //public void Render(Player player, PlayerDrawSet drawInfo, PlayerLayer layerInfo)
        //{
        //	// We don't want the glowmask to draw if the player is cloaked or dead
        //	if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        //	{
        //		return;
        //	}

        //	Player drawPlayer = drawInfo.drawPlayer;
        //	Mod mod = ModLoader.GetMod("Polarities");

        //	// The texture we want to display on our player
        //	Texture2D texture = mod.GetTexture("Items/Armor/ConductiveArmor/ElectrisConductiveHelmet_Head_MaskA");

        //	float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        //	float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
        //	Vector2 origin = drawInfo.bodyVect;
        //	Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
        //	float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
        //	Color color = Color.White * drawPlayer.stealth;
        //	Rectangle frame = drawPlayer.bodyFrame;
        //	float rotation = drawPlayer.bodyRotation;
        //	SpriteEffects spriteEffects = drawInfo.playerEffect;

        //	DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);
        //	drawData.shader = drawInfo.cHead;
        //	Main.playerDrawData.Add(drawData);
        //}
    }

    [AutoloadEquip(EquipType.Head)]
    public class MagnetonConductiveHelmet : ModItem/*, IVanityMaskRender*/
    {
        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Magnetic Helm");
            // Tooltip.SetDefault("6% increased damage"+"\n6% increased crit chance");
        }

        public override void SetDefaults() {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Red;
            Item.defense = 25;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) {
            return body.type == ItemType<ConductiveArmor>() && legs.type == ItemType<ConductiveLeggings>();
        }

        public override void UpdateEquip(Player player) {
            player.GetDamage(DamageClass.Generic) += 0.6f;
            player.GetCritChance(DamageClass.Generic) += 6;
            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetCritChance(DamageClass.Ranged) += 6;
            player.GetCritChance(DamageClass.Throwing) += 6;

            player.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType = 2;
        }

        public override void UpdateVanity(Player player) {
            //player.GetModPlayer<PolaritiesPlayer>().conductiveArmorSetType = 2;

            //player.GetModPlayer<PolaritiesPlayer>().customDrawcodeHead = Item.ModItem;
        }

        public override void UpdateArmorSet(Player player) {
            player.setBonus = "Grants a defense boost that gradually increases while attacking enemies" + "\nThis bonus is reset on getting hit";

            player.GetModPlayer<PolaritiesPlayer>().magneticSetBonus = true;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient<PolarizedBar>(25);
            recipe.AddIngredient<SmiteSoul>(5);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.Register();
        }

        //public bool Override()
        //{
        //	return false;
        //}

        //public void Render(Player player, PlayerDrawSet drawInfo, PlayerLayer layerInfo)
        //{
        //	// We don't want the glowmask to draw if the player is cloaked or dead
        //	if (drawInfo.shadow != 0f || drawInfo.drawPlayer.dead)
        //	{
        //		return;
        //	}

        //	Player drawPlayer = drawInfo.drawPlayer;
        //	Mod mod = ModLoader.GetMod("Polarities");

        //	// The texture we want to display on our player
        //	Texture2D texture = mod.GetTexture("Items/Armor/ConductiveArmor/MagnetonConductiveHelmet_Head_MaskB");

        //	float drawX = (int)drawInfo.Position.X + drawPlayer.width / 2;
        //	float drawY = (int)drawInfo.Position.Y + drawPlayer.height - drawPlayer.bodyFrame.Height / 2 + 4f;
        //	Vector2 origin = drawInfo.bodyVect;
        //	Vector2 position = new Vector2(drawX, drawY) + drawPlayer.bodyPosition - Main.screenPosition;
        //	float alpha = (255 - drawPlayer.immuneAlpha) / 255f;
        //	Color color = Color.White * drawPlayer.stealth;
        //	Rectangle frame = drawPlayer.bodyFrame;
        //	float rotation = drawPlayer.bodyRotation;
        //	SpriteEffects spriteEffects = drawInfo.playerEffect;

        //	DrawData drawData = new DrawData(texture, position, frame, color * alpha, rotation, origin, 1f, spriteEffects, 0);

        //	drawData.shader = drawInfo.cHead;
        //	Main.playerDrawData.Add(drawData);
        //}
    }
}

namespace Polarities {
    public partial class PolaritiesPlayer {
        public int conductiveArmorSetType;
        public bool electricSetBonus;
        public bool magneticSetBonus;

        public void ResetEffects_HighTechArmor() {
            conductiveArmorSetType = 0;
            electricSetBonus = false;
            magneticSetBonus = false;
        }
    }
}