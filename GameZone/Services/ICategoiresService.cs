namespace GameZone.Services
{
    public interface ICategoiresService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
