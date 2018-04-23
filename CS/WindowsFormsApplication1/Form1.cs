using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils;

namespace WindowsFormsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            BindingList<PictureObject> list = new BindingList<PictureObject>();
            list.Add(new PictureObject("Data Grid", @"https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/WinForms-Data-Grid-VisualStudio2013-Light-Theme.png"));
            list.Add(new PictureObject("Loading Error", @"https://www.devexpress.com/notfound.png"));
            list.Add(new PictureObject("Banded Reporting", @"https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/Reporting-VisualStudio2013-Light-Theme.png"));

            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            gridControl1.DataSource = list;
            
            list.ListChanged += (s, args) => { 
                if(args.PropertyDescriptor.Name == "Image")
                    gridView1.LayoutChanged(); 
            };
        }
        public class PictureObject : INotifyPropertyChanged {
            public string Name {get; set;}
            public Image Image {get; set;}
            public event PropertyChangedEventHandler PropertyChanged;
            public PictureObject(string name, string url) {
                Name = name;
                Image = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", typeof(BackgroundImageLoader).Assembly);
                BackgroundImageLoader bg = new BackgroundImageLoader();
                bg.Load(url);
                bg.Loaded += (s, e) => {
                    Image = bg.Result;
                    if(!(Image is Image)) Image = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", typeof(BackgroundImageLoader).Assembly);
                    PropertyChanged(this, new PropertyChangedEventArgs("Image"));
                    bg.Dispose();
                };
            }
        }
    }
}
