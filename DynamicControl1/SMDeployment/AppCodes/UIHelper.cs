using SMTools.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SMDeployment.AppCodes
{
    public static class UIHelper
    {
        public static TreeView CreateTree(DirInfor dir, Brush folderColor, Brush fileColor, int fontSize)
        {
            TreeView tree = new TreeView();
            TreeViewItem root = new TreeViewItem();
            root.Header = CreateGrid(dir.RelativeRoot, dir.ModifiedDate);
            root.Background = folderColor;
            root.FontSize = fontSize;
            foreach (DirInfor df in dir.SubDirectories)
            {
                root.Items.Add(CreateTree(df, folderColor, fileColor, fontSize));
            }
            foreach (FileInfor ff in dir.Files)
            {
                TreeViewItem fitem = new TreeViewItem();
                fitem.Header = CreateGrid(ff.FileName, ff.ModifiedDate);
                fitem.Foreground = fileColor;
                fitem.FontSize = fontSize;
                root.Items.Add(fitem);
            }
            tree.Items.Add(root);
            return tree;
        }

        public static Grid CreateGrid(string column1, DateTime column2)
        {
            Grid grid = new Grid();
            RowDefinition r1 = new RowDefinition();
            ColumnDefinition c1 = new ColumnDefinition()
            {
                Width = new System.Windows.GridLength(450) // column1 width
            };
            ColumnDefinition c2 = new ColumnDefinition();
            grid.RowDefinitions.Add(r1);
            grid.ColumnDefinitions.Add(c1);
            grid.ColumnDefinitions.Add(c2);
            Label l1 = new Label()
            {
                Content = column1,
                ToolTip = column1,
            };
            bool isToday =
                DateTime.Now.Year == column2.Year
                && DateTime.Now.Month == column2.Month
                && DateTime.Now.Day == column2.Day;
            Label l2 = new Label()
            {
                Content = column2,
                ToolTip = column2,
                Foreground = isToday ? Brushes.Green : Brushes.Red
            };
            Grid.SetRow(l1, 0);
            Grid.SetRow(l2, 0);
            Grid.SetColumn(l1, 0);
            Grid.SetColumn(l2, 1);
            grid.Children.Add(l1);
            grid.Children.Add(l2);
            return grid;
        }

        public static ScrollViewer CreateScroll(object content)
        {
            ScrollViewer scroll = new ScrollViewer()
            {
                CanContentScroll = true,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                FlowDirection = FlowDirection.LeftToRight
            };
            scroll.Content = content;
            return scroll;
        }

        public static void AddChild(UIElementCollection collection, UIElement element, bool clear = true)
        {
            collection.Clear();
            collection.Add(element);
        }

        public static ScrollViewer CreateScrollTree(DirInfor dir)
        {
            return CreateScroll(CreateTree(dir, Brushes.LightGoldenrodYellow, Brushes.Black, 13));
        }

        public static ScrollViewer CreateScrollDataGrid(IEnumerable<object> dataSource)
        {
            DataGrid grid = new DataGrid();
            grid.CanUserAddRows = false;
            grid.ItemsSource = dataSource;
            return CreateScroll(grid);
        }

        /// <summary>
        /// Create empty grid with row and column
        /// </summary>
        public static Grid CreateGrid(int row, int column)
        {
            Grid grid = new Grid();
            grid.Background = Brushes.Transparent;
            for (int i = 0; i < row; i++)
            {
                RowDefinition r1 = new RowDefinition();
                grid.RowDefinitions.Add(r1);
            }
            for (int i = 0; i < column; i++)
            {
                ColumnDefinition c1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(c1);
            }
            return grid;
        }

        /// <summary>
        /// Create grid 1 row, 2 columns, width dropdown in column 2
        /// </summary>
        /// <param name="dropdownItems"></param>
        /// <returns></returns>
        public static Grid CreateProjectSelectionGrid(List<string> dropdownItems, SelectionChangedEventHandler onDropdownSelectionChanged)
        {
            var grid = CreateGrid(1, 2);
            ComboBox cbx = new ComboBox()
            {
                Height = 30
            };
            cbx.SelectionChanged += onDropdownSelectionChanged;
            cbx.ItemsSource = dropdownItems;
            Grid.SetColumn(cbx, 1);
            Grid.SetRow(cbx, 0);
            Label lbl = new Label()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                Content = "Select project:",
            };
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);
            grid.Children.Add(cbx);
            grid.Children.Add(lbl);
            return grid;
        }
        /// <summary>
        /// Create a grid with model vertically
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Grid CreateRotateVerticalGrid(object model)
        {
            PropertyInfo[] props = model.GetType().GetProperties();
            var grid = CreateGrid(props.Length, 2);
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                Label lbl = new Label()
                {
                    Content = prop.Name
                };
                Grid.SetColumn(lbl, 0);
                Grid.SetRow(lbl, i);
                TextBox txt = new TextBox()
                {
                    Text = prop.GetValue(model) == null ? string.Empty : prop.GetValue(model).ToString()
                };
                Grid.SetColumn(txt, 1);
                Grid.SetRow(txt, i);
                grid.Children.Add(lbl);
                grid.Children.Add(txt);
            }
            return grid;
        }
        /// <summary>
        /// Open choose folder dialog
        /// </summary>
        /// <returns>Selected folder path</returns>
        public static string OpenChooseFolderDialog()
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dr = fbd.ShowDialog();
            string res = string.Empty;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                res = fbd.SelectedPath;
            }
            return res;
        }

        public static T FindControl<T>(DependencyObject obj, string controlName) where T : Control
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        if (((T)child).Name == controlName)
                        {
                            return (T)child;
                        }
                    }
                    else
                    {
                        T t = FindControl<T>(child, controlName);
                        if (t != null)
                        {
                            return t;
                        }
                    }
                }
            }
            return null;
        }
    }
}
