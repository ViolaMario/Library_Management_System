using LibraryManager.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookListPage : Page
    {
        public List<Book> books;
        public List<Book> books2;
        //storing the member with whom the loan should be associated
        public Member selectedMember { get; set; }

        public BookListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                string memberID = e.Parameter.ToString();

                selectedMember = MemberStore.Instance.GetMembersByID(memberID);

                books = BookStore.Instance.books;
            }
            base.OnNavigatedTo(e);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selectedBook = books[BooksListView.SelectedIndex];
            DisplayLoanBookDialog(selectedBook);
        }

        //TODO: LoanBook

        private async void DisplayLoanBookDialog(Book book)
        {
            ContentDialog loanBookDialog = new ContentDialog
            {
                Title = $"Loan Book",
                Content = $"Wolud you like to loan the {book.title} book?",
                PrimaryButtonText = "OK",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await loanBookDialog.ShowAsync();

            // Loan the book if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
            {
                LoanStore.Instance.CreateNewLoan(selectedMember, book);
                this.Frame.Navigate(typeof(MemberLoanPage), selectedMember.id);
                // TODO: Loan the book
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MemberLoanPage), selectedMember.id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchTitle = book_name_in.Text.ToLower();
            var searchBarcode = string.IsNullOrWhiteSpace(Barcode_txt.Text) ? 0 : int.Parse(Barcode_txt.Text);
            var searchBirth = book_birth.Text;
            var searchNationality = book_nation.Text.ToLower();

            books2 = BookStore.Instance.books.Where(book =>
                (string.IsNullOrWhiteSpace(searchTitle) || book.title.ToLower().Contains(searchTitle)) &&
                (searchBarcode == 0 || book.barcode == searchBarcode) &&
                (string.IsNullOrWhiteSpace(searchBirth) || book.Authorybirthday.ToLower().Equals(searchBirth.ToLower())) &&
                (string.IsNullOrWhiteSpace(searchNationality) || book.Authornationality.ToLower().Contains(searchNationality))
            ).ToList();

            BooksListView.ItemsSource = books2; // Assuming BooksListView is the name of your ListView control
        }

    }
}
