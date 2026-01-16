// Modified by MediaExplorer (2026)
// Type: Helicopter.GamePlay.GameplayPopups.StoryWin
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Items;
using Helicopter.Model.Common;
using Helicopter.Model.SpriteObjects.Sprites;
using Helicopter.Utils.SoundManagers;
using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace Helicopter.GamePlay.GameplayPopups
{
  internal class StoryWin : BasePopup
  {
    private readonly int _stars;
    private BasicControl _root;
    private readonly int _loacation;
    private readonly int _episode;

    public event EventHandler Map;

    public event EventHandler Replay;

    public event EventHandler Next;

    private void OnMap(EventArgs e)
    {
      EventHandler map = this.Map;
      if (map == null)
        return;
      map((object) this, e);
    }

    private void OnReplay(EventArgs e)
    {
      EventHandler replay = this.Replay;
      if (replay == null)
        return;
      replay((object) this, e);
    }

    private void OnNext(EventArgs e)
    {
      EventHandler next = this.Next;
      if (next == null)
        return;
      next((object) this, e);
    }

    public StoryWin(int loacation, int episode, int stars)
    {
      this.IsPopup = true;
      this._loacation = loacation;
      this._episode = episode;
      this._stars = stars;
    }

    public override void Draw(DrawContext drawContext)
    {
      if (!this.OnTop)
        return;
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    private void OnMapButtonClicked(object sender, EventArgs e) => this.OnMap(EventArgs.Empty);

    private void OnNextClicked(object sender, EventArgs e) => this.OnNext(EventArgs.Empty);

    private void OnRetryClicked(object sender, EventArgs e) => this.OnReplay(EventArgs.Empty);

    private BasicControl CreateLevelLabel(int level, int sublevel)
    {
      BasicControl levelLabel = new BasicControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/level");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/tire");
      Sprite sprite3 = ResourcesManager.Instance.GetSprite(string.Format("PopUpWindow/num{0}", (object) level));
      Sprite sprite4 = ResourcesManager.Instance.GetSprite(string.Format("PopUpWindow/num{0}", (object) sublevel));
      levelLabel.AddChild((BasicControl) new TexturedControl(sprite1, Vector2.Zero));
      levelLabel.AddChild((BasicControl) new TexturedControl(sprite3, new Vector2(levelLabel.Size.X + 14f, 0.0f)));
      levelLabel.AddChild((BasicControl) new TexturedControl(sprite2, new Vector2(levelLabel.Size.X + 7f, (float) ((double) levelLabel.Size.Y / 2.0 - (double) sprite2.Bounds.Height / 2.0))));
      levelLabel.AddChild((BasicControl) new TexturedControl(sprite4, new Vector2(levelLabel.Size.X + 7f, 0.0f)));
      levelLabel.Position = new Vector2((float) (400.0 - (double) levelLabel.Size.X / 2.0), 230f);
      return levelLabel;
    }

    private BasicControl CreateStars(int stars)
    {
      BasicControl stars1 = new BasicControl();
      Sprite sprite1 = ResourcesManager.Instance.GetSprite("PopUpWindow/starFull");
      Sprite sprite2 = ResourcesManager.Instance.GetSprite("PopUpWindow/starEmpty");
      int num = 20;
      stars1.AddChild((BasicControl) new TexturedControl(sprite1, Vector2.Zero));
      stars1.AddChild((BasicControl) new TexturedControl(stars >= 2 ? sprite1 : sprite2, new Vector2(stars1.Size.X + (float) num, 0.0f)));
      stars1.AddChild((BasicControl) new TexturedControl(stars >= 3 ? sprite1 : sprite2, new Vector2(stars1.Size.X + (float) num, 0.0f)));
      stars1.Position = new Vector2((float) (400.0 - (double) stars1.Size.X / 2.0), 260f);
      return stars1;
    }

    public override void HandleInput(InputState input)
    {
      base.HandleInput(input);
      this._root.HandleInput(input);
    }

    public override void LoadContent()
    {
      this._root = new BasicControl();
      Sprite sprite = ResourcesManager.Instance.GetSprite("PopUpWindow/winPopUpBg");
      this._root.AddChild((BasicControl) new TexturedControl(sprite, new Vector2((float) (400 - sprite.Bounds.Width / 2), (float) (245 - sprite.Bounds.Height / 2))));
      BasicControl child1 = new BasicControl();
      MenuControl child2 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butMap"), ResourcesManager.Instance.GetSprite("PopUpWindow/butMapSelect"), Vector2.Zero);
      child2.Clicked += new EventHandler<EventArgs>(this.OnMapButtonClicked);
      child1.AddChild((BasicControl) child2);
      int num = 11;
      MenuControl child3 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butReplay"), ResourcesManager.Instance.GetSprite("PopUpWindow/butReplaySelect"), new Vector2(child1.Size.X + (float) num, 0.0f));
      child3.Clicked += new EventHandler<EventArgs>(this.OnRetryClicked);
      child1.AddChild((BasicControl) child3);
      MenuControl child4 = new MenuControl(ResourcesManager.Instance.GetSprite("PopUpWindow/butNext"), ResourcesManager.Instance.GetSprite("PopUpWindow/butNextSelect"), new Vector2(child1.Size.X + (float) num, 0.0f));
      child4.Clicked += new EventHandler<EventArgs>(this.OnNextClicked);
      child1.AddChild((BasicControl) child4);
      child1.Position = new Vector2((float) (402.0 - (double) child1.Size.X / 2.0), 350f);
      this._root.AddChild(child1);
      this._root.AddChild(this.CreateStars(this._stars));
      this._root.AddChild(this.CreateLevelLabel(this._loacation, this._episode));
      BackgroundSounds.Instance.PlayWin();
    }

    public void OnNewRank(object sender, EventArgs e)
    {
      Sprite sprite;
      switch (Gamer.Instance.Rank)
      {
        case Rank.Pilot:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_pilot");
          break;
        case Rank.Sergeant:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_sergeant");
          break;
        case Rank.Lieutenant:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_lieutenant");
          break;
        case Rank.Captain:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_pilot");
          break;
        case Rank.Major:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_major");
          break;
        case Rank.Colonel:
          sprite = ResourcesManager.Instance.GetSprite("MainMenu/Achievement/achieveRank_colonel");
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      TexturedControl child1 = new TexturedControl(sprite, new Vector2(575f, 173f));
      TexturedControl child2 = new TexturedControl(ResourcesManager.Instance.GetSprite("PopUpWindow/newRank"), new Vector2(550f, 237f));
      this._root.AddChild((BasicControl) child1);
      this._root.AddChild((BasicControl) child2);
    }
  }
}
