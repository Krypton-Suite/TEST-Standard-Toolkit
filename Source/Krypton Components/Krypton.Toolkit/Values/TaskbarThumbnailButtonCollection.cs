#region BSD License
/*
 *  
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner (aka Wagnerp), Simon Coghlan (aka Smurf-IV), Giduac & Ahmed Abdelhameed et al. 2026 - 2026. All rights reserved.
 *  
 */
#endregion

using System.Linq;

namespace Krypton.Toolkit;

/// <summary>
/// Collection class for managing thumbnail buttons.
/// </summary>
public class TaskbarThumbnailButtonCollection : List<TaskbarThumbnailButton>
{
    #region Instance Fields
    private const int MaxButtons = 7;
    internal event Action? CollectionChanged;
    #endregion

    #region Identity
    /// <summary>
    /// Initialize a new instance of the TaskbarThumbnailButtonCollection class.
    /// </summary>
    public TaskbarThumbnailButtonCollection()
    {
    }
    #endregion

    #region Public
    /// <summary>
    /// Adds a button to the collection.
    /// </summary>
    /// <param name="button">Button to add.</param>
    /// <exception cref="InvalidOperationException">Thrown when collection already has 7 buttons (Windows limitation).</exception>
    public new void Add(TaskbarThumbnailButton button)
    {
        if (Count >= MaxButtons)
        {
            throw new InvalidOperationException($"Maximum {MaxButtons} buttons allowed in thumbnail toolbar (Windows limitation).");
        }

        base.Add(button);
        OnCollectionChanged();
    }

    /// <summary>
    /// Removes a button from the collection.
    /// </summary>
    /// <param name="button">Button to remove.</param>
    public new bool Remove(TaskbarThumbnailButton button)
    {
        bool result = base.Remove(button);
        if (result)
        {
            OnCollectionChanged();
        }
        return result;
    }

    /// <summary>
    /// Removes all buttons from the collection.
    /// </summary>
    public new void Clear()
    {
        base.Clear();
        OnCollectionChanged();
    }

    /// <summary>
    /// Finds a button by its ID.
    /// </summary>
    /// <param name="id">Button ID to find.</param>
    /// <returns>Button with the specified ID, or null if not found.</returns>
    public TaskbarThumbnailButton? FindById(uint id)
    {
        return this.FirstOrDefault(b => b.Id == id);
    }

    /// <summary>
    /// Raises the CollectionChanged event.
    /// </summary>
    protected virtual void OnCollectionChanged()
    {
        CollectionChanged?.Invoke();
    }
    #endregion
}
