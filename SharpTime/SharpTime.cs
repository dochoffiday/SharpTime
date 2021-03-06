﻿using System;

public static class SharpTime
{
    [ThreadStatic]
    private static DateTime? _dateTimeUtc;

    public static DateTime UtcNow
    {
        get
        {
            if (_dateTimeUtc.HasValue)
            {
                return _dateTimeUtc.Value;
            }

            return DateTime.UtcNow;
        }
    }

    public static IDisposable UseSpecificDateTimeUtc(DateTime dateTimeUtc)
    {
        if (_dateTimeUtc.HasValue) throw new InvalidOperationException("SharpTime is already locked");

        _dateTimeUtc = dateTimeUtc;

        return new LockedDateTimeUtc();
    }

    private class LockedDateTimeUtc : IDisposable
    {
        public void Dispose()
        {
            _dateTimeUtc = null;

            GC.SuppressFinalize(this);
        }
    }
}