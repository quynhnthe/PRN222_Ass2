﻿using Microsoft.EntityFrameworkCore;
using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly FunewsManagementContext _context;

        public NewsRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        public void Create(NewsArticle model)
        {
            _context.NewsArticles.Add(model);
            _context.SaveChanges();
        }

        public void Update(NewsArticle model)
        {
            _context.NewsArticles.Update(model);
            _context.SaveChanges();
        }

        public void Delete(NewsArticle model)
        {
            _context.NewsArticles.Remove(model);
            _context.SaveChanges();
        }

        public NewsArticle? FindById(string id)
        {
            return _context.NewsArticles
                           .Include(n => n.Category )
                           .Include(c => c.Comments)
                           .ThenInclude(u => u.User)
                           .Include(x => x.CreatedBy)// Nạp thông tin Category
                           .FirstOrDefault(n => n.NewsArticleId == id);
        }
        public List<NewsArticle> GetAll() => _context.NewsArticles.ToList();
        public List<NewsArticle> GetNewsByAccountID(short accountId)
        {
            return _context.NewsArticles
            .Where(n => n.CreatedById == accountId) // Lọc theo AccountId
            .OrderByDescending(n => n.CreatedDate) // Sắp xếp theo ngày mới nhất
            .ToList();
        }

        public List<NewsArticle> StatisticNews(DateTime startDate, DateTime endDate)
        {
            return _context.NewsArticles
            .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
            .Include(x => x.CreatedBy)
            .Include(n => n.Category)
            .OrderByDescending(n => n.CreatedDate)
            .ToList();
        }
    }
}
