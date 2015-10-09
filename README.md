# Supersonic Sound
## C# Wrapper For FMOD Studio Programmers API

 > FMOD Studio is a game changing Pro Audio content creation pipeline for sound designers and musicians. FMOD Studio programmer’s API is the interface for programmers to load FMOD Studio projects and play them back in realtime. Also includes a low level API for simple sounds/channels/dsp/geometry.

SupersonicSound is a C# wrapper for the FMOD studio API. There are a few wrappers for FMOD ex, the legacy sound system, but as far as I am aware *SupersonicSound* is the only C# wrapper for the modern FMOD Studio programmers API.

The aim of the *SupersonicSound* is to present a much more C# friendly experience for using FMOD studio programmers API.

## Licensing

Obviously I do no control licensing of the any FMOD technologies. You should [go here](http://www.fmod.org/sales/) to the FMOD sales page and check what the licensing conditions are before proceeding any further! As of January 1st 2015 the licensing conditions are very friendly to indies, non-commercial and educational uses.

This library itself is MIT licensed, see the LICENSE.md file for more details.

## Installation Instructions

The easiest way to get started using SupersonicSound is to use the [nuget package](https://www.nuget.org/packages/SupersonicSound/). Run:

 > Install-Package SupersonicSound
 
at the nuget package manager console. Currently this package requires .Net 4.6 (due to a dependency on System.Numerics).

Unfortunately FMOD licensing does not allow for the FMOD dlls to be distributed with the nuget package and you will need to download these yourself.

 - [Linux installation instructions](https://github.com/martindevans/SupersonicSound/wiki/Installation:-Linux)
 - [Windows installation instructions](https://github.com/martindevans/SupersonicSound/wiki/Installation:-Windows)

## Examples

SupersonicSound is *almost* a direct translation from the C++ API but instead does things in C# style. For example to get a parameter from an event instance in C++ is:

    FMOD.Studio.ParameterInstance instance;
    ERRCHECK(_eventInstance.getParameter(name, &instance));
    return instance;
    
Doing the same in C# is:

    return _eventInstance.Parameters[name];
    
Because the two APIs are so similar you should be able to follow along with the documentation supplied with the C++ API without any trouble. If you find any places where SupersonicSound is inconsistent then please create an issue here. Here's a more complete example of how to load banks, get an event from them and then play back a sound with a varying parameter:

    using (System system = new System())
    {
        // Load bank from disk
        var master = system.LoadBankFromFile("Master Bank.bank", BankLoadingFlags.Normal);
        var strings = system.LoadBankFromFile("Master Bank.strings.bank", BankLoadingFlags.Normal);
        var bank = system.LoadBankFromFile("Surround_Ambience.bank", BankLoadingFlags.Normal);

        // Get a single playback event
        var loopingAmbienceDescription = system.GetEvent("event:/Ambience/Country");

        //Create an instance of it
        var loopingAmbienceInstance = loopingAmbienceDescription.CreateInstance();

        // Get a parameter from this playback instance
        var timeParam = loopingAmbienceInstance.Parameters["Time"];

        // Start playing it
        loopingAmbienceInstance.Start();

        // Loop forever, playing the sound
        while (true)
        {
            Thread.Sleep(1);
            
            // Update the playback system
            system.Update();

            // Vary the parameter we fetched earlier
            timeParam.Value = (float)(DateTime.Now.TimeOfDay.TotalSeconds * 0.025f) % 1;
        }
    }
    
## Implementation Notes

#### Wrapping The Wrapper

Supplied with the [FMOD Studio API](http://www.fmod.org/download/#StudioAPIDownloads) is a C# wrapper which is almost a direct translation of the C++ API. SupersonicSound is built around this wrapper and acts as a translation layer from C# conventions to the C++ conventions.

The supplied FMOD C++ wrapper is included almost unchanged in SupersonicSound/Wrapper/Fmod (some very minor changes were required to expose some private fields as internal - this seems to have been an implementation mistakes by the FMOD wrapper programmers). This means that when a new FMOD version is released (with a new FMOD wrapper) it should be very simple to update SupersonicSound to the new version - simply drop in the new wrapper and the new DLLs.

#### Native DLLs

Obviously a wrapper around a native API requires some native DLLs! SupersonicSound DLLs are loaded as soon as you try to create a "System" (a core part of the FMOD API). You can load the DLLs early by calling ```Native.Load()```. If you have already loaded the fmod DLLs yourself, you can instead call ```Native.SuppressLoad()``` - be warned this cause a *System.DllNotFoundException* if you have not loaded the correct DLLs.

The required DLLs are:

 - fmod.dll
 - fmodstudio.dll

#### Structs

The supplied wrapper allocates objects to wrap up C++ handles in C# objects. If SupersonicSound also allocated objects to wrap these objects that would make this wrapper excessively allocation heavy. Instead all the SupersonicSound wrappers are structs which have been optimised to be very small (the most commonly used ones are pointer sized). This makes using SupersonicSound almost a zero cost abstraction.

## Contributions

Contributions are welcome! Please open up an issue before you write any code though, just in case I'm already working on something similar.

## Known Issues

 - Async file systems are not supported. There seems to be a problem with the underlying FMOD wrapper being incorrectly structured.
