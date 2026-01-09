#region BSD License
/*
 * 
 * Original BSD 3-Clause License (https://github.com/ComponentFactory/Krypton/blob/master/LICENSE)
 *  © Component Factory Pty Ltd, 2006 - 2016, All rights reserved.
 * 
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2017 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Ribbon;

/// <summary>
/// Draws the notification bar below the ribbon groups.
/// </summary>
internal class ViewDrawRibbonNotificationBar : ViewComposite
{
	#region Type Definitions
	/// <summary>
	/// Represents a clickable button area within the notification bar.
	/// </summary>
	private class ButtonArea
	{
		public Rectangle Rectangle { get; set; }
		public int Index { get; set; }
		public bool IsCloseButton { get; set; }
	}
	#endregion

	#region Instance Fields
	private readonly KryptonRibbon _ribbon;
	private readonly NeedPaintHandler _needPaint;
	private readonly int _iconSize;
	private readonly int _closeButtonSize;
	private readonly int _buttonSpacing;
	private readonly int _buttonPadding;
	private KryptonRibbonNotificationBarData? _notificationData;
	private readonly List<ButtonArea> _buttonAreas;
	private int _hoveredButtonIndex;
	private bool _closeButtonHovered;
	private IDisposable? _mementoBack;
	private ViewLayoutDocker? _layoutDocker;
	private ViewDrawContent? _iconContent;
	private ViewDrawContent? _textContent;
	private ViewLayoutStack? _buttonStack;
	#endregion

	#region Events
	/// <summary>
	/// Occurs when an action button or close button is clicked.
	/// </summary>
	public event EventHandler<RibbonNotificationBarEventArgs>? ButtonClick;
	#endregion

	#region Identity
	/// <summary>
	/// Initialize a new instance of the ViewDrawRibbonNotificationBar class.
	/// </summary>
	/// <param name="ribbon">Reference to owning ribbon control.</param>
	/// <param name="needPaintDelegate">Delegate for notifying paint/layout changes.</param>
	public ViewDrawRibbonNotificationBar([DisallowNull] KryptonRibbon ribbon,
		[DisallowNull] NeedPaintHandler needPaintDelegate)
	{
		Debug.Assert(ribbon != null);
		Debug.Assert(needPaintDelegate != null);

		_ribbon = ribbon!;
		_needPaint = needPaintDelegate!;
		_iconSize = (int)(24 * FactorDpiX);
		_closeButtonSize = (int)(20 * FactorDpiX);
		_buttonSpacing = (int)(8 * FactorDpiX);
		_buttonPadding = (int)(6 * FactorDpiX);
		_buttonAreas = new List<ButtonArea>();
		_hoveredButtonIndex = -1;
		_closeButtonHovered = false;

		// Create layout structure
		_layoutDocker = new ViewLayoutDocker();
		_buttonStack = new ViewLayoutStack(false);

		// Attach mouse controller for button interactions
		var controller = new NotificationBarController(this, needPaintDelegate);
		controller.Click += OnControllerClick;
		MouseController = controller;
		SourceController = controller;

		Add(_layoutDocker);
	}

	/// <summary>
	/// Obtains the String representation of this instance.
	/// </summary>
	/// <returns>User readable name of the instance.</returns>
	public override string ToString() =>
		$@"ViewDrawRibbonNotificationBar:{Id}";

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (_mementoBack != null)
			{
				_mementoBack.Dispose();
				_mementoBack = null;
			}

			if (_notificationData != null)
			{
				_notificationData.PropertyChanged -= OnNotificationDataPropertyChanged;
				_notificationData = null;
			}
		}

		base.Dispose(disposing);
	}
	#endregion

	#region Public
	/// <summary>
	/// Gets and sets the notification bar data.
	/// </summary>
	public KryptonRibbonNotificationBarData? NotificationData
	{
		get => _notificationData;
		set
		{
			if (_notificationData != value)
			{
				if (_notificationData != null)
				{
					_notificationData.PropertyChanged -= OnNotificationDataPropertyChanged;
				}

				_notificationData = value;

				if (_notificationData != null)
				{
					_notificationData.PropertyChanged += OnNotificationDataPropertyChanged;
				}

				UpdateLayout();
				_needPaint(this, new NeedLayoutEventArgs(false));
			}
		}
	}

	/// <summary>
	/// Gets and sets the visible state of the element.
	/// </summary>
	public override bool Visible
	{
		get => _ribbon.Visible && base.Visible && (_notificationData?.Visible ?? false);
		set => base.Visible = value;
	}
	#endregion

	#region Layout
	/// <summary>
	/// Discover the preferred size of the element.
	/// </summary>
	/// <param name="context">Layout context.</param>
	public override Size GetPreferredSize(ViewLayoutContext context)
	{
		if (_notificationData == null || !_notificationData.Visible)
		{
			return Size.Empty;
		}

		// Calculate preferred height
		int preferredHeight = _notificationData.Height > 0 
			? _notificationData.Height 
			: Math.Max((int)(40 * FactorDpiY), CalculateContentHeight());

		// Width fills available space
		return new Size(context.DisplayRectangle.Width, preferredHeight);
	}

	/// <summary>
	/// Perform a layout of the elements.
	/// </summary>
	/// <param name="context">Layout context.</param>
	public override void Layout([DisallowNull] ViewLayoutContext context)
	{
		Debug.Assert(context != null);

		if (_notificationData == null || !_notificationData.Visible || _layoutDocker == null)
		{
			ClientRectangle = Rectangle.Empty;
			return;
		}

		Rectangle clientRect = context!.DisplayRectangle;
		ClientRectangle = clientRect;

		// Update layout structure
		UpdateLayout();

		// Layout the docker
		context.DisplayRectangle = ClientRectangle;
		_layoutDocker.Layout(context);

		// Calculate button areas for hit testing
		CalculateButtonAreas();

		context.DisplayRectangle = ClientRectangle;
	}
	#endregion

	#region Paint
	/// <summary>
	/// Perform rendering before child elements are rendered.
	/// </summary>
	/// <param name="context">Rendering context.</param>
	public override void RenderBefore(RenderContext context)
	{
		if (_notificationData == null || !_notificationData.Visible)
		{
			return;
		}

		// Draw background
		DrawBackground(context);

		// Draw border
		DrawBorder(context);
	}

	/// <summary>
	/// Perform rendering after child elements are rendered.
	/// </summary>
	/// <param name="context">Rendering context.</param>
	public override void RenderAfter(RenderContext context)
	{
		if (_notificationData == null || !_notificationData.Visible)
		{
			return;
		}

		// Draw action buttons
		if (_notificationData.ShowActionButtons && _notificationData.ActionButtonTexts != null)
		{
			DrawActionButtons(context);
		}

		// Draw close button
		if (_notificationData.ShowCloseButton)
		{
			DrawCloseButton(context);
		}
	}
	#endregion

	#region Implementation
	private void UpdateLayout()
	{
		if (_layoutDocker == null || _notificationData == null)
		{
			return;
		}

		// Clear existing children
		_layoutDocker.Clear();

		// Create icon view if needed
		if (_notificationData.ShowIcon && _notificationData.Icon != null)
		{
			if (_iconContent == null)
			{
				var iconProvider = new NotificationIconContent(_notificationData);
				_iconContent = new ViewDrawContent(iconProvider, this, VisualOrientation.Top);
			}
			_layoutDocker.Add(_iconContent, ViewDockStyle.Left);
		}
		else
		{
			_iconContent = null;
		}

		// Create text view
		if (_textContent == null)
		{
			var textProvider = new NotificationTextContent(_notificationData);
			_textContent = new ViewDrawContent(textProvider, this, VisualOrientation.Top);
		}
		_layoutDocker.Add(_textContent, ViewDockStyle.Fill);

		// Create button stack if needed
		if (_notificationData.ShowActionButtons && _notificationData.ActionButtonTexts != null && _notificationData.ActionButtonTexts.Length > 0)
		{
			if (_buttonStack == null)
			{
				_buttonStack = new ViewLayoutStack(false);
			}
			_buttonStack.Clear();

			// Add button views (we'll draw them manually in RenderAfter for better control)
			_layoutDocker.Add(_buttonStack, ViewDockStyle.Right);
		}
		else
		{
			_buttonStack = null;
		}
	}

	private int CalculateContentHeight()
	{
		if (_notificationData == null)
		{
			return 0;
		}

		int height = _notificationData.Padding.Vertical;
		int textHeight = (int)(16 * FactorDpiY); // Default text height
		height += Math.Max(textHeight, _iconSize);
		return height;
	}

	private void CalculateButtonAreas()
	{
		_buttonAreas.Clear();

		if (_notificationData == null || !_notificationData.Visible)
		{
			return;
		}

		Rectangle clientRect = ClientRectangle;
		int rightEdge = clientRect.Right - _notificationData.Padding.Right;

		// Calculate close button area
		if (_notificationData.ShowCloseButton)
		{
			int closeY = clientRect.Top + _notificationData.Padding.Top + (clientRect.Height - _notificationData.Padding.Vertical - _closeButtonSize) / 2;
			var closeRect = new Rectangle(
				rightEdge - _closeButtonSize,
				closeY,
				_closeButtonSize,
				_closeButtonSize);
			_buttonAreas.Add(new ButtonArea { Rectangle = closeRect, Index = -1, IsCloseButton = true });
			rightEdge -= _closeButtonSize + _buttonSpacing;
		}

		// Calculate action button areas
		if (_notificationData.ShowActionButtons && _notificationData.ActionButtonTexts != null)
		{
			for (int i = _notificationData.ActionButtonTexts.Length - 1; i >= 0; i--)
			{
				string buttonText = _notificationData.ActionButtonTexts[i];
				Size buttonSize = MeasureButton(buttonText);
				int buttonY = clientRect.Top + _notificationData.Padding.Top + (clientRect.Height - _notificationData.Padding.Vertical - buttonSize.Height) / 2;
				var buttonRect = new Rectangle(
					rightEdge - buttonSize.Width,
					buttonY,
					buttonSize.Width,
					buttonSize.Height);
				_buttonAreas.Add(new ButtonArea { Rectangle = buttonRect, Index = i, IsCloseButton = false });
				rightEdge -= buttonSize.Width + _buttonSpacing;
			}
		}
	}

	private Size MeasureButton(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return new Size((int)(60 * FactorDpiX), (int)(24 * FactorDpiY));
		}

		using var g = Graphics.FromHwnd(IntPtr.Zero);
		SizeF textSize = g.MeasureString(text, SystemFonts.DefaultFont);
		return new Size(
			(int)textSize.Width + _buttonPadding * 2,
			Math.Max((int)(24 * FactorDpiY), (int)textSize.Height + _buttonPadding * 2));
	}

	private void DrawBackground(RenderContext context)
	{
		if (_notificationData == null)
		{
			return;
		}

		Color backColor = GetBackColor();
		using var brush = new SolidBrush(backColor);
		context.Graphics.FillRectangle(brush, ClientRectangle);
	}

	private void DrawBorder(RenderContext context)
	{
		if (_notificationData == null)
		{
			return;
		}

		Color borderColor = GetBorderColor();
		using var pen = new Pen(borderColor, 1);
		context.Graphics.DrawLine(pen, ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Right, ClientRectangle.Top);
	}

	private void DrawActionButtons(RenderContext context)
	{
		if (_notificationData == null || _notificationData.ActionButtonTexts == null)
		{
			return;
		}

		foreach (var buttonArea in _buttonAreas.Where(ba => !ba.IsCloseButton))
		{
			DrawButton(context, buttonArea);
		}
	}

	private void DrawButton(RenderContext context, ButtonArea buttonArea)
	{
		Rectangle rect = buttonArea.Rectangle;
		bool isHovered = buttonArea.IsCloseButton ? _closeButtonHovered : (_hoveredButtonIndex == buttonArea.Index);

		// Draw button background
		Color backColor = isHovered 
			? Color.FromArgb(220, 220, 220) 
			: Color.FromArgb(240, 240, 240);
		using var brush = new SolidBrush(backColor);
		context.Graphics.FillRectangle(brush, rect);

		// Draw button border
		using var pen = new Pen(isHovered ? Color.FromArgb(180, 180, 180) : Color.FromArgb(200, 200, 200));
		context.Graphics.DrawRectangle(pen, rect);

		// Draw button text
		if (!buttonArea.IsCloseButton && _notificationData != null && _notificationData.ActionButtonTexts != null && buttonArea.Index >= 0 && buttonArea.Index < _notificationData.ActionButtonTexts.Length)
		{
			string text = _notificationData.ActionButtonTexts[buttonArea.Index];
			Color textColor = _notificationData.CustomForeColor;
			using var textBrush = new SolidBrush(textColor);
			using var format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};
			context.Graphics.DrawString(text, SystemFonts.DefaultFont, textBrush, rect, format);
		}
	}

	private void DrawCloseButton(RenderContext context)
	{
		var closeButtonArea = _buttonAreas.FirstOrDefault(ba => ba.IsCloseButton);
		if (closeButtonArea == null)
		{
			return;
		}

		Rectangle rect = closeButtonArea.Rectangle;
		bool isHovered = _closeButtonHovered;

		// Draw button background
		Color backColor = isHovered 
			? Color.FromArgb(220, 220, 220) 
			: Color.Transparent;
		if (backColor != Color.Transparent)
		{
			using var brush = new SolidBrush(backColor);
			context.Graphics.FillRectangle(brush, rect);
		}

		// Draw X symbol
		Color xColor = isHovered ? Color.Black : Color.FromArgb(120, 120, 120);
		using var pen = new Pen(xColor, 1.5f);
		int margin = rect.Width / 4;
		context.Graphics.DrawLine(pen, rect.Left + margin, rect.Top + margin, rect.Right - margin, rect.Bottom - margin);
		context.Graphics.DrawLine(pen, rect.Left + margin, rect.Bottom - margin, rect.Right - margin, rect.Top + margin);
	}

	private Color GetBackColor()
	{
		if (_notificationData == null)
		{
			return Color.White;
		}

		return _notificationData.Type switch
		{
			RibbonNotificationBarType.Information => Color.FromArgb(217, 236, 255),
			RibbonNotificationBarType.Warning => Color.FromArgb(255, 242, 204),
			RibbonNotificationBarType.Error => Color.FromArgb(255, 204, 204),
			RibbonNotificationBarType.Success => Color.FromArgb(204, 255, 204),
			RibbonNotificationBarType.Custom => _notificationData.CustomBackColor,
			_ => Color.White
		};
	}

	private Color GetForeColor()
	{
		if (_notificationData == null)
		{
			return Color.Black;
		}

		return _notificationData.Type == RibbonNotificationBarType.Custom 
			? _notificationData.CustomForeColor 
			: Color.Black;
	}

	private Color GetBorderColor()
	{
		if (_notificationData == null)
		{
			return Color.FromArgb(200, 200, 200);
		}

		return _notificationData.Type switch
		{
			RibbonNotificationBarType.Information => Color.FromArgb(91, 155, 213),
			RibbonNotificationBarType.Warning => Color.FromArgb(255, 192, 0),
			RibbonNotificationBarType.Error => Color.FromArgb(192, 0, 0),
			RibbonNotificationBarType.Success => Color.FromArgb(0, 192, 0),
			RibbonNotificationBarType.Custom => _notificationData.CustomBorderColor,
			_ => Color.FromArgb(200, 200, 200)
		};
	}

	private void OnNotificationDataPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		UpdateLayout();
		_needPaint(this, new NeedLayoutEventArgs(false));
	}

	private void OnControllerClick(object? sender, MouseEventArgs e)
	{
		Point clickPoint = e.Location;
		var clickedButton = _buttonAreas.FirstOrDefault(ba => ba.Rectangle.Contains(clickPoint));

		if (clickedButton != null)
		{
			ButtonClick?.Invoke(this, new RibbonNotificationBarEventArgs(clickedButton.IsCloseButton ? -1 : clickedButton.Index));
		}
	}

	/// <summary>
	/// Updates the hover state for buttons.
	/// </summary>
	internal void UpdateHoverState(Point mouseLocation)
	{
		bool changed = false;
		int newHoveredIndex = -1;
		bool newCloseHovered = false;

		foreach (var buttonArea in _buttonAreas)
		{
			if (buttonArea.Rectangle.Contains(mouseLocation))
			{
				if (buttonArea.IsCloseButton)
				{
					newCloseHovered = true;
				}
				else
				{
					newHoveredIndex = buttonArea.Index;
				}
				break;
			}
		}

		if (newHoveredIndex != _hoveredButtonIndex || newCloseHovered != _closeButtonHovered)
		{
			_hoveredButtonIndex = newHoveredIndex;
			_closeButtonHovered = newCloseHovered;
			_needPaint(this, new NeedLayoutEventArgs(false));
		}
	}

	/// <summary>
	/// Clears the hover state.
	/// </summary>
	internal void ClearHoverState()
	{
		if (_hoveredButtonIndex != -1 || _closeButtonHovered)
		{
			_hoveredButtonIndex = -1;
			_closeButtonHovered = false;
			_needPaint(this, new NeedLayoutEventArgs(false));
		}
	}
	#endregion

	#region Content Providers
	/// <summary>
	/// Provides icon content for the notification bar.
	/// </summary>
	private class NotificationIconContent : IContentValues
	{
		private readonly KryptonRibbonNotificationBarData _data;

		public NotificationIconContent(KryptonRibbonNotificationBarData data)
		{
			_data = data;
		}

		public bool HasContent => _data.ShowIcon && _data.Icon != null;

		public Image? GetImage(PaletteState state) => _data.Icon;

		public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

		public string GetShortText() => string.Empty;

		public string GetLongText() => string.Empty;
	}

	/// <summary>
	/// Provides text content for the notification bar.
	/// </summary>
	private class NotificationTextContent : IContentValues
	{
		private readonly KryptonRibbonNotificationBarData _data;

		public NotificationTextContent(KryptonRibbonNotificationBarData data)
		{
			_data = data;
		}

		public bool HasContent => !string.IsNullOrEmpty(_data.Text) || !string.IsNullOrEmpty(_data.Title);

		public Image? GetImage(PaletteState state) => null;

		public Color GetImageTransparentColor(PaletteState state) => Color.Empty;

		public string GetShortText()
		{
			if (!string.IsNullOrEmpty(_data.Title))
			{
				return _data.Title + " " + (_data.Text ?? string.Empty);
			}
			return _data.Text ?? string.Empty;
		}

		public string GetLongText() => GetShortText();
	}
	#endregion
}

/// <summary>
/// Controller for handling mouse interactions with the notification bar.
/// </summary>
internal class NotificationBarController : MouseController
{
	private readonly ViewDrawRibbonNotificationBar _notificationBar;

	public NotificationBarController(ViewDrawRibbonNotificationBar notificationBar, NeedPaintHandler needPaint)
		: base(null, needPaint)
	{
		_notificationBar = notificationBar;
	}

	public override void MouseMove(Point pt)
	{
		base.MouseMove(pt);
		_notificationBar.UpdateHoverState(pt);
	}

	public override void MouseLeave(Point pt)
	{
		base.MouseLeave(pt);
		_notificationBar.ClearHoverState();
	}
}

