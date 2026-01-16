using GameManager;
using Helicopter;
using Helicopter.BaseScreens.Controls;
using Helicopter.Model.WorldObjects.DeviceBonus;
using Microsoft.Advertising.Mobile.UI;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using MoVend;
using NotificationScheduledAgent;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace HelicopterSL
{
    public class Game1 //: PhoneApplicationPage, IMoVend
    {
        private readonly ContentManager _contentManager;
        private readonly GameTimer _timer;
        private HelicopterGame _helicopter;
        private UIElementRenderer render;
        private MoVendAPI _api;
        private bool _inUps;
        private DispatcherTimer _bgTimer = new DispatcherTimer();
        private Stopwatch updateStopwatch;
        private Stopwatch drawStopwatch;
        internal Grid LayoutGrid;
        internal Image HorisontalBG;
        internal Image VerticalBG;
        internal AdControl Banner;
        private bool _contentLoaded;

        private bool IsBannerVisible { get; set; }

        public Game1()
        {
            this.InitializeComponent();
            this._contentManager = ((App)Application.Current).Content;
            this._timer = new GameTimer();
            this._timer.UpdateInterval = TimeSpan.FromTicks(333333L);
            this._timer.Update += new EventHandler<GameTimerEventArgs>(this.OnUpdate);
            this._timer.Draw += new EventHandler<GameTimerEventArgs>(this.OnDraw);
            this._bgTimer.Interval = new TimeSpan(0, 0, 4);
            this._bgTimer.Tick += new EventHandler(this.OnBgTimerTick);
            this.updateStopwatch = Stopwatch.StartNew();
            this.drawStopwatch = Stopwatch.StartNew();
        }

        public void PurchaseSuccessCallback(
          NavigationService source,
          string productId,
          string transactionId)
        {
            this._helicopter.AddMoneyForGamer((float)new Dictionary<string, int>()
      {
        {
          "1040654",
          5000
        },
        {
          "1040655",
          12000
        },
        {
          "1040656",
          25000
        },
        {
          "1040657",
          45000
        }
      }[productId]);
            this._api.ConsumeProduct(productId);
            this.ContinueGame();
            this._helicopter.ShowTransitionComplitedPopup();
        }

        public void PurchaseFailureCallback(
          NavigationService source,
          ResultType resultType,
          string errorMessage,
          string productId)
        {
            this.ContinueGame();
            this._helicopter.ShowTransitionFailedPopup();
        }

        public void ContinueGame()
        {
            if (!this._inUps)
                return;
            ((UIElement)this.Banner).Visibility = (Visibility)0;
            this._api.CloseMoVendScreen(new CancelEventArgs(false));
            this._inUps = false;
            this._bgTimer.Stop();
            this.SupportedOrientations = (SupportedPageOrientation)2;
            this.Orientation = (PageOrientation)2;
            ((UIElement)this.HorisontalBG).Visibility = (Visibility)1;
            ((UIElement)this.VerticalBG).Visibility = (Visibility)1;
            GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, true);
            this._timer.Start();
        }

        protected virtual void OnBackKeyPress(CancelEventArgs e)
        {
            if (this._inUps)
            {
                e.Cancel = true;
            }
            else
            {
                if (this._helicopter.IsQuitCan)
                    return;
                e.Cancel = true;
                this._helicopter.BackButtonPressed();
            }
        }

        private void OnDraw(object sender, GameTimerEventArgs e)
        {
            this.drawStopwatch.Reset();
            this.drawStopwatch.Start();
            SharedGraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.CornflowerBlue);
            this._helicopter.Draw(new GameTime(e.TotalTime, e.ElapsedTime));
            this.drawStopwatch.Stop();
            long elapsedMilliseconds1 = this.drawStopwatch.ElapsedMilliseconds;
            this.drawStopwatch.Reset();
            this.drawStopwatch.Start();
            if (this.render != null && this.IsBannerVisible)
            {
                this._helicopter.SpriteBatch.Begin();
                this._helicopter.SpriteBatch.Draw(this.render.Texture, new Vector2(160f, 0.0f), Color.White);
                this._helicopter.SpriteBatch.End();
            }
            this.drawStopwatch.Stop();
            long elapsedMilliseconds2 = this.drawStopwatch.ElapsedMilliseconds;
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            this._timer.Stop();
            this._helicopter.OnDeactivated((object)this, (EventArgs)e);
            GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, false);
            ((Page)this).OnNavigatedFrom(e);
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Uri.OriginalString.Contains("?"))
                this._helicopter.AddMoneyForGamer((float)new Dictionary<string, int>()
        {
          {
            "1040654",
            5000
          },
          {
            "1040655",
            12000
          },
          {
            "1040656",
            25000
          },
          {
            "1040657",
            45000
          }
        }[((Page)this).NavigationContext.QueryString["ID"]]);
            ((Page)this).OnNavigatedTo(e);
            ((UIElement)this.Banner).InvalidateArrange();
            ((UIElement)this.Banner).InvalidateMeasure();
            GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, true);
            if (this._helicopter == null)
            {
                this._helicopter = new HelicopterGame();
                this._helicopter.ShopNeeded += (EventHandler<InAppEventArgs>)((x, y) => this.BuyInApp(y.ProductID));
                this._helicopter.OnActivated((object)this, (EventArgs)e);
                this._helicopter.Quit += (EventHandler)((x, y) => ((Page)this).NavigationService.GoBack());
                this._helicopter.BannerStateChanged += (EventHandler<BooleanEventArgs>)((x, y) =>
                {
                    this.IsBannerVisible = y.State;
                    ((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke((Action)(() => ((UIElement)this.Banner).Visibility = y.State ? (Visibility)0 : (Visibility)1));
                });
                this._helicopter.Init(SharedGraphicsDeviceManager.Current.GraphicsDevice, this._contentManager);
                this.UpdateNotificationSettings();
            }
            this._timer.Start();
        }

        private void UpdateNotificationSettings()
        {
            NotificationSettings notificationSettings = NotificationSettings.Load();
            if (Guide.IsVisible || notificationSettings.UserSelectAllowNotification)
                return;
            Guide.BeginShowMessageBox("Notifications", string.Format("Would you like to allow this app to display notifications? (if yes you'll earn {0} credits!)", (object)500), (IEnumerable<string>)new string[2]
            {
        "Yes",
        "No"
            }, 0, MessageBoxIcon.Alert, new AsyncCallback(this.OnNotificationAnswer), (object)null);
        }

        private void OnNotificationAnswer(IAsyncResult result)
        {
            NotificationSettings notificationSettings = NotificationSettings.Load();
            int? nullable = Guide.EndShowMessageBox(result);
            if (nullable.HasValue && nullable.Value == 0)
            {
                this._helicopter.AddMoneyForGamer(500f);
                notificationSettings.AllowNotification = true;
                notificationSettings.UserSelectAllowNotification = true;
            }
            else
            {
                notificationSettings.AllowNotification = false;
                notificationSettings.UserSelectAllowNotification = true;
            }
            notificationSettings.Save();
        }

        private void OnUpdate(object sender, GameTimerEventArgs e)
        {
            this.updateStopwatch.Reset();
            this.updateStopwatch.Start();
            if (this.render != null && this.IsBannerVisible)
                this.render.Render();
            this.updateStopwatch.Stop();
            long elapsedMilliseconds1 = this.updateStopwatch.ElapsedMilliseconds;
            this.updateStopwatch.Reset();
            this.updateStopwatch.Start();
            this._helicopter.Update(new GameTime(e.TotalTime, e.ElapsedTime));
            this.updateStopwatch.Stop();
            long elapsedMilliseconds2 = this.updateStopwatch.ElapsedMilliseconds;
        }

        private void BuyInApp(string productID)
        {
            this._timer.Stop();
            GraphicsDeviceExtensions.SetSharingMode(SharedGraphicsDeviceManager.Current.GraphicsDevice, false);
            this.SupportedOrientations = (SupportedPageOrientation)3;
            this._inUps = true;
            this._bgTimer.Start();
            ((UIElement)this.Banner).Visibility = (Visibility)1;
            this._api.PurchaseProduct(productID);
        }

        private void OnBgTimerTick(object sender, EventArgs e)
        {
            this._bgTimer.Stop();
            if (!this._inUps)
                return;
            this.UpdateBackground(this.Orientation);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.render == null)
            {
                this.render = new UIElementRenderer((UIElement)this.Banner, 480, 80);
                this.render.Render();
            }
            this._api = MoVendAPI.GetInstance((IMoVend)this, "oY7Lobr3tQBPo1aj", "1039241");
        }

        private void OnOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            this.UpdateBackground(e.Orientation);
        }

        private void UpdateBackground(PageOrientation orientation)
        {
            if (!this._inUps)
            {
                ((UIElement)this.HorisontalBG).Visibility = (Visibility)1;
                ((UIElement)this.VerticalBG).Visibility = (Visibility)1;
            }
            else
            {
                PageOrientation pageOrientation = orientation;
                if (pageOrientation <= 9)
                {
                    switch (pageOrientation - 1)
                    {
                        case 0:
                        case 4:
                            ((UIElement)this.HorisontalBG).Visibility = (Visibility)1;
                            ((UIElement)this.VerticalBG).Visibility = (Visibility)0;
                            return;
                        case 1:
                            break;
                        case 2:
                            return;
                        case 3:
                            return;
                        default:
                            if (pageOrientation != 9)
                                return;
                            goto case 0;
                    }
                }
                else if (pageOrientation != 18 && pageOrientation != 34)
                    return;
                ((UIElement)this.HorisontalBG).Visibility = (Visibility)0;
                ((UIElement)this.VerticalBG).Visibility = (Visibility)1;
            }
        }

        private void Image_Tap(object sender, GestureEventArgs e) => this.ContinueGame();

       
    }
}
