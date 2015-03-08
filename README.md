# windows-phone-8-bootstrap
bootstrap server for windows phone 8

## How does it work
The server drives the windows phone 8 device using the officially supported coded ui test framework which Microsoft ships with Visual Studio 2013 Ultimate and Premium editions

### Current Technique
* the console application receives a JSON wire command and executes it (if it can be done using the SmartDevice DLLs or passes it on the bootstrap if it needs to do so)
* commands passed to the bootstrap are built into a DLL
* DLLs are executed using vstest.console.exe and the results are sent back

### Why this craziness?
* Much like iOS UIAutomation, Coded UI Test for Windows Phone is heavily sandboxed. Unlike the Windows version, data-driven tests must be coded inline and not in a separate file. 

### Future Improvements
* Change the string value of the command in the DLL using a framework like(reduces build-time between commands)
* Use the command-line debugger to allow a looping bootstrap, loop by settings a breakpoint and injecting the command by changing the value using the debugger each time one is received
