# SharpTime is a micro-library for a DateTime built with testing in mind

### Normal Usage

```
var utcNow = SharpTime.UtcNow;
```

### Testing Usage

If you need to specifically set the UTC DateTime returned by SharpTime, you can do so by calling the `UseSpecificDateTimeUtc` method:

```
using (SharpTime.UseSpecificDateTimeUtc(new DateTime(2020, 12, 25)))
{
   var utcNow = SharpTime.UtcNow; // will return Christmas! 🎅🎄
}
```

`UseSpecificDateTimeUtc` sets the UTC DateTime for the current thread and _only_ the current that (and it does so without any locking). This is intentional, so it doesn't mess up tests being run in parallel.

**Warning:** You _cannot_ nest `UseSpecificDateTimeUtc`. This will cause an `InvalidOperationException` to be thrown.