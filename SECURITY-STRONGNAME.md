# Strong Name Key Management

## Security Notice

⚠️ **CRITICAL**: Strong name key files (.snk) have been removed from this repository for security reasons.

## Background

Strong name key files (.snk) are used to digitally sign .NET assemblies and should never be committed to source control as they represent a security risk.

## Development Setup

### Option 1: Auto-Generate Keys (Recommended for Development)

The MSBuild process can automatically generate strong name keys during build:

```xml
<PropertyGroup>
  <SignAssembly>true</SignAssembly>
  <DelaySign>false</DelaySign>
  <!-- MSBuild will auto-generate if file doesn't exist -->
  <AssemblyOriginatorKeyFile>$(MSBuildProjectName).snk</AssemblyOriginatorKeyFile>
</PropertyGroup>
```

### Option 2: Manual Key Generation

Generate your own keys locally (do not commit these):

```powershell
# Generate a new strong name key pair
sn -k YourAssembly.snk

# Or use the provided task
dotnet build -p:GenerateStrongNameKey=true
```

### Option 3: Production Signing

For production releases, use:
- Azure Key Vault for centralized key management
- Build agent certificates
- Hardware security modules (HSM)

## Environment Setup

1. Clone the repository
2. Run initial build - keys will be auto-generated locally
3. Keys are automatically excluded from git via .gitignore

## Build Process

The build system automatically:
- Generates missing .snk files during first build
- Uses existing local .snk files if present
- Excludes .snk files from version control

## CI/CD Integration

For continuous integration:
- Use secret management systems (GitHub Secrets, Azure Key Vault)
- Generate keys during build process
- Never store keys in pipeline configuration