Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim list As New BindingList(Of PictureObject)()
			list.Add(New PictureObject("Data Grid", "https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/WinForms-Data-Grid-VisualStudio2013-Light-Theme.png"))
			list.Add(New PictureObject("Loading Error", "https://www.devexpress.com/notfound.png"))
			list.Add(New PictureObject("Banded Reporting", "https://www.devexpress.com/Products/NET/Controls/WinForms/i/landing/controls/Reporting-VisualStudio2013-Light-Theme.png"))

			gridView1.OptionsView.RowAutoHeight = True
			gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
			gridControl1.DataSource = list

			AddHandler list.ListChanged, Function(s, args) AnonymousMethod1(s, args)
		End Sub
		
		Private Function AnonymousMethod1(ByVal s As Object, ByVal args As Object) As Boolean
			If args.PropertyDescriptor.Name = "Image" Then
				gridView1.LayoutChanged()
			End If
			Return True
		End Function
		Public Class PictureObject
			Implements INotifyPropertyChanged
			Private privateName As String
			Public Property Name() As String
				Get
					Return privateName
				End Get
				Set(ByVal value As String)
					privateName = value
				End Set
			End Property
			Private privateImage As Image
			Public Property Image() As Image
				Get
					Return privateImage
				End Get
				Set(ByVal value As Image)
					privateImage = value
				End Set
			End Property
			Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
			Public Sub New(ByVal name As String, ByVal url As String)
				Name = name
				Image = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", GetType(BackgroundImageLoader).Assembly)
				Dim bg As New BackgroundImageLoader()
				bg.Load(url)
                AddHandler bg.Loaded, Function(s, e) AnonymousMethod2(s, e, bg)
			End Sub
			
            Private Function AnonymousMethod2(ByVal s As Object, ByVal e As Object, ByVal bg As BackgroundImageLoader) As Boolean
                Image = bg.Result
                If Not (TypeOf Image Is Image) Then
                    Image = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", GetType(BackgroundImageLoader).Assembly)
                End If
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Image"))
                bg.Dispose()
                Return True
            End Function
		End Class
	End Class
End Namespace
