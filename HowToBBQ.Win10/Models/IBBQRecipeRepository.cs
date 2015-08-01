using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToBBQ.Win10.Models
{
     public interface IBBQRecipeRepository
    {
        IEnumerable<BBQRecipe> GetAll();
        BBQRecipe Get(int id);
        BBQRecipe Add(BBQRecipe item);
        void Remove(int id);
        bool Update(BBQRecipe item);
    }
}
