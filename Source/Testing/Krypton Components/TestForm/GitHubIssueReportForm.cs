#region BSD License
/*
 *
 *  New BSD 3-Clause License (https://github.com/Krypton-Suite/Standard-Toolkit/blob/master/LICENSE)
 *  Modifications by Peter Wagner(aka Wagnerp), Simon Coghlan(aka Smurf-IV), et al. 2025 - 2026. All rights reserved.
 *
 */
#endregion

using System.Diagnostics;
using Krypton.Toolkit;

namespace TestForm;

/// <summary>
/// Form for creating a bug report issue on the repository's GitHub issue tracker.
/// Fields match .github/ISSUE_TEMPLATE/bug_report.yml.
/// </summary>
public partial class GitHubIssueReportForm : KryptonForm
{
    private readonly BugReportGitHubService _githubService = new BugReportGitHubService();
    private readonly KryptonErrorProvider _errorProvider;

    public GitHubIssueReportForm()
    {
        InitializeComponent();
        _errorProvider = new KryptonErrorProvider
        {
            ContainerControl = this,
            BlinkStyle = KryptonErrorBlinkStyle.BlinkIfDifferentError
        };
        LoadDefaults();
    }

    private void LoadDefaults()
    {
        ktbOwner.Text = "Krypton-Suite";
        ktbRepo.Text = "Standard-Toolkit";
        ktbToken.Text = string.Empty;
        kcmbAreasAffected.SelectedIndex = -1;

        // Pre-fill environment info when available
        try
        {
            if (string.IsNullOrWhiteSpace(ktbOs.Text))
            {
                ktbOs.Text = "Windows";
            }

            if (string.IsNullOrWhiteSpace(ktbOsVersion.Text))
            {
                ktbOsVersion.Text = Environment.OSVersion.Version.ToString();
            }

            if (string.IsNullOrWhiteSpace(ktbFrameworkVersion.Text))
            {
                var ver = Environment.Version;
                ktbFrameworkVersion.Text = $"{ver.Major}.{ver.Minor}";
            }
        }
        catch
        {
            // Ignore
        }
    }

    private BugReportGitHubConfig GetConfig()
    {
        return new BugReportGitHubConfig
        {
            Owner = ktbOwner.Text?.Trim() ?? string.Empty,
            RepositoryName = ktbRepo.Text?.Trim() ?? string.Empty,
            PersonalAccessToken = ktbToken.Text?.Trim() ?? string.Empty
        };
    }

    private BugReportGitHubContent GetContent()
    {
        return new BugReportGitHubContent
        {
            Summary = ktbSummary.Text?.Trim() ?? string.Empty,
            Description = krtbDescription.Text?.Trim() ?? string.Empty,
            StepsToReproduce = krtbStepsToReproduce.Text?.Trim() ?? string.Empty,
            ExpectedBehavior = krtbExpectedBehavior.Text?.Trim() ?? string.Empty,
            ActualBehavior = krtbActualBehavior.Text?.Trim() ?? string.Empty,
            OperatingSystem = ktbOs.Text?.Trim() ?? string.Empty,
            OsVersion = ktbOsVersion.Text?.Trim() ?? string.Empty,
            FrameworkVersion = ktbFrameworkVersion.Text?.Trim() ?? string.Empty,
            ToolkitVersion = ktbToolkitVersion.Text?.Trim() ?? string.Empty,
            AdditionalInformation = krtbAdditionalInfo.Text?.Trim() ?? string.Empty,
            AreasAffected = kcmbAreasAffected.SelectedItem?.ToString() ?? string.Empty
        };
    }

    private bool ValidateInput()
    {
        _errorProvider.Clear();

        var valid = true;

        if (string.IsNullOrWhiteSpace(ktbSummary.Text))
        {
            _errorProvider.SetError(ktbSummary, "Summary is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbDescription.Text))
        {
            _errorProvider.SetError(krtbDescription, "Description is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbStepsToReproduce.Text))
        {
            _errorProvider.SetError(krtbStepsToReproduce, "Steps to reproduce are required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbExpectedBehavior.Text))
        {
            _errorProvider.SetError(krtbExpectedBehavior, "Expected behavior is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(krtbActualBehavior.Text))
        {
            _errorProvider.SetError(krtbActualBehavior, "Actual behavior is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(ktbToken.Text))
        {
            _errorProvider.SetError(ktbToken, "GitHub Personal Access Token is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(ktbOwner.Text))
        {
            _errorProvider.SetError(ktbOwner, "Repository owner is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(ktbRepo.Text))
        {
            _errorProvider.SetError(ktbRepo, "Repository name is required.");
            valid = false;
        }

        return valid;
    }

    private void kbtnCreate_Click(object sender, EventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        var config = GetConfig();
        var content = GetContent();

        kbtnCreate.Enabled = false;
        kbtnCreate.Values.Text = "Creating...";
        Application.DoEvents();

        try
        {
            var result = _githubService.CreateIssue(config, content);

            if (result.Success)
            {
                KryptonMessageBox.Show(
                    "Bug report created successfully.",
                    "Success",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Information);

                if (!string.IsNullOrWhiteSpace(result.IssueUrl))
                {
                    var open = KryptonMessageBox.Show(
                        "Open the issue in your browser?",
                        "Open Issue",
                        KryptonMessageBoxButtons.YesNo,
                        KryptonMessageBoxIcon.Question);

                    if (open == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = result.IssueUrl,
                            UseShellExecute = true
                        });
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                KryptonMessageBox.Show(
                    result.ErrorMessage ?? "Failed to create issue.",
                    "Create Issue Failed",
                    KryptonMessageBoxButtons.OK,
                    KryptonMessageBoxIcon.Error);
            }
        }
        finally
        {
            kbtnCreate.Enabled = true;
            kbtnCreate.Values.Text = "Create on GitHub";
        }
    }

    private void kbtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _errorProvider?.Clear();
        _errorProvider?.Dispose();
        base.OnFormClosed(e);
    }
}
