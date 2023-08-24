using Microsoft.EntityFrameworkCore;
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using WebApi.WebApi.Database;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class ImageRepo : IImageRepo
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<Image> _dbSet;
        public ImageRepo(DatabaseContext dbContext)
        {
            _databaseContext = dbContext;
            _dbSet = dbContext.Set<Image>();
        }
        public async Task<Image> AddImageAsync(Image image)
        {
            await _databaseContext.Images.AddAsync(image);
            await _databaseContext.SaveChangesAsync();
            return image;
        }
    }
}