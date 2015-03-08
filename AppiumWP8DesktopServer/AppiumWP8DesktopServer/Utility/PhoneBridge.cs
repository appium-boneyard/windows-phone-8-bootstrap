using Microsoft.SmartDevice.Connectivity.Interface;
using Microsoft.SmartDevice.MultiTargeting.Connectivity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace AppiumWP8DesktopServer.Utility
{
    public class PhoneBridge
    {
        public string DeviceId { get; private set; }
        public IDevice Device { get; private set; }
        public bool IsConnected { get; private set; }

        private string _PathToMSBuild;
        private string _PathToVSTestConsole;
        private string _PathToBootstrap;
        private string _PathToCodeUITestServiceCode;

        public PhoneBridge(string deviceId)
        {
            DeviceId = deviceId;
            _PathToMSBuild = Path.Combine((string)Registry.GetValue(@"HKLM\SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0", "MSBuildToolsPath", null),"msbuild.exe");
            _PathToVSTestConsole = @"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe";
            _PathToBootstrap = Path.GetTempPath();
            _PathToCodeUITestServiceCode = @"C:\Users\Joseph\Desktop\\";
            IsConnected = false;
        }

        public object ProxyCommand(string commandText)
        {
            // Create a temp folder for the command
            var tempFolder = Path.GetTempPath();
            _PathToBootstrap = Path.Combine(tempFolder, "CodedUITestBootstrap");
            Directory.CreateDirectory(_PathToBootstrap);
            var files = Directory.GetFiles(_PathToCodeUITestServiceCode);
            foreach(var file in files) {
                File.Copy(file, Path.Combine(_PathToBootstrap, Path.GetFileName(file)));
            }

            // install the command
            var settingsFilePath = Path.Combine(_PathToBootstrap, "CodeUITestService", "Settings.cs");
            var sr = new StreamReader(settingsFilePath);
            var oldSettingsFileContent = sr.ReadToEnd();
            sr.Close();
            var newSettingsFileContent = oldSettingsFileContent.Replace("\"\"", "\"" + commandText + "\"");
            File.Delete(settingsFilePath);
            var sw = new StreamWriter(settingsFilePath);
            sw.Write(newSettingsFileContent);
            sw.Close();

            // build the action as a unit test
            var buildPSI = new ProcessStartInfo();
            buildPSI.FileName = _PathToMSBuild;
            buildPSI.Arguments = "CodedUITestService.sln";
            buildPSI.WorkingDirectory = _PathToBootstrap;
            var buildProcess = Process.Start(buildPSI);
            buildProcess.WaitForExit();

            // edit the .runsettings for the device
            // skip for now (just update the device line to have the appropriate device id)

            // run the bootstrap as a unit test
            var bootstrapPSI = new ProcessStartInfo();
            bootstrapPSI.FileName = _PathToVSTestConsole;
            bootstrapPSI.Arguments = "bin/Debug/CodedUITestService.dll /RunSettings sample.runsettings";
            bootstrapPSI.WorkingDirectory = _PathToBootstrap;
            var bootstrapProcess = Process.Start(bootstrapPSI);
            bootstrapProcess.WaitForExit();

            // process the results
            return null;
        }

        public void Connect()
        {
            var connectivity = new MultiTargetingConnectivity(CultureInfo.CurrentUICulture.LCID);
            var connectableDevice = connectivity.GetConnectableDevice(DeviceId);
            Device = connectableDevice.Connect();
            IsConnected = true;
        }

        public static List<string> GetAllDeviceIDs()
        {
            MultiTargetingConnectivity connectivity = new MultiTargetingConnectivity(CultureInfo.CurrentUICulture.LCID);

            var connectableDevices = connectivity.GetConnectableDevices();
            List<string> deviceIds = new List<string>();
            foreach (var device in connectableDevices)
            {
                deviceIds.Add(device.Id);
            }
            return deviceIds;
        }

        public IRemoteApplication LaunchApp(string appGuid, IDevice device)
        {
            Guid appID = new Guid(appGuid);
            var app = device.GetApplication(appID);
            app.Launch();
            return app;
        }

    }
}
