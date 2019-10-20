using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Branzuela_BinarySearchTreeVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields
        private SoundPlayer player = new SoundPlayer();
        private BinaryTree<int> intTree = new BinaryTree<int>();
        private BinaryTree<string> stringTree = new BinaryTree<string>();
        private double BaseLeftMargin = 900;
        private double BaseRightMargin = 900;
        private double BaseTopMargin = 10;
        private double BaseBottomMargin = 440;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            #region color animation

            //ColorAnimation blackToWhite = new ColorAnimation(Colors.SaddleBrown, Colors.LimeGreen, new Duration(new TimeSpan(1)));
            //blackToWhite.AutoReverse = true;
            //blackToWhite.RepeatBehavior = RepeatBehavior.Forever;
            //SolidColorBrush scb = new SolidColorBrush(Colors.Black);
            //scb.BeginAnimation(SolidColorBrush.ColorProperty, blackToWhite);
            //TextEffect tfe = new TextEffect();
            //tfe.Foreground = scb;
            //// Range of text to apply effect to (all).
            //tfe.PositionStart = 0;
            //tfe.PositionCount = int.MaxValue;
            //tbVisualizer.TextEffects.Add(tfe);
            #endregion

        }

        #region background music

        public void Music(string musicType)
        {
            player = new SoundPlayer(musicType);
            player.Play();
        }

        private void TbRelax_Click(object sender, RoutedEventArgs e)
        {
            Music(@"relax.wav");
        }

        private void TbCalm_Click(object sender, RoutedEventArgs e)
        {
            Music(@"calm1.wav");
        }

        private void TbHeadBang_Click(object sender, RoutedEventArgs e)
        {
            Music(@"headbang.wav");
        }

        private void TbStopMusic_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
        #endregion

        #region Drawing
        private void Line(double xfrom, double yfrom, double xto, double yto)
        {
            Line newLine = new Line();
            newLine.Stroke = System.Windows.Media.Brushes.SaddleBrown;
            newLine.StrokeThickness = 5;

            newLine.X1 = xfrom;
            newLine.Y1 = yfrom;
            newLine.X2 = xto;
            newLine.Y2 = yto;
            cnvTree.Children.Add(newLine);
        }
        private void DrawTreeIntNode(BinaryTreeNode<int> node, int currentHeight,
            double left, double right, double top, double bot)
        {
            if (node == null) return;
            int levelCounter = 1;
            if (node != intTree.Root) levelCounter = intTree.levelFromRootCounter(node); //solution para dili mag dikit2 ang nga node sa intTree
            var container = DrawLeaf(node.Data.ToString(), 30); //CHANGE RADIUS
            if (node == intTree.Root) container = DrawRoot(node.Data.ToString(), 30);
            cnvTree.Children.Add(container);

            #region visualize current node

            Canvas.SetLeft(container, left);
            Canvas.SetRight(container, right);
            Canvas.SetTop(container, top);
            Canvas.SetBottom(container, bot);

            #endregion

            #region set new measurements to left and right children of nodes

            var newLeftMarginTOP = top + 50;
            //var newLeftMarginLEFT = left - (30 * levelCounter);
            var newLeftMarginLEFT = left - 70;
            //var newLeftMarginRIGHT = right + (30 * levelCounter);
            var newLeftMarginRIGHT = right + 70;
            var newLeftMarginBOT = bot - 50;
            var newRightMarginTOP = top + 50;
            //var newRightMarginLEFT = left + (30 * levelCounter);
            var newRightMarginLEFT = left + 70;
            //var newRightMarginRIGHT = right - (30 * levelCounter);
            var newRightMarginRIGHT = right - 70;
            var newRightMarginBOT = bot - 50;
            if (node == intTree.Root)
            {
                newRightMarginLEFT -= 100;
                newLeftMarginLEFT -= 100;
            }
            #endregion of current node



            #region RecycledBin

            //if (node == intTree.Root)
            //{
            //    newLeftMarginLEFT = 500;
            //    newLeftMarginRIGHT = 500;
            //    newRightMarginLEFT = 1200;
            //    newRightMarginRIGHT = 1200;
            //    //newLeftMarginLEFT = 700;
            //    //newLeftMarginRIGHT = 700;
            //    //newRightMarginLEFT = 1100;
            //    //newRightMarginRIGHT = 1100;
            //}
            //draw lines

            // if (node.Right != null && node.Left != null && node != intTree.Root) //auto adjust if puno na ang nodes

            //if (node.Right != null && node.Left != null) //auto adjust if duha ang anak
            //{
            //    if (intTree.Compare(intTree.Root.Data, node.Data) == 1) //left subtree
            //    {
            //        if (node.Left != null) Line(left + 30, top + 60, newLeftMarginLEFT, newLeftMarginTOP + 60);
            //        if (node.Right != null) Line(left + 30, top + 60, newRightMarginLEFT, newRightMarginTOP + 60);
            //        DrawTreeIntNode(node.Left, currentHeight + 50,
            //            newLeftMarginLEFT - 30, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            //        DrawTreeIntNode(node.Right, currentHeight + 50,
            //            newRightMarginLEFT - 30, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            //    }
            //    else //(intTree.Compare(intTree.Root.Data,node.Data) == -1) //right subtree
            //    {
            //        if (node.Left != null) Line(left+30, top + 60, newLeftMarginLEFT+50, newLeftMarginTOP + 60);
            //        if (node.Right != null) Line(left+30, top + 60, newRightMarginLEFT+50, newRightMarginTOP + 60);
            //        DrawTreeIntNode(node.Left, currentHeight + 50,
            //            newLeftMarginLEFT + 30, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            //        DrawTreeIntNode(node.Right, currentHeight + 50,
            //            newRightMarginLEFT + 30, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            //    }
            //else if (node == intTree.Root)
            //{
            //    if (node.Left != null) Line(left + 30, top + 60, newLeftMarginLEFT, newLeftMarginTOP + 60);
            //    if (node.Right != null) Line(left + 30, top + 60, newRightMarginLEFT+50, newRightMarginTOP + 60);
            //    DrawTreeIntNode(node.Left, currentHeight + 50,
            //        newLeftMarginLEFT - 30, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            //    DrawTreeIntNode(node.Right, currentHeight + 50,
            //        newRightMarginLEFT + 30, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);

            //}

            //}
            //else //if dili duha iyang anak
            //{
            //    if (intTree.NumberOfLeavesCounter(intTree.Root.Left) > intTree.NumberOfLeavesCounter(intTree.Root.Right)) //number of leaves on left is greater
            //    {
            //        if (node.Left != null) Line(left + 30, top + 60, newLeftMarginLEFT+60, newLeftMarginTOP + 60);
            //        if (node.Right != null) Line(left + 30, top + 60, newRightMarginLEFT+60, newRightMarginTOP + 60);
            //        DrawTreeIntNode(node.Left, currentHeight + 50,
            //            newLeftMarginLEFT - 30, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            //        DrawTreeIntNode(node.Right, currentHeight + 50,
            //            newRightMarginLEFT - 30, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            //    }
            //    else if (intTree.NumberOfLeavesCounter(intTree.Root.Left) < intTree.NumberOfLeavesCounter(intTree.Root.Right)) //number of leaves on right is greater
            //    {
            //        if (node.Left != null) Line(left + 30, top + 60, newLeftMarginLEFT + 60, newLeftMarginTOP + 60);
            //        if (node.Right != null) Line(left + 30, top + 60, newRightMarginLEFT + 60, newRightMarginTOP + 60);
            //        DrawTreeIntNode(node.Left, currentHeight + 50,
            //            newLeftMarginLEFT + 30, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            //        DrawTreeIntNode(node.Right, currentHeight + 50,
            //            newRightMarginLEFT + 30, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            //    }
            //else
            //{

            #endregion

            #region solution para dili mag dikit2 sa visualizer ang mga node

            //if (intTree.NumberOfLeavesCounter(intTree.Root.Left) > 0 && intTree.NumberOfLeavesCounter(intTree.Root.Right) > 0) //naay sulod ang left and right sa root
            //{
            if (node.Left != null && intTree.Compare(intTree.Root.Data, node.Left.Data) == 1) //kung ang node kay lesser than sa root
                { //sa left
                    newLeftMarginTOP -= 20;
                    newLeftMarginLEFT -= (50 / (levelCounter));
                    newRightMarginLEFT += (200 / (levelCounter));
                    if (intTree.GetParent(node, intTree.Root) != intTree.Root)
                    {
                        newLeftMarginTOP += 75;
                        newRightMarginTOP += 75;
                    }

                    //newRightMarginLEFT += 150;
                    //newRightMarginRIGHT -= (200/levelCounter);

                    //newRightMarginTOP += (30*levelCounter);
                }

                if (node.Right != null && intTree.Compare(intTree.Root.Data, node.Right.Data) == -1)//kung ang node kay greater than sa root
                {
                    //newRightMarginLEFT += (100 / levelCounter);
                    //newLeftMarginLEFT -= (100 / levelCounter);
                    //newRightMarginTOP -= 20;
                    newRightMarginLEFT += (200 / levelCounter);
                    //newRightMarginLEFT += (100 / levelCounter);
                    //newLeftMarginLEFT -= (100 / levelCounter);
                    if (intTree.GetParent(node, intTree.Root) != intTree.Root)
                    {
                        newRightMarginTOP += 75;
                        newLeftMarginTOP += 75;
                    }
                }
                if (intTree.GetParent(node, intTree.Root).Left != null && intTree.GetParent(node, intTree.Root).Right != null) //if ang parent sa node kay naay duha ka anak
                {
                    if (intTree.Compare(node.Data, intTree.GetParent(node, intTree.Root).Data) == 1) // mas dako ang node compared saiyahang parent
                    {
                        newLeftMarginLEFT -= (100 / levelCounter);
                        newRightMarginLEFT -= (100 / levelCounter);
                    }

                    if (intTree.Compare(node.Data, intTree.GetParent(node, intTree.Root).Data) == -1) // mas gamay ang node compared saiyahang parent
                    {
                        newRightMarginLEFT -= (200 / levelCounter);
                        newLeftMarginLEFT -= (200 / levelCounter);
                    }
                }
                newLeftMarginTOP += (20 * levelCounter);
                newRightMarginTOP += (20 * levelCounter);
            //}
            #endregion

            #region visualize children of nodes with matching lines to connect
            if (node.Left != null) Line(left + 50, top + 60, newLeftMarginLEFT + 50, newLeftMarginTOP + 60);
            if (node.Right != null) Line(left + 50, top + 60, newRightMarginLEFT + 50, newRightMarginTOP + 60);
            DrawTreeIntNode(node.Left, currentHeight + 50,
                newLeftMarginLEFT, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            DrawTreeIntNode(node.Right, currentHeight + 50,
                newRightMarginLEFT, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            #endregion

        }
        private void DrawTreeStringNode(BinaryTreeNode<string> node, int currentHeight,
    double left, double right, double top, double bot)
        {

            #region visualize current node
            if (node == null) return;
            int levelCounter = 1;
            if (node != stringTree.Root) levelCounter = stringTree.levelFromRootCounter(node); //solution para dili mag dikit2 ang nga node sa intTree
            var container = DrawLeaf(node.Data.ToString(), 30); //CHANGE RADIUS
            if (node == stringTree.Root) container = DrawRoot(node.Data.ToString(), 30);
            cnvTree.Children.Add(container);
            Canvas.SetLeft(container, left);
            Canvas.SetRight(container, right);
            Canvas.SetTop(container, top);
            Canvas.SetBottom(container, bot);

            #endregion

            #region set new measurements to left and right children of nodes

            var newLeftMarginTOP = top + 50;
            var newLeftMarginLEFT = left - 70;
            var newLeftMarginRIGHT = right + 70;
            var newLeftMarginBOT = bot - 50;
            var newRightMarginTOP = top + 50;
            var newRightMarginLEFT = left + 70;
            var newRightMarginRIGHT = right - 70;
            var newRightMarginBOT = bot - 50;
            if (node == stringTree.Root)
            {
                newRightMarginLEFT -= 100;
                newLeftMarginLEFT -= 100;
            }
            #endregion of current node


            #region solution para dili mag dikit2 sa visualizer ang mga node
            //if (intTree.NumberOfLeavesCounter(intTree.Root.Left) > 0 && intTree.NumberOfLeavesCounter(intTree.Root.Right) > 0) //naay sulod ang left and right sa root
            //{
            if (node.Left != null && stringTree.Compare(stringTree.Root.Data, node.Left.Data) == 1) //kung ang node kay lesser than sa root
            { //sa left
                newLeftMarginTOP -= 20;
                newLeftMarginLEFT -= (50 / (levelCounter));
                newRightMarginLEFT += (200 / (levelCounter));
                if (stringTree.GetParent(node, stringTree.Root) != stringTree.Root)
                {
                    newLeftMarginTOP += 75;
                    newRightMarginTOP += 75;
                }

                //newRightMarginLEFT += 150;
                //newRightMarginRIGHT -= (200/levelCounter);

                //newRightMarginTOP += (30*levelCounter);
            }

            if (node.Right != null && stringTree.Compare(stringTree.Root.Data, node.Right.Data) == -1)//kung ang node kay greater than sa root
            {
                //newRightMarginLEFT += (100 / levelCounter);
                //newLeftMarginLEFT -= (100 / levelCounter);
                //newRightMarginTOP -= 20;
                newRightMarginLEFT += (200 / levelCounter);
                //newRightMarginLEFT += (100 / levelCounter);
                //newLeftMarginLEFT -= (100 / levelCounter);
                if (stringTree.GetParent(node, stringTree.Root) != stringTree.Root)
                {
                    newRightMarginTOP += 75;
                    newLeftMarginTOP += 75;
                }
            }
            if (stringTree.GetParent(node, stringTree.Root).Left != null && stringTree.GetParent(node, stringTree.Root).Right != null) //if ang parent sa node kay naay duha ka anak
            {
                if (stringTree.Compare(node.Data, stringTree.GetParent(node, stringTree.Root).Data) == 1) // mas dako ang node compared saiyahang parent
                {
                    newLeftMarginLEFT -= (100 / levelCounter);
                    newRightMarginLEFT -= (100 / levelCounter);
                }

                if (stringTree.Compare(node.Data, stringTree.GetParent(node, stringTree.Root).Data) == -1) // mas gamay ang node compared saiyahang parent
                {
                    newRightMarginLEFT -= (200 / levelCounter);
                    newLeftMarginLEFT -= (200 / levelCounter);
                }
            }
            newLeftMarginTOP += (20 * levelCounter);
            newRightMarginTOP += (20 * levelCounter);
            //}
            #endregion

            #region visualize children of nodes with matching lines to connect
            if (node.Left != null) Line(left + 50, top + 60, newLeftMarginLEFT + 50, newLeftMarginTOP + 60);
            if (node.Right != null) Line(left + 50, top + 60, newRightMarginLEFT + 50, newRightMarginTOP + 60);
            DrawTreeStringNode(node.Left, currentHeight + 50,
                newLeftMarginLEFT, newLeftMarginRIGHT, newLeftMarginTOP, newLeftMarginBOT);
            DrawTreeStringNode(node.Right, currentHeight + 50,
                newRightMarginLEFT, newRightMarginRIGHT, newRightMarginTOP, newRightMarginBOT);
            #endregion

        }
        private Canvas DrawLeaf(string content, double radius)
        {

            var tb = new TextBlock
            {
                Width = radius * 4,
                Height = radius * 2,
                FontSize = 15,
                FontWeight = FontWeights.UltraBold,
                Foreground = Brushes.Black,
                TextEffects = new TextEffectCollection(),
                FontStretch = FontStretches.UltraExpanded,
                TextAlignment = TextAlignment.Center
            };
            tb.Text = content.ToUpper();
            #region recycledBin

            //var circle = new Ellipse
            //{
            //    Stroke = new SolidColorBrush(Colors.LawnGreen),
            //    Fill = new SolidColorBrush(Colors.LimeGreen),
            //    StrokeThickness = 4,
            //    Width = radius * 2,
            //    Height = radius * 2
            //};

            #endregion
            var im = new Image()
            {
                Source = new BitmapImage(new Uri(@"leaf.png", UriKind.Relative)),
                Width = radius * 2,
                Height = radius * 2
            };
            var container = new Canvas
            {
                Width = radius * 3,
                Height = radius * 2
            };
            container.Children.Add(im);
            container.Children.Add(tb);
            Canvas.SetTop(tb, radius - 20);
            Canvas.SetLeft(tb, 0);
            return container;
        }
        private Canvas DrawRoot(string content, double radius)
        {
            var tb = new TextBlock
            {
                Width = radius * 4,
                Height = radius * 2,
                FontSize = 15,
                FontWeight = FontWeights.UltraBold,
                Foreground = Brushes.Black,
                FontStretch = FontStretches.UltraExpanded,
                TextAlignment = TextAlignment.Center
            };
            tb.Text = content.ToUpper();
            var im = new Image()
            {
                Source = new BitmapImage(new Uri(@"root.png", UriKind.Relative)),
                Width = radius * 2,
                Height = radius * 2
            };
            var container = new Canvas
            {
                Width = radius * 3,
                Height = radius * 2
            };
            container.Children.Add(im);
            container.Children.Add(tb);
            Canvas.SetTop(tb, radius - 20);
            Canvas.SetLeft(tb, 0);
            return container;
        }
#endregion


        private void btnReset(object sender, RoutedEventArgs e)
        {
            #region reload used
            intTree = new BinaryTree<int>();
            stringTree = new BinaryTree<string>();
            rbIntegers.IsEnabled = true;
            rbString.IsEnabled = true;
            rbString.IsChecked = false;
            rbIntegers.IsChecked = false;
            cnvTree.Children.Clear();

            #endregion

        }

        private void BtnMatic_Click(object sender, RoutedEventArgs e)
        {
            #region if radio button integer is checked
            if (rbIntegers.IsChecked == true)
            {
                var nodes = tbMaticTree.Text.Split(',');
                int temp;
                rbIntegers.IsEnabled = false;
                rbString.IsEnabled = false;
                foreach (string s in nodes)
                {
                    if (int.TryParse(s, out temp) == true) intTree.Insert(temp);
                    else MessageBox.Show("Invalid Input");
                }
                //intTree.Insert(tbMaticTree.Text);
                cnvTree.Children.Clear();
                DrawTreeIntNode(intTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
                tbMaticTree.Text = "";
            }
            #endregion

            #region if radio button string is checked

            else if (rbString.IsChecked == true)
            {
                var nodes = tbMaticTree.Text.Split(',');
                rbIntegers.IsEnabled = false;
                rbString.IsEnabled = false;
                foreach (string s in nodes) stringTree.Insert(s);
                cnvTree.Children.Clear();
                DrawTreeStringNode(stringTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
                tbMaticTree.Text = "";
            }

            #endregion

            else MessageBox.Show("Please check either one radio button.");

        }

        private void TbBalance_Click(object sender, RoutedEventArgs e)
        {
            if (rbIntegers.IsChecked == true && intTree.NumberOfLeavesCounter(intTree.Root) > 2)
            {
                intTree.Root = intTree.Balance();
                cnvTree.Children.Clear();
                DrawTreeIntNode(intTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
            }

            else if (rbString.IsChecked == true && stringTree.NumberOfLeavesCounter(stringTree.Root) > 2)
            {
                stringTree.Root = stringTree.Balance();
                cnvTree.Children.Clear();
                DrawTreeStringNode(stringTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
            }
            else
            {
                MessageBox.Show("Insufficient amount of data.");
                return;
            }



        }


        private void TbDelMerging_Click(object sender, RoutedEventArgs e)
        {
            #region merging
            //if (rbIntegers.IsChecked == true)
            //{
            //    //BinaryTreeNode<string> search = intTree.Search(tbMaticTree.Text); //tama ang getparent. naay mali sa deletemethod
            //    var searched = intTree.Search(int.Parse(tbMaticTree.Text));
            //    if (searched == null) return;
            //    if (!intTree.HasTwoChildren(int.Parse(tbMaticTree.Text))) intTree.DeleteLessThanTwoNodes(int.Parse(tbMaticTree.Text));
            //    //delete by merging
            //    var temp = intTree.GetParent(searched, intTree.Root);
            //    if (temp.Left != null && temp.Right != null) intTree.DeleteMerging(int.Parse(tbMaticTree.Text));
            //    cnvTree.Children.Clear();
            //    //MessageBox.Show(intTree.GetParent(search, intTree.Root).Data);
            //    DrawTreeIntNode(intTree.Root, 0, BaseLeftMargin, BaseRightMargin,
            //        BaseTopMargin, BaseBottomMargin);
            //}

            //else if (rbString.IsChecked == true)
            //{
            //    var searched = stringTree.Search(tbMaticTree.Text);
            //    if (searched == null) return;
            //    if (!stringTree.HasTwoChildren(tbMaticTree.Text)) stringTree.DeleteLessThanTwoNodes(tbMaticTree.Text);
            //    var temp = stringTree.GetParent(searched, stringTree.Root);
            //    if (temp.Left != null && temp.Right != null) stringTree.DeleteMerging(tbMaticTree.Text);
            //    cnvTree.Children.Clear();
            //    DrawTreeStringNode(stringTree.Root, 0, BaseLeftMargin, BaseRightMargin,
            //        BaseTopMargin, BaseBottomMargin);
            //}
            //else
            //{
            //    MessageBox.Show("Please select data type and input data");
            //}



            #endregion

            if (rbIntegers.IsChecked == true)
            {
                int n;
                if (!int.TryParse(tbMaticTree.Text, out n) || intTree.Search(int.Parse(tbMaticTree.Text)) == null)
                {
                    MessageBox.Show("Data not found on tree.");
                    return;
                }
                    

                intTree.Delete(int.Parse(tbMaticTree.Text));
                cnvTree.Children.Clear();
                DrawTreeIntNode(intTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
            }

            if (rbString.IsChecked == true)
            {
                if (stringTree.Search(tbMaticTree.Text) == null)
                {
                    MessageBox.Show("Data not found on tree");
                    return;
                }
                stringTree.Delete(tbMaticTree.Text);
                cnvTree.Children.Clear();
                DrawTreeStringNode(stringTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                    BaseTopMargin, BaseBottomMargin);
            }

        }

    }
}
