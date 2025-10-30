﻿#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp) & Simon Coghlan(aka Smurf-IV), tobitege et al. 2024 - 2025. All rights reserved.
 *
 */
#endregion

using ContentAlignment = System.Drawing.ContentAlignment;
using Timer = System.Windows.Forms.Timer;

namespace Krypton.Toolkit;

internal partial class VisualToastNotificationBasicWithProgressBarForm : VisualToastNotificationBaseForm
{
    #region Instance Fields

    private int _time, _countdownValue;

    private Timer _timer;

    private SoundPlayer? _soundPlayer;

    private PaletteBase _palette;

    private readonly KryptonBasicToastNotificationData _basicToastNotificationData;

    #endregion

    #region Identity

    public VisualToastNotificationBasicWithProgressBarForm(KryptonBasicToastNotificationData data)
    {
        _basicToastNotificationData = data;

        InitializeComponent();

        GotFocus += VisualToastNotificationBasicWithProgressBarForm_GotFocus;

        Resize += VisualToastNotificationBasicWithProgressBarForm_Resize;

        LocationChanged += VisualToastNotificationBasicWithProgressBarForm_LocationChanged;

        DoubleBuffered = true;

        UpdateBorderColors();

        /* FadeValues disabled and moved to extended until proven stable. Further development in V100
        UpdateFadeValues();
        */

        UpdateFonts();
    }

    #endregion

    #region Public

    internal bool ReturnValue => kchkDoNotShowAgain.Checked;

    internal CheckState ReturnCheckBoxStateValue => kchkDoNotShowAgain.CheckState;

    #endregion

    #region Implementation

    private void UpdateText()
    {
        krtbNotificationContentText.Text = _basicToastNotificationData.NotificationContent ?? string.Empty;

        klblHeader.Text = _basicToastNotificationData.NotificationTitle;

        klblHeader.StateCommon.ShortText.TextH = _basicToastNotificationData.NotificationTitleAlignmentH ?? PaletteRelativeAlign.Center;

        klblHeader.StateCommon.ShortText.TextV = _basicToastNotificationData.NotificationTitleAlignmentV ?? PaletteRelativeAlign.Center;
    }

    private void UpdateBorderColors()
    {
        StateCommon!.Border.Color1 = _basicToastNotificationData.BorderColor1 ?? GlobalStaticValues.EMPTY_COLOR;

        StateCommon.Border.Color2 = _basicToastNotificationData.BorderColor2 ?? GlobalStaticValues.EMPTY_COLOR;
    }

    /* FadeValues disabled and moved to extended until proven stable. Further development in V100
    private void UpdateFadeValues() => FadeValues.FadingEnabled = _basicToastNotificationData.UseFade;
    */

    private void UpdateFonts()
    {
        krtbNotificationContentText.StateCommon.Content.Font = _basicToastNotificationData.NotificationContentFont ??
                                                               KryptonManager.CurrentGlobalPalette.BaseFont;

        if (_basicToastNotificationData.NotificationTitleFont != null)
        {
            krtbNotificationContentText.InputControlStyle = InputControlStyle.PanelClient;

            klblHeader.StateCommon.ShortText.Font =
                _basicToastNotificationData.NotificationTitleFont ?? _palette.Header1ShortFont;
        }
        else
        {
            klblHeader.LabelStyle = LabelStyle.TitleControl;
        }
    }

    private void UpdateIcon()
    {
        var bitmap = GraphicsExtensions.GetToastNotificationBitmap(
            _basicToastNotificationData.NotificationIcon,
            null,
            _basicToastNotificationData.CustomImage,
            new Size(128, 128));

        SetIcon(bitmap);
    }

    private void UpdateDoNotShowAgainOptionChecked() =>
        kchkDoNotShowAgain.Checked = _basicToastNotificationData.IsDoNotShowAgainOptionChecked;

    private void UpdateDoNotShowAgainOptionCheckState() =>
        kchkDoNotShowAgain.CheckState =
            _basicToastNotificationData.DoNotShowAgainOptionCheckState ?? CheckState.Unchecked;

    private void SetIcon(Bitmap? image) => pbxImage.Image = image;

    private void UpdateLocation()
    {
        //Once loaded, position the form, or position it to the bottom left of the screen with added padding
        Location = _basicToastNotificationData.NotificationLocation ?? new Point(Screen.PrimaryScreen!.WorkingArea.Width - Width - 5,
            Screen.PrimaryScreen!.WorkingArea.Height - Height - 5);
    }

    private void VisualToastNotificationBasicWithProgressBarForm_Load(object sender, EventArgs e)
    {
        UpdateLocation();

        ShowCloseButton();

        _timer.Start();

        _soundPlayer?.Play();

        kbtnDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;

        itbDismiss.Text = KryptonManager.Strings.ToastNotificationStrings.Dismiss;
    }

    private void VisualToastNotificationBasicWithProgressBarForm_Resize(object? sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Maximized)
        {
            WindowState = FormWindowState.Normal;
        }
    }

    private void VisualToastNotificationBasicWithProgressBarForm_GotFocus(object? sender, EventArgs e)
    {
        kbtnDismiss.Focus();
    }

    private void VisualToastNotificationBasicWithProgressBarForm_LocationChanged(object? sender, EventArgs e)
    {
        if (_basicToastNotificationData.ReportToastLocation)
        {
            ReportToastLocation();
        }
    }

    private void ReportToastLocation() => klblToastLocation.Text = _basicToastNotificationData.ReportToastLocation ? $"Location: X: {Location.X}, Y: {Location.Y}" : string.Empty;

    private void kbtnDismiss_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ShowCloseButton()
    {
        CloseBox = _basicToastNotificationData.ShowCloseBox ?? false;

        FormBorderStyle = CloseBox ? FormBorderStyle.Fixed3D : FormBorderStyle.FixedSingle;

        ControlBox = _basicToastNotificationData.ShowCloseBox ?? false;
    }

    private void UpdateProgressBarText() => kpbCountDown.Text = _basicToastNotificationData.ShowCountDownSecondsOnProgressBar ? $@"{_basicToastNotificationData.CountDownSeconds - _time}" : string.Empty;

    public new void Show()
    {
        TopMost = _basicToastNotificationData.TopMost ?? true;

        UpdateText();

        UpdateIcon();

        if (_basicToastNotificationData.CountDownSeconds != 0)
        {
            _countdownValue = _basicToastNotificationData.CountDownSeconds ?? 60;

            kpbCountDown.Maximum = _countdownValue;

            kpbCountDown.Value = _countdownValue;

            UpdateProgressBarText();

            _timer = new Timer();

            _timer.Interval = _basicToastNotificationData.CountDownTimerInterval ?? 1000;

            _timer.Tick += (sender, args) =>
            {
                _time++;

                kpbCountDown.Value -= 1;

                UpdateProgressBarText();

                if (kpbCountDown.Value == kpbCountDown.Minimum)
                {
                    _timer.Stop();

                    Close();
                }
            };
        }


        base.Show();
    }

    public new DialogResult ShowDialog()
    {
        TopMost = _basicToastNotificationData.TopMost ?? true;

        UpdateText();

        UpdateIcon();

        if (_basicToastNotificationData.IsDoNotShowAgainOptionChecked)
        {
            UpdateDoNotShowAgainOptionChecked();
        }

        if (_basicToastNotificationData.DoNotShowAgainOptionCheckState != null)
        {
            UpdateDoNotShowAgainOptionCheckState();
        }

        if (_basicToastNotificationData.CountDownSeconds != 0)
        {
            _countdownValue = _basicToastNotificationData.CountDownSeconds ?? 60;

            kpbCountDown.Maximum = _countdownValue;

            kpbCountDown.Value = _countdownValue;

            UpdateProgressBarText();

            kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss}";

            _timer = new Timer();

            _timer.Interval = _basicToastNotificationData.CountDownTimerInterval ?? 1000;

            _timer.Tick += (sender, args) =>
            {
                _time++;

                kpbCountDown.Value -= 1;

                UpdateProgressBarText();

                kbtnDismiss.Text = $@"{KryptonManager.Strings.ToastNotificationStrings.Dismiss}";

                if (_time == _basicToastNotificationData.CountDownSeconds)
                {
                    _timer.Stop();

                    Close();
                }
            };
        }

        return base.ShowDialog();
    }

    internal static bool InternalShowWithBooleanReturnValue(KryptonBasicToastNotificationData toastNotificationData)
    {
        using var toast = new VisualToastNotificationBasicWithProgressBarForm(toastNotificationData);

        return toast.ShowDialog() == DialogResult.OK && toast.ReturnValue;
    }

    internal static CheckState InternalShowWithCheckStateReturnValue(KryptonBasicToastNotificationData toastNotificationData)
    {
        using var toast = new VisualToastNotificationBasicWithProgressBarForm(toastNotificationData);

        return toast.ShowDialog() == DialogResult.OK
            ? toast.ReturnCheckBoxStateValue
            : CheckState.Unchecked;
    }

    private void itbDismiss_Click(object sender, EventArgs e) => Close();

    internal static void InternalShow(KryptonBasicToastNotificationData toastNotificationData)
    {
        var kt = new VisualToastNotificationBasicWithProgressBarForm(toastNotificationData);

        kt.Show();
    }

    #endregion
}