using System;


public static class StringTime
{
    public static string SecondToTimeString(float secong)
    {
        return TimeSpan.FromSeconds(secong).ToString(@"mm\:ss\.ff");
    }
}
