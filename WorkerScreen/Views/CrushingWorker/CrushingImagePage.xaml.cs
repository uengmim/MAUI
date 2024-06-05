using WorkerScreen.ViewModel.CrushingWorker;

namespace WorkerScreen.Views.CrushingWorker
{
    /// <summary>
    /// 문서 파쇄 이미지 화면입니다.
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
        /// CrushingCameraPage ParnentView 설정
        /// </summary>
        public CrushingCameraPage ParentView { get; set; }

        /// <summary>
        /// 이미지 체크
        /// </summary>
        public class ObservableImage
        {
            public ImageSource Source { get; set; }
            public bool IsChecked { get; set; }
        }

        private readonly List<ObservableImage> _pics;
        private int _index;

        /// <summary>
        /// 이미지 불러오는 이벤트
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
                        HorizontalOptions = LayoutOptions.Start // 체크박스를 왼쪽에 정렬합니다.
                    };
                    checkbox.BindingContext = _pics[i];
                    checkbox.CheckedChanged += Checkbox_CheckedChanged;

                    // 이미지를 탭했을 때 체크박스의 상태를 변경합니다.
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
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }
        /// <summary>
        /// 체크박스 변동 이벤트
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
        /// 삭제 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            //이미지 체크박스 리스트 들고온다.
            var selectedImages = _pics.Where(image => image.IsChecked).ToList();
            //선택 이미지가 있을 경우
            if (selectedImages.Count > 0)
            {
                foreach (var observableImage in selectedImages)
                {
                    _pics.Remove(observableImage);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("통보", "삭제할 이미지를 선택해주세요.", "OK");
            }
            //초기화
            photoStackLayout.Children.Clear();
            //다시 이미지 로드
            LoadImage();
        }
        /// <summary>
        /// 저장 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            // 선택되지 않은 이미지들만 필터링합니다.
            var remainingImages = _pics.Where(image => !image.IsChecked).ToList();

            //CrushingImagePage에 남아 있는 이미지들을 전달합니다.
            ParentView?.UpdateImages(remainingImages);

            // CrushingCameraPage로 이동합니다.
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}