namespace Uspa.Domain.Settings
{
    public static class PagingSettings
    {
        public static int PageSizeInCategories { get; } = 20;
        public static int PageSizeInContent { get; } = 5;
        public static int PageSizeInMenu { get; } = 20;
        public static int PageSizeInUsers { get; } = 10;
        public static int PageSizeInAlbum { get; } = 10;
    }
}
