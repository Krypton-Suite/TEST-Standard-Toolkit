#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp) & Simon Coghlan (aka Smurf-IV), et al. 2026 - 2026. All rights reserved.
 *
 */
#endregion

namespace TestForm;

/// <summary>
/// Comprehensive test form demonstrating taskbar thumbnail button functionality on KryptonForm.
/// </summary>
public partial class TaskbarThumbnailButtonsTest : KryptonForm
{
    private const uint ButtonIdPlay = 1;
    private const uint ButtonIdPause = 2;
    private const uint ButtonIdStop = 3;
    private const uint ButtonIdNext = 4;
    private const uint ButtonIdPrevious = 5;

    private ImageList? _buttonImageList;

    public TaskbarThumbnailButtonsTest()
    {
        InitializeComponent();
        InitializeTaskbarThumbnailButtons();
    }

    private void InitializeTaskbarThumbnailButtons()
    {
        Icon = SystemIcons.Application;

        // Create ImageList with programmatic icons (32x32 for thumbnail buttons)
        _buttonImageList = new ImageList
        {
            ImageSize = new Size(32, 32),
            ColorDepth = ColorDepth.Depth32Bit
        };
        _buttonImageList.Images.Add(CreateButtonIcon(Color.ForestGreen, "Play"));   // 0: Play
        _buttonImageList.Images.Add(CreateButtonIcon(Color.Orange, "Pause"));       // 1: Pause
        _buttonImageList.Images.Add(CreateButtonIcon(Color.Crimson, "Stop"));       // 2: Stop
        _buttonImageList.Images.Add(CreateButtonIcon(Color.DodgerBlue, "Next"));    // 3: Next
        _buttonImageList.Images.Add(CreateButtonIcon(Color.DodgerBlue, "Prev"));    // 4: Previous

        Taskbar.ThumbnailButtons.ImageList = _buttonImageList;

        // Add media-style buttons
        Taskbar.ThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = ButtonIdPlay,
            ImageIndex = 0,
            Tooltip = "Play",
            Flags = ThumbnailButtonFlags.Enabled
        });
        Taskbar.ThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = ButtonIdPause,
            ImageIndex = 1,
            Tooltip = "Pause",
            Flags = ThumbnailButtonFlags.Enabled
        });
        Taskbar.ThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = ButtonIdStop,
            ImageIndex = 2,
            Tooltip = "Stop",
            Flags = ThumbnailButtonFlags.Enabled | ThumbnailButtonFlags.DismissOnClick
        });
        Taskbar.ThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = ButtonIdNext,
            ImageIndex = 3,
            Tooltip = "Next",
            Flags = ThumbnailButtonFlags.Enabled
        });
        Taskbar.ThumbnailButtons.Buttons.Add(new TaskbarThumbnailButton
        {
            Id = ButtonIdPrevious,
            ImageIndex = 4,
            Tooltip = "Previous",
            Flags = ThumbnailButtonFlags.Enabled
        });

        TaskbarThumbnailButtonClick += OnTaskbarThumbnailButtonClick;

        SetupStateExamples();
        SetupHint();
        propertyGrid.SelectedObject = this;
        UpdateClickStatus("Click a thumbnail button (minimize form, hover taskbar) to see feedback.");
    }

    private static Bitmap CreateButtonIcon(Color color, string label)
    {
        var bitmap = new Bitmap(32, 32);
        using (var g = Graphics.FromImage(bitmap))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);

            using (var brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, 2, 2, 28, 28);
            }
            using (var pen = new Pen(Color.White, 2f))
            {
                g.DrawEllipse(pen, 2, 2, 28, 28);
            }
            using (var font = new Font("Segoe UI", 8, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                g.DrawString(label, font, brush, new RectangleF(0, 0, 32, 32), sf);
            }
        }
        return bitmap;
    }

    private void SetupStateExamples()
    {
        lblExample1.Text = "Example 1: Media-style thumbnail buttons (Play, Pause, Stop, Next, Previous). Minimize this form or hover over the taskbar button to see them.";

        lblExample2.Text = "Example 2: Toggle Pause button disabled state";
        btnTogglePauseDisabled.Text = "Toggle Pause Disabled";
        btnTogglePauseDisabled.Click += BtnTogglePauseDisabled_Click;

        lblExample3.Text = "Example 3: Hide or show Next/Previous buttons";
        btnHideNextPrev.Text = "Hide Next/Prev";
        btnHideNextPrev.Click += BtnHideNextPrev_Click;
        btnShowNextPrev.Text = "Show Next/Prev";
        btnShowNextPrev.Click += BtnShowNextPrev_Click;
    }

    private void SetupHint()
    {
        lblHint.Text = "Minimize this form, then hover over its taskbar button to see the thumbnail preview with buttons. Click them to trigger actions.";
    }

    private void OnTaskbarThumbnailButtonClick(object? sender, TaskbarThumbnailButtonClickEventArgs e)
    {
        string action = e.ButtonId switch
        {
            ButtonIdPlay => "Play",
            ButtonIdPause => "Pause",
            ButtonIdStop => "Stop",
            ButtonIdNext => "Next",
            ButtonIdPrevious => "Previous",
            _ => $"Button {e.ButtonId}"
        };
        UpdateClickStatus($"Last clicked: {action}");
    }

    private void UpdateClickStatus(string text)
    {
        lblClickStatus.Text = text;
    }

    private void BtnTogglePauseDisabled_Click(object? sender, EventArgs e)
    {
        var btn = Taskbar.ThumbnailButtons.Buttons.FindById(ButtonIdPause);
        if (btn != null)
        {
            btn.Flags = btn.Flags.HasFlag(ThumbnailButtonFlags.Disabled)
                ? ThumbnailButtonFlags.Enabled
                : ThumbnailButtonFlags.Disabled;
            // Force refresh: briefly clear and restore ImageList to trigger update
            var iml = Taskbar.ThumbnailButtons.ImageList;
            Taskbar.ThumbnailButtons.ImageList = null;
            Taskbar.ThumbnailButtons.ImageList = iml;
        }
    }

    private void BtnHideNextPrev_Click(object? sender, EventArgs e)
    {
        var next = Taskbar.ThumbnailButtons.Buttons.FindById(ButtonIdNext);
        var prev = Taskbar.ThumbnailButtons.Buttons.FindById(ButtonIdPrevious);
        if (next != null)
        {
            next.Flags |= ThumbnailButtonFlags.Hidden;
        }
        if (prev != null)
        {
            prev.Flags |= ThumbnailButtonFlags.Hidden;
        }
        var iml = Taskbar.ThumbnailButtons.ImageList;
        Taskbar.ThumbnailButtons.ImageList = null;
        Taskbar.ThumbnailButtons.ImageList = iml;
    }

    private void BtnShowNextPrev_Click(object? sender, EventArgs e)
    {
        var next = Taskbar.ThumbnailButtons.Buttons.FindById(ButtonIdNext);
        var prev = Taskbar.ThumbnailButtons.Buttons.FindById(ButtonIdPrevious);
        if (next != null)
        {
            next.Flags &= ~ThumbnailButtonFlags.Hidden;
        }
        if (prev != null)
        {
            prev.Flags &= ~ThumbnailButtonFlags.Hidden;
        }
        var iml = Taskbar.ThumbnailButtons.ImageList;
        Taskbar.ThumbnailButtons.ImageList = null;
        Taskbar.ThumbnailButtons.ImageList = iml;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            TaskbarThumbnailButtonClick -= OnTaskbarThumbnailButtonClick;
            _buttonImageList?.Dispose();
            _buttonImageList = null;
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
