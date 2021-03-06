﻿using SMTools.Deployment.Utility;
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

namespace WpfApplication1
{
    public interface ITest
    {
        void Run();
    }
    public class Prop
    {
        public string P { get; set; }
    }
    public class Base : ITest
    {
        public Prop Prop { get; set; }
        public virtual void Run()
        {
            MessageBox.Show("base");
        }
    }
    public class Child : Base
    {
        public string S { get; set; }
        public override void Run()
        {
            base.Run();
            MessageBox.Show("child");
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void Test (Base b)
        {
            Child c = b as Child;
            c.S = "Access child";
        }
        public MainWindow()
        {
            InitializeComponent();
            Prop p = new Prop() { P = "sdfsdf" };
            Base b = new Base() { Prop =  p};
            p.P = "changed";
            MessageBox.Show(b.Prop.P);
        }
    }
}
