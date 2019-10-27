using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTheaterManager.collection;
using WpfTheaterManager.common;
using WpfTheaterManager.data;

namespace WpfTheaterManager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Akb48Collection ColViewAkb48;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ColViewAkb48 = new Akb48Collection();

                DgridAkb48.ItemsSource = ColViewAkb48.ColViewListData;
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

        }

        private void OnFilterFileExist(object sender, RoutedEventArgs e)
        {
            Button tbtn = sender as Button;

            if ((string)tbtn.Content == "Exist")
                ColViewAkb48.FilterStatus = "exist";
            else if ((string)tbtn.Content == "Not Exist")
                ColViewAkb48.FilterStatus = "not exist";
            else
                ColViewAkb48.FilterStatus = "";

            ColViewAkb48.Execute();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ColViewAkb48.FilterWord = TxtFilterWord.Text;
            ColViewAkb48.Execute();
        }

        private void DgridAkb48_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Akb48Data data = (Akb48Data)DgridAkb48.SelectedItem;

            if (data == null)
                return;

            TxtbDetailId.Text = Convert.ToString(data.Id);
            TxtbDetailGroupName.Text = data.GroupName;
            TxtDetailTitle.Text = data.Title;
            TxtDetailSubTitle.Text = data.SubTitle;
            TxtbDetailTheDate.Text = data.TheDate.ToString("yyyy/MM/dd HH:mm");
            TxtDetailMember.Text = data.Member.Replace("<br>","\n");
            //TxtDetailMember.NavigateToString(data.Member);
            TxtDetailMemo.Text = data.Memo;
            TxtDetailMemo2.Text = data.Memo2;
            TxtDetailFilename.Text = data.Filename;

            CmbDetailRating1.SelectedValue = data.Rating1;
        }

        private void DgridAkb48_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Akb48Data data = (Akb48Data)DgridAkb48.SelectedItem;
            if (data == null)
                return;

            try
            {
                Player.Execute(data, "GOM");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnDetailUpdate_Click(object sender, RoutedEventArgs e)
        {
            Akb48Data data = (Akb48Data)DgridAkb48.SelectedItem;
            data.Title = TxtDetailTitle.Text.Trim();
            data.SubTitle = TxtDetailSubTitle.Text.Trim();
            data.Member = TxtDetailMember.Text.Replace("\n", "<br>").Trim();
            data.Memo = TxtDetailMemo.Text.Replace("\n", "<br>").Trim();
            data.Memo2 = TxtDetailMemo2.Text.Replace("\n", "<br>").Trim();
            data.TheDate = System.DateTime.ParseExact(TxtbDetailTheDate.Text, "yyyy/MM/dd HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
            data.SubTitle = TxtDetailSubTitle.Text;
            data.Rating1 = (int)CmbDetailRating1.SelectedValue;

            ColViewAkb48.Update(data);
        }
    }
}
