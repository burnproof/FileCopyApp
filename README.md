# FileCopyApp

Further improvements:
	- Add a button to delete the destination file, if it exists.
	- Add a checkbox for always on-top behavior. In that mode, only the "Play" button is shown hovering on top.
	- Add a button to open the destination folder.
	- Add a button to open the source folder.
	- Add a button to open the source file.
	- Add a mechanism to detect if the destination file is locked by another process and show a message to the user. This can be done by checking if the file is in use before copying and displaying a tooltip message or changing the color of the copy button to yellow.
	- Add a scaling mechanism so that the app can be resized, hiding the source and destination file paths.
	- Add a taskbar hover-interface, showing the copy button and the status led. This would allow to hide the app window and still have the copy functionality available. It would work only on non-server environment, as to server we log on through RDP and this type of interface would not be available.
		- Subnote: Maybe to develop this as a application tray application/menu? I guess that would also work on server? 
	- Add a wildcard based copy mechanism. For example, if the source file is "C:\temp\*.txt", then all .txt files in the folder would be copied to the destination folder.
		- Subnote: This would require mechanism to detect if the destination contains also any wildcard-matching files, and then would not show the actual coped files in the list, but instead show the wildcard-matching files.

Version 1.0.1
	- Add a mechanism to detect if the source file exists. Currently, if the file is deleted after it has been already selected, the app will not detect it.
		--> Added. If the source file does not exist, then the copy button is disabled and the input field is colored with an orange frame.
			- Additional notification: If the source file is deleted after, then the status "led" is still colored green, but the message is updated to "Source file does not exist". It might be that we should change the color of the status led to red if the source file does not exist, but as the file location is still valid per the previous selection, I decided to keep it green.

Version 1.0.2
	- Added app.config. The app now reads the source and destination paths from the app.config file and saves the last used paths to the app.config file.