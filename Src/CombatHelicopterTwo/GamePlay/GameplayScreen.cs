// Decompiled with JetBrains decompiler
// Type: Helicopter.GamePlay.GameplayScreen
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items.Ammunition;
using Helicopter.Items.HangarDesc;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.Sounds;
using Helicopter.Model.SpriteObjects;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Model.WorldObjects;
using Helicopter.Model.WorldObjects.Instances;
using Helicopter.Model.WorldObjects.Instances.Weapons;
using Helicopter.Model.WorldObjects.Modifiers;
using Helicopter.Playing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.GamePlay
{
  internal class GameplayScreen : GameScreen
  {
    private readonly Tweener _fadeawayTweener = new Tweener(0.0f, 0.0f, 0.0f, new TweeningFunction(Linear.EaseIn));
    public TimeSpan FlyTime = TimeSpan.Zero;
    public int Score;
    private BasicControl _controlsManager;
    private Level _level;
    private LevelProgressControl _levelProgress;
    private LifeBarSprite _lifeLifeBar;
    private Sprite _tapToScreen;
    private Vector2 _tapToScreenPosition;
    private InterfaceTopPanel _topPanel;
    private Tweener _ammoUseTweener = new Tweener(0.0f, 0.0f, 0.0f, new TweeningFunction(Linear.EaseIn));
    private PanelControl _amunitionPanel;
    private SpecialSlotTwoStateMenuControl _damageBonus;
    private SpecialSlotTwoStateMenuControl _defenseBonus;
    private SpecialSlotTwoStateMenuControl _healthBonus;
    private static readonly Rectangle _allScreenRectangle = new Rectangle(0, 0, 800, 480);
    private Color _colorLoseFadeaway;
    private SpriteFont _font12;
    private Color _colorYellow;

    public event EventHandler Landing;

    public event EventHandler Pause;

    public event EventHandler PlayerLose;

    public event EventHandler PlayerWin;

    public bool IsBossBattle { get; set; }

    public Level Level
    {
      get => this._level;
      set
      {
        if (this._level == value)
          return;
        this._level = value;
        this._level.StateChanged += new EventHandler<StateChangeEventArgs<LevelState>>(this.OnLevelStateChanged);
      }
    }

    public bool PauseAtActivate { get; set; }

    public override void Update(GameTime gameTime)
    {
      switch (this.Level.State)
      {
        case LevelState.Play:
          if (this.PauseAtActivate)
          {
            this.PauseAtActivate = false;
            this.PauseGame();
          }
          this.FlyTime += gameTime.ElapsedGameTime;
          this.Level.Update(gameTime);
          this._levelProgress.Progress = this.IsBossBattle ? this.Level.BossHealth : this.Level.ProgressPercent;
          this._lifeLifeBar.Full = (double) this.Level.Player.EnergyPercent;
          break;
        case LevelState.Lose:
          this.Level.Update(gameTime);
          break;
      }
      float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
      if (this._fadeawayTweener.Running)
      {
        this._fadeawayTweener.Update(totalSeconds);
        this._colorLoseFadeaway = Color.Black * this._fadeawayTweener.Position;
      }
      this.UpdateControls();
      this._ammoUseTweener.Update(totalSeconds);
      this._controlsManager.Update(gameTime);
      base.Update(gameTime);
    }

    public override void Draw(DrawContext drawContext)
    {
      this.Level.Draw(drawContext);
      this._controlsManager.Draw(drawContext);
      if (this.Level.Mode == GameMode.Challenge)
        drawContext.SpriteBatch.DrawString(this._font12, (GameProcess.Instance.ChallengeGameSession.EpisodeNumber + 1).ToString((IFormatProvider) CultureInfo.InvariantCulture), new Vector2(480f, 460f), this._colorYellow);
      switch (this.Level.State)
      {
        case LevelState.Preparation:
          this.DrawBeforeStartGame();
          break;
        case LevelState.Lose:
          drawContext.SpriteBatch.Draw(drawContext.BlankTexture, GameplayScreen._allScreenRectangle, this._colorLoseFadeaway);
          break;
      }
      if (this.IsFullyLoaded)
        return;
      this.IsFullyLoaded = true;
    }

    public override void HandleInput(InputState input)
    {
      bool flag = false;
      if (this.Level.State == LevelState.Play)
        flag = true;
      else if (this.Level.State == LevelState.Preparation)
      {
        using (TouchCollection.Enumerator enumerator = input.TouchState.GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            if ((double) enumerator.Current.Position.Y > 86.0)
              this.Level.Play();
            else
              flag = true;
          }
        }
      }
      if (!flag)
        return;
      this._controlsManager.HandleInput(input);
    }

    public override void LoadContent()
    {
      HelicopterGame.SaveSettings();
      ResourcesManager.Instance.LoadFromFolder("Effects");
      this._font12 = ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12");
      this._colorYellow = new Color(254, 224, 23);
      this.Level.ScreenManager = this.ScreenManager;
      this.Level.LoadLevelContent();
      this.Level.Init();
      this.Level.Landing += new EventHandler(this.OnLevelLanding);
      this.Level.UnitAdded += new EventHandler<EnemyAnnihilationEventArgs>(this.OnUnitAdded);
      this._tapToScreen = ResourcesManager.Instance.GetSprite("UI/tapToStart");
      this._tapToScreenPosition = new Vector2((float) (this.ScreenManager.GraphicsDevice.Viewport.Bounds.Center.X - this._tapToScreen.Bounds.Width / 2), (float) (this.ScreenManager.GraphicsDevice.Viewport.Bounds.Center.Y - this._tapToScreen.Bounds.Height / 2));
      this.InitControls();
      base.LoadContent();
      this._level.UnitHitted += new EventHandler<EventArgs>(this.ScreenManager.AudioManager.GameplaySounds.OnUnitHitted);
      this._level.UnitFire += new EventHandler<WeaponEventArgs>(this.ScreenManager.AudioManager.GameplaySounds.OnFire);
      this._level.EpisodeChanged += new EventHandler<EventArgs>(this.OnEpisodeChanged);
      if (this.Level.Player.ExternalSupply != null)
        this.InitHealthIcon();
      this.CreateAmunitionIcons();
      this.SetScoreCount(this.Score);
    }

    private void OnUnitAdded(object sender, EnemyAnnihilationEventArgs e)
    {
      if (e.Unit is Copter)
      {
        foreach (Weapon weapon in ((Copter) e.Unit).Weapons)
          this.ConfigureWeaponSounds(weapon);
      }
      if (!(e.Unit is MothershipCopter))
        return;
      foreach (Weapon weapon in ((Copter) e.Unit).Weapons)
        this.ConfigureWeaponSounds(weapon);
    }

    public override void UnloadContent()
    {
      this.Level.ReleaseAll();
      Audio.StopAllSounds();
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
    }

    public void InvokeLanding()
    {
      EventHandler landing = this.Landing;
      if (landing == null)
        return;
      landing((object) this, EventArgs.Empty);
    }

    public void InvokePause()
    {
      EventHandler pause = this.Pause;
      if (pause == null)
        return;
      pause((object) this, EventArgs.Empty);
    }

    private void InvokePlayerLose()
    {
      EventHandler playerLose = this.PlayerLose;
      if (playerLose == null)
        return;
      playerLose((object) this, EventArgs.Empty);
    }

    private void InvokePlayerWin()
    {
      EventHandler playerWin = this.PlayerWin;
      if (playerWin == null)
        return;
      playerWin((object) this, EventArgs.Empty);
    }

    public override void OnBackButton() => this.PauseGame();

    private void OnEpisodeChanged(object sender, EventArgs e)
    {
      if (this._amunitionPanel == null)
        return;
      this._amunitionPanel.RemoveChild((BasicControl) this._damageBonus);
      this._amunitionPanel.RemoveChild((BasicControl) this._defenseBonus);
      this._amunitionPanel.LayoutRow(136f, 373f, 0.0f);
    }

    public void OnFireDown(object sender, EventArgs e) => this.Level.FireStart(0);

    public void OnFireDown2(object sender, EventArgs e) => this.Level.FireStart(1);

    public void OnFireUp(object sender, EventArgs e) => this.Level.FireStop(0);

    public void OnFireUp2(object sender, EventArgs e) => this.Level.FireStop(1);

    private void OnLevelLanding(object sender, EventArgs e) => this.InvokeLanding();

    private void OnLevelStateChanged(object sender, StateChangeEventArgs<LevelState> e)
    {
      if (e.NextState == LevelState.Lose)
      {
        Audio.StopAllSounds();
        this.ScreenManager.AudioManager.GameplaySounds.PlayLose();
        this._fadeawayTweener.Init(0.0f, 1f, 1f, new TweeningFunction(Linear.EaseIn));
        this._fadeawayTweener.Ended += (EventHandler<EventArgs>) ((x, y) => this.InvokePlayerLose());
        this._fadeawayTweener.Start();
      }
      if (e.NextState != LevelState.Win)
        return;
      this.InvokePlayerWin();
    }

    private void OnPauseClicked(object sender, EventArgs e) => this.PauseGame();

    public void OnUpliftingDown(object sender, EventArgs e) => this.Level.StartUpPlayer();

    public void OnUpliftingUp(object sender, EventArgs e) => this.Level.StopUpPlayer();

    private void ConfigureWeaponSounds(Weapon weapon)
    {
      if (weapon is VulcanWeapon)
      {
        (weapon as VulcanWeapon).ShootingState += new EventHandler(this.OnVulcanShooting);
        (weapon as VulcanWeapon).FireAttemptWhileRecharging += new EventHandler(this.OnVulcanFireAttemptWhileRecharging);
      }
      if (weapon is PlasmaGunWeapon)
      {
        (weapon as PlasmaGunWeapon).ShootingState += new EventHandler(this.OnPlasmaGunShooting);
        (weapon as PlasmaGunWeapon).ShootingAfter125 += new EventHandler(this.OnPlasmaGunShootingAfter125);
        (weapon as PlasmaGunWeapon).EnemyCaptured += (EventHandler) ((x, y) => this.ScreenManager.AudioManager.GameplaySounds.PlayPlasmaGunHitting((object) weapon));
        (weapon as PlasmaGunWeapon).CaptureLost += (EventHandler) ((x, y) => this.ScreenManager.AudioManager.GameplaySounds.StopPlasmaGunHitting((object) weapon));
      }
      if (!(weapon is ShieldWeapon))
        return;
      (weapon as ShieldWeapon).ShootingState += new EventHandler(this.OnShieldShooting);
      (weapon as ShieldWeapon).FireAttemptWhileRecharging += new EventHandler(this.OnShieldFireAttemptWhileRecharging);
    }

    private void CreateBossHealth()
    {
      this._levelProgress = new LevelProgressControl(ResourcesManager.Instance.GetSprite("UI/BossHealth/mapCompleteLine"), ResourcesManager.Instance.GetSprite("UI/BossHealth/mapLine"), (Sprite) null, new Vector2(156f, 466f));
      this._controlsManager.AddChild((BasicControl) this._levelProgress);
    }

    private void CreateMiniMap(bool isWhite)
    {
      if (isWhite)
      {
        this._levelProgress = new LevelProgressControl(ResourcesManager.Instance.GetSprite("UI/MiniMap/Light/mapCompleteLine"), ResourcesManager.Instance.GetSprite("UI/MiniMap/Light/mapLine"), ResourcesManager.Instance.GetSprite("UI/MiniMap/Light/copter"), new Vector2(130f, 466f));
        this._controlsManager.AddChild((BasicControl) this._levelProgress);
      }
      else
      {
        this._levelProgress = new LevelProgressControl(ResourcesManager.Instance.GetSprite("UI/MiniMap/Dark/mapCompleteLine"), ResourcesManager.Instance.GetSprite("UI/MiniMap/Dark/mapLine"), ResourcesManager.Instance.GetSprite("UI/MiniMap/Dark/copter"), new Vector2(130f, 466f));
        this._controlsManager.AddChild((BasicControl) this._levelProgress);
      }
    }

    private void DrawBeforeStartGame()
    {
      this._tapToScreen.Draw(this.ScreenManager.SpriteBatch, this._tapToScreenPosition);
    }

    private void InitControls()
    {
      this._controlsManager = new BasicControl();
      this._topPanel = new InterfaceTopPanel();
      this._topPanel.Initialize(this.Level.Mode == GameMode.Story ? this.Level.WorldType : WorldType.LandingZone);
      this._controlsManager.AddChild((BasicControl) this._topPanel);
      this._topPanel.Pause += new EventHandler(this.OnPauseClicked);
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("UI/butUp");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("UI/butUpSelect");
      Control child = new Control(sprite1, new Vector2(0.0f, 320f), new Rectangle(0, 160, 320, 320));
      child.PressedSprite = sprite2;
      child.Down += new EventHandler<EventArgs>(this.OnUpliftingDown);
      child.Up += new EventHandler<EventArgs>(this.OnUpliftingUp);
      this._controlsManager.AddChild((BasicControl) child);
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("UI/levelBarFied");
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("UI/levelBar");
      Sprite sprite5 = ResourcesManager.Instance.GetSprite("UI/levelBarLine");
      this._lifeLifeBar = new LifeBarSprite(new Rectangle(116, 422, 226, 38), sprite4, sprite3, sprite5);
      this._controlsManager.AddChild((BasicControl) this._lifeLifeBar);
      SmartPlayer player = this.Level.Player;
      if (player.Weapons[0] != null)
      {
        Weapon weapon = player.Weapons[0];
        FireControl buttonForWeapon = FireControlFactory.GetButtonForWeapon(weapon, new Vector2(640f, 320f), new Rectangle(640, 320, 160, 160));
        buttonForWeapon.Down += new EventHandler<EventArgs>(this.OnFireDown);
        buttonForWeapon.Up += new EventHandler<EventArgs>(this.OnFireUp);
        this._controlsManager.AddChild((BasicControl) buttonForWeapon);
        this.ConfigureWeaponSounds(weapon);
      }
      if (player.Weapons[1] != null)
      {
        Weapon weapon = player.Weapons[1];
        FireControl buttonForWeapon = FireControlFactory.GetButtonForWeapon(weapon, new Vector2(480f, 320f), new Rectangle(480, 320, 160, 160));
        buttonForWeapon.Down += new EventHandler<EventArgs>(this.OnFireDown2);
        buttonForWeapon.Up += new EventHandler<EventArgs>(this.OnFireUp2);
        this._controlsManager.AddChild((BasicControl) buttonForWeapon);
        this.ConfigureWeaponSounds(weapon);
      }
      if (this.IsBossBattle)
        this.CreateBossHealth();
      else if (this.Level.WorldType == WorldType.Ice || this.Level.WorldType == WorldType.EnemyBase)
        this.CreateMiniMap(false);
      else
        this.CreateMiniMap(true);
    }

    public void PauseGame()
    {
      if (this.Level.State == LevelState.Preparation || this.Level.State == LevelState.Play)
      {
        this.Level.Pause();
        this.InvokePause();
      }
      else
      {
        if (this.Level.State != LevelState.Pause)
          return;
        this.Play();
      }
    }

    public void Play() => this.Level.Play();

    public void SetScoreCount(int scores) => this._topPanel.SetScoreCount(scores);

    public override void TransitionComplete()
    {
      this.ScreenManager.AudioManager.BackgroundSounds.BossBattle = this.IsBossBattle;
      this.ScreenManager.AudioManager.BackgroundSounds.PlayGameplayTheme();
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = true
      });
    }

    private void UpdateControls()
    {
      if (this.Level.Mode != GameMode.Challenge)
        return;
      this._topPanel.UpdateWorldType(this.Level.WorldType);
      if (this.Level.LandingZoneStart <= 800 && this.Level.LandingZoneEnd >= 0)
        this._topPanel.UpdateLandingZone(this.Level.LandingZoneStart - 10, this.Level.LandingZoneEnd - 10);
      else
        this._topPanel.HideLandingZone();
    }

    private void OnPlasmaGunShooting(object sender, EventArgs e)
    {
      if (((Weapon) sender).IsShooting)
        this.ScreenManager.AudioManager.GameplaySounds.PlayPlasmaStart(sender);
      else
        this.ScreenManager.AudioManager.GameplaySounds.PlayPlasmaGunEnd(sender);
    }

    private void OnPlasmaGunShootingAfter125(object sender, EventArgs e)
    {
      this.ScreenManager.AudioManager.GameplaySounds.PlayPlasmaGunLoop(sender);
    }

    private void OnShieldFireAttemptWhileRecharging(object sender, EventArgs e)
    {
      this.ScreenManager.AudioManager.GameplaySounds.PlayShieldCantShoot();
    }

    private void OnShieldShooting(object sender, EventArgs e)
    {
      if (((Weapon) sender).IsShooting)
        this.ScreenManager.AudioManager.GameplaySounds.PlayShield(sender);
      else
        this.ScreenManager.AudioManager.GameplaySounds.StopPlayShield(sender);
    }

    private void OnVulcanFireAttemptWhileRecharging(object sender, EventArgs e)
    {
      this.ScreenManager.AudioManager.GameplaySounds.PlayVulcanCantShoot();
    }

    private void OnVulcanShooting(object sender, EventArgs e)
    {
      if (((Weapon) sender).IsShooting)
        this.ScreenManager.AudioManager.GameplaySounds.PlayVulcanLoop(sender);
      else
        this.ScreenManager.AudioManager.GameplaySounds.PlayVulcanEnd(sender);
    }

    private void OnUsedAmmunition(object sender, EventArgs e)
    {
      this._healthBonus.IsActive = true;
      this._healthBonus.Alpha = 0.0f;
      this._ammoUseTweener.Reset();
      this._ammoUseTweener.Start();
    }

    private void CreateAmunitionIcons()
    {
      this._amunitionPanel = new PanelControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("UI/Aminitions/health");
      ResourcesManager.Instance.GetResource<SpriteFont>("fonts/tahoma12");
      Color white = Color.White;
      this._healthBonus = new SpecialSlotTwoStateMenuControl(sprite1, (Sprite) null, sprite1, Vector2.Zero, Rectangle.Empty);
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("UI/Aminitions/healthNumberField");
      Vector2 vector2 = new Vector2(47f, 47f);
      this._healthBonus.Size = vector2;
      TexturedControl texturedControl = new TexturedControl(sprite2, new Vector2(17f, -1f));
      texturedControl.Visible = false;
      TexturedControl child1 = texturedControl;
      TextControl child2 = new TextControl("11", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/tahoma12"), white, sprite2.ScaledSize / 2f)
      {
        Scale = 0.6f,
        Centered = true,
        Origin = new Vector2(0.5f, 0.0f)
      };
      child2.Position = new Vector2(child2.Position.X + 1.5f, child2.Position.Y - 1f);
      child1.AddChild((BasicControl) child2);
      this._healthBonus.AddChild((BasicControl) child1, 0);
      this._healthBonus.ItemTexture = sprite1;
      if (Gamer.Instance.HealthBonus.IsInstalled)
      {
        this.UpdateHealthIcon(Gamer.Instance.HealthBonus.Item);
        this._amunitionPanel.AddChild((BasicControl) this._healthBonus);
      }
      Sprite sprite3 = ResourcesManager.Instance.GetSprite("UI/Aminitions/shieldCollisions");
      this._defenseBonus = new SpecialSlotTwoStateMenuControl(sprite3, (Sprite) null, sprite3, Vector2.Zero, Rectangle.Empty);
      this._defenseBonus.ItemTexture = sprite3;
      if (Gamer.Instance.DefenseBonus.IsInstalled)
      {
        this.UpdateDefenseBonus(Gamer.Instance.DefenseBonus.Item);
        this._amunitionPanel.AddChild((BasicControl) this._defenseBonus);
      }
      this._defenseBonus.Size = vector2;
      Sprite sprite4 = ResourcesManager.Instance.GetSprite("UI/Aminitions/gunsPlus");
      this._damageBonus = new SpecialSlotTwoStateMenuControl(sprite4, (Sprite) null, sprite4, Vector2.Zero, Rectangle.Empty);
      this._damageBonus.ItemTexture = sprite4;
      if (Gamer.Instance.DamageBonus.IsInstalled)
      {
        this.UpdateDamageBonus(Gamer.Instance.DamageBonus.Item);
        this._amunitionPanel.AddChild((BasicControl) this._damageBonus);
      }
      this._damageBonus.Size = vector2;
      this._amunitionPanel.LayoutRow(136f, 373f, 0.0f);
      this._controlsManager.AddChild((BasicControl) this._amunitionPanel);
    }

    private void InitHealthIcon()
    {
      this._ammoUseTweener = new Tweener(0.0f, 3.14159274f, 1f, new TweeningFunction(Quadratic.EaseInOut));
      this._ammoUseTweener.Updated += (EventHandler<EventArgs>) ((x, y) => this._healthBonus.Animated.Alpha = (float) Math.Sin((double) this._ammoUseTweener.Position));
      this._ammoUseTweener.Ended += (EventHandler<EventArgs>) ((x, y) =>
      {
        ExternalSupply externalSupply = this.Level.Player.ExternalSupply;
        if ((double) externalSupply.Volume <= 0.0)
        {
          PanelControl parent = this._healthBonus.Parent as PanelControl;
          parent.RemoveChild((BasicControl) this._healthBonus);
          parent.LayoutRow(136f, 373f, 0.0f);
        }
        ((TextControl) this._healthBonus.Children[0].Children[0]).Text = string.Format("{0}", (object) externalSupply.Volume);
        this._healthBonus.Alpha = 1f;
        this._healthBonus.IsActive = false;
      });
      this.Level.Player.ExternalSupply.Used += new EventHandler(this.OnUsedAmmunition);
    }

    private void UpdateDamageBonus(AmmunitionItem item)
    {
      this._damageBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).InGameTexture);
      this._damageBonus.Animated = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).InGameTextureSelected);
      this._damageBonus.Offset = Vector2.Zero;
    }

    private void UpdateDefenseBonus(AmmunitionItem item)
    {
      this._defenseBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).InGameTexture);
      this._defenseBonus.Animated = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) item.HangarDesc).InGameTextureSelected);
      this._defenseBonus.Offset = Vector2.Zero;
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
        this._healthBonus.ItemTexture = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) healthBonus.HangarDesc).InGameTexture);
        this._healthBonus.Animated = ResourcesManager.Instance.GetSprite(((ItemLayoutDescription) healthBonus.HangarDesc).InGameTextureSelected);
        this._healthBonus.Offset = Vector2.Zero;
      }
    }
  }
}
