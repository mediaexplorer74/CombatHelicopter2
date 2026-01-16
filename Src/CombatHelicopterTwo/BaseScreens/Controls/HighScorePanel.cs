// Modified by MediaExplorer (2026)
// Type: Helicopter.BaseScreens.Controls.HighScorePanel
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Globalization;

#nullable disable
namespace Helicopter.BaseScreens.Controls
{
  public class HighScorePanel : ScrollingPanelControl
  {
    private BasicControl resultListControl;
    private SpriteFont titleFont;
    private SpriteFont headerFont;
    private SpriteFont detailFont;

    public HighScorePanel(ContentManager content)
    {
      this.titleFont = content.Load<SpriteFont>("Font\\MenuTitle");
      this.headerFont = content.Load<SpriteFont>("Font\\MenuHeader");
      this.detailFont = content.Load<SpriteFont>("Font\\MenuDetail");
      this.AddChild((BasicControl) new TextControl("High score", this.titleFont));
      this.AddChild(this.CreateHeaderControl());
      this.PopulateWithFakeData();
    }

    private void PopulateWithFakeData()
    {
      PanelControl panelControl = new PanelControl();
      Random random = new Random();
      for (int index = 0; index < 50; ++index)
      {
        long rating = (long) (10000 - index * 10);
        TimeSpan time = TimeSpan.FromSeconds((double) random.Next(60, 3600));
        panelControl.AddChild(this.CreateLeaderboardEntryControl("player" + index.ToString((IFormatProvider) CultureInfo.InvariantCulture), rating, time));
      }
      panelControl.LayoutColumn(0.0f, 0.0f, 0.0f);
      if (this.resultListControl != null)
        this.RemoveChild(this.resultListControl);
      this.resultListControl = (BasicControl) panelControl;
      this.AddChild(this.resultListControl);
      this.LayoutColumn(0.0f, 0.0f, 0.0f);
    }

    protected BasicControl CreateHeaderControl()
    {
      PanelControl headerControl = new PanelControl();
      headerControl.AddChild((BasicControl) new TextControl("Player", this.headerFont, Color.Turquoise, new Vector2(0.0f, 0.0f)));
      headerControl.AddChild((BasicControl) new TextControl("Score", this.headerFont, Color.Turquoise, new Vector2(200f, 0.0f)));
      return (BasicControl) headerControl;
    }

    protected BasicControl CreateLeaderboardEntryControl(string player, long rating, TimeSpan time)
    {
      Color white = Color.White;
      PanelControl leaderboardEntryControl = new PanelControl();
      PanelControl panelControl1 = leaderboardEntryControl;
      TextControl textControl1 = new TextControl();
      textControl1.Text = player;
      textControl1.Font = this.detailFont;
      textControl1.Color = white;
      textControl1.Position = new Vector2(0.0f, 0.0f);
      TextControl child1 = textControl1;
      panelControl1.AddChild((BasicControl) child1);
      PanelControl panelControl2 = leaderboardEntryControl;
      TextControl textControl2 = new TextControl();
      textControl2.Text = string.Format("{0}", (object) rating);
      textControl2.Font = this.detailFont;
      textControl2.Color = white;
      textControl2.Position = new Vector2(200f, 0.0f);
      TextControl child2 = textControl2;
      panelControl2.AddChild((BasicControl) child2);
      PanelControl panelControl3 = leaderboardEntryControl;
      TextControl textControl3 = new TextControl();
      textControl3.Text = string.Format("Completed in {0:g}", (object) time);
      textControl3.Font = this.detailFont;
      textControl3.Color = white;
      textControl3.Position = new Vector2(400f, 0.0f);
      TextControl child3 = textControl3;
      panelControl3.AddChild((BasicControl) child3);
      return (BasicControl) leaderboardEntryControl;
    }
  }
}
