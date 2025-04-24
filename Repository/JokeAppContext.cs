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
                authorModel.HasKey(x => x.Id);
                
                // maps the model properties to the columns in the database
                authorModel.Property(x => x.Id).HasColumnName("id");
                authorModel.Property(x => x.Name).HasColumnName("name");
                authorModel.Property(x => x.Age).HasColumnName("age");
            });

            modelBuilder.Entity<Category>(categoriesModel => 
            {
                categoriesModel.ToTable("categories");

                categoriesModel.HasKey(x => x.Id);

                categoriesModel.Property(x => x.Id).HasColumnName("id");
                categoriesModel.Property(x => x.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Joke>(jokesModel => {

                jokesModel.ToTable("jokes");

                jokesModel.HasKey(x => x.Id);
                
                jokesModel.Property(x => x.Id).HasColumnName("id");
                jokesModel.Property(x => x.AuthorId).HasColumnName("author_id");
                jokesModel.Property(x => x.CategoryId).HasColumnName("category_id");
                jokesModel.Property(x => x.JokeText).HasColumnName("joke");
                jokesModel.Property(x => x.CreatedAt).HasColumnName("created_at");
                jokesModel.Property(x => x.IsApproved).HasColumnName("is_approved");
                jokesModel.Property(x => x.Likes).HasColumnName("likes");
                jokesModel.Property(x => x.Dislikes).HasColumnName("dislikes");

                // Foreign Key Relationships
                jokesModel.HasOne(j => j.Author)
                    .WithMany(a => a.Jokes)
                    .HasForeignKey(j => j.AuthorId);

                jokesModel.HasOne(j => j.Category)
                    .WithMany(c => c.Jokes)
                    .HasForeignKey(j => j.CategoryId);
            });
        }
    }
}