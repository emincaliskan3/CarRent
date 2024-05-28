using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ICategoryService
    {   // Burada CategoryService classında  kategorilerle ilgili tüm crud işlemlerini yapabilmemizi sağlayan metotları imzalarını belirliyoruz
        List<Category> GetCategories();
        Category GetCategory(int id);
        Category GetCategoryByPosts(int id);
        Category GetCategoryByCar(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        int Save();

    }
}

