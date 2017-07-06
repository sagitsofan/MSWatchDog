# Media Services Watch-Dog
Online health check of the Azure Media Services. 
<br>
Helps users to get an notification about the service status.
<BR>
To get a full status check on all of the Azure products: https://azure.microsoft.com/en-us/status/
<br>
This service is based on the NLog infrastructure so you can watch the current state of the service.
<Br>
Installation is easy with 2 different ways:

1) Install as a Azure Function Apps- "MSWatchDog.AzureFunctions" project
2) Install as a Windwos Services - "MSWatchDog.Service" project

# Windows Service instructions

# Configuration
1. Open "App.config"
2. Change "MSWatchDog.SMTPServer" into your own smtp server
3. Change "MSWatchDog.ToEmailsAlert" + "MSWatchDog.FromEmailAlert" to your email preference
4. Change the default "MSWatchDog.WatcherInterval" value in seconds of the status check interval
5. Setup your own "watcherEndPoints" and add "ism" files from the Azure Media services repository

# Installation
This will install the app as a local windows service
1. Edit "reinstall.bat" and change the "C:\projects\..." value into your local path.
2. Run "reinstall.bat" with administrator privileges
3. A pop-up will show for entering the service running credentials
4. That's it!
