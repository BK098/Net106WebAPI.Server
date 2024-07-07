﻿using Application.Services.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Repositories.Repositories.Base;

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
        public async Task<bool> IsUniqueComboName(string name, CancellationToken cancellationToken)
        {
            var combo = await _context.Combos
                .FirstOrDefaultAsync(p => p.Name == name);

            return combo == null;
        }
        public async Task<bool> IsProductItemExist(Guid comboId, Guid productId, CancellationToken cancellationToken)
        {
            var combo = await _context.Combos
            .Include(x => x.ProductItems)
            .FirstOrDefaultAsync(p => p.Id == comboId, cancellationToken);

            return combo?.ProductItems.Any(x => x.ProductId == productId) ?? false;
        }
        public async Task<IEnumerable<Combo>> GetAllCombosAsync()
        {
            IEnumerable<Combo> combos = await _context.Combos.Include(x => x.ProductItems)
             .ThenInclude(x => x.Product)
             .ToListAsync();
            return combos;
        }
        public async Task<Combo> GetComboByIdAsync(Guid id)
        {
            return await _context.Combos.Include(x => x.ProductItems)
           .ThenInclude(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);
        }
        #endregion
    }
}
