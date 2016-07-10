using SMTools.FileCopier;
using System.Windows;

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
            FileCopierConfigurator configurator = new FileCopierConfigurator("ProcessConfig.xml", "filecopier");
            FileCopier copier = new FileCopier(configurator);
            datagrid.ItemsSource = configurator.DestinationFolders;
        }
    }
}
