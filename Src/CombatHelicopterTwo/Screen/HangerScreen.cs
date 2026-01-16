// Decompiled with JetBrains decompiler
// Type: Helicopter.Screen.HangerScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.Analytics;
using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.GamePlay;
using Helicopter.GamePlay.GameplayPopups;
using Helicopter.Items;
using Helicopter.Items.AchievementsSystem;
using Helicopter.Items.Ammunition;
using Helicopter.Items.DeviceItems;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Screen.Hangar;
using Helicopter.Screen.MapScreen.Tutorial_Popups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Helicopter.Screen
{
  internal class HangerScreen : GameScreen
  {
    private readonly Rectangle CopterArea = new Rectangle(0, 0, 800, 330);
    private Rectangle WeaponArea = new Rectangle(161, 327, 480, 146);
    private Rectangle WeaponAreaDoubleTap = new Rectangle(220, 327, 360, 146);
    private MenuControl _buyButton;
    private Color _colorGreen;
    private Color _colorYellow;
    private CopterFixedStepHorizontalScrollPanel _copters;
    private TextControl _costCreditLabel;
    private ItemFixedStepHorizontalScrollPanel _currentPanel;
    private HangerScreen.Slots _currentSlot;
    private SpecialSlotTwoStateMenuControl _damageBonus;
    private ItemFixedStepHorizontalScrollPanel _damagePanel;
    private SpecialSlotTwoStateMenuControl _defenseBonus;
    private ItemFixedStepHorizontalScrollPanel _defensePanel;
    private MenuControl _equipButton;
    private ItemTexturedControl _firstWeapon;
    private ItemFixedStepHorizontalScrollPanel _firstWeaponPanel;
    private SpriteFont _font11;
    private SpriteFont _font14;
    private SpriteFont _font8;
    private SpecialSlotTwoStateMenuControl _healthBonus;
    private ItemFixedStepHorizontalScrollPanel _healthPanel;
    private TextControl _playerCreditsLabel;
    private TexturedControl _root;
    private ItemTexturedControl _secondWeapon;
    private ItemFixedStepHorizontalScrollPanel _secondWeaponPanel;
    private ItemTexturedControl _upgradeA;
    private ItemTexturedControl _upgradeB;
    private ItemFixedStepHorizontalScrollPanel _upgradePanel;
    private MenuControl _previousItem;
    private MenuControl _nextItem;
    public bool IsPreviousBought = true;
    private MenuControl _repairButton;
    private readonly BasicControl _stats = new BasicControl();

    public event EventHandler Back;

    public event EventHandler BuyNewCopter;

    public event EventHandler Fight;

    public HangerScreen.Slots State
    {
      get => this._currentSlot;
      set
      {
        ItemFixedStepHorizontalScrollPanel currentPanel = this._currentPanel;
        this._currentSlot = value;
        Item currentPanelItem = this.CurrentPanelItem;
        this.EquipBuyButtonStates(currentPanelItem);
        this.UpdateCostLabel(currentPanelItem);
        switch (this._currentSlot)
        {
          case HangerScreen.Slots.FirstWeapon:
            this._currentPanel = this._firstWeaponPanel;
            if (Gamer.Instance.HasNewFirstWeapon.IsInstalled)
            {
              this._currentPanel.CurrentItem = (Item) Gamer.Instance.HasNewFirstWeapon.Item;
              Gamer.Instance.HasNewFirstWeapon.Item = (WeaponItem) null;
              break;
            }
            break;
          case HangerScreen.Slots.SecondWeapon:
            this._currentPanel = this._secondWeaponPanel;
            if (Gamer.Instance.HasNewSecondWeapon.IsInstalled)
            {
              this._currentPanel.CurrentItem = (Item) Gamer.Instance.HasNewSecondWeapon.Item;
              Gamer.Instance.HasNewSecondWeapon.Item = (WeaponItem) null;
              break;
            }
            break;
          case HangerScreen.Slots.UpgradeA:
          case HangerScreen.Slots.UpgradeB:
            this._currentPanel = this._upgradePanel;
            if (Gamer.Instance.HasNewUpgrade.IsInstalled)
            {
              this._currentPanel.CurrentItem = (Item) Gamer.Instance.HasNewUpgrade.Item;
              Gamer.Instance.HasNewUpgrade.Item = (UpgradeItem) null;
              break;
            }
            break;
          case HangerScreen.Slots.Defense:
            this._currentPanel = this._defensePanel;
            break;
          case HangerScreen.Slots.Damage:
            this._currentPanel = this._damagePanel;
            break;
          case HangerScreen.Slots.Health:
            this._currentPanel = this._healthPanel;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
        currentPanel.Visible = false;
        this._currentPanel.Visible = true;
      }
    }

    private Item CurrentPanelItem => this._currentPanel.CurrentItem;

    public HangerScreen() => this.IsPopup = true;

    public override void Draw(DrawContext drawContext) => this._root.Draw(drawContext);

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this.ShowAchievements();
      this._root.Update(gameTime);
    }

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      foreach (GestureSample gesture in input.Gestures)
      {
        if (gesture.GestureType == GestureType.DoubleTap && this.WeaponAreaDoubleTap.Contains(new Point((int) gesture.Position.X, (int) gesture.Position.Y)))
        {
          if (this._buyButton.Visible && this._buyButton.Enabled)
            this._buyButton.OnSelectEntry();
          else if (this._equipButton.Visible && this._equipButton.Enabled)
            this._equipButton.OnSelectEntry();
        }
      }
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._font8 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition8");
      this._font11 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition11");
      this._font14 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition14");
      ResourcesManager.Instance.GetResource<SpriteFont>("fonts/tahoma");
      this._colorYellow = new Color(254, 224, 23);
      this._colorGreen = new Color(174, 248, 69);
      TouchPanel.EnabledGestures |= GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.Flick | GestureType.DragComplete;
      this._root = new TexturedControl(ResourcesManager.Instance.GetSprite("Hangar/bgHangar"), Vector2.Zero);
      this.CreateArrowsForPanel();
      this.CreateAmunitionIcons();
      this.CreateCopters();
      this.CreateBottomPanel();
      this.CreateTopPanel();
      this.CreateStats();
      if (this._copters.CurrentHelicopter.IsBought)
        this._copters.Slot = HangerScreen.Slots.FirstWeapon;
      this.ScreenManager.AudioManager.BackgroundSounds.PlayHangerTheme();
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = true
      });
      this.NotifyOpenHangar();
      this.CreateRepairButton();
    }

    private void CreateArrowsForPanel()
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/previousItem");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/previousItemSelect");
      this._previousItem = new MenuControl(sprite1, sprite2, new Vector2(10f, (float) (this.WeaponArea.Height / 2 - sprite1.Bounds.Height / 2)));
      Rectangle entryPosition1 = this._previousItem.EntryPosition;
      entryPosition1.Inflate(30, 30);
      this._previousItem.EntryPosition = entryPosition1;
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/nextItem");
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("Hangar/nextItemSelect");
      this._nextItem = new MenuControl(sprite3, sprite4, new Vector2((float) (this.WeaponArea.Width - 10 - sprite3.Bounds.Width), (float) (this.WeaponArea.Height / 2 - sprite1.Bounds.Height / 2)));
      Rectangle entryPosition2 = this._nextItem.EntryPosition;
      entryPosition2.Inflate(30, 30);
      this._nextItem.EntryPosition = entryPosition2;
    }

    public override void UnloadContent()
    {
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
      base.UnloadContent();
    }

    private void CreateAmunitionPanel()
    {
      this.CreateHealthPanel();
      this.CreateDamagePanel();
      this.CreateDefensePanel();
    }

    private void CreateDamagePanel()
    {
      this._damagePanel = new ItemFixedStepHorizontalScrollPanel();
      this._damagePanel.Visible = false;
      this._damagePanel.Position = new Vector2(161f, 327f);
      this._damagePanel.AreaOnScreen = this.WeaponArea;
      foreach (AmmunitionItem amunitionItem in ItemCollection.Instance.AmunitionItems)
      {
        if ((amunitionItem.Type & AmunitionType.DamageIncrease50) != (AmunitionType) 0)
          this._damagePanel.AddElement((Item) amunitionItem, this._font14, this._font8, this._colorYellow);
      }
      this._damagePanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._damagePanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._root.AddChild((BasicControl) this._damagePanel);
      this._damagePanel.AddLeftArrow(this._previousItem.Clone());
      this._damagePanel.AddRightArrow(this._nextItem.Clone());
    }

    private void CreateDefensePanel()
    {
      this._defensePanel = new ItemFixedStepHorizontalScrollPanel();
      this._defensePanel.Visible = false;
      this._defensePanel.Position = new Vector2(161f, 327f);
      this._defensePanel.AreaOnScreen = this.WeaponArea;
      foreach (AmmunitionItem amunitionItem in ItemCollection.Instance.AmunitionItems)
      {
        if ((amunitionItem.Type & AmunitionType.Defense) != (AmunitionType) 0)
          this._defensePanel.AddElement((Item) amunitionItem, this._font14, this._font8, this._colorYellow);
      }
      this._defensePanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._defensePanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._root.AddChild((BasicControl) this._defensePanel);
      this._defensePanel.AddLeftArrow(this._previousItem.Clone());
      this._defensePanel.AddRightArrow(this._nextItem.Clone());
    }

    private void CreateHealthPanel()
    {
      this._healthPanel = new ItemFixedStepHorizontalScrollPanel();
      this._healthPanel.Visible = false;
      this._healthPanel.Position = new Vector2(161f, 327f);
      this._healthPanel.AreaOnScreen = this.WeaponArea;
      foreach (AmmunitionItem amunitionItem in ItemCollection.Instance.AmunitionItems)
      {
        if ((amunitionItem.Type & AmunitionType.Health) != (AmunitionType) 0)
          this._healthPanel.AddElement((Item) amunitionItem, this._font14, this._font8, this._colorYellow);
      }
      this._healthPanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._healthPanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._root.AddChild((BasicControl) this._healthPanel);
      this._healthPanel.AddLeftArrow(this._previousItem.Clone());
      this._healthPanel.AddRightArrow(this._nextItem.Clone());
    }

    private void OnDamageButtonClick(object sender, EventArgs e)
    {
      this._damagePanel.CurrentItem = (Item) Gamer.Instance.DamageBonus.Item;
      this.OnCopterSlotClick(HangerScreen.Slots.Damage);
    }

    private void OnDefenseButtonClick(object sender, EventArgs e)
    {
      this._defensePanel.CurrentItem = (Item) Gamer.Instance.DefenseBonus.Item;
      this.OnCopterSlotClick(HangerScreen.Slots.Defense);
    }

    private void OnHealthBonusClick(object sender, EventArgs e)
    {
      this._healthPanel.CurrentItem = (Item) ItemCollection.Instance.AmunitionItems[0];
      this.OnCopterSlotClick(HangerScreen.Slots.Health);
    }

    private void CreateAmunitionIcons()
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/Amunition/emptySlotSelectAnimation");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/Amunition/emptySlotGreen");
      Rectangle entryRectangle = new Rectangle();
      this._healthBonus = new SpecialSlotTwoStateMenuControl(sprite2, (Sprite) null, sprite1, Vector2.Zero, entryRectangle);
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/Amunition color/healthNumberField2");
      this._healthBonus.Size = new Vector2(45f, 45f);
      TexturedControl texturedControl = new TexturedControl(sprite3, new Vector2(17f, -1f));
      texturedControl.Visible = false;
      TexturedControl child1 = texturedControl;
      TextControl child2 = new TextControl("11", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/tahoma12"), this._colorGreen, sprite3.ScaledSize / 2f)
      {
        Scale = 0.6f,
        Centered = true,
        Origin = new Vector2(0.5f, 0.0f)
      };
      child2.Position = new Vector2(child2.Position.X + 1.5f, child2.Position.Y - 1f);
      child1.AddChild((BasicControl) child2);
      this._healthBonus.AddChild((BasicControl) child1, 0);
      this._healthBonus.ItemTexture = sprite2;
      if (Gamer.Instance.HealthBonus.IsInstalled)
        this.UpdateHealthIcon(Gamer.Instance.HealthBonus.Item);
      this._healthBonus.Clicked += new EventHandler<EventArgs>(this.OnHealthBonusClick);
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("Hangar/Amunition/emptySlotBlue");
      this._defenseBonus = new SpecialSlotTwoStateMenuControl(sprite4, (Sprite) null, sprite1, Vector2.Zero, entryRectangle);
      this._defenseBonus.ItemTexture = sprite4;
      if (Gamer.Instance.DefenseBonus.IsInstalled)
        this.UpdateDefenseBonus(Gamer.Instance.DefenseBonus.Item);
      this._defenseBonus.Clicked += new EventHandler<EventArgs>(this.OnDefenseButtonClick);
      this._defenseBonus.Size = new Vector2(45f, 45f);
      Sprite sprite5 = ResourcesManager.Instance.GetSprite("Hangar/Amunition/emptySlotRed");
      this._damageBonus = new SpecialSlotTwoStateMenuControl(sprite5, (Sprite) null, sprite1, Vector2.Zero, entryRectangle);
      this._damageBonus.ItemTexture = sprite5;
      if (Gamer.Instance.DamageBonus.IsInstalled)
        this.UpdateDamageBonus(Gamer.Instance.DamageBonus.Item);
      this._damageBonus.Clicked += new EventHandler<EventArgs>(this.OnDamageButtonClick);
      this._damageBonus.Size = new Vector2(45f, 45f);
      PanelControl child3 = new PanelControl();
      child3.AddChild((BasicControl) this._healthBonus);
      child3.AddChild((BasicControl) this._defenseBonus);
      child3.AddChild((BasicControl) this._damageBonus);
      child3.LayoutRow(3f, 277f, 8f);
      this._healthBonus.AddChild((BasicControl) new TextControl("power ups", this._font14, this._colorYellow, new Vector2(0.0f, -20f)));
      this._root.AddChild((BasicControl) child3);
    }

    private void UpdateDamageBonus(AmmunitionItem item)
    {
      this._damageBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).OnCopterTexture);
    }

    private void UpdateDefenseBonus(AmmunitionItem item)
    {
      this._defenseBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).OnCopterTexture);
    }

    private void UpdateHealthIcon(HealthAmmunitionItem healthBonus)
    {
      if ((double) healthBonus.Volume <= 0.0)
      {
        Gamer.Instance.HealthBonus.Item = (HealthAmmunitionItem) null;
      }
      else
      {
        this._healthBonus.Children[0].Visible = (double) healthBonus.Volume > 0.0;
        ((TextControl) this._healthBonus.Children[0].Children[0]).Text = string.Format("{0}", (object) healthBonus.Volume);
        this._healthBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) healthBonus.HangarDesc).OnCopterTexture);
      }
    }

    private void CreateWeaponPanel()
    {
      this._firstWeaponPanel = new ItemFixedStepHorizontalScrollPanel();
      this._secondWeaponPanel = new ItemFixedStepHorizontalScrollPanel();
      this._firstWeaponPanel.Position = new Vector2(161f, 327f);
      this._secondWeaponPanel.Position = new Vector2(161f, 327f);
      this._firstWeaponPanel.Visible = true;
      this._secondWeaponPanel.Visible = false;
      this._firstWeaponPanel.AreaOnScreen = this.WeaponArea;
      this._secondWeaponPanel.AreaOnScreen = this.WeaponArea;
      foreach (WeaponItem weaponItem in ItemCollection.Instance.WeaponItems)
      {
        weaponItem.Name = weaponItem.Name.ToLower();
        weaponItem.Description = string.Format("damage: {0} rate: {1}", (object) weaponItem.Damage, (object) weaponItem.Rate).ToLower();
        if (weaponItem.Slot == Slot.First)
          this._firstWeaponPanel.AddElement((Item) weaponItem, this._font14, this._font8, this._colorYellow);
        else
          this._secondWeaponPanel.AddElement((Item) weaponItem, this._font14, this._font8, this._colorYellow);
      }
      this._firstWeaponPanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._secondWeaponPanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._firstWeaponPanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._secondWeaponPanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._currentPanel = this._firstWeaponPanel;
      this._root.AddChild((BasicControl) this._firstWeaponPanel);
      this._root.AddChild((BasicControl) this._secondWeaponPanel);
      this._firstWeaponPanel.CurrentItem = (Item) Gamer.Instance.FirstWeapon.Item;
      this._secondWeaponPanel.CurrentItem = (Item) Gamer.Instance.SecondWeapon.Item;
      this._firstWeaponPanel.AddLeftArrow(this._previousItem.Clone());
      this._secondWeaponPanel.AddLeftArrow(this._previousItem.Clone());
      this._firstWeaponPanel.AddRightArrow(this._nextItem.Clone());
      this._secondWeaponPanel.AddRightArrow(this._nextItem.Clone());
    }

    private void CreateUpgradePanel()
    {
      this._upgradePanel = new ItemFixedStepHorizontalScrollPanel();
      this._upgradePanel.Position = new Vector2(161f, 327f);
      this._upgradePanel.Visible = false;
      this._upgradePanel.AreaOnScreen = this.WeaponArea;
      foreach (Item deviceItem in ItemCollection.Instance.DeviceItems)
        this._upgradePanel.AddElement(deviceItem, this._font14, this._font8, this._colorYellow);
      this._upgradePanel.ItemChanged += new EventHandler(this.OnItemChanged);
      this._upgradePanel.StartItemChanged += new EventHandler(this.OnStartItemChanged);
      this._root.AddChild((BasicControl) this._upgradePanel);
      this._upgradePanel.CurrentItem = (Item) Gamer.Instance.UpgradeA.Item;
      this._upgradePanel.AddLeftArrow(this._previousItem.Clone());
      this._upgradePanel.AddRightArrow(this._nextItem.Clone());
    }

    private void OnCopterChanged(object sender, EventArgs e)
    {
      if (this._copters.CurrentHelicopter.IsBought)
      {
        if (!this.IsPreviousBought)
        {
          this._currentPanel.MoveUp();
          this.IsPreviousBought = true;
        }
        this.OnCopterSlotClick(this._currentSlot);
      }
      else
      {
        this._currentPanel.MoveDown(this.CreateCopterDescription());
        this.UpdateCostLabel((Item) this._copters.CurrentHelicopter);
        this.IsPreviousBought = false;
        this.EquipBuyButtonStates((Item) this._copters.CurrentHelicopter);
      }
      this.ChoseCopter();
      this.CreateStats();
    }

    private void OnCopterSlotClick(HangerScreen.Slots slot)
    {
      this._copters.Slot = slot;
      this.State = this._copters.Slot;
    }

    private void ChoseCopter()
    {
      if (!this._copters.CurrentHelicopter.IsBought)
        return;
      Gamer.Instance.CurrentHelicopter.Item = this._copters.CurrentHelicopter;
    }

    private CopterControl CreateCopter(HelicopterItem helicopterItem)
    {
      CopterControl control = new CopterControl();
      control.Item = (Item) helicopterItem;
      control.Size = new Vector2(800f, 330f);
      if (!helicopterItem.IsBought)
        this.CreateLockedCopter(helicopterItem, control);
      else
        this.CreateUnlockedCopter(helicopterItem, control);
      return control;
    }

    private BasicControl CreateCopterDescription()
    {
      BasicControl copterDescription = new BasicControl()
      {
        Size = new Vector2((float) this.WeaponArea.Width, (float) this.WeaponArea.Height),
        Position = new Vector2(0.0f, (float) -this.WeaponArea.Height)
      };
      TextControl child = new TextControl(string.Format("Would you like to buy \n{0}?", (object) this._copters.CurrentHelicopter.Name).ToLower(), this._font14, this._colorYellow, copterDescription.Size / 2f)
      {
        Centered = true,
        Origin = new Vector2(0.5f, 0.0f)
      };
      copterDescription.AddChild((BasicControl) child);
      return copterDescription;
    }

    private void CreateCopters()
    {
      Sprite blankSprite = ResourcesManager.BlankSprite;
      this._firstWeapon = new ItemTexturedControl(Item.Empty, blankSprite, Vector2.Zero);
      this._firstWeapon.Sprite.Alpha = 0.0f;
      this._firstWeapon.Size = new Vector2(68f, 38f);
      this._secondWeapon = new ItemTexturedControl(Item.Empty, blankSprite, Vector2.Zero);
      this._secondWeapon.Sprite.Alpha = 0.0f;
      this._secondWeapon.Size = new Vector2(138f, 44f);
      this._upgradeA = new ItemTexturedControl(Item.Empty, blankSprite, Vector2.Zero);
      this._upgradeA.Sprite.Alpha = 0.0f;
      this._upgradeA.Size = new Vector2(58f, 47f);
      this._upgradeB = new ItemTexturedControl(Item.Empty, blankSprite, Vector2.Zero);
      this._upgradeB.Sprite.Alpha = 0.0f;
      this._upgradeB.Size = new Vector2(58f, 47f);
      Gamer instance = Gamer.Instance;
      if (!instance.FirstWeapon.IsInstalled)
        instance.FirstWeapon.Item = ItemCollection.Instance.GetWeapon(WeaponType.SingleMachineGun);
      if (instance.FirstWeapon.IsInstalled)
      {
        this._firstWeapon.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) Gamer.Instance.FirstWeapon.Item.HangarDesc).OnCopterTexture);
        this._firstWeapon.Sprite.Alpha = 1f;
      }
      if (instance.SecondWeapon.IsInstalled)
      {
        this._secondWeapon.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) Gamer.Instance.SecondWeapon.Item.HangarDesc).OnCopterTexture);
        this._secondWeapon.Sprite.Alpha = 1f;
      }
      if (instance.UpgradeA.IsInstalled)
      {
        this._upgradeA.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) Gamer.Instance.UpgradeA.Item.HangarDesc).OnCopterTexture);
        this._upgradeA.Sprite.Alpha = 1f;
      }
      if (instance.UpgradeB.IsInstalled)
      {
        this._upgradeB.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) Gamer.Instance.UpgradeB.Item.HangarDesc).OnCopterTexture);
        this._upgradeB.Sprite.Alpha = 1f;
      }
      this._copters = new CopterFixedStepHorizontalScrollPanel();
      foreach (HelicopterItem helicopterItem in ItemCollection.Instance.HelicopterItems)
      {
        CopterControl copter = this.CreateCopter(helicopterItem);
        copter.Position = new Vector2(this._copters.Size.X, 0.0f);
        this._copters.AddChild((BasicControl) copter);
      }
      this._copters.LayoutRow(0.0f, 0.0f, 0.0f);
      this._copters.AreaOnScreen = this.CopterArea;
      this._root.AddChild((BasicControl) this._copters);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/previousCopter");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/previousCopterSelect");
      int x1 = 12;
      this._copters.AddLeftArrow(new MenuControl(sprite1, sprite2, new Vector2((float) x1, 144f)));
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/nextCopter");
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("Hangar/nextCopterSelect");
      this._copters.AddRightArrow(new MenuControl(sprite3, sprite4, new Vector2((float) (800 - x1 - sprite3.Bounds.Width), 144f)));
      for (int index = 0; index < this._copters.Children.Count; ++index)
      {
        HelicopterItem helicopterItem = (HelicopterItem) ((CopterControl) this._copters.Children[index]).Item;
        if (Gamer.Instance.HasNewCopter.IsInstalled)
        {
          if (helicopterItem == Gamer.Instance.HasNewCopter.Item)
          {
            this._copters.InstantNavigateToItem(index);
            Gamer.Instance.HasNewCopter.Item = (HelicopterItem) null;
            break;
          }
        }
        else if (helicopterItem == Gamer.Instance.CurrentHelicopter.Item)
        {
          this._copters.InstantNavigateToItem(index);
          break;
        }
      }
      this._copters.ItemChanged += new EventHandler(this.OnCopterChanged);
      this._copters.StartItemChanged += (EventHandler) ((x, y) => this.ScreenManager.AudioManager.HangarSounds.PlayCopterSlide());
    }

    private void CreateLockedCopter(HelicopterItem helicopterItem, CopterControl control)
    {
      CopterLayoutDescription hangarDesc = (CopterLayoutDescription) helicopterItem.HangarDesc;
      control.AddChild((BasicControl) new TexturedControl(ResourcesManager.Instance.GetSprite(hangarDesc.LockedCopter), hangarDesc.CopterPosition));
      Sprite sprite = ResourcesManager.Instance.GetSprite(hangarDesc.LockedHeader);
      TexturedControl child = new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), 80f));
      control.AddChild((BasicControl) child);
    }

    private void CreateUnlockedCopter(HelicopterItem helicopterItem, CopterControl control)
    {
      CopterLayoutDescription hangarDesc = (CopterLayoutDescription) helicopterItem.HangarDesc;
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite(hangarDesc.Copter), hangarDesc.CopterPosition);
      control.AddChild((BasicControl) child1);
      if ((double) this.HealthPercent > 0.0)
      {
        TexturedControl damagedCopter = new TexturedControl(ResourcesManager.Instance.GetSprite(hangarDesc.CopterDamaged), hangarDesc.CopterPosition);
        control.AddDamagedCopter((BasicControl) damagedCopter);
      }
      Sprite sprite1 = ResourcesManager.Instance.GetSprite(helicopterItem.HangarDesc.HeaderTexture);
      TexturedControl child2 = new TexturedControl(sprite1, new Vector2((float) (400 - sprite1.Bounds.Width / 2), 80f));
      control.AddChild((BasicControl) child2);
      if (helicopterItem.HasFirstWeapon)
      {
        ButtonDesc firstWeapon = hangarDesc.FirstWeapon;
        BasicControl child3 = new BasicControl();
        child3.Position = firstWeapon.ItemPosition;
        child3.AddChild((BasicControl) this._firstWeapon);
        control.AddChild(child3);
      }
      if (helicopterItem.HasSecondWeapon)
      {
        ButtonDesc secondWeapon = hangarDesc.SecondWeapon;
        BasicControl child4 = new BasicControl();
        child4.Position = secondWeapon.ItemPosition;
        child4.AddChild((BasicControl) this._secondWeapon);
        control.AddChild(child4);
      }
      if (helicopterItem.HasUpgradeA)
      {
        ButtonDesc upgradeA = hangarDesc.UpgradeA;
        BasicControl child5 = new BasicControl();
        child5.Position = upgradeA.ItemPosition;
        child5.AddChild((BasicControl) this._upgradeA);
        control.AddChild(child5);
      }
      if (helicopterItem.HasUpgradeB)
      {
        ButtonDesc upgradeB = hangarDesc.UpgradeB;
        BasicControl child6 = new BasicControl();
        child6.Position = upgradeB.ItemPosition;
        child6.AddChild((BasicControl) this._upgradeB);
        control.AddChild(child6);
      }
      if (hangarDesc.Wing != null)
      {
        ButtonDesc wing = hangarDesc.Wing;
        TexturedControl child7 = new TexturedControl(ResourcesManager.Instance.GetSprite(wing.TextureName), wing.TexturePosition);
        control.AddChild((BasicControl) child7);
        if ((double) this.HealthPercent > 0.0)
        {
          TexturedControl damagedWing = new TexturedControl(ResourcesManager.Instance.GetSprite(wing.TextureNameDamaged), wing.TexturePosition);
          control.AddDamagedWing((BasicControl) damagedWing);
        }
      }
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/Slots/lightSlot");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/Slots/lightSlotAnimation");
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("Hangar/Slots/heavySlot");
      Sprite sprite5 = ResourcesManager.Instance.GetSprite("Hangar/Slots/heavySlotAnimation");
      Sprite sprite6 = ResourcesManager.Instance.GetSprite("Hangar/Slots/heavySlotNewElement");
      Sprite sprite7 = ResourcesManager.Instance.GetSprite("Hangar/Slots/lightSlotNewElement");
      if (helicopterItem.HasFirstWeapon)
      {
        ButtonDesc firstWeapon = hangarDesc.FirstWeapon;
        Rectangle entryRectangle = new Rectangle((int) firstWeapon.ItemPosition.X, (int) firstWeapon.ItemPosition.Y, (int) this._firstWeapon.Size.X, (int) this._firstWeapon.Size.Y);
        SpecialSlotTwoStateMenuControl control1 = new SpecialSlotTwoStateMenuControl(sprite2, sprite7, sprite3, new Vector2((float) (entryRectangle.Center.X - sprite2.Bounds.Width / 2), (float) (entryRectangle.Center.Y - sprite2.Bounds.Height / 2)), entryRectangle)
        {
          HasNew = Gamer.Instance.HasNewFirstWeapon.IsInstalled
        };
        control1.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnCopterSlotClick(HangerScreen.Slots.FirstWeapon));
        control.AddSlot(HangerScreen.Slots.FirstWeapon, control1);
      }
      if (helicopterItem.HasSecondWeapon)
      {
        ButtonDesc secondWeapon = hangarDesc.SecondWeapon;
        Rectangle entryRectangle = new Rectangle((int) secondWeapon.ItemPosition.X, (int) secondWeapon.ItemPosition.Y, (int) this._secondWeapon.Size.X, (int) this._secondWeapon.Size.Y);
        SpecialSlotTwoStateMenuControl control2 = new SpecialSlotTwoStateMenuControl(sprite4, sprite6, sprite5, new Vector2((float) (entryRectangle.Center.X - sprite4.Bounds.Width / 2), (float) (entryRectangle.Center.Y - sprite2.Bounds.Height / 2)), entryRectangle)
        {
          HasNew = Gamer.Instance.HasNewSecondWeapon.IsInstalled
        };
        control2.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnCopterSlotClick(HangerScreen.Slots.SecondWeapon));
        control.AddSlot(HangerScreen.Slots.SecondWeapon, control2);
      }
      if (helicopterItem.HasUpgradeA)
      {
        ButtonDesc upgradeA = hangarDesc.UpgradeA;
        Rectangle entryRectangle = new Rectangle((int) upgradeA.ItemPosition.X, (int) upgradeA.ItemPosition.Y, (int) this._upgradeA.Size.X, (int) this._upgradeA.Size.Y);
        SpecialSlotTwoStateMenuControl control3 = new SpecialSlotTwoStateMenuControl(sprite2, sprite7, sprite3, new Vector2((float) (entryRectangle.Center.X - sprite2.Bounds.Width / 2), (float) (entryRectangle.Center.Y - sprite2.Bounds.Height / 2)), entryRectangle)
        {
          HasNew = Gamer.Instance.HasNewUpgrade.IsInstalled
        };
        control3.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnCopterSlotClick(HangerScreen.Slots.UpgradeA));
        control.AddSlot(HangerScreen.Slots.UpgradeA, control3);
      }
      if (helicopterItem.HasUpgradeB)
      {
        ButtonDesc upgradeB = hangarDesc.UpgradeB;
        Rectangle entryRectangle = new Rectangle((int) upgradeB.ItemPosition.X, (int) upgradeB.ItemPosition.Y, (int) this._upgradeB.Size.X, (int) this._upgradeB.Size.Y);
        SpecialSlotTwoStateMenuControl control4 = new SpecialSlotTwoStateMenuControl(sprite2, sprite7, sprite3, new Vector2((float) (entryRectangle.Center.X - sprite2.Bounds.Width / 2), (float) (entryRectangle.Center.Y - sprite2.Bounds.Height / 2)), entryRectangle)
        {
          HasNew = Gamer.Instance.HasNewUpgrade.IsInstalled
        };
        control4.Clicked += (EventHandler<EventArgs>) ((x, y) => this.OnCopterSlotClick(HangerScreen.Slots.UpgradeB));
        control.AddSlot(HangerScreen.Slots.UpgradeB, control4);
      }
      control.RegisterSlot(HangerScreen.Slots.Damage, this._damageBonus);
      control.RegisterSlot(HangerScreen.Slots.Defense, this._defenseBonus);
      control.RegisterSlot(HangerScreen.Slots.Health, this._healthBonus);
      control.SlotsState(true);
    }

    private void UnlockCopter(HelicopterItem item)
    {
      foreach (BasicControl child in this._copters.Children)
      {
        if (((CopterControl) child).Item == item)
        {
          child.RemoveAllChilds();
          this.CreateUnlockedCopter(item, (CopterControl) child);
          break;
        }
      }
    }

    private void UpdateCostLabel(Item item)
    {
      this._costCreditLabel.Text = item.IsBought ? "-" : string.Format("&{0}", (object) Gamer.Instance.GetItemPrice(item));
    }

    private void CreateBottomPanel()
    {
      Sprite sprite = ResourcesManager.Instance.GetSprite("Hangar/sidePanel");
      TexturedControl child = new TexturedControl(sprite, new Vector2(0.0f, (float) (480 - sprite.Bounds.Height)));
      int paddingX = 3;
      int paddingY = 9;
      int spacing = 0;
      child.AddChild(this.CreateLeftSide(paddingX, paddingY, spacing));
      child.AddChild(this.CreateRightSide());
      this._root.AddChild((BasicControl) child);
      this.CreateCenterPanel();
    }

    private BasicControl CreateLeftSide(int paddingX, int paddingY, int spacing)
    {
      PanelControl leftSide = new PanelControl();
      TexturedControl child1 = new TexturedControl(ResourcesManager.Instance.GetSprite("Hangar/costField"), Vector2.Zero);
      PanelControl child2 = new PanelControl();
      TextControl child3 = new TextControl("cost", this._font14, this._colorYellow, Vector2.Zero);
      child2.AddChild((BasicControl) child3);
      this._costCreditLabel = new TextControl("", this._font14, this._colorGreen, Vector2.Zero);
      child2.AddChild((BasicControl) this._costCreditLabel);
      child2.LayoutColumn(7f, 7f, 13f);
      child1.AddChild((BasicControl) child2);
      leftSide.AddChild((BasicControl) child1);
      this._buyButton = new MenuControl(ResourcesManager.Instance.GetSprite("Hangar/butBuy"), ResourcesManager.Instance.GetSprite("Hangar/butBuySelect"), Vector2.Zero);
      this._buyButton.Clicked += new EventHandler<EventArgs>(this.OnBuyButtonClicked);
      this._buyButton.Visible = false;
      leftSide.AddChild((BasicControl) this._buyButton);
      leftSide.LayoutColumn((float) paddingX, (float) paddingY, (float) spacing);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/butEquip");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/butEquipSelect");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/butEquipDisable");
      this._equipButton = new MenuControl(sprite1, sprite2, Vector2.Zero);
      this._equipButton.Clicked += new EventHandler<EventArgs>(this.OnEquipedClicked);
      this._equipButton.Visible = true;
      this._equipButton.Position = this._buyButton.Position;
      this._equipButton.SetDisableButton(sprite3);
      leftSide.AddChild((BasicControl) this._equipButton);
      return (BasicControl) leftSide;
    }

    private BasicControl CreateRightSide()
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/butFight");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/butFightSelect");
      MenuControl rightSide = new MenuControl(sprite1, sprite2, new Vector2((float) (800 - sprite1.Bounds.Width - 4), 12f));
      rightSide.Clicked += new EventHandler<EventArgs>(this.OnFightClicked);
      return (BasicControl) rightSide;
    }

    private void OnBack(EventArgs e)
    {
      EventHandler back = this.Back;
      if (back == null)
        return;
      back((object) this, e);
    }

    public override void OnBackButton()
    {
      this.ExitScreen();
      this.NotifyHideHangar();
      this.OnBack(EventArgs.Empty);
    }

    private void OnBuyButtonClicked(object sender, EventArgs e)
    {
      Item obj;
      if (!this._copters.CurrentHelicopter.IsBought)
      {
        obj = (Item) this._copters.CurrentHelicopter;
      }
      else
      {
        obj = this.CurrentPanelItem;
        if (obj is AmmunitionItem && this.IsAmunitionAlreadyBought(obj))
          return;
      }
      if (Gamer.Instance.Money.TryPaidMoney((float) Gamer.Instance.GetItemPrice(obj)))
      {
        this.ScreenManager.AudioManager.HangarSounds.PlayBought();
        if (!(obj is AmmunitionItem))
        {
          obj.IsBought = true;
          if (Gamer.Instance.AchievementManager.IsAllItemsBought)
          {
            Gamer.Instance.AchievementManager.GrantAchievement("QuartermasterMedal");
            this.ShowAchievements();
          }
        }
        else
          this.NotifyBuyAmmunition((AmmunitionItem) obj);
        if (obj is HelicopterItem)
        {
          this.UnlockCopter((HelicopterItem) obj);
          this.Repair();
          this.OnCopterChanged(sender, e);
        }
        else
          this.OnEquipedClicked(sender, e);
        this.EquipBuyButtonStates(this._currentPanel.CurrentItem);
      }
      else
        this.ShowInAppPopup();
    }

    private void OnEquipedClicked(object sender, EventArgs e)
    {
      if (!this._copters.CurrentHelicopter.IsBought)
        return;
      switch (this.State)
      {
        case HangerScreen.Slots.FirstWeapon:
        case HangerScreen.Slots.SecondWeapon:
          this.EquipWeapon();
          break;
        case HangerScreen.Slots.UpgradeA:
        case HangerScreen.Slots.UpgradeB:
          this.EquipUpgrade();
          break;
        case HangerScreen.Slots.Defense:
        case HangerScreen.Slots.Damage:
        case HangerScreen.Slots.Health:
          this.EquipAmunition();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.CreateStats();
    }

    private void InvokeFight(EventArgs e)
    {
      EventHandler fight = this.Fight;
      if (fight == null)
        return;
      fight((object) this, e);
    }

    private void OnFightClicked(object x, EventArgs y)
    {
      this.ChoseCopter();
      this.NotifyHideHangar();
      this.InvokeFight(EventArgs.Empty);
    }

    private void OnItemChanged(object sender, EventArgs e)
    {
      this.UpdateCostLabel(this._currentPanel.CurrentItem);
      this.EquipBuyButtonStates(this._currentPanel.CurrentItem);
    }

    private void OnNewCopter()
    {
      EventHandler buyNewCopter = this.BuyNewCopter;
      if (buyNewCopter == null)
        return;
      buyNewCopter((object) this, EventArgs.Empty);
    }

    private void OnStartItemChanged(object sender, EventArgs e)
    {
    }

    private void CreateCenterPanel()
    {
      Sprite sprite = ResourcesManager.Instance.GetSprite("Hangar/centerPanel");
      this._root.AddChild((BasicControl) new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), (float) (480 - sprite.Bounds.Height))));
      this.CreateWeaponPanel();
      this.CreateAmunitionPanel();
      this.CreateUpgradePanel();
      this._firstWeaponPanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
      this._secondWeaponPanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
      this._defensePanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
      this._damagePanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
      this._healthPanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
      this._upgradePanel.StartItemChanged += new EventHandler(this.SoundOnSlide);
    }

    private BasicControl CreateDiscountLabel()
    {
      Sprite sprite;
      switch (Gamer.Instance.Rank)
      {
        case Rank.Pilot:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_piot");
          break;
        case Rank.Sergeant:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_rank_sergeant");
          break;
        case Rank.Lieutenant:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_rank_lieutenant");
          break;
        case Rank.Captain:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_rank_captain");
          break;
        case Rank.Major:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_rank_major");
          break;
        case Rank.Colonel:
          sprite = ResourcesManager.Instance.GetSprite("Hangar/Rank/hungar_rank_colonel");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      TexturedControl discountLabel = new TexturedControl(sprite, Vector2.Zero);
      discountLabel.ImageSize = discountLabel.Size * 0.7f;
      discountLabel.AddChild((BasicControl) new TextControl(string.Format("{0}%\ndiscount", (object) (float) ((double) Gamer.Instance.Discount * 100.0)), this._font8, this._colorYellow, new Vector2(68f, discountLabel.ImageSize.Y / 2f))
      {
        Origin = new Vector2(0.5f, 0.0f),
        Centered = true
      });
      TexturedControl texturedControl = discountLabel;
      texturedControl.Size = texturedControl.Size + discountLabel.Children.First<BasicControl>().Size;
      return (BasicControl) discountLabel;
    }

    private void CreateTopPanel()
    {
      InterfaceTopPanel.TexturesPack texturesPack = new InterfaceTopPanel.TexturesPack();
      texturesPack.Left = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/leftTopPanel2");
      texturesPack.Right = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/rightTopPanel2");
      texturesPack.Center = ResourcesManager.Instance.GetSprite("UI/Top Panel/Level2/centerTopPanel2");
      Vector2 zero = Vector2.Zero;
      Vector2 position1 = new Vector2((float) (800 - texturesPack.Right.Bounds.Width), 0.0f);
      Vector2 position2 = new Vector2((float) (400 - texturesPack.Center.Bounds.Width / 2), 0.0f);
      BasicControl child1 = new BasicControl();
      TexturedControl child2 = new TexturedControl(texturesPack.Left, zero);
      TexturedControl child3 = new TexturedControl(texturesPack.Right, position1);
      TextControl child4 = new TextControl("credits", this._font11, this._colorYellow, new Vector2(2f, 2f));
      this._playerCreditsLabel = new TextControl(Gamer.Instance.Money.Count.ToString((IFormatProvider) CultureInfo.InvariantCulture), this._font14, this._colorYellow, new Vector2(0.0f, child4.Size.Y));
      Gamer.Instance.Money.MoneyChanged += (EventHandler) ((x, y) => this._playerCreditsLabel.Text = Gamer.Instance.Money.Count.ToString((IFormatProvider) CultureInfo.InvariantCulture));
      child4.AddChild((BasicControl) this._playerCreditsLabel);
      child2.AddChild((BasicControl) child4);
      child1.AddChild((BasicControl) child2);
      BasicControl discountLabel = this.CreateDiscountLabel();
      discountLabel.Position = new Vector2(37f, 3f);
      child3.AddChild(discountLabel);
      this._root.AddChild((BasicControl) child3);
      TexturedControl child5 = new TexturedControl(texturesPack.Center, position2);
      child1.AddChild((BasicControl) child5);
      this._root.AddChild(child1);
      MenuControl child6 = new MenuControl(ResourcesManager.Instance.GetSprite("Hangar/butAddCredits"), ResourcesManager.Instance.GetSprite("Hangar/butAddCreditsSelect"), new Vector2(118f, 0.0f));
      child6.EntryPosition = new Rectangle(0, 0, 200, 50);
      child6.Clicked += (EventHandler<EventArgs>) ((x, y) => this.ShowInAppPopup());
      this._root.AddChild((BasicControl) child6);
    }

    public void AddHelpButton()
    {
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/butHelpHungarSelect");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/butHelpHungar");
      MenuControl child = new MenuControl(sprite1, sprite2, new Vector2((float) (682 - sprite2.Bounds.Width), 0.0f));
      child.EntryPosition = new Rectangle(600, 0, 200, 50);
      child.Clicked += (EventHandler<EventArgs>) ((x, y) => this.ScreenManager.AddScreen((GameScreen) new TutorialPopup(TutorialPopup.Tutorial.Challenge)));
      this._root.AddChild((BasicControl) child);
    }

    private void EquipAmunition()
    {
      AmmunitionItem currentItem = (AmmunitionItem) this._currentPanel.CurrentItem;
      if ((currentItem.Type & AmunitionType.Health) != (AmunitionType) 0)
      {
        float num;
        switch (currentItem.Type)
        {
          case AmunitionType.HealthPack50:
            num = 0.5f;
            break;
          case AmunitionType.HealthPack100:
            num = 1f;
            break;
          default:
            throw new Exception(string.Format("Unknown Health Amunition Type '{0}'.", (object) currentItem.Type));
        }
        if (!Gamer.Instance.HealthBonus.IsInstalled)
          Gamer.Instance.HealthBonus.Item = new HealthAmmunitionItem();
        Gamer.Instance.HealthBonus.Item.Volume += num;
        Gamer.Instance.HealthBonus.Item.HangarDesc = currentItem.HangarDesc;
        this.UpdateHealthIcon(Gamer.Instance.HealthBonus.Item);
      }
      if ((currentItem.Type & AmunitionType.DamageIncrease50) != (AmunitionType) 0)
      {
        Gamer.Instance.DamageBonus.Item = currentItem;
        this.UpdateDamageBonus(currentItem);
      }
      if ((currentItem.Type & AmunitionType.Defense) == (AmunitionType) 0)
        return;
      Gamer.Instance.DefenseBonus.Item = currentItem;
      this.UpdateDefenseBonus(currentItem);
    }

    private void EquipBuyButtonStates(Item item)
    {
      this._equipButton.Visible = item.IsBought;
      this._buyButton.Visible = !this._equipButton.Visible;
      if (!item.IsBought)
        return;
      this._equipButton.IsDisabled = this.IsInstalled(item);
    }

    private void EquipUpgrade()
    {
      UpgradeItem currentItem = (UpgradeItem) this._upgradePanel.CurrentItem;
      switch (this._copters.Slot)
      {
        case HangerScreen.Slots.UpgradeA:
          if (this._copters.CurrentHelicopter.HasUpgradeB && Gamer.Instance.UpgradeB.Item == currentItem)
            break;
          Gamer.Instance.UpgradeA.Item = currentItem;
          this._upgradeA.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) currentItem.HangarDesc).OnCopterTexture);
          this._upgradeA.Sprite.Alpha = 1f;
          this._upgradeA.Item = (Item) currentItem;
          if (!this._copters.CurrentHelicopter.HasUpgradeB)
            break;
          this.OnCopterSlotClick(HangerScreen.Slots.UpgradeB);
          break;
        case HangerScreen.Slots.UpgradeB:
          if (Gamer.Instance.UpgradeA.Item == currentItem)
            break;
          Gamer.Instance.UpgradeB.Item = currentItem;
          this._upgradeB.Sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) currentItem.HangarDesc).OnCopterTexture);
          this._upgradeB.Sprite.Alpha = 1f;
          this._upgradeB.Item = (Item) currentItem;
          if (!this._copters.CurrentHelicopter.HasUpgradeA)
            break;
          this.OnCopterSlotClick(HangerScreen.Slots.UpgradeA);
          break;
      }
    }

    private void EquipWeapon()
    {
      this.ScreenManager.AudioManager.HangarSounds.PlayEquiped();
      WeaponItem currentItem = this._currentPanel.CurrentItem as WeaponItem;
      Sprite sprite = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) currentItem.HangarDesc).OnCopterTexture);
      if (currentItem.Slot == Slot.First)
      {
        Gamer.Instance.FirstWeapon.Item = currentItem;
        this._firstWeapon.Sprite = sprite;
        this._firstWeapon.Sprite.Alpha = 1f;
        this._firstWeapon.Item = (Item) currentItem;
        if (!this._copters.CurrentHelicopter.HasFirstWeapon)
          return;
        this._copters.Slot = HangerScreen.Slots.FirstWeapon;
      }
      else
      {
        Gamer.Instance.SecondWeapon.Item = currentItem;
        this._secondWeapon.Sprite = sprite;
        this._secondWeapon.Sprite.Alpha = 1f;
        this._secondWeapon.Item = (Item) currentItem;
        if (!this._copters.CurrentHelicopter.HasSecondWeapon)
          return;
        this._copters.Slot = HangerScreen.Slots.SecondWeapon;
      }
    }

    private bool IsAmunitionAlreadyBought(Item item)
    {
      return (((AmmunitionItem) item).Type & AmunitionType.DamageIncrease50) != (AmunitionType) 0 && Gamer.Instance.DamageBonus.Item == item || (((AmmunitionItem) item).Type & AmunitionType.Defense) != (AmunitionType) 0 && Gamer.Instance.DefenseBonus.Item == item;
    }

    protected bool IsInstalled(Item item)
    {
      return item == Gamer.Instance.FirstWeapon.Item || item == Gamer.Instance.SecondWeapon.Item || item == Gamer.Instance.UpgradeA.Item || item == Gamer.Instance.UpgradeB.Item;
    }

    private void ShowAchievements()
    {
      while (Gamer.Instance.AchievementManager.UnshownAchievement.Count > 0)
      {
        Achievement achievement = Gamer.Instance.AchievementManager.UnshownAchievement.First<Achievement>();
        NewAchievementPopup screen = new NewAchievementPopup();
        screen.Init(achievement);
        this.ScreenManager.AddScreen((GameScreen) screen);
        Gamer.Instance.AchievementManager.UnshownAchievement.RemoveAt(0);
      }
    }

    private void ShowInAppPopup() => this.ScreenManager.AddScreen((GameScreen) new InAppPopup());

    private void SoundOnSlide(object sender, EventArgs e)
    {
      this.ScreenManager.AudioManager.HangarSounds.PlayBottomMenuSlide();
    }

    public float HealthPercent { get; set; }

    private void OnRepairButtonClicked(object sender, EventArgs e)
    {
      if (Gamer.Instance.Money.TryPaidMoney((float) this.CalculateCost(this.HealthPercent)))
      {
        this.Repair();
        this.ScreenManager.AudioManager.HangarSounds.PlayRepair();
      }
      else
        this.ShowInAppPopup();
    }

    private void Repair()
    {
      if (!this._root.Children.Contains((BasicControl) this._repairButton))
        return;
      this.OnNewCopter();
      foreach (CopterControl child in this._copters.Children)
        child.RemoveDamaged();
      this.HealthPercent = 0.0f;
      this._root.RemoveChild((BasicControl) this._repairButton);
    }

    private int CalculateCost(float percentHealth)
    {
      return (int) ((1.0 - (double) percentHealth) * 100.0 * 20.0);
    }

    private void CreateRepairButton()
    {
      if ((double) this.HealthPercent <= 0.0)
        return;
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/butRepeare");
      Vector2 position = new Vector2((float) (800 - sprite1.Bounds.Width - 6), 278f);
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/butRepeareSelect");
      this._repairButton = new MenuControl(sprite1, sprite2, position);
      this._repairButton.Clicked += new EventHandler<EventArgs>(this.OnRepairButtonClicked);
      this._repairButton.AddChild((BasicControl) new TextControl("&" + (object) this.CalculateCost(this.HealthPercent), this._font11, this._colorYellow, new Vector2((float) sprite1.Bounds.Width / 2f, 0.0f))
      {
        Origin = new Vector2(0.5f, 1f)
      });
      this._root.AddChild((BasicControl) this._repairButton);
    }

    private int CalculateDamage()
    {
      int num1 = 0;
      if (Gamer.Instance.FirstWeapon.IsInstalled)
      {
        WeaponItem weaponItem = Gamer.Instance.FirstWeapon.Item;
        float num2 = 0.0f;
        switch (Gamer.Instance.FirstWeapon.Item.WeaponType)
        {
          case WeaponType.SingleMachineGun:
            num2 = 20f;
            break;
          case WeaponType.DualMachineGun:
            num2 = 40f;
            break;
          case WeaponType.PlasmaGun:
            num2 = 60f;
            break;
          case WeaponType.Vulcan:
            num2 = 90f;
            break;
        }
        if (Gamer.Instance.UpgradeA.IsInstalled && Gamer.Instance.CurrentHelicopter.Item.HasUpgradeA)
        {
          switch (Gamer.Instance.UpgradeA.Item.Type)
          {
            case UpgradeType.BulletControlSystem:
              if (weaponItem.WeaponType == WeaponType.SingleMachineGun || weaponItem.WeaponType == WeaponType.DualMachineGun || weaponItem.WeaponType == WeaponType.Vulcan)
              {
                num2 *= 1.3f;
                break;
              }
              break;
            case UpgradeType.HotPlasmaModule:
              if (weaponItem.WeaponType == WeaponType.PlasmaGun)
              {
                num2 *= 1.3f;
                break;
              }
              break;
          }
        }
        if (Gamer.Instance.UpgradeB.IsInstalled && Gamer.Instance.CurrentHelicopter.Item.HasUpgradeB)
        {
          switch (Gamer.Instance.UpgradeB.Item.Type)
          {
            case UpgradeType.BulletControlSystem:
              if (weaponItem.WeaponType == WeaponType.SingleMachineGun || weaponItem.WeaponType == WeaponType.DualMachineGun || weaponItem.WeaponType == WeaponType.Vulcan)
              {
                num2 *= 1.3f;
                break;
              }
              break;
            case UpgradeType.HotPlasmaModule:
              if (weaponItem.WeaponType == WeaponType.PlasmaGun)
              {
                num2 *= 1.3f;
                break;
              }
              break;
          }
        }
        num1 += (int) num2;
      }
      if (Gamer.Instance.SecondWeapon.IsInstalled && Gamer.Instance.CurrentHelicopter.Item.HasSecondWeapon)
      {
        WeaponItem weaponItem = Gamer.Instance.SecondWeapon.Item;
        float num3 = 0.0f;
        switch (Gamer.Instance.SecondWeapon.Item.WeaponType)
        {
          case WeaponType.RocketLauncher:
            num3 = 30f;
            break;
          case WeaponType.DualRocketLauncher:
            num3 = 60f;
            break;
          case WeaponType.CasseteRocket:
            num3 = 90f;
            break;
          case WeaponType.HomingRocket:
            num3 = 30f;
            break;
        }
        if (Gamer.Instance.UpgradeA.IsInstalled && Gamer.Instance.CurrentHelicopter.Item.HasUpgradeA)
        {
          switch (Gamer.Instance.UpgradeA.Item.Type)
          {
            case UpgradeType.TargetAssistentSystem:
              if (weaponItem.WeaponType == WeaponType.HomingRocket)
              {
                num3 *= 2f;
                break;
              }
              break;
            case UpgradeType.EnhanchedRechargeSystem:
              num3 /= 0.8f;
              break;
            case UpgradeType.UpgradedWarhead:
              if (weaponItem.WeaponType == WeaponType.RocketLauncher || weaponItem.WeaponType == WeaponType.DualRocketLauncher || weaponItem.WeaponType == WeaponType.HomingRocket)
              {
                num3 *= 2f;
                break;
              }
              break;
          }
        }
        if (Gamer.Instance.UpgradeB.IsInstalled && Gamer.Instance.CurrentHelicopter.Item.HasUpgradeB)
        {
          switch (Gamer.Instance.UpgradeB.Item.Type)
          {
            case UpgradeType.TargetAssistentSystem:
              if (weaponItem.WeaponType == WeaponType.HomingRocket)
              {
                num3 *= 2f;
                break;
              }
              break;
            case UpgradeType.EnhanchedRechargeSystem:
              num3 /= 0.8f;
              break;
            case UpgradeType.UpgradedWarhead:
              if (weaponItem.WeaponType == WeaponType.RocketLauncher || weaponItem.WeaponType == WeaponType.DualRocketLauncher || weaponItem.WeaponType == WeaponType.HomingRocket)
              {
                num3 *= 2f;
                break;
              }
              break;
          }
        }
        num1 += (int) num3;
      }
      return (int) MathHelper.Clamp((float) ((double) num1 / 200.0 * 10.0), 1f, 10f);
    }

    private int CalculateEnergy()
    {
      return (int) MathHelper.Clamp((float) ((double) Gamer.Instance.GetPlayer(new GameWorld()).Energy / 2500.0 * 10.0), 1f, 10f);
    }

    public void CreateStats()
    {
      if (this._stats.Children != null)
        this._stats.RemoveAllChilds();
      if (!this._copters.CurrentHelicopter.IsBought)
        return;
      PanelControl child = new PanelControl();
      child.AddChild(this.DamageState());
      child.AddChild(this.EnergyState());
      child.LayoutRow(174f, 302f, 49f);
      this._stats.AddChild((BasicControl) child);
      if (this._stats.Parent == this._root)
        this._root.RemoveChild(this._stats);
      this._root.AddChild(this._stats);
    }

    public BasicControl DamageState()
    {
      PanelControl panelControl = new PanelControl();
      int damage = this.CalculateDamage();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/empltySlot");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/fullSlot");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/damage");
      TexturedControl child = new TexturedControl(sprite3, Vector2.Zero);
      TexturedControl texturedControl = child;
      texturedControl.Size = texturedControl.Size + new Vector2(5f, 0.0f);
      panelControl.AddChild((BasicControl) child, 0);
      for (int index = 0; index < damage; ++index)
        panelControl.AddChild((BasicControl) new TexturedControl(sprite2, Vector2.Zero));
      for (int index = damage; index < 10; ++index)
        panelControl.AddChild((BasicControl) new TexturedControl(sprite1, Vector2.Zero));
      panelControl.LayoutRow(0.0f, 0.0f, -5f);
      child.Position = new Vector2(0.0f, (float) (sprite1.Bounds.Height / 2 - sprite3.Bounds.Height / 2));
      return (BasicControl) panelControl;
    }

    public BasicControl EnergyState()
    {
      PanelControl panelControl = new PanelControl();
      int energy = this.CalculateEnergy();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("Hangar/empltySlot");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("Hangar/fullSlot");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("Hangar/energy");
      TexturedControl child = new TexturedControl(sprite3, Vector2.Zero);
      TexturedControl texturedControl = child;
      texturedControl.Size = texturedControl.Size + new Vector2(5f, 0.0f);
      panelControl.AddChild((BasicControl) child, 0);
      for (int index = 0; index < energy; ++index)
        panelControl.AddChild((BasicControl) new TexturedControl(sprite2, Vector2.Zero));
      for (int index = energy; index < 10; ++index)
        panelControl.AddChild((BasicControl) new TexturedControl(sprite1, Vector2.Zero));
      panelControl.LayoutRow(0.0f, 0.0f, -5f);
      child.Position = new Vector2(0.0f, (float) (sprite1.Bounds.Height / 2 - sprite3.Bounds.Height / 2));
      return (BasicControl) panelControl;
    }

    private void NotifyBuyAmmunition(AmmunitionItem item)
    {
      Api.LogEvent(FlurryEvents.BuyAmmunition, new List<Parameter>()
      {
        ParametersFactory.GetAmmunitionParam(item)
      });
    }

    private void NotifyHideHangar() => Api.EndTimedEvent(FlurryEvents.HangarSession);

    private void NotifyOpenHangar() => Api.LogEvent(FlurryEvents.HangarSession, true);

    public enum Slots
    {
      FirstWeapon,
      SecondWeapon,
      UpgradeA,
      UpgradeB,
      Defense,
      Damage,
      Health,
    }
  }
}
