using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Budget
{
    public class ListBudgetViewModel
    {
        public List<BusinessLogic.Models.Budget> Budgets { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
