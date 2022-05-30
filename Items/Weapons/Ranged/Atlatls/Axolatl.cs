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
	public class Axolatl : AtlatlBase
	{
		public override Vector2[] ShotDistances => new Vector2[] { new Vector2(34) };
		public override float BaseShotDistance => 30;

		public override void SetDefaults()
		{
			Item.SetWeaponValues(16, 6, 0);
			Item.DamageType = DamageClass.Ranged;

			Item.width = 50;
			Item.height = 42;

			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;

			Item.shoot = 10;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Dart;

			Item.autoReuse = true;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Blue;
		}

		bool shotConsumed;
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
			shotConsumed = Main.rand.NextBool();
			return shotConsumed;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (!shotConsumed) type = ProjectileType<AxolatlBubble>();
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool RealShoot(Player player, EntitySource_ItemUse_WithAmmo source, int index, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			if (type == ProjectileType<AxolatlBubble>())
            {
				velocity = velocity.RotatedByRandom(0.1f);
			}

			return true;
        }
    }

	public class AxolatlBubble : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.alpha = 0;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.timeLeft = 600;
			Projectile.scale = 1f;
		}

		public override void AI()
		{
			if (Projectile.wet)
			{
				Projectile.velocity.Y -= 0.3f;
			}
			else
			{
				Projectile.velocity.Y -= 0.1f;
			}
			Projectile.velocity *= 0.92f;
		}
	}
}
