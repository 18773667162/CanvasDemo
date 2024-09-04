using CanvasDemo.Common;
using CanvasDemo.Core;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Core.StyleManager;
using CanvasDemo.Other;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CanvasDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICanvasManager _canvasManager;
        private readonly IStyleManager _styleManager;
        private readonly IUndoRedoManager _undoRedoManager;
        private WidgetStyle _widgetStyle = null;
        private bool _Lock = false;

        private void InitComboBox()
        {
            var dashStyles = new List<DashStyleItem>
            {
                new DashStyleItem { Name = "实线", Value = DashStyles.Solid },
                new DashStyleItem { Name = "虚线", Value = DashStyles.Dash },
                new DashStyleItem { Name = "点线", Value = DashStyles.Dot },
                new DashStyleItem { Name = "虚点线", Value = DashStyles.DashDot },
                new DashStyleItem { Name = "虚点点线", Value = DashStyles.DashDotDot }
            };
            BorderStyleType.ItemsSource = dashStyles;
        }

        public MainWindow(ICanvasManager canvasManager, IStyleManager styleManager, IUndoRedoManager undoRedoManager)
        {
            InitializeComponent();
            _canvasManager = canvasManager;
            _styleManager = styleManager;
            _undoRedoManager = undoRedoManager;
            _styleManager.StyleChangeEvent += ShowAllSelectedWidgets; 
            InitComboBox();
        }

        private void AddElement_Click(object sender, RoutedEventArgs e)
        {
            _canvasManager.AddElement(LayerOperationTypeEnum.ImageBox);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _canvasManager.RemoveElements();
        }
        private void ToggleNavigation_Click(object sender, RoutedEventArgs e)
        {
            if (NavBar.Width == 0)
            {
                // 显示导航栏
                NavBar.Width = 200;
                FadeIn(NavBar);
            }
            else
            {
                // 隐藏导航栏
                FadeOut(NavBar, () => NavBar.Width = 0);
            }
        }

        private void BorderThiness_Leave(object sender, RoutedEventArgs e)
        {
            var isSuccess = double.TryParse(BorderThiness.Text, out double res);
            if (!isSuccess || _Lock) return;
            _widgetStyle.BorderThickness = res;
            _styleManager.SetElementStyle(_widgetStyle);
        }

        private void ShowAllSelectedWidgets(object sender, StyleChangeEventArgs e)
        {
            if (e.Layer != null)
            {
                _Lock = true;
                // 显示导航栏
                NavBar.Width = 200;
                BorderThiness.Text = e.Layer.BorderStyle.BorderThickness.ToString();
                BorderStyleType.SelectedIndex = (int)ObjConvert.DashStyleToEnum(e.Layer.BorderStyle.DashStyle);
                colorPicker.SetColor(e.Layer.BorderStyle.BorderBrush);
                _Lock = false;
                _widgetStyle = new WidgetStyle();
                _widgetStyle.DashStyle = e.Layer.BorderStyle.DashStyle;
                _widgetStyle.BorderBrush = e.Layer.BorderStyle.BorderBrush;
                _widgetStyle.BorderThickness = e.Layer.BorderStyle.BorderThickness;
            }
            else
            {
                // 隐藏导航栏
                NavBar.Width = 0;
            }
        }

        private void ColorSelected(object sender, RoutedEventArgs e)
        {
            colorPicker.SetColor(Colors.Red);
        }

        private void FadeIn(UIElement element)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            element.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
        }

        private void FadeOut(UIElement element, System.Action onCompleted)
        {
            DoubleAnimation fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
            fadeOutAnimation.Completed += (s, e) => onCompleted();
            element.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }

        private void BorderStyleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null || _Lock) return;
            var ty = (BorderDashStyleEnum)comboBox.SelectedIndex;
            _widgetStyle.DashStyle = ObjConvert.EnumToDashStyle(ty);
            _styleManager.SetElementStyle(_widgetStyle);
        }

        private void colorPicker_OnPickColor(Color color)
        {
            if (_Lock) return;
            _widgetStyle.BorderBrush = color;
            _styleManager.SetElementStyle(_widgetStyle);
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Undo();
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            _undoRedoManager.Redo();
        }
    }
}
