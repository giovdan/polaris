namespace Mitrol.Framework.StartPolaris.Models.Controls
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ButtonUserControl : UserControl
    {
        public ButtonUserControl()
        {
            InitializeComponent();

            labelText.Click += (s, e) => InvokeOnClick(this, e);
        }

        public Image ImageIcon
        {
            get => iconBox.BackgroundImage;
            set => iconBox.BackgroundImage = value;
        }

        public string TextCaption
        {
            get => labelText.Text;
            set => labelText.Text = value;
        }

        public bool Highlighted {
            get => _highlighted;
            set
            {
                if (_highlighted != value)
                {
                    _highlighted = value;
                    OnHighlighedChange();
                }
            }
        }
        private bool _highlighted;

        void OnHighlighedChange()
        {
            if (_highlighted)
            {
                BackColor = Color.LightGray;
            }
            else
            {
                BackColor = Color.DimGray;
            }
        }


    }
}
