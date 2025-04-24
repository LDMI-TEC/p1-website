using Microsoft.EntityFrameworkCore;
using poke_poke.Models.Jokes;

namespace poke_poke.Repository
{
    public class JokeAppContext : DbContext
    {
        public JokeAppContext(DbContextOptions<JokeAppContext> options) : base(options)
        {

        }

        public DbSet<Author> authors{ get; set; }
        public DbSet<Category> categories{ get; set; }
        public DbSet<Joke> jokes{ get; set; }

        // map the tables in the db
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(authorModel => 
            {
                // maps to table
                authorModel.ToTable("authors");    

                // defines what part of the model is the primary key
                authorModel.HasKey(x => x.id);
                
                // maps the model properties to the columns in the database
                authorModel.Property(x => x.id).HasColumnName("id");
                authorModel.Property(x => x.name).HasColumnName("name");
                authorModel.Property(x => x.age).HasColumnName("age");
            });

            modelBuilder.Entity<Category>(categoriesModel => 
            {
                categoriesModel.ToTable("categories");

                categoriesModel.HasKey(x => x.id);

                categoriesModel.Property(x => x.id).HasColumnName("id");
                categoriesModel.Property(x => x.name).HasColumnName("name");
            });

            modelBuilder.Entity<Joke>(jokesModel => {

                jokesModel.ToTable("jokes");

                jokesModel.HasKey(x => x.id);
                
                jokesModel.Property(x => x.id).HasColumnName("id");
                jokesModel.Property(x => x.authorId).HasColumnName("author_id");
                jokesModel.Property(x => x.categoryId).HasColumnName("category_id");
                jokesModel.Property(x => x.joke).HasColumnName("joke");
                jokesModel.Property(x => x.createAt).HasColumnName("created_at");
                jokesModel.Property(x => x.isApproved).HasColumnName("is_approved");
                jokesModel.Property(x => x.likes).HasColumnName("likes");
                jokesModel.Property(x => x.dislikes).HasColumnName("dislikes");

                // Foreign Key Relationships
                jokesModel.HasOne(j => j.author)
                    .WithMany(a => a.jokes)
                    .HasForeignKey(j => j.authorId);

                jokesModel.HasOne(j => j.category)
                    .WithMany(c => c.jokes)
                    .HasForeignKey(j => j.categoryId);
            });
        }
    }
}