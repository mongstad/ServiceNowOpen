using ServiceNow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Image = System.Windows.Controls.Image;
using Monitor = Computer.Monitor;

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
            AddHomepageLink();

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
            HideFromTaskBarCheckBox.IsChecked = snSettings.HideFromTaskbar;
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
            }
            else
            {
                gridMainWindow.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MainWindowDefaultBackgroundColor);
            }

            if(!(serviceNowTheme.TitleBarBackgroundColor == ""))
            {
                TitleBarGrid.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarBackgroundColor);
            }
            else
            {
                TitleBarGrid.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarDefaultBackgroundColor);
            }

            if(!(serviceNowTheme.MenuBackgroundColor == ""))
            {
                stackpanelMenu.Background = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MenuBackgroundColor);
            }
            else
            {
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

                //Home Button @"Images\Home.png"
                ImageBrush backgroundHomeImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], @"Images\Home.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                HomeMenuButton.Background = backgroundHomeImage;


                //Themes Button @"Images\Theme.png"
                ImageBrush backgroundThemeImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], @"Images\Theme.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                ThemesMenuButton.Background = backgroundThemeImage;

                //Recently Opened Items Button @"Images\RecentlyOpenedItems.png"
                ImageBrush backgroundRecentItemsImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], @"Images\RecentlyOpenedItems.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                RecentItemsMenuButton.Background = backgroundRecentItemsImage;

                //Settings Button @"Images\Settings.png"
                ImageBrush backgroundSettingsButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], @"Images\Settings.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                SettingsMenuButton.Background = backgroundSettingsButton;


                //About Button @"Images\About.png"
                ImageBrush backgroundAboutButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MenuPanelButtonsRGB[0], serviceNowTheme.MenuPanelButtonsRGB[1], serviceNowTheme.MenuPanelButtonsRGB[2], @"Images\About.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                AboutMenuButton.Background = backgroundAboutButton;

            }

            if(serviceNowTheme.TitleBarButtonColor != serviceNowTheme.DefaultButtonColor)
            {
                //Power Button @"Images\Close.png"
                ImageBrush backgroundPowerButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.TitleBarButtonsRGB[0], serviceNowTheme.TitleBarButtonsRGB[1], serviceNowTheme.TitleBarButtonsRGB[2], @"Images\Close.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                CloseButton.Background = backgroundPowerButton;

                //Minimize Button @"Images\Minimize.png"
                ImageBrush backgroundMinimizeButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.TitleBarButtonsRGB[0], serviceNowTheme.TitleBarButtonsRGB[1], serviceNowTheme.TitleBarButtonsRGB[2], @"Images\Minimize.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                MinimizeButton.Background = backgroundMinimizeButton;
            }


            if(serviceNowTheme.MainWindowButtonColor != serviceNowTheme.DefaultButtonColor)
            {

                //Copy Button @"Image\Copy.png"
                ImageBrush backgroundCopyButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], @"Images\Copy.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                CopyButton.Background = backgroundCopyButton;

                //Open In Browser Button  @"Images\OpenInBrowser.png"
                ImageBrush backgroundOpenInBrowserButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], @"Images\OpenInBrowser.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                OpenInBrowserButton.Background = backgroundOpenInBrowserButton;

                //OK (CheckMark) Button @"Images\Checkmark.png"
                ImageBrush backgroundOKButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], @"Images\Checkmark.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                GoButton.Background = backgroundOKButton;

                //Load File Button @"Images\BrowseForFile.png" 
                ImageBrush backgroundLoadFileButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], @"Images\BrowseForFile.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                LoadFileButton.Background = backgroundLoadFileButton;

                //Save To File Button @"Images\SaveToFile.png"
                ImageBrush backgroundSaveToFileButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, serviceNowTheme.MainWindowButtonsRGB[0], serviceNowTheme.MainWindowButtonsRGB[1], serviceNowTheme.MainWindowButtonsRGB[2], @"Images\SaveToFile.png"),
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
                System.Windows.Media.Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.MainWindowTextColor);

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
                txtHomePage.Foreground = textColor;
            }
            else
            {
                System.Windows.Media.Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.DefaultTextColor);


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
                System.Windows.Media.Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.TitleBarTextColor);
                txtTitle.Foreground = textColor;
            }
            else
            {
                System.Windows.Media.Brush textColor = serviceNowTheme.ConvertHexColorToBrush(serviceNowTheme.DefaultTextColor);
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
        public void AddHomepageLink()
        {
            var homePageLink = new Hyperlink();
            homePageLink.Inlines.Add("https://github.com/mongstad/ServiceNowOpen");
            HomePageTextBlock.Inlines.Add(homePageLink);
        }

        private void NotifyIcon_MouseDown(object Sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if(this.ShowInTaskbar == false && this.Visibility == Visibility.Hidden)
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                    return;
                }

                if(this.ShowInTaskbar == false && this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                    return;
                }

                if(this.ShowInTaskbar == true && this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Minimized;
                    return;
                }

                if(this.ShowInTaskbar == true && this.WindowState == WindowState.Minimized)
                {
                   
                    this.WindowState = WindowState.Normal;
                    this.Activate();
                    return;
                }

              

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
            double[] centerScreenPos = Monitor.GetProgramCenterWindowPosition(this.Width, this.Height);
            this.Left = centerScreenPos[0];
            this.Top = centerScreenPos[1];
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

            try
            {
                newSNItem.ExecuteProcess();
                RecentlyOpenedItem newRecentItem = new RecentlyOpenedItem(newSNItem.Item, newSNItem.Url, DateTime.Now);
                recentlyOpenedItems.RecentItems.Insert(0, newRecentItem);
            }
            catch(Exception)
            {
               
            }
          
          
            
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

            CloseApplication();
        }
        private void MenuItemExit_Click(object Sender, EventArgs e)
        {

            CloseApplication();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            WindowMinimized();
        }
        private void WindowMinimized()
        {


            if(HideFromTaskBarCheckBox.IsChecked == true)
            {
                this.Visibility = Visibility.Hidden;


            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
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
                HideFromTaskbar = (bool)HideFromTaskBarCheckBox.IsChecked,
                URLServiceNowPortal = ServiceNowPortalTextBox.Text,
                RegExConfigurationItems = CIPrefixTextBox.Text,
                RegExPeripherals = PeripheralPrefixTextBox.Text,
                RegExUsernames = UserNamesPrefixTextBox.Text,


            };

            settingsServiceNow.Save();

        }
       

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "ServiceNowOpen";
            HomeGrid.Visibility = Visibility.Visible;
            RecentlyOpenedItemsGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;
            ThemeGrid.Visibility = Visibility.Hidden;
            AboutGrid.Visibility = Visibility.Hidden;
            FocusInputTextBox();
         

        }
        private void RecentlyOpenedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Recently Opened Items";
            HomeGrid.Visibility = Visibility.Hidden;
            RecentlyOpenedItemsGrid.Visibility = Visibility.Visible;
            SettingsGrid.Visibility = Visibility.Hidden;
            ThemeGrid.Visibility = Visibility.Hidden;
            AboutGrid.Visibility = Visibility.Hidden;
        }
        private void ThemesButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Theme";
            HomeGrid.Visibility = Visibility.Hidden;
            RecentlyOpenedItemsGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Hidden;
            ThemeGrid.Visibility = Visibility.Visible;
            AboutGrid.Visibility = Visibility.Hidden;
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "Settings";
            SettingsGrid.Visibility = Visibility.Visible;
            HomeGrid.Visibility = Visibility.Hidden;
            RecentlyOpenedItemsGrid.Visibility = Visibility.Hidden;
            ThemeGrid.Visibility = Visibility.Hidden;
            AboutGrid.Visibility = Visibility.Hidden;
        }
        private void AboutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            txtTitle.Text = "About ServiceNowOpen";
            AboutGrid.Visibility = Visibility.Visible;
            SettingsGrid.Visibility = Visibility.Hidden;
            HomeGrid.Visibility = Visibility.Hidden;
            RecentlyOpenedItemsGrid.Visibility = Visibility.Hidden;
            ThemeGrid.Visibility = Visibility.Hidden;
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


        // TODO Add logic to set sliderbar positions for current color values on the UI
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

                ImageBrush backgroundHomeImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255,colors[0],colors[1], colors[2], @"Images\Home.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
               
                HomeMenuButton.Background = backgroundHomeImage;

                //Themes Button @"Images\Theme.png"
                ImageBrush backgroundThemeImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Theme.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                ThemesMenuButton.Background = backgroundThemeImage;

                //Recently Opened Items Button @"Images\RecentlyOpenedItems.png"
                ImageBrush backgroundRecentItemsImage = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\RecentlyOpenedItems.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                RecentItemsMenuButton.Background = backgroundRecentItemsImage;

                //Settings Button @"Images\Settings.png"
                ImageBrush backgroundSettingsButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Settings.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                SettingsMenuButton.Background = backgroundSettingsButton;


                //About Button @"Images\About.png"
                ImageBrush backgroundAboutButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\About.png"),
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

                //Power Button @"Images\Close.png"
                ImageBrush backgroundPowerButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Close.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                CloseButton.Background = backgroundPowerButton;

                //Minimize Button @"Images\Minimize.png"
                ImageBrush backgroundMinimizeButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Minimize.png"),
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

                //Copy Button @"Image\Copy.png"
                ImageBrush backgroundCopyButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Copy.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                CopyButton.Background = backgroundCopyButton;

                //Open In Browser Button  @"Images\OpenInBrowser.png"
                ImageBrush backgroundOpenInBrowserButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\OpenInBrowser.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };

                OpenInBrowserButton.Background = backgroundOpenInBrowserButton;

                //OK (CheckMark) Button @"Images\Checkmark.png"
                ImageBrush backgroundOKButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\Checkmark.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                GoButton.Background = backgroundOKButton;

                //Load File Button @"Images\BrowseForFile.png" 
                ImageBrush backgroundLoadFileButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\BrowseForFile.png"),
                    Stretch = Stretch.Fill,
                    TileMode = TileMode.None
                };
                LoadFileButton.Background = backgroundLoadFileButton;

                //Save To File Button @"Images\SaveToFile.png"
                ImageBrush backgroundSaveToFileButton = new ImageBrush
                {
                    ImageSource = ImageManipulation.ChangeImageColor(255, colors[0], colors[1], colors[2], @"Images\SaveToFile.png"),
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

                System.Windows.Media.Brush color = serviceNowTheme.ConvertRGBToBrush(sliderRed.Value, sliderGreen.Value, sliderBlue.Value);
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
                txtHomePage.Foreground = color;

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
                return 0.925;
            }

            if(sliderValue > 80)
            {
                return 0.9;
            }

            if(sliderValue > 75)
            {
                return 0.875;
            }
            if(sliderValue > 70)
            {
                return 0.85;
            }
            if(sliderValue > 65)
            {
                return 0.825;
            }
            if(sliderValue > 60)
            {
                return 0.8;
            }
            if(sliderValue > 55)
            {
                return 0.775;
            }
            if(sliderValue > 50)
            {
                return 0.750;
            }
            if(sliderValue > 45)
            {
                return 0.725;
            }
            if(sliderValue > 40)
            {
                return 0.7;
            }
            if(sliderValue > 35)
            {
                return 0.675;
            }
            if(sliderValue > 30)
            {
                return 0.65;
            }
            if(sliderValue > 25)
            {
                return 0.625;
            }
            if(sliderValue > 20)
            {
                return 0.6;
            }
            if(sliderValue > 20)
            {
                return 0.575;
            }
            if(sliderValue > 15)
            {
                return 0.55;
            }
            if(sliderValue > 10)
            {
                return 0.525;
            }
            if(sliderValue > 5)
            {
                return 0.5;
            }
            if(sliderValue >= 0)
            {
                return 0.475;
            }

            return 0.45;

        }
        private void SetOpacity(double opacity)
        {

            serviceNowTheme.Opacity = opacity;
            this.Opacity = serviceNowTheme.Opacity;

        }
        private void ResetToDefaultButton_Click(object sender, RoutedEventArgs e)
        {
            
            ResetTheme();
          
        }
        private void ResetTheme()
        {
            ResetButtonColors();
            serviceNowTheme.ResetTextColorandBackgroundColorToDefault();
            LoadTextColors();
            SetWindowBackgroundColors();
            this.Opacity = 1;

        }
        private void ResetButtonColors()
        {
            byte[] colors = new byte[3];
            colors[0] = serviceNowTheme.ConvertFromHexToRGB(serviceNowTheme.DefaultButtonColor).R;
            colors[1] = serviceNowTheme.ConvertFromHexToRGB(serviceNowTheme.DefaultButtonColor).R;
            colors[2] = serviceNowTheme.ConvertFromHexToRGB(serviceNowTheme.DefaultButtonColor).R;

            serviceNowTheme.MenuButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
            serviceNowTheme.MenuPanelButtonsRGB = colors;
            serviceNowTheme.TitleBarButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
            serviceNowTheme.TitleBarButtonsRGB = colors;
            serviceNowTheme.MainWindowButtonColor = serviceNowTheme.ConvertRGBToHexColor(colors[0], colors[1], colors[2]);
            serviceNowTheme.MainWindowButtonsRGB = colors;
            LoadButtonColors();
        }
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
                ServiceNowPortalTextBox.Text = "Enter url to ServiceNow portal";
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

            try{
                snConfig.Save();
            }
            catch(Exception)
            {

            }
           

        }
        private void LoadFile()
        {

            ServiceNowConfig snConfigLoad = new ServiceNowConfig();

            try{

                ServiceNowConfig snConfigLoaded = snConfigLoad.Load();

                if(snConfigLoaded != null)
                {
                    ServiceNowPortalTextBox.Text = snConfigLoaded.URLServiceNowPortal;
                    CIPrefixTextBox.Text = snConfigLoaded.RegExPatternCI;
                    PeripheralPrefixTextBox.Text = snConfigLoaded.RegExPatternPeripherals;
                    UserNamesPrefixTextBox.Text = snConfigLoaded.RegExPatternUsers;
                }

            }
            catch(Exception)
            {
               
            }
           

        }
        private void HomePageTextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenHomepage();
        }
        private void OpenHomepage()
        {
            Process.Start("https://github.com/mongstad/ServiceNowOpen");
        }
    }
}


