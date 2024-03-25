using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using LibraryManager.BLL;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static System.Reflection.Metadata.BlobBuilder;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DVDListPage : Page
    {
        public List<DVD> DVDs;
        public List<DVD> DVDs2;
        public Member selectedDVD { get; set; }
        public DVDListPage()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                string memberID = e.Parameter.ToString();

                selectedDVD = MemberStore.Instance.GetMembersByID(memberID);

                DVDs = DVDStore.Instance.DVDs;

            }
            base.OnNavigatedTo(e);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DVD selectedDVD = DVDs[DVDListView.SelectedIndex];
            DisplayLoanDVDDialog(selectedDVD);
        }
        private async void DisplayLoanDVDDialog(DVD DVDs)
        {
            ContentDialog loandvdDialog = new ContentDialog
            {
                Title = $"Loan DVD",
                Content = $"Wolud you like to loan the {DVDs.title} ?",
                PrimaryButtonText = "OK",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await loandvdDialog.ShowAsync();

            // Loan the DVD if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                // TODO: Loan the DVD
                LoanStore.Instance.CreateNewLoan(selectedDVD, DVDs);
                this.Frame.Navigate(typeof(MemberLoanPage), selectedDVD.id);
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MemberLoanPage), selectedDVD.id);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchTitle = DVD_name_in.Text.ToLower();
            var searchBarcode = string.IsNullOrWhiteSpace(DVD_Barcode.Text) ? 0 : int.Parse(DVD_Barcode.Text);
            var searchBirthday = DVD_birth.Text;
            var searchNationality = DVD_nation.Text.ToLower();

            DVDs2 = DVDs.Where(dvd =>
                (string.IsNullOrWhiteSpace(searchTitle) || dvd.title.ToLower().Contains(searchTitle)) &&
                (searchBarcode == 0 || dvd.barcode == searchBarcode) &&
                (string.IsNullOrWhiteSpace(searchBirthday) || dvd.Authorybirthday == searchBirthday) && 
                (string.IsNullOrWhiteSpace(searchNationality) || dvd.Authornationality.ToLower().Contains(searchNationality))
            ).ToList();

            DVDListView.ItemsSource = DVDs2; // Assuming DVDListView is the name of your ListView control
        }

    }
}