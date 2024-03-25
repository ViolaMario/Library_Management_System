using LibraryManager.BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class MemberSearchPage : Page
    {
        //This will be presented in the list
        readonly ObservableCollection<Member> members = new ObservableCollection<Member>();

        public MemberSearchPage()
        {
            this.InitializeComponent();

            LoadAllMembers();

            //We set the navigation cache mode -> the page will be chached while we are "away" from it.
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        //Loads all members
        
        private void LoadAllMembers()
        {
            members.Clear();
            foreach (var member in MemberStore.Instance.members)
            {
                // 创建新的 Member 对象，确保包含完整的信息，包括 birthday 和 nationality
                Member Member = new Member(member.id, member.name, member.birthday,member.nationality);
                members.Add(Member);
            }
        }

        //TODO: Load searched members

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MembersListView.SelectedIndex < 0) return;

            Member mb = members[MembersListView.SelectedIndex];

            this.Frame.Navigate(typeof(MemberLoanPage), mb.id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            members.Clear();
            foreach (var member in MemberStore.Instance.members)
            {
                if (
                    member.name.ToLower().Contains(txtBoxName.Text.ToLower()) 
                    && member.id.Contains(txtBoxID.Text.ToLower()) 
                    && member.birthday.Contains(txtBoxBirthday.Text)
                    && member.nationality.ToLower().Contains(txtBoxNationality.Text.ToLower())
                    )
                {
                    members.Add(member);
                }
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
