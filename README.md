# PastebinClone

## Instructions

Clone project. Add the path to directory containing files to be made public in appsettings.json. In the terminal, navigate to the project containing the PastebinClone solution file and build and run the project.

```dotnet build```

```dotnet run```

## Improvements

- Security - The user inputs the directory name as a parameter and then read the file directly. As such it is important that they only have access to the intended files - it would be better to keep public files in wwwroot, ensuring the project only has access to internal file, rather than wherever the location entered into appsettings. The parameter also needs validation.
- Testing - Needs a lot more thorough testing.
- Pattern matching - would probably be better to test directories to see if they contain the required file rather than returning null to reduce null checks.

I wasted a bit of time trying to make the file reads async under the assumption that if this was a production application we would not want the I/0 opperations blocking threads, however this did not seem to be the correct way with the file system - something I need to look into more.
