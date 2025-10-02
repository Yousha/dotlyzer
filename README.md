# Dotlyzer - .NET Process Analyzer

CLI & interactive based process analyzer and diagnostic tool. For CLI/headless based servers, open-source, no false-report & no malware.

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

1. Launch the application
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

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the **GPL-3.0**. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or feedback, please use [issues](https://github.com/yousha/dotlyzer/issues) section.
