namespace BeaverTinder.Clickhouse.Helpers;

public static class AllDaysViewsHelper
{
    public static long? AllDaysViewsWithoutToday { get; set; } = null;
    public static DateTime? LastDay { get; set; } = null;
}