// Decompiled with JetBrains decompiler
// Type: Helicopter.Gamer
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Items;
using Helicopter.Items.AchievementsSystem;
using Helicopter.Items.Ammunition;
using Helicopter.Items.DeviceItems;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Model.WorldObjects.Patterns;
using Helicopter.Utils;
using System;
using System.Xml.Linq;
using Windows.Storage;

#nullable disable
namespace Helicopter
{
  internal class Gamer
  {
    private static Gamer _instance;

    public Rank Rank { get; private set; }

    public Money Money { get; set; }

    public SlotDescription<HelicopterItem> CurrentHelicopter { get; set; }

    public SlotDescription<WeaponItem> FirstWeapon { get; set; }

    public SlotDescription<WeaponItem> SecondWeapon { get; set; }

    public SlotDescription<UpgradeItem> UpgradeA { get; set; }

    public SlotDescription<UpgradeItem> UpgradeB { get; set; }

    public SlotDescription<AmmunitionItem> DamageBonus { get; set; }

    public SlotDescription<AmmunitionItem> DefenseBonus { get; set; }

    public SlotDescription<HealthAmmunitionItem> HealthBonus { get; set; }

    public AchievementManager AchievementManager { get; protected set; }

    public Statistics Statistics { get; protected set; }

    public static Gamer Instance => Gamer._instance ?? (Gamer._instance = new Gamer());

    public float Energy => this.GetPlayer(new GameWorld()).MaxEnergy;

    public float Discount
    {
      get
      {
        switch (this.Rank)
        {
          case Rank.Pilot:
            return 0.0f;
          case Rank.Sergeant:
            return 0.03f;
          case Rank.Lieutenant:
            return 0.06f;
          case Rank.Captain:
            return 0.09f;
          case Rank.Major:
            return 0.12f;
          case Rank.Colonel:
            return 0.15f;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public SlotDescription<WeaponItem> HasNewFirstWeapon { get; set; }

    public SlotDescription<WeaponItem> HasNewSecondWeapon { get; set; }

    public SlotDescription<UpgradeItem> HasNewUpgrade { get; set; }

    public SlotDescription<HelicopterItem> HasNewCopter { get; set; }

    public Gamer()
    {
      this.AchievementManager = new AchievementManager();
      this.Statistics = new Statistics();
      this.Money = new Money();
      this.FirstWeapon = new SlotDescription<WeaponItem>();
      this.SecondWeapon = new SlotDescription<WeaponItem>();
      this.UpgradeA = new SlotDescription<UpgradeItem>();
      this.UpgradeB = new SlotDescription<UpgradeItem>();
      this.HasNewFirstWeapon = new SlotDescription<WeaponItem>();
      this.HasNewSecondWeapon = new SlotDescription<WeaponItem>();
      this.HasNewUpgrade = new SlotDescription<UpgradeItem>();
      this.HasNewCopter = new SlotDescription<HelicopterItem>();
      this.DamageBonus = new SlotDescription<AmmunitionItem>();
      this.DefenseBonus = new SlotDescription<AmmunitionItem>();
      this.HealthBonus = new SlotDescription<HealthAmmunitionItem>();
      this.CurrentHelicopter = new SlotDescription<HelicopterItem>();
    }

    public event EventHandler NewRank;

    public void InvokeNewRank()
    {
      EventHandler newRank = this.NewRank;
      if (newRank == null)
        return;
      newRank((object) this, EventArgs.Empty);
    }

    public void UpdateRank(int stars)
    {
      Rank rank = this.Rank;
      this.Rank = stars >= 4 ? (stars >= 8 ? (stars >= 15 ? (stars >= 22 ? (stars >= 30 ? Rank.Colonel : Rank.Major) : Rank.Captain) : Rank.Lieutenant) : Rank.Sergeant) : Rank.Pilot;
      if (this.Rank == rank)
        return;
      this.InvokeNewRank();
    }

    public int GetItemPrice(Item item)
    {
      return (int) ((double) item.Price * (1.0 - (double) this.Discount));
    }

    public SmartPlayer GetPlayer(GameWorld gameWorld)
    {
      if (!this.FirstWeapon.IsInstalled)
        this.FirstWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.SingleMachineGun);
      PlayerPattern pattern = this.CurrentHelicopter.Item.Pattern;
      pattern.Id = IdFactory.Instance.GetId();
      SmartPlayer smartPlayer = new SmartPlayer();
      smartPlayer.GameWorld = gameWorld;
      smartPlayer.Id = IdFactory.Instance.GetId();
      smartPlayer.Team = 1;
      smartPlayer.Weapons = new Weapon[2];
      smartPlayer.ZIndex = 20f;
      smartPlayer.Money = this.Money;
      SmartPlayer player = smartPlayer;
      player.Init(pattern);
      this.InitAmmunition(player);
      player.SetPosition(200f, 200f);
      player.Energy = player.MaxEnergy = pattern.Energy;
      if (this.FirstWeapon.IsInstalled && this.CurrentHelicopter.Item.HasFirstWeapon)
        player.AddWeapon(0, WeaponFactory.GetWeapon((Helicopter.Model.WorldObjects.Instances.Instance) player, this.FirstWeapon.Item.WeaponType));
      if (this.SecondWeapon.IsInstalled && this.CurrentHelicopter.Item.HasSecondWeapon)
        player.AddWeapon(1, WeaponFactory.GetWeapon((Helicopter.Model.WorldObjects.Instances.Instance) player, this.SecondWeapon.Item.WeaponType));
      if (this.UpgradeA.IsInstalled && this.CurrentHelicopter.Item.HasUpgradeA)
        this.UpgradeA.Item.Apply(player);
      if (this.UpgradeB.IsInstalled && this.CurrentHelicopter.Item.HasUpgradeB)
        this.UpgradeB.Item.Apply(player);
      gameWorld.AddInstance((Helicopter.Model.WorldObjects.Instances.Instance) player);
      player.Rammed += new EventHandler<PlayerEventArgs>(this.OnRammed);
      player.Damaged += new EventHandler<PlayerEventArgs>(this.OnPlayerDamaged);
      player.MountainCollided += new EventHandler<PlayerEventArgs>(this.OnPlayerDamaged);
      return player;
    }

    private void OnPlayerDamaged(object sender, PlayerEventArgs e)
    {
      if (e.DamageType != DamageType.Rocket && e.DamageType != DamageType.Collision)
        return;
      Vibro.Vibrate();
    }

    private void OnRammed(object sender, EventArgs e)
    {
      this.Statistics.AddRammedCopter(1);
      Vibro.Vibrate();
    }

    public void InitAmmunition(SmartPlayer player)
    {
      if (this.DefenseBonus.IsInstalled)
      {
        player.DefenseModifier = new DefenseModifier();
        switch (this.DefenseBonus.Item.Type)
        {
          case AmunitionType.Shield25:
            player.DefenseModifier.AddDefenseCoef(DamageType.Collision, 0.25f);
            player.DefenseModifier.AddDefenseCoef(DamageType.Bullet, 0.25f);
            player.DefenseModifier.AddDefenseCoef(DamageType.Rocket, 0.25f);
            break;
          case AmunitionType.Shield50:
            player.DefenseModifier.AddDefenseCoef(DamageType.Collision, 0.5f);
            player.DefenseModifier.AddDefenseCoef(DamageType.Bullet, 0.5f);
            player.DefenseModifier.AddDefenseCoef(DamageType.Rocket, 0.5f);
            break;
          case AmunitionType.MisslesShieldUnit:
            player.DefenseModifier.AddDefenseCoef(DamageType.Rocket, 1f);
            break;
          case AmunitionType.BallisticShieldModule:
            player.DefenseModifier.AddDefenseCoef(DamageType.Bullet, 1f);
            break;
          case AmunitionType.CollisionsModule:
            player.DefenseModifier.AddDefenseCoef(DamageType.Collision, 1f);
            break;
          default:
            throw new Exception(string.Format("Unknown Defense Amunition Type '{0}'.", (object) this.DefenseBonus.Item.Type));
        }
      }
      else
        player.DefenseModifier = (DefenseModifier) null;
      if (this.DamageBonus.IsInstalled)
      {
        player.DamageModifier = new DamageModifier();
        if (this.DamageBonus.Item.Type != AmunitionType.DamageIncrease50)
          throw new Exception(string.Format("Unknown Damage Amunition Type '{0}'.", (object) this.DefenseBonus.Item.Type));
        player.DamageModifier.DamageCoef = 0.5f;
      }
      else
        player.DamageModifier = (DamageModifier) null;
      SmartPlayer smartPlayer = player;
      ExternalSupply externalSupply;
      if (!this.HealthBonus.IsInstalled)
        externalSupply = (ExternalSupply) null;
      else
        externalSupply = new ExternalSupply()
        {
          Volume = this.HealthBonus.Item.Volume
        };
      smartPlayer.ExternalSupply = externalSupply;
    }

    public void InitItems()
    {
      if (this.CurrentHelicopter.IsInstalled)
        return;
      this.CurrentHelicopter.Item = ItemCollection.Instance.HelicopterItems[0];
    }

    private void Deserialize(XElement element)
    {
      XElement xelement = element;
      this.Rank = (Rank) Enum.Parse(this.Rank.GetType(), xelement.Element((XName) "Rank").Value, true);
      this.Money = Money.Deserialize(xelement.Element((XName) "Money").Value);
      this.AchievementManager.Deserialize(xelement.Element((XName) "AchievementManager").Value);
      this.CurrentHelicopter.Item = ItemCollection.Instance.GetHelicopter(xelement.Element((XName) "CurrentHelicopterItem").Value);
      this.FirstWeapon.Item = ItemCollection.Instance.GetWeapon(xelement.Element((XName) "FirstWeapon").Value);
      this.SecondWeapon.Item = ItemCollection.Instance.GetWeapon(xelement.Element((XName) "SecondWeapon").Value);
      this.UpgradeA.Item = ItemCollection.Instance.GetUpgrade(xelement.Element((XName) "UpgradeA").Value);
      this.UpgradeB.Item = ItemCollection.Instance.GetUpgrade(xelement.Element((XName) "UpgradeB").Value);
      this.DamageBonus.Item = ItemCollection.Instance.GetAmmunition(xelement.Element((XName) "DamageBonus").Value);
      this.DefenseBonus.Item = ItemCollection.Instance.GetAmmunition(xelement.Element((XName) "DefenseBonus").Value);
      this.HealthBonus.Item = ItemCollection.Instance.GetHealthAmunition(xelement.Element((XName) "HealthBonus").Value);
      this.Statistics.Deserialize(xelement.Element((XName) "Statistics"));
    }

    private XElement Serialize()
    {
      XElement xelement = new XElement((XName) "gamer");
      XElement content1 = new XElement((XName) "Rank", (object) this.Rank);
      xelement.Add((object) content1);
      XElement content2 = new XElement((XName) "Money", (object) this.Money.Serialize());
      xelement.Add((object) content2);
      XElement content3 = new XElement((XName) "CurrentHelicopterItem", (object) this.CurrentHelicopter.Serialize());
      xelement.Add((object) content3);
      XElement content4 = new XElement((XName) "FirstWeapon", (object) this.FirstWeapon.Serialize());
      xelement.Add((object) content4);
      XElement content5 = new XElement((XName) "SecondWeapon", (object) this.SecondWeapon.Serialize());
      xelement.Add((object) content5);
      XElement content6 = new XElement((XName) "UpgradeA", (object) this.UpgradeA.Serialize());
      xelement.Add((object) content6);
      XElement content7 = new XElement((XName) "UpgradeB", (object) this.UpgradeB.Serialize());
      xelement.Add((object) content7);
      XElement content8 = new XElement((XName) "DamageBonus", (object) this.DamageBonus.Serialize());
      xelement.Add((object) content8);
      XElement content9 = new XElement((XName) "DefenseBonus", (object) this.DefenseBonus.Serialize());
      xelement.Add((object) content9);
      XElement content10 = new XElement((XName) "HealthBonus", (object) this.HealthBonus.Serialize());
      xelement.Add((object) content10);
      XElement content11 = new XElement((XName) "AchievementManager", (object) this.AchievementManager.Serialize());
      xelement.Add((object) content11);
      XElement content12 = this.Statistics.Serialize();
      xelement.Add((object) content12);
      return xelement;
    }

    public void Save()
    {
      var settings = ApplicationData.Current.LocalSettings;
      settings.Values["gamer"] = this.Serialize().ToString();
    }

    public void Load()
    {
      var settings = ApplicationData.Current.LocalSettings;
      if (!settings.Values.ContainsKey("gamer"))
        return;
      var xml = settings.Values["gamer"]?.ToString();
      if (string.IsNullOrEmpty(xml))
        return;
      this.Deserialize(XElement.Parse(xml));
    }
  }
}
