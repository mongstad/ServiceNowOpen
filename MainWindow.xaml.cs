using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ServiceNow;
using System.Reflection;
using Image = System.Windows.Controls.Image;
using System.Windows.Resources;
using Monitor = Computer.Monitor;
using Microsoft.Win32;

namespace ServiceNowOpen
{
   
    public partial class MainWindow : Window
    {
    
        RecentlyOpenedItems recentlyOpenedItems = new RecentlyOpenedItems();
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
        ServiceNowTheme serviceNowTheme = new ServiceNowTheme();
        public MainWindow()
        {

            InitializeComponent();
            LoadSettings();
            SetWindowBackgroundColors();
            LoadButtonColors();
            LoadTextColors();   
            SetNotifyIconSettings();
            SetVersionInfo();
            //SetRGBSliderValues();
        }

        private void LoadSettings()
        {
            Settings snSettings = new Settings();
            try
            {
                snSettings = snSettings.Load();
            }
            catch(Exception)
            {

            }

            serviceNowTheme.Opacity = snSettings.Opacity;

            this.Top = snSettings.Top;
            this.Left = snSettings.Left;
            sliderOpacityValue.Value = snSettings.SliderPosition;
            AlwaysOnTopCheckBox.IsChecked = snSettings.TopMost;
            FreeTextSearchCheckBox.IsChecked = snSettings.FreeTextSearch;
            HideFromTaskBarCheckBox.IsChecked = snSettings.MinimizeToTray;
            serviceNowTheme = snSettings.ServiceNowTheme;
            recentlyOpenedItems = snSettings.RecentItems;
            this.DataContext = recentlyOpenedItems;
            recentlyOpenedItems = snSettings.RecentItems;
            ServiceNowPortalTextBox.Text = snSettings.URLServiceNowPortal;
            CIPrefixTextBox.Text = snSettings.RegExConfigurationItems;
            PeripheralPrefixTextBox.Text = snSettings.RegExPeripherals;
            UserNamesPrefixTextBox.Text = snSettings.RegExUsernames;
          

        }
        private void SetWindowBackgroundColors()
        {
            
            if(!(serviceNowTheme.MainWindowBackgroundColor == ""))
            {
                gridMainWindow.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MainWindowBackgroundColor);
            }else{
                gridMainWindow.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MainWindowDefaultBackgroundColor);
            }
            
            if(!(serviceNowTheme.TitleBarBackgroundColor == ""))
            {
                gridTitleBar.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarBackgroundColor);
            }else{
                gridTitleBar.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarDefaultBackgroundColor);
            }

            if(!(serviceNowTheme.MenuBackgroundColor ==""))
            {
                stackpanelMenu.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MenuBackgroundColor);
            }else{
                stackpanelMenu.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MenuDefaultBackgroundColor);
            }
         
            if(serviceNowTheme.Opacity != 0)
            {
                this.Opacity = serviceNowTheme.Opacity;
            }

        }
        private void LoadButtonColors()
        {

            if(serviceNowTheme.MenuButtonColor != serviceNowTheme.DefaultButtonColor)
            {
                //Home Button
                Image imgHome = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], 255, @"/Images/home-white.png")
                };

                ImageBrush backgroundHomeImage = new ImageBrush 
                {
                    ImageSource = imgHome.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                HomeMenuButton.Background = backgroundHomeImage;

                //Themes Button
                Image imgTheme = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], 255, @"/Images/paint-brush-64.png")
                };

                ImageBrush backgroundThemeImage = new ImageBrush
                {
                    ImageSource = imgTheme.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                ThemesMenuButton.Background = backgroundThemeImage;

                //Recently Opened Items Button
                Image imgRecentItems = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], 255, @"/Images/recentitems-white.png")
                };

                ImageBrush backgroundRecentItemsImage = new ImageBrush
                {
                    ImageSource = imgRecentItems.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                RecentItemsMenuButton.Background = backgroundRecentItemsImage;

                //Settings Button
                Image imgSettingsButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], 255, @"/Images/settings-white.png")
                };

                ImageBrush backgroundSettingsButton = new ImageBrush
                {
                    ImageSource = imgSettingsButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                SettingsMenuButton.Background = backgroundSettingsButton;


                //About Button
                Image imgAboutButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], 255, @"/Images/about.png")
                };

                ImageBrush backgroundAboutButton = new ImageBrush
                {
                    ImageSource = imgAboutButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                AboutMenuButton.Background = backgroundAboutButton;

            }

            if(serviceNowTheme.TitleBarButtonColor != serviceNowTheme.DefaultButtonColor)
                {
                    //Power Button
                    Image imgPowerButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.TitleBarButtonsRGB[0], serviceNowTheme.TitleBarButtonsRGB[1], serviceNowTheme.TitleBarButtonsRGB[2], 255, @"/Images/close.png")
                    };

                    ImageBrush backgroundPowerButton = new ImageBrush
                    {
                        ImageSource = imgPowerButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };

                    CloseButton.Background = backgroundPowerButton;

                    //Minimize Button
                    Image imgMinimizeButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.TitleBarButtonsRGB[0], serviceNowTheme.TitleBarButtonsRGB[1], serviceNowTheme.TitleBarButtonsRGB[2], 255, @"/Images/minimize-white.png")
                    };

                    ImageBrush backgroundMinimizeButton = new ImageBrush
                    {
                        ImageSource = imgMinimizeButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };

                    MinimizeButton.Background = backgroundMinimizeButton;
                 }
              

            if(serviceNowTheme.MainWindowButtonColor != serviceNowTheme.DefaultButtonColor)
                {

                    //Copy Button
                    Image imgCopyButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], 255, @"/Images/copy-white.png")
                    };

                    ImageBrush backgroundCopyButton = new ImageBrush
                    {
                        ImageSource = imgCopyButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };

                    CopyButton.Background = backgroundCopyButton;

                    //Open In Browser Button
                    Image imgOpenInBrowserButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], 255, @"/Images/openinbrowser-white.png")
                    };

                    ImageBrush backgroundOpenInBrowserButton = new ImageBrush
                    {
                        ImageSource = imgOpenInBrowserButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };

                    OpenInBrowserButton.Background = backgroundOpenInBrowserButton;

                    //OK (CheckMark) Button
                    Image imgOKButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], 255, @"/Images/checkmark-white.png")
                    };

                    ImageBrush backgroundOKButton = new ImageBrush
                    {
                        ImageSource = imgOKButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };
                    GoButton.Background = backgroundOKButton;

                    //Load File Button
                    Image imgLoadFileButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], 255, @"/Images/BrowseForFile.png")
                    };

                    ImageBrush backgroundLoadFileButton = new ImageBrush
                    {
                        ImageSource = imgLoadFileButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };
                    LoadFileButton.Background = backgroundLoadFileButton;

                    //Save To File Button
                    Image imgSaveToFileButton = new Image
                    {
                        Source = serviceNowTheme.ConvertImageColor(serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], 255, @"/Images/SaveToFile.png")
                    };

                    ImageBrush backgroundSaveToFileButton = new ImageBrush
                    {
                        ImageSource = imgSaveToFileButton.Source,
                        Stretch = Stretch.Fill,
                        TileMode = TileMode.None
                    };
                    SaveToFileButton.Background = backgroundSaveToFileButton;

            }

        }
        private void LoadTextColors()
        {

                if(serviceNowTheme.MainWindowTextColor != "")
                {   
                    Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MainWindowTextColor);

                    //txt_Theme.Foreground = textColor;
                    LeftMenuCheckBox.Foreground = textColor;
                    WindowContentCheckBox.Foreground = textColor;
                    txtRed.Foreground = textColor;
                    txtGreen.Foreground = textColor;
                    txtBlue.Foreground = textColor;
                    txtOpacity.Foreground = textColor;
                    ButtonColorCheckBox.Foreground = textColor;
                    TextColorCheckBox.Foreground = textColor;
                    sliderRed.Foreground = textColor;
                    sliderGreen.Foreground = textColor;
                    sliderBlue.Foreground = textColor;
                    borderSliders.BorderBrush = textColor;
                    ResetToDefaultButton.Foreground = textColor;
                    txtVersion.Foreground = textColor;
                    txtOpen.Foreground = textColor;
                    FreeTextSearchCheckBox.Foreground = textColor;
                    HideFromTaskBarCheckBox.Foreground = textColor;
                    AlwaysOnTopCheckBox.Foreground = textColor;
                    TitleBarCheckBox.Foreground = textColor;
                    txtUsernames.Foreground = textColor;
                    txtPeripherals.Foreground = textColor;
                    txtConfigurationItems.Foreground = textColor;
                    txtServiceNowURL.Foreground = textColor;
                    txtServiceNowOpen.Foreground = textColor;
                    txtDevelopedBy.Foreground = textColor;
                    txtVersion.Foreground = textColor;
            }
            else{
                    Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.DefaultTextColor);

                    
                    LeftMenuCheckBox.Foreground = textColor;
                    WindowContentCheckBox.Foreground = textColor;
                    txtRed.Foreground = textColor;
                    txtGreen.Foreground = textColor;
                    txtBlue.Foreground = textColor;
                    txtOpacity.Foreground = textColor;
                    ButtonColorCheckBox.Foreground = textColor;
                    TextColorCheckBox.Foreground = textColor;
                    sliderRed.Foreground = textColor;
                    sliderGreen.Foreground = textColor;
                    sliderBlue.Foreground = textColor;
                    borderSliders.BorderBrush = textColor;
                    ResetToDefaultButton.Foreground = textColor;
                    txtVersion.Foreground = textColor;
                    txtOpen.Foreground = textColor;
                    FreeTextSearchCheckBox.Foreground = textColor;
                    HideFromTaskBarCheckBox.Foreground = textColor;
                    AlwaysOnTopCheckBox.Foreground = textColor;
                    TitleBarCheckBox.Foreground = textColor;
                    txtUsernames.Foreground = textColor;
                    txtPeripherals.Foreground = textColor;
                    txtConfigurationItems.Foreground = textColor;
                    txtServiceNowURL.Foreground = textColor;
                    txtServiceNowOpen.Foreground = textColor;
                    txtDevelopedBy.Foreground = textColor;
                    txtVersion.Foreground = textColor;

            }

                if(!(serviceNowTheme.TitleBarTextColor == ""))
                {
                    Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarTextColor);
                    txtTitle.Foreground = textColor;
                }else{
                    Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.DefaultTextColor);
                    txtTitle.Foreground = textColor;
                }

        }
        private void SetNotifyIconSettings()
        {

            notifyIcon.Text = "ServiceNowOpen";
            notifyIcon.Icon = new System.Drawing.Icon("ServiceNow.ico");
            notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown);

            System.Windows.Forms.MenuItem menuItemResetWindowPosition = new System.Windows.Forms.MenuItem();
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItemResetWindowPosition });
            menuItemResetWindowPosition.Index = 0;
            menuItemResetWindowPosition.Text = "Reset window position";
            menuItemResetWindowPosition.Click += new System.EventHandler(ResetWindowPosition_Click);
            notifyIcon.ContextMenu = contextMenu;

            System.Windows.Forms.MenuItem menuItemExit = new System.Windows.Forms.MenuItem();
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItemExit });
            menuItemExit.Index = 1;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(MenuItemExit_Click);
            notifyIcon.ContextMenu = contextMenu;

            notifyIcon.Visible = true;

        }
        private void SetVersionInfo()
        {

            Assembly thisAssem = typeof(MainWindow).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();

            Version ver = thisAssemName.Version;
            txtVersion.Text += ver.ToString();
        }
        //private void SetRGBSliderValues()
        //{
        //    if(TitleBarCheckBox.IsChecked == true)
        //    {


        //        System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(gridTitleBar.Background.ToString());


        //        int intSliderRedValue = 0;

        //        bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
        //        if(redOk)
        //        {
        //            sliderRed.Value = intSliderRedValue;
        //        }


        //        int intSliderGreenValue = 0;

        //        bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
        //        if(greenOk)
        //        {
        //            sliderGreen.Value = intSliderGreenValue;
        //        }


        //        int intSliderBlueValue = 0;

        //        bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
        //        if(blueOk)
        //        {
        //            sliderBlue.Value = intSliderBlueValue;
        //        }
        //    }

        //    if(LeftMenuCheckBox.IsChecked == true)
        //    {

        //        System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(stackpanelMenu.Background.ToString());

        //        int intSliderRedValue = 0;
        //        bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
        //        if(redOk)
        //        {
        //            sliderRed.Value = intSliderRedValue;
        //        }

        //        int intSliderGreenValue = 0;
        //        bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
        //        if(greenOk)
        //        {
        //            sliderGreen.Value = intSliderGreenValue;
        //        }

        //        int intSliderBlueValue = 0;
        //        bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
        //        if(blueOk)
        //        {
        //            sliderBlue.Value = intSliderBlueValue;
        //        }

        //    }

        //    if(WindowContentCheckBox.IsChecked == true)
        //    {

        //        System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(gridMainWindow.Background.ToString());

        //        int intSliderRedValue = 0;
        //        bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
        //        if(redOk)
        //        {
        //            sliderRed.Value = intSliderRedValue;
        //        }

        //        int intSliderGreenValue = 0;

        //        bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
        //        if(greenOk)
        //        {
        //            sliderGreen.Value = intSliderGreenValue;
        //        }

        //        int intSliderBlueValue = 0;
        //        bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
        //        if(blueOk)
        //        {
        //            sliderBlue.Value = intSliderBlueValue;
        //        }

        //    }

        //}


        private void NotifyIcon_MouseDown(object Sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Visibility = Visibility.Visible;
                this.Activate();
            }



            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MethodInfo mi = typeof(System.Windows.Forms.NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);

            }

        }
        private void ResetWindowPosition_Click(object Sender, EventArgs e)
        {

            ResetWindowPosition();

        }
        private void ResetWindowPosition()
        {
            double[] centerScreenPos = Monitor.GetPrimaryMonitorCenterPosition(this.Width, this.Height);
            this.Left = centerScreenPos[0];
            this.Top = centerScreenPos[1];
        }


        private void TitleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoveWindow();
        }
        private void MoveWindow()
        {
            try
            {
                DragMove();
            }
            catch(Exception)
            {

                throw;
            }
        }


        private void UserInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(UserInputTextBox.Text != "")
            {
                EnableOKButton();
            }
            else { DisableOKButton(); }
        }
        private void UserInputTextBox_KeyUp(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Escape)
            {
                UserInputTextBox.Clear();
            }

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string cleanedItem = UserInputTextBox.Text.Trim();
            OpenUserInputInServiceNow(cleanedItem);
        }
        private void OpenUserInputInServiceNow(string item)
        {

            UserInputTextBox.Clear();

            RemoveOldEntriesFromListView();
            ServiceNowItem newSNItem = new ServiceNowItem(item, CIPrefixTextBox.Text, PeripheralPrefixTextBox.Text, UserNamesPrefixTextBox.Text, ServiceNowPortalTextBox.Text);

            if(FreeTextSearchCheckBox.IsChecked == true)
            {
                newSNItem.ForceFreeTextSearch = true;
            }
            else { newSNItem.ForceFreeTextSearch = false; }


            newSNItem.ExecuteProcess();
            RecentlyOpenedItem newRecentItem = new RecentlyOpenedItem(newSNItem.Item, newSNItem.Url, DateTime.Now);
            recentlyOpenedItems.RecentItems.Insert(0, newRecentItem);
            UserInputTextBox.Focus();

        }
        private void RemoveOldEntriesFromListView()
        {
            int recenItemsCount = recentlyOpenedItems.RecentItems.Count;
            if(recenItemsCount >= 100)
            {
                recentlyOpenedItems.RecentItems.RemoveAt(recenItemsCount - 1);
            }
        }
        private void EnableOKButton()
        {
            GoButton.IsEnabled = true;
            GoButton.Opacity = 1;

        }
        private void DisableOKButton()
        {

            GoButton.IsEnabled = false;
            GoButton.Opacity = 0.2;

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            CloseApplication();
        }
        private void MenuItemExit_Click(object Sender, EventArgs e)
        {

            CloseApplication();
        }
        private void CloseApplication()
        {

            SaveSettings();
            notifyIcon.Dispose();
            this.Close();
        }
        private void SaveSettings()
        {
            Settings settingsServiceNow = new Settings
            {
                ServiceNowTheme = serviceNowTheme,
                Opacity = this.Opacity,
                Left = this.Left,
                Top = this.Top,
                TopMost = (bool)AlwaysOnTopCheckBox.IsChecked,
                FreeTextSearch = (bool)FreeTextSearchCheckBox.IsChecked,
                SliderPosition = sliderOpacityValue.Value,
                RecentItems = recentlyOpenedItems,
                MinimizeToTray = (bool)HideFromTaskBarCheckBox.IsChecked,
                URLServiceNowPortal = ServiceNowPortalTextBox.Text,
                RegExConfigurationItems = CIPrefixTextBox.Text,
                RegExPeripherals = PeripheralPrefixTextBox.Text,
                RegExUsernames = UserNamesPrefixTextBox.Text,


            };

            settingsServiceNow.Save();

        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            WindowGotMiniMizedActions();
        }
        private void WindowGotMiniMizedActions()
        {


            if(HideFromTaskBarCheckBox.IsChecked == false)
            {
                this.WindowState = WindowState.Minimized;
                this.ShowInTaskbar = true;
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                this.ShowInTaskbar = false;

            }
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "ServiceNowOpen";
            gridMainContent.Visibility = Visibility.Visible;
            gridRecentlyOpenedItems.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
            gridAbout.Visibility = Visibility.Hidden;
            FocusInputTextBox();

        }
        private void RecentlyOpenedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Recently Opened Items";
            gridMainContent.Visibility = Visibility.Hidden;
            gridRecentlyOpenedItems.Visibility = Visibility.Visible;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
            gridAbout.Visibility = Visibility.Hidden;
        }
        private void ThemesButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Theme";
            gridMainContent.Visibility = Visibility.Hidden;
            gridRecentlyOpenedItems.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Visible;
            gridAbout.Visibility = Visibility.Hidden;
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Settings";
            gridSettingsContent.Visibility = Visibility.Visible;
            gridMainContent.Visibility = Visibility.Hidden;
            gridRecentlyOpenedItems.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
            gridAbout.Visibility = Visibility.Hidden;
        }
        private void AboutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text ="About ServiceNowOpen";
            gridAbout.Visibility = Visibility.Visible;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridMainContent.Visibility = Visibility.Hidden;
            gridRecentlyOpenedItems.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
        }


        private void MainWindow_Activated(object sender, EventArgs e)
        {
            FocusInputTextBox();
        }
        private void MainWindow_GotFocus(object sender, RoutedEventArgs e)
        {
            FocusInputTextBox();
        }
        private void FocusInputTextBox()
        {
            UserInputTextBox.Focus();

        }


        private void ListViewRecentlyOpenedItems_KeyDown(object sender, KeyEventArgs e)
        {


            if((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) // Is Alt key pressed
            {
                if(Keyboard.IsKeyDown(Key.C))
                {
                    try
                    {
                        SetSelectedItemToClipBoard();
                    }
                    catch(Exception)
                    {

                    }
                }
            }


            if(e.Key == Key.Enter || e.OriginalSource is System.Windows.Controls.TextBlock)
            {
                OpenSelectedItemInBrowser();
            }

        }
        private void ListViewRecentlyOpenedItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(e.OriginalSource is System.Windows.Controls.TextBlock)
            {
                OpenSelectedItemInBrowser();
            }
        }
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if(listViewRecentlyOpenedItems.Items.Count > 0 && listViewRecentlyOpenedItems.SelectedIndex >= 0)
            {
                SetSelectedItemToClipBoard();
            }
        }
        private void OpenInBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            if(listViewRecentlyOpenedItems.Items.Count > 0 && listViewRecentlyOpenedItems.SelectedIndex >= 0)
            {
                OpenSelectedItemInBrowser();
            }
        }
        private void SetSelectedItemToClipBoard()
        {
            try
            {
                RecentlyOpenedItem selectedItem = GetSelectedItem();
                Clipboard.SetText(selectedItem.Item);
            }
            catch(Exception)
            {

            }

        }
        private void OpenSelectedItemInBrowser()
        {
            try
            {
                RecentlyOpenedItem selectedItem = GetSelectedItem();
                OpenUserInputInServiceNow(selectedItem.Item);
            }
            catch(Exception)
            {

            }
            if(listViewRecentlyOpenedItems.Items.Count > 0)
            {

            }
        }
        private RecentlyOpenedItem GetSelectedItem()
        {
            try
            {
                int selectedItem = listViewRecentlyOpenedItems.SelectedIndex;
                RecentlyOpenedItem item = recentlyOpenedItems.RecentItems.ElementAt(selectedItem);
                return item;
            }
            catch(Exception)
            {
                return null;
            }

        }


        private void AlwaysOnTopCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }
        private void AlawaysOnTopCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }
        private void HideFromTaskBarCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ToggleHideFromTaskBar();
        }
        private void HideFromTaskBarCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleHideFromTaskBar();
        }
        private void ToggleHideFromTaskBar()
        {
            if(HideFromTaskBarCheckBox.IsChecked == true)
            {
              
                this.ShowInTaskbar = false;
            }

            if(HideFromTaskBarCheckBox.IsChecked == false)
            {
                this.ShowInTaskbar = true;
            }
        }


        private void SliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
            ChangeTextColor();
        }
        private void SliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
            ChangeTextColor();
        }
        private void SliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
            ChangeTextColor();
        }
        private void SetWindowColorsToSelectSliderColor()
        {

            if(ButtonColorCheckBox.IsChecked == true)
            {

                ChangeButtonColor();

            }

            if(TitleBarCheckBox.IsChecked == true && ButtonColorCheckBox.IsChecked == false && TextColorCheckBox.IsChecked == false)
            {
                serviceNowTheme.TitleBarBackgroundColor = serviceNowTheme.ConvertRGBToHexColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);

                SetWindowBackgroundColors();

            }

            if(WindowContentCheckBox.IsChecked == true && ButtonColorCheckBox.IsChecked == false && TextColorCheckBox.IsChecked == false)
            {
                serviceNowTheme.MainWindowBackgroundColor = serviceNowTheme.ConvertRGBToHexColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowBackgroundColors();
            }

            if(LeftMenuCheckBox.IsChecked == true && ButtonColorCheckBox.IsChecked == false && TextColorCheckBox.IsChecked == false)
            {
                serviceNowTheme.MenuBackgroundColor = serviceNowTheme.ConvertRGBToHexColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowBackgroundColors();
            }

        }
        private void ChangeButtonColor()
        {

            byte[] colors = new byte[3];
            colors[0] = (byte)sliderRed.Value;
            colors[1] = (byte)sliderGreen.Value;
            colors[2] = (byte)sliderBlue.Value;


            // Menu Button Colors
            if(ButtonColorCheckBox.IsChecked == true && LeftMenuCheckBox.IsChecked == true)
            {
                serviceNowTheme.MenuButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
                serviceNowTheme.MenuPanelButtonsRGB = colors;

                //Home Button
                Image imgHome = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/home-white.png")
                };

                ImageBrush backgroundHomeImage = new ImageBrush
                {
                    ImageSource = imgHome.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                HomeMenuButton.Background = backgroundHomeImage;

                //Themes Button
                Image imgTheme = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/paint-brush-64.png")
                };

                ImageBrush backgroundThemeImage = new ImageBrush
                {
                    ImageSource = imgTheme.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                ThemesMenuButton.Background = backgroundThemeImage;

                //Recently Opened Items Button
                Image imgRecentItems = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/recentitems-white.png")
                };

                ImageBrush backgroundRecentItemsImage = new ImageBrush
                {
                    ImageSource = imgRecentItems.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                RecentItemsMenuButton.Background = backgroundRecentItemsImage;

                //Settings Button
                Image imgSettingsButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/settings-white.png")
                };

                ImageBrush backgroundSettingsButton = new ImageBrush
                {
                    ImageSource = imgSettingsButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                SettingsMenuButton.Background = backgroundSettingsButton;


                //About Button
                Image imgAboutButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/about.png")
                };

                ImageBrush backgroundAboutButton = new ImageBrush
                {
                    ImageSource = imgAboutButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                AboutMenuButton.Background = backgroundAboutButton;
            }

            // TitleBar Button Colors
            if(ButtonColorCheckBox.IsChecked == true && TitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
                serviceNowTheme.TitleBarButtonsRGB = colors;

                //Power Button
                Image imgPowerButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/close.png")
                };

                ImageBrush backgroundPowerButton = new ImageBrush
                {
                    ImageSource = imgPowerButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                CloseButton.Background = backgroundPowerButton;

                //Minimize Button
                Image imgMinimizeButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/minimize-white.png")
                };
                ImageBrush backgroundMinimizeButton = new ImageBrush
                {
                    ImageSource = imgMinimizeButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                MinimizeButton.Background = backgroundMinimizeButton;

            }


            // Main Window Button Colors
            if(ButtonColorCheckBox.IsChecked == true && WindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.MainWindowButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
                serviceNowTheme.MainWindowButtonsRGB = colors;

                //Copy Button
                Image imgCopyButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/copy-white.png")
                };
                ImageBrush backgroundCopyButton = new ImageBrush
                {
                    ImageSource = imgCopyButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                CopyButton.Background = backgroundCopyButton;

                //Open In Browser Button
                Image imgOpenInBrowserButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/openinbrowser-white.png")
                };
                ImageBrush backgroundOpenInBrowserButton = new ImageBrush
                {
                    ImageSource = imgOpenInBrowserButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                OpenInBrowserButton.Background = backgroundOpenInBrowserButton;

                //OK (CheckMark) Button
                Image imgOKButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/checkmark-white.png")
                };
                ImageBrush backgroundOKButton = new ImageBrush
                {
                    ImageSource = imgOKButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                GoButton.Background = backgroundOKButton;

                //Load File Button
                Image imgLoadFileButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/BrowseForFile.png")
                };
                ImageBrush backgroundLoadFileButton = new ImageBrush
                {
                    ImageSource = imgLoadFileButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                LoadFileButton.Background = backgroundLoadFileButton;

                //Save To File Button
                Image imgSaveToFileButton = new Image
                {
                    Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/SaveToFile.png")
                };
                ImageBrush backgroundSaveToFileButton = new ImageBrush
                {
                    ImageSource = imgSaveToFileButton.Source,
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                SaveToFileButton.Background = backgroundSaveToFileButton;
            }


        }
        private void ChangeTextColor()
        {

            // Text in title bar
            if(TextColorCheckBox.IsChecked == true && TitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarTextColor = serviceNowTheme.ConvertRGBToHexColor(sliderRed.Value, sliderGreen.Value, sliderBlue.Value);
                txtTitle.Foreground = serviceNowTheme.ConvertRGBToBrush(sliderRed.Value, sliderGreen.Value, sliderBlue.Value);

            }

            // Text in main window
            if(TextColorCheckBox.IsChecked == true && WindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.MainWindowTextColor = serviceNowTheme.ConvertRGBToHexColor(sliderRed.Value, sliderGreen.Value, sliderBlue.Value);

                Brush color = serviceNowTheme.ConvertRGBToBrush(sliderRed.Value, sliderGreen.Value, sliderBlue.Value);
                TitleBarCheckBox.Foreground = color;
                LeftMenuCheckBox.Foreground = color;
                WindowContentCheckBox.Foreground = color;
                txtRed.Foreground = color;
                txtGreen.Foreground = color;
                txtBlue.Foreground = color;
                txtOpacity.Foreground = color;
                ButtonColorCheckBox.Foreground = color;
                TextColorCheckBox.Foreground = color;
                sliderRed.Foreground = color;
                sliderGreen.Foreground = color;
                sliderBlue.Foreground = color;
                borderSliders.BorderBrush = color;
                ResetToDefaultButton.Foreground = color;
                txtVersion.Foreground = color;
                txtOpen.Foreground = color;
                FreeTextSearchCheckBox.Foreground = color;
                HideFromTaskBarCheckBox.Foreground = color;
                AlwaysOnTopCheckBox.Foreground = color;
                txtUsernames.Foreground = color;
                txtPeripherals.Foreground = color;
                txtConfigurationItems.Foreground = color;
                txtServiceNowURL.Foreground = color;
                txtServiceNowOpen.Foreground = color;
                txtDevelopedBy.Foreground = color;
                txtVersion.Foreground = color;

            }

        }


        private void SliderOpacityValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            double selectedOpacityValue = GetOpacityValue();
            SetOpacity(selectedOpacityValue);

        }
        private double GetOpacityValue()
        {

            double sliderValue = sliderOpacityValue.Value;

            if(sliderValue > 95)
            {
                return 1.0;
            }
            if(sliderValue > 90)
            {
                return 0.95;
            }

            if(sliderValue > 85)
            {
                return 0.90;
            }

            if(sliderValue > 70)
            {
                return 0.85;
            }

            if(sliderValue > 60)
            {
                return 0.80;
            }
            if(sliderValue > 60)
            {
                return 0.75;
            }
            if(sliderValue > 50)
            {
                return 0.70;
            }
            if(sliderValue > 45)
            {
                return 0.65;
            }
            if(sliderValue > 30)
            {
                return 0.60;
            }
            if(sliderValue > 25)
            {
                return 0.55;
            }
            if(sliderValue > 20)
            {
                return 0.50;
            }
            if(sliderValue > 15)
            {
                return 0.45;
            }
            if(sliderValue > 10)
            {
                return 0.40;
            }
            if(sliderValue > 5)
            {
                return 0.35;
            }
            if(sliderValue == 0)
            {
                return 0.3;
            }

            return 0.3;

        }
        private void SetOpacity(double opacity)
        {

            serviceNowTheme.Opacity = opacity;
            this.Opacity = serviceNowTheme.Opacity;

        }
        

        private void ResetToDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            serviceNowTheme.ResetTextColorandBackgroundColorToDefault();
            SetWindowBackgroundColors();
            LoadTextColors();
            LoadDefaultImages();
            serviceNowTheme.Opacity = 1;
            this.Opacity = 1;
           
        }
        private void LoadDefaultImages()
        {
            // Close Window Button
            Uri closeImageUri = new Uri("Images/close.png", UriKind.Relative);
            StreamResourceInfo closeImagestreamInfo = Application.GetResourceStream(closeImageUri);
            BitmapFrame closeImagetemp = BitmapFrame.Create(closeImagestreamInfo.Stream);
            ImageBrush closeButtonBrush = new ImageBrush
            {
                ImageSource = closeImagetemp
            };

            CloseButton.Background = closeButtonBrush;

            // Minimize Window Button
            Uri minimizeImageUri = new Uri("Images/minimize-white.png", UriKind.Relative);
            StreamResourceInfo minimizeImagestreamInfo = Application.GetResourceStream(minimizeImageUri);
            BitmapFrame minimizeImagetemp = BitmapFrame.Create(minimizeImagestreamInfo.Stream);
            ImageBrush minimizeButtonBrush = new ImageBrush
            {
                ImageSource = minimizeImagetemp
            };
            MinimizeButton.Background = minimizeButtonBrush;


            // Home Menu Button
            Uri homeImageUri = new Uri("Images/home-white.png", UriKind.Relative);
            StreamResourceInfo homeImagestreamInfo = Application.GetResourceStream(homeImageUri);
            BitmapFrame homeImagetemp = BitmapFrame.Create(homeImagestreamInfo.Stream);
            ImageBrush homeButtonBrush = new ImageBrush
            {
                ImageSource = homeImagetemp
            };

            HomeMenuButton.Background = homeButtonBrush;

            // Recent Items Menu Button
            Uri recentitemsImageUri = new Uri("Images/recentitems-white.png", UriKind.Relative);
            StreamResourceInfo recentitemsImagestreamInfo = Application.GetResourceStream(recentitemsImageUri);
            BitmapFrame recentitemsImagetemp = BitmapFrame.Create(recentitemsImagestreamInfo.Stream);
            ImageBrush recentitemsButtonBrush = new ImageBrush
            {
                ImageSource = recentitemsImagetemp
            };
            RecentItemsMenuButton.Background = recentitemsButtonBrush;

            // Themes Menu Button
            Uri themeImageUri = new Uri("Images/paint-brush-64.png", UriKind.Relative);
            StreamResourceInfo themeImagestreamInfo = Application.GetResourceStream(themeImageUri);
            BitmapFrame themeImagetemp = BitmapFrame.Create(themeImagestreamInfo.Stream);
            ImageBrush themeButtonBrush = new ImageBrush
            {
                ImageSource = themeImagetemp
            };
            ThemesMenuButton.Background = themeButtonBrush;

            // Settings Button
            Uri settingsImageUri = new Uri("Images/settings-white.png", UriKind.Relative);
            StreamResourceInfo settingsImagestreamInfo = Application.GetResourceStream(settingsImageUri);
            BitmapFrame settingsImagetemp = BitmapFrame.Create(settingsImagestreamInfo.Stream);
            ImageBrush settingsButtonBrush = new ImageBrush
            {
                ImageSource = settingsImagetemp
            };
            SettingsMenuButton.Background = settingsButtonBrush;

            // About Button
            Uri aboutImageUri = new Uri("Images/about.png", UriKind.Relative);
            StreamResourceInfo aboutImagestreamInfo = Application.GetResourceStream(aboutImageUri);
            BitmapFrame aboutImagetemp = BitmapFrame.Create(aboutImagestreamInfo.Stream);
            ImageBrush aboutButtonBrush = new ImageBrush
            {
                ImageSource = aboutImagetemp
            };
            AboutMenuButton.Background = aboutButtonBrush;


            // Recent Items - Copy Button
            Uri copyImageUri = new Uri("Images/copy-white.png", UriKind.Relative);
            StreamResourceInfo copyImagestreamInfo = Application.GetResourceStream(copyImageUri);
            BitmapFrame copyImagetemp = BitmapFrame.Create(copyImagestreamInfo.Stream);
            ImageBrush copyButtonBrush = new ImageBrush
            {
                ImageSource = copyImagetemp
            };

            CopyButton.Background = copyButtonBrush;

            // Recent Items OpenInBrowser Button
            Uri openinbrowserImageUri = new Uri("Images/openinbrowser-white.png", UriKind.Relative);
            StreamResourceInfo openinbrowserImagestreamInfo = Application.GetResourceStream(openinbrowserImageUri);
            BitmapFrame openinbrowserImagetemp = BitmapFrame.Create(openinbrowserImagestreamInfo.Stream);
            ImageBrush openinbrowserButtonBrush = new ImageBrush
            {
                ImageSource = openinbrowserImagetemp
            };

            OpenInBrowserButton.Background = openinbrowserButtonBrush;

            // Checkmark Button
            Uri checkmarkImageUri = new Uri("Images/checkmark-white.png", UriKind.Relative);
            StreamResourceInfo checkmarkImagestreamInfo = Application.GetResourceStream(checkmarkImageUri);
            BitmapFrame checkmarkImagetemp = BitmapFrame.Create(checkmarkImagestreamInfo.Stream);
            ImageBrush checkmarkButtonBrush = new ImageBrush
            {
                ImageSource = checkmarkImagetemp
            };
            GoButton.Background = checkmarkButtonBrush;

           

            // Load File Button
            Uri LoadFileImageUri = new Uri("Images/BrowseForFile.png", UriKind.Relative);
            StreamResourceInfo LoadFileImagestreamInfo = Application.GetResourceStream(LoadFileImageUri);
            BitmapFrame LoadFileImagetemp = BitmapFrame.Create(LoadFileImagestreamInfo.Stream);
            ImageBrush LoadFileButtonBrush = new ImageBrush
            {
                ImageSource = LoadFileImagetemp
            };
            LoadFileButton.Background = LoadFileButtonBrush;


            // Save To File Button
            Uri SaveToFileFileImageUri = new Uri("Images/SaveToFile.png", UriKind.Relative);
            StreamResourceInfo SaveToFileImagestreamInfo = Application.GetResourceStream(SaveToFileFileImageUri);
            BitmapFrame SaveToFileImagetemp = BitmapFrame.Create(SaveToFileImagestreamInfo.Stream);
            ImageBrush SaveToFileButtonBrush = new ImageBrush
            {
                ImageSource = SaveToFileImagetemp
            };
            SaveToFileButton.Background = SaveToFileButtonBrush;



            serviceNowTheme.TitleBarButtonColor = serviceNowTheme.DefaultButtonColor;
            serviceNowTheme.MainWindowButtonColor = serviceNowTheme.DefaultButtonColor;
            serviceNowTheme.MenuButtonColor = serviceNowTheme.DefaultButtonColor;


        }


        // TODO Add logic to set sliderbar positions for current color values on the UI
        private void TitleBarCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void MenuPanelCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           

        }
        private void WindowContentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
          

        }


        private void ServiceNowPortalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ServiceNowPortalTextBox.Text == "")
            {
                ServiceNowPortalTextBox.Text = "Enter url ServiceNow portal";
            }
        }
        private void CIPrefixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(CIPrefixTextBox.Text == "")
            {
                CIPrefixTextBox.Text = "Enter a regex pattern for configuration items";
            }
        }
        private void PeripheralPrefixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(PeripheralPrefixTextBox.Text == "")
            {
                PeripheralPrefixTextBox.Text = "Enter a regex pattern for peripherals";
            }
        }
        private void UserNamesPrefixTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(UserNamesPrefixTextBox.Text == "")
            {
                UserNamesPrefixTextBox.Text = "Enter a regex pattern for usernames";
            }
           
        }
        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }
        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile();
        }
        private void SaveToFile()
        {
                ServiceNowConfig snConfig = new ServiceNowConfig
                {
                    URLServiceNowPortal = ServiceNowPortalTextBox.Text,
                    RegExPatternCI = CIPrefixTextBox.Text,
                    RegExPatternPeripherals = PeripheralPrefixTextBox.Text,
                    RegExPatternUsers = UserNamesPrefixTextBox.Text,
                };

                snConfig.Save();

        }
        private void LoadFile()
        {

            ServiceNowConfig snConfigLoad = new ServiceNowConfig();
            ServiceNowConfig snConfigLoaded = snConfigLoad.Load();

            if(snConfigLoaded != null)
            {
                ServiceNowPortalTextBox.Text = snConfigLoaded.URLServiceNowPortal;
                CIPrefixTextBox.Text = snConfigLoaded.RegExPatternCI;
                PeripheralPrefixTextBox.Text = snConfigLoaded.RegExPatternPeripherals;
                UserNamesPrefixTextBox.Text = snConfigLoaded.RegExPatternUsers;
            }

        }
    }
}


