using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeachImageWebsite.Models.ViewModel
{
    public class RecipeViewModel//這個vm裡面含有哪些model
    {
        
            public aRecipe aRecipe { get; set; }
            public List<Ingredient> ingredients { get; set; }
            public List<Steps> steps { get; set; }
            }
}