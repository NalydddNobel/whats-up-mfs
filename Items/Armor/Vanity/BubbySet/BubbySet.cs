﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Polarities.Items;
using Polarities.Items.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Polarities.Items.Accessories.Wings;
using ReLogic.Content;

namespace Polarities.Items.Armor.Vanity.BubbySet
{
    [AutoloadEquip(EquipType.Head)]
    public class BubbyHead : ModItem, IDrawArmor
    {
        public override void SetStaticDefaults()
        {
            this.SetResearch(1);

            ArmorIDs.Head.Sets.DrawFullHair[EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head)] = true;

            //registers a head glowmask
            ArmorMasks.headIndexToArmorDraw.TryAdd(EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head), this);
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }

        public static Asset<Texture2D> HeadOpenTexture;
        public static Asset<Texture2D> HeadClosedTexture;
        public static Asset<Texture2D> HeadClosedGlowTexture;

        public override void Load()
        {
            HeadOpenTexture = Request<Texture2D>(Texture + "_HeadOpen");
            HeadClosedTexture = Request<Texture2D>(Texture + "_HeadClosed");
            HeadClosedGlowTexture = Request<Texture2D>(Texture + "_HeadClosed_Glow");
        }

        public override void Unload()
        {
            HeadOpenTexture = null;
            HeadClosedTexture = null;
            HeadClosedGlowTexture = null;
        }

        public void DrawArmor(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            if (!drawPlayer.invis)
            {
                Texture2D texture = (drawPlayer.HeldItem.damage > 0) ? HeadClosedTexture.Value : HeadOpenTexture.Value;

                Rectangle bodyFrame3 = drawInfo.drawPlayer.bodyFrame;
                Vector2 headVect2 = drawInfo.headVect;
                if (drawInfo.drawPlayer.gravDir == 1f)
                {
                    bodyFrame3.Height -= 4;
                }
                else
                {
                    headVect2.Y -= 4f;
                    bodyFrame3.Height -= 4;
                }
                Vector2 helmetOffset = drawInfo.helmetOffset;
                Vector2 position = helmetOffset + new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.headPosition + drawInfo.headVect;

                DrawData drawData = new DrawData(texture, position, bodyFrame3, drawInfo.colorArmorHead, drawInfo.drawPlayer.headRotation, headVect2, 1f, drawInfo.playerEffect, 0);
                drawData.shader = drawInfo.cHead;
                drawInfo.DrawDataCache.Add(drawData);

                if (drawPlayer.HeldItem.damage > 0)
                {
                    Color color = new Color(192, 192, 192);
                    if (drawPlayer.HeldItem.useAmmo == AmmoID.Bullet)
                    {
                        color = new Color(128, 255, 255);
                    }
                    if (drawPlayer.HeldItem.useAmmo == AmmoID.Dart)
                    {
                        color = new Color(255, 161, 209);
                    }
                    else if (drawPlayer.HeldItem.useAmmo == AmmoID.Rocket)
                    {
                        color = new Color(255, 198, 98);
                    }

                    drawData = new DrawData(HeadClosedGlowTexture.Value, position, bodyFrame3, color, drawInfo.drawPlayer.headRotation, headVect2, 1f, drawInfo.playerEffect, 0);
                    drawData.shader = drawInfo.cHead;
                    drawInfo.DrawDataCache.Add(drawData);
                }
            }
        }
    }

    [AutoloadEquip(EquipType.Body)] //TODO: Convert to 1.4 sheeting
    public class BubbyBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            this.SetResearch(1);

            ArmorIDs.Body.Sets.HidesArms[EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body)] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class BubbyLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            this.SetResearch(1);
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 14;
            Item.value = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}
