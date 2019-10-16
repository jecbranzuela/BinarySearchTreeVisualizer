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
using System.Xml.Schema;

namespace Branzuela_BinarySearchTreeVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region fields
        BinaryTree<int> intTree = new BinaryTree<int>();
        BinaryTree<string> stringTree = new BinaryTree<string>();
        double BaseLeftMargin = 900;
        double BaseRightMargin = 900;
        double BaseTopMargin = 10;
        double BaseBottomMargin = 440;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

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
            int levelCounter=1;
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
            var newLeftMarginLEFT = left - (30 * levelCounter);
            var newLeftMarginRIGHT = right + (30 * levelCounter);
            var newLeftMarginBOT = bot - 50;
            var newRightMarginTOP = top + 50;
            var newRightMarginLEFT = left + (30 * levelCounter);
            var newRightMarginRIGHT = right - (30 * levelCounter);
            var newRightMarginBOT = bot - 50;

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


            if (intTree.NumberOfLeavesCounter(intTree.Root.Left) > 0 && intTree.NumberOfLeavesCounter(intTree.Root.Right) > 0) //naay sulod ang left and right sa root
            {
                if (node.Left != null && intTree.Compare(intTree.Root.Data, node.Left.Data) == 1) //kung ang node kay lesser than sa root
                { //sa left
                    newLeftMarginLEFT -= (100 / (levelCounter));
                    newRightMarginLEFT += (100 / (levelCounter));
                }

                if (node.Right != null && intTree.Compare(intTree.Root.Data, node.Right.Data) == -1)
                {
                    newRightMarginLEFT += (200 / levelCounter);
                    newLeftMarginLEFT -= (100 / levelCounter);
                }
                if (intTree.GetParent(node, intTree.Root).Left != null && intTree.GetParent(node, intTree.Root).Right != null) //if ang parent sa node kay naay duha ka anak
                {
                    if (intTree.Compare(node.Data, intTree.GetParent(node, intTree.Root).Data) == 1) // mas dako ang node compared saiyahang parent
                    {
                        newLeftMarginLEFT += (100 / levelCounter);
                        newRightMarginLEFT -= (200 / levelCounter);
                    }
                    if (intTree.Compare(node.Data, intTree.GetParent(node, intTree.Root).Data) == -1) // mas gamay ang node compared saiyahang parent
                    {
                        newRightMarginLEFT += (100 / levelCounter);
                        newLeftMarginLEFT -= (100 / levelCounter);
                    }
                }
                newLeftMarginTOP += (30 * levelCounter);
            }
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

            var newLeftMarginTOP = top + 80;
            var newLeftMarginLEFT = left - (30 * levelCounter);
            var newLeftMarginRIGHT = right + (30 * levelCounter);
            var newLeftMarginBOT = bot - 80;
            var newRightMarginTOP = top + 80;
            var newRightMarginLEFT = left + (30 * levelCounter);
            var newRightMarginRIGHT = right - (30 * levelCounter);
            var newRightMarginBOT = bot - 80;

            #endregion of current node

            #region solution para dili mag dikit2 sa visualizer ang mga node


            if (stringTree.NumberOfLeavesCounter(stringTree.Root.Left) > 0 && stringTree.NumberOfLeavesCounter(stringTree.Root.Right) > 0) //naay sulod ang left and right sa root
            {
                if (node.Left != null && stringTree.Compare(stringTree.Root.Data, node.Left.Data) == 1) //kung ang node kay lesser than sa root
                { //sa left
                    newLeftMarginLEFT -= (100 / (levelCounter));
                    newRightMarginLEFT += (100 / (levelCounter));
                }

                if (node.Right != null && stringTree.Compare(stringTree.Root.Data, node.Right.Data) == -1)
                {
                    newRightMarginLEFT += (200 / levelCounter);
                    newLeftMarginLEFT -= (100 / levelCounter);
                }
                if (stringTree.GetParent(node, stringTree.Root).Left != null && stringTree.GetParent(node, stringTree.Root).Right != null) //if ang parent sa node kay naay duha ka anak
                {
                    if (stringTree.Compare(node.Data, stringTree.GetParent(node, stringTree.Root).Data) == 1) // mas dako ang node compared saiyahang parent
                    {
                        newLeftMarginLEFT += (100 / levelCounter);
                        newRightMarginLEFT -= (200 / levelCounter);
                    }
                    if (stringTree.Compare(node.Data, stringTree.GetParent(node, stringTree.Root).Data) == -1) // mas gamay ang node compared saiyahang parent
                    {
                        newRightMarginLEFT += (100 / levelCounter);
                        newLeftMarginLEFT -= (100 / levelCounter);
                    }
                }
                newLeftMarginTOP += (30 * levelCounter);
            }
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
                Width = radius * 3,
                Height = radius * 2,
                FontSize = 15,
                FontWeight = FontWeights.UltraBold,
                Foreground = Brushes.Black,
                FontStretch = FontStretches.UltraExpanded,
                TextAlignment = TextAlignment.Center
            };
            tb.Text = content.ToString();
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
                Width = radius * 3,
                Height = radius * 2,
                FontSize = 15,
                FontWeight = FontWeights.UltraBold,
                Foreground = Brushes.Black,
                FontStretch = FontStretches.UltraExpanded,
                TextAlignment = TextAlignment.Center
            };
            tb.Text = content;
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

        #region Garbage pero basig magamit

        //private void DrawLeaf(string content, double radius, double centerX, double centerY)
        //{
        //    intTree.Insert(content);
        //    var container = DrawLeaf(content, radius);
        //    cnvTree.Children.Add(container);
        //    if (intTree.Root.Data == content) //if root
        //    {
        //        Canvas.SetTop(container, centerY - 250);
        //        Canvas.SetLeft(container, centerX);
        //    }
        //    else if (intTree.Compare(intTree.Root.Data, content) == 1) //left side 
        //    {
        //        if (intTree.Compare(intTree.Root.Left.Data, content) == 0) //left directly from root
        //        {
        //            Canvas.SetTop(container, centerY - (centerY / 2));
        //            Canvas.SetLeft(container, centerX - (centerX / 2));
        //        }
        //        else
        //        {
        //        }

        //    }
        //    else if (intTree.Compare(intTree.Root.Data, content) == -1) //right
        //    {
        //        Canvas.SetTop(container, centerY - (centerY / 2));
        //        Canvas.SetLeft(container, centerX + (centerX / 2));
        //    }
        //}
        //private void btnGenerate(object sender, RoutedEventArgs e)
        //{
        //    DrawLeaf(tbTreeContent.Text, 50, cnvTree.Width / 2, cnvTree.Height / 2);
        //}
        //private void DrawTreeWithoutAesthetic(BinaryTreeNode<string> node, int currentHeight, Thickness margin)
        //{
        //    if (node == null)
        //    {
        //        return;
        //    }

        //    Label label = new Label { Content = node.Data, Margin = margin, Width = 20 };

        //    cnvTree.Children.Add(label);

        //    Thickness newLeftMargin = new Thickness
        //    {
        //        Top = margin.Top + 50,
        //        Left = margin.Left - 25,
        //        Right = margin.Right + 25,
        //        Bottom = margin.Bottom - 50
        //    };

        //    Thickness newRightMargin = new Thickness
        //    {
        //        Top = margin.Top + 50,
        //        Left = margin.Left + 25,
        //        Right = margin.Right - 25,
        //        Bottom = margin.Bottom - 50
        //    };

        //    // Show children
        //    DrawTreeWithoutAesthetic(node.Left, currentHeight + 1, newLeftMargin);
        //    DrawTreeWithoutAesthetic(node.Right, currentHeight + 1, newRightMargin);
        //}

        #endregion



        private void btnReset(object sender, RoutedEventArgs e)
        {
            #region reload used resources para virgin usab
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
            //var queueInOrderNodes = intTree.InOrderTraversal();
            //intTree.Clear();
            //foreach (var queueInOrderNode in queueInOrderNodes) intTree.Insert(queueInOrderNode.Data);
            //MessageBox.Show(intTree.buildTree(intTree.Root).Data);
            //MessageBox.Show(intTree.buildTree(intTree.Root).Data);
            cnvTree.Children.Clear();
            //DrawTreeIntNode(intTree.Balance().Root, 0, BaseLeftMargin, BaseRightMargin,
                //BaseTopMargin, BaseBottomMargin);

        }

        private void TbDelMerging_Click(object sender, RoutedEventArgs e)
        {
            //BinaryTreeNode<string> search = intTree.Search(tbMaticTree.Text); //tama ang getparent. naay mali sa deletemethod
            var searched = intTree.Search(int.Parse(tbMaticTree.Text));
            if (searched == null) return;
            if (!intTree.HasTwoChildren(int.Parse(tbMaticTree.Text))) intTree.DeleteLessThanTwoNodes(int.Parse(tbMaticTree.Text));
            //delete by merging
            if (intTree.GetParent(searched,intTree.Root).Left != null && intTree.GetParent(searched,intTree.Root).Right != null) intTree.DeleteMerging(int.Parse(tbMaticTree.Text));
            cnvTree.Children.Clear();
            //MessageBox.Show(intTree.GetParent(search, intTree.Root).Data);
            DrawTreeIntNode(intTree.Root, 0, BaseLeftMargin, BaseRightMargin,
                BaseTopMargin, BaseBottomMargin);
        }

        private void TbGetParent_Click(object sender, RoutedEventArgs e)
        {
            var searched = intTree.Search(int.Parse(tbMaticTree.Text));
            MessageBox.Show(intTree.HasTwoChildren(int.Parse(tbMaticTree.Text)).ToString()); //tama na ang code sa number of leaves
        }

        private void RbString_Checked(object sender, RoutedEventArgs e)
        {
            //intTree = new BinaryTree<string>();
        }

        private void RbIntegers_Checked(object sender, RoutedEventArgs e)
        {
        } 
    }
}
