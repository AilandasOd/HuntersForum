using HuntersForum.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HuntersForum.Data
{
    public static class SeedData
    {
        public static async Task Seed(DbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            string dateString = "2024-05-12";

            var firstTopic = new Topic()
            {
                Id = 1,
                Title = "Kokias kameras naudojate žvėrių stebėjimui?",
                Description = "Diskusija apie naudojamas stebėjimo kameras.",
                CreatedAt = DateTime.Parse(dateString),
            };

            var secondTopic = new Topic()
            {
                Id = 2,
                Title = "Medžiojamų elnių skaičius.",
                Description = "Po taisyklių pakeitimo, vykdoma diskusija apie elnių medžiojimo kiekį.",
                CreatedAt = DateTime.Parse(dateString),
            };


            var firstPost = new Post()
            {
                Id = 1,
                Content = "Naudoju Bolyguard firmos kamera.",
                CreatedAt = DateTime.Parse(dateString),
                TopicId = firstTopic.Id,
            };

            var secondPost = new Post()
            {
                Id = 2,
                Content = "Mūsų būrelis nusprendė palikti tokius pat skaičius.",
                CreatedAt = DateTime.Parse(dateString),
                TopicId = secondTopic.Id,
            };

            var firstComment = new Comment()
            {
                Id = 1,
                Content = "Ar pasiteisino?",
                CreatedAt = DateTime.Parse(dateString),
                PostId = firstTopic.Id,
            };

            var secondComment = new Comment()
            {
                Id = 2,
                Content = "Keista, pas mus šis skaičius buvo padidintas.",
                CreatedAt = DateTime.Parse(dateString),
                PostId = secondTopic.Id,
            };

            var topics = new List<Topic>() { firstTopic, secondTopic };
            var posts = new List<Post>() { firstPost, secondPost};
            var comments = new List<Comment>() { firstComment, secondComment };
            

            await context.AddRangeAsync(topics);
            await context.AddRangeAsync(posts);
            await context.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }
    }
}