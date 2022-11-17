Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Namespace WindowsFormsApplication1

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim list As BindingList(Of PictureObject) = New BindingList(Of PictureObject)()
            list.Add(New PictureObject("Data Grid", "https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/WinForms-Data-Grid-VisualStudio2013-Light-Theme.png"))
            list.Add(New PictureObject("Loading Error", "https://www.devexpress.com/notfound.png"))
            list.Add(New PictureObject("Banded Reporting", "https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/Reporting-VisualStudio2013-Light-Theme.png"))
            gridView1.OptionsView.RowAutoHeight = True
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
            gridControl1.DataSource = list
            AddHandler list.ListChanged, Sub(s, args)
                If Equals(args.PropertyDescriptor.Name, "Image") Then gridView1.LayoutChanged()
            End Sub
        End Sub

        Public Class PictureObject
            Implements INotifyPropertyChanged

            Public Property Name As String

            Public Property Image As Image

            Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

            Public Sub New(ByVal name As String, ByVal url As String)
                Me.Name = name
                Image = ResourceImageHelperCore.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", GetType(BackgroundImageLoader).Assembly)
                Dim bg As BackgroundImageLoader = New BackgroundImageLoader()
                AddHandler bg.Loaded, Sub(s, e)
                    Image = TryCast(bg.ImageObject, Image)
                    If Not(TypeOf Image Is Image) Then Image = ResourceImageHelperCore.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", GetType(BackgroundImageLoader).Assembly)
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Image"))
                    bg.Dispose()
                End Sub
                bg.Load(url)
            End Sub
        End Class
    End Class
End Namespace
