# Overwrites the whole PE Header

* Remember starting the process suspended with CreateProcess can defeat this, therefor this should be used with code getting invoked with reflection on runtime.

- And you might wanna change the RPM api out with Marshal.ReadInt
- And it would probally be better overwriting offsets in .NET Directory instead of the PE Header. But this gives a good idea about how you find around in the Header with .NET
- And yes I hate structures, if anyone wonder why I use arrays over structures..
