﻿using Polarities.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Polarities.Items.Materials
{
	public class AlkalineFluid : ModItem
	{
		public override void SetStaticDefaults()
		{
			this.SetResearch(25);
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 16;
			Item.maxStack = 999;
			Item.value = 10;
			Item.rare = ItemRarityID.White;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, 198 / 512f, 239 / 512f, 159 / 512f);
		}
	}
}