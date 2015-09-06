using System.Collections.Generic;

namespace HowToBBQ.Win10.Models
{
    public interface IBBQRecipeRepository
    {
        IEnumerable<BBQRecipe> GetAll();
        BBQRecipe Get(string id);
        BBQRecipe Add(BBQRecipe item);
        void Remove(string id);
        bool Update(BBQRecipe item);
    }
}
