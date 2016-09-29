# Auth0Demo
#### A demo of how to use the Auth0 client SDK with Xamarin Forms

![UI on Devices](https://github.com/jsauve/Auth0Demo/blob/master/readme_images/UiOnDevices.png)

## Setup

#### 1. Move `PrivateKeys.cs` into place
In the `src/Auth0Demo.Config/Temp` folder, you'll see a file named `PrivateKeys.cs`. Move this file into the `src/Auth0Demo.Config` folder. The final path should be `src/Auth0Demo.Config/PrivateKeys.cs`:

    src/Auth0Demo.Config/Temp/PrivateKeys.cs --> src/Auth0Demo.Config/PrivateKeys.cs

#### 2. Obtain an Auth0 domain and client ID

Sign up for an Auth0 account. Follow the instructions to setup whichever auth providers you're interested in (Github, Microsoft, LinkedIn, etc.), and then find your Auth0 Domain and Client ID:
![Auth0 Dashboard - Clients section](https://github.com/jsauve/Auth0Demo/blob/master/readme_images/Auth0Dashboard-ClientSection.png)

#### 3. Then copy and paste your Auth0 domain and Client ID into `PrivateKeys.cs`
![Paste into PrivateKeys.cs](https://github.com/jsauve/Auth0Demo/blob/master/readme_images/PasteIntoPrivateKeys.png)

## Caveats

#### UWP
The UWP target is not currently working. For some reason, the static Settings class cannot be found at runtime, and I'm not sure why. I'll try to fix when I get time, but no promises.

#### Insecure storage of authentication token and profile information
For the purposes of this sample, the token and profile information that is recieved after a successful login is stored in Xam.Plugins.Settings. This is done merely as a convenience for this demo. This is **_NOT_** a secure storage medium. It is strongly advised that you instead use some sort of secure storage mechanism, such as the iOS keychain, or perhaps enctrypted SQLite or encrypted Akavache. **Secure storage of tokens and profiles is _your_ responsibility.**
