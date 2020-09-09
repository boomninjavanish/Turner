# Turner
## An eye tracking page turner for Windows.
![The Turner eye tracking page turning interface contains two red overlays on either side of the screen.](/turnerScreenshot.png)

Turner allows a pianist or other person whose limbs are encumbered to increment or decrement the page in a PDF reading application. The user's gaze is tracked using a Tobii Eye Tracker 4C via the Tobii Core SDK. If the user gazes at the overlay for a set amount of time, the application will press a virtual key that corresponds with a page turn for an ebook reader application. The ereader used for testing, [Drawboard PDF](https://www.drawboard.com), maps the left and right arrow keys to page turns. Therefore, this button mapping is hardcoded into the Turner application. Once the application is executed, it may be minimized or left running in the background while the ebook reading software is in focus.

## Settings
The application allows for the width of the left and right gaze area overlays to be adjuastable. In addition, the amount of the time in which the user must gaze before a left or right key press is triggered may be modified.

## Download
[Version 0.1](https://github.com/boomninjavanish/Turner/releases/tag/v0.1-alpha)

Right now, there is no installer for the application. However, this means that the application is portable; simply place the files in a directory of your choosing then execute the EyeTrackingPageTurner.exe file. Note that a Tobii Eye Tracker device is required along with the relevant drivers.

## Compiling
Turner is written in C# for the Windows Presentation Foundation framework. The Nuget package manager was used to obtain dependancies Therefore, [Visual Studio 2019](https://visualstudio.microsoft.com) is required to compile the application. 

## Contributing
If you wish to contribute code or have ideas on how this application may be improved, [feel free to reach out to me](https://dunlap.media/contact).
