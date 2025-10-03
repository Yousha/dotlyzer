# [Dotlyzer](https://github.com/yousha/dotlyzer/)

CLI & interactive based process analyzer and diagnostic tool. For CLI/headless based servers, open-source, no false-report & no malware.

[![CodeQL](https://github.com/Yousha/dotlyzer/actions/workflows/github-code-scanning/codeql/badge.svg?branch=main)](https://github.com/Yousha/dotlyzer/actions/workflows/github-code-scanning/codeql)

![Logo](resources/images/logo_64x64.png)

## Features

- Process Analysis
- Memory Analysis
- Thread Analysis
- Diagnostic Features
- Profiling Capabilities
- Dumping Features
- System Integration

## Requirements

- .NET 8.0 Runtime
- Windows OS (all Windows servers/clients)
- Administrator privileges for full feature access

## Installation

### Download

1. Download archived package.
2. Extract package.
3. Execute the application.

### Build

1. Clone the repository:

```bash
git clone https://github.com/yousha/dotlyzer.git
```

2. Build the project:

```bash
dotnet build -c Release
```

3. Run the application:

```bash
dotnet run
```

Or build an executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## Usage

1. Run the application
2. Choose from the main menu:
   - List Processes: View all running processes
   - Inspect Process: Analyze a specific process by PID
   - About: View application information
   - Exit: Close the application

3. For process inspection, enter the PID and choose from available analysis options

## Security Considerations

- Some features require administrator privileges
- Process inspection may be limited by access permissions
- Minidump creation requires appropriate process access rights
- Always run with minimal required privileges

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork repository.
2. Create a new branch for your feature or bugfix.
3. Submit a pull request with a detailed description of your changes.

For more details see [CONTRIBUTING.txt](CONTRIBUTING.txt).

## Code of Conduct

See [CODE_OF_CONDUCT.txt](CODE_OF_CONDUCT.txt) file.

## DCO

See [DCO.txt](DCO.txt) file.

## License

This open-source software is distributed under the GPL-3.0 license. See [LICENSE](LICENSE) file.

## Contact

For questions or feedback, please use [issues](https://github.com/yousha/dotlyzer/issues) section.
