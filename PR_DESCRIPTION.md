# Fix KryptonRichTextBox formatting preservation when palette changes (#2832)

## Description

Fixes issue where `KryptonRichTextBox` loses its RTF formatting (bold, italic, underline, colors, fonts) when the palette or `InputControlStyle` is changed.

## Problem

When the palette or `InputControlStyle` property changed, the `OnNeedPaint` method would update the `Font` property of the underlying `RichTextBox` control. Setting the `Font` property on a `RichTextBox` in Windows Forms resets all RTF formatting, causing users to lose their formatted text.

Additionally, setting `BackColor` or `ForeColor` properties could also reset RTF formatting in some scenarios.

## Solution

The fix implements a comprehensive RTF formatting detection and preservation mechanism:

1. **Early RTF Detection**: Before any property changes, the RTF content is saved and analyzed for formatting codes
2. **Formatting Detection**: Detects RTF formatting through multiple indicators:
   - Explicit formatting codes (`\b`, `\i`, `\ul`, `\fs`, `\cf`, `\highlight`)
   - Custom font references (beyond default `\f0`)
   - RTF length comparison (formatted RTF is significantly longer than plain text)
3. **Conditional Font Setting**: Only sets the `Font` property if no RTF formatting is detected (plain text mode)
4. **RTF Restoration**: After property updates, if formatting was detected and the RTF was modified, it is restored to preserve formatting

## Changes Made

### Core Fix
- **`KryptonRichTextBox.cs`**: Modified `OnNeedPaint` method to:
  - Save RTF content before any property changes
  - Detect RTF formatting using multiple heuristics
  - Skip setting `Font` property when formatting is detected
  - Restore RTF if it was modified during updates

### Test Form
- **`RichTextBoxFormattingTest.cs`**: Comprehensive test form demonstrating the fix
  - Pre-loaded RTF content with various formatting (bold, italic, underline, colors, fonts)
  - Palette selection combo box (40+ palette options)
  - InputControlStyle selection combo box (7 style options)
  - Buttons to load sample RTF, plain text, verify formatting, and clear
  - Status messages and instructions

- **`RichTextBoxFormattingTest.Designer.cs`**: Designer file for the test form

- **`StartScreen.cs`**: Added menu entry for the new test form

## Testing

The fix has been tested with:
- ✅ Multiple palette changes (Office 2010, Office 2013, Office 365, Sparkle, Professional variants)
- ✅ InputControlStyle changes (Standalone, Ribbon, Custom1-3, PanelClient, PanelAlternate)
- ✅ RTF content with bold, italic, underline, colors, and custom fonts
- ✅ Plain text (which correctly uses palette font)
- ✅ Mixed formatting scenarios

## Behavior

- **Plain text**: Correctly uses palette font when palette/style changes (as expected)
- **RTF formatted text**: Formatting is preserved when palette/style changes (fix verified)

## Breaking Changes

None. This is a bug fix that maintains backward compatibility. Plain text behavior is unchanged, and RTF formatting is now preserved as expected.

## Related Issue

Fixes #2832
