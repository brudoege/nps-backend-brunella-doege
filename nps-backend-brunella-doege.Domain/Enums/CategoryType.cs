namespace nps_backend_brunella_doege.Domain.Enums;

public enum CategoryType
{
    OTHER,
    PRODUCTACCESS,
    SLOWNESS,
    INTERFACE,
    BUGS,
    CONNECTIVITY
}

public static class CategoryTypeExtensions
{
    public static Guid ToGuid(this CategoryType categoryType)
    {
        return categoryType switch
        {
            CategoryType.OTHER => Guid.Parse("8656aec6-9f0f-41e1-a94c-49e2d49a5492"),
            CategoryType.PRODUCTACCESS => Guid.Parse("e0001d6c-905e-42a0-8f2c-89184a6225da"), 
            CategoryType.SLOWNESS => Guid.Parse("ab7e4d23-ce17-4049-9856-9f1cea110a7e"),
            CategoryType.INTERFACE => Guid.Parse("438109f9-c8bf-43b1-94a0-a186b758b1e1"),
            CategoryType.BUGS => Guid.Parse("883fdf80-70a2-4e36-bf0a-a291c1174cba"),
            CategoryType.CONNECTIVITY => Guid.Parse("25301326-f806-42e7-9fd9-4ea1e0ddf396"),
            _ => throw new ArgumentOutOfRangeException(nameof(categoryType), $"Not expected category: {categoryType}")
        };
    }

    public static CategoryType FromGuid(Guid? categoryId)
    {
        return categoryId switch
        {
            var g when g == Guid.Parse("8656aec6-9f0f-41e1-a94c-49e2d49a5492") => CategoryType.OTHER,
            var g when g == Guid.Parse("e0001d6c-905e-42a0-8f2c-89184a6225da") => CategoryType.PRODUCTACCESS,
            var g when g == Guid.Parse("ab7e4d23-ce17-4049-9856-9f1cea110a7e") => CategoryType.SLOWNESS,
            var g when g == Guid.Parse("438109f9-c8bf-43b1-94a0-a186b758b1e1") => CategoryType.INTERFACE,
            var g when g == Guid.Parse("883fdf80-70a2-4e36-bf0a-a291c1174cba") => CategoryType.BUGS,
            var g when g == Guid.Parse("25301326-f806-42e7-9fd9-4ea1e0ddf396") => CategoryType.CONNECTIVITY,
            _ => throw new ArgumentOutOfRangeException(nameof(categoryId), $"Not expected Guid: {categoryId}")
        };
    }
}

