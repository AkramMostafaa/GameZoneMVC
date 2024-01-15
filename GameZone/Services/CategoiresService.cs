
namespace GameZone.Services
{
    public class CategoiresService : ICategoiresService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoiresService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
          return  _dbContext.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(c => c.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
