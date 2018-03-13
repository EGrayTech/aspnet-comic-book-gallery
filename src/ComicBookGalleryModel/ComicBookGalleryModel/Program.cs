﻿using ComicBookGalleryModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookGalleryModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                //Series Variables
                var series1 = new Series()
                {
                    Title = "The Amazing Spider-Man"
                };
                var series2 = new Series()
                {
                    Title = "The Invincible Iron Man"
                };

                //Artist Variables
                var artist1 = new Artist()
                {
                    Name = "Stan Lee"
                };
                var artist2 = new Artist()
                {
                    Name = "Steve Ditko"
                };
                var artist3 = new Artist()
                {
                    Name = "Jack Kirby"
                };

                //Artist Roles Variables
                var role1 = new Role()
                {
                    Name = "Script"
                };
                var role2 = new Role()
                {
                    Name = "Pencils"
                };

                //Individual Comic Books
                var comicBook1 = new ComicBook()
                {
                    Series = series1,
                    IssueNumber = 1,
                    PublishedOn = DateTime.Today
                };
                comicBook1.AddArtist(artist1, role1);
                comicBook1.AddArtist(artist2, role2);

                var comicBook2 = new ComicBook()
                {
                    Series = series1,
                    IssueNumber = 2,
                    PublishedOn = DateTime.Today
                };
                comicBook2.AddArtist(artist1, role1);
                comicBook2.AddArtist(artist2, role2);

                var comicBook3 = new ComicBook()
                {
                    Series = series2,
                    IssueNumber = 1,
                    PublishedOn = DateTime.Today
                };
                comicBook3.AddArtist(artist1, role1);
                comicBook3.AddArtist(artist3, role2);

                //Adding the comic books
                context.ComicBooks.Add(comicBook1);
                context.ComicBooks.Add(comicBook2);
                context.ComicBooks.Add(comicBook3);

                context.SaveChanges();

                //Rest of code
                var comicBooks = context.ComicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists)
                    .ToList();

                foreach (var comicBook in comicBooks)
                {
                    var artistNames = comicBook.Artists.Select(a => a.Name).ToList();

                    var artistsDisplayText = string.Join(", ", artistNames);

                    Console.WriteLine(comicBook.DisplayText);
                    Console.WriteLine(artistsDisplayText);
                }

                Console.ReadLine();
            }
        }
    }
}
