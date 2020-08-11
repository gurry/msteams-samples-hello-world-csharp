# TestTeamApp

This is a Teams a to demo how Microsoft Teams can integrate with Tachyon and few other things

## How to set up

Follow the below steps to set up this app:

1. Set up Tachyon server on a virtual machine along with switches and the rest of the crap on the same machine.
2. Set up Tachyon client on a second virtual machine and ensure that it is connected to the switch.
3. Build `Proxy` project and copy its output to the virtual machine running Tachyon.
4. Change `UpstreamUrl` in `appsettings.json` of `Proxy` to your Tachyon hostname and start `Proxy.exe`. The proxy will make Tachyon API accessible from any machine and by any user.
5. Download and run ngrok on the physical machine (which is hosting the above virtuals) by following the directions here: https://dashboard.ngrok.com/get-started/setup. Point ngrok to `http://localhost:5000` which is where our Teams app will be running (see below). Note down ngrok's Internet facing URL.
6. Replace all the current ngrok URLs in the `manifest.json` file in Teams app project `Microsoft.Teams.Samples.HelloWorld.Web` with the Internet-facing ngrok URL you noted above.
7. Change `TachyonHostname` in `appsettings.json` of `Microsoft.Teams.Samples.HelloWorld.Web` project to your Tachyon hostname.
8. Set `Microsoft.Teams.Samples.HelloWorld.Web` project as the start-up project and start it in debug mode with F5. It should start listening at `http://localhost:5000`, the URL to which you pointed ngrok earlier. (This project shouid be running on the same machine as ngrok above, which is going to be your physical machine). 
9. Set up a third virtual machine with Internet access. Set up a Teams developer instance on it following directions here: https://docs.microsoft.com/en-us/microsoftteams/platform/toolkit/visual-studio-code-overview
10. From the output folder of project `Microsoft.Teams.Samples.HelloWorld.Web` copy `helloworldapp.zip` to the above virtual machine where you set up Teams instance.
11. Open `helloworldapp.zip` in Teams' and update and ensure all the URLs in the manifest have the Internet-facing ngrok URL as their base URL. Especially check the messaging API URL of the bot. It is bound to be set to something wrong.
12. After making the above changes download the app from AppStudio which will save it in the Windows Download folder. It will be in the form of another zip file.
13. Add this app to Teams by uploading the zip fiwl "Upload custom app" button in Apps area of MS Teams.
14. Use the app. It should now be able to get device info from Tachyon and what not.
