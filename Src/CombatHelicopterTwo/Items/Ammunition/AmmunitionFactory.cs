// Modified by MediaExplorer (2026)
// Type: Helicopter.Items.Ammunition.AmmunitionFactory
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items.HangarDesc;

#nullable disable
namespace Helicopter.Items.Ammunition
{
  internal static class AmmunitionFactory
  {
    public static AmmunitionItem GetBallisticShieldModule()
    {
      AmmunitionItem ballisticShieldModule = new AmmunitionItem();
      ballisticShieldModule.Name = "Ballistic shield module";
      ballisticShieldModule.Description = "During one mission \r\nabsorbs all damage from bullets";
      ballisticShieldModule.Type = AmunitionType.BallisticShieldModule;
      ballisticShieldModule.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemShieldBallisticMmodule",
        OnCopterTexture = "Hangar/Amunition color/shieldBallisticMmodule",
        InGameTexture = "UI/Aminitions/shieldBallisticMmodule",
        InGameTextureSelected = "UI/Aminitions/shieldBallisticMmoduleS"
      };
      ballisticShieldModule.UnlockCondition.Price = 5000;
      return ballisticShieldModule;
    }

    public static AmmunitionItem GetCollisionsModule()
    {
      AmmunitionItem collisionsModule = new AmmunitionItem();
      collisionsModule.Name = "Collision module";
      collisionsModule.Description = "During one mission\r\nabsorbs all damage from Collision";
      collisionsModule.Type = AmunitionType.CollisionsModule;
      collisionsModule.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemCollisionsModule",
        OnCopterTexture = "Hangar/Amunition color/collisionsModule",
        InGameTexture = "UI/Aminitions/shieldCollisions",
        InGameTextureSelected = "UI/Aminitions/shieldCollisionsS"
      };
      collisionsModule.UnlockCondition.Price = 5000;
      return collisionsModule;
    }

    public static AmmunitionItem GetDamageIncrease50()
    {
      AmmunitionItem damageIncrease50 = new AmmunitionItem();
      damageIncrease50.Name = "Enhanced ammo";
      damageIncrease50.Description = "During one mission \r\nincreases all damage done by 50%";
      damageIncrease50.Type = AmunitionType.DamageIncrease50;
      damageIncrease50.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemGunsPlus",
        OnCopterTexture = "Hangar/Amunition color/gunsPlus",
        InGameTexture = "UI/Aminitions/gunsPlus",
        InGameTextureSelected = "UI/Aminitions/gunsPlusS"
      };
      damageIncrease50.UnlockCondition.Price = 7000;
      damageIncrease50.IsBought = false;
      return damageIncrease50;
    }

    public static AmmunitionItem GetHealthPack100()
    {
      AmmunitionItem healthPack100 = new AmmunitionItem();
      healthPack100.Name = "Extra Power Supply";
      healthPack100.Description = "Restores up to 100% of energy.\r\nAuto-activates at 5%";
      healthPack100.Type = AmunitionType.HealthPack100;
      healthPack100.UnlockCondition.Price = 5000;
      healthPack100.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemHealth",
        OnCopterTexture = "Hangar/Amunition color/health",
        InGameTexture = "UI/Aminitions/health",
        InGameTextureSelected = "UI/Aminitions/healthS"
      };
      return healthPack100;
    }

    public static AmmunitionItem GetHealthPack50()
    {
      AmmunitionItem healthPack50 = new AmmunitionItem();
      healthPack50.Type = AmunitionType.HealthPack50;
      healthPack50.Name = "Extra Power Supply: Small";
      healthPack50.Description = "Restores up to 50% of energy.\r\nAuto-activates at 5%";
      healthPack50.UnlockCondition.Price = 2500;
      healthPack50.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemHealthHalf",
        OnCopterTexture = "Hangar/Amunition color/healthHalf",
        InGameTexture = "UI/Aminitions/health",
        InGameTextureSelected = "UI/Aminitions/healthS"
      };
      return healthPack50;
    }

    public static AmmunitionItem GetMisslesShieldUnit()
    {
      AmmunitionItem misslesShieldUnit = new AmmunitionItem();
      misslesShieldUnit.Name = "Missile shield unit";
      misslesShieldUnit.Description = "During one mission \r\nabsorbs all damage from missiles";
      misslesShieldUnit.Type = AmunitionType.MisslesShieldUnit;
      misslesShieldUnit.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemShieldMissileUnit",
        OnCopterTexture = "Hangar/Amunition color/shieldMissileUnit",
        InGameTexture = "UI/Aminitions/shieldMissileUnit",
        InGameTextureSelected = "UI/Aminitions/shieldMissileUnitS"
      };
      misslesShieldUnit.UnlockCondition.Price = 5000;
      return misslesShieldUnit;
    }

    public static AmmunitionItem GetShield25()
    {
      AmmunitionItem shield25 = new AmmunitionItem();
      shield25.Name = "Small protection module";
      shield25.Description = "During one mission \r\nReduces incoming damage by 25%";
      shield25.Type = AmunitionType.Shield25;
      shield25.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemShieldHalf",
        OnCopterTexture = "Hangar/Amunition color/shieldHalf",
        InGameTexture = "UI/Aminitions/shieldHalf",
        InGameTextureSelected = "UI/Aminitions/shieldHalfS"
      };
      shield25.UnlockCondition.Price = 5000;
      return shield25;
    }

    public static AmmunitionItem GetShield50()
    {
      AmmunitionItem shield50 = new AmmunitionItem();
      shield50.Name = "Protection module";
      shield50.Description = "During one mission \r\nReduces incoming damage by 50%";
      shield50.Type = AmunitionType.Shield50;
      shield50.HangarDesc = (HangarLayoutDescription) new ItemLayoutDescription()
      {
        OnShopTexture = "Hangar/Items/Amunition color/itemShield",
        OnCopterTexture = "Hangar/Amunition color/shield",
        InGameTexture = "UI/Aminitions/shield",
        InGameTextureSelected = "UI/Aminitions/shieldS"
      };
      shield50.UnlockCondition.Price = 10000;
      return shield50;
    }
  }
}
