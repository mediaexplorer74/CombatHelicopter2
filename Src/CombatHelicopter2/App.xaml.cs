/*
using FlurryWP7SDK;
using Helicopter.Playing;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NotificationScheduledAgent;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace HelicopterSL
{
  public class App : Application
  {
    public const string FlurryApiKeyValue = "EJ2FG79HQT3V4TDC46NE";
    private bool _contentLoaded;
    private bool phoneApplicationInitialized;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/HelicopterSL;component/App.xaml", UriKind.Relative));
    }

    public PhoneApplicationFrame RootFrame { get; private set; }

    public ContentManager Content { get; private set; }

    public GameTimer FrameworkDispatcherTimer { get; private set; }

    public AppServiceProvider Services { get; private set; }

    public App()
    {
      this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
      this.InitializeComponent();
      this.InitializePhoneApplication();
      this.InitializeXnaApplication();
      this.RootFrame.Obscured += new EventHandler<ObscuredEventArgs>(this.RootFrame_Obscured);
      if (!Debugger.IsAttached)
        return;
      Application.Current.Host.Settings.EnableFrameRateCounter = true;
      PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode) 1;
    }

    private void RootFrame_Obscured(object sender, ObscuredEventArgs e) => App.ShowPause();

    private void Application_Activated(object sender, ActivatedEventArgs e)
    {
      App.ShowPause();
      if (!(((ContentControl) this.RootFrame).Content is GamePage content))
        return;
      content.ContinueGame();
    }

    private static void ShowPause()
    {
      if (GameProcess.Instance.StoryGameSession.IsGameStarted)
      {
        GameProcess.Instance.StoryGameSession.Pause();
      }
      else
      {
        if (!GameProcess.Instance.ChallengeGameSession.IsGameStarted)
          return;
        GameProcess.Instance.ChallengeGameSession.Pause();
      }
    }

    private void Application_Closing(object sender, ClosingEventArgs e)
    {
    }

    private void Application_Deactivated(object sender, DeactivatedEventArgs e)
    {
    }

    private void Application_Launching(object sender, LaunchingEventArgs e)
    {
      Api.StartSession("EJ2FG79HQT3V4TDC46NE");
      this.RestartBackgroundAgent();
    }

    private void RestartBackgroundAgent()
    {
      if (ScheduledActionService.Find("NotificationScheduledAgent") != null)
        ScheduledActionService.Remove("NotificationScheduledAgent");
      NotificationSettings notificationSettings = NotificationSettings.Load();
      notificationSettings.GameLastRunDate = DateTime.UtcNow;
      notificationSettings.Save();
      PeriodicTask periodicTask = new PeriodicTask("NotificationScheduledAgent");
      ((ScheduledTask) periodicTask).Description = "Notifications from Combat Helicopter 2";
      try
      {
        ScheduledActionService.Add((ScheduledAction) periodicTask);
      }
      catch (InvalidOperationException ex)
      {
      }
      catch (SchedulerServiceException ex)
      {
      }
    }

    private void Application_UnhandledException(
      object sender,
      ApplicationUnhandledExceptionEventArgs e)
    {
      MessageBox.Show(e.ExceptionObject.StackTrace, e.ExceptionObject.Message, (MessageBoxButton) 0);
      if (!Debugger.IsAttached)
        return;
      Debugger.Break();
    }

    private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.Break();
    }

    private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
    {
      if (this.RootVisual != this.RootFrame)
        this.RootVisual = (UIElement) this.RootFrame;
      ((Frame) this.RootFrame).Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
    }

    private void InitializePhoneApplication()
    {
      if (this.phoneApplicationInitialized)
        return;
      this.RootFrame = new PhoneApplicationFrame();
      ((Frame) this.RootFrame).Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
      ((Frame) this.RootFrame).NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
      this.phoneApplicationInitialized = true;
    }

    private void FrameworkDispatcherFrameAction(object sender, EventArgs e)
    {
      FrameworkDispatcher.Update();
    }

    private void InitializeXnaApplication()
    {
      this.Services = new AppServiceProvider();
      foreach (object applicationLifetimeObject in (IEnumerable) this.ApplicationLifetimeObjects)
      {
        if (applicationLifetimeObject is IGraphicsDeviceService)
          this.Services.AddService(typeof (IGraphicsDeviceService), applicationLifetimeObject);
      }
      this.Content = new ContentManager((IServiceProvider) this.Services, "Content");
      this.FrameworkDispatcherTimer = new GameTimer();
      this.FrameworkDispatcherTimer.FrameAction += new EventHandler<EventArgs>(this.FrameworkDispatcherFrameAction);
      this.FrameworkDispatcherTimer.Start();
    }
  }
}*/

//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  The MIT License (MIT)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace HelocopterSL
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }


        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

            // **********************************************************************************         
            // The database will be installed together with the application in the folder. 
            // However, the application takes databases from 
            // the ApplicationData.Current.LocalFolder folder. 
            // Therefore, when we first launch the application, 
            // we need to copy the database to ApplicationData.Current.LocalFolder
            /* 
            // "First app start or not"?
            if (await ApplicationData.Current.LocalFolder.TryGetItemAsync("terraria.db") == null)
            {
                StorageFile databaseFile = default;

                try
                {
                    databaseFile =
                        await Package.Current.InstalledLocation.GetFileAsync("terraria.db");

                    // Copy DB
                    await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] databaseFile.CopyAsync error: " + ex.Message);
                }
            }
            */
            //**************************************************************************



            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(GamePage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}

