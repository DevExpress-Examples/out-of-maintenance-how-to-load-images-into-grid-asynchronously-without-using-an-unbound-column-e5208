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
           list.Add(new PictureObject("Image0", @"https://i.imgur.com/tHyzhwKb.jpg"));
            list.Add(new PictureObject("Image1", @"https://i.imgur.com/tHyzhwKb.jpg"));
            list.Add(new PictureObject("Image2", @"https://i.imgur.com/tHyzhwKb.jpg"));
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
                 bg.Loaded += (s, e) => {
                     Image = bg.ImageObject as Image;
                    if (!(Image is Image)) Image = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", typeof(BackgroundImageLoader).Assembly);
                    PropertyChanged(this, new PropertyChangedEventArgs("Image"));
                    //bg.Dispose();
                };
                bg.Load(url);
            }
        }
    }
}
