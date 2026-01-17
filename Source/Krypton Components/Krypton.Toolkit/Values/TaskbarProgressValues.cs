#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

namespace Krypton.Toolkit;

/// <summary>
/// Storage for taskbar progress value information.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class TaskbarProgressValues : Storage
{
    #region Instance Fields
    private TaskbarProgressState _state;
    private ulong _value;
    private ulong _maximum;
    internal event Action? OnTaskbarProgressChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarProgressValues class.
    /// </summary>
    /// <param name="needPaint">Delegate for notifying paint requests.</param>
    public TaskbarProgressValues(NeedPaintHandler needPaint)
    {
        // Store the provided paint notification delegate
        NeedPaint = needPaint;

        Reset();
    }
    #endregion

    #region IsDefault
    /// <summary>
    /// Gets a value indicating if all values are default.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool IsDefault => (State == TaskbarProgressState.NoProgress) && (Value == 0) && (Maximum == 100);

    #endregion

    #region State
    /// <summary>
    /// Gets and sets the taskbar progress state.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Progress state to display on the taskbar button.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(TaskbarProgressState.NoProgress)]
    public TaskbarProgressState State
    {
        get => _state;

        set
        {
            if (_state != value)
            {
                _state = value;
                PerformNeedPaint(true);
                // Notify parent form to update taskbar progress
                OnTaskbarProgressChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeState() => State != TaskbarProgressState.NoProgress;

    /// <summary>
    /// Resets the State property to its default value.
    /// </summary>
    public void ResetState() => State = TaskbarProgressState.NoProgress;
    #endregion

    #region Value
    /// <summary>
    /// Gets and sets the current progress value.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Current progress value. Must be between 0 and Maximum.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0UL)]
    public ulong Value
    {
        get => _value;

        set
        {
            if (_value != value)
            {
                // Clamp value to valid range
                if (value > _maximum)
                {
                    value = _maximum;
                }

                _value = value;
                PerformNeedPaint(true);
                // Notify parent form to update taskbar progress
                OnTaskbarProgressChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeValue() => Value != 0;

    /// <summary>
    /// Resets the Value property to its default value.
    /// </summary>
    public void ResetValue() => Value = 0;
    #endregion

    #region Maximum
    /// <summary>
    /// Gets and sets the maximum progress value.
    /// </summary>
    [Localizable(true)]
    [Category(@"Visuals")]
    [Description(@"Maximum progress value. Value must be between 0 and this value.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(100UL)]
    public ulong Maximum
    {
        get => _maximum;

        set
        {
            if (_maximum != value)
            {
                _maximum = value;
                
                // Clamp current value to new maximum
                if (_value > _maximum)
                {
                    _value = _maximum;
                }

                PerformNeedPaint(true);
                // Notify parent form to update taskbar progress
                OnTaskbarProgressChanged?.Invoke();
            }
        }
    }

    private bool ShouldSerializeMaximum() => Maximum != 100;

    /// <summary>
    /// Resets the Maximum property to its default value.
    /// </summary>
    public void ResetMaximum() => Maximum = 100;
    #endregion

    #region CopyFrom
    /// <summary>
    /// Value copy from the provided source to ourself.
    /// </summary>
    /// <param name="source">Source instance.</param>
    public void CopyFrom(TaskbarProgressValues source)
    {
        State = source.State;
        Value = source.Value;
        Maximum = source.Maximum;
    }
    #endregion

    #region Reset
    /// <summary>
    /// Resets all values to their default.
    /// </summary>
    public void Reset()
    {
        ResetState();
        ResetValue();
        ResetMaximum();
    }
    #endregion
}
