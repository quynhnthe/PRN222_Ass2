﻿using NewsManagementSystem_Assigment01.Models;
using NewsManagementSystem_Assigment01.ViewModel;

namespace NewsManagementSystem_Assigment01.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FunewsManagementContext _context;
        public AccountRepository(FunewsManagementContext context) 
        {
            _context = context;
        }

        public void AccountStatus(SystemAccount account)
        {
            account.IsActive = !account.IsActive;
            _context.SaveChanges();
        }

        public void CreateNewAccount(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
        }

        public SystemAccount? GetAccountById(short Id)
        {
            return _context.SystemAccounts.Find(Id);
        }

        public List<SystemAccount> GetListUser()
        {
            return _context.SystemAccounts.ToList();
        }

        public SystemAccount? SignIn(LoginViewModel model)
        {
            return _context.SystemAccounts.Where(x => (x.AccountEmail == model.Email) && (x.AccountPassword == model.Password)).FirstOrDefault();
        }
    }
}
