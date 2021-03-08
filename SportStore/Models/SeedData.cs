using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Каяк", Discription = "Одноместное судно", Category = "ВодныйСпорт", Price = 275 },
                    new Product { Name = "Спасательный жилет", Discription = "Не дает утонуть", Category = "ВодныйСпорт", Price = 48.95m},
                    new Product { Name = "Футбольный мяч", Discription = "Соответствует стандаотам ФИФА", Category = "Футбол", Price = 19.50m},
                    new Product { Name = "Угловой флаг", Discription = "Придаст вашей площадке шарм профессионализма", Category = "Футбол", Price = 34.95m},
                    new Product { Name = "Стадион", Discription = "Вместимость 25000 человек и целая сетка", Category = "Футбол", Price = 4050000m },
                    new Product { Name = "Клюшка для хоккея с мячом", Discription = "Легкая и прочная профессиональная клюшка", Category = "Хоккей_с_мячом", Price = 4600m},
                    new Product { Name = "Мяч для хоккея с мячом", Discription = "Официальный мяч чемпионата мира в Иркутске 2021", Category = "Хоккей_с_мячом", Price = 1000m },
                    new Product { Name = "Коньки хоккейные", Discription = "Профессиональные коньки, чрезвычайно легкие и удобные", Category = "Хоккей_с_мячом", Price = 15000m },
                    new Product { Name = "Щитки хоккейные", Discription = "Профессиональные щитки защитят вас во время тренировок и игр", Category = "Хоккей_с_мячом", Price = 5500m}
                    );
                context.SaveChanges();
            }
        }
    }
}
