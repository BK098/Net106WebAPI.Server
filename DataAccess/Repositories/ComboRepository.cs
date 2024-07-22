using Application.Services.Contracts.Repositories;
using Application.Services.Models.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;
using System.Linq.Expressions;

namespace Repositories.Repositories
{
    public class ComboRepository : GeneralReposotory<Combo>, IComboRepository
    {
        public ComboRepository(ApplicationDbContext context) : base(context) { }
        #region Command
        public async Task<Combo> CreateComboAsync(Combo combo)
        {
            return await CreateAsync(combo);
        }
        public bool DeleteCombo(Combo combo)
        {
            Delete(combo);
            return true;
        }
        public Combo UpdateCombo(Combo combo)
        {
            return Update(combo);
        }
        #endregion

        #region Queries
        public async Task<bool> IsUniqueComboName(string name)
        {
            var combo = await _context.Combos
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
            return combo == null;
        }
        public async Task<bool> IsProductComboExist(Guid comboId, Guid? productId)
        {
            var combo = await _context.Combos
                .Include(x => x.ProductCombos)
                .FirstOrDefaultAsync(p => p.Id == comboId);

            return combo?.ProductCombos.Any(x => x.ProductId == productId) ?? false;
        }
        public async Task<IEnumerable<Combo>> GetAllCombosAsync()
        {
            IEnumerable<Combo> combos = await _context.Combos.Include(x => x.ProductCombos)
                .ThenInclude(x => x.Product)
                .ToListAsync();
            return combos;
        }
        public async Task<Combo> GetComboByIdAsync(Guid id)
        {
            return await _context.Combos
                .Include(x => x.ProductCombos)
                .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        public IQueryable<Combo> GetAllCombosAsync(SearchBaseModel model, CancellationToken cancellationToken)
        {
            var comboQuery = _context.Combos.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                comboQuery = comboQuery.Where(p =>
                    p.Name.ToLower().Contains(model.SearchTerm.ToLower()));
            }

            if (model.SortOrder?.ToLower() == "desc")
            {
                comboQuery = comboQuery.OrderByDescending(GetSortProperty(model.SortColumn));
            }
            else
            {
                comboQuery = comboQuery.OrderBy(GetSortProperty(model.SortColumn));
            }

            return comboQuery;
        }
        private static Expression<Func<Combo, object>> GetSortProperty(string? sortColumn)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return combo => combo.Id;
            }
            return sortColumn?.ToLower() switch
            {
                "name" => combo => combo.Name,
                "price" => combo => combo.Price,
                "discount" => combo => combo.Discount,
                _ => combo => combo.Id
            };
        }
        #endregion
    }
}
