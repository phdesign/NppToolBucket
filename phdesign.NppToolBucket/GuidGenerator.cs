using System;
using System.Text;
using phdesign.NppToolBucket.Forms;
using phdesign.NppToolBucket.PluginCore;

namespace phdesign.NppToolBucket
{
    internal class GuidGenerator
    {
        private readonly Editor _editor;

        private GuidGenerator()
        {
            _editor = Editor.GetActive();
        }

        internal static void Show()
        {
            new GuidGenerator().ShowDialog();
        }

        private void ShowDialog()
        {
            var dialog = new GuidGeneratorForm();
            dialog.ShowDialog();

            var guids = new StringBuilder();
            for (var i = 0; i < dialog.HowMany; i++)
            {
                if (dialog.IncludeBraces)
                    guids.Append("{");
                var guid = Guid.NewGuid().ToString();
                if (dialog.UseUppercase)
                    guid = guid.ToUpper();
                guids.Append(guid);
                if (dialog.IncludeBraces)
                    guids.Append("}");
                guids.AppendLine();
            }
            if (!dialog.IncludeHyphens)
                guids.Replace("-", "");

            _editor.SetSelectedText(guids.ToString());
        }
    }
}