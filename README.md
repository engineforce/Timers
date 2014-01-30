Timers
======

Countdown timer and Stopwatch timer


# Descriptions

A high resolution Countdown Timer and Stop Watch written in C# .NET that pop up a message box with user defined message and play user specified sound when time is up.

It use a QueueTimer (via PInvoke) for high resolution timing and System.Timers.Timer for normal resolution.

It also allow users to save, load and remove current state to/from a XML file.


# Features

* Countdown timer and Stop Watch.
* Pop up user defined message when time is up.
* Play user specifed sound when time is up.
* Automatically imecalculate effective t from input, such as 80 seconds equal to 1 minute and 20 seconds.
* Used QueueTimer (via PInvoke) for high resolution. Used System.Timers.Timer for normal resolution.
* Support down to 1 millisecond (ms) resolution.
* Save, Load, Remove current state to/from a XML file


# Snapshots

Countdown Timer

<img src="/Snapshots/CountdownTimer.png" title="Countdown Timer" alt="Countdown Timer" width="400px" height="317px">

Stopwatch Timer

<img src="/Snapshots/StopwatchTimer.png" title="Stopwatch Timer" alt="Stopwatch Timer" width="400px" height="317px">
