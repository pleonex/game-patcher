using System;
using Eto.Forms;
using Eto.Drawing;
using System.Reflection;

namespace GamePatcher.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Icon = Icon.FromResource("GamePatcher.UI.Resources.icon.png");
            Title = "Game patcher ~~ by pleonex";

            var credits = new Dialog {
                Title = "Credits",
                Size = new Size(400, 240),
                Resizable = false,
                Content = new ImageView {
                    Size = new Size(400, 240),
                    Image = Bitmap.FromResource("GamePatcher.UI.Resources.bg_credits.png"),
                },
            };

            var wizard = new Dialog {
                Title = "Patcher wizard",
            };

            var drawable = new Drawable {
                Size = new Size(600, 359),
                Content = new StackLayout {
                    Padding = 10,
                    Spacing = 10,
                    VerticalContentAlignment = VerticalAlignment.Bottom,
                    Orientation = Orientation.Horizontal,
                    Items = {
                        new Button((sender, e) => wizard.ShowModal(this)) {
                            Text = "Patch!",
                        },
                        new Button((sender, e) => credits.ShowModal(this)) {
                            Text = "Credits",
                        }
                    }
                },
            };

            drawable.Paint += (sender, e) => e.Graphics.DrawImage(Bitmap.FromResource("GamePatcher.UI.Resources.bg.png"), 0, 0, drawable.Width, drawable.Height);
            Content = drawable;
        }
    }
}
