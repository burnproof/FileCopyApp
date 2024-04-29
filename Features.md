# FileCopyApp

## Further improvements:
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
	- Add a functionality for global hotkey registration, so that the user can press a hotkey to copy the file. This would be useful for example when the user is working in another application and wants to copy the file without switching to the FileCopyApp.
		- Subnote: This would require a way to define the hotkey in the app.config file.
    - Add a functionality to move the file instead of copy. That would require a checkbox to select if the file should be moved or copied. This feature would be useful for example when the user has a case, where they constanly receive a new file to certain directory and want to move it from there. 
    - Add functionality to have input path and maybe also the output path as a api paths (though, we are passing to the realms of postman and other great utilities with this). Still it would be useful for some folks. If done, to impement in an API like way, having authentication, GET, POST, PUT, DELETE methods and at least raw headers and body.


* Version 1.0.1 *
	- Add a mechanism to detect if the source file exists. Currently, if the file is deleted after it has been already selected, the app will not detect it.
		--> Added. If the source file does not exist, then the copy button is disabled and the input field is colored with an orange frame.
			- Additional notification: If the source file is deleted after, then the status "led" is still colored green, but the message is updated to "Source file does not exist". It might be that we should change the color of the status led to red if the source file does not exist, but as the file location is still valid per the previous selection, I decided to keep it green.

* Version 1.0.2 *
	- Added app.config. The app now reads the source and destination paths from the app.config file and saves the last used paths to the app.config file.










# Sample code for global hotkey registration

```csharp
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        // Read the hotkey combination from app.config
        string hotkeyConfig = ConfigurationManager.AppSettings["Hotkey"];

        // Parse the hotkey combination
        Tuple<int, Keys> hotkey = ParseHotkey(hotkeyConfig);

        if (hotkey != null)
        {
            // Register the hotkey
            RegisterHotKey(IntPtr.Zero, hotkey.Item1, hotkey.Item2);
            Console.WriteLine($"Registered hotkey: {hotkeyConfig}");
        }
        else
        {
            Console.WriteLine("Invalid hotkey configuration.");
        }

        Console.ReadLine();
    }

    static Tuple<int, Keys> ParseHotkey(string hotkeyConfig)
    {
        // Mapping of modifier names to their corresponding key codes
        Dictionary<string, Keys> modifierMap = new Dictionary<string, Keys>
        {
            { "{ALT}", Keys.Alt },
            { "{CTRL}", Keys.Control },
            { "{SHIFT}", Keys.Shift },
            { "{WIN}", Keys.LWin },
            { "{LEFT}", Keys.Left },
            { "{RIGHT}", Keys.Right },
            { "{UP}", Keys.Up },
            { "{DOWN}", Keys.Down },
            { "{INS}", Keys.Insert },
            { "{DEL}", Keys.Delete },
            { "{HOME}", Keys.Home },
            { "{END}", Keys.End },
            { "{PGUP}", Keys.PageUp },
            { "{PGDN}", Keys.PageDown },
            { "{TAB}", Keys.Tab },
            { "{ENTER}", Keys.Enter },
            { "{BKSP}", Keys.Back },
            { "{ESC}", Keys.Escape },
            { "{F1}", Keys.F1 },
            { "{F2}", Keys.F2 },
            { "{F3}", Keys.F3 },
            { "{F4}", Keys.F4 },
            { "{F5}", Keys.F5 },
            { "{F6}", Keys.F6 },
            { "{F7}", Keys.F7 },
            { "{F8}", Keys.F8 },
            { "{F9}", Keys.F9 },
            { "{F10}", Keys.F10 },
            { "{F11}", Keys.F11 },
            { "{F12}", Keys.F12 }
        };

        // Parse the hotkey configuration
        string[] parts = hotkeyConfig.Split('+');

        // Initialize modifier and key variables
        int modifiers = 0;
        Keys key = 0;

        foreach (string part in parts)
        {
            // Check if part is a modifier
            if (modifierMap.TryGetValue(part.Trim(), out Keys modifierKey))
            {
                modifiers |= (int)modifierKey;
            }
            else
            {
                // Check if part is a regular key
                if (Enum.TryParse(part.Trim(), out Keys regularKey))
                {
                    key = regularKey;
                }
                else
                {
                    // Invalid key detected
                    return null;
                }
            }
        }

        // Return the parsed hotkey combination
        return Tuple.Create(modifiers, key);
    }

    // P/Invoke declarations for RegisterHotKey and UnregisterHotKey
    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}
