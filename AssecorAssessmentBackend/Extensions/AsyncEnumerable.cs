namespace assecor_assessment_backend.Extensions;

public static class AsyncEnumerable
{
    public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> input)
    {
        foreach(var value in input)
        {
            yield return value;
        }
    }
}