﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Polarities.NPCs;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Audio;

namespace Polarities.Items.Weapons.Ranged.Atlatls
{
	public class Ratlatl : AtlatlBase
	{
		public override Vector2[] ShotDistances => new Vector2[] { new Vector2(30) };
		public override float BaseShotDistance => 30;

		public override void SetDefaults()
		{
			Item.SetWeaponValues(16, 6, 0);
			Item.DamageType = DamageClass.Ranged;

			Item.width = 34;
			Item.height = 34;

			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;

			Item.shoot = 10;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Dart;

			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Rattle").WithVolume(.7f).WithPitchVariance(.5f), player.position);
			type = ProjectileID.PoisonDartBlowgun;

			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("Polarities:WoodenAtlatl")
				.AddIngredient(ItemType<Items.Materials.Rattle>())
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}