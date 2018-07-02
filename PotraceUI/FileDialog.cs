namespace PotraceUI
{
    static class OpenFileDialog
    {
        static private Microsoft.Win32.OpenFileDialog dialog;

        static OpenFileDialog()
        {
            dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = @"
BMP (*.bmp)|*.bmp|
GIF (*.gif)|*.gif|
JPG (*.jpg)|*.jpg|
JPEG (*.jpeg)|*.jpeg|
PNG (*.png)|*.png|
TIFF (*.tif, *.tiff)|*.tif;*.tiff|
All Graphics Types(*.bmp,*.gif,*.jpg,*.jpeg,*.png,*.tif,*.tiff)|*.bmp;*.gif;*.jpg;*.jpeg;*.png;*.tif;*.tiff",
                FilterIndex = 7,
                CheckFileExists = true
            };
        }

        static public bool? ShowDialog()
        {
            dialog.FileName = null;
            return dialog.ShowDialog();
        }

        static public string FileName
        {
            get
            {
                return dialog.FileName;
            }
        }

    }

    static class SaveFileDialog
    {
        static private Microsoft.Win32.SaveFileDialog dialog;

        static SaveFileDialog()
        {
            dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = @"Textdateien (*.svg)|*.svg",
                AddExtension = true
            };
        }

        static public bool? ShowDialog()
        {
            dialog.FileName = null;
            return dialog.ShowDialog();
        }

        static public string FileName
        {
            get
            {
                return dialog.FileName;
            }
        }

    }
}
