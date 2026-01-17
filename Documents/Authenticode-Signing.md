# Authenticode Signing Documentation

## Table of Contents

1. [Overview](#overview)
2. [What is Authenticode Signing?](#what-is-authenticode-signing)
3. [How It Works](#how-it-works)
4. [MSBuild Configuration](#msbuild-configuration)
5. [Local Development](#local-development)
6. [GitHub Actions Integration](#github-actions-integration)
7. [Certificate Management](#certificate-management)
8. [Troubleshooting](#troubleshooting)
9. [Best Practices](#best-practices)
10. [Examples](#examples)

---

## Overview

The Krypton Standard Toolkit build system includes integrated support for **Authenticode signing** of compiled binaries. This feature automatically signs DLL and EXE files after compilation to provide an extra layer of security and trust for end users on Windows.

### Key Features

- ✅ **Automatic signing** during build process
- ✅ **Optional configuration** - builds work without certificates
- ✅ **Multi-targeting support** - signs binaries for each target framework
- ✅ **Timestamp support** - signatures remain valid after certificate expiration
- ✅ **GitHub Actions integration** - seamless CI/CD signing
- ✅ **Flexible certificate sources** - supports PFX files and certificate store

### Current Status

✅ **Fully Implemented** - Ready for use in all release configurations

---

## What is Authenticode Signing?

**Authenticode** is Microsoft's code signing technology that uses X.509 digital certificates to sign Portable Executable (PE) files (.dll, .exe, etc.). It provides:

- **Publisher Identity**: Verifies who published the software
- **Integrity Verification**: Confirms the file hasn't been tampered with
- **Trust Establishment**: Reduces security warnings for end users
- **Compliance**: Required for some enterprise deployments and Windows features

### Difference from Strong-Name Signing

The Krypton Toolkit already uses **strong-name signing** (via `StrongKrypton.snk`), but this is different from Authenticode:

| Feature | Strong-Name Signing | Authenticode Signing |
|---------|-------------------|---------------------|
| **Purpose** | .NET assembly identity | Binary trust/security |
| **Certificate** | Public/private key pair (.snk) | X.509 certificate (.pfx) |
| **Scope** | .NET assemblies only | All PE files (.dll, .exe) |
| **Trust** | Framework-level | OS-level |
| **Windows Integration** | No | Yes (UAC, SmartScreen) |

Both signing methods complement each other and provide comprehensive security.

---

## How It Works

### Build Process Flow

```
1. Compile Project → Generate DLL/EXE
2. Build Target Completes → AuthenticodeSign Target Triggers
3. Check Configuration → Is signing enabled?
4. Locate SignTool.exe → Auto-detect from Windows SDK
5. Prepare Certificate → Load from file or certificate store
6. Sign Binary → Apply Authenticode signature
7. Add Timestamp → Request RFC 3161 timestamp
8. Verify Signature → Confirm signing succeeded
```

### Implementation Details

The signing is implemented as an MSBuild target in `Source/Krypton Components/Directory.Build.targets`:

```xml
<Target Name="AuthenticodeSign" AfterTargets="Build" 
        Condition="'$(EnableAuthenticodeSigning)' == 'true' And ('$(AuthenticodeCertificatePath)' != '' Or '$(AuthenticodeCertificateThumbprint)' != '')">
  <!-- Signing logic -->
</Target>
```

The target runs automatically after each build, checking for DLL and EXE files in the output directory.

---

## MSBuild Configuration

### Required Properties

#### `EnableAuthenticodeSigning`

**Type**: `Boolean`  
**Default**: `false` (except `Release` and `Installer` configurations default to `false` but can be enabled)  
**Description**: Master switch to enable/disable Authenticode signing

```xml
<PropertyGroup>
  <EnableAuthenticodeSigning>true</EnableAuthenticodeSigning>
</PropertyGroup>
```

#### `AuthenticodeCertificatePath`

**Type**: `String`  
**Default**: Empty  
**Description**: Path to the code signing certificate file (.pfx)

```xml
<PropertyGroup>
  <AuthenticodeCertificatePath>C:\Certificates\MyCert.pfx</AuthenticodeCertificatePath>
</PropertyGroup>
```

#### `AuthenticodeCertificatePassword`

**Type**: `String`  
**Default**: Empty  
**Description**: Password for the PFX certificate file (if password-protected)

```xml
<PropertyGroup>
  <AuthenticodeCertificatePassword>MyPassword123</AuthenticodeCertificatePassword>
</PropertyGroup>
```

**⚠️ Security Warning**: Never hardcode passwords in project files. Use environment variables or MSBuild properties passed at build time.

#### `AuthenticodeCertificateThumbprint`

**Type**: `String`  
**Default**: Empty  
**Description**: SHA-1 thumbprint of certificate in Windows certificate store (alternative to PFX file)

```xml
<PropertyGroup>
  <AuthenticodeCertificateThumbprint>ABC123DEF456...</AuthenticodeCertificateThumbprint>
</PropertyGroup>
```

**Note**: Only used if `AuthenticodeCertificatePath` is empty.

#### `AuthenticodeTimestampServer`

**Type**: `String`  
**Default**: `http://timestamp.digicert.com`  
**Description**: URL of the timestamp server for RFC 3161 timestamping

```xml
<PropertyGroup>
  <AuthenticodeTimestampServer>http://timestamp.digicert.com</AuthenticodeTimestampServer>
</PropertyGroup>
```

**Recommended Timestamp Servers**:
- `http://timestamp.digicert.com` (DigiCert - default)
- `http://timestamp.sectigo.com` (Sectigo)
- `http://timestamp.verisign.com/scripts/timstamp.dll` (VeriSign)
- `http://timestamp.globalsign.com/tsa/r6advanced1` (GlobalSign)

#### `AuthenticodeSignToolAdditionalArgs`

**Type**: `String`  
**Default**: Empty  
**Description**: Additional command-line arguments to pass to SignTool.exe

```xml
<PropertyGroup>
  <AuthenticodeSignToolAdditionalArgs>/as /ph</AuthenticodeSignToolAdditionalArgs>
</PropertyGroup>
```

### Configuration in Directory.Build.props

Global signing configuration is defined in `Directory.Build.props`:

```xml
<!-- Authenticode Signing Configuration -->
<PropertyGroup>
  <!-- Enable by default only for Release and Installer configurations -->
  <EnableAuthenticodeSigning Condition="'$(EnableAuthenticodeSigning)' == '' And ('$(Configuration)' == 'Release' Or '$(Configuration)' == 'Installer')">false</EnableAuthenticodeSigning>
  <EnableAuthenticodeSigning Condition="'$(EnableAuthenticodeSigning)' == ''">false</EnableAuthenticodeSigning>
  
  <!-- Certificate configuration -->
  <AuthenticodeCertificatePath Condition="'$(AuthenticodeCertificatePath)' == ''"></AuthenticodeCertificatePath>
  <AuthenticodeCertificatePassword Condition="'$(AuthenticodeCertificatePassword)' == ''"></AuthenticodeCertificatePassword>
  <AuthenticodeCertificateThumbprint Condition="'$(AuthenticodeCertificateThumbprint)' == ''"></AuthenticodeCertificateThumbprint>
  <AuthenticodeTimestampServer Condition="'$(AuthenticodeTimestampServer)' == ''">http://timestamp.digicert.com</AuthenticodeTimestampServer>
  <AuthenticodeSignToolAdditionalArgs Condition="'$(AuthenticodeSignToolAdditionalArgs)' == ''"></AuthenticodeSignToolAdditionalArgs>
</PropertyGroup>
```

---

## Local Development

### Prerequisites

1. **Windows SDK** - Includes SignTool.exe
   - Usually installed with Visual Studio
   - Located at: `C:\Program Files (x86)\Windows Kits\10\bin\<version>\<arch>\signtool.exe`

2. **Code Signing Certificate**
   - PFX file (.pfx) with private key, OR
   - Certificate installed in Windows certificate store

### Enabling Signing Locally

#### Method 1: Using MSBuild Properties (Recommended)

```bash
msbuild Krypton.Toolkit.csproj ^
  /p:EnableAuthenticodeSigning=true ^
  /p:AuthenticodeCertificatePath="C:\Path\To\cert.pfx" ^
  /p:AuthenticodeCertificatePassword="YourPassword" ^
  /p:Configuration=Release
```

#### Method 2: Using Environment Variables

```powershell
$env:EnableAuthenticodeSigning = "true"
$env:AuthenticodeCertificatePath = "C:\Path\To\cert.pfx"
$env:AuthenticodeCertificatePassword = "YourPassword"
msbuild Krypton.Toolkit.csproj /p:Configuration=Release
```

#### Method 3: Using Certificate Store (Thumbprint)

```bash
msbuild Krypton.Toolkit.csproj ^
  /p:EnableAuthenticodeSigning=true ^
  /p:AuthenticodeCertificateThumbprint="ABC123DEF456..." ^
  /p:Configuration=Release
```

**To find certificate thumbprint**:
```powershell
Get-ChildItem -Path Cert:\CurrentUser\My | Where-Object { $_.Subject -like "*Your Certificate*" } | Select-Object Thumbprint, Subject
```

#### Method 4: Configuration-Specific (Directory.Build.props)

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <EnableAuthenticodeSigning>true</EnableAuthenticodeSigning>
  <AuthenticodeCertificatePath>$(MSBuildThisFileDirectory)certificates\codesign.pfx</AuthenticodeCertificatePath>
  <!-- Password should be passed at build time or via environment variable -->
</PropertyGroup>
```

### Testing Without Certificate

To test builds without signing (default behavior):

```bash
# Just build normally - signing is disabled by default
msbuild Krypton.Toolkit.csproj /p:Configuration=Release
```

The build will complete successfully with a log message:
```
Authenticode certificate not provided - signing will be skipped
```

### Verifying Signatures Locally

#### Using SignTool

```powershell
signtool verify /pa /v "Artefacts\Release\net48\Krypton.Toolkit.dll"
```

Expected output:
```
Successfully verified: Krypton.Toolkit.dll

Number of files: 1
```

#### Using PowerShell

```powershell
$file = Get-AuthenticodeSignature "Artefacts\Release\net48\Krypton.Toolkit.dll"
$file | Format-List *
```

Look for:
- `Status: Valid`
- `SignerCertificate` - Shows certificate details
- `TimeStamperCertificate` - Shows timestamp certificate

#### Using File Properties

1. Right-click the DLL/EXE file
2. Select **Properties**
3. Click the **Digital Signatures** tab
4. Verify signature is present and valid

---

## GitHub Actions Integration

### Overview

All release workflows have been configured to support Authenticode signing when certificates are provided via GitHub Secrets.

### Supported Workflows

- ✅ `.github/workflows/New System/release.yml`
  - `release-master`
  - `release-v105-lts`
  - `release-v85-lts`
  - `release-canary`
  - `release-alpha`

- ✅ `.github/workflows/release.yml`
  - All release jobs

- ✅ `.github/workflows/nightly.yml`
  - Nightly builds

### Required GitHub Secrets

#### `AUTHENTICODE_CERT_BASE64`

**Type**: Secret  
**Required**: Yes (for signing to occur)  
**Format**: Base64-encoded PFX certificate file

**How to create**:
```powershell
# Export certificate to base64
$certBytes = [System.IO.File]::ReadAllBytes("C:\Path\To\cert.pfx")
$base64 = [Convert]::ToBase64String($certBytes)
$base64 | Out-File -FilePath "cert-base64.txt" -Encoding ASCII
# Copy content from cert-base64.txt to GitHub Secret
```

**Or using certutil**:
```cmd
certutil -encode cert.pfx cert-base64.txt
# Remove BEGIN/END lines and copy content
```

#### `AUTHENTICODE_CERT_PASSWORD`

**Type**: Secret  
**Required**: Conditional (only if PFX is password-protected)  
**Description**: Password for the PFX certificate

### How It Works in CI/CD

1. **Certificate Preparation Step**
   ```yaml
   - name: Prepare Code Signing Certificate (Optional)
     id: prepare_cert
     shell: pwsh
     run: |
       if ($env:AUTHENTICODE_CERT_BASE64) {
         # Decode certificate and save to temp file
         $certBytes = [Convert]::FromBase64String($env:AUTHENTICODE_CERT_BASE64)
         $certPath = "$env:RUNNER_TEMP\codesign.pfx"
         [System.IO.File]::WriteAllBytes($certPath, $certBytes)
         echo "cert_available=true" >> $env:GITHUB_OUTPUT
       }
   ```

2. **Build Step with Signing**
   ```yaml
   - name: Build Release
     shell: pwsh
     run: |
       $buildArgs = 'Scripts/Build/build.proj /t:Build /p:Configuration=Release ...'
       if (${{ steps.prepare_cert.outputs.cert_available }} -eq 'true') {
         $buildArgs += ' /p:EnableAuthenticodeSigning=true'
         $buildArgs += " /p:AuthenticodeCertificatePath=`"${{ steps.prepare_cert.outputs.cert_path }}`""
         # ...
       }
       msbuild $buildArgs
   ```

### Workflow Behavior

- **If secrets are set**: Binaries are automatically signed during build
- **If secrets are not set**: Build continues normally without signing (no errors)
- **If signing fails**: Warning is logged but build continues (prevents CI failures)

### Adding Secrets to GitHub

1. Go to **Repository Settings** → **Secrets and variables** → **Actions**
2. Click **New repository secret**
3. Add `AUTHENTICODE_CERT_BASE64`:
   - Name: `AUTHENTICODE_CERT_BASE64`
   - Secret: (Paste base64-encoded certificate content)
4. Add `AUTHENTICODE_CERT_PASSWORD` (if required):
   - Name: `AUTHENTICODE_CERT_PASSWORD`
   - Secret: (Enter password)

**⚠️ Security Best Practices**:
- Never commit certificates to the repository
- Use GitHub Secrets for all sensitive data
- Rotate certificates regularly
- Restrict access to secrets using GitHub environments

---

## Certificate Management

### Obtaining a Code Signing Certificate

#### Public Certificate Authorities

- **DigiCert** - https://www.digicert.com/code-signing/
- **Sectigo** (formerly Comodo) - https://sectigo.com/ssl-certificates-tls/code-signing
- **GlobalSign** - https://www.globalsign.com/en/code-signing-certificate
- **VeriSign/Symantec** - Enterprise solutions

**Typical Costs**: $100-500/year depending on validation level

#### Types of Certificates

1. **Standard Code Signing** - Basic validation, suitable for most use cases
2. **EV (Extended Validation)** - Higher trust, no SmartScreen warnings for new publishers
3. **Windows Hardware Developer** - For Windows Store and drivers

### Certificate Storage

#### Option 1: PFX File (Recommended for CI/CD)

**Pros**:
- Portable and easy to use in CI/CD
- Can be stored in secret management systems
- Works across different machines

**Cons**:
- Must be kept secure (password-protected)
- Risk if file is compromised

**Best Practices**:
- Use strong passwords
- Store in secure locations (GitHub Secrets, Azure Key Vault, etc.)
- Restrict file access permissions
- Use separate certificates for dev/staging/production

#### Option 2: Certificate Store (Recommended for Local Dev)

**Pros**:
- Integrated with Windows security
- Better for local development
- Hardware token support (smart cards)

**Cons**:
- Not portable across machines
- Requires manual installation on each machine

**Installation**:
```powershell
# Import certificate to Personal store
Import-PfxCertificate -FilePath "cert.pfx" -CertStoreLocation Cert:\CurrentUser\My -Password (ConvertTo-SecureString -String "Password" -AsPlainText -Force)
```

### Certificate Validation and Testing

#### Using Test Certificates

For development and testing, you can create self-signed certificates:

```powershell
# Create self-signed certificate (Windows 10/11)
New-SelfSignedCertificate `
  -Type CodeSigningCert `
  -Subject "CN=Your Company Name" `
  -KeyUsage DigitalSignature `
  -FriendlyName "Code Signing Certificate" `
  -CertStoreLocation Cert:\CurrentUser\My `
  -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3")
```

**Note**: Self-signed certificates won't be trusted by Windows, but are useful for testing the signing process.

#### Verifying Certificate Properties

```powershell
$cert = Get-ChildItem -Path Cert:\CurrentUser\My | Where-Object { $_.Subject -like "*Your Certificate*" }
$cert | Format-List Subject, Thumbprint, NotBefore, NotAfter, HasPrivateKey
```

Ensure:
- ✅ `HasPrivateKey` is `True`
- ✅ `NotBefore` is in the past
- ✅ `NotAfter` is in the future
- ✅ Intended Purpose includes "Code Signing"

### Certificate Renewal

Certificates typically expire after 1-3 years. Plan renewal before expiration:

1. **Before Expiration**:
   - Order new certificate (can take 1-5 business days for validation)
   - Update GitHub Secrets with new certificate
   - Test signing process

2. **Timestamping Protection**:
   - Files signed with timestamps remain valid after certificate expiration
   - Users can verify signatures even after expiration
   - **Important**: Always use timestamp servers

---

## Troubleshooting

### Common Issues

#### 1. SignTool Not Found

**Error**:
```
error MSB6006: "signtool.exe" exited with code 9009
```

**Solution**:
- Ensure Windows SDK is installed
- Add SDK bin path to PATH environment variable
- Or specify SignTool path explicitly in Directory.Build.targets

#### 2. Certificate Not Found

**Error**:
```
SignTool Error: No certificates were found that met all the given criteria.
```

**Solution**:
- Verify certificate path is correct
- Check certificate password is correct
- Ensure certificate has private key (for PFX files)
- For certificate store: verify thumbprint is correct

#### 3. Invalid Certificate Format

**Error**:
```
SignTool Error: An unexpected internal error has occurred.
```

**Solution**:
- Verify PFX file is not corrupted
- Check certificate is valid for code signing
- Ensure certificate has not expired

#### 4. Timestamp Server Unavailable

**Error**:
```
SignTool Error: An error occurred while attempting to timestamp
```

**Solution**:
- Check internet connectivity (timestamp requires network)
- Try alternative timestamp server
- Temporarily disable timestamping for testing (not recommended)

#### 5. Access Denied

**Error**:
```
SignTool Error: Access is denied.
```

**Solution**:
- Ensure running with appropriate permissions
- Check file is not locked by another process
- Verify file permissions allow modification

### Debugging Signing Process

#### Enable Verbose Logging

The signing target already includes `/v` flag for verbose output. Check build logs for detailed information.

#### Manual Signing Test

Test signing manually to isolate issues:

```powershell
# Test signing a file manually
signtool sign /f "cert.pfx" /p "password" /tr "http://timestamp.digicert.com" /td sha256 /fd sha256 /v "test.dll"
```

#### Check MSBuild Properties

Add diagnostic output to see actual values:

```xml
<Target Name="DiagnoseSigning" BeforeTargets="AuthenticodeSign">
  <Message Text="EnableAuthenticodeSigning: $(EnableAuthenticodeSigning)" />
  <Message Text="AuthenticodeCertificatePath: $(AuthenticodeCertificatePath)" />
  <Message Text="AuthenticodeTimestampServer: $(AuthenticodeTimestampServer)" />
</Target>
```

### Build Log Analysis

Look for these messages in build output:

**Success**:
```
Authenticode signing completed for: Artefacts\Release\net48\Krypton.Toolkit.dll
```

**Failure**:
```
Warning: Authenticode signing failed or skipped for: [file]. Exit code: [code]. Ensure SignTool.exe is available and certificate is configured correctly.
```

**Skipped** (normal if not configured):
```
Authenticode certificate not provided - signing will be skipped
```

---

## Best Practices

### Security

1. **Never commit certificates to repository**
   - Use `.gitignore` for certificate files
   - Store in GitHub Secrets or secure vaults
   - Rotate credentials regularly

2. **Use strong passwords**
   - Minimum 16 characters
   - Mix of uppercase, lowercase, numbers, symbols
   - Consider password managers

3. **Limit certificate access**
   - Only authorized personnel should have access
   - Use GitHub environments with approval requirements
   - Audit certificate usage regularly

4. **Separate certificates for environments**
   - Development (test certificates)
   - Staging (limited trust certificates)
   - Production (publicly trusted certificates)

### Configuration

1. **Always use timestamps**
   - Ensures signatures remain valid after certificate expiration
   - Default timestamp server is configured automatically
   - Use RFC 3161 format (recommended)

2. **Sign only Release/Installer builds**
   - Avoid signing Debug builds (not necessary, slows development)
   - Enable signing selectively:
     ```xml
     <PropertyGroup Condition="'$(Configuration)' == 'Release'">
       <EnableAuthenticodeSigning>true</EnableAuthenticodeSigning>
     </PropertyGroup>
     ```

3. **Handle signing failures gracefully**
   - Don't fail builds if signing fails (current implementation)
   - Log warnings for investigation
   - Consider fail-on-error for production builds

4. **Test signing process**
   - Test locally before committing
   - Verify signatures after signing
   - Test with different certificate scenarios

### Performance

1. **Sign only necessary files**
   - Current implementation only signs DLLs and EXEs
   - No need to sign other file types

2. **Parallel builds**
   - Signing occurs after each target framework build
   - Minimal impact on build time
   - Typically adds 1-3 seconds per binary

3. **Cache considerations**
   - Signatures invalidate file hashes
   - May impact incremental build detection
   - Consider signing as separate step after all builds

### Maintenance

1. **Monitor certificate expiration**
   - Set reminders 90 days before expiration
   - Automate certificate renewal workflows if possible
   - Maintain certificate inventory

2. **Keep SignTool updated**
   - Use latest Windows SDK
   - Update timestamp servers if needed
   - Test new versions before adoption

3. **Documentation**
   - Maintain certificate locations and credentials
   - Document renewal procedures
   - Track signing configuration changes

---

## Examples

### Example 1: Local Release Build with Signing

```powershell
# Build and sign locally
msbuild "Source\Krypton Components\Krypton.Toolkit\Krypton.Toolkit.csproj" `
  /t:Build `
  /p:Configuration=Release `
  /p:EnableAuthenticodeSigning=true `
  /p:AuthenticodeCertificatePath="C:\Certificates\codesign.pfx" `
  /p:AuthenticodeCertificatePassword="SecurePassword123!" `
  /v:minimal
```

### Example 2: Build Script with Certificate

```powershell
# build-and-sign.ps1
param(
    [string]$CertificatePath = "$env:USERPROFILE\Certificates\codesign.pfx",
    [string]$CertificatePassword = $env:CODESIGN_PASSWORD,
    [string]$Configuration = "Release"
)

if (-not (Test-Path $CertificatePath)) {
    Write-Warning "Certificate not found at $CertificatePath"
    Write-Host "Building without signing..."
    $EnableSigning = "false"
} else {
    Write-Host "Certificate found. Building with signing enabled..."
    $EnableSigning = "true"
}

msbuild "Scripts\Build\build.proj" `
  /t:Build `
  /p:Configuration=$Configuration `
  /p:EnableAuthenticodeSigning=$EnableSigning `
  /p:AuthenticodeCertificatePath=$CertificatePath `
  /p:AuthenticodeCertificatePassword=$CertificatePassword
```

### Example 3: Conditional Signing in Directory.Build.props

```xml
<PropertyGroup>
  <!-- Enable signing only if certificate environment variable is set -->
  <EnableAuthenticodeSigning Condition="'$(CODESIGN_CERT_PATH)' != ''">true</EnableAuthenticodeSigning>
  <AuthenticodeCertificatePath Condition="'$(CODESIGN_CERT_PATH)' != ''">$(CODESIGN_CERT_PATH)</AuthenticodeCertificatePath>
  <AuthenticodeCertificatePassword Condition="'$(CODESIGN_CERT_PASSWORD)' != ''">$(CODESIGN_CERT_PASSWORD)</AuthenticodeCertificatePassword>
</PropertyGroup>
```

Usage:
```powershell
$env:CODESIGN_CERT_PATH = "C:\Certificates\codesign.pfx"
$env:CODESIGN_CERT_PASSWORD = "Password123"
msbuild MyProject.csproj /p:Configuration=Release
```

### Example 4: Verify All Signed Files

```powershell
# verify-signatures.ps1
$artifacts = Get-ChildItem -Path "Artefacts\Release" -Recurse -Include *.dll,*.exe
$failed = @()

foreach ($file in $artifacts) {
    $sig = Get-AuthenticodeSignature $file.FullName
    if ($sig.Status -ne "Valid") {
        $failed += $file.FullName
        Write-Warning "Invalid signature: $($file.FullName) - Status: $($sig.Status)"
    } else {
        Write-Host "✓ Valid signature: $($file.Name)"
    }
}

if ($failed.Count -gt 0) {
    Write-Error "Found $($failed.Count) files with invalid signatures"
    exit 1
} else {
    Write-Host "All files are properly signed!"
}
```

### Example 5: Batch Sign Multiple Files

```powershell
# Manual batch signing (if needed)
$files = Get-ChildItem -Path "Artefacts\Release" -Recurse -Include *.dll,*.exe
$certPath = "C:\Certificates\codesign.pfx"
$certPassword = "Password123"
$timestampServer = "http://timestamp.digicert.com"

foreach ($file in $files) {
    Write-Host "Signing: $($file.FullName)"
    & signtool sign /f $certPath /p $certPassword /tr $timestampServer /td sha256 /fd sha256 /v $file.FullName
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to sign: $($file.FullName)"
    }
}
```

---

## Additional Resources

### Microsoft Documentation

- [SignTool (Sign Tool)](https://docs.microsoft.com/en-us/windows/win32/seccrypto/signtool)
- [Code Signing Best Practices](https://docs.microsoft.com/en-us/windows/win32/seccrypto/cryptography-tools)
- [Authenticode Signatures](https://docs.microsoft.com/en-us/windows/win32/seccrypto/authenticode)

### Tools

- **SignTool** - Included with Windows SDK
- **Certificate Manager (certmgr.msc)** - Windows certificate store management
- **PowerShell Certificate Cmdlets** - `Get-ChildItem Cert:\`, `Import-PfxCertificate`, etc.

### Related Issues

- GitHub Issue #2897: [Feature Request: Add Authenticode signing](https://github.com/Krypton-Suite/Standard-Toolkit/issues/2897)

---

## Changelog

### Version 1.0 (2026-01)

- ✅ Initial implementation of Authenticode signing
- ✅ MSBuild target integration
- ✅ GitHub Actions workflow support
- ✅ Configuration via MSBuild properties
- ✅ Support for PFX files and certificate store
- ✅ Automatic timestamp server integration
- ✅ Graceful fallback when certificates not configured

---

## Support

For issues or questions regarding Authenticode signing:

1. **Check this documentation** - Most common issues are covered
2. **Review build logs** - Look for signing-related messages
3. **Verify certificate** - Ensure certificate is valid and accessible
4. **Test locally** - Reproduce issue in local environment
5. **Create GitHub Issue** - Include relevant build logs and error messages

---

**Last Updated**: January 2026  
**Maintainer**: Krypton Toolkit Team  
**Status**: Production Ready ✅