# Overwrites the whole PE Header

* Remember starting the process suspended with CreateProcess can defeat this, therefor this should be used with code getting invoked with reflection on runtime.

- It would probally be better overwriting offsets in .NET Directory instead of the PE Header. But this gives a good idea about how you find around in the Header with .NET
