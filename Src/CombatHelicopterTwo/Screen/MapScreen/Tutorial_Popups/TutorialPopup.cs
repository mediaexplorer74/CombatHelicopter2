// Modified by MediaExplorer (2026)
// Type: Helicopter.Screen.MapScreen.Tutorial_Popups.TutorialPopup
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Helicopter.BaseScreens;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.Common;
using Helicopter.Model.Common.Tween;
using Helicopter.Model.WorldObjects;
using Helicopter.Playing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Helicopter.Screen.MapScreen.Tutorial_Popups
{
  internal class TutorialPopup : GameScreen
  {
    private const float AnimTime = 0.6f;
    private readonly string[] _challengePictures = new string[3]
    {
      "Tutorial/pic_2_1",
      "Tutorial/pic_2_2",
      "Tutorial/pic_2_3"
    };
    private readonly string[] _challengeTexts = new string[3]
    {
      "Challenge other players and show them who's the real Combat Pilot!",
      "Earn credits for each destroyed enemy and use it to upgrade your ammunition.",
      "Explore the hangar!"
    };
    private readonly string[] _currentPictures;
    private readonly string[] _currentTexts;
    private readonly TutorialPopup.Tutorial _state;
    private readonly string[] _storyAfterPictures = new string[1]
    {
      "Tutorial/pic_2_1"
    };
    private readonly string[] _storyAfterTexts = new string[1]
    {
      "Great progress! By the way in Survival mode, you can compete with the other pilots for the best!"
    };
    private readonly string[] _storyPictures = new string[3]
    {
      "Tutorial/pic_1_1",
      "Tutorial/pic_1_2",
      "Tutorial/pic_1_3"
    };
    private readonly string[] _storyTexts = new string[3]
    {
      "Welcome pilot! Your mission is to destroy all enemy forces in each location. That's the order!",
      "No worries, we will support you! Each successful mission will gain you access to new weapons and supplies. Explore the hangar!",
      "Make us proud and your service will be awarded with ranks and medals. And always preserve your life energy!"
    };
    private readonly Vector2Animator animator = new Vector2Animator();
    private bool Animating;
    private BlessingControl _arrow;
    private Tweener _cloudTweener;
    private int _currentPage;
    private Tweener _fadeInTweener;
    private Tweener _fadeOutTweener;
    private TexturedControl _paint;
    private BasicControl _root;
    private TextControl _text;
    private TexturedControl board;
    private TexturedControl cloud;
    private TexturedControl lamp;
    private TexturedControl major;

    public TutorialPopup(TutorialPopup.Tutorial state)
    {
      this.IsPopup = true;
      this._state = state;
      switch (state)
      {
        case TutorialPopup.Tutorial.Story:
          this._currentPictures = this._storyPictures;
          this._currentTexts = this._storyTexts;
          break;
        case TutorialPopup.Tutorial.Challenge:
          this._currentPictures = this._challengePictures;
          this._currentTexts = this._challengeTexts;
          break;
        case TutorialPopup.Tutorial.After22:
          this._currentPictures = this._storyAfterPictures;
          this._currentTexts = this._storyAfterTexts;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (state));
      }
    }

    public override void LoadContent()
    {
      base.LoadContent();
      this._root = new BasicControl();
      this.board = new TexturedControl(ResourcesManager.Instance.GetSprite("Tutorial/board"), new Vector2(205f, 40f));
      this._root.AddChild((BasicControl) this.board);
      this.major = new TexturedControl(ResourcesManager.Instance.GetSprite("Tutorial/major"), new Vector2(40f, 38f));
      this._root.AddChild((BasicControl) this.major);
      this._paint = new TexturedControl(ResourcesManager.Instance.GetSprite(((IEnumerable<string>) this._currentPictures).First<string>()), new Vector2(32f, 30f));
      this.board.AddChild((BasicControl) this._paint);
      TexturedControl texturedControl1 = new TexturedControl(ResourcesManager.Instance.GetSprite("Tutorial/lampOffTutorial"), new Vector2(564f, 0.0f));
      texturedControl1.Visible = false;
      this.lamp = texturedControl1;
      this._root.AddChild((BasicControl) this.lamp);
      TexturedControl texturedControl2 = new TexturedControl(ResourcesManager.Instance.GetSprite("Tutorial/cloudTutorial"), new Vector2(186f, 318f));
      texturedControl2.Visible = false;
      this.cloud = texturedControl2;
      this._root.AddChild((BasicControl) this.cloud);
      this._arrow = new BlessingControl(ResourcesManager.Instance.GetSprite("Tutorial/nextArrow"), new Vector2(504f, 128f));
      this.cloud.AddChild((BasicControl) this._arrow);
      this._cloudTweener = new Tweener(0.0f, 1f, 0.6f, new TweeningFunction(Linear.EaseIn));
      this._cloudTweener.Stop();
      MenuControl child = new MenuControl(ResourcesManager.Instance.GetSprite("Tutorial/butSkip"), ResourcesManager.Instance.GetSprite("Tutorial/butSkipSelect"), new Vector2(702f, 7f));
      child.EntryPosition = new Rectangle(650, 0, 150, 50);
      child.Clicked += new EventHandler<EventArgs>(this.OnSkipClicked);
      this._root.AddChild((BasicControl) child);
      this._text = new TextControl("", ResourcesManager.Instance.GetResource<SpriteFont>("fonts/coalition12"), Color.Black, new Vector2(303f, 76f))
      {
        Centered = true,
        MaxSymbolsPerLine = 35,
        Origin = new Vector2(0.5f, 0.0f)
      };
      this._text.Text = ((IEnumerable<string>) this._currentTexts).First<string>().ToLowerInvariant();
      this.cloud.AddChild((BasicControl) this._text);
      this._currentPage = 1;
      this.InAnimation(0.6f);
      this._fadeOutTweener = new Tweener(1f, 0.0f, 0.3f, new TweeningFunction(Linear.EaseIn));
      this._fadeOutTweener.Ended += new EventHandler<EventArgs>(this.OnFadeOutTweenerEnded);
      this._fadeInTweener = new Tweener(0.0f, 1f, 0.3f, new TweeningFunction(Linear.EaseIn));
      this._fadeInTweener.Ended += new EventHandler<EventArgs>(this.OnFadeInTweenerEnded);
      this._fadeInTweener.Stop();
      this._fadeOutTweener.Stop();
      if (this._state != TutorialPopup.Tutorial.Challenge)
        return;
      this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
      {
        State = false
      });
    }

    public override void Draw(DrawContext drawContext)
    {
      drawContext.SpriteBatch.Draw(drawContext.BlankTexture, drawContext.Device.Viewport.Bounds, Color.Black * 0.7f);
      this._root.Draw(drawContext);
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      this._root.Update(gameTime);
      float totalSeconds = (float) gameTime.ElapsedGameTime.TotalSeconds;
      this.animator.Update(totalSeconds);
      if (this._cloudTweener.Running)
      {
        this._cloudTweener.Update(totalSeconds);
        this.cloud.Color = Color.White * this._cloudTweener.Position;
        this._text.Color = Color.Black * this._cloudTweener.Position;
      }
      if (this._fadeInTweener.Running)
      {
        this._fadeInTweener.Update(totalSeconds);
        this._paint.Color = Color.White * this._fadeInTweener.Position;
        this._text.Color = Color.Black * this._fadeInTweener.Position;
      }
      else
      {
        if (!this._fadeOutTweener.Running)
          return;
        this._fadeOutTweener.Update(totalSeconds);
        this._paint.Color = Color.White * this._fadeOutTweener.Position;
        this._text.Color = Color.Black * this._fadeOutTweener.Position;
      }
    }

    public override void HandleInput(InputState input)
    {
      this._root.HandleInput(input);
      if (input.TouchState.Count <= 0 || this.Animating)
        return;
      this.ChangePage();
    }

    public override void OnBackButton() => this.Exit();

    private void OnFadeInTweenerEnded(object x, EventArgs y)
    {
      this.Animating = false;
      this._fadeOutTweener.Reset();
    }

    private void OnFadeOutTweenerEnded(object x, EventArgs y)
    {
      this._paint.Sprite = ResourcesManager.Instance.GetSprite(this._currentPictures[this._currentPage - 1]);
      this._text.Text = this._currentTexts[this._currentPage - 1].ToLowerInvariant();
      if (this._currentPage == this._currentTexts.Length)
        this._arrow.Visible = false;
      this._fadeInTweener.Reset();
      this._fadeInTweener.Start();
    }

    private void OnSkipClicked(object x, EventArgs y) => this.Exit();

    private void ChangePage()
    {
      if (this.Animating)
        return;
      this.Animating = true;
      ++this._currentPage;
      if (this._currentPage > this._currentTexts.Length)
        this.OutAnimation(0.6f);
      else
        this._fadeOutTweener.Start();
    }

    private void Exit()
    {
      this.ExitScreen();
      switch (this._state)
      {
        case TutorialPopup.Tutorial.Story:
          if (!SettingsGame.NeedMapTutorial)
            break;
          GameProcess.Instance.StoryGameSession.StartSessionWithoutHangar(WorldType.Canyon, 1);
          SettingsGame.NeedMapTutorial = false;
          break;
        case TutorialPopup.Tutorial.Challenge:
          if (SettingsGame.NeedChallengeTutorial)
          {
            GameProcess.Instance.ChallengeGameSession.StartSessionWithoutHangar();
            SettingsGame.NeedChallengeTutorial = false;
          }
          this.ScreenManager.Game.OnBannerStateChanged(new BooleanEventArgs()
          {
            State = true
          });
          break;
        case TutorialPopup.Tutorial.After22:
          SettingsGame.NeedAfter22Tutorial = false;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void InAnimation(float animTime)
    {
      this.Animating = true;
      this.animator.AddAnim(new AnimDesc()
      {
        Control = (BasicControl) this.board,
        From = new Vector2(800f, 40f),
        To = new Vector2(205f, 40f),
        Function = new TweeningFunction(Back.EaseInOut),
        Time = animTime
      });
      this.animator.AddAnim(new AnimDesc()
      {
        Control = (BasicControl) this.major,
        From = new Vector2((float) -this.major.Sprite.Texture.Width, 38f),
        To = new Vector2(40f, 38f),
        Function = new TweeningFunction(Back.EaseInOut),
        Time = animTime,
        EndAction = (Action) (() =>
        {
          this.lamp.Visible = true;
          this.cloud.Visible = true;
          this.animator.AddAnim(new AnimDesc()
          {
            Control = (BasicControl) this.lamp,
            From = new Vector2(564f, (float) -this.lamp.Sprite.Texture.Height),
            To = new Vector2(564f, 0.0f),
            Function = new TweeningFunction(Quadratic.EaseOut),
            Time = animTime,
            EndAction = (Action) (() => this.animator.AddAnim(new AnimDesc()
            {
              Control = (BasicControl) this.lamp,
              EndAction = (Action) (() => this.lamp.Sprite = ResourcesManager.Instance.GetSprite("Tutorial/lampTutorial")),
              Time = 0.5f,
              To = new Vector2(564f, 0.0f),
              From = new Vector2(564f, 0.0f),
              Function = new TweeningFunction(Linear.EaseIn)
            }))
          });
          this._cloudTweener.Ended += (EventHandler<EventArgs>) ((x, y) => this.Animating = false);
          this._cloudTweener.Start();
        })
      });
      this.animator.Update(0.0f);
    }

    private void OutAnimation(float animTime)
    {
      this.Animating = true;
      this.animator.Clear();
      this.lamp.Sprite = ResourcesManager.Instance.GetSprite("Tutorial/lampOffTutorial");
      this.animator.AddAnim(new AnimDesc()
      {
        Control = (BasicControl) this.lamp,
        From = new Vector2(564f, 0.0f),
        To = new Vector2(564f, (float) -this.lamp.Sprite.Bounds.Height),
        Function = new TweeningFunction(Quadratic.EaseIn),
        Time = animTime * 0.5f,
        EndAction = (Action) (() =>
        {
          this.animator.AddAnim(new AnimDesc()
          {
            Control = (BasicControl) this.board,
            From = new Vector2(205f, 40f),
            To = new Vector2(800f, 40f),
            Function = new TweeningFunction(Back.EaseInOut),
            Time = animTime
          });
          this.animator.AddAnim(new AnimDesc()
          {
            Control = (BasicControl) this.major,
            From = new Vector2(40f, 38f),
            To = new Vector2((float) -this.major.Sprite.Bounds.Width, 38f),
            Function = new TweeningFunction(Back.EaseInOut),
            Time = animTime,
            EndAction = new Action(this.Exit)
          });
        })
      });
      this._cloudTweener.Reverse();
      this._cloudTweener.Start();
      this.animator.Update(0.0f);
    }

    public enum Tutorial
    {
      Story,
      Challenge,
      After22,
    }
  }
}
