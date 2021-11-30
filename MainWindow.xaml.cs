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
using System.Windows.Navigation;
using System.Windows.Resources;

namespace ServiceNowOpen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        RecentlyOpenedItems recentlyOpenedItems = new RecentlyOpenedItems();
        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
        ServiceNowTheme serviceNowTheme = new ServiceNowTheme();
        public MainWindow()
        {

            this.DataContext = recentlyOpenedItems;
            InitializeComponent();

            if(txtItem.Text == "")
            {
                btnOK.IsEnabled = false;

            }

            LoadSettings();
            SetWindowColors();
            SetRGBSliderValues();
            SetNotifyIconSettings();
            SetVersionInfo();

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
            serviceNowTheme.TitleBarButtonImagesInverted = snSettings.TitleBarImagesInverted;
            serviceNowTheme.TitleBarTextColorInverted = snSettings.TitleBarTextColorInverted;
            serviceNowTheme.WindowContentButtonImagesInverted = snSettings.WindowContentImagesInverted;
            serviceNowTheme.WindowContentTextColorInverted = snSettings.WindowContentTextColorInverted;
            serviceNowTheme.MenuPanelImagesInverted = snSettings.MenuPanelImagesInverted;

            //SetWhiteButtonImages();
           // SetBlackButtonImages();
            SetWhiteText();
            SetBlackText();


            this.Top = snSettings.Top;
            this.Left = snSettings.Left;
            sliderOpacityValue.Value = snSettings.SliderPosition;
            chkBoxAlwaysOnTop.IsChecked = snSettings.TopMost;
            chkBoxFreeTextSearch.IsChecked = snSettings.FreeTextSearch;
            chkMinimizeToSystemTray.IsChecked = snSettings.MinimizeToTray;
           
            if(snSettings.CenterWindowBrushString != "")
            {
                serviceNowTheme.CenterWindowBackground = serviceNowTheme.ConvertStringToBrush(snSettings.CenterWindowBrushString);

            } else {
                serviceNowTheme.CenterWindowBackground = serviceNowTheme.ConvertStringToBrush(serviceNowTheme.CenterWindowDefaultHexColor);

            }

            if(snSettings.MenuBrushString != "")
            {
                serviceNowTheme.MenuBackground = serviceNowTheme.ConvertStringToBrush(snSettings.MenuBrushString);

            } else {
                serviceNowTheme.MenuBackground = serviceNowTheme.ConvertStringToBrush(serviceNowTheme.MenuDefaultHexColor);
            }

            if(snSettings.TitleBarBrushString != "")
            {
                serviceNowTheme.TitleBarBackground = serviceNowTheme.ConvertStringToBrush(snSettings.TitleBarBrushString);

            } else {
                serviceNowTheme.TitleBarBackground = serviceNowTheme.ConvertStringToBrush(serviceNowTheme.TitleBarDefaultHexColor);
            }

            foreach(RecentlyOpenedItem item in snSettings.RecentItems.RecentItems)
            {
                recentlyOpenedItems.RecentItems.Add(item);
            }

        }

        private void SetWindowColors()
        {
            //set the background colors for the different grid and stackpanels
            gridMasterContentGrid.Background = serviceNowTheme.CenterWindowBackground;
            stackPanelMenu.Background = serviceNowTheme.MenuBackground;
            gridTitleGrid.Background = serviceNowTheme.TitleBarBackground;
            this.Opacity = serviceNowTheme.Opacity;

            //adjust styles for buttons and images

        }
        private void SetRGBSliderValues()
        {
            if(chkTitleBarCheckBox.IsChecked == true)
            {


                System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(gridTitleGrid.Background.ToString());


                int intSliderRedValue = 0;

                bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
                if(redOk)
                {
                    sliderRed.Value = intSliderRedValue;
                }


                int intSliderGreenValue = 0;

                bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
                if(greenOk)
                {
                    sliderGreen.Value = intSliderGreenValue;
                }


                int intSliderBlueValue = 0;

                bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
                if(blueOk)
                {
                    sliderBlue.Value = intSliderBlueValue;
                }
            }

            if(chkMenuCheckBox.IsChecked == true)
            {

                System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(stackPanelMenu.Background.ToString());

                int intSliderRedValue = 0;
                bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
                if(redOk)
                {
                    sliderRed.Value = intSliderRedValue;
                }

                int intSliderGreenValue = 0;
                bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
                if(greenOk)
                {
                    sliderGreen.Value = intSliderGreenValue;
                }

                int intSliderBlueValue = 0;
                bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
                if(blueOk)
                {
                    sliderBlue.Value = intSliderBlueValue;
                }

            }

            if(chkWindowContentCheckBox.IsChecked == true)
            {

                System.Drawing.Color selectedOptionColor = serviceNowTheme.ConvertFromHexToRGB(gridMasterContentGrid.Background.ToString());

                int intSliderRedValue = 0;
                bool redOk = Int32.TryParse(selectedOptionColor.R.ToString(), out intSliderRedValue);
                if(redOk)
                {
                    sliderRed.Value = intSliderRedValue;
                }

                int intSliderGreenValue = 0;

                bool greenOk = Int32.TryParse(selectedOptionColor.G.ToString(), out intSliderGreenValue);
                if(greenOk)
                {
                    sliderGreen.Value = intSliderGreenValue;
                }

                int intSliderBlueValue = 0;
                bool blueOk = Int32.TryParse(selectedOptionColor.B.ToString(), out intSliderBlueValue);
                if(blueOk)
                {
                    sliderBlue.Value = intSliderBlueValue;
                }

            }

        }

        private void SetNotifyIconSettings()
        {

            notifyIcon.Text = "ServiceNow Open";
            notifyIcon.Icon = new System.Drawing.Icon("now-agent-icon.ico");
            notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown);

            System.Windows.Forms.MenuItem menuItemExit = new System.Windows.Forms.MenuItem();
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItemExit });
            menuItemExit.Index = 0;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(MenuItemExit_Click);
            notifyIcon.ContextMenu = contextMenu;

        }

        private void SetVersionInfo()
        {

            Assembly thisAssem = typeof(MainWindow).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();

            Version ver = thisAssemName.Version;
            txtVersion.Text += ver.ToString();
        }

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
        private void MenuItemExit_Click(object Sender, EventArgs e)
        {

            CloseApplication();
        }

        private void WindowGotMiniMizedActions()
        {


            if(chkMinimizeToSystemTray.IsChecked == false)
            {
                this.WindowState = WindowState.Minimized;
                notifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
            else
            {
                notifyIcon.Visible = true;
                this.Visibility = Visibility.Hidden;
                this.ShowInTaskbar = false;

            }
        }

        private void TitleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ItemTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtItem.Text != "")
            {
                EnableOKButton();
            }
            else { DisableOKButton(); }
        }

        private void EnableOKButton()
        {
            btnOK.IsEnabled = true;
            btnOK.Opacity = 1;

        }

        private void DisableOKButton()
        {

            btnOK.IsEnabled = false;
            btnOK.Opacity = 0.2;

        }

        private void ItemTextBox_KeyUp(object sender, KeyEventArgs e)
        {

            if(e.Key == Key.Escape)
            {
                txtItem.Clear();
            }

        }

        private void OpenItemInServiceNow(string item)
        {

            txtItem.Clear();

            RemoveOldEntries();
            ServiceNowItem newSNItem = new ServiceNowItem(item);

            if(chkBoxFreeTextSearch.IsChecked == true)
            {
                newSNItem.ForceFreeTextSearch = true;
            }
            else { newSNItem.ForceFreeTextSearch = false; }


            newSNItem.ExecuteProcess();
            RecentlyOpenedItem newRecentItem = new RecentlyOpenedItem(newSNItem.Item, newSNItem.Url, DateTime.Now);
            recentlyOpenedItems.RecentItems.Insert(0, newRecentItem);
            txtItem.Focus();

        }

        private void RemoveOldEntries()
        {
            int recenItemsCount = recentlyOpenedItems.RecentItems.Count;
            if(recenItemsCount >= 100)
            {
                recentlyOpenedItems.RecentItems.RemoveAt(recenItemsCount - 1);
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
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

                Inverted = (bool)ChkBox_ButtonColors.IsChecked,
                Opacity = this.Opacity,
                Left = this.Left,
                Top = this.Top,
                TopMost = (bool)chkBoxAlwaysOnTop.IsChecked,
                FreeTextSearch = (bool)chkBoxFreeTextSearch.IsChecked,
                SliderPosition = sliderOpacityValue.Value,
                RecentItems = recentlyOpenedItems,
                MinimizeToTray = (bool)chkMinimizeToSystemTray.IsChecked,
                TitleBarImagesInverted = serviceNowTheme.TitleBarButtonImagesInverted,
                TitleBarTextColorInverted = serviceNowTheme.TitleBarTextColorInverted,
                WindowContentImagesInverted = serviceNowTheme.WindowContentButtonImagesInverted,
                WindowContentTextColorInverted = serviceNowTheme.WindowContentTextColorInverted,
                MenuPanelImagesInverted = serviceNowTheme.MenuPanelImagesInverted
                

            };


            BrushConverter brushConverter = new BrushConverter();
            settingsServiceNow.TitleBarBrushString = brushConverter.ConvertToString(serviceNowTheme.TitleBarBackground);
            settingsServiceNow.MenuBrushString = brushConverter.ConvertToString(serviceNowTheme.MenuBackground);
            settingsServiceNow.CenterWindowBrushString = brushConverter.ConvertToString(serviceNowTheme.CenterWindowBackground);
            settingsServiceNow.Save();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Visible;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
            FocusSearchBox();

        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Visible;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
        }


        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

            WindowGotMiniMizedActions();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Visible;
            gridColorPalette.Visibility = Visibility.Hidden;
        }

        private void SliderOpacityValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            double selectedOpacityValue = GetOpacityValue();
            SetOpacity(selectedOpacityValue);

        }

        private void SetOpacity(double opacity)
        {

            serviceNowTheme.Opacity = opacity;
            this.Opacity = serviceNowTheme.Opacity;

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

            if(sliderValue > 80)
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

        private void FocusSearchBox()
        {
            txtItem.Focus();

        }

        private void MainWindow1_Activated(object sender, EventArgs e)
        {
            FocusSearchBox();
        }

        private void MainWindow1_GotFocus(object sender, RoutedEventArgs e)
        {
            FocusSearchBox();
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
                OpenItemInServiceNow(selectedItem.Item);
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

        private void MinimizeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ProcessMinimizeToTrayState();
        }

        private void MinimizeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ProcessMinimizeToTrayState();
        }

        private void ProcessMinimizeToTrayState()
        {
            if(chkMinimizeToSystemTray.IsChecked == true)
            {
                notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
            }

            if(chkMinimizeToSystemTray.IsChecked == false)
            {
                notifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void RadioLeftMenuButton_Checked(object sender, RoutedEventArgs e)
        {
            SetRGBSliderValues();
        }

        private void SliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
        }

        private void SliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
        }
        private void SliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();
            ChangeButtonColor();
        }

        private void SetWindowColorsToSelectSliderColor()
        {

            if(ChkBox_ButtonColors.IsChecked == true) 
            {

              

                ChangeButtonColor();


            }

            if(chkTitleBarCheckBox.IsChecked == true && ChkBox_ButtonColors.IsChecked == false)
            {
                serviceNowTheme.TitleBarBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                
                SetWindowColors();
                
            }

            if(chkWindowContentCheckBox.IsChecked == true && ChkBox_ButtonColors.IsChecked == false)
            {
                serviceNowTheme.CenterWindowBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowColors();
            }

            if(chkMenuCheckBox.IsChecked == true && ChkBox_ButtonColors.IsChecked == false)
            {
                serviceNowTheme.MenuBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowColors();
            }

        }

        private void BtnResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            serviceNowTheme.ResetToDefaultTheme();
            SetWindowColors();
            SetRGBSliderValues();


        }

        private void SetBlackButtonImages()
        {

            if(serviceNowTheme.MenuPanelImagesInverted == true)
            {

                //Home menu
                BitmapImage bitImgHomeButton = new BitmapImage();
                bitImgHomeButton.BeginInit();
                bitImgHomeButton.UriSource = new Uri(@"\Images\home-black.png", UriKind.RelativeOrAbsolute);
                bitImgHomeButton.EndInit();

                Image imgImgHomeButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgHomeButton
                };

                btnHome.Content = imgImgHomeButton;
                btnHome.Background = new ImageBrush(bitImgHomeButton);

                //Recently Opened Items menu
                BitmapImage bitImgRecentlyOpenedItemsButton = new BitmapImage();
                bitImgRecentlyOpenedItemsButton.BeginInit();
                bitImgRecentlyOpenedItemsButton.UriSource = new Uri(@"\Images\recentitems-black.png", UriKind.RelativeOrAbsolute);
                bitImgRecentlyOpenedItemsButton.EndInit();

                Image imgRecentlyOpenedItemsButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgRecentlyOpenedItemsButton
                };

                btnHistory.Content = imgRecentlyOpenedItemsButton;
                btnHistory.Background = new ImageBrush(bitImgRecentlyOpenedItemsButton);

                //Theme Button
                BitmapImage bitImgThemeButton = new BitmapImage();
                bitImgThemeButton.BeginInit();
                bitImgThemeButton.UriSource = new Uri(@"\Images\palette-black.png", UriKind.RelativeOrAbsolute);
                bitImgThemeButton.EndInit();

                Image imgThemeButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgThemeButton
                };

                btnThemes.Content = imgThemeButton;
                btnThemes.Background = new ImageBrush(bitImgThemeButton);


                //Settings menu
                BitmapImage bitImgSettingsButton = new BitmapImage();
                bitImgSettingsButton.BeginInit();
                bitImgSettingsButton.UriSource = new Uri(@"\Images\settings-black.png", UriKind.RelativeOrAbsolute);
                bitImgSettingsButton.EndInit();

                Image imgSettingsButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgSettingsButton
                };

                btnSettings.Content = imgSettingsButton;
                btnSettings.Background = new ImageBrush(bitImgSettingsButton);

            }

            if(serviceNowTheme.TitleBarButtonImagesInverted == true)
            {
                //Close Window Button
                BitmapImage bitImgCloseWindowButton = new BitmapImage();
                bitImgCloseWindowButton.BeginInit();
                bitImgCloseWindowButton.UriSource = new Uri(@"\Images\power-black.png", UriKind.RelativeOrAbsolute);
                bitImgCloseWindowButton.EndInit();

                Image imgCloseWindowButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCloseWindowButton
                };

                btnClose.Content = imgCloseWindowButton;
                btnClose.Background = new ImageBrush(bitImgCloseWindowButton);

                //Minimize Window Button
                BitmapImage bitImgMinimizeWindowButton = new BitmapImage();
                bitImgMinimizeWindowButton.BeginInit();
                bitImgMinimizeWindowButton.UriSource = new Uri(@"\Images\minimize-black.png", UriKind.RelativeOrAbsolute);
                bitImgMinimizeWindowButton.EndInit();

                Image imgMinimizeWindowButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgMinimizeWindowButton
                };

                btnMinimize.Content = imgMinimizeWindowButton;
                btnMinimize.Background = new ImageBrush(bitImgMinimizeWindowButton);
            }
           
            if(serviceNowTheme.WindowContentButtonImagesInverted == true)
            {
                //Copy Button
                BitmapImage bitImgCopyButton = new BitmapImage();
                bitImgCopyButton.BeginInit();
                bitImgCopyButton.UriSource = new Uri(@"\Images\copy-black.png", UriKind.RelativeOrAbsolute);
                bitImgCopyButton.EndInit();

                Image imgCopyButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCopyButton
                };

                btnCopy.Content = imgCopyButton;
                btnCopy.Background = new ImageBrush(bitImgCopyButton);

                //Open in Browser Button
                BitmapImage bitImgOpenInBrowserButton = new BitmapImage();
                bitImgOpenInBrowserButton.BeginInit();
                bitImgOpenInBrowserButton.UriSource = new Uri(@"\Images\openinbrowser-black.png", UriKind.RelativeOrAbsolute);
                bitImgOpenInBrowserButton.EndInit();

                Image imgOpenInBrowserButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgOpenInBrowserButton
                };

                btnOpenInBrowser.Content = imgOpenInBrowserButton;
                btnOpenInBrowser.Background = new ImageBrush(bitImgOpenInBrowserButton);


               

                //CheckMark Button
                BitmapImage bitImgCheckMarkButton = new BitmapImage();
                bitImgCheckMarkButton.BeginInit();
                bitImgCheckMarkButton.UriSource = new Uri(@"\Images\checkmark-black.png", UriKind.RelativeOrAbsolute);
                bitImgCheckMarkButton.EndInit();

                Image imgCheckMarkButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCheckMarkButton
                };

                btnOK.Content = imgCheckMarkButton;
                btnOK.Background = new ImageBrush(bitImgCheckMarkButton);
            }

        }

        private void SetWhiteButtonImages()
        {

            if(serviceNowTheme.MenuPanelImagesInverted == false)
            {
                //Home menu
                BitmapImage bitImgHomeButton = new BitmapImage();
                bitImgHomeButton.BeginInit();
                bitImgHomeButton.UriSource = new Uri(@"\Images\home-white.png", UriKind.RelativeOrAbsolute);
                bitImgHomeButton.EndInit();

                Image imgImgHomeButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgHomeButton
                };

                btnHome.Content = imgImgHomeButton;
                btnHome.Background = new ImageBrush(bitImgHomeButton);

                //Recently Opened Items menu
                BitmapImage bitImgRecentlyOpenedItemsButton = new BitmapImage();
                bitImgRecentlyOpenedItemsButton.BeginInit();
                bitImgRecentlyOpenedItemsButton.UriSource = new Uri(@"\Images\recentitems-white.png", UriKind.RelativeOrAbsolute);
                bitImgRecentlyOpenedItemsButton.EndInit();

                Image imgRecentlyOpenedItemsButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgRecentlyOpenedItemsButton
                };

                btnHistory.Content = imgRecentlyOpenedItemsButton;
                btnHistory.Background = new ImageBrush(bitImgRecentlyOpenedItemsButton);

                //Theme Button
                BitmapImage bitImgThemeButton = new BitmapImage();
                bitImgThemeButton.BeginInit();
                bitImgThemeButton.UriSource = new Uri(@"\Images\palette-white.png", UriKind.RelativeOrAbsolute);
                bitImgThemeButton.EndInit();

                Image imgThemeButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgThemeButton
                };

                btnThemes.Content = imgThemeButton;
                btnThemes.Background = new ImageBrush(bitImgThemeButton);

                //Settings menu
                BitmapImage bitImgSettingsButton = new BitmapImage();
                bitImgSettingsButton.BeginInit();
                bitImgSettingsButton.UriSource = new Uri(@"\Images\settings-white.png", UriKind.RelativeOrAbsolute);
                bitImgSettingsButton.EndInit();

                Image imgSettingsButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgSettingsButton
                };

                btnSettings.Content = imgSettingsButton;
                btnSettings.Background = new ImageBrush(bitImgSettingsButton);

            }
           
            if(serviceNowTheme.TitleBarButtonImagesInverted == false)
            {
                //Close Window Button
                BitmapImage bitImgCloseWindowButton = new BitmapImage();
                bitImgCloseWindowButton.BeginInit();
                bitImgCloseWindowButton.UriSource = new Uri(@"\Images\power-white.png", UriKind.RelativeOrAbsolute);
                bitImgCloseWindowButton.EndInit();

                Image imgCloseWindowButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCloseWindowButton
                };

                btnClose.Content = imgCloseWindowButton;
                btnClose.Background = new ImageBrush(bitImgCloseWindowButton);

                //Minimize Window Button
                BitmapImage bitImgMinimizeWindowButton = new BitmapImage();
                bitImgMinimizeWindowButton.BeginInit();
                bitImgMinimizeWindowButton.UriSource = new Uri(@"\Images\minimize-white.png", UriKind.RelativeOrAbsolute);
                bitImgMinimizeWindowButton.EndInit();

                Image imgMinimizeWindowButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgMinimizeWindowButton
                };

                btnMinimize.Content = imgMinimizeWindowButton;
                btnMinimize.Background = new ImageBrush(bitImgMinimizeWindowButton);
            }
           
            if(serviceNowTheme.WindowContentButtonImagesInverted == false)
            {
                //Copy Button
                BitmapImage bitImgCopyButton = new BitmapImage();
                bitImgCopyButton.BeginInit();
                bitImgCopyButton.UriSource = new Uri(@"\Images\copy-white.png", UriKind.RelativeOrAbsolute);
                bitImgCopyButton.EndInit();

                Image imgCopyButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCopyButton
                };

                btnCopy.Content = imgCopyButton;
                btnCopy.Background = new ImageBrush(bitImgCopyButton);

                //Open in Browser Button
                BitmapImage bitImgOpenInBrowserButton = new BitmapImage();
                bitImgOpenInBrowserButton.BeginInit();
                bitImgOpenInBrowserButton.UriSource = new Uri(@"\Images\openinbrowser-white.png", UriKind.RelativeOrAbsolute);
                bitImgOpenInBrowserButton.EndInit();

                Image imgOpenInBrowserButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgOpenInBrowserButton
                };

                btnOpenInBrowser.Content = imgOpenInBrowserButton;
                btnOpenInBrowser.Background = new ImageBrush(bitImgOpenInBrowserButton);

                //CheckMark Button
                BitmapImage bitImgCheckMarkButton = new BitmapImage();
                bitImgCheckMarkButton.BeginInit();
                bitImgCheckMarkButton.UriSource = new Uri(@"\Images\checkmark-white.png", UriKind.RelativeOrAbsolute);
                bitImgCheckMarkButton.EndInit();

                Image imgCheckMarkButton = new Image
                {
                    Stretch = Stretch.Fill,
                    Source = bitImgCheckMarkButton
                };

                btnOK.Content = imgCheckMarkButton;
                btnOK.Background = new ImageBrush(bitImgCheckMarkButton);
            }

        }

        private void SetBlackText()
        {
            if(serviceNowTheme.TitleBarTextColorInverted == true)
            {
                txtTitle.Foreground = serviceNowTheme.BlackBrush();
            }

            //if(serviceNowTheme.MenuPanelImagesInverted == true)
            //{
            //    btnHome.Foreground = serviceNowTheme.BlackBrush();
            //    btnHistory.Foreground = serviceNowTheme.BlackBrush();
            //    btnSettings.Foreground = serviceNowTheme.BlackBrush();
            //}

            if(serviceNowTheme.WindowContentTextColorInverted == true)
            {
                txt_Theme.Foreground = serviceNowTheme.BlackBrush();
                chkTitleBarCheckBox.Foreground = serviceNowTheme.BlackBrush();
                chkMenuCheckBox.Foreground = serviceNowTheme.BlackBrush();
                chkWindowContentCheckBox.Foreground = serviceNowTheme.BlackBrush();
                txtRed.Foreground = serviceNowTheme.BlackBrush();
                txtGreen.Foreground = serviceNowTheme.BlackBrush();
                txtBlue.Foreground = serviceNowTheme.BlackBrush();
                txtOpacity.Foreground = serviceNowTheme.BlackBrush();
                ChkBox_ButtonColors.Foreground = serviceNowTheme.BlackBrush();
                ChkBox_InvertTextColors.Foreground = serviceNowTheme.BlackBrush();
                sliderRed.Foreground = serviceNowTheme.BlackBrush();
                sliderGreen.Foreground = serviceNowTheme.BlackBrush();
                sliderBlue.Foreground = serviceNowTheme.BlackBrush();
                borderSliders.BorderBrush = serviceNowTheme.BlackBrush();
                btnResetToDefault.Foreground = serviceNowTheme.BlackBrush();
                txtSettings.Foreground = serviceNowTheme.BlackBrush();
                txtVersion.Foreground = serviceNowTheme.BlackBrush();
                txtRecentlyOpenedItems.Foreground = serviceNowTheme.BlackBrush();
                txtOpen.Foreground = serviceNowTheme.BlackBrush();
                chkBoxFreeTextSearch.Foreground = serviceNowTheme.BlackBrush();
                chkMinimizeToSystemTray.Foreground = serviceNowTheme.BlackBrush();
                chkBoxAlwaysOnTop.Foreground = serviceNowTheme.BlackBrush();
            }


        }

        private void SetWhiteText()
        {
            if(serviceNowTheme.TitleBarTextColorInverted == false)
            {
                txtTitle.Foreground = serviceNowTheme.WhiteBrush();
            }

            //if(serviceNowTheme.MenuPanelImagesInverted == false)
            //{
            //    btnHome.Foreground = serviceNowTheme.WhiteBrush();
            //    btnHistory.Foreground = serviceNowTheme.WhiteBrush();
            //    btnSettings.Foreground = serviceNowTheme.WhiteBrush();
            //}

            if(serviceNowTheme.WindowContentTextColorInverted == false)
            {
                txt_Theme.Foreground = serviceNowTheme.WhiteBrush();
                chkTitleBarCheckBox.Foreground = serviceNowTheme.WhiteBrush();
                chkMenuCheckBox.Foreground = serviceNowTheme.WhiteBrush();
                chkWindowContentCheckBox.Foreground = serviceNowTheme.WhiteBrush();
                txtRed.Foreground = serviceNowTheme.WhiteBrush();
                txtGreen.Foreground = serviceNowTheme.WhiteBrush();
                txtBlue.Foreground = serviceNowTheme.WhiteBrush();
                txtOpacity.Foreground = serviceNowTheme.WhiteBrush();
                ChkBox_ButtonColors.Foreground = serviceNowTheme.WhiteBrush();
                ChkBox_InvertTextColors.Foreground = serviceNowTheme.WhiteBrush();
                sliderRed.Foreground = serviceNowTheme.WhiteBrush();
                sliderGreen.Foreground = serviceNowTheme.WhiteBrush();
                sliderBlue.Foreground = serviceNowTheme.WhiteBrush();
                borderSliders.BorderBrush = serviceNowTheme.WhiteBrush();
                btnResetToDefault.Foreground = serviceNowTheme.WhiteBrush();
                txtSettings.Foreground = serviceNowTheme.WhiteBrush();
                txtVersion.Foreground = serviceNowTheme.WhiteBrush();
                txtRecentlyOpenedItems.Foreground = serviceNowTheme.WhiteBrush();
                txtOpen.Foreground = serviceNowTheme.WhiteBrush();
                chkBoxFreeTextSearch.Foreground = serviceNowTheme.WhiteBrush();
                chkMinimizeToSystemTray.Foreground = serviceNowTheme.WhiteBrush();
                chkBoxAlwaysOnTop.Foreground = serviceNowTheme.WhiteBrush();
            }
        }

        private void ThemesButton_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Visible;
        }

       

        private void MenuPanelCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           

        }

        private void WindowContentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
          

        }

        private void TitleBarCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

       
        private void ChangeButtonColor()
        {

            byte[] colors = new byte[3];
            colors[0] = (byte)sliderRed.Value;
            colors[1] = (byte)sliderGreen.Value;
            colors[2] = (byte)sliderBlue.Value;
            serviceNowTheme.MenuPanelButtonsRGB = colors;

            if(ChkBox_ButtonColors.IsChecked == true && chkMenuCheckBox.IsChecked ==true)
            {
           
                //Home Button
                Image imgHome = new Image();
                imgHome.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/home-white.png");
                ImageBrush backgroundHomeImage = new ImageBrush();
                backgroundHomeImage.ImageSource = imgHome.Source;
                backgroundHomeImage.Stretch = Stretch.Fill;
                backgroundHomeImage.TileMode = TileMode.None;
                btnHome.Background = backgroundHomeImage;

                //Themes Button
                Image imgTheme = new Image();
                imgTheme.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/paint-brush-64.png");
                ImageBrush backgroundThemeImage = new ImageBrush();
                backgroundThemeImage.ImageSource = imgTheme.Source;
                backgroundThemeImage.Stretch = Stretch.Fill;
                backgroundThemeImage.TileMode = TileMode.None;
                btnThemes.Background = backgroundThemeImage;

                //Recently Opened Items Button
                Image imgRecentItems = new Image();
                imgRecentItems.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/recentitems-white.png");
                ImageBrush backgroundRecentItemsImage = new ImageBrush();
                backgroundRecentItemsImage.ImageSource = imgRecentItems.Source;
                backgroundRecentItemsImage.Stretch = Stretch.Fill;
                backgroundRecentItemsImage.TileMode = TileMode.None;
                btnHistory.Background = backgroundRecentItemsImage;

              

                //Settings Button
                Image imgSettingsButton = new Image();
                imgSettingsButton.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/settings-white.png");
                ImageBrush backgroundSettingsButton = new ImageBrush();
                backgroundSettingsButton.ImageSource = imgSettingsButton.Source;
                backgroundSettingsButton.Stretch = Stretch.Fill;
                backgroundSettingsButton.TileMode = TileMode.None;
                btnSettings.Background = backgroundSettingsButton;
            }

            if(chkTitleBarCheckBox.IsChecked == true && ChkBox_ButtonColors.IsChecked == true)
            {
                //Power Button
                Image imgPowerButton = new Image();
                imgPowerButton.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/close.png");
                ImageBrush backgroundPowerButton = new ImageBrush();
                backgroundPowerButton.ImageSource = imgPowerButton.Source;
                backgroundPowerButton.Stretch = Stretch.Fill;
                backgroundPowerButton.TileMode = TileMode.None;
                btnClose.Background = backgroundPowerButton;

                //Minimize Button
                Image imgMinimizeButton = new Image();
                imgMinimizeButton.Source = serviceNowTheme.ConvertImageColor(colors[0], colors[1], colors[2], 255, @"/Images/minimize-white.png");
                ImageBrush backgroundMinimizeButton = new ImageBrush();
                backgroundMinimizeButton.ImageSource = imgMinimizeButton.Source;
                backgroundMinimizeButton.Stretch = Stretch.Fill;
                backgroundMinimizeButton.TileMode = TileMode.None;
                btnMinimize.Background = backgroundMinimizeButton;

            }


        }

        public WriteableBitmap ConvertImageColors(byte red, byte green, byte blue, byte alpha, string imagename)
        {

            StreamResourceInfo x = Application.GetResourceStream(new Uri(BaseUriHelper.GetBaseUri(this), imagename));
          
                BitmapDecoder dec = BitmapDecoder.Create(x.Stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
                BitmapFrame image = dec.Frames[0];
                byte[] pixels = new byte[image.PixelWidth * image.PixelHeight * 4];
                image.CopyPixels(pixels, image.PixelWidth * 4, 0);

                var bmp = new WriteableBitmap(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, PixelFormats.Pbgra32, null);

                for(int i = 0; i < pixels.Length / 4; ++i)
                {
                    byte b = pixels[i * 4];
                    byte g = pixels[i * 4 + 1];
                    byte r = pixels[i * 4 + 2];
                    byte a = pixels[i * 4 + 3];


                    if((r == 255 &&
                        g == 255 &&
                        b == 255 &&
                        a == 255) ||
                    (a != 0 && a != 255 &&
                        r == g && g == b && r != 0))
                    {

                        {

                            r = red;
                            g = green;
                            b = blue;
                            a = alpha;

                            pixels[i * 4] = b;
                            pixels[i * 4 + 1] = g;
                            pixels[i * 4 + 2] = r;
                            pixels[i * 4 + 3] = a;

                        }

                    }

                    bmp.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), pixels, image.PixelWidth * 4, 0);

                    // Source: https://stackoverflow.com/questions/20856424/wpf-modifying-image-colors-on-the-fly-c
                }
                
                return bmp;
           
          

        }

        private void InvertButtonColorsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if(chkTitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarButtonImagesInverted = false;
            }

            if(chkMenuCheckBox.IsChecked == true)
            {
                serviceNowTheme.MenuPanelImagesInverted = false;
            }

            if(chkWindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.WindowContentButtonImagesInverted = false;
            }

           // SetWhiteButtonImages();
            
        }

        private void InvertTextColorsCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            if(chkTitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarTextColorInverted = true;
            }


            if(chkWindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.WindowContentTextColorInverted = true;
            }

            SetBlackText();
            //SetWhiteText();
        }

        private void InvertTextColorsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if(chkTitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarTextColorInverted = false;
            }


            if(chkWindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.WindowContentTextColorInverted = false;
            }

            SetWhiteText();
        }

        private void TitleBarCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
           
        }

        private void MenuPanelCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }

        private void WindowContentCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
           
        }

        private void OpenInBrowserButton_Click(object sender, RoutedEventArgs e)
        {
            if(listViewRecentlyOpenedItems.Items.Count > 0 && listViewRecentlyOpenedItems.SelectedIndex >= 0)
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

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string cleanedItem = txtItem.Text.Trim();
            OpenItemInServiceNow(cleanedItem);
        }

        private void ChkBox_ButtonColors_Checked(object sender, RoutedEventArgs e)
        {
            if(chkTitleBarCheckBox.IsChecked == true)
            {
                serviceNowTheme.TitleBarButtonImagesInverted = true;
            }

            if(chkMenuCheckBox.IsChecked == true)
            {
                serviceNowTheme.MenuPanelImagesInverted = true;
            }

            if(chkWindowContentCheckBox.IsChecked == true)
            {
                serviceNowTheme.WindowContentButtonImagesInverted = true;
            }

            // SetBlackButtonImages();
        }




        private void ChkBox_ButtonColors_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}


