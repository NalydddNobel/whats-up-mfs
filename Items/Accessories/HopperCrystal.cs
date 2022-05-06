﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System;

namespace Polarities.Items.Accessories
{
	public class HopperCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.SetResearch(1);
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 24;
			Item.accessory = true;
			Item.value = 2500;
			Item.rare = ItemRarityID.Blue;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.jumpSpeedBoost += 1.2f;
			player.autoJump = true;
			player.GetModPlayer<PolaritiesPlayer>().hopperCrystal = true;
		}
	}
}