using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Microsoft.Maui.Storage;

namespace Lab02;

public partial class MainPage : ContentPage
{
    private IDocumentAnalyzer _analyzer;

    public MainPage()
    {
        InitializeComponent();
        AnalysisPicker.ItemsSource = new List<string> { "SAX", "DOM", "LINQ to XML" };
    }

    private async void BrowseXmlButton_Clicked(object sender, EventArgs e)
    {
        XmlFilePathEntry.Text = await PickFileAsync("Виберіть XML-файл", new[] { ".xml" });
    }

    private async void BrowseXslButton_Clicked(object sender, EventArgs e)
    {
        XslFilePathEntry.Text = await PickFileAsync("Виберіть XSL-файл", new[] { ".xsl" });
    }

    private async void BrowseHtmlButton_Clicked(object sender, EventArgs e)
    {
        HtmlFilePathEntry.Text = await PickFileAsync("Виберіть HTML-файл", new[] { ".html" });
    }

    private void OnAnalyze(object sender, EventArgs e) { AnalyzeXml(); }

    private void OnTransform(object sender, EventArgs e) { TransformToHtml(); }

    private void OnClear(object sender, EventArgs e) { ClearAll(); }

    private async Task<string> PickFileAsync(string pickerTitle, string[] allowedExtensions)
    {
        var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.WinUI, allowedExtensions }
        });

        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = customFileType,
            PickerTitle = pickerTitle
        });

        return result?.FullPath ?? string.Empty;
    }

    private async void AnalyzeXml()
    {
        var strategy = AnalysisPicker.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(XmlFilePathEntry.Text))
        {
            await DisplayAlert("Помилка", "Будь ласка, виберіть XML-файл.", "OK");
            return;
        }

        switch (strategy)
        {
            case "SAX": _analyzer = new SaxAnalyzer(); break;
            case "DOM": _analyzer = new DomAnalyzer(); break;
            case "LINQ to XML": _analyzer = new LinqToXmlAnalyzer(); break;
            default:
                await DisplayAlert("Помилка", "Оберіть спосіб аналізу", "OK");
                return;
        }

        var name = NameEntry.Text?.Trim();
        var faculty = FacultyEntry.Text?.Trim();
        var department = DepartmentEntry.Text?.Trim();
        var position = PositionEntry.Text?.Trim();
        var salaryFrom = SalaryFromEntry.Text?.Trim();
        var salaryTo = SalaryToEntry.Text?.Trim();

        var results = _analyzer.AnalyzeDocument(XmlFilePathEntry.Text, name, faculty, department, position, salaryFrom, salaryTo);

        // Перевіряємо результати
        if (results.Any())
        {
            ResultsCollectionView.ItemsSource = results;
        }
        else
        {
            await DisplayAlert("Результати", "Нічого не знайдено за заданими критеріями.", "OK");
        }
    }

    private async void TransformToHtml()
    {
        if (string.IsNullOrEmpty(XmlFilePathEntry.Text) || string.IsNullOrEmpty(XslFilePathEntry.Text) || string.IsNullOrEmpty(HtmlFilePathEntry.Text))
        {
            await DisplayAlert("Помилка", "Будь ласка, вкажіть файли XML, XSL і HTML.", "OK");
            return;
        }

        try
        {
            XslCompiledTransform xslTransform = new();
            xslTransform.Load(XslFilePathEntry.Text);

            using FileStream input = new(XmlFilePathEntry.Text, FileMode.Open);
            using FileStream output = new(HtmlFilePathEntry.Text, FileMode.Create);

            xslTransform.Transform(XmlReader.Create(input), null, output);

            await DisplayAlert("Успіх", "Файл XML успішно перетворено у HTML.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", $"Сталася помилка під час трансформації: {ex.Message}", "OK");
        }
    }

    private void ClearAll()
    {
        XmlFilePathEntry.Text = "";
        XslFilePathEntry.Text = "";
        HtmlFilePathEntry.Text = "";
        NameEntry.Text = "";
        FacultyEntry.Text = "";
        DepartmentEntry.Text = "";
        PositionEntry.Text = "";
        SalaryFromEntry.Text = "";
        SalaryToEntry.Text = "";
        AnalysisPicker.SelectedItem = null;
        ResultsCollectionView.ItemsSource = null;
    }

}
