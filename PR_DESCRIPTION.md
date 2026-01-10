# Fix badge transparent border color issue and add margin property (#2783)

## Description

Fixes an issue where badge borders with transparent color incorrectly render as visible colored borders during pulse animation, and adds a configurable `BadgeMargin` property for badge positioning.

## Problem

1. **Transparent Border Color Issue**: When using pulse animation on a badge with `RoundedRectangle` shape and `badgeBorderColor: Transparent` (with no bevel effect), the transparent border was incorrectly being rendered as a semi-transparent version of the badge color during animation. The opacity calculation was applying to transparent colors, creating an incorrect visible color instead of remaining transparent.

2. **Missing Margin/Padding Control**: The badge offset from the button edge was hardcoded as a constant (5 pixels), preventing customization of badge positioning per instance.

## Solution

### 1. Transparent Border Color Fix

- Added transparency check in `DrawBadgeBorder` method to skip drawing when border color alpha is 0
- Fixed opacity calculation to properly use the color's alpha channel: `Color.FromArgb((int)(opacity * borderColor.A), ...)` instead of hardcoded 255
- Added similar transparency check in `DrawBevelBorder` method for safety

### 2. Margin Property Addition

- Added `BadgeMargin` property to `BadgeContentValues` class with default value of 5 pixels (maintaining backward compatibility)
- Updated `ViewDrawBadge.CalculateBadgeLocation` to use `_badgeValues.BadgeContentValues.BadgeMargin` instead of hardcoded `BADGE_OFFSET` constant
- Removed unused `BADGE_OFFSET` constant

## Changes Made

### Core Fixes
- **`ViewDrawBadge.cs`**: 
  - Modified `DrawBadgeBorder` method to check for transparent colors (A=0) and skip drawing
  - Fixed opacity calculation to respect color's alpha channel
  - Modified `DrawBevelBorder` method with transparency check
  - Updated `CalculateBadgeLocation` to use configurable margin property
  - Removed unused `BADGE_OFFSET` constant

### New Feature
- **`BadgeContentValues.cs`**: 
  - Added `DEFAULT_BADGE_MARGIN` constant (5 pixels)
  - Added `_badgeMargin` instance field
  - Added `BadgeMargin` property with validation (minimum 0)
  - Updated `IsDefault` check to include badge margin
  - Added `ShouldSerializeBadgeMargin` and `ResetBadgeMargin` methods

## Usage

### Setting Custom Margin
```csharp
// Set custom margin/offset from button edge
button.BadgeValues.BadgeContentValues.BadgeMargin = 5;

// Reset to default (5 pixels)
button.BadgeValues.BadgeContentValues.ResetBadgeMargin();
```

## Testing

The fix has been tested with:
- ✅ Badge with transparent border color and pulse animation (RoundedRectangle shape)
- ✅ Badge with transparent border color and pulse animation (Circle and Square shapes)
- ✅ Badge with transparent border color and bevel effects
- ✅ Badge with transparent border color and fade animation
- ✅ Badge with non-transparent border colors (verifying no regression)
- ✅ BadgeMargin property with various values (0, 3, 5, 10 pixels)
- ✅ Multiple badges with different margin values on the same form

## Behavior

- **Transparent border with animation**: Border remains transparent throughout animation cycle (fix verified)
- **Non-transparent border with animation**: Border opacity animates correctly as before
- **BadgeMargin property**: Default value (5) maintains backward compatibility, custom values allow fine-tuning badge position

## Breaking Changes

None. This is a bug fix and feature addition that maintains backward compatibility:
- The transparent border fix only affects cases where borders were incorrectly visible
- The badge margin property defaults to 5 pixels (same as previous hardcoded value)
- Existing code continues to work without modification
