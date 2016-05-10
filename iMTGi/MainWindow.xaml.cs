using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xceed.Wpf.Toolkit;

namespace iMTGi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MtgParse iMtg = new MtgParse();
        public ListView masterList = new ListView();
        string colorSort = "";
        List<string> typeSort = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        /*public MainWindow(MtgParse parsedMtg)
        {
            iMtg = parsedMtg;
            InitializeComponent();
        }*/

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var gridView = new GridView();
            lv.View = gridView;
            // Add a column with width 20 and left alignment.
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "ID",
                Width = 25,
                DisplayMemberBinding = new Binding("Id")
            });
            gridView.Columns.Add(new GridViewColumn{
                Header = "Name",
                Width = 200,
                DisplayMemberBinding = new Binding("Name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Type",
                Width = 150,
                DisplayMemberBinding = new Binding("Type")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Cost",
                Width = 75,
                DisplayMemberBinding = new Binding("Cost")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Text",
                Width = 315,
                DisplayMemberBinding = new Binding("ChunkText")
            });
            int i = 0;
            List<Card> items = new List<Card>();
            foreach (Card card in iMtg.cards)
            {
                string newText = "";
                foreach (string str in card.Text)
                { newText += str; }
                items.Add(new Card() { Id = i, Name = card.Name, Type = card.Type, Cost = card.Cost, ChunkText = newText, Block = card.Block, Text = card.Text,Color = card.Color });
                //this.lv.Items.Add(new Card {Id = i,Name = card.Name, Type = card.Type, Cost = card.Cost, ChunkText = newText, Block = card.Block, Text = card.Text });
                i++;
            }
            this.lv.ItemsSource = items;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.lv.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            masterList = this.lv;
            //this.lv.coll
        }

        private void lv_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lv.SelectedItems.Count == 0)
                return;
            var item = lv.SelectedItems[0] as Card;
            if (item == null)
            {
                return;
            }
            textBox.Clear();
            textBox.Text += item.name + "\n";
            textBox.Text += item.Cost + "\n";
            textBox.Text += item.Type + "\n";
            foreach(var c in item.Text)
            { textBox.Text += c + "\n"; }
            //textBox.Text += item.Text
           // int index = item.ChunkText.IndexOf(item.Block);
            //string cleanPath = (index < 0)
            //    ? item.ChunkText
             //   : item.ChunkText.Remove(index, item.Block.Length);
           // textBox.Text += cleanPath + "\n";
            return;
        }

        private void ColorSort_Checked(object sender, RoutedEventArgs e)
        {
            //colorSort = "w";
        }

        private void ColorAllSort_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(this.lv.ItemsSource);
            view.Filter = filterHolder;
            CollectionViewSource.GetDefaultView(lv.ItemsSource).Refresh();
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            //this.lv.Items.fi
        }
        public bool filterHolder(object item)
        {
            if (colorSort == "" && typeSort.Count == 0)
            {
                return true;
            }
            else if (colorSort == "")
            {
                return filterType(item);
            }
            else if (typeSort.Count == 0)
            {
                return filterColor(item);
            }
            else
            { return filterColor(item) && filterType(item); }
            
        }
        public bool filterType(object item)
        {
            var card = item as Card;
            bool check = false;
            List<string> newType = card.Type.Split(' ').ToList();
            foreach (var row in newType)
            {
                if (typeSort.Contains(row))
                    check = true;
            }
            return check;
        }
        public bool filterColor(object item)
        {
            var card = item as Card;
            bool check = true;
            if (String.IsNullOrEmpty(colorSort))
                return true;
            else
            {
                foreach (var c in colorSort)
                {

                    if (check)
                    {
                        string checkChar = "" + c;
                        check = (card.Color.IndexOf(checkChar, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                    if (!check)
                    {
                        return false;
                    }
                }
            }
            return check;
           // return (card.Color.IndexOf(colorSort, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void ColorSort_CheckedW(object sender, RoutedEventArgs e)
        {
            colorSort += "W";
        }

        private void ColorSort_CheckedU(object sender, RoutedEventArgs e)
        {
            colorSort += "U";
        }

        private void ColorSort_CheckedB(object sender, RoutedEventArgs e)
        {
            colorSort += "B";
        }

        private void ColorSort_CheckedR(object sender, RoutedEventArgs e)
        {
            colorSort += "R";
        }

        private void ColorSort_CheckedG(object sender, RoutedEventArgs e)
        {
            colorSort += "G";
        }

        private void ColorSort_CheckedL(object sender, RoutedEventArgs e)
        {
            colorSort += "L";
        }

        private void ColorSort_UnCheckedL(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("L", string.Empty);
        }

        private void ColorSort_UnCheckedG(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("G", string.Empty);
        }

        private void ColorSort_UnCheckedR(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("R", string.Empty);
        }

        private void ColorSort_UnCheckedB(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("B", string.Empty);
        }

        private void ColorSort_UnCheckedU(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("U", string.Empty);
        }

        private void ColorSort_UnCheckedW(object sender, RoutedEventArgs e)
        {
            colorSort = colorSort.Replace("W", string.Empty);
        }

        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a TreeViewItem.
            TreeViewItem item = new TreeViewItem();
            item.Header = "Color";
            item.IsExpanded = true;
            CheckBox checkBoxB = new CheckBox();
            checkBoxB.Checked += new RoutedEventHandler(ColorSort_CheckedB);
            checkBoxB.Unchecked += new RoutedEventHandler(ColorSort_UnCheckedB);
            checkBoxB.Content = "Black";
            CheckBox checkBoxW = new CheckBox();
            checkBoxW.Checked += new RoutedEventHandler(ColorSort_CheckedW);
            checkBoxW.Unchecked += new RoutedEventHandler(ColorSort_UnCheckedW);
            checkBoxW.Content = "White";
            CheckBox checkBoxR = new CheckBox();
            checkBoxR.Checked += new RoutedEventHandler(ColorSort_CheckedR);
            checkBoxR.Unchecked += new RoutedEventHandler(ColorSort_UnCheckedR);
            checkBoxR.Content = "Red";
            CheckBox checkBoxU = new CheckBox();
            checkBoxU.Checked += new RoutedEventHandler(ColorSort_CheckedU);
            checkBoxU.Unchecked += new RoutedEventHandler(ColorSort_UnCheckedU);
            checkBoxU.Content = "Blue";
            CheckBox checkBoxG = new CheckBox();
            checkBoxG.Checked += new RoutedEventHandler(ColorSort_CheckedG);
            checkBoxG.Unchecked += new RoutedEventHandler(ColorSort_UnCheckedG);
            checkBoxG.Content = "Green";
            item.ItemsSource = new CheckBox[] { checkBoxB, checkBoxW, checkBoxR, checkBoxU, checkBoxG};

            // ... Create a second TreeViewItem.
            TreeViewItem item2 = new TreeViewItem();
            item2.Header = "Type";
            item2.IsExpanded = true;
            CheckBox checkBoxCreature = new CheckBox();
            checkBoxCreature.Checked += new RoutedEventHandler(typeChecked);
            checkBoxCreature.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxCreature.Content = "Creature";
            CheckBox checkBoxEnchantment = new CheckBox();
            checkBoxEnchantment.Checked += new RoutedEventHandler(typeChecked);
            checkBoxEnchantment.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxEnchantment.Content = "Enchantment";
            CheckBox checkBoxSorcery = new CheckBox();
            checkBoxSorcery.Checked += new RoutedEventHandler(typeChecked);
            checkBoxSorcery.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxSorcery.Content = "Sorcery";
            CheckBox checkBoxInstant = new CheckBox();
            checkBoxInstant.Checked += new RoutedEventHandler(typeChecked);
            checkBoxInstant.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxInstant.Content = "Instant";
            CheckBox checkBoxVanguard = new CheckBox();
            checkBoxVanguard.Checked += new RoutedEventHandler(typeChecked);
            checkBoxVanguard.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxVanguard.Content = "Vanguard";
            CheckBox checkBoxPlainswalker = new CheckBox();
            checkBoxPlainswalker.Checked += new RoutedEventHandler(typeChecked);
            checkBoxPlainswalker.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxPlainswalker.Content = "Planeswalker";
            CheckBox checkBoxArtifact = new CheckBox();
            checkBoxArtifact.Checked += new RoutedEventHandler(typeChecked);
            checkBoxArtifact.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxArtifact.Content = "Artifact";
            CheckBox checkBoxLand = new CheckBox();
            checkBoxLand.Checked += new RoutedEventHandler(typeChecked);
            checkBoxLand.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxLand.Content = "Land";
            CheckBox checkBoxPlanes = new CheckBox();
            checkBoxPlanes.Checked += new RoutedEventHandler(typeChecked);
            checkBoxPlanes.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxPlanes.Content = "Plane";
            CheckBox checkBoxPhenomena = new CheckBox();
            checkBoxPhenomena.Checked += new RoutedEventHandler(typeChecked);
            checkBoxPhenomena.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxPhenomena.Content = "Phenomenon";
            CheckBox checkBoxScheme = new CheckBox();
            checkBoxScheme.Checked += new RoutedEventHandler(typeChecked);
            checkBoxScheme.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxScheme.Content = "Scheme";
            CheckBox checkBoxConspiracy = new CheckBox();
            checkBoxConspiracy.Checked += new RoutedEventHandler(typeChecked);
            checkBoxConspiracy.Unchecked += new RoutedEventHandler(typeUnChecked);
            checkBoxConspiracy.Content = "Conspiracy";
            item2.ItemsSource = new CheckBox[] { checkBoxCreature, checkBoxEnchantment, checkBoxSorcery, checkBoxInstant, checkBoxVanguard, checkBoxPlainswalker, checkBoxArtifact
            , checkBoxLand, checkBoxPlanes, checkBoxPhenomena, checkBoxScheme, checkBoxConspiracy};

            // ... Get TreeView reference and add both items.
            var tree = sender as TreeView;
            tree.Items.Add(item);
            tree.Items.Add(item2);
        }
        private void typeChecked(object sender, RoutedEventArgs e)
        {
            var item = sender as CheckBox;
            typeSort.Add(item.Content.ToString());
        }
        private void typeUnChecked(object sender, RoutedEventArgs e)
        {
            var item = sender as CheckBox;
            typeSort.RemoveAll(u => u.Contains(item.Content.ToString()));
            //typeSort = typeSort.Replace(item.Content.ToString(), string.Empty);
        }
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            /*var tree = sender as TreeView;

            // ... Determine type of SelectedItem.
            if (tree.SelectedItem is TreeViewItem)
            {
                // ... Handle a TreeViewItem.
                var item = tree.SelectedItem as CheckBox;
                if (item.IsChecked == true)
                {
                    typeSort = typeSort.Replace(item.Content.ToString(), string.Empty);
                    typeSort += item.Content;
                }
                //this.Title = "Selected header: " + item.Content.ToString();
            }
            else if (tree.SelectedItem is string)
            {
                // ... Handle a string.
                this.Title = "Selected: " + tree.SelectedItem.ToString();
            }*/
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
