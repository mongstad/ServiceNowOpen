using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using ServiceNow;
using System.Threading;
using System.Xml;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Reflection;
using System.Drawing;
using Color = System.Windows.Media.Color;

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

            //TODO Fix backgroundcolor on buttons and images when changing sliders
            //TODO Add option to change text color
            //TODO Add option to switch between white or black button icons (images)
            SetRGBSliderValues();
            SetWindowColors();
            SetNotifyIconSettings();
            SetVersionInfo();

        }

        private void SetRGBSliderValues()
        {
            if(radioTitleButton.IsChecked==true)
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

            if(radioMenuButton.IsChecked == true)
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

            if(radioCenterContentButton.IsChecked == true)
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

        private void LoadSettings()
        {
            Settings snSettings = new Settings();
            try
            {
                snSettings = snSettings.Load();
                this.Opacity = snSettings.Opacity;
                this.Top = snSettings.Top;
                this.Left = snSettings.Left;
                sliderOpacityValue.Value = snSettings.SliderPosition;
                chkBoxAlwaysOnTop.IsChecked = snSettings.TopMost;
                chkBoxFreeTextSearch.IsChecked = snSettings.FreeTextSearch;
                chkMinimizeToSystemTray.IsChecked = snSettings.MinimizeToTray;
                serviceNowTheme.CenterWindowBackground = serviceNowTheme.ConvertStringToBrush(snSettings.CenterWindowBrushString);
                serviceNowTheme.MenuBackground = serviceNowTheme.ConvertStringToBrush(snSettings.MenuBrushString);
                serviceNowTheme.TitleBarBackground = serviceNowTheme.ConvertStringToBrush(snSettings.TitleBarBrushString);

               foreach(RecentlyOpenedItem item in snSettings.RecentItems.RecentItems)
                {
                    recentlyOpenedItems.RecentItems.Add(item);  
                }

            }
            catch (Exception ex)
            {
                
            }
         
        }

        private void SetNotifyIconSettings()
        {

            notifyIcon.Text = "ServiceNow Open";
            notifyIcon.Icon = new System.Drawing.Icon("now-agent-icon.ico");
            notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown) ;
            
            System.Windows.Forms.MenuItem menuItemExit = new System.Windows.Forms.MenuItem();
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItemExit });
            menuItemExit.Index = 0;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(menuItemExit_Click);
            notifyIcon.ContextMenu = contextMenu;

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
        private void menuItemExit_Click(object Sender, EventArgs e)
        {
            
            CloseApplication();
        }

        private void WindowGotMiniMizedActions()
        {


            if (chkMinimizeToSystemTray.IsChecked == false)
            {
                this.WindowState = WindowState.Minimized;
                notifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
            else
            {
                notifyIcon.Visible = true;
                this.Visibility = Visibility.Hidden;
                this.ShowInTaskbar= false;

            }
        }


        private void gridTitleGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtItem_TextChanged(object sender, TextChangedEventArgs e)
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
            
            btnOK.IsEnabled= false;
            btnOK.Opacity = 0.2;
           
        }

        private void txtItem_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                Storyboard sb = new Storyboard();
                string cleanedItem = txtItem.Text.Trim();
                OpenItemInServiceNow(cleanedItem);
                sb = this.FindResource("OkClickedAnimation") as Storyboard;
                Storyboard.SetTarget(sb,this.imgOkBtn);
                sb.Begin(); 
            }

            if(e.Key == Key.Escape)
            {
                txtItem.Clear();
            }

        }

        private void imgOkBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            string cleanedItem = txtItem.Text.Trim();
            OpenItemInServiceNow(cleanedItem);

        }

        private void OpenItemInServiceNow(string item)
        {

            txtItem.Clear();
            
            RemoveOldEntries();
            ServiceNowItem newSNItem = new ServiceNowItem(item);

            if (chkBoxFreeTextSearch.IsChecked == true)
            {
                newSNItem.ForceFreeTextSearch = true;
            }
            else { newSNItem.ForceFreeTextSearch = false; }

         
            newSNItem.ExecuteProcess();
            RecentlyOpenedItem newRecentItem = new RecentlyOpenedItem(newSNItem.Item, newSNItem.Url, DateTime.Now);
            recentlyOpenedItems.RecentItems.Insert(0,newRecentItem);
            txtItem.Focus();

        }

        private void RemoveOldEntries()
        {
            int recenItemsCount = recentlyOpenedItems.RecentItems.Count;
            if(recenItemsCount >=100)
            {
                recentlyOpenedItems.RecentItems.RemoveAt(recenItemsCount-1);
            }
        }

        private void SetVersionInfo()
        {

            Assembly thisAssem = typeof(MainWindow).Assembly;
            AssemblyName thisAssemName = thisAssem.GetName();

            Version ver = thisAssemName.Version;
            txtVersion.Text += ver.ToString(); 
        }

        private void chkBoxAlwaysOnTop_Checked(object sender, RoutedEventArgs e)
        {
           this.Topmost=true;   
        }

        private void chkBoxAlwaysOnTop_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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
            Settings settingsServiceNow = new Settings();
            settingsServiceNow.Opacity = this.Opacity;
            settingsServiceNow.Left = this.Left;
            settingsServiceNow.Top = this.Top;
            settingsServiceNow.TopMost = (bool)chkBoxAlwaysOnTop.IsChecked;
            settingsServiceNow.FreeTextSearch = (bool)chkBoxFreeTextSearch.IsChecked;
            settingsServiceNow.SliderPosition = sliderOpacityValue.Value;
            settingsServiceNow.RecentItems = recentlyOpenedItems;
            settingsServiceNow.MinimizeToTray = (bool)chkMinimizeToSystemTray.IsChecked;
            BrushConverter brushConverter = new BrushConverter();
            settingsServiceNow.TitleBarBrushString = brushConverter.ConvertToString(serviceNowTheme.TitleBarBackground);
            settingsServiceNow.MenuBrushString= brushConverter.ConvertToString(serviceNowTheme.MenuBackground);
            settingsServiceNow.CenterWindowBrushString = brushConverter.ConvertToString(serviceNowTheme.CenterWindowBackground);
            settingsServiceNow.Save();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Visible;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
            FocusSearchBox();

        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Visible;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Hidden;
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
         
            WindowGotMiniMizedActions();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Visible;
            gridColorPalette.Visibility = Visibility.Hidden;
        }

        private void sliderOpacityValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {


            double opacityValue = GetOpacityValue();
            this.Opacity = GetOpacityValue();
         
        }
        private double GetOpacityValue()
        {

            double sliderValue = sliderOpacityValue.Value;

            if (sliderValue > 95)
            {
                return 1.0;
            }
            if (sliderValue > 90)
            {
                return 0.95;
            }

            if (sliderValue > 80)
            {
                return 0.90;
            }

            if (sliderValue > 70)
            {
                return 0.85;
            }

            if (sliderValue > 60)
            {
                return 0.80;
            }
            if (sliderValue > 60)
            {
                return 0.75;
            }
            if (sliderValue > 50)
            {
                return 0.70;
            }
            if (sliderValue > 45)
            {
                return 0.65;
            }
            if (sliderValue > 30)
            {
                return 0.60;
            }
            if (sliderValue > 25)
            {
                return 0.55;
            }

            if (sliderValue > 20)
            {
                return 0.50;
            }
            if (sliderValue >15)
            {
                return 0.45;
            }
            if (sliderValue > 10)
            {
                return 0.40;
            }
            if (sliderValue > 5)
            {
                return 0.35;
            }
            if (sliderValue == 0)
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

        private void imgCopy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (listViewRecentlyOpenedItems.Items.Count > 0 && listViewRecentlyOpenedItems.SelectedIndex >=0)
            {
                SetSelectedItemToClipBoard();
            }
           
        }
        private void SetSelectedItemToClipBoard()
        {
            try
            {
                RecentlyOpenedItem selectedItem = GetSelectedItem();
                Clipboard.SetText(selectedItem.Item);
            }
            catch (Exception ex)
            {

            }
         
        }

        private void ImgOpenInBrowser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(listViewRecentlyOpenedItems.Items.Count > 0 && listViewRecentlyOpenedItems.SelectedIndex >= 0)
            {
                OpenSelectedItemInBrowser();
            }
              
            
        }

        private void OpenSelectedItemInBrowser()
        {
            try
            {
                RecentlyOpenedItem selectedItem = GetSelectedItem();
                OpenItemInServiceNow(selectedItem.Item);
            }
            catch (Exception ex)
            {

            }
            if (listViewRecentlyOpenedItems.Items.Count > 0)
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
            catch (Exception ex)
            {
                return null;
            }
        
        }

        private void ListViewRecentlyOpenedItems_KeyDown(object sender, KeyEventArgs e)
        {
       

            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) // Is Alt key pressed
            {
                if (Keyboard.IsKeyDown(Key.C) )
                {
                    try
                    {
                        SetSelectedItemToClipBoard();
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }


            if( e.Key == Key.Enter  || e.OriginalSource is System.Windows.Controls.TextBlock)
            {
                OpenSelectedItemInBrowser();
            }
            
        }

        private void listViewRecentlyOpenedItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(e.OriginalSource is System.Windows.Controls.TextBlock)
            {
                OpenSelectedItemInBrowser();
            }
         

        }

        private void chkMinimize_Checked(object sender, RoutedEventArgs e)
        {
            ProcessMinimizeToTrayState();
        }

        private void ChkMinimize_Unchecked(object sender, RoutedEventArgs e)
        {
            ProcessMinimizeToTrayState();
        }

       private void ProcessMinimizeToTrayState()
        {
            if(chkMinimizeToSystemTray.IsChecked ==true)
            {
                notifyIcon.Visible = true;
                this.ShowInTaskbar = false;
            }

            if(chkMinimizeToSystemTray.IsChecked ==false)
            {
                notifyIcon.Visible =false;
                this.ShowInTaskbar =true;
            }
        }

        private void RadioTitleButton_Checked(object sender, RoutedEventArgs e)
        {
            SetRGBSliderValues();
        }

        private void RadioLeftMenuButton_Checked(object sender, RoutedEventArgs e)
        {
            SetRGBSliderValues();
        }

        private void RadioCenterContentButton_Checked(object sender, RoutedEventArgs e)
        {
            SetRGBSliderValues();
        }

        private void SliderRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetWindowColorsToSelectSliderColor();
        }

        private void SliderBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();

        }

        private void SliderGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            SetWindowColorsToSelectSliderColor();

        }

        private void SetWindowColorsToSelectSliderColor()
        {
            
            if(radioTitleButton.IsChecked == true)
            {
                serviceNowTheme.TitleBarBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowColors();
            }

            if(radioCenterContentButton.IsChecked == true)
            {
                serviceNowTheme.CenterWindowBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowColors();
            }

            if(radioMenuButton.IsChecked == true)
            {
                serviceNowTheme.MenuBackground = serviceNowTheme.ConvertRGBToBackgroundColor((byte)sliderRed.Value, (byte)sliderGreen.Value, (byte)sliderBlue.Value);
                SetWindowColors();
            }


        }

        private void ImgTheme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            gridMainContent.Visibility = Visibility.Hidden;
            gridHistoryContent.Visibility = Visibility.Hidden;
            gridSettingsContent.Visibility = Visibility.Hidden;
            gridColorPalette.Visibility = Visibility.Visible;
        }

        private void BtnResetToDefault_Click(object sender, RoutedEventArgs e)
        {
            serviceNowTheme.ResetToDefaultTheme();
            SetWindowColors();
            SetRGBSliderValues();


        }

        private void SetWindowColors()
        {
            //set the background colors for the different grid and stackpanels
            gridMasterContentGrid.Background = serviceNowTheme.CenterWindowBackground;
            stackPanelMenu.Background = serviceNowTheme.MenuBackground;
            gridTitleGrid.Background = serviceNowTheme.TitleBarBackground;

            //adjust styles for buttons and images
          



        }

       
    }
}


