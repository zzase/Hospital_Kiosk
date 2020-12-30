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

namespace HKiosk.Pages.ConfirmRequestInfoPage
{
    /// <summary>
    /// ConfirmRequestInfoPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ConfirmRequestInfoPage : Page
    {
        string line1Start = "> 효율적인 고객지원을 위하여 ";
        string line1Highlight = "신용카드 결제";
        string line1End = "를 권장합니다.";

        string line2Start = "> 결제내역은 ";
        string line2Highlight = "LG데이콤 홈페이지";
        string line2End = "에서 확인 가능합니다.";

        public ConfirmRequestInfoPage()
        {
            
            InitializeComponent();
            RichBoxTextHighLight(Line1, line1Start, line1Highlight, line1End);
            RichBoxTextHighLight(Line2, line2Start, line2Highlight, line2End);

        }

        private void RichBoxTextHighLight(RichTextBox richTextBox, string start, string highlight, string end)
        {
            FlowDocument doc = new FlowDocument();
            Run lineStart = new Run(start);
            Run lineHighlight = new Run(highlight);
            Run lineEnd = new Run(end);

            lineHighlight.FontWeight = FontWeights.Bold;
            lineHighlight.Foreground = Brushes.DarkRed;

            Paragraph paragraph = new Paragraph();

            paragraph.Inlines.Add(lineStart);
            paragraph.Inlines.Add(lineHighlight);
            paragraph.Inlines.Add(lineEnd);

            doc.Blocks.Add(paragraph);

            richTextBox.Document = doc;
        }
    }
}
