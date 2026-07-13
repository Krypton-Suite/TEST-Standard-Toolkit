## Summary

Adds an automated GitHub Actions workflow to keep Krypton license header end years current across the repository. The workflow checks out `alpha`, updates `Modifications by Peter Wagner` year ranges where the end year is behind the calendar year, and opens a pull request back into `alpha`.

## Changes

- **`.github/workflows/update-license-year.yml`** — new workflow
  - Scheduled: 1 January at 06:00 UTC
  - Manual: `workflow_dispatch` with optional **dry run** input
  - Creates PR on branch `automation/license-year-YYYY` targeting `alpha`
  - Skips PR creation when no files need updating
- **`.github/scripts/Update-LicenseYear.ps1`** — PowerShell script that:
  - Scans `*.cs`, `*.licenseheader`, `*.yml`, `*.yaml`, `*.md`, and `.editorconfig`
  - Matches lines containing `Modifications by Peter Wagner` with a `YYYY - YYYY` range
  - Bumps only the **end year** when it is less than the target year (e.g. `2025 - 2025` → `2025 - 2026`)
  - Leaves unrelated headers unchanged (e.g. Component Factory `2006 - 2016`, JDH `2013-2021`)
  - Preserves UTF-8 BOM and existing line endings (CRLF/LF)
  - Supports `-DryRun` to preview changes without writing files

## Example behaviour

| Current header | Target year | Result |
| --- | --- | --- |
| `2017 - 2025` | 2026 | `2017 - 2026` |
| `2025 - 2025` | 2026 | `2025 - 2026` |
| `2017 - 2026` | 2026 | unchanged |
| `2006 - 2016` (Component Factory) | 2026 | unchanged |

## Test plan

- [ ] Merge this PR into `alpha`
- [ ] Run **Actions → Update License Year → Run workflow** with **dry run** enabled
  - [ ] Confirm job summary reports the expected file count
  - [ ] Confirm log lines show `Would update: ...` and no files are committed
  - [ ] Confirm no PR is created
- [ ] Run the workflow again with **dry run** disabled
  - [ ] Confirm an `automation/license-year-YYYY` PR is opened against `alpha`
  - [ ] Spot-check updated `.licenseheader`, `.editorconfig`, and `.cs` files for correct year and preserved formatting
- [ ] Re-run the workflow (dry run or normal) when all headers are current
  - [ ] Confirm the job reports no changes and does not open a duplicate PR
- [ ] (Optional) Run locally:
  ```powershell
  pwsh -File .github/scripts/Update-LicenseYear.ps1 -Year 2026 -DryRun
  ```

## Notes

- The scheduled run on 1 January performs a real update and opens a PR; it does not use dry run.
- `GlobalStaticValues.cs` already uses `DateTime.Now.Year` for embedded license text and is unaffected by this workflow.
- First automated run after merge is expected to update remaining template files (e.g. `.licenseheader` files still on older end years).

## See also

- [Developer documentation](../Documents/Automation/Update-License-Year.md) — architecture, matching rules, runbook, and troubleshooting
