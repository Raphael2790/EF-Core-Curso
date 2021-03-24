using AprendendoEntityFramework;
using AprendendoEntityFramework.ConfigurationModels;
using AprendendoEntityFramework.Repositorios;
using AprendendoEntityFramework.Repositorios.Interfaces;
using AprendendoEntityFramework.Services;
using AprendendoEntityFramework.Services.Interfaces;
using AprendendoEntityFrmaework.BankContext;
using AprendendoEntityFrmaework.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AprendendoEntityFrmaework
{
    class Program
    {
        private static AppSettingsModel _appSettings;
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            RegisterConfiguration();
            var productRepository = _serviceProvider.GetService<IProductRepository>();

            //A partir de uma pasta raiz será provido os arquivos
            var fileService = _serviceProvider.GetService<IFileProvider>();

            var products = productRepository.GetProducts().ToList();

            foreach (var item in products)
            {
                Console.WriteLine(item.ProductName);
            }

            var productService = _serviceProvider.GetService<IProductService>();
            var lowerPrices = productService.GetProductsWithLowerPrice();
            foreach (var item in lowerPrices)
            {
                Console.WriteLine(item.ProductName);
            }

            DisposeServices();


            //GetLastProduct();
            //Console.WriteLine(CalculateAveragePriceByCategorie("higiene"));
            //Console.WriteLine(MaxPriceByCategorie("higiene"));
            //Console.WriteLine(CountProductsByCategorie("higiene"));
            //RegisterProductWithCategorie();
            //AddCategorieToProduct();
            //GetProductsByCategory("higiene");
            //GroupByCategorie();
            //ShowAllCategoriesAndProducts();
            //ShowAllProductsAndCategories();
            //CrossJoin();
            //GetCategoriesWithProductLazyLoad();
            //GetProductsWithCategorieEagerLoad();
            //RegisterProduct(new Product {ProductName="Massa para Lasanha", ProductPrice=7.89M, Category = null });
            //RegisterProduct(new Product { ProductName = "Sabonete", ProductPrice = 0.89M, CategoryID=1 });
            //RegisterProduct(new Product { ProductName = "Sabonete Líquido", ProductPrice = 5.99M, CategoryID = 1 });
            //RegisterProduct(new Product { ProductName = "Creme Dental Colgate", ProductPrice = 12.89M , CategoryID = 1});
            //Console.WriteLine("======================================\n");
            //ListOfRegions();
            //ListOfProducts();
            //ListOfRegions();
            //ListOfRegionsCompiledQuery();
            //RemoveProduct();
            //RegiteringRegions();
            //RegisterClient(new Client { Name = "Juliano", Address = new List<Address>() { new Address { StreetName = "Rua Bartolomeu Fonseca", City = "São Paulo", Country = "Brasil", ZipCode = "01010-011" } } });
        }

        private static void RegisterServices()
        {
            var servicesCollection = new ServiceCollection()
                            .AddDbContext<ProductsRegionDbContext>(options => options.UseSqlServer(_appSettings.ConnectionStrings.DefaultConnection))
                            .AddScoped<IProductRepository, ProductRepository>()
                            .AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()))
                            .AddScoped<IProductService, ProductService>()
                            .AddScoped(service => new ClientContext());


            _serviceProvider = servicesCollection.BuildServiceProvider();
        }

        private static void RegisterConfiguration()
        {
            _appSettings = new AppSettingsHandler(_serviceProvider.GetService<IFileProvider>())
                                    .GetAppSettings();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

        private static void RegisterClient(Client client)
        {
            using (var context = new ClientContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        private static void UpdateProductDetached()
        {
            //Podemos usar o update para atualizar entidades detached que já possuem seu identificador único
            //Porém não podemos realizar consultas no contexto para a entidade no banco com mesmo id
            //Existe também o metodo UpdateRange para coleção de entidades a serem atualizadas que possuam identificadores
            //Caso um ou mais entidades não possua identificador será feito a inclusão
            var product = new Product { ProductId = 8, ProductName = "Macarrão Espaguete n 6", CategoryID = 3 };

            using (var db = new ProductsRegionDbContext())
            {
                if (db.Entry(product).IsKeySet)
                {
                    var result = db.Products.Update(product);
                }
                else
                {
                    db.Products.Add(product);
                }

                db.SaveChanges();
            }
        }

        private static void GetClientsWithNegativeBalanceUsingScalarFunction()
        {
            using (var context = new BankDbContext())
            {
                try
                {
                    context.Clients
                                    .Include(x => x.Account)
                                    .Where(x => BankDbContext.ClientsWithNegativeBalance(x.ClientId) < 0)
                                    .ToList();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void CreatingScalarFunction()
        {
            using (var context = new BankDbContext())
            {
                try
                {
                    const string sql = @"CREATE FUNCTION [ClientsWithNegativeBalance]
                                    (
                                        @ClientId INT
                                    )
                                                RETURNS DECIMAL
                                                AS
                                                BEGIN
                                                   DECLARE @Result AS DECIMAL
                                                   SELECT @Result = Account.AccountBalance FROM Account
                                                   WHERE Account.ClientId = @ClientId
                                                   RETURN @Result
                                                END";

                    context.Database.ExecuteSqlRaw(sql);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void InsertProductsWithDiferentContexts()
        {
            var connectionString = "Data Source=DESKTOP-083D5DN\\SQLEXPRESS;Initial Catalog=CursoEFCoreBD;Integrated Security=True;Pooling=False";
            var options = new DbContextOptionsBuilder<ProductsRegionDbContext>()
                .UseSqlServer(new SqlConnection(connectionString)).Options;

            using (var db = new ProductsRegionDbContext(options))
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var product = new Product { ProductId = 1, ProductName = "Arroz 5kg", ProductPrice = 18.00M };

                        db.Products.Add(product);

                        var product2 = db.Products.Find(2);
                        product2.ProductName = "Macarrão Instantaneo";
                        db.SaveChanges();

                        using (var db2 = new ProductsRegionDbContext())
                        {
                            db2.Products.Add(product);
                            db2.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }

        private static void InsertProductsWithTransaction()
        {
            using(var db = new ProductsRegionDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var product = new Product { ProductId = 1, ProductName = "Arroz 5kg", ProductPrice = 18.00M };

                        db.Products.Add(product);

                        var product2 = db.Products.Find(2);
                        product2.ProductName = "Macarrão Instantaneo";
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }

                }
            }
        }

        private static void RemoveProductDetached(int id)
        {
            //Se a entidade já possuir o identificador único atribuido podemos chamar apenas o remove
            var product = new Product { ProductId = id };

            using (var db = new ProductsRegionDbContext())
            {
                try
                {
                    //UpdateEntityState(product, db, EntityState.Deleted);
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    throw new Exception(ex.Message);
                }
                
            }
        }

        private static void UpdateEntityState<T>(T entity, DbContext context, EntityState state) where T : class
        {
            var entityName = entity.GetType().Name;
            Console.WriteLine($"Atualizando entidade {entityName}");
            context.Entry(entity).State = state;
        }

        private static void RemoveProduct()
        {
            using (var context = new ProductsRegionDbContext())
            {
                var product = context.Products.Find(1);
                context.Remove(product);
                context.SaveChanges();
            }
        }

        private static void ListOfRegions()
        {
            //Como se fosse um cronometro para execução de um código de um ponto para o outro
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var db = new ProductsRegionDbContext())
            {
                var regions = db.Regions.AsNoTracking().ToList();

                foreach (var item in regions)
                {
                    Console.WriteLine($"{item.RegionId} {item.RegionDescription}");
                    Console.WriteLine(db.Entry(item).State);
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"Consulta Normal => gastou {stopwatch.ElapsedMilliseconds.ToString().PadLeft(4)}ms");
        }

        private static void ListOfRegionsCompiledQuery() 
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            //O beneficio de usar uma compiled query é quando fazemos a mesma chamada várias vezes
            //Porém ao realizar a mesma chamada de query 
            var consultaCompilada = EF.CompileQuery(
                (ProductsRegionDbContext ctx) => 
                    ctx.Regions.AsNoTracking().ToList());

            using (var context = new ProductsRegionDbContext())
            {
                var regions = consultaCompilada(context).ToList();

                foreach (var item in regions)
                {
                    Console.WriteLine($"{item.RegionId} {item.RegionDescription}");
                    Console.WriteLine(context.Entry(item).State);
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"Consulta Normal => gastou {stopwatch.ElapsedMilliseconds.ToString().PadLeft(4)}ms");
        }

        private static ICollection<Product> GetProductsWithCategorieEagerLoad()
        {
            //usando Include do linq para trazer junto com os produtos suas Categorias
            //existe ainda a possibilidade de incluir navegação para propriedades dentro do primeiro encadeamento de include
            using (var db = new ProductsRegionDbContext())
            {
                return db.Products
                    .Include(x => x.Category)
                    .IgnoreQueryFilters() //caso a entidade possua filtros automaticos para consultas irá ignorar
                    //.ThenInclude(x => x.Products) //Nesse caso ficariamos com uma navegação em circulo
                    .ToList();
            }
        }

        private static ICollection<Category> GetCategoriesExplicitLoad()
        {
            using (var db = new ProductsRegionDbContext())
            {
                var categories = db.Categories.ToList();

                var product = db.Products.Find(2);

                if(!db.Entry(product).Reference(x => x.Category).IsLoaded)
                    db.Entry(product).Reference(x => x.Category).Load();

                foreach (var category in categories)
                {
                    if (!db.Entry(category).Reference(x => x.Products).IsLoaded)
                        db.Entry(category).Collection(p => p.Products)
                            .Query() // executar um filtro antes de trazer o carregamento explicito
                            .Where(x => x.CategoryID == product.CategoryID)
                            .Load();
                }
                return categories;
            }
        }

        private static ICollection<Category> GetCategoriesWithProductLazyLoad()
        {
            //o lazy load acontece ao instalar o pacote Entity Proxies e definir prodpriedades de navegação com virtual 
            using (var db = new ProductsRegionDbContext())
            {
                var categories = db.Categories.ToList();
                return categories;
            }
        }

        private static void UpdatingByTrackingGraph(ICollection<Category> categories)
        {
            using (var context = new ProductsRegionDbContext())
            {
                context.ChangeTracker.TrackGraph(categories, c => 
                {
                    if (c.Entry.IsKeySet)
                        c.Entry.State = EntityState.Unchanged;

                    if (!c.Entry.IsKeySet)
                        c.Entry.State = EntityState.Added;
                });

                context.SaveChanges();
            }
        }

        private static Func<Product,bool> GetProductsFunc(string categorie)
        {
            return x => x.Category.CategoryName.ToLower() == categorie.ToLower();
        }

        private static int CountProductsByCategorie(string categorie)
        {
            using (var db = new ProductsRegionDbContext())
            {
                int count;

                var products = db.Products
                               .Join(db.Categories, 
                               x => x.CategoryID, 
                               x => x.CategoryID, 
                               (product, categorie ) => new Product
                               {
                                   ProductId = product.ProductId,
                                   ProductName = product.ProductName,
                                   ProductPrice = product.ProductPrice,
                                   Category = categorie,
                                   CategoryID = categorie.CategoryID
                               }).ToList();

                var productsQuerySyntax = (from p in db.Products
                                           join category in db.Categories
                                           on p.CategoryID equals category.CategoryID
                                           select p).ToList();

                count = products.Any(GetProductsFunc(categorie))
                    ? products.Where(GetProductsFunc(categorie)).Count()
                    : 0;

                return count;
            }
        }

        private static void RegisterProductWithCategorie()
        {
            using (var db = new ProductsRegionDbContext())
            {
                var product = new Product 
                { 
                    ProductName = "Macarrão Espaguete n 8", 
                    ProductPrice = 3.80M, 
                    Category = new Category 
                    { 
                        CategoryName = "Alimentícios" 
                    } 
                };

                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        private static void AddCategorieToProduct()
        {
            using (var db = new ProductsRegionDbContext())
            {
                var product = (from p in db.Products
                               where p.ProductName.Contains("Massa")
                               select p).FirstOrDefault();

                product.Category = db.Categories.FirstOrDefault(x => x.CategoryName.Contains("Alimentícios"));

                db.SaveChanges();
            }
        }

        private static void RegisterProduct(Product product)
        {
            using (var db = new ProductsRegionDbContext())
            {
                db.Entry<Product>(product).State = EntityState.Added;
                db.Set<Product>().Add(product);
                db.SaveChanges();
            }
        }

        private static void ShowAllCategoriesAndProducts()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Exemplo de leftJoin usando query e method syntax
                var categories = (from c in db.Categories
                                  join product in db.Products
                                  on c.CategoryID equals product.CategoryID
                                  into allCategories
                                  from p in db.Products.DefaultIfEmpty()
                                  select new
                                  {
                                      c.CategoryName,
                                      p.ProductName
                                  }).ToList();

                var categoriesMethodSyntax = db.Categories
                                            .GroupJoin(db.Products,
                                            x => x.CategoryID
                                            , x => x.CategoryID,
                                            (categories, product) => new
                                            {
                                                categories,
                                                product
                                            }).SelectMany(x => x.product.DefaultIfEmpty(),
                                            (categorie, product) => new 
                                            { 
                                                categorie.categories.CategoryName, 
                                                product.ProductName 
                                            });
                                            

                foreach (var item in categoriesMethodSyntax)
                {
                    Console.Write(item.CategoryName + "\t\t");
                    Console.WriteLine(item.ProductName);
                }
            }
        }

        private static void ShowAllProductsAndCategories()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Linq não dá suporte a rightjoin então simplesmente invertemos a ordem das tabelas
                var allProductsAndCategories = (from p in db.Products
                                                join c in db.Categories
                                                on p.CategoryID equals c.CategoryID
                                                into products
                                                from cat in db.Categories.DefaultIfEmpty()
                                                select new
                                                {
                                                    p.ProductName,
                                                    cat.CategoryName
                                                });

                var allProductsAndCategoriesMethod = db.Products
                                                     .GroupJoin(db.Categories,
                                                     x => x.CategoryID,
                                                     x => x.CategoryID,
                                                     (products, categories) => new
                                                     {
                                                         products,
                                                         categories
                                                     }).SelectMany(x => x.categories.DefaultIfEmpty(),
                                                     (product, category) => new
                                                     {
                                                         product.products.ProductName,
                                                         category.CategoryName
                                                     });


                foreach (var item in allProductsAndCategoriesMethod)
                {
                    Console.Write(item.ProductName + "\t\t");
                    Console.WriteLine(item.CategoryName);
                }
            }
        }

        private static void FullOutterJoin()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Full Outer Join união de left join e right join, a ordem dos itens deve ser o mesmo
                var allProductsAndCategories = (from p in db.Products
                                                join c in db.Categories
                                                on p.CategoryID equals c.CategoryID
                                                into products
                                                from cat in db.Categories.DefaultIfEmpty()
                                                select new
                                                {
                                                    cat.CategoryName,
                                                    p.ProductName
                                                }).AsParallel();

                var categories = (from c in db.Categories
                                  join product in db.Products
                                  on c.CategoryID equals product.CategoryID
                                  into allCategories
                                  from p in db.Products.DefaultIfEmpty()
                                  select new
                                  {
                                      c.CategoryName,
                                      p.ProductName
                                  }).AsParallel();

                var unionJoin = allProductsAndCategories.Union(categories);

                foreach (var item in unionJoin)
                {
                    Console.WriteLine(item.CategoryName);
                    Console.WriteLine(item.ProductName);
                }
            }
        }

        private static void CrossJoin()
        {
            using (var db = new ProductsRegionDbContext())
            {
                var croosJoin = (from p in db.Products
                                 from c in db.Categories
                                 select new
                                 {
                                     ProductName = p.ProductName,
                                     CategoryName = c.CategoryName
                                 });

                var crossJoinMethod = db.Categories
                                      .Join(db.Products, x => true, y => true,
                                      (x, y) => new
                                      {
                                          y.ProductName,
                                          x.CategoryName
                                      });

                foreach (var item in crossJoinMethod)
                {
                    Console.Write(item.CategoryName + "\t\t");
                    Console.WriteLine(item.ProductName);
                }
            }
        }

        private static void RegiteringRegions()
        {
            var listOfRegions = new List<Region>()
            {
                new Region { RegionDescription = "Zona Norte"},
                new Region { RegionDescription = "Zona Sul"},
                new Region { RegionDescription = "Zona Leste"},
                new Region { RegionDescription = "Zona Oeste"},
                new Region { RegionDescription = "Centro"}

            };

            using (var db = new ProductsRegionDbContext())
            {
                db.Regions.AddRange(listOfRegions);
                db.SaveChanges();
            }
        }

        private static ICollection<Product> GetProductsOrderedByName()
        {
            using(var db = new ProductsRegionDbContext())
            {
                var productOrdered = (from product in db.Products
                                      orderby product.ProductName 
                                      select product).AsNoTracking().ToList();

                var produscts = db.Products
                                    .Include(x => x.Category)
                                    .Select(x => new { x.ProductName, x.Category, x.ProductPrice })
                                    .OrderBy(x => x.ProductName)
                                    .ThenBy(x => x.Category.CategoryName)
                                    .AsNoTracking()
                                    .ToList();

                return productOrdered;
            }
        }

        private static ICollection<Product> GetProductsByCategory(string category)
        {
            //Select many tira lista dentro de outros objetos
            using (var db = new ProductsRegionDbContext())
            {
                return db.Categories
                    .Where(x => x.CategoryName.ToLower() == category.ToLower())
                    .SelectMany(x => x.Products).AsNoTracking().ToList();
            }
        }

        private static void GroupByCategorie()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Para realizar GroupJoin temos que executar a consulta antes para ter disponivel a lista ou enumerador
                var productGroups = db.Products
                                    .Include(x => x.Category)
                                    .AsNoTracking()
                                    .ToList()
                                    .GroupBy(x => x.Category.CategoryName, x => new { x.ProductName, x.ProductPrice });

                foreach (var grupo in productGroups)
                {
                    Console.WriteLine(grupo.Key);

                    foreach (var item in grupo)
                    {
                        Console.Write(item.ProductName + "\t");
                        Console.WriteLine(item.ProductPrice);
                    }
                }
            }
        }

        private static int GetProductsExecutingSql()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Podemos usar comandos sql diretos, podemos usar para aumentar a performance da consulta
                var products = db.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM Products");

                return products;
            }
        }

        private static Product GetLastProduct()
        {
            using (var db = new ProductsRegionDbContext())
            {
                //Last ou LastOrDefault exige um clausula order by pq inverte a ordem da consulta pegando o top(1) inverso
                var product = db.Products.OrderBy(x => x.ProductId).LastOrDefault();

                var lastProduct = (from p in db.Products
                                   orderby p.ProductId
                                   select p).LastOrDefault();
                return lastProduct;
            }

        }

        private static string MaxPriceByCategorie(string categorie)
        {
            using (var db = new ProductsRegionDbContext())
            {
                return db.Products
                    .Where(p => p.Category.CategoryName == categorie)
                    .AsNoTracking()
                    .Max(x => x.ProductPrice)
                    .ToString("c");
            }
        }

        private static string CalculateAveragePriceByCategorie(string category)
        {
            using (var db = new ProductsRegionDbContext())
            {
                return db.Products
                    .Where(p => p.Category.CategoryName
                    .ToLower() == category.ToLower())
                    .AsNoTracking()
                    .Average(p => p.ProductPrice)
                    .ToString("c");
            }
        }

        private static ICollection<Product> GetProductsWithPriceGreaterThan(decimal price)
        {
            using (var db = new ProductsRegionDbContext())
            {
                var products = (from p in db.Products
                                where p.ProductPrice >= price
                                orderby p.ProductName
                                select p).AsNoTracking().ToList();

                var productsMethodSyntax = db.Products
                                            .Where(x => x.ProductPrice >= price)
                                            .OrderBy(x => x.ProductName)
                                            .Select(x => x).AsNoTracking().ToList();

                return products;
            }
        }

        private static void ListOfProducts()
        {
            using (var db = new ProductsRegionDbContext())
            {
                var products = db.Products.AsNoTracking().ToList();

                foreach (var item in products)
                {
                    Console.WriteLine($"{item.ProductName} {item.ProductPrice.ToString("c")} {item.Category.CategoryID}");
                }

                foreach (var e in db.ChangeTracker.Entries<Product>())
                {
                    Console.WriteLine(e.State);
                }
            }
        }
    }
}
