using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// ���� �ļ� �̹��� ȭ���Դϴ�.
    /// </summary>
    public partial class CrushingImagePage : ContentPage
    {
        public CrushingImagePage(int indexParam, IEnumerable<byte[]> imageDatas)
        {
            InitializeComponent();

            _pics = new List<ObservableImage>();
            foreach (var imageData in imageDatas)
            {
                var img = ImageSource.FromStream(() => new MemoryStream(imageData));
                _pics.Add(new ObservableImage { Source = img });
            }

            _index = indexParam;

            LoadImage();
        }
        /// <summary>
        /// CrushingCameraPage ParnentView ����
        /// </summary>
        public CrushingCameraPage ParentView { get; set; }

        /// <summary>
        /// �̹��� üũ
        /// </summary>
        public class ObservableImage
        {
            public ImageSource Source { get; set; }
            public bool IsChecked { get; set; }
        }

        private readonly List<ObservableImage> _pics;
        private int _index;

        /// <summary>
        /// �̹��� �ҷ����� �̺�Ʈ
        /// </summary>
        private async void LoadImage()
        {
            try
            {
                StackLayout imageStackLayout = null;

                for (int i = 0; i < _pics.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        imageStackLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Spacing = 5
                        };

                        photoStackLayout.Children.Add(imageStackLayout);
                    }

                    var image = new Image
                    {
                        Source = _pics[i].Source,
                        HeightRequest = 250,
                        WidthRequest = 250
                    };

                    var checkbox = new CheckBox
                    {
                        HorizontalOptions = LayoutOptions.Start // üũ�ڽ��� ���ʿ� �����մϴ�.
                    };
                    checkbox.BindingContext = _pics[i];
                    checkbox.CheckedChanged += Checkbox_CheckedChanged;

                    // �̹����� ������ �� üũ�ڽ��� ���¸� �����մϴ�.
                    image.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(() => checkbox.IsChecked = !checkbox.IsChecked)
                    });

                    var stackLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 5
                    };

                    stackLayout.Children.Add(checkbox);
                    stackLayout.Children.Add(image);

                    imageStackLayout.Children.Add(stackLayout);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("�뺸", ex.Message, "OK");
                return;
            }
        }
        /// <summary>
        /// üũ�ڽ� ���� �̺�Ʈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var observableImage = checkbox.BindingContext as ObservableImage;
            if (observableImage != null)
            {
                observableImage.IsChecked = e.Value;
            }
        }
        /// <summary>
        /// ���� ��ư Ŭ�� �̺�Ʈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            //�̹��� üũ�ڽ� ����Ʈ ���´�.
            var selectedImages = _pics.Where(image => image.IsChecked).ToList();
            //���� �̹����� ���� ���
            if (selectedImages.Count > 0)
            {
                foreach (var observableImage in selectedImages)
                {
                    _pics.Remove(observableImage);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("�뺸", "������ �̹����� �������ּ���.", "OK");
            }
            //�ʱ�ȭ
            photoStackLayout.Children.Clear();
            //�ٽ� �̹��� �ε�
            LoadImage();
        }
        /// <summary>
        /// ���� ��ư Ŭ�� �̺�Ʈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            // ���õ��� ���� �̹����鸸 ���͸��մϴ�.
            var remainingImages = _pics.Where(image => !image.IsChecked).ToList();

            //CrushingImagePage�� ���� �ִ� �̹������� �����մϴ�.
            ParentView?.UpdateImages(remainingImages);

            // CrushingCameraPage�� �̵��մϴ�.
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}